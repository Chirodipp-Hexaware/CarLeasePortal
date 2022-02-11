using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CarLeasePPT.Utility;

namespace CarLeasePPT.Models
{
    public class UserCreateViewModel
    {
        #region Public Properties

        [Required]
        [Display(Name = "User_Common_EmailAddress_DisplayName", ResourceType = typeof(Resources))]
        [MaxLength(255)]
        [EmailAddress]
        public string EmailAddress { get; set; }

        [Display(Name = "User_Common_FullName_DisplayName", ResourceType = typeof(Resources))]
        [MaxLength(256)]
        [Required]
        public string FullName { get; set; }

        [Required]
        [Display(Name = "User_Common_PreferredName_DisplayName", ResourceType = typeof(Resources))]
        [MaxLength(64)]
        public string PreferredName { get; set; }

        [Display(Name = "User_Common_RolesAssigned_DisplayName", ResourceType = typeof(Resources))]
        public List<int> RolesAssigned { get; set; }

        [Display(Name = "User_Common_RolesAvailable_DisplayName", ResourceType = typeof(Resources))]
        public List<int> RolesAvailable { get; set; }

        #endregion
    }
}