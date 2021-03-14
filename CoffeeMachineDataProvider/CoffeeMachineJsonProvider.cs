using CoffeeMachineDataProvider.Poco;
using CoffeeMachineModel;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CoffeeMachineDataProvider
{
    /// <summary>
    /// Load recipes and products from JSon files
    /// </summary>
    public class CofferMachineJsonProvider : ICoffeeMachineProvider
    {
        public List<Product> LoadProducts()
        {
            string jsonString = File.ReadAllText(@".\Datas\Products.json");
            return JsonConvert.DeserializeObject<List<Product>>(jsonString);
        }

        public List<Recipe> LoadRecipes()
        {
            string jsonString = File.ReadAllText(@".\Datas\Recipes.json");
            List<RecipePoco> pocos = JsonConvert.DeserializeObject<List<RecipePoco>>(jsonString);

            var products = LoadProducts().ToDictionary(p => p.Id, p => p);
            List<Recipe> recipes = new List<Recipe>();
            foreach(var poco in pocos)
            {
                recipes.Add(new Recipe(poco.Id, poco.Label, poco.Ingredients.ToDictionary(i => products[i.Key], i => i.Value)));
            }
            return recipes;
        }
    }
}
