using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using CarLeasePPT.Utility;

namespace CarLeasePPT.DataAccessLayer
{
    public static class BlockedPasswordEngine
    {
        #region Properties

        private static int BlockedPasswordCacheDuration =>
            int.TryParse(ConfigurationManager.AppSettings["BlockedPasswordCacheDuration"], out var tcd) ? tcd : 3600;

        private static string BlockedPasswordCacheKey =>
            ConfigurationManager.AppSettings["BlockedPasswordCacheKeyName"] ??
            "hexa_default_43112fca5db9444ea5eb18a91faabc78";

        #endregion

        #region Public Methods and Operators

        public static IEnumerable<string> GetBlockedPasswords(bool refresh = false)
        {
            try
            {
                if (Caching.GetCachedData(BlockedPasswordCacheKey) is IEnumerable<string> blockedPasswords && !refresh)
                    return blockedPasswords;
                blockedPasswords = GetBlockedPasswordsFromDatabase();
                // Cache life of 60 seconds should reduce hits to database while ensuring double sessions don't work past 60 seconds
                Caching.SetCachedData(BlockedPasswordCacheKey, blockedPasswords, BlockedPasswordCacheDuration);
                return blockedPasswords;
            }
            catch (Exception exception)
            {
                Log.Instance.Error(exception);
                return new List<string>();
            }
        }

        #endregion

        #region Methods

        private static IEnumerable<string> GetBlockedPasswordsFromDatabase()
        {
            try
            {
                using (var e = new HexaEntities())
                {
                    var p = e.BlockedPasswords.Select(b => b.Password.ToUpper()).ToList();
                    return p;
                }
            }
            catch (Exception exception)
            {
                Log.Instance.Error(exception);
                return new List<string>();
            }
        }

        #endregion
    }
}