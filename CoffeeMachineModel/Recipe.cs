using System;
using System.Collections.Generic;

namespace CoffeeMachineModel
{
    public class Recipe
    {
        #region Properties
        /// <summary>
        /// Id of the recipe
        /// </summary>
        /// <example>coffee, tea, chocolate</example>
        public string Id { get; }
        /// <summary>
        /// User friendly Label
        /// </summary>
        /// <example>Café, thé </example>
        public string Label { get; set; }

        /// <summary>
        /// Products with corresponding quantity of the recipe
        /// </summary>
        public Dictionary<Product, int> Ingredients { get; } = new Dictionary<Product, int>();
        #endregion Properties

        #region Constructors
        public Recipe(string id, string label, Dictionary<Product, int> ingredients)
        {
            Id = id;
            Label = label;
            Ingredients = ingredients;
        }
        #endregion Constructors
        public override string ToString()
        {
            return Id;
        }

    }
}
