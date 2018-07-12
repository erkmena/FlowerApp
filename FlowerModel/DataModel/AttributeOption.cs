using System;
using System.Collections.Generic;
using System.Text;

namespace FlowerApp.Model.DataModel
{
    /// <summary>
    /// AttributeOption
    /// </summary>
    public class AttributeOption
    {
        /// <summary>
        /// ID of AttributeOption
        /// </summary>
        public int AttributeOptionID { get; set; }

        /// <summary>
        /// Attribute ID of Attribute Option
        /// </summary>
        public int AttributeOptionAttributeID { get; set; }
        /// <summary>
        /// Value Text of Attribute Option
        /// </summary>
        public string ValueText { get; set; }
    }
}
