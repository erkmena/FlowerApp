using System;
using System.Collections.Generic;
using System.Text;

namespace FlowerApp.Model.DataModel
{
    public class MaterialAttribute
    {
        /// <summary>
        /// ID Of MaterialAttribute Row
        /// </summary>
        public int MaterialAttributeID { get; set; }

        /// <summary>
        /// Attribute Option of middle table 'MaterialAttribute'
        /// </summary>
        public int MaterialAttributeAttributeOptionID { get; set; }

        /// <summary>
        /// Material ID of middle table 'MaterialAttribute'
        /// </summary>
        public int MaterialAttributeMaterialID { get; set; }
    }
}
