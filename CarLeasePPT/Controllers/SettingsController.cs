using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.Owin.Security;
using CarLeasePPT.DataAccessLayer;
using CarLeasePPT.Encryption;
using CarLeasePPT.Filters;
using CarLeasePPT.Helpers;
using CarLeasePPT.Models;
using CarLeasePPT.Utility;

namespace CarLeasePPT.Controllers
{
    [Authorize]
    [VerifyPersonSession]
    public class SettingsController : BaseController
    {
        #region Public Methods and Operators

        public async Task<ActionResult> Account()
        {
            try
            {
                var userId = AuthenticationHelper.GetPersonIdentifier(AuthenticationManager);
                var person = await PersonEngine.GetAsync(userId);
                var roles = await PersonEngine.GetPersonRoleRecordsAsync(userId);
                var model = new PersonAccountViewModel
                {
                    PersonId = person.PersonId,
                    EmailAddress = person.EmailAddress,
                    FullName = person.FullName,
                    PreferredName = person.PreferredName,
                    UserRoles = roles.Select(r => r.RoleName).ToList().AsReadOnly()
                };
                return View(model);
            }
            catch (Exception exception)
            {
                Log.Instance.Error(exception);
            }

            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<ActionResult> Account(PersonAccountViewModel model)
        {
            var userId = AuthenticationHelper.GetPersonIdentifier(AuthenticationManager);
            var person = await PersonEngine.GetAsync(userId);
            var roles = await PersonEngine.GetPersonRoleRecordsAsync(userId);
            model.PersonId = person.PersonId;
            model.EmailAddress = person.EmailAddress;
            model.FullName = person.FullName;
            model.UserRoles = roles.Select(r => r.RoleName).ToList().AsReadOnly();
            if (!ModelState.IsValid) return View(model);
            var result =
                await PersonEngine.UpdatePersonProfile(AuthenticationHelper.GetPersonIdentifier(AuthenticationManager),
                    model.PreferredName);
            if (!result)
            {
                ModelState.AddModelError("", Resources.Settings_MyProfile_UnableToUpdateError_Message);
            }
            else
            {
                var identity = new ClaimsIdentity(User.Identity);
                identity.RemoveClaim(identity.FindFirst(ClaimTypes.Name));
                identity.AddClaim(new Claim(ClaimTypes.Name, model.PreferredName));
                AuthenticationManager.AuthenticationResponseGrant =
                    new AuthenticationResponseGrant(new ClaimsPrincipal(identity),
                        new AuthenticationProperties {IsPersistent = false});
            }

            return View(model);
        }


        public async Task<ActionResult> Password()
        {
            try
            {
                var personId = AuthenticationHelper.GetPersonIdentifier(AuthenticationManager);
                var personSecurityRecord = await PersonSecurityEngine.GetSecurityRecord(personId);
                var policyCollection = PasswordSecurityPolicy.GetPasswordPolicyList();
                var model = new SecurityPasswordViewModel
                {
                    EffectiveDateTime = personSecurityRecord.EffectiveDateTime,
                    ExpirationDateTime = personSecurityRecord.ExpirationDateTime,
                    PasswordSecurityPolicyCollection = policyCollection
                };
                return View(model);
            }
            catch (Exception exception)
            {
                Log.Instance.Error(exception);
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Password(SecurityPasswordViewModel model)
        {
            var personId = AuthenticationHelper.GetPersonIdentifier(AuthenticationManager);
            var personSecurityRecord = await PersonSecurityEngine.GetSecurityRecord(personId);
            model.EffectiveDateTime = personSecurityRecord.EffectiveDateTime;
            model.ExpirationDateTime = personSecurityRecord.ExpirationDateTime;
            model.PasswordSecurityPolicyCollection = PasswordSecurityPolicy.GetPasswordPolicyList();
            if (!ModelState.IsValid) return View(model);
            if (Pbkdf2.Verify(model.Password, personSecurityRecord.SecurityValue))
            {
                var passwordStandardsFailureList =
                    await AuthenticationHelper.PasswordMeetsMinimumRequirementsAsync(personId, model.NewPassword);
                if (!passwordStandardsFailureList.Any())
                {
                    if (await AuthenticationHelper.UpdatePasswordAsync(personId, model.NewPassword))
                    {
                        personSecurityRecord = await PersonSecurityEngine.GetSecurityRecord(personId);
                        model.EffectiveDateTime = personSecurityRecord.EffectiveDateTime;
                        model.ExpirationDateTime = personSecurityRecord.ExpirationDateTime;
                        model.PasswordSecurityPolicyCollection = PasswordSecurityPolicy.GetPasswordPolicyList();
                        model.IsSuccess = true;

                        // Reset the user's application cookie to update expiration data
                        AuthenticationHelper.UpdatePasswordExpirationClaim(AuthenticationManager,
                            personSecurityRecord.ExpirationDateTime);
                        return View(model);
                    }

                    ModelState.AddModelError("", Resources.Settings_Password_PasswordCouldNotBeChanged_Message);
                    return View(model);
                }

                foreach (var s in passwordStandardsFailureList)
                    ModelState.AddModelError("", s);
                return View(model);
            }

            ModelState.AddModelError("", Resources.Account_Login_InvalidCredentials_Message);
            return View(model);
        }

        #endregion
    }
}