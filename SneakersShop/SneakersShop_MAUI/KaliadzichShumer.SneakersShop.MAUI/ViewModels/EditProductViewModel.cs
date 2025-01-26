using KaliadzichShumer.SneakersShop.MAUI.Models;
using KaliadzichShumer.SneakersShop.MAUI.Services;
using System.Collections.ObjectModel;

namespace KaliadzichShumer.SneakersShop.MAUI.ViewModels
{
    public class EditProductViewModel : EditViewModelBase
    {
        private readonly ProductsViewModel _productsViewModel;
        private readonly ProducerService _producerService;
        private readonly IDialogService _dialogService;
        private readonly INavigationService _navigationService;
        private readonly int? _productId;

        public ObservableCollection<Producer> Producers { get; } = new();

        private Producer _selectedProducer;
        public Producer SelectedProducer
        {
            get => _selectedProducer;
            set {
                if (_selectedProducer != value) {
                    _selectedProducer = value;
                    OnPropertyChanged();
                }
            }
        }

        public EditProductViewModel(
            ProductsViewModel productsViewModel,
            ProducerService producerService,
            INavigationService navigationService,
            IDialogService dialogService,
            int? productId = null) : base(navigationService) {
            _productsViewModel = productsViewModel;
            _producerService = producerService;
            _dialogService = dialogService;
            _navigationService = navigationService;
            _productId = productId;
        }

        public async Task LoadData() {
            if (IsBusy) return;

            try  {
                IsBusy = true;

                var producers = await _producerService.GetProducersAsync();
                await MainThread.InvokeOnMainThreadAsync(() =>
                {
                    Producers.Clear();
                    foreach (var producer in producers)  {
                        Producers.Add(producer);
                    }
                });

                if (_productId.HasValue) {
                    var product = _productsViewModel.Products.FirstOrDefault(p => p.Id == _productId.Value);
                    if (product != null)
                    {
                        Name = product.Name;
                        SelectedProducer = Producers.FirstOrDefault(p => p.Id == product.ProducerId);
                    }
                }
            } finally {
                IsBusy = false;
            }
        }

        protected override async Task OnSave() {
            if (IsBusy) return;

            try {
                IsBusy = true;

                if (string.IsNullOrEmpty(Name)) {
                    await _dialogService.DisplayAlert("Error", "Please enter product name", "OK");
                    return;
                }

                if (SelectedProducer == null) {
                    await _dialogService.DisplayAlert("Error", "Please select a producer", "OK");
                    return;
                }

                if (_productId.HasValue){
                        await _productsViewModel.UpdateProduct(_productId.Value, Name, SelectedProducer.Id);
                } else {await _productsViewModel.AddProduct(Name, SelectedProducer.Id);
                }

                await _navigationService.PopModalAsync();
            }
            catch (Exception ex)  {
                await _dialogService.DisplayAlert("Error", "Failed to save product. Please try again.", "OK");
            }  finally {
                IsBusy = false;
            }
        }
    }
} 