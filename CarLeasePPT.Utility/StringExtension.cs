using System;
using Ganss.XSS;

namespace CarLeasePPT.Utility
{
    public static class StringExtension
    {
        #region Public Methods and Operators

        public static string AsNullIfWhiteSpace(this string s)
        {
            // Extensions is based on the post here:
            // http://haacked.com/archive/2010/06/16/null-or-empty-coalescing.aspx/
            return string.IsNullOrWhiteSpace(s) ? null : s;
        }

        public static bool IsNullOrWhiteSpace(this string s)
        {
            return string.IsNullOrWhiteSpace(s);
        }


        public static string SanitizeHtml(this string s)
        {
            try
            {
                var sanitizer = new HtmlSanitizer();
                return sanitizer.Sanitize(s);
            }
            catch (Exception exception)
            {
                Log.Instance.Error(exception);
                return string.Empty;
            }
        }

        public static string Truncate(this string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value)) return value;
            return maxLength <= 0 ? value : value.Substring(0, Math.Min(value.Length, maxLength));
        }

        #endregion
    }
}