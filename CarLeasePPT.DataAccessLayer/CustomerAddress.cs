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
    
    public partial class CustomerAddress
    {
        public int CustomerAddressId { get; set; }
        public int CarLeaseId { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public System.DateTime EffectiveDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
    
        public virtual CarLease CarLease { get; set; }
    }
}
