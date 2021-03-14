using CoffeeMachineModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoffeeMachineDataProvider
{
    public interface ICoffeeMachineProvider
    {
        /// <summary>
        /// Loads all recipes
        /// </summary>
        /// <returns></returns>
        public List<Recipe> LoadRecipes();

        /// <summary>
        /// Loads all products
        /// </summary>
        /// <returns></returns>
        public List<Product> LoadProducts();
    }
}
