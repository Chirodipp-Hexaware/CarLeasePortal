using System.ComponentModel.DataAnnotations;
using CarLeasePPT.Utility;

namespace CarLeasePPT.Models
{
    public class PasswordResetViewModel
    {
        #region Public Properties

        [DataType(DataType.EmailAddress)]
        [Display(Name = "Account_PasswordReset_EmailAddress_DisplayName", ResourceType = typeof(Resources))]
        public string Email { get; set; }

        #endregion
    }
}