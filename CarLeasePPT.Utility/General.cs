using System;
using System.IO;

namespace CarLeasePPT.Utility
{
    public static class General
    {
        #region Properties

        private static string InvalidPathCharacterList =>
            new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars());

        #endregion

        #region Public Methods and Operators

        public static string CleanFileName(string input)
        {
            const string defaultFileName = "Attachment";
            try
            {
                var cleanFileName = Path.GetFileNameWithoutExtension(input) ?? defaultFileName;
                foreach (var c in InvalidPathCharacterList) cleanFileName = cleanFileName.Replace(c.ToString(), "_");
                var baseFileName = cleanFileName.AsNullIfWhiteSpace() ?? defaultFileName;
                // Make the output name unique
                return $"{baseFileName}_{DateTime.Now.Ticks}.pdf";
            }
            catch (Exception exception)
            {
                Log.Instance.Error(exception);
                return defaultFileName;
            }
        }


        public static DateTime RoundTimeToNearestN(DateTime dt, TimeSpan d)
        {
            return new DateTime((dt.Ticks + d.Ticks - 1) / d.Ticks * d.Ticks, dt.Kind);
        }

        public static byte[] StreamToByteArray(Stream input)
        {
            try
            {
                using (var ms = new MemoryStream())
                {
                    input.CopyTo(ms);
                    return ms.ToArray();
                }
            }
            catch (Exception exception)
            {
                Log.Instance.Error(exception);
                return null;
            }
        }

        #endregion
    }
}