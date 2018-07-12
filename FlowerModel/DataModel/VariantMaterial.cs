using System;
using System.Collections.Generic;
using System.Text;

namespace FlowerApp.Model.DataModel
{
    public class VariantMaterial
    {
        /// <summary>
        /// ID of VariantMaterial
        /// </summary>
        public int VariantMaterialID { get; set; }
        /// <summary>
        /// VariantID of VariantMaterial
        /// </summary>
        public int VariantMaterialVariantID { get; set; }
        /// <summary>
        /// MaterialID of Variant Material
        /// </summary>
        public int VariantMaterialMaterialID { get; set; }
        /// <summary>
        /// Number of Material in that Variant
        /// </summary>
        public int MaterialCount { get; set; }
    }
}
