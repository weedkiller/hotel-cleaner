using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using NawafizApp.Web.Models;
using NawafizApp.Services.Identity;
using NawafizApp.Common;
using NawafizApp.Services.Dtos;
using FluentValidation.Mvc;
using NawafizApp.Services.Interfaces;

namespace NawafizApp.Web.Controllers
{
    [Authorize]
    public class AccountController : BaseAuthorizeController
    {
        IUserService _UserService;
        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager, IUserService us)
            : base(userManager, signInManager)
        {
            _UserService = us;
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {

            SetANewRequestVerificationTokenManuallyInCookieAndOnTheForm();
            
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
        private void SetANewRequestVerificationTokenManuallyInCookieAndOnTheForm()
        {
            if (Response == null)
                return;

            string cookieToken, formToken;
            System.Web.Helpers.AntiForgery.GetTokens(null, out cookieToken, out formToken);
            SetCookie("__RequestVerificationToken", cookieToken);
            ViewBag.FormToken = formToken;
            ViewBag.yy = "qwe";
        }
        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]

        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Require the user to have a confirmed email before they can log on.

            /*  if (user != null)
              {
                  if (!await UserManager.IsEmailConfirmedAsync(user.Id))
                  {
                      ViewBag.errorMessage = "You must have a confirmed email to log on.";
                      return View("Error");
                  }
              }
              */
            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(model.UserName, model.Password, false, shouldLockout: true);

            switch (result)
            {
                case SignInStatus.Success:
                    // await UserManager.ResetAccessFailedCountAsync(user.Id);
                    var user = await UserManager.FindByNameAsync(model.UserName);
                    if (user != null)
                    {
                        //add http cookie 
                        System.Web.HttpContext.Current.Session["userId"] = user.Id.ToString();
                        System.Web.HttpContext.Current.Session["userRoles"] = _UserService.Roles(user.Id);
                        HttpCookie usernameCookie = new HttpCookie("username", user.UserName);
                        HttpCookie userIdCookie = new HttpCookie("userId", user.Id.ToString());
                        usernameCookie.Expires = DateTime.Now.AddDays(1);
                        userIdCookie.Expires = DateTime.Now.AddDays(1);
                        HttpContext.Response.Cookies.Add(userIdCookie);
                        HttpContext.Response.Cookies.Add(usernameCookie);

                        if (user.PassWordExpired)
                        {
                            var ErrorMessage = "انتهت صلاحية كلمة المرور الرجاء اعادة تعيين كلمة المرور";
                            return RedirectToAction("ChangePassword", "Account", new { ErrorMessage = ErrorMessage });
                        }
                    }
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "خطأ في كلمة المرور أو اسم المستخدم");

                    return View(model);
            }

        }


        private void SetCookie(string name, string value)
        {
            if (Response.Cookies.AllKeys.Contains(name))
                Response.Cookies[name].Value = value;
            else
                Response.Cookies.Add(new HttpCookie(name, value));
        }
        [Authorize(Roles = "HouseKeep,Reception,Admin,Hoster,service,MaintenanceEmp,BlockSupervisor,Cleaner")]
        public ActionResult ChangePassword(String ErrorMessage)
        {
            var id = getGuid(User.Identity.GetUserId());
            var model = _UserService.GetById(id);
            ChangePasswordDto dto = new ChangePasswordDto();
            ViewBag.ErrorMessage = ErrorMessage;
            dto.fullname = model.FullName;
            dto.UserName = model.UserName;
            dto.Phone = model.PhoneNumber;
            dto.Email = model.Email;

            return View(dto);
        }
        [Authorize(Roles = "HouseKeep,Reception,Admin,Hoster,service,MaintenanceEmp,BlockSupervisor,Cleaner")]
        [RuleSetForClientSideMessages("Edit")]
        public ActionResult Edit(Guid id)
        {
            if (_UserService.Exists(id))
            {
                var dto = _UserService.GetById(id);
                ViewBag.userRole = dto.role;

                return View(dto);
            }
            return RedirectToAction("index", "Home");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "HouseKeep,Reception,Admin,Hoster,service,MaintenanceEmp,BlockSupervisor,Cleaner")]
        [RuleSetForClientSideMessages("Edit")]
        public ActionResult Edit(Guid id, [CustomizeValidator(RuleSet = "Edit")] RegisterUserDto dto, string oldRole, HttpPostedFileBase postedFile, HttpPostedFileBase postedFile1)
        {
            if (dto.wantEditPassword)
            {
                if (!dto.Password.Equals(dto.ConfirmPassword)) { ViewBag.pass = "الكلمتان غير متطابقتان"; return View(); }
                if (dto.Password.Length < 6) { ViewBag.pass = "طول كلمة المرور 6 محارف"; return View(); }

                var x = UserManager.RemovePassword(id);
                if (x.Succeeded)
                {
                    var y = UserManager.AddPassword(id, dto.Password);
                    if (!y.Succeeded) { ViewBag.pass = "حدثت مشكلة لم يتم التعديل بنجاح"; return View(); }
                }
                else
                {
                    ViewBag.pass = "حدثت مشكلة لم يتم التعديل بنجاح"; return View();
                }
            }
            string uploadPath = System.Configuration.ConfigurationManager.AppSettings["uploadPathEditeAccount"];
            string ext = "";
            if (postedFile != null)
            {
                //Extract Image File Name.
                string fileName1 = System.IO.Path.GetFileNameWithoutExtension(postedFile.FileName);
                ext = System.IO.Path.GetExtension(postedFile.FileName);
                fileName1 = fileName1 + Guid.NewGuid();
                //Set the Image File Path.
                string filePath = uploadPath + fileName1 + ext;

                //Save the Image File in Folder.
                postedFile.SaveAs(Server.MapPath(filePath));
                dto.Image = fileName1 + ext;
            }
            if (postedFile1 != null)
            {

                //Extract Image File Name.
                string fileName2 = System.IO.Path.GetFileNameWithoutExtension(postedFile1.FileName);
                ext = System.IO.Path.GetExtension(postedFile1.FileName);
                fileName2 = fileName2 + Guid.NewGuid();
                //Set the Image File Path.
                string filePath = uploadPath + fileName2 + ext;

                //Save the Image File in Folder.
                postedFile.SaveAs(Server.MapPath(filePath));
                dto.Contract = fileName2 + ext;
            }
            string s = "";

            var rRul = new IdentityResult();
            if (!String.IsNullOrEmpty(oldRole))
            {
                if (oldRole.Trim().ToLower() != dto.role.Trim().ToLower())
                {
                    rRul = UserManager.RemoveFromRole(dto.UserId, oldRole);
                    if (rRul.Succeeded)
                    {
                        var adRul = UserManager.AddToRole(dto.UserId, dto.role);
                    }
                    else
                    {
                        s = rRul.Errors.FirstOrDefault();
                    }
                }
            }
            if (String.IsNullOrEmpty(oldRole))
            {
                var adRul = UserManager.AddToRole(dto.UserId, dto.role);
            }



            if (_UserService.Edit(dto, id))
            {
                return RedirectToAction("getAllUsers");
            }


            return RedirectToAction("Error");
        }
        [Authorize(Roles = "HouseKeep,Reception,Admin,Hoster,service,MaintenanceEmp,BlockSupervisor,Cleaner")]
        public ActionResult Error()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "HouseKeep,Reception,Admin,Hoster,service,MaintenanceEmp,BlockSupervisor,Cleaner")]
        public ActionResult ChangePassword(ChangePasswordDto dto)
        {
            var id = getGuid(User.Identity.GetUserId());

            if (dto.wantEditpassword)
            {


                var u = UserManager.ChangePassword(id, dto.oldPassword, dto.newPassword);
                if (u.Succeeded)
                {
                    var user = _UserService.GetById(id);
                    RegisterUserDto userDto = new RegisterUserDto();
                    userDto.PassWordExpired = false;
                    var result = _UserService.Edit(userDto, id);
                    return RedirectToAction("index", "Home");

                }
                else
                {
                    ViewBag.error = "حدث خطأ تأكد من كلمة المرور الخاصة بك وكون الكلمة الجديدة من 6 حروف";
                    return View(dto);
                }
            }
            _UserService.editForAdm(id, dto.fullname, dto.UserName);
            return RedirectToAction("index", "Home");
        }

        //
        // GET: /Account/VerifyCode
        [AllowAnonymous]
        public async Task<ActionResult> VerifyCode(string provider, string returnUrl, bool rememberMe)
        {
            // Require that the user has already logged in via username/password or external login
            if (!await SignInManager.HasBeenVerifiedAsync())
            {
                return View("Error");
            }
            return View(new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/VerifyCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyCode(VerifyCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // The following code protects for brute force attacks against the two factor codes. 
            // If a user enters incorrect codes for a specified amount of time then the user account 
            // will be locked out for a specified amount of time. 
            // You can configure the account lockout settings in IdentityConfig
            var result = await SignInManager.TwoFactorSignInAsync(model.Provider, model.Code, isPersistent: model.RememberMe, rememberBrowser: model.RememberBrowser);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(model.ReturnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid code.");
                    return View(model);
            }
        }
        [AllowAnonymous]
        public ActionResult Remove(Guid id)
        {
            bool o = _UserService.Delete(id);
            return RedirectToAction("getAllUsers");
        }
        //
        // GET: /Account/Register

        [RuleSetForClientSideMessages("Register")]
        [Authorize(Roles = "HouseKeep,Reception,Admin,Hoster,service,MaintenanceEmp,BlockSupervisor,Cleaner")]
        public ActionResult Register()
        {
            ViewBag.t = false;
            return View();
        }
        [AllowAnonymous]
        public ActionResult getAllUsers()
        {
            return View(_UserService.GetAll().Where(x => !_UserService.HasRole(x.UserId, "Admin")));
        }
        //
        // POST: /Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "HouseKeep,Reception,Admin,Hoster,service,MaintenanceEmp,BlockSupervisor,Cleaner")]
        public async Task<ActionResult> Register([CustomizeValidator(RuleSet = "Register")] RegisterUserDto model, string role, HttpPostedFileBase postedFile, HttpPostedFileBase postedFile1)
        {
            if (ModelState.IsValid)
            {
                string uploadPath = System.Configuration.ConfigurationManager.AppSettings["uploadPath"];
                string ext = "";
                if (postedFile != null)
                {
                    //Extract Image File Name.
                    string fileName1 = System.IO.Path.GetFileNameWithoutExtension(postedFile.FileName);
                    ext = System.IO.Path.GetExtension(postedFile.FileName);
                    fileName1 = fileName1 + Guid.NewGuid();
                    //Set the Image File Path.
                    string filePath = uploadPath + fileName1 + ext;

                    //Save the Image File in Folder.
                    postedFile.SaveAs(Server.MapPath(filePath));
                    model.Image = fileName1 + ext;
                }
                if (postedFile1 != null)
                {

                    //Extract Image File Name.
                    string fileName2 = System.IO.Path.GetFileNameWithoutExtension(postedFile1.FileName);
                    ext = System.IO.Path.GetExtension(postedFile1.FileName);
                    fileName2 = fileName2 + Guid.NewGuid();
                    //Set the Image File Path.
                    string filePath = uploadPath + fileName2 + ext;

                    //Save the Image File in Folder.
                    postedFile.SaveAs(Server.MapPath(filePath));
                    model.Contract = fileName2 + ext;
                }
                var user = new IdentityUser { UserName = model.UserName, FullName = model.FullName, Image = model.Image, Contract = model.Contract, PassWordExpired = true, NationalNum = model.NationalNum, Mobile = model.Mobile, PhoneNumber = model.PhoneNumber, Email = model.Email };
                user.CreationDate = Utils.ServerNow;
                var result = await UserManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    var assignRuleResult = UserManager.AddToRole(user.Id, role);

                    //await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    // Send an email with this link
                    //string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);

                    //var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    //await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                    //// Email Confirmation
                    //TempData["ViewBagLink"] = callbackUrl;
                    //ViewBag.Message = "Check your email and confirm your account, you must be confirmed "
                    //     + "before you can log in.";
                    //return View("Info");
                    //

                    return RedirectToAction("Index", "Home");
                }
                AddErrors(result);
                ViewBag.t = true;
                ViewBag.err = result.Errors.ToList();
                return View();
            }

            // If we got this far, something failed, redisplay form

            return View(model);
        }

        //
        // GET: /Account/ConfirmEmail
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var result = await UserManager.ConfirmEmailAsync(getGuid(userId), code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(model.Email);
                if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return View("ForgotPasswordConfirmation");
                }

                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                // string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                // var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);		
                // await UserManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");
                // return RedirectToAction("ForgotPasswordConfirmation", "Account");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        //
        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            return code == null ? View("Error") : View();
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await UserManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            AddErrors(result);
            return View();
        }

        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        //
        // GET: /Account/SendCode
        [AllowAnonymous]
        public async Task<ActionResult> SendCode(string returnUrl, bool rememberMe)
        {
            var userId = await SignInManager.GetVerifiedUserIdAsync();
            if (userId == null)
            {
                return View("Error");
            }
            var userFactors = await UserManager.GetValidTwoFactorProvidersAsync(userId);
            var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
            return View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/SendCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendCode(SendCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            // Generate the token and send it
            if (!await SignInManager.SendTwoFactorCodeAsync(model.SelectedProvider))
            {
                return View("Error");
            }
            return RedirectToAction("VerifyCode", new { Provider = model.SelectedProvider, ReturnUrl = model.ReturnUrl, RememberMe = model.RememberMe });
        }

        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            // Sign in the user with this external login provider if the user already has a login
            var result = await SignInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = false });
                case SignInStatus.Failure:
                default:
                    // If the user does not have an account, then prompt the user to create an account
                    ViewBag.ReturnUrl = returnUrl;
                    ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                    return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = loginInfo.Email });
            }
        }

        //
        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]

        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Manage");
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new IdentityUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await UserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        //
        // POST: /Account/LogOff
        [HttpPost]

        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        //private async Task SignInAsync(IdentityUser user, bool isPersistent)
        //{
        //    AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
        //    var identity = await _userManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
        //    AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, identity);
        //}

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}