using System;
using System.Collections.Generic;
using System.Text;

namespace CoffeeMachineDataProvider.Poco
{
    internal class RecipePoco
    {
        public string Id { get; set; }
        public string Label { get; set; }
        public Dictionary<string, int> Ingredients { get; set; } = new Dictionary<string, int>();
    }
}
