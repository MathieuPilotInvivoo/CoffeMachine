using System;
using System.Collections.Generic;
using System.Text;

namespace CoffeeMachineModel
{
    public class Product
    {
        #region Properties
        /// <summary>
        /// Id of the product
        /// </summary>
        /// <example>sugar, coffee, tea</example>
        public string Id { get; }
        /// <summary>
        /// User friendly Label
        /// </summary>
        /// <example>Sucre, Café, thé </example>
        public string Label { get; set; }
        /// <summary>
        /// Price in euro
        /// </summary>
        public decimal Price { get; set; }
        #endregion Properties

        #region Constructors
        public Product(string id, string label, decimal price)
        {
            Id = id;
            Label = label;
            Price = price;
        }
        #endregion Constructors

        public override string ToString()
        {
            return Id;
        }
    }
}
