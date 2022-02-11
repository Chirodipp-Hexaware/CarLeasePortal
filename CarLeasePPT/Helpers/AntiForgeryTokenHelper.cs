using System.Web.Helpers;

namespace CarLeasePPT.Helpers
{
    public static class AntiForgeryTokenHelper
    {
        #region Public Methods and Operators

        public static string TokenHeaderValue()
        {
            AntiForgery.GetTokens(null, out var cookieToken, out var formToken);
            return $"{cookieToken}:{formToken}";
        }

        #endregion
    }
}