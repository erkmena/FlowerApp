using Dapper;
using FlowerApp.Core.Caching;
using FlowerApp.Data.Infrastructure;
using FlowerApp.Helper;
using FlowerApp.Model.DataModel;
using FlowerApp.Services.Product;
using System;
using System.Collections.Generic;
using Xunit;

namespace FlowerApp.UnitTest
{
    public class UnitTest1
    {
        private const string connectionString = "Data Source=.;Initial Catalog=TestDB;Integrated Security=True";

        bool isFromCache = true;

        IQuery _queryHelper;
        ICacheManager _cacheManager;
        IProductService _productService;
        
        public UnitTest1()
        {
            _queryHelper = new QueryExtension();
            _cacheManager = new MemoryCacheManager();
            _productService = new ProductService(_cacheManager,_queryHelper);
        }

        [Fact]
        public void Test1()
        {
            const string query = "Select * FROM v_Product";
            var result = _queryHelper.ToProductList(query, connectionString);
            var product = result;
        }
        [Fact]
        public void GetProductByIDTest()
        {
            const string Query = @"SELECT * FROM v_Product WHERE ID=@Id";

            var parameters = new DynamicParameters();
            parameters.Add("@Id", 1);

            var product = _queryHelper.ToProductList(Query, connectionString, parameters);
        }

        [Fact]
        public void CachingTest()
        {
            _cacheManager.Get("returnInt", () => returnInt());
            Console.WriteLine(isFromCache.ToString());
            isFromCache = true;
            _cacheManager.Get("returnInt", () => returnInt());
            Console.WriteLine(isFromCache.ToString());

        }

        int returnInt()
        {
            isFromCache = false;
            return 1;
        }

        [Fact]
        public void ProductServiceGetAllProductsTest()
        {
            var allProducts = _productService.GetAllProductList(connectionString);
            var productByID = _productService.GetProductById(connectionString, 1);
        }
    }
}
