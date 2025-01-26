using KaliadzichShumer.SneakersShop.MAUI.ViewModels;

namespace KaliadzichShumer.SneakersShop.MAUI.Views;

public partial class EditProducerPage : ContentPage
{
    public EditProducerPage(EditProducerViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
