using FlowerApp.Model.DataModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace FlowerApp.Data.Infrastructure
{
    /// <summary>
    /// The IQuery interface for Queryable classes.
    /// </summary>
    public interface IQuery
    {
        List<T> ToListQuery<T>(string query, string connectionString, object param = null, CommandType? commandType = null, IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = null);

        /// <summary>
        /// Main query for gettin products
        /// </summary>
        /// <param name="query">query to execute</param>
        /// <param name="connectionString">The connection string</param>
        /// <param name="param">Params for queries</param>
        /// <param name="commandType">Type of command</param>
        /// <param name="transaction"></param>
        /// <param name="buffered"></param>
        /// <param name="commandTimeout">The Command Timeout</param>
        /// <returns>List of Products</returns>
        List<Product> ToProductList(string query, string connectionString, object param = null, CommandType? commandType = null, IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = null);
    }
}
