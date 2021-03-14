using CoffeeMachineModel;
using System;
using System.Linq;

namespace CoffeeMachineBusiness
{
    /// <summary>
    /// Pricing engine that computes prices of a recipe
    /// </summary>
    public class RecipePricingEngineWithMargin : IRecipePricingEngine
    {
        /// <summary>
        /// Margin value
        /// </summary>
        /// <example>0.3 for a 30% margin</example>
        protected decimal Margin { get; }
        public RecipePricingEngineWithMargin(decimal margin)
        {
            if (margin < 0)
                throw new ArgumentException("Margin cannot be nonpositive.");
            Margin = margin;
        }

        /// <summary>
        /// Computes the price of a recipe
        /// </summary>
        /// <param name="recipe">the recipe</param>
        /// <returns></returns>
        public decimal ComputePrice(Recipe recipe)
        {
            if (recipe == null)
                throw new ArgumentNullException(nameof(recipe));

            decimal price = recipe.Ingredients.Sum(kvp => kvp.Key.Price * kvp.Value) * (1 + Margin);
            return Math.Round(price, 2, MidpointRounding.ToEven);
        }
    }
}
