using System;
using System.Collections.Concurrent;

namespace FlowerApp.Core.Caching
{
    /// <summary>
    /// The cache extensions.
    /// </summary>
    public static class CacheExtensions
    {
        private readonly static ConcurrentDictionary<string, object> _lockers = new ConcurrentDictionary<string, object>();

        /// <summary> 
        /// The get. 
        /// </summary>
        /// <param name="cacheManager"> The cache manager. </param>
        /// <param name="key"> The key. </param>
        /// <param name="acquire"> The acquire. </param>
        /// <typeparam name="T">Type parameter </typeparam>
        /// <returns> The <see cref="T"/> Result Object </returns>
        public static T Get<T>(this ICacheManager cacheManager, string key, Func<T> acquire)
        {
            return Get(cacheManager, key, 60, acquire);
        }

        /// <summary> 
        /// The get. 
        /// </summary>
        /// <param name="cacheManager"> The cache manager. </param>
        /// <param name="key"> The key. </param>
        /// <param name="cacheTime"> The cache time. </param>
        /// <param name="acquire"> The acquire. </param>
        ///  <typeparam name="T">type parameter </typeparam>
        /// <returns> The <see cref="T"/> result object </returns>
        public static T Get<T>(this ICacheManager cacheManager, string key, int cacheTime, Func<T> acquire)
        {
            T retVal;
            bool isExist = cacheManager.IsSet(key);

            if (isExist)
            {
                retVal = cacheManager.Get<T>(key);
            }
            else
            {
                object locker = _lockers.GetOrAdd(key, new object());
                lock (locker)
                {
                    isExist = cacheManager.IsSet(key);
                    if (isExist)
                    {
                        retVal = cacheManager.Get<T>(key);
                    }
                    else
                    {
                        retVal = acquire();
                        cacheManager.Set(key, retVal, cacheTime);
                    }
                }
            }

            return retVal;
        }

        /// <summary> 
        /// The set. 
        /// </summary>
        /// <param name="cacheManager"> The cache manager. </param>
        /// <param name="key"> The key. </param>
        /// <param name="data"> The data. </param>
        public static void Set(this ICacheManager cacheManager, string key, object data)
        {
            cacheManager.Set(key, data, 60);
        }
    }
}
