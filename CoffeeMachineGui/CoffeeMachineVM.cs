using CoffeeMachineDataProvider;
using CoffeeMachineModel;
using CoffeeMachineBusiness;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CoffeMachineGui
{
    public class CoffeeMachineVM : INotifyPropertyChanged
    {
        #region Properties
        public ObservableCollection<Recipe> Recipes { get; set; }

        private Recipe _selectedRecipe;
        public Recipe SelectedRecipe
        {
            get
            {
                return _selectedRecipe;
            }
            set
            {
                if (value != _selectedRecipe)
                {
                    _selectedRecipe = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(Price));
                }
            }
        }

        public decimal? Price
        {
            get
            {
                return ComputePrice();
            }
        }
        #endregion Properties

        public CoffeeMachineVM()
        {
            if (!App.IsDesignMode)
            {
                ICoffeeMachineProvider provider = new CofferMachineJsonProvider();
                LoadDatas(provider);
            }
        }

        public virtual void LoadDatas(ICoffeeMachineProvider provider)
        {
            Recipes = new ObservableCollection<Recipe>(provider.LoadRecipes());
            OnPropertyChanged(nameof(Recipes));
        }

        private decimal? ComputePrice()
        {
            if (SelectedRecipe == null)
                return null;

            IRecipePricingEngine pricer = new RecipePricingEngineWithMargin(0.3m);
            return pricer.ComputePrice(SelectedRecipe);
        }


        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion PropertyChanged

    }
}
