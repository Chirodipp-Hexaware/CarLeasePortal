#region

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using NLog;
using CarLeasePPT.DataAccessLayer;
using CarLeasePPT.Encryption;
using CarLeasePPT.Helpers;
using CarLeasePPT.Models;
using CarLeasePPT.Utility;

#endregion

namespace CarLeasePPT.Controllers
{
    [Authorize]
    [SuppressMessage("ReSharper", "StringLiteralTypo")]
    public class AccountController : BaseController
    {
        #region Constants

        private const string InvalidHash =
            "pbkdf2:G3Z22e6b8Ic+UloMa1QlVbQfDYjCd+4npZMJ/hScLHJHuBDUiDsVQaF9qHSx4vmJPAqQJkMbtuEipbCPtL7sKw==:40:DRn8igrPPk2Xq9i0XnFYQ+TSshyTJQZR3t88mS8XPf5lME4l6pX5O+++IqunU46zO2a42ZYqDog1BNtm8RSKpTwxqnCkxau5LN3ysyXr+dMWM2/yuNjFKzEGlFR2S4BL/hQLLzcdg+ezTS0O47FIF7Azec1znPhL+3MnboHaUlpwxbS07EG5kckOkbCaAzMfSVY0Niw4NVCgHphx33eQfLaqjHEVp4o7PCP+oCzmK1Fr3iJJcdgG8V6FZ1riTECZbipEA8oMFnxuQyfBMlnkpekmdhHjURNSFhAON7QLBFbAms7ZmyLAlmyG5YGi2N9KSUaCgCXv0ipoIQuQ1+EUsw==";

        private const string InvalidPassword = "1CE743BC4A514B5687C3BD26833E987F";

        #endregion

        #region Public Methods and Operators

        [AllowAnonymous]
        public ActionResult InvalidToken()
        {
            return View();
        }

        // GET: /Account/Login
        [AllowAnonymous]
        public async Task<ActionResult> Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            await CredentialResetEngine.DeletePersonSecurityResetsAsync();
            // Check to see if the user is already authenticated
            var personId = AuthenticationHelper.GetPersonIdentifier(AuthenticationManager);
            // If we have a PersonId in the cookie. If not, display the Login View
            if (personId.Equals(-1)) return View();
            // Get the ExternalUserRecord from the database
            var personRecord = await PersonEngine.GetAsync(personId);
            // If the record doesn't exist, display the Login View
            // If the user account is locked, display the Login View, otherwise, redirect the user to Home/Index
            if (personRecord != null && !personRecord.IsLocked &&
                (!personRecord.ExpirationDateTime.HasValue || personRecord.ExpirationDateTime.Value > DateTime.Now))
            {
                var logEvent = new LogEventInfo(LogLevel.Info, "AuditLog", "Log In");
                logEvent.Properties.Add("PersonId", personId);
                logEvent.Properties.Add("FullName", personRecord.FullName);
                Audit.Instance.Log(logEvent);
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }

            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie,
                DefaultAuthenticationTypes.TwoFactorCookie);
            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            //var ipAddressBytes = IpAddressHelper.GetIpAddressBytes(System.Web.HttpContext.Current);

            var userRecord = await PersonEngine.GetPersonAuthenticationRecordAsync(model.EmailAddress);
            //var userRecord = await PersonEngine.GetAsync(model.EmailAddress);
            if (userRecord == null)
            {
                var logEvent = new LogEventInfo(LogLevel.Error, "AuditLog", "Invalid Token Request. Unknown User.");
                logEvent.Properties.Add("FullName", model.EmailAddress);
                Audit.Instance.Log(logEvent);
                //Hash a made up password/hash string to ensure that the processing for an invalid username is the same as a valid username.
                Pbkdf2.Verify(InvalidPassword, InvalidHash);
                ModelState.AddModelError("", Resources.Account_Login_InvalidCredentials_Message);
                return View(model);
            }

            var isBlockedResult = await PersonEngine.GetPersonBlockResultAsync(userRecord.PersonId);
            if (isBlockedResult.IsBlocked)
            {
                var exp = isBlockedResult.ExpirationDateTime;
                var tzi = TimeZoneInfo.Local;
                AuthenticationHelper.SetBlockCookie(System.Web.HttpContext.Current, new BlockedPersonViewModel
                {
                    ExpirationDateTime = exp,
                    TimeZone = tzi.IsDaylightSavingTime(exp) ? tzi.DaylightName : tzi.StandardName
                });
                return RedirectToAction("RestrictedUser", "AccessDenied");
            }

            if (userRecord.IsExpired)
            {
                var logEvent = new LogEventInfo(LogLevel.Warn, "AuditLog",
                    "Invalid Token Request. Account is Expired.");
                logEvent.Properties.Add("PersonId", userRecord.PersonId);
                logEvent.Properties.Add("FullName", userRecord.FullName);
                Audit.Instance.Log(logEvent);
                ModelState.AddModelError("", Resources.Account_Login_CredentialsExpiredStatus_Message);
                Pbkdf2.Verify(InvalidPassword, InvalidHash);
                return View(model);
            }

            if (userRecord.IsLocked)
            {
                // Simulate token request (for proper delay)
                AuthenticationHelper.SimulateRequestToken();
                var logEvent = new LogEventInfo(LogLevel.Error, "AuditLog",
                    "Invalid Token Request. Account is Locked.");
                logEvent.Properties.Add("PersonId", userRecord.PersonId);
                logEvent.Properties.Add("FullName", userRecord.FullName);
                Audit.Instance.Log(logEvent);
                ModelState.AddModelError("", Resources.Account_Login_CredentialsLockedStatus_Message);
                Pbkdf2.Verify(InvalidPassword, InvalidHash);
                return View(model);
            }

            var isValid = Pbkdf2.Verify(model.Password, userRecord.SecurityValue);
            if (isValid)
            {
                var person = await PersonEngine.GetAsync(userRecord.PersonId);
                if (person == null)
                    return RedirectToAction("AuthenticationError", "AccessDenied");
                person.PasswordExpiration = userRecord.PersonSecurityExpirationDateTime;
                await SignInPerson(person);
                return string.IsNullOrEmpty(returnUrl)
                    ? RedirectToAction("Index", "Home", new {area = ""})
                    : RedirectToLocal(returnUrl);
            }

            await AuthenticationFailureEngine.AddAuthenticationFailureRecordAsync(userRecord.PersonId);
            // Check for person blocked after adding a failure record.
            isBlockedResult = await PersonEngine.GetPersonBlockResultAsync(userRecord.PersonId);
            if (isBlockedResult.IsBlocked)
            {
                var exp = isBlockedResult.ExpirationDateTime;
                var tzi = TimeZoneInfo.Local;
                AuthenticationHelper.SetBlockCookie(System.Web.HttpContext.Current, new BlockedPersonViewModel
                {
                    ExpirationDateTime = exp,
                    TimeZone = tzi.IsDaylightSavingTime(exp) ? tzi.DaylightName : tzi.StandardName
                });
                return RedirectToAction("RestrictedUser", "AccessDenied");
            }

            ModelState.AddModelError("", Resources.Account_Login_InvalidCredentials_Message);
            return View(model);
        }

        // GET: /Account/Logout
        public async Task<ActionResult> Logout()
        {
            var userIdentityName = User.Identity.Name ?? "Unauthenticated User";
            var personId = AuthenticationHelper.GetPersonIdentifier(AuthenticationManager);
            var logEvent = new LogEventInfo(LogLevel.Info, "AuditLog", "Log Out");
            logEvent.Properties.Add("PersonId", personId);
            logEvent.Properties.Add("FullName", userIdentityName);
            Audit.Instance.Log(logEvent);
            await SessionTokenHelper.RemoveToken(personId);
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie,
                DefaultAuthenticationTypes.TwoFactorCookie);
            return RedirectToAction(nameof(Login));
        }

        [AllowAnonymous]
        public ActionResult PasswordReset()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> PasswordReset(PasswordResetViewModel model)
        {
            if (!ModelState.IsValid || string.IsNullOrEmpty(model.Email))
            {
                ModelState.AddModelError("", Resources.Helper_ValidatePasswordReset_RequiredValidation_Message);
                return View(model);
            }

            var baseUri = HttpContext.Request.Url;
            if (baseUri == null) return View("TokenRequested");
            var urlData = Url.Action("RedeemToken", "Account", null, baseUri.Scheme);
            await AuthenticationHelper.ResetPassword(model.Email, urlData);
            return View("TokenRequested");
        }

        [AllowAnonymous]
        public async Task<ActionResult> RedeemToken(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) RedirectToAction("Login");
            var tokenId = await AuthenticationHelper.CheckPasswordResetTokenAsync(id);
            if (tokenId.Equals(-1)) return RedirectToAction("InvalidToken");
            var personId = await CredentialResetEngine.FindPersonBySecurityResetTokenAsync(tokenId);
            if (personId.Equals(-1)) return RedirectToAction("InvalidToken");
            var identity = new ClaimsIdentity(DefaultAuthenticationTypes.TwoFactorCookie);
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, $"{personId}"));
            AuthenticationManager.SignIn(identity);
            return RedirectToAction("SetPassword");
        }

        public ActionResult SessionTerminated()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie,
                DefaultAuthenticationTypes.TwoFactorCookie);
            return View();
        }

        [AllowAnonymous]
        public ActionResult SetPassword()
        {
            var policyCollection = PasswordSecurityPolicy.GetPasswordPolicyList();
            var model = new SetPasswordViewModel
            {
                PasswordSecurityPolicyCollection = policyCollection
            };
            return View(model);
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SetPassword(SetPasswordViewModel model)
        {
            var personId = await GetTwoFactorPersonIdAsync();
            if (personId.Equals(-1))
                return RedirectToAction("Login");
            // Redeem all outstanding tokens for this person
            await CredentialResetEngine.RedeemPersonSecurityResetTokensAsync(personId);
            Collection<string> policyCollection;
            if (!ModelState.IsValid)
            {
                policyCollection = PasswordSecurityPolicy.GetPasswordPolicyList();
                model.PasswordSecurityPolicyCollection = policyCollection;
                return View(model);
            }

            var passwordStandardsFailureList =
                await AuthenticationHelper.PasswordMeetsMinimumRequirementsAsync(personId, model.NewPassword);
            if (!passwordStandardsFailureList.Any())
            {
                if (await AuthenticationHelper.UpdatePasswordAsync(personId, model.NewPassword))
                    return RedirectToAction("SetPasswordSuccess");
                ModelState.AddModelError("", Resources.Settings_Password_PasswordCouldNotBeChanged_Message);
                return View(model);
            }

            foreach (var s in passwordStandardsFailureList) ModelState.AddModelError("", s);
            policyCollection = PasswordSecurityPolicy.GetPasswordPolicyList();
            model.PasswordSecurityPolicyCollection = policyCollection;
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult SetPasswordSuccess()
        {
            return View();
        }


        [AllowAnonymous]
        public ActionResult TokenRequested()
        {
            return View();
        }

        #endregion

        #region Methods

        private async Task<int> GetTwoFactorPersonIdAsync()
        {
            var result = await AuthenticationManager.AuthenticateAsync(DefaultAuthenticationTypes.TwoFactorCookie);
            var userId = !string.IsNullOrEmpty(result?.Identity?.GetUserId()) ? result.Identity.GetUserId() : null;
            return int.TryParse(userId, out var personId) ? personId : -1;
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);
            return RedirectToAction("Index", "Home", new {area = ""});
        }

        private async Task SignInPerson(PersonRecord person)
        {
            try
            {
                var sessionToken = Convert.ToBase64String(AesGcm.NewKey());
                await SessionTokenHelper.AddToken(person.PersonId, sessionToken);
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, person.FullName),
                    new Claim(ClaimTypes.NameIdentifier, $"{person.PersonId}"),
                    new Claim(ClaimTypes.Email, person.EmailAddress),
                    new Claim(CustomClaimTypes.SessionToken, sessionToken),
                    new Claim(CustomClaimTypes.PasswordExpiration, $"{person.PasswordExpiration:s}")
                };

                var personRoleRecords = await PersonEngine.GetRoleRecordsAsync(person.PersonId);
                claims.AddRange(personRoleRecords.Select(r => new Claim(ClaimTypes.Role, r.RoleCode)));

                var id = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);
                AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie,
                    DefaultAuthenticationTypes.TwoFactorCookie);
                var ap = new AuthenticationProperties
                {
                    IsPersistent = true
                };
                AuthenticationManager.SignIn(ap, id);
                await AuthenticationFailureEngine.ClearAuthenticationFailureRecordAsync(person.PersonId);
            }
            catch (Exception exception)
            {
                Log.Instance.Error(exception);
            }
        }

        #endregion
    }
}