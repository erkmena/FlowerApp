using System;
using System.Linq;
using System.Runtime.Caching;
using System.Text.RegularExpressions;

namespace FlowerApp.Core.Caching
{
    /// <inheritdoc />
    /// <summary>
    /// Represents a MemoryCacheCache
    /// </summary>
    public class MemoryCacheManager : ICacheManager
    {
        /// <summary>
        /// Gets the cache.
        /// </summary>
        protected ObjectCache Cache
        {
            get
            {
                return MemoryCache.Default;
            }
        }

        /// <summary>
        /// Gets or sets the value associated with the specified key.
        /// </summary>
        /// <typeparam name="T">Type parameter</typeparam>
        /// <param name="key">The key of the value to get.</param>
        /// <returns>The value associated with the specified key.</returns>
        public T Get<T>(string key)
        {
            return (T)this.Cache[key];
        }

        /// <summary>
        /// Adds the specified key and object to the cache.
        /// </summary>
        /// <param name="key">key value</param>
        /// <param name="data">Data object</param>
        /// <param name="cacheTime">Cache time</param>
        public void Set(string key, object data, int cacheTime)
        {
            if (data == null)
            {
                return;
            }

            var policy = new CacheItemPolicy { AbsoluteExpiration = DateTime.Now + TimeSpan.FromMinutes(cacheTime) };
            this.Cache.Add(new CacheItem(key, data), policy);
        }

        /// <summary>
        /// Gets a value indicating whether the value associated with the specified key is cached
        /// </summary>
        /// <param name="key">key value</param>
        /// <returns>key value is set</returns>
        public bool IsSet(string key)
        {
            return this.Cache.Contains(key);
        }

        /// <summary>
        /// Removes the value with the specified key from the cache
        /// </summary>
        /// <param name="key">key value</param>
        public void Remove(string key)
        {
            this.Cache.Remove(key);
        }

        /// <summary>
        /// Removes items by pattern
        /// </summary>
        /// <param name="pattern">pattern to be searched</param>
        public void RemoveByPattern(string pattern)
        {
            var regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
            var keysToRemove = (from item in this.Cache where regex.IsMatch(item.Key) select item.Key).ToList();

            foreach (var key in keysToRemove)
            {
                this.Remove(key);
            }
        }

        /// <summary>
        /// Clear all cache data
        /// </summary>
        public void Clear()
        {
            foreach (var item in this.Cache)
            {
                this.Remove(item.Key);
            }
        }
    }
}