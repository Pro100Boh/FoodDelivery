using FoodDeliveryMobileApp.Models;
using FoodDeliveryMobileApp.Services;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace FoodDeliveryMobileApp.ViewModels
{
    public class DrinksViewModel
    {
        private readonly IDrinksService _drinksService;

        public ObservableCollection<Drink> DrinksCollection { get; set; }

        public DrinksViewModel(IDrinksService drinksService)
        {
            DrinksCollection = new ObservableCollection<Drink>();
            _drinksService = drinksService;
        }

        public async Task LoadDrinksAsync()
        {
            DrinksCollection.Clear();

            var drinks = await _drinksService.GetDrinksAsync();

            foreach (var drink in drinks)
            {
                drink.DrinkImageUri = _drinksService.GetDrinkImageUri(drink.Id);

                DrinksCollection.Add(drink);
            }

        }
    }
}
