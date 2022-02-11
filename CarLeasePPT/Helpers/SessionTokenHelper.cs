using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using CarLeasePPT.DataAccessLayer;
using CarLeasePPT.Utility;

namespace CarLeasePPT.Helpers
{
    public static class SessionTokenHelper
    {
        #region Properties

        private static int TokenCacheDuration =>
            int.TryParse(ConfigurationManager.AppSettings["TokenCacheDuration"], out var tcd) ? tcd : 60;

        private static string TokenCacheKey => ConfigurationManager.AppSettings["TokenCacheKeyName"] ??
                                               "hexa_default_f79a8f5558584c5989aab6588419c646";

        #endregion

        #region Public Methods and Operators

        public static async Task<bool> AddToken(int personId, string token)
        {
            try
            {
                //Create/Update the session token in the database
                await PersonEngine.SessionCreateAsync(personId, token);
                //Force a cache refresh
                GetSessionTokens(true);
                return true;
            }
            catch (Exception exception)
            {
                Log.Instance.Error(exception);
                return false;
            }
        }

        public static bool IsTokenValid(int personId, string token)
        {
            try
            {
                var sessionTokens = GetSessionTokens();
                if (!sessionTokens.ContainsKey(personId)) sessionTokens = GetSessionTokens(true);
                var e = sessionTokens.SingleOrDefault(t => t.Key.Equals(personId)).Value;
                // Only allow when token exists and matches
                return e != null && e.Equals(token);
            }
            catch (Exception exception)
            {
                Log.Instance.Error(exception);
                // Return false if exception thrown
                return false;
            }
        }

        public static async Task<bool> RemoveToken(int personId)
        {
            try
            {
                return await PersonEngine.SessionDeleteAsync(personId);
            }
            catch (Exception exception)
            {
                Log.Instance.Error(exception);
                return false;
            }
        }

        #endregion

        #region Methods

        private static Dictionary<int, string> GetSessionTokens(bool refresh = false)
        {
            try
            {
                if (Caching.GetCachedData(TokenCacheKey) is Dictionary<int, string> sessionTokens && !refresh)
                    return sessionTokens;
                sessionTokens = PersonEngine.GetAllSessions();
                // Cache life of 60 seconds should reduce hits to database while ensuring double sessions don't work past 60 seconds
                Caching.SetCachedData(TokenCacheKey, sessionTokens, TokenCacheDuration);
                return sessionTokens;
            }
            catch (Exception exception)
            {
                Log.Instance.Error(exception);
                return new Dictionary<int, string>();
            }
        }

        #endregion
    }
}