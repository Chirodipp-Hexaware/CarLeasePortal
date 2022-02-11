using System;

namespace CarLeasePPT.DataAccessLayer
{
    public class LeaseAttachmentRecord
    {
        #region Public Properties

        public string AttachmentName { get; set; }
        public DateTime CreatedOnDateTime { get; set; }
        public int LeaseAttachmentId { get; set; }
        public string LeaseAttachmentTypeName { get; set; }

        #endregion
    }
}