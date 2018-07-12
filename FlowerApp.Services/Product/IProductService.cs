using System;
using System.Collections.Generic;
using System.Text;
using FlowerApp.Model.DataModel;
namespace FlowerApp.Services.Product
{
    public interface IProductService
    {
        /// <summary>
        /// The get product by id.
        /// </summary>
        /// <param name="productConnectionString"> The product Connection String. </param>
        /// <param name="id"> The id. </param>
        /// <returns> The <see cref="ProductService"/>.  </returns>
        Model.DataModel.Product GetProductById(string productConnectionString, int id);
        /// <summary>
        /// The get all product list
        /// </summary>
        /// <param name="productConnectionString"> The product Connection String.</param>
        /// <returns> The <see cref="ProductService"/>. </returns>
        List<Model.DataModel.Product> GetAllProductList(string productConnectionString);

    }
}
