using FlowerApp.Core.Caching;
using NLog;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlowerApp.Helper
{

    public class ConfigurationHelper
    {
        private const string GetItemsCountNotDisplayedCacheKey = "GetItemsCountNotDisplayed";
        private const string ConnectionStringCacheKey = "CacheKey";
        static Logger logger = NLog.LogManager.LoadConfiguration("nlog.config").GetCurrentClassLogger();

        /// <summary>
        /// Get method of Connection string
        /// </summary>
        /// <param name="_cacheManager"></param>
        /// <returns>returns connection string</returns>
        public static string ConnectionString(ICacheManager _cacheManager)
        {
            return _cacheManager.Get(ConnectionStringCacheKey, ()=>System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString()); //Çalışma süresi boyunca değişmeyecek bir değer. Cacheden okunabilir.
        }
        /// <summary>
        /// Get method of Items which numbers will not be displayed on output.
        /// </summary>
        /// <param name="_cacheManager"></param>
        /// <returns>Returns list of int which stores item ids</returns>
        public static List<int> GetMaterialIDsWhichCountWillNotBeDisplayed(ICacheManager _cacheManager)
        {
            List<int> itemCountNotDisplayed = new List<int>();

            try
            {
                itemCountNotDisplayed = _cacheManager.Get(GetItemsCountNotDisplayedCacheKey, () => GetItemsCountNotDisplayed()); // Anlık değişen bir değişken değil. Her seferinde config'e gidip okumaya gerek yok.
            }
            catch (Exception ex)
            {
                logger.Error(ex, "ConfigurationHelper => GetMaterialIDsWhichCountWillNotBeDisplayed da hata meydana geldi.");

                //Don't throw ex. This part is not so important.
            }
            return itemCountNotDisplayed;

        }
        /// <summary>
        /// Get Items Count Which will be not displayed
        /// </summary>
        /// <returns>List of item ids</returns>
        private static List<int> GetItemsCountNotDisplayed()
        {

            string itemCountString = System.Configuration.ConfigurationManager.AppSettings["ItemCountNotDisplayed"].ToString();

            return new List<int>( Array.ConvertAll(itemCountString.Split(','), int.Parse));
        }
    }
}
