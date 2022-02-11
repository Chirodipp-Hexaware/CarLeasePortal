using System;

namespace CarLeasePPT.DataAccessLayer
{
    public class AuditLogDetailRecord
    {
        #region Public Properties

        public string Action { get; set; }
        public int AuditLogId { get; set; }
        public string Controller { get; set; }
        public bool IsReviewed { get; set; }
        public DateTime Logged { get; set; }
        public string LogLevel { get; set; }
        public string Message { get; set; }
        public string PersonFullName { get; set; }
        public int? PersonId { get; set; }
        public int? RecordId { get; set; }
        public string RemoteAddress { get; set; }
        public string ReviewedByFullName { get; set; }
        public DateTime? ReviewedOnDateTime { get; set; }
        public string Url { get; set; }

        #endregion
    }
}