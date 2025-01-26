namespace KaliadzichShumer.SneakersShop.MAUI.Views;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private async void OnManageProducersClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//ProducersPage");
    }

    private async void OnManageProductsClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//ProductsPage");
    }
}
