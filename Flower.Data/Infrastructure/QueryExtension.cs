using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlowerApp.Model;
using FlowerApp.Model.DataModel;

namespace FlowerApp.Data.Infrastructure
{
    public class QueryExtension : IQuery
    {
        /// <summary>
        /// The to list query.
        /// </summary>
        /// <param name="query"> The query. </param>
        /// <param name="connectionString"> The connection string. </param>
        /// <param name="param"> The param. </param>
        /// <param name="transaction"> The transaction. </param>
        /// <param name="buffered"> The buffered. </param>
        /// <param name="commandTimeout"> The command timeout. </param>
        /// <param name="commandType"> The command type. </param>
        /// <typeparam name="T"> </typeparam> <returns>
        /// The <see cref="List{T}"/>. </returns>
        public List<T> ToListQuery<T>(string query, string connectionString, object param = null, CommandType? commandType = null, IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = null)
        {
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                FlowerApp.Model.DataModel.Product product = new FlowerApp.Model.DataModel.Product();
                return sqlConnection.Query<T>(query, param, transaction, buffered, commandTimeout, commandType).ToList();

            }
        }
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
        public List<Product> ToProductList(string query, string connectionString, object param = null, CommandType? commandType = null, IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = null)
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);


            var productDictionary = new Dictionary<int, Product>();
            var variantDictionary = new Dictionary<int, Variant>();
            var materialDictionary = new Dictionary<int, Material>();

            var resultList = sqlConnection.Query<Product, Variant, VariantAttribute, VariantMaterial, Material, MaterialAttribute, AttributeOption, Product>
                (query, (product, variant, variantAttribute, variantMaterial, material, materialAttribute, attributeOption) =>
                {
                    Product productEntry;
                    Variant variantEntry;
                    Material materialEntry;

                    if (!productDictionary.TryGetValue(product.ID, out productEntry))
                    {
                        productEntry = product;
                        productEntry.Variant = new List<Variant>();
                        productDictionary.Add(productEntry.ID, productEntry);
                    }

                    if (!variantDictionary.TryGetValue(variant.VariantID, out variantEntry))
                    {
                        variantEntry = variant;
                        variantEntry.VariantMaterial = new List<VariantMaterial>();
                        variantEntry.VariantAttribute = new List<VariantAttribute>();
                        variantEntry.Material = new List<Material>();

                        variantDictionary.Add(variantEntry.VariantID, variantEntry);
                    }

                    if (!materialDictionary.TryGetValue(material.MaterialID, out materialEntry))
                    {
                        materialEntry = material;
                        materialEntry.MaterialAttributeOption = new List<AttributeOption>();
                        materialEntry.MaterialAttribute = new List<MaterialAttribute>();


                        materialDictionary.Add(materialEntry.MaterialID, materialEntry);
                    }
                    if (materialEntry.MaterialAttribute.FindIndex(m => m.MaterialAttributeID == materialAttribute.MaterialAttributeID) < 0)
                    {
                        materialEntry.MaterialAttribute.Add(materialAttribute);
                    }
                    if (materialEntry.MaterialAttributeOption.FindIndex(m => m.AttributeOptionID == attributeOption.AttributeOptionID) < 0)
                    {
                        materialEntry.MaterialAttributeOption.Add(attributeOption);
                    }


                    int materialIndex = variantEntry.Material.FindIndex(p => p.MaterialID == material.MaterialID);

                    if (materialIndex >= 0)
                    {
                        variantEntry.Material[materialIndex] = materialEntry;
                    }
                    else
                    {
                        variantEntry.Material.Add(materialEntry);
                    }

                    int variantIndex = productEntry.Variant.FindIndex(p => p.VariantID == variant.VariantID);

                    if (variantIndex >= 0)
                    {
                        productEntry.Variant[variantIndex] = variantEntry;
                    }
                    else
                    {
                        productEntry.Variant.Add(variantEntry);
                    }

                    if (variantEntry.VariantAttribute.FindIndex(v => v.VariantAttributeID == variantAttribute.VariantAttributeID) < 0)
                        variantEntry.VariantAttribute.Add(variantAttribute);
                    if (variantEntry.VariantMaterial.FindIndex(v => v.VariantMaterialID == variantMaterial.VariantMaterialID) < 0)
                        variantEntry.VariantMaterial.Add(variantMaterial);

                    return productEntry;
                }, param: param, splitOn: "VariantId, VariantAttributeID, VariantMaterialID,MaterialID,MaterialAttributeID,AttributeOptionID").Distinct().ToList();

            return resultList;
        }
    }
}
