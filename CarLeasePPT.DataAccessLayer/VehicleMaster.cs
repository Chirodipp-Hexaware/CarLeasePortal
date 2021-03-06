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
    
    public partial class CarMaster
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CarMaster()
        {
            this.CarLeases = new HashSet<CarLease>();
            this.WorkItems = new HashSet<WorkItem>();
        }
    
        public int CarMasterId { get; set; }
        public string Vin { get; set; }
        public string CarMake { get; set; }
        public string CarModel { get; set; }
        public string ModelYear { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CarLease> CarLeases { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WorkItem> WorkItems { get; set; }
    }
}
