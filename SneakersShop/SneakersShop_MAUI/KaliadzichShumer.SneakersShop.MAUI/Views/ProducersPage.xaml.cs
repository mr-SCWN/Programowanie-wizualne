using KaliadzichShumer.SneakersShop.MAUI.ViewModels;

namespace KaliadzichShumer.SneakersShop.MAUI.Views;

public partial class ProducersPage : ContentPage
{
    private readonly ProducersViewModel _viewModel;

    public ProducersPage(ProducersViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.LoadData();
    }
}
