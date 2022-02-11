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
    
    public partial class AuditLog
    {
        public int AuditLogId { get; set; }
        public System.DateTime Logged { get; set; }
        public string Level { get; set; }
        public Nullable<int> PersonId { get; set; }
        public string FullName { get; set; }
        public string Message { get; set; }
        public Nullable<bool> Https { get; set; }
        public string ServerName { get; set; }
        public string Port { get; set; }
        public string Url { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public Nullable<int> RecordId { get; set; }
        public string ServerAddress { get; set; }
        public string RemoteAddress { get; set; }
        public bool IsReviewed { get; set; }
        public Nullable<int> ReviewedByPersonId { get; set; }
        public Nullable<System.DateTime> ReviewedOnDateTime { get; set; }
    
        public virtual Person PersonCreatedBy { get; set; }
        public virtual Person PersonReviewedBy { get; set; }
    }
}
