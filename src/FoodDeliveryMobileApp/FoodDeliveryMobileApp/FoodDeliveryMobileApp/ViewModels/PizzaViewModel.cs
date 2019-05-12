using FoodDeliveryMobileApp.Models;
using FoodDeliveryMobileApp.Services;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace FoodDeliveryMobileApp.ViewModels
{
    public class PizzaViewModel
    {
        private readonly IPizzaService _pizzaService;

        public ObservableCollection<Pizza> PizzasCollection { get; set; }

        public PizzaViewModel(IPizzaService pizzaService)
        {
            PizzasCollection = new ObservableCollection<Pizza>();
            _pizzaService = pizzaService;
        }

        public async Task LoadPizzasAsync()
        {
            var pizzas = await _pizzaService.GetPizzasAsync();

            foreach (var pizza in pizzas)
            {
                pizza.PizzaImageUri = _pizzaService.GetPizzaImageUri(pizza.Id);

                foreach (var ingradient in pizza.Ingradients)
                {
                    ingradient.IngradientImageUri = _pizzaService.GetIngradientImageUri(ingradient.Id);
                }

                PizzasCollection.Add(pizza);
            }

        }

    }
}
