namespace CarLeasePPT.DataAccessLayer
{
    public static partial class AuthenticationFailureEngine
    {
        private class BlockSequenceRecord
        {
            #region Public Properties

            public int BlockMinutes { get; set; }
            public int FailureSequence { get; set; }

            #endregion
        }
    }
}