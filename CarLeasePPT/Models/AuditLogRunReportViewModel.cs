using System;
using System.ComponentModel.DataAnnotations;
using CarLeasePPT.Utility;

namespace CarLeasePPT.Models
{
    public class AuditLogRunReportViewModel
    {
        #region Public Properties

        [Display(Name = "AuditLog_RunReport_EndDate_DisplayName", ResourceType = typeof(Resources))]
        [Required(ErrorMessageResourceType = typeof(Resources),
            ErrorMessageResourceName = "Common_Validation_Required_Message")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime EndDateTime { get; set; }

        [Display(Name = "AuditLog_RunReport_IncludeInfo_DisplayName", ResourceType = typeof(Resources))]
        public bool IncludeInfo { get; set; }

        [Display(Name = "AuditLog_RunReport_IncludeReviewed_DisplayName", ResourceType = typeof(Resources))]
        public bool IncludeReviewed { get; set; }

        [Display(Name = "AuditLog_RunReport_StartDate_DisplayName", ResourceType = typeof(Resources))]
        [Required(ErrorMessageResourceType = typeof(Resources),
            ErrorMessageResourceName = "Common_Validation_Required_Message")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime StartDateTime { get; set; }

        #endregion
    }
}