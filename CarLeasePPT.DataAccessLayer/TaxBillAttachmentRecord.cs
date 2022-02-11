using System;

namespace CarLeasePPT.DataAccessLayer
{
    public class TaxBillAttachmentRecord
    {
        #region Public Properties

        public string AttachmentName { get; set; }
        public string CreatedByPersonFullName { get; set; }
        public DateTime CreatedOnDateTime { get; set; }
        public int TaxBillAttachmentId { get; set; }
        public string TaxBillAttachmentTypeName { get; set; }

        #endregion
    }
}