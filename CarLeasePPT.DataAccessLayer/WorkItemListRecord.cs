using System;

namespace CarLeasePPT.DataAccessLayer
{
    public class WorkItemListRecord
    {
        #region Public Properties

        public DateTime CreateDateTime { get; set; }
        public bool IsUrgent { get; set; }
        public string IsUrgentString { get; set; }
        public DateTime? LastUpdated { get; set; }
        public string NoticeNumber { get; set; }
        public string TaxCollectorName { get; set; }
        public int WorkItemId { get; set; }
        public string WorkItemStatusName { get; set; }
        public string WorkItemTypeName { get; set; }

        #endregion
    }
}