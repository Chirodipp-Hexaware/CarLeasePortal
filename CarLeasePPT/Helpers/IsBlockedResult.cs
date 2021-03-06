using System;

namespace CarLeasePPT.Helpers
{
    public static partial class AuthenticationHelper
    {
        public class IsBlockedResult
        {
            #region Constructors and Destructors

            public IsBlockedResult()
            {
                IsBlocked = false;
                ExpirationDateTime = DateTime.Now;
            }

            public IsBlockedResult(DateTime expirationDateTime)
            {
                IsBlocked = true;
                ExpirationDateTime = expirationDateTime;
            }

            #endregion

            #region Public Properties

            public DateTime ExpirationDateTime { get; }
            public bool IsBlocked { get; }

            #endregion
        }
    }
}