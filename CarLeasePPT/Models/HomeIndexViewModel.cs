using System.ComponentModel.DataAnnotations;
using CarLeasePPT.Utility;

namespace CarLeasePPT.Models
{
    public class HomeIndexViewModel
    {
        #region Public Properties

        [Display(Name = "Home_Index_BillNumber_DisplayName", ResourceType = typeof(Resources))]
        public string BillNumber { get; set; }

        [Display(Name = "Home_Index_LeaseNumber_DisplayName", ResourceType = typeof(Resources))]
        public string LeaseNumber { get; set; }

        [Display(Name = "Home_Index_LesseeFirstName_DisplayName", ResourceType = typeof(Resources))]
        public string LesseeFirstName { get; set; }

        [Display(Name = "Home_Index_LesseeLastName_DisplayName", ResourceType = typeof(Resources))]
        public string LesseeLastName { get; set; }

        [Display(Name = "Home_Index_PlateNumber_DisplayName", ResourceType = typeof(Resources))]
        public string PlateNumber { get; set; }

        [Display(Name = "Home_Index_TaxAccountNumber_DisplayName", ResourceType = typeof(Resources))]
        public string TaxAccountNumber { get; set; }

        [Display(Name = "Home_Index_Vin_DisplayName", ResourceType = typeof(Resources))]
        public string VIN { get; set; }

        #endregion
    }
}