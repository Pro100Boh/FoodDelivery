using FoodDeliveryMobileApp.Models;
using FoodDeliveryMobileApp.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace FoodDeliveryMobileApp.ViewModels
{
    public class PizzaViewModel : INotifyPropertyChanged
    {
        private bool _isRefreshing = false;
        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set
            {
                _isRefreshing = value;
                OnPropertyChanged(nameof(IsRefreshing));
            }
        }

        public ICommand RefreshCommand
        {
            get
            {
                return new Command(async () =>
                {
                    IsRefreshing = true;

                    PizzasCollection.Clear();

                    await LoadPizzasAsync();

                    IsRefreshing = false;
                });
            }
        }

        private static PizzaViewModel source = null;

        public static PizzaViewModel Instance
        {
            get
            {
                if (source == null)
                    source = new PizzaViewModel(new PizzaService());

                return source;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private readonly IPizzaService _pizzaService;

        public ObservableCollection<Pizza> PizzasCollection { get; set; }

        private PizzaViewModel(IPizzaService pizzaService)
        {
            PizzasCollection = new ObservableCollection<Pizza>();
            _pizzaService = pizzaService;
        }

        public async Task LoadPizzasAsync()
        {
            PizzasCollection.Clear();

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

        private void OnPropertyChanged(string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
