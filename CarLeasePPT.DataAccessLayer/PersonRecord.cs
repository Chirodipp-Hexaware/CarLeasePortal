using System;

namespace CarLeasePPT.DataAccessLayer
{
    public class PersonRecord
    {
        #region Public Properties

        public DateTime EffectiveDateTime { get; set; }
        public string EmailAddress { get; set; }
        public DateTime? ExpirationDateTime { get; set; }
        public string FullName { get; set; }
        public bool IsExpired { get; set; }
        public bool IsLocked { get; set; }
        public int ModifiedByPersonId { get; set; }
        public DateTime ModifiedOnDateTime { get; set; }
        public DateTime PasswordExpiration { get; set; }
        public int PersonId { get; set; }
        public string PreferredName { get; set; }

        #endregion
    }
}