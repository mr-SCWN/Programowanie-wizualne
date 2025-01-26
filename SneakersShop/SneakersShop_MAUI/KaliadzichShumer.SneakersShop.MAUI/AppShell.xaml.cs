using KaliadzichShumer.SneakersShop.MAUI.Views;

namespace KaliadzichShumer.SneakersShop.MAUI;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		Routing.RegisterRoute(nameof(ProductsPage), typeof(ProductsPage));
        Routing.RegisterRoute(nameof(ProducersPage), typeof(ProducersPage));
        Routing.RegisterRoute(nameof(EditProductPage), typeof(EditProductPage));
        Routing.RegisterRoute(nameof(EditProducerPage), typeof(EditProducerPage));
    
	}
}
