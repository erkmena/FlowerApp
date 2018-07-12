using FlowerApp.Model.DataModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlowerApp.Helper
{
    class ScreenHelper
    {
        /// <summary>
        /// Main method for writing console
        /// </summary>
        /// <param name="products">All products of system</param>
        /// <param name="itemsWhichCountWillNotBeDisplayed">List of item ids which counts will not be displayed</param>
        internal static void WriteItemsToConsole(List<Product> products, List<int> itemsWhichCountWillNotBeDisplayed)
        {
            foreach (Product productItem in products)// Look for all product
            {
                Console.WriteLine(productItem.ProductName + ':'); // Write product Name
                for (int i = 0; i < productItem.Variant.Count; i++)// Look for product variants
                {
                    Variant currentVariant = productItem.Variant[i];
                    Console.WriteLine('\t' + currentVariant.VariantAttributeOptionValueText);
                    string ingredits = "\t\tİçerik : "; // Prepare string to write for that variant
                    for (int k = 0; k < currentVariant.Material.Count; k++)
                    {
                        Material currentMaterial = currentVariant.Material[k];
                        if (itemsWhichCountWillNotBeDisplayed.Contains(currentMaterial.MaterialID))// If items count will not be displayed enter this scope (Like 'süslemeler')
                        {
                            ingredits += String.Format("{0}", currentMaterial.MaterialName);
                        }
                        else
                        {
                            ingredits += String.Format("{0} adet {1}", currentVariant.VariantMaterial[k].MaterialCount, currentMaterial.MaterialName);
                        }
                        if (k != currentVariant.Material.Count - 1)
                        {
                            ingredits += "  +  ";
                        }
                    }
                    Console.WriteLine(ingredits);
                    Console.WriteLine("\t\tFiyat : {0} TL", currentVariant.Price); // Write down price of variant.

                }
            }
        }
    }
}
