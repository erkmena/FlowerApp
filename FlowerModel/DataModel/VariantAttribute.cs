using System;
using System.Collections.Generic;
using System.Text;

namespace FlowerApp.Model.DataModel
{
    public class VariantAttribute
    {
        /// <summary>
        /// ID of VariantAttribute
        /// </summary>
        public int VariantAttributeID { get; set; }
        /// <summary>
        /// VariantID of VariantAttribute
        /// </summary>
        public int VariantAttributeVariantID { get; set; }
        /// <summary>
        /// AttributeOptionID of VariantAttribute
        /// </summary>
        public int VariantAttributeOptionID { get; set; }
    }
}
