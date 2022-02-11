using System.ComponentModel.DataAnnotations;
using CarLeasePPT.Utility;

namespace CarLeasePPT.Models
{
    public class TaxCollectorDeleteViewModel
    {
        #region Public Properties
        public int TaxCollectorId { get; set; }
        public string TaxCollectorName { get; set; }
        public string VendorCode { get; set; }

        [Display(Name = "TaxCollector_Delete_ConfirmDelete_DisplayName", ResourceType = typeof(Resources))]
        public bool ConfirmDelete { get; set; }

        #endregion
    }
}