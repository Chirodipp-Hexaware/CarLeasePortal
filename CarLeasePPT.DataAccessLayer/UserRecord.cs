namespace CarLeasePPT.DataAccessLayer
{
    public class UserRecord
    {
        #region Public Properties

        public string EmailAddress { get; set; }
        public string FullName { get; set; }
        public bool IsExpired { get; set; }
        public bool IsLocked { get; set; }
        public int PersonId { get; set; }
        public string PersonStatus { get; set; }
        public string PreferredName { get; set; }
        public string Username { get; set; }

        #endregion
    }
}