using System;

namespace CarLeasePPT.DataAccessLayer
{
    public class PersonSecurityResetRecord
    {
        #region Public Properties

        public int PersonId { get; set; }
        public int PersonSecurityResetId { get; set; }
        public bool RequiresSecondary { get; set; }
        public DateTime ResetExpiration { get; set; }
        public string ResetToken { get; set; }

        #endregion
    }
}