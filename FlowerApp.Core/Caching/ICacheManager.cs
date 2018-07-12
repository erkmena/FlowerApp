﻿namespace FlowerApp.Core.Caching
{
    /// <summary> Cache manager interface </summary>
    public interface ICacheManager
    {
        /// <summary> Gets or sets the value associated with the specified key. </summary>
        /// <typeparam name="T">Type parameter</typeparam>
        /// <param name="key">The key of the value to get.</param>
        /// <returns>The value associated with the specified key.</returns>
        T Get<T>(string key);

        /// <summary> Adds the specified key and object to the cache. </summary>
        /// <param name="key">Type parameter</param>
        /// <param name="data">Data object</param>
        /// <param name="cacheTime">Cache time</param>
        void Set(string key, object data, int cacheTime);

        /// <summary> Gets a value indicating whether the value associated with the specified key is cached </summary>
        /// <param name="key">key value</param>
        /// <returns>key is set</returns>
        bool IsSet(string key);

        /// <summary> Removes the value with the specified key from the cache </summary>
        /// <param name="key">key to be removed</param>
        void Remove(string key);

        /// <summary> Removes items by pattern </summary>
        /// <param name="pattern">pattern to be searched</param>
        void RemoveByPattern(string pattern);

        /// <summary> Clear all cache data </summary>
        void Clear();
    }
}