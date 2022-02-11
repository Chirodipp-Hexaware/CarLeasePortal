#region

using System;

#endregion

namespace CarLeasePPT.DataAccessLayer
{
    public static partial class PersonEngine
    {
        public class PersonAuthenticationRecord
        {
            #region Public Properties

            public string FullName { get; set; }
            public bool IsActiveRecord { get; set; }
            public bool IsExpired { get; set; }
            public bool IsLocked { get; set; }
            public int PersonId { get; set; }
            public DateTime PersonSecurityEffectiveDateTime { get; set; }
            public DateTime PersonSecurityExpirationDateTime { get; set; }
            public string SecurityValue { get; set; }

            #endregion
        }
    }
}