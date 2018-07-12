using System;
using System.Collections.Generic;
using System.Text;
using Dapper;
using FlowerApp.Data.Infrastructure;
using FlowerApp.Core.Caching;
using FlowerApp.Model.DataModel;
using NLog;

namespace FlowerApp.Services.Product
{
    public class ProductService : IProductService
    {
        /// <summary>
        /// The logger.
        /// </summary>
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// The cache manager.
        /// </summary>
        private readonly ICacheManager _cacheManager;
        private readonly IQuery _queryHelper;

        private readonly int commandTimeout = 600;
        private readonly int cacheTime = 60;
        private const string GetAllProductListCacheKey = "GetAllProductList";
        private const string GetProductByIDCacheKey = "GetProductByID";


        /// <summary>
        /// Initializes a new instance of the <see cref="ProductService"/> class.
        /// </summary>
        public ProductService(ICacheManager cacheManager, IQuery queryHelper)
        {
            this._cacheManager = cacheManager;
            this._queryHelper = queryHelper;
        }

        /// <summary>
        /// Get All Products From DB
        /// </summary>
        /// <param name="connectionString">The Connection String</param>
        /// <returns>Returns productList</returns>
        public List<Model.DataModel.Product> GetAllProductList(string connectionString)
        {
            return this._cacheManager.Get(GetAllProductListCacheKey, cacheTime,() => GetAll(connectionString));
        }

        /// <summary>
        /// Get All Products From DB
        /// </summary>
        /// <param name="connectionString">The connection string</param>
        /// <returns>Product List</returns>
        private List<Model.DataModel.Product> GetAll(string connectionString)
        {
            List<Model.DataModel.Product> product = new List<Model.DataModel.Product>();
            if (string.IsNullOrEmpty(connectionString))
            {
                return product;
            }

            try
            {
                const string query = @"SELECT * FROM v_Product";
                List<Model.DataModel.Product> result = _queryHelper.ToProductList(query, connectionString);
                return result;
            }
            catch (Exception exception)
            {
                Logger.Error(exception, "Data => ProductService => GetAll da hata meydana geldi.");
                return product;
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// The get product by ıd.
        /// </summary>
        /// <param name="productConnectionString"> The product Connection String.  </param>
        /// <param name="id"> The id.   </param>
        /// <returns> The <see cref="T:Ecommerce.Integration.Model.DataModel.Products.Product" />.   </returns>
        public Model.DataModel.Product GetProductById(string productConnectionString, int id)
        {
            var product = new Model.DataModel.Product();
            if (string.IsNullOrEmpty(productConnectionString) || id == default(int))
            {
                return product;
            }

            try
            {
                const string Query = @"SELECT * FROM v_Product WHERE ID=@Id";

                var parameters = new DynamicParameters();
                parameters.Add("@Id", id);

                List<Model.DataModel.Product> productList = _cacheManager.Get(GetProductByIDCacheKey,cacheTime, () =>_queryHelper.ToProductList(Query, productConnectionString, parameters));

                return productList.Count > 0 ? productList[0] : new Model.DataModel.Product();
            }
            catch (Exception exception)
            {
                Logger.Error(exception, "Data => ProductService => GetProductById da hata meydana geldi.");
                return product;
            }
        }



    }
}
