using System;
using System.Collections.Generic;
using System.Text;

namespace FlowerApp.Model.DataModel
{
    public class Product
    {
        /// <summary>
        /// Constructor of Product
        /// </summary>
        public Product()
        {
            this.Variant = new List<Variant>();
        }
        /// <summary>
        /// ID of Product
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Name of Product
        /// </summary>
        public string ProductName { get; set; }
        /// <summary>
        /// Type Of Product
        /// </summary>
        public int ProductTypeID { get; set; }
        /// <summary>
        /// Variant of Product
        /// </summary>
        public List<Variant> Variant { get; set; }
    }
}
