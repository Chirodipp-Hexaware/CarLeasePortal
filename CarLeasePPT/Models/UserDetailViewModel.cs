using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CarLeasePPT.Utility;

namespace CarLeasePPT.Models
{
    public class UserDetailViewModel
    {
        #region Public Properties

        [Display(Name = "User_Common_AssignedRoles_Title", ResourceType = typeof(Resources))]
        public IEnumerable<string> AssignedRoles { get; set; }

        [Display(Name = "User_Common_EffectiveDate", ResourceType = typeof(Resources))]
        public DateTime EffectiveDateTime { get; set; }

        [Display(Name = "User_Common_EmailAddress_DisplayName", ResourceType = typeof(Resources))]
        [EmailAddress]
        public string EmailAddress { get; set; }

        [Display(Name = "User_Common_ExpirationDate_DisplayName", ResourceType = typeof(Resources))]
        public DateTime? ExpirationDateTime { get; set; }

        [Display(Name = "User_Common_FullName_DisplayName", ResourceType = typeof(Resources))]
        public string FullName { get; set; }

        public bool IsExpired { get; set; }
        public bool IsLocked { get; set; }
        public int PersonId { get; set; }

        [Display(Name = "User_Common_PreferredName_DisplayName", ResourceType = typeof(Resources))]
        public string PreferredName { get; set; }

        #endregion
    }
}