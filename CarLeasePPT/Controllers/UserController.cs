using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using CarLeasePPT.DataAccessLayer;
using CarLeasePPT.Filters;
using CarLeasePPT.Helpers;
using CarLeasePPT.Models;
using CarLeasePPT.Utility;

namespace CarLeasePPT.Controllers
{
    [Authorize]
    [VerifyPersonSession]
    [AuthorizeRole(Roles = "USER_MANAGEMENT")]
    public class UserController : BaseController
    {
        #region Public Methods and Operators

        public async Task<ActionResult> Create()
        {
            ViewBag.RoleList = await RoleEngine.GetCollectionAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(UserCreateViewModel model, FormCollection collection)
        {
            try
            {
                var roleList = await RoleEngine.GetCollectionAsync();
                ViewBag.RoleList = roleList;
                if (!ModelState.IsValid) return View(model);

                var myId = AuthenticationHelper.GetPersonIdentifier(AuthenticationManager);
                if (await PersonEngine.ExistsEmailAsync(model.EmailAddress))
                {
                    ModelState.AddModelError("", Resources.User_Common_EmailExists_Message);
                    return View(model);
                }

                var newPersonId =
                    await PersonEngine.CreateAsync(myId, model.FullName, model.PreferredName, model.EmailAddress);

                if (!newPersonId.Equals(-1))
                {
                    var roleIdList = new List<int>();
                    var roleCodeList = roleList.Select(r => r.RoleCode).ToList();
                    foreach (var name in collection.AllKeys)
                    {
                        if (!roleCodeList.Contains(name)) continue;
                        var value = collection[name].ToUpperInvariant();
                        if (!value.Contains("TRUE") && !value.Contains("ON")) continue;
                        var roleId = roleList.SingleOrDefault(r => r.RoleCode.Equals(name))?.RoleId ?? 0;
                        if (!roleId.Equals(0)) roleIdList.Add(roleId);
                    }

                    if (roleIdList.Any())
                    {
                        var addRoles = await PersonEngine.SetPersonRoleRecordsAsync(roleIdList, newPersonId, myId);
                        if (!addRoles)
                            ModelState.AddModelError("", Resources.User_Create_RoleSaveFailure_Message);
                    }

                    var baseUri = HttpContext.Request.Url;
                    if (baseUri == null)
                    {
                        ModelState.AddModelError("", Resources.User_Create_PasswordResetFailure_Message);
                        return View(model);
                    }

                    var redeemTokenUrl = Url.Action("RedeemToken", "Account", new {area = ""}, baseUri.Scheme);
                    var resetResult =
                        await AuthenticationHelper.NewUserResetAsync(newPersonId, redeemTokenUrl);
                    if (!resetResult)
                        ModelState.AddModelError("", Resources.User_Create_PasswordResetFailure_Message);
                }
                else
                {
                    ModelState.AddModelError("", Resources.User_Create_SaveFailure_Message);
                }

                if (ModelState.IsValid)
                    return RedirectToAction("Index");
                return View(model);
            }
            catch
            {
                ModelState.AddModelError("", Resources.User_Create_SaveFailure_Message);
                return View(model);
            }
        }

        public async Task<ActionResult> Delete(int id = -1)
        {
            if (id.Equals(-1)) return RedirectToAction("Index");
            var modifyingPersonId = AuthenticationHelper.GetPersonIdentifier(AuthenticationManager);
            // Don't allow the user to delete themselves.
            if (id.Equals(modifyingPersonId)) return RedirectToAction("Index");
            // Get the person record we're editing
            var p = await PersonEngine.GetAsync(id);
            if (p == null || p.IsExpired)
                return RedirectToAction("Index");
            ViewBag.PersonFullName = p.FullName;
            ViewBag.EmailAddress = p.EmailAddress;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        // ReSharper disable once UnusedParameter.Global
        public async Task<ActionResult> Delete(FormCollection collection, int id = -1)
        {
            try
            {
                if (id.Equals(-1)) return RedirectToAction("Index");
                var myId = AuthenticationHelper.GetPersonIdentifier(AuthenticationManager);
                // Don't allow the user to delete themselves.
                if (id.Equals(myId)) return RedirectToAction("Index");
                // Get the person record we're editing
                var p = await PersonEngine.GetAsync(id);
                if (p == null || p.IsExpired)
                    return RedirectToAction("Index");
                ViewBag.PersonFullName = p.FullName;
                await PersonEngine.DeleteAsync(p.PersonId, myId);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult> Detail(int id = -1)
        {
            if (id.Equals(-1)) return RedirectToAction("Index");
            var p = await PersonEngine.GetAsync(id);
            if (p == null)
                return RedirectToAction("Index");
            var roles = await PersonEngine.GetPersonRoleRecordsAsync(id);
            var model = new UserDetailViewModel
            {
                AssignedRoles = roles.OrderBy(r => r.RoleName).Select(r => r.RoleName).ToList(),
                EmailAddress = p.EmailAddress,
                EffectiveDateTime = p.EffectiveDateTime,
                ExpirationDateTime = p.ExpirationDateTime,
                FullName = p.FullName,
                PersonId = p.PersonId,
                PreferredName = p.PreferredName,
                IsExpired = p.IsExpired,
                IsLocked = p.IsLocked
            };
            return View(model);
        }

        public async Task<ActionResult> Edit(int id = -1)
        {
            if (id.Equals(-1)) return RedirectToAction("Index");
            var p = await PersonEngine.GetAsync(id);
            if (p == null || p.IsExpired)
                return RedirectToAction("Index");

            var isInternaluser = p.EmailAddress.EndsWith(PersonEngine.InternalUserEmailDomain);

            ViewBag.RoleList = await RoleEngine.GetCollectionAsync(isInternaluser);

            var personRoleListReadOnly = await PersonEngine.GetPersonRoleIdsAsync(p.PersonId);

            var model = new UserEditViewModel
            {
                EmailAddress = p.EmailAddress,
                FullName = p.FullName,
                PersonId = p.PersonId,
                PreferredName = p.PreferredName,
                PersonRoleIds = personRoleListReadOnly
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(UserEditViewModel model, FormCollection collection, int id = -1)
        {
            try
            {
                if (id.Equals(-1)) return RedirectToAction("Index");
                var myId = AuthenticationHelper.GetPersonIdentifier(AuthenticationManager);
                var p = await PersonEngine.GetAsync(id);
                if (p == null || p.IsExpired)
                    return RedirectToAction("Index");
                var isInternaluser = model.EmailAddress.EndsWith(PersonEngine.InternalUserEmailDomain);
                var roleList = await RoleEngine.GetCollectionAsync(isInternaluser);
                ViewBag.RoleList = roleList;

                var personRoleListReadOnly = await PersonEngine.GetPersonRoleIdsAsync(p.PersonId);
                model.PersonRoleIds = personRoleListReadOnly;
                if (!ModelState.IsValid) return View(model);

                if (!p.EmailAddress.Equals(model.EmailAddress) &&
                    await PersonEngine.ExistsEmailAsync(p.PersonId, model.EmailAddress))
                {
                    ModelState.AddModelError("", Resources.User_Common_EmailExists_Message);
                    return View(model);
                }

                var userRecordSaveResult = await PersonEngine.UpdatePersonRecordAsync(myId, p.PersonId, model.FullName,
                    model.PreferredName, model.EmailAddress);
                if (!userRecordSaveResult)
                    ModelState.AddModelError("", Resources.User_Edit_SaveFailure_Message);

                var roleIdList = new List<int>();
                var roleCodeList = roleList.Select(r => r.RoleCode).ToList();
                foreach (var name in collection.AllKeys)
                {
                    if (!roleCodeList.Contains(name)) continue;
                    var value = collection[name].ToUpperInvariant();
                    if (!value.Contains("TRUE") && !value.Contains("ON")) continue;
                    var roleId = roleList.SingleOrDefault(r => r.RoleCode.Equals(name))?.RoleId ?? 0;
                    if (!roleId.Equals(0)) roleIdList.Add(roleId);
                }

                var rolesToAdd = roleIdList.Except(personRoleListReadOnly).ToList().AsReadOnly();
                var rolesToRemove = personRoleListReadOnly.Except(roleIdList).ToList().AsReadOnly();

                if (rolesToRemove.Any())
                {
                    var removeRoles =
                        await PersonEngine.RemovePersonRoleRecordsAsync(rolesToRemove, p.PersonId, myId);
                    if (!removeRoles)
                        ModelState.AddModelError("", Resources.User_Edit_RolesNotRemoved_Message);
                }

                if (rolesToAdd.Any())
                {
                    var addRoles = await PersonEngine.SetPersonRoleRecordsAsync(rolesToAdd, p.PersonId,
                        myId);
                    if (!addRoles)
                        ModelState.AddModelError("", Resources.User_Edit_RolesNotAdded_Message);
                }

                if (!ModelState.IsValid)
                    return View(model);
                return RedirectToAction("Detail", new {id = p.PersonId});
            }
            catch (Exception exception)
            {
                ModelState.AddModelError("", Resources.User_Edit_SaveFailure_Message);
                Log.Instance.Error(exception);
                return View(model);
            }
        }

        public ActionResult Index()
        {
            var myId = AuthenticationHelper.GetPersonIdentifier(AuthenticationManager);
            ViewBag.MyId = myId;
            return View();
        }

        public async Task<ActionResult> Unlock(int id = -1)
        {
            if (id.Equals(-1)) return RedirectToAction("Index");
            var myId = AuthenticationHelper.GetPersonIdentifier(AuthenticationManager);
            var p = await PersonEngine.GetAsync(id);
            if (p == null || p.IsExpired) return RedirectToAction("Index");
            if (!p.IsLocked) return RedirectToAction("Detail", new {id});
            // CLear authentication failures, if any
            await AuthenticationFailureEngine.ClearAuthenticationFailureRecordAsync(id);
            // Override blocks, if any
            await AuthenticationFailureEngine.ClearBlockRecordAsync(id, myId);
            await PersonEngine.UnlockUser(id, myId);
            return RedirectToAction("Detail", new {id});
        }

        #endregion
    }
}