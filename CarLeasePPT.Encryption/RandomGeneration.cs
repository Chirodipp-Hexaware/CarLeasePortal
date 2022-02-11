using System;
using System.Text;
using Org.BouncyCastle.Crypto.Prng;
using CarLeasePPT.Utility;

namespace CarLeasePPT.Encryption
{
    public static class RandomGeneration
    {
        #region Static Fields

        private static readonly char[] Base62Chars =
            "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz".ToCharArray();

        #endregion

        #region Public Methods and Operators

        public static string GenerateRandomToken(int tokenLength = 32)
        {
            try
            {
                var sb = new StringBuilder(tokenLength);
                var prng = new VmpcRandomGenerator();
                var seedGen = new ThreadedSeedGenerator();
                prng.AddSeedMaterial(seedGen.GenerateSeed(4, false));
                var randomBytes = new byte[4];
                for (var i = 0; i < tokenLength; i++)
                {
                    prng.NextBytes(randomBytes, 0, 4);
                    sb.Append(Base62Chars[BitConverter.ToUInt32(randomBytes, 0) % 62]);
                }

                return sb.ToString();
            }
            catch (Exception exception)
            {
                Log.Instance.Error(exception);
                return string.Empty;
            }
        }

        #endregion
    }
}