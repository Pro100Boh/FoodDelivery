using System;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using System.Diagnostics;
using Xamarin.Essentials;

namespace FoodDeliveryMobileApp.Views
{
    public class MapPage : ContentPage
    {
        public MapPage()
        {
            var map = new Xamarin.Forms.Maps.Map(
                MapSpan.FromCenterAndRadius(
                        new Position(50.4471424, 30.455454), Distance.FromMiles(3)))
            {
                IsShowingUser = true,
                HeightRequest = 100,
                WidthRequest = 960,
                VerticalOptions = LayoutOptions.FillAndExpand
            };
            var stack = new StackLayout { Spacing = 0 };
            stack.Children.Add(map);
            Content = stack;

            // pins
            var pins = new Pin[]
            {
                new Pin
                {
                    Type = PinType.Place,
                    Position = new Position(50.454330, 30.447978),
                    Label = "Shop 1",
                    Address = ""
                },
                new Pin
                {
                    Type = PinType.Place,
                    Position = new Position(50.452182, 30.484380),
                    Label = "Shop 2",
                    Address = ""
                },
                new Pin
                {
                    Type = PinType.Place,
                    Position = new Position(50.428824, 30.440601),
                    Label = "Shop 3",
                    Address = ""
                },
                new Pin
                {
                    Type = PinType.Place,
                    Position = new Position(50.4385211, 30.4245122),
                    Label = "Shop 4",
                    Address = ""
                },
            };

            foreach (var pin in pins)
            {
                map.Pins.Add(pin);
            }
        }
    }

}
