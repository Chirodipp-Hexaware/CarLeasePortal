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
    
    public partial class TaxCollector
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TaxCollector()
        {
            this.TaxBills = new HashSet<TaxBill>();
            this.WorkItems = new HashSet<WorkItem>();
        }
    
        public int TaxCollectorId { get; set; }
        public int TaxAssessorId { get; set; }
        public string TaxCollectorName { get; set; }
        public string VendorCode { get; set; }
        public Nullable<int> CreatedByPersonId { get; set; }
        public Nullable<System.DateTime> CreatedOnDateTime { get; set; }
        public Nullable<int> ModifiedByPersonId { get; set; }
        public Nullable<System.DateTime> ModifiedOnDateTime { get; set; }
        public bool IsDeleted { get; set; }
        public Nullable<int> DeletedByPersonId { get; set; }
        public Nullable<System.DateTime> DeletedOnDateTime { get; set; }
    
        public virtual TaxAssessor TaxAssessor { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TaxBill> TaxBills { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WorkItem> WorkItems { get; set; }
        public virtual Person PersonCreatedBy { get; set; }
        public virtual Person PersonDeletedBy { get; set; }
        public virtual Person PersonModifiedBy { get; set; }
    }
}
