using System;

namespace CarLeasePPT.DataAccessLayer
{
    public class CustomerAddressRecord
    {
        #region Public Properties

        public string City { get; set; }
        public DateTime EffectiveDateTime { get; set; }
        public DateTime? EndDate { get; set; }
        public string PostalCode { get; set; }
        public string State { get; set; }
        public string StreetAddress { get; set; }

        #endregion
    }
}