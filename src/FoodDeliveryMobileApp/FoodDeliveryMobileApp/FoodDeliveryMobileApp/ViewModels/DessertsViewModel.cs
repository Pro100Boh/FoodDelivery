using FoodDeliveryMobileApp.Models;
using FoodDeliveryMobileApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace FoodDeliveryMobileApp.ViewModels
{
    public class DessertsViewModel
    {
        private readonly IDessertsService _dessertsService;

        public ObservableCollection<Dessert> DessertsCollection { get; set; }

        public DessertsViewModel(IDessertsService dessertsService)
        {
            DessertsCollection = new ObservableCollection<Dessert>();
            _dessertsService = dessertsService;
        }

        public async Task LoadDessertsAsync()
        {
            var desserts = await _dessertsService.GetDessertsAsync();

            foreach (var dessert in desserts)
            {
                dessert.DessertImageUri = _dessertsService.GetDessertImageUri(dessert.Id);

                DessertsCollection.Add(dessert);
            }

        }
    }
}
