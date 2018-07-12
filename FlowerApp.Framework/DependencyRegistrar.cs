using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using FlowerApp.Core.Caching;
using FlowerApp.Data.Infrastructure;
using FlowerApp.Services.Product;

namespace FlowerApp.Framework
{
    public class DependencyRegistrar
    {
        /// <summary>
        /// Initialize method for DI
        /// </summary>
        /// <returns>Returns IContainer</returns>
        public static IContainer Init()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<MemoryCacheManager>().As<ICacheManager>().Named<ICacheManager>(CacheEnum.MemoryCache.ToString()).SingleInstance();
            builder.RegisterType<ProductService>().As<IProductService>().Named<IProductService>(ProductEnum.ProductService.ToString()).SingleInstance();
            builder.RegisterType<QueryExtension>().As<IQuery>().Named<IQuery>(QueryEnum.QueryExtension.ToString()).SingleInstance();
            return builder.Build();
        }
    }
}
