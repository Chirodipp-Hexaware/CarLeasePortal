using System.ComponentModel.DataAnnotations;
using CarLeasePPT.Utility;

namespace CarLeasePPT.Models
{
    public class TaxAssessorDeleteViewModel
    {
        #region Public Properties

        public string TaxAssessorName { get; set; }

        public string StateAbbreviation { get; set; }

        public string County { get; set; }

        public int TaxAssessorId { get; set; }

        [Display(Name = "TaxAssessor_Delete_ConfirmDelete_DisplayName", ResourceType = typeof(Resources))]
        public bool ConfirmDelete { get; set; }

        #endregion
    }
}