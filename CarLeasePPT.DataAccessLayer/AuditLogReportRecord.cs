using System;
using System.ComponentModel;
using CarLeasePPT.Spreadsheet;

namespace CarLeasePPT.DataAccessLayer
{
    public class AuditLogReportRecord
    {
        #region Public Properties

        [DisplayName("MVC Action")] public string Action { get; set; }

        [DisplayName("MVC Controller")] public string Controller { get; set; }

        [DisplayName("Reviewed?")] public string IsReviewed { get; set; }

        [DisplayName("Event Date/Time")]
        [EpPlusNumberFormat("m/d/yyyy h:mm:ss AM/PM")]
        public DateTime Logged { get; set; }

        [DisplayName("Log Level")] public string LogLevel { get; set; }

        [DisplayName("Event Type")] public string Message { get; set; }

        [DisplayName("Person Name")] public string PersonFullName { get; set; }

        [DisplayName("Record Id")] public int? RecordId { get; set; }

        [DisplayName("IP Address")] public string RemoteAddress { get; set; }

        [DisplayName("Reviewed By")] public string ReviewedByFullName { get; set; }

        [DisplayName("Reviewed On")]
        [EpPlusNumberFormat("m/d/yyyy h:mm:ss AM/PM")]
        public DateTime? ReviewedOnDateTime { get; set; }

        [DisplayName("Relative URL")] public string Url { get; set; }

        #endregion
    }
}