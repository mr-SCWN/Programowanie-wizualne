using KaliadzichShumer.SneakersShop.MAUI.ViewModels;

namespace KaliadzichShumer.SneakersShop.MAUI.Views
{
    public partial class EditProductPage : ContentPage
    {
        private readonly EditProductViewModel _viewModel;

        public EditProductPage(EditProductViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            BindingContext = viewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await _viewModel.LoadData();
        }
    }
}
