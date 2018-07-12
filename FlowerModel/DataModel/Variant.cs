using System;
using System.Collections.Generic;
using System.Text;

namespace FlowerApp.Model.DataModel
{
    public class Variant
    {
        /// <summary>
        /// ID of Variant
        /// </summary>
        public int VariantID { get; set; }

        /// <summary>
        /// ID of Product for Variant
        /// </summary>
        public int ProductID { get; set; }
        /// <summary>
        /// Price of Variant
        /// </summary>
        public float Price { get; set; }
        /// <summary>
        /// Middle object between Variant And Attribute
        /// </summary>
        public List<VariantAttribute> VariantAttribute { get; set; }
        /// <summary>
        /// Midde object between Variant And Material
        /// </summary>
        public List<VariantMaterial> VariantMaterial { get; set; }
        /// <summary>
        /// Material list of Variant
        /// </summary>
        public List<Material> Material { get; set; }
        /// <summary>
        /// Size of Variant
        /// </summary>
        public string VariantAttributeOptionValueText { get; set; }
    }
}
