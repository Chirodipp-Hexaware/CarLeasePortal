using System;

namespace CarLeasePPT.DataAccessLayer
{
    public class PersonSecurityRecord
    {
        #region Public Properties

        public DateTime EffectiveDateTime { get; set; }
        public DateTime ExpirationDateTime { get; set; }
        public int PersonId { get; set; }
        public int PersonSecurityId { get; set; }
        public string SecurityValue { get; set; }

        #endregion
    }
}