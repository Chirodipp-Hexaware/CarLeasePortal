using System.ComponentModel.DataAnnotations;
using CarLeasePPT.Utility;

namespace CarLeasePPT.Models
{
    public class TaxBillIndexViewModel
    {
        #region Public Properties

        [Display(Name = "TaxBill_Index_BillNumber_DisplayName", ResourceType = typeof(Resources))]
        public string BillNumber { get; set; }

        [Display(Name = "TaxBill_Index_LeaseNumber_DisplayName", ResourceType = typeof(Resources))]
        public string LeaseNumber { get; set; }

        [Display(Name = "TaxBill_Index_TaxAccountNumber_DisplayName", ResourceType = typeof(Resources))]
        public string TaxAccountNumber { get; set; }

        [Display(Name = "TaxBill_Index_Vin_DisplayName", ResourceType = typeof(Resources))]
        public string Vin { get; set; }

        #endregion
    }
}