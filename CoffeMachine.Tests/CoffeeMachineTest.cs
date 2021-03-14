using System;
using System.Collections.Generic;
using System.Linq;
using CoffeeMachineBusiness;
using CoffeeMachineDataProvider;
using CoffeeMachineModel;
using CoffeMachineGui;
using Moq;
using NUnit.Framework;

namespace CoffeMachine.Tests
{
    public class CoffeeMachineTest
    {
        protected Mock<ICoffeeMachineProvider> mockProvider;

        
        [SetUp]
        public void Setup()
        {
            var coffee = new Product("coffee", "Café", 1m);
            var sugar = new Product("sugar", "Sucre", 0.1m);
            var testProduct = new Product("test", "Test rounding", 0.25m);

            List<Product> products = new List<Product>
            {
                coffee,
                sugar,
                testProduct
            };

            List<Recipe> recipes = new List<Recipe>
            {
                new Recipe("coffee", "café simple", new Dictionary<Product, int>{ { coffee, 1 } }),
                new Recipe("sugarcoffee", "café sucré", new Dictionary<Product, int>{ { coffee, 2 }, { sugar, 1 } }),
                new Recipe("roundingpricecoffee", "prix arrondi", new Dictionary<Product, int>{ { testProduct, 1 } })
            };

            mockProvider = new Mock<ICoffeeMachineProvider>();
            mockProvider.Setup(m => m.LoadRecipes()).Returns(recipes);
        }

        [Test]
        public void GivenPricingEngine_WhenMarginNonPositive_ThenThrowException()
        {
            Assert.Throws<ArgumentException>(() => new RecipePricingEngineWithMargin(-1));
        }

        [TestCase("coffee")]
        [TestCase("sugarcoffee")]
        [TestCase("roundingpricecoffee")]
        [Test]
        public void GivenRecipe_WhenMargin0_ThenSumPrices(string recipeName)
        {
            var recipe = mockProvider.Object.LoadRecipes().Single(r => r.Id == recipeName);
            RecipePricingEngineWithMargin pricer = new RecipePricingEngineWithMargin(0);

            decimal price = pricer.ComputePrice(recipe);
            decimal expectedPrice = recipe.Ingredients.Sum(kvp => kvp.Key.Price * kvp.Value);
            Assert.AreEqual(price, expectedPrice);
        }

        [TestCase(0, 1)]
        [TestCase(0.1, 1.1)]
        [TestCase(0.5, 1.5)]
        [Test]
        public void GivenRecipe_WhenMarginModified_ThenPriceModified(decimal margin, decimal result)
        {
            var recipe = mockProvider.Object.LoadRecipes().Single(r => r.Id == "coffee");
            var pricer = new RecipePricingEngineWithMargin(margin);
            Assert.AreEqual(pricer.ComputePrice(recipe), result);
        }

        [TestCase(0.5, 0.38)]
        [Test]
        public void GivenRecipeWithMargin_WhenMoreThan3Digits_ThenPriceIsRounded(decimal margin, decimal result)
        {
            var recipe = mockProvider.Object.LoadRecipes().Single(r => r.Id == "roundingpricecoffee");
            var pricer = new RecipePricingEngineWithMargin(margin);
            Assert.AreEqual(pricer.ComputePrice(recipe), result);
        }

        [Test]
        public void GivenCoffeeScreen_WhenRecipeModified_PriceUpdate()
        {
            Mock<CoffeeMachineVM> coffeeVM = new Mock<CoffeeMachineVM>();
            coffeeVM.Setup(m => m.LoadDatas(It.IsAny<ICoffeeMachineProvider>()));

            var recipes = mockProvider.Object.LoadRecipes();
            coffeeVM.Object.Recipes = new System.Collections.ObjectModel.ObservableCollection<Recipe>(recipes);

            Assert.IsNull(coffeeVM.Object.Price);

            coffeeVM.Object.SelectedRecipe = recipes.Single(r => r.Id == "coffee");

            Assert.AreEqual(coffeeVM.Object.Price, 1.3m);
        }
    }
}