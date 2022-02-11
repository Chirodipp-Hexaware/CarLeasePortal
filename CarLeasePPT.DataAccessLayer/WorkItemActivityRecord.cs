using System;
using System.Collections.Generic;

namespace CarLeasePPT.DataAccessLayer
{
    public class WorkItemActivityRecord
    {
        #region Public Properties

        public DateTime ActivityDateTime { get; set; }
        public int CreatedByPersonId { get; set; }
        public string CreatedByPersonName { get; set; }
        public DateTime CreatedOnDateTime { get; set; }
        public int? DeletedByPersonId { get; set; }
        public string DeletedByPersonName { get; set; }
        public DateTime? DeletedOnDateTime { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsPrivate { get; set; }
        public DateTime? ModifiedOnDateTime { get; set; }
        public string Narrative { get; set; }
        public IEnumerable<WorkItemAttachmentRecord> WorkItemActivityAttachmentRecords { get; set; }
        public int WorkItemActivityId { get; set; }
        public string WorkItemActivityType { get; set; }
        public int WorkItemActivityTypeId { get; set; }
        public int WorkItemId { get; set; }

        #endregion
    }
}