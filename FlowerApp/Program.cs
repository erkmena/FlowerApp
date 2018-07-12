using System;
using Autofac;
using System.Reflection;
using FlowerApp.Model.DataModel;
using FlowerApp.Services.Product;
using FlowerApp.Data.Infrastructure;
using FlowerApp.Core.Caching;
using FlowerApp.Framework;
using FlowerApp.Helper;

namespace FlowerApp
{
    class Program
    {
        static IProductService _productService;
        static ICacheManager _cacheManager;
        static IQuery _query;

        /// <summary>
        /// Main Console Function
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            IContainer container = DependencyRegistrar.Init();

            _productService = container.Resolve<IProductService>();
            _cacheManager = container.Resolve<ICacheManager>();
            _query = container.Resolve<IQuery>();

            var list = _productService.GetAllProductList(ConfigurationHelper.ConnectionString(_cacheManager));
            ScreenHelper.WriteItemsToConsole(list, ConfigurationHelper.GetMaterialIDsWhichCountWillNotBeDisplayed(_cacheManager));
        }
        
    }
}
