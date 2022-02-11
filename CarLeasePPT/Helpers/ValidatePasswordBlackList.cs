using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using CarLeasePPT.DataAccessLayer;
using CarLeasePPT.Utility;

namespace CarLeasePPT.Helpers
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ValidatePasswordBlacklist : ValidationAttribute
    {
        #region Methods

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var passwordValue = string.Empty;
            var newPasswordObject = validationContext.ObjectType.GetProperty("NewPassword");
            if (newPasswordObject != null)
            {
                passwordValue = (string) newPasswordObject.GetValue(validationContext.ObjectInstance, null);
            }
            else
            {
                var passwordObject = validationContext.ObjectType.GetProperty("Password");
                if (passwordObject != null)
                    passwordValue = (string) passwordObject.GetValue(validationContext.ObjectInstance, null);
            }

            var result = BlockedPasswordEngine.GetBlockedPasswords();
            return result.Contains(passwordValue.ToUpperInvariant())
                ? new ValidationResult(Resources.Helper_ValidatePasswordChange_BlockedPassword_Message)
                : ValidationResult.Success;
        }

        #endregion
    }
}