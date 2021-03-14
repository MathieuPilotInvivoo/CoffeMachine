using CoffeeMachineModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoffeeMachineBusiness
{
    /// <summary>
    /// Interface of a pricing engine for recipes
    /// </summary>
    public interface IRecipePricingEngine
    {
        /// <summary>
        /// Computes the price of a recipe
        /// </summary>
        /// <param name="recipe">the recipe</param>
        /// <returns></returns>
        public Decimal ComputePrice(Recipe recipe);
    }
}
