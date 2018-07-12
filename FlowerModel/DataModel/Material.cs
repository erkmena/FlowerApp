using System;
using System.Collections.Generic;
using System.Text;

namespace FlowerApp.Model.DataModel
{
    public class Material
    {
        /// <summary>
        /// ID of Material
        /// </summary>
        public int MaterialID { get; set; }

        /// <summary>
        /// Name Of Material
        /// </summary>
        public string MaterialName { get; set; }

        /// <summary>
        /// Attribute Option of Material
        /// </summary>
        public List<AttributeOption> MaterialAttributeOption { get; set; }

        /// <summary>
        /// Attribute of Material
        /// </summary>
        public List<MaterialAttribute> MaterialAttribute { get; set; }
    }
}
