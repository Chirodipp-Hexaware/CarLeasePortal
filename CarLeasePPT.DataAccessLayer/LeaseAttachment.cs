//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CarLeasePPT.DataAccessLayer
{
    using System;
    using System.Collections.Generic;
    
    public partial class LeaseAttachment
    {
        public int LeaseAttachmentId { get; set; }
        public int CarLeaseId { get; set; }
        public int LeaseAttachmentTypeId { get; set; }
        public Nullable<System.Guid> DocumentFileStoreStreamId { get; set; }
        public string AttachmentName { get; set; }
        public string AttachmentDescription { get; set; }
        public Nullable<int> CreatedByPersonId { get; set; }
        public System.DateTime CreatedOnDateTime { get; set; }
        public Nullable<int> ModifiedByPersonId { get; set; }
        public Nullable<System.DateTime> ModifiedOnDateTime { get; set; }
        public bool IsDeleted { get; set; }
        public Nullable<int> DeletedByPersonId { get; set; }
        public Nullable<System.DateTime> DeletedOnDateTime { get; set; }
    
        public virtual LeaseAttachmentType LeaseAttachmentType { get; set; }
        public virtual Person PersonCreatedBy { get; set; }
        public virtual Person PersonDeletedBy { get; set; }
        public virtual Person PersonModifiedBy { get; set; }
        public virtual CarLease CarLease { get; set; }
    }
}
