using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace NNI.PayerPortal.WebUI.Models
{
    public class LogOnViewModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")] // Username
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember Password")]
        public bool RememberMe { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string ChangePassword { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string ForwardUrl { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string Error { get; set; }

        public IEnumerable<string> ErrorFields { get; set; }
    }



    // lightbox-new-password
    public class NewPasswordModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Old Password")]
        public string OldPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string ChangePassword { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string Error { get; set; }

        public IEnumerable<string> ErrorFields { get; set; }
    }

    // lightbox-request-temporary-password
    public class RequestTemporaryPasswordModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string ChangePassword { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string TemporaryPassword { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string Error { get; set; }

        public IEnumerable<string> ErrorFields { get; set; }
    }

    // lightbox-request-access
    public class RequestAccessModel
    {
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone")]
        public string Phone { get; set; }

        [Display(Name = "Organization")]
        public string Organization { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string Error { get; set; }

        public IEnumerable<string> ErrorFields { get; set; }
    }

    public class PreferencesModel
    {
        public PreferencesPasswordModel PasswordModel { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string Error { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string ErrorModel { get; set; }

        
    }

    public class PreferencesPasswordModel
    {
        [HiddenInput(DisplayValue = false)]
        public string ChangePassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Old Password")]
        public string OldPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string ConfirmPassword { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string ChangeEmail { get; set; }

        [DataType(DataType.EmailAddress)]
        [Display(Name = "Old Email")]
        public string OldEmail { get; set; }

        [DataType(DataType.EmailAddress)]
        [Display(Name = "New Email")]
        public string NewEmail { get; set; }

        [DataType(DataType.EmailAddress)]
        [Display(Name = "New Email")]
        public string ConfirmNewEmail { get; set; }

        public string Error { get; set; }
        public List<string> ErrorFields { get; set; }
    }

    /// <summary>
    /// Microsoft Default Accounts Implementation
    /// </summary>
    //public class ChangePasswordModel
    //{
    //    [Required]
    //    [DataType(DataType.Password)]
    //    [Display(Name = "Current password")]
    //    public string OldPassword { get; set; }

    //    [Required]
    //    [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
    //    [DataType(DataType.Password)]
    //    [Display(Name = "New password")]
    //    public string NewPassword { get; set; }

    //    [DataType(DataType.Password)]
    //    [Display(Name = "Confirm new password")]
    //    [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
    //    public string ConfirmPassword { get; set; }
    //}
    //public class RegisterModel
    //{
    //    [Required]
    //    [DataType(DataType.EmailAddress)]
    //    [Display(Name = "User name")]
    //    public string UserName { get; set; }

    //    [Required]
    //    [DataType(DataType.EmailAddress)]
    //    [Display(Name = "Email address")]
    //    public string Email { get; set; }

    //    [Required]
    //    [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
    //    [DataType(DataType.Password)]
    //    [Display(Name = "Password")]
    //    public string Password { get; set; }

    //    [DataType(DataType.Password)]
    //    [Display(Name = "Confirm password")]
    //    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    //    public string ConfirmPassword { get; set; }
    //}
}