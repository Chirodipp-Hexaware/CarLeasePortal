namespace CarLeasePPT.DataAccessLayer
{
    public class RoleRecord
    {
        #region Public Properties

        public bool InternalUseOnly { get; set; }
        public string RoleCode { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public int SequenceId { get; set; }

        #endregion
    }
}