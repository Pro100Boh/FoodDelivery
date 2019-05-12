using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.Xaml;

namespace FoodDeliveryMobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TabbedProductsPage : Xamarin.Forms.TabbedPage
    {
        public TabbedProductsPage()
        {
            On<Android>().SetBarSelectedItemColor(Color.FromHex("#D41A1D")); 
            On<Android>().SetBarItemColor(Color.Gray); 
             InitializeComponent();
        }
    }
}