using System;

namespace CarLeasePPT.DataAccessLayer
{
    public class PersonTokenRecord
    {
        #region Public Properties

        public int PersonId { get; set; }
        public string Token { get; set; }
        public bool TokenClaimed { get; set; }
        public DateTime TokenExpiration { get; set; }

        #endregion
    }
}