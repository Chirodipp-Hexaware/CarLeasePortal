using System;

namespace CarLeasePPT.DataAccessLayer
{
    public class AuditLogListRecord
    {
        #region Public Properties

        public string Action { get; set; }
        public int AuditLogId { get; set; }
        public string Controller { get; set; }
        public bool IsReviewed { get; set; }
        public string IsReviewedString { get; set; }
        public DateTime Logged { get; set; }
        public string LogLevel { get; set; }
        public string Message { get; set; }
        public string PersonFullName { get; set; }

        #endregion
    }
}