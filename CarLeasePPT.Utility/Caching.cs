using System;
using System.Runtime.Caching;

namespace CarLeasePPT.Utility
{
    public static class Caching
    {
        #region Static Fields

        private static readonly object CacheLock = new object();

        #endregion

        #region Public Methods and Operators

        public static object GetCachedData(string cacheKey)
        {
            var o = MemoryCache.Default.Get(cacheKey, null);
            if (o != null) return o;
            lock (CacheLock)
            {
                o = MemoryCache.Default.Get(cacheKey, null);
                return o;
            }
        }

        public static bool SetCachedData(string cacheKey, object o, int cacheDuration)
        {
            try
            {
                var cip = new CacheItemPolicy
                {
                    AbsoluteExpiration = new DateTimeOffset(DateTime.Now.AddSeconds(cacheDuration)),
                    Priority = CacheItemPriority.NotRemovable
                };
                MemoryCache.Default.Set(cacheKey, o, cip);
                return true;
            }
            catch (Exception exception)
            {
                Log.Instance.Error(exception);
                return false;
            }
        }

        #endregion
    }
}