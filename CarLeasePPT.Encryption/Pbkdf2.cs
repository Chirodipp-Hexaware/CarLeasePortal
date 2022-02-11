using System;
using System.Collections.Generic;
using System.Configuration;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Macs;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using CarLeasePPT.Utility;

namespace CarLeasePPT.Encryption
{
    public static class Pbkdf2
    {
        #region Constants

        private const int HashByteSize = 256;

        private const int SaltByteSize = 64;

        #endregion

        #region Properties

        private static int Iterations =>
            int.TryParse(ConfigurationManager.AppSettings["EncryptionIterations"] ?? "40", out var i) ? i : 40;

        private static byte[] Pepper =>
            Convert.FromBase64String(ConfigurationManager.AppSettings["Pbkdf2Pepper"] ?? "");

        #endregion

        #region Public Methods and Operators

        public static string Hash(string password)
        {
            try
            {
                var mainHashingLib = new SecureRandom();
                var saltBytes = new byte[SaltByteSize];
                mainHashingLib.NextBytes(saltBytes);
                var saltString = Convert.ToBase64String(saltBytes);
                var pepperedSaltBytes = HmacSha512Hash(saltBytes, Pepper);
                var passwordHash =
                    Convert.ToBase64String(
                        PBKDF2_SHA512_GetHash(password, pepperedSaltBytes, Iterations * 1000, HashByteSize));
                return $"pbkdf2:{saltString}:{Iterations:D0}:{passwordHash}";
            }
            catch (Exception exception)
            {
                Log.Instance.Error(exception);
                return string.Empty;
            }
        }

        public static bool Verify(string password, string hash)
        {
            try
            {
                var hashArray = hash.Split(':');
                if (!hashArray.Length.Equals(4))
                {
                    Log.Instance.Error($"Hash array length is {hashArray.Length} instead of 4.");
                    return false;
                }

                var saltString = hashArray[1];
                var saltBytes = Convert.FromBase64String(saltString);
                var iterationsBaseString = hashArray[2];
                var hashString = hashArray[3];

                if (!int.TryParse(iterationsBaseString, out var iterations))
                {
                    Log.Instance.Error($"Unable to parse iterations of '{iterationsBaseString}'.");
                    return false;
                }

                iterations *= 1000;
                var pepperedSaltBytes = HmacSha512Hash(saltBytes, Pepper);
                var isValid = ValidatePassword(password, pepperedSaltBytes, iterations, HashByteSize,
                    Convert.FromBase64String(hashString));
                return isValid;
            }
            catch (Exception exception)
            {
                Log.Instance.Error(exception);
                return false;
            }
        }

        #endregion

        #region Methods

        private static byte[] HmacSha512Hash(byte[] textBytes, byte[] keyBytes)
        {
            var hmac = new HMac(new Sha512Digest());
            hmac.Init(new KeyParameter(keyBytes));
            var result = new byte[hmac.GetMacSize()];
            hmac.BlockUpdate(textBytes, 0, textBytes.Length);
            hmac.DoFinal(result, 0);
            return result;
        }

        private static byte[] PBKDF2_SHA512_GetHash(string password, byte[] salt, int iterations, int hashByteSize)
        {
            var pdb = new Pkcs5S2ParametersGenerator(new Sha512Digest());
            pdb.Init(PbeParametersGenerator.Pkcs5PasswordToBytes(password.ToCharArray()), salt, iterations);
            var key = (KeyParameter) pdb.GenerateDerivedMacParameters(hashByteSize * 8);
            return key.GetKey();
        }

        private static bool SlowEquals(IReadOnlyList<byte> a, IReadOnlyList<byte> b)
        {
            var diff = (uint) a.Count ^ (uint) b.Count;
            for (var i = 0; i < a.Count && i < b.Count; i++)
                diff |= (uint) (a[i] ^ b[i]);
            return diff == 0;
        }

        private static bool ValidatePassword(string password, byte[] saltBytes, int iterations, int hashByteSize,
            IReadOnlyList<byte> storedPasswordHashAsByteArray)
        {
            var regeneratedPasswordHash = PBKDF2_SHA512_GetHash(password, saltBytes, iterations, hashByteSize);
            return SlowEquals(storedPasswordHashAsByteArray, regeneratedPasswordHash);
        }

        #endregion
    }
}