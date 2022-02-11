using System.ComponentModel.DataAnnotations;
using CarLeasePPT.Utility;

namespace CarLeasePPT.Models
{
    public class TaxCollectorDetailViewModel
    {
        #region Public Properties

        public int TaxCollectorId { get; set; }

        [Display(Name = "TaxCollector_Common_TaxCollectorName_DisplayName", ResourceType = typeof(Resources))]

        public string TaxCollectorName { get; set; }

        [Display(Name = "TaxCollector_Common_TaxAssessorName_DisplayName", ResourceType = typeof(Resources))]
        public string TaxAssessorName { get; set; }

        [Display(Name = "TaxCollector_Common_StateAbbreviation_DisplayName", ResourceType = typeof(Resources))]
        public string StateAbbreviation { get; set; }

        [Display(Name = "TaxCollector_Common_County_DisplayName", ResourceType = typeof(Resources))]
        public string County { get; set; }

        [Display(Name = "TaxCollector_Common_VendorCode_DisplayName", ResourceType = typeof(Resources))]
        public string VendorCode { get; set; }

        public bool IsDeleted { get; set; }

        #endregion
    }
}