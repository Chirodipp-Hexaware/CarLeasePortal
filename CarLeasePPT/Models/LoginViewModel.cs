using System.ComponentModel.DataAnnotations;
using CarLeasePPT.Utility;

namespace CarLeasePPT.Models
{
    public class LoginViewModel
    {
        #region Public Properties

        [Required(ErrorMessageResourceType = typeof(Resources),
            ErrorMessageResourceName = "Common_Validation_Required_Message")]
        [Display(Name = "Common_EmailAddress_DisplayName", ResourceType = typeof(Resources))]
        public string EmailAddress { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources),
            ErrorMessageResourceName = "Common_Validation_Required_Message")]
        [DataType(DataType.Password)]
        [Display(Name = "Account_Login_Password_DisplayName", ResourceType = typeof(Resources))]
        public string Password { get; set; }

        #endregion
    }
}