using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NNI.PayerPortal.WebUI.Infrastructure.Abstract;
using NNI.PayerPortal.WebUI.Models;
using System.Web.Security;
using NNI.PayerPortal.Domain.Concrete;
using System.Text.RegularExpressions;
using System.Text;

namespace NNI.PayerPortal.WebUI.Controllers
{
    public class AccountController : Controller
    {
        IAuthProvider authProvider;

        public AccountController(IAuthProvider auth)
        {
            authProvider = auth;
        }

        //DONE!
        #region LogOn LogOff
        //
        // GET: /Account/LogOn
        [HttpGet]
        public ViewResult LogOn()
        {
            return View();
        }

        //
        // POST: /Account/LogOn
        [HttpPost]
        public ActionResult LogOn(LogOnViewModel model, string returnUrl)
        {
            if (ModelState.IsValid && Request.IsAjaxRequest())
            {
                // Get User
                string userName = Membership.GetUserNameByEmail(model.Email);     
 
                if (authProvider.Authenticate(userName, model.Password, model.RememberMe))
                {
                    MembershipUser currentUser = Membership.GetUser(userName); 
                    if (model.Password == SystemSettings.DefaultPassword)
                    {
                        // If the user is logging in for the first time, open up the "lightbox-new-password" window
                        model.ChangePassword = "true";
                        model.Error = "";
                        model.ForwardUrl = "";
                        return Json(model, JsonRequestBehavior.AllowGet);
                    }
                    else if (currentUser.Password == currentUser.PasswordQuestion)
                    {
                        // If the user has a temporary password, open up the "lightbox-new-password" window
                        model.ChangePassword = "true";
                        model.Error = "";
                        model.ForwardUrl = "";
                        return Json(model, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        model.ForwardUrl = "";
                        model.ChangePassword = "false";
                        model.Error = "";
                    }
                    model.ForwardUrl = Url.Action("Index", "Home");
                    return Json(model, JsonRequestBehavior.AllowGet);
                    // return Redirect(returnUrl ?? Url.Action("Index", "Home"));
                }
                else
                {
                    // Return Error Message
                    model.ForwardUrl = "";
                    model.ChangePassword = "false";
                    model.Error = "The password you entered is not valid. Please try again.";
                    return Json(model, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                // If we got this far, something failed, redisplay form
                return View();
            }
        }

        //
        // GET: /Account/LogOff
        [HttpGet]
        [Authorize]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("LogOn", "Account");
        }
        #endregion

        //DONE!
        #region New Password
        //
        // GET: /Account/NewPassword
        [HttpGet]
        public ActionResult NewPassword()
        {
            return View();
        }

        //
        // POST: /Account/NewPassword
        [HttpPost]
        public ActionResult NewPassword(NewPasswordModel model)
        {
            if (ModelState.IsValid && Request.IsAjaxRequest())
            {
                // Verify that user & passwords are valid
                if (authProvider.Authenticate(model.Email, model.OldPassword, false) && (model.OldPassword != model.NewPassword)  && Regex.IsMatch(model.NewPassword, SystemSettings.PasswordRegex))
                {
                    // Change the Password
                    try
                    {
                        MembershipUser currentUser = Membership.GetUser(model.Email, true);
                        model.ChangePassword = currentUser.ChangePassword(model.OldPassword, model.NewPassword).ToString().ToLower();        
                        model.Error = "";
                    }
                    catch
                    {
                        model.Error = "The information you entered is incorrect. Please try again";
                    }
                }
                else
                {
                    //model.Error = "We do not have this email address on file. Please enter a valid email.";
                    model.Error = "The information you entered is incorrect. Please try again";
                }

                return Json(model, JsonRequestBehavior.AllowGet);
            }
            else
            {
                // If we got this far, something failed, redisplay form
                return View();
            }
        }
        #endregion

        //DONE!
        #region Request Temporary Password
        //
        // GET: /Account/RequestTemporaryPassword
        [HttpGet]
        public ActionResult RequestTemporaryPassword()
        {
            return View();
        }

        // 
        // POST: /Account/RequestTemporaryPassword
        [HttpPost]
        public ActionResult RequestTemporaryPassword(RequestTemporaryPasswordModel model)
        {
            if (ModelState.IsValid && Request.IsAjaxRequest())
            {
                string userName = Membership.GetUserNameByEmail(model.Email);     
                // Verify that user is valid
                if (userName != null)
                {
                    try
                    {
                        // Step 1: If user exists Reset their Password
                        MembershipUser currentUser = Membership.GetUser(userName);
                        model.TemporaryPassword = currentUser.ResetPassword();
                        // Step 2: Temporarily Disable the User
                        currentUser.ChangePasswordQuestionAndAnswer(model.TemporaryPassword, model.TemporaryPassword, model.TemporaryPassword);  
                        // Step 3: Email user their newly generated password 
                        EmailMessage message = new EmailMessage
                        {
                            MailToAddress = model.Email,
                            MailFromAddress = SystemSettings.EmailNotificationsFrom,
                            MessageSubject = "NovoPayerLink: New Temporary Password",
                            MessageBody = model.TemporaryPassword                            
                        };

                        SendMail sendMail = new SendMail(message);
                        sendMail.Send();

                        model.Error = "";
                        model.ChangePassword = "true";
                    }
                    catch
                    {
                        model.Error = "We do not have this email address on file. Please enter a valid email.";
                        model.ChangePassword = "false";
                    }
                }
                else
                {
                    model.Error = "We do not have this email address on file. Please enter a valid email.";
                    model.ChangePassword = "false";
                }
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            else
            {
                // redisplay form
                return View();
            }
        }
        #endregion

        //DONE!
        #region Request Access
        //
        // GET: /Account/RequestAccess
        [HttpGet]
        public ActionResult RequestAccess()
        {
            return View();
        }

        //
        // POST: /Account/RequestAccess
        [HttpPost]
        public ActionResult RequestAccess(RequestAccessModel model)
        {
            if (ModelState.IsValid && Request.IsAjaxRequest())
            {
                try
                {
                    StringBuilder mBody = new StringBuilder();
                    mBody.AppendLine("Description: NovoPayerLink - New User Access Request for " + model.FirstName + " " + model.LastName);
                    mBody.AppendLine("-- User Details --");
                    mBody.AppendLine("First Name:   " + model.FirstName);
                    mBody.AppendLine("Last Name:    " + model.LastName);
                    mBody.AppendLine("Email:        " + model.Email);
                    mBody.AppendLine("Phone:        " + model.Phone);
                    mBody.AppendLine("Organization: " + model.Organization);
                    mBody.AppendLine("Requested On: " + DateTime.Now.ToShortDateString());
                    // Email user access request to the administrators 
                    EmailMessage message = new EmailMessage
                    {
                        MailToAddress = SystemSettings.EmailNotificationsTo,
                        MailFromAddress = SystemSettings.EmailNotificationsFrom,
                        MessageSubject = "NovoPayerLink: New User Access Request for " + model.FirstName + " " + model.LastName,
                        MessageBody = mBody.ToString()
                    };

                    SendMail sendMail = new SendMail(message);
                    sendMail.Send();
                  
                    model.Error = "";
                }
                catch
                {
                    model.Error = "The information you entered is incorrect. Please try again.";
                }
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            else
            {
                // redisplay form
                return View();
            }
        }
        #endregion

        //DONE!
        #region Preferences
        //
        // GET: /Account/Preferences
        [HttpGet]
        [Authorize]
        public ActionResult Preferences()
        {
            return View();
        }

        //
        // POST: /Account/Preferences
        [HttpPost]
        [Authorize]
        public ActionResult Preferences(PreferencesPasswordModel model)
        {
            var errorFieldsIndex = 0;
            PreferencesPasswordModel response = new PreferencesPasswordModel();


            if (ModelState.IsValid && Request.IsAjaxRequest() && ((model.ChangeEmail == "true") || (model.ChangePassword == "true")))
            {
                if (model.ChangeEmail == "true")
                {                   
                    // Update users e-mail address
                    if ((model.NewEmail != model.OldEmail) && (model.NewEmail == model.ConfirmNewEmail))
                    {
                        MembershipUser currentUser = Membership.GetUser();
                        currentUser.Email = model.NewEmail;
                        Membership.UpdateUser(currentUser);
                    }
                    else
                    {
                        response.Error = "The information you entered is incorrect. Please try again.";
                        response.ErrorFields.Insert(errorFieldsIndex++, "Email");
                    }
                }
                if (model.ChangePassword == "true")
                {
                    // Update users password
                    if ((model.NewPassword != model.OldPassword) && (model.NewPassword == model.ConfirmPassword))
                    {
                        MembershipUser currentUser = Membership.GetUser();
                        if (currentUser.ChangePassword(model.OldPassword, model.NewPassword))
                        {
                            response.Error = "";
                        }
                        else
                        {
                            response.Error = "The information you entered is incorrect. Please try again.";
                            response.ErrorFields.Insert(errorFieldsIndex++, "Email");
                        }
                    }
                    else
                    {
                        response.Error = "The information you entered is incorrect. Please try again.";
                        response.ErrorFields.Insert(errorFieldsIndex++, "Email");
                    }
                }
                return Json(response, JsonRequestBehavior.AllowGet);
            }
            else
            {
                // redisplay form
                return View();
            }
        }
        #endregion

        #region Status Codes
        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
        #endregion

        #region Microsoft Sample Implemenation
        /// <summary>
        /// Microsoft Default Accounts Implementation
        /// </summary>
        ////
        //// GET: /Account/ChangePassword
        //[Authorize]
        //public ActionResult ChangePassword()
        //{
        //    return View();
        //}
        ////
        //// POST: /Account/ChangePassword
        //[Authorize]
        //[HttpPost]
        //public ActionResult ChangePassword(ChangePasswordModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        // ChangePassword will throw an exception rather
        //        // than return false in certain failure scenarios.
        //        bool changePasswordSucceeded;
        //        try
        //        {
        //            MembershipUser currentUser = Membership.GetUser(User.Identity.Name, true /* userIsOnline */);
        //            changePasswordSucceeded = currentUser.ChangePassword(model.OldPassword, model.NewPassword);
        //        }
        //        catch (Exception)
        //        {
        //            changePasswordSucceeded = false;
        //        }
        //        if (changePasswordSucceeded)
        //        {
        //            return RedirectToAction("ChangePasswordSuccess");
        //        }
        //        else
        //        {
        //            ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
        //        }
        //    }
        //    // If we got this far, something failed, redisplay form
        //    return View(model);
        //}
        //
        // GET: /Account/ChangePasswordSuccess
        //public ActionResult ChangePasswordSuccess()
        //{
        //    return View();
        //}
        ////
        //// GET: /Account/Register
        //public ActionResult Register()
        //{
        //    return View();
        //}
        ////
        //// POST: /Account/Register
        //[HttpPost]
        //public ActionResult Register(RegisterModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        // Attempt to register the user
        //        MembershipCreateStatus createStatus;
        //        Membership.CreateUser(model.UserName, model.Password, model.Email, null, null, true, null, out createStatus);
        //        if (createStatus == MembershipCreateStatus.Success)
        //        {
        //            FormsAuthentication.SetAuthCookie(model.UserName, false /* createPersistentCookie */);
        //            return RedirectToAction("Index", "Home");
        //        }
        //        else
        //        {
        //            ModelState.AddModelError("", ErrorCodeToString(createStatus));
        //        }
        //    }
        //    // If we got this far, something failed, redisplay form
        //    return View(model);
        //}
        #endregion
    }
}
