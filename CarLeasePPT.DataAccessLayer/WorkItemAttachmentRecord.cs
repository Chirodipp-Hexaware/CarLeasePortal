using System;

namespace CarLeasePPT.DataAccessLayer
{
    public class WorkItemAttachmentRecord
    {
        #region Public Properties

        public string AttachmentDescription { get; set; }
        public string AttachmentName { get; set; }
        public string CreatedByPersonFullName { get; set; }
        public DateTime CreatedOnDateTime { get; set; }
        public int WorkItemAttachmentId { get; set; }

        #endregion
    }
}