using System;
using System.ComponentModel.DataAnnotations;
using CarLeasePPT.Utility;

namespace CarLeasePPT.Models
{
    public class AuditLogViewModel
    {
        #region Public Properties

        [Display(Name = "AuditLog_Detail_Action_DisplayName", ResourceType = typeof(Resources))]
        public string Action { get; set; }

        public int AuditLogId { get; set; }

        [Display(Name = "AuditLog_Detail_Controller_DisplayName", ResourceType = typeof(Resources))]
        public string Controller { get; set; }

        public bool IsReviewed { get; set; }

        [Display(Name = "AuditLog_Detail_LogLevel_DisplayName", ResourceType = typeof(Resources))]
        public string Level { get; set; }

        [Display(Name = "AuditLog_Detail_Logged_DisplayName", ResourceType = typeof(Resources))]
        public DateTime Logged { get; set; }

        [Display(Name = "AuditLog_Detail_Message_DisplayName", ResourceType = typeof(Resources))]
        public string Message { get; set; }

        [Display(Name = "AuditLog_Detail_PersonFullName_DisplayNane", ResourceType = typeof(Resources))]
        public string PersonFullName { get; set; }

        public int? PersonId { get; set; }

        [Display(Name = "AuditLog_Detail_RecordId_DisplayName", ResourceType = typeof(Resources))]
        public int? RecordId { get; set; }

        [Display(Name = "AuditLog_Detail_RemoteAddress_DisplayName", ResourceType = typeof(Resources))]
        public string RemoteAddress { get; set; }

        [Display(Name = "AuditLog_Detail_ReviewedByPersonName_DisplayName", ResourceType = typeof(Resources))]
        public string ReviewedByPersonName { get; set; }

        [Display(Name = "AuditLog_Detail_ReviewedOnDateTime_DisplayName", ResourceType = typeof(Resources))]
        public DateTime? ReviewedOnDateTime { get; set; }

        [Display(Name = "AuditLog_Detail_Url_DisplayName", ResourceType = typeof(Resources))]
        public string Url { get; set; }

        #endregion
    }
}