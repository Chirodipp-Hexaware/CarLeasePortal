using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using CarLeasePPT.Helpers;
using CarLeasePPT.Utility;

namespace CarLeasePPT.Models
{
    public class SetPasswordViewModel
    {
        #region Public Properties

        [Required(ErrorMessageResourceName = "Common_Validation_Required_Message",
            ErrorMessageResourceType = typeof(Resources))]
        [Compare("NewPassword", ErrorMessageResourceType = typeof(Resources),
            ErrorMessageResourceName = "Settings_Password_NewPassword_MatchValidation_Message")]
        [DataType(DataType.Password)]
        [Display(Name = "Settings_Password_ConfirmPassword_DisplayName", ResourceType = typeof(Resources))]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessageResourceName = "Common_Validation_Required_Message",
            ErrorMessageResourceType = typeof(Resources))]
        [DataType(DataType.Password)]
        [MinLength(10, ErrorMessageResourceName = "Common_Validation_FieldLengthMinimumError_Message",
            ErrorMessageResourceType = typeof(Resources))]
        [Display(Name = "Settings_Password_NewPassword_DisplayName", ResourceType = typeof(Resources))]
        [ValidatePasswordBlacklist]
        public string NewPassword { get; set; }

        public Collection<string> PasswordSecurityPolicyCollection { get; set; }

        #endregion
    }
}