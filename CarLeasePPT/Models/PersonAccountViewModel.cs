using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using CarLeasePPT.Utility;

namespace CarLeasePPT.Models
{
    public class PersonAccountViewModel
    {
        #region Public Properties

        [Display(Name = "Settings_MyAccount_EmailAddress_DisplayName", ResourceType = typeof(Resources))]
        public string EmailAddress { get; set; }

        [Display(Name = "Settings_MyProfile_FullName_DisplayName", ResourceType = typeof(Resources))]
        [MaxLength(256)]
        [Required]
        public string FullName { get; set; }

        public int PersonId { get; set; }

        [Display(Name = "Settings_MyProfile_PreferredName_DisplayName", ResourceType = typeof(Resources))]
        [MaxLength(64)]
        [Required]
        public string PreferredName { get; set; }

        public ReadOnlyCollection<string> UserRoles { get; set; }

        #endregion
    }
}