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
    
    public partial class Parcel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Parcel()
        {
            this.ParcelTaxBills = new HashSet<ParcelTaxBill>();
            this.PhysicalAddresses = new HashSet<PhysicalAddress>();
        }
    
        public int ParcelId { get; set; }
        public int TaxAssessorId { get; set; }
        public string ParcelNumber { get; set; }
    
        public virtual TaxAssessor TaxAssessor { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ParcelTaxBill> ParcelTaxBills { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PhysicalAddress> PhysicalAddresses { get; set; }
    }
}
