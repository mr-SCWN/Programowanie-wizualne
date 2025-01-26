
using KaliadzichShumer.SneakersShop.MAUI.Models;
using KaliadzichShumer.SneakersShop.MAUI.Services;
using KaliadzichShumer.SneakersShop.MAUI.Views;
using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Input;

namespace KaliadzichShumer.SneakersShop.MAUI.ViewModels
{
    public class ProductsViewModel : BindableObject
    {
        private readonly ProductService _productService;
        private readonly ProducerService _producerService;
        private readonly INavigationService _navigationService;
        private readonly IDialogService _dialogService;
        private readonly Func<ProductsViewModel, ProducerService, INavigationService, IDialogService, int?, EditProductViewModel> _editViewModelFactory;
        public ObservableCollection<Product> Products { get; } = new();

        public ICommand AddProductCommand { get; }
        public ICommand EditProductCommand { get; }
        public ICommand DeleteProductCommand { get; }

        private bool _isBusy;
        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                if (_isBusy != value)
                {
                    _isBusy = value;
                    OnPropertyChanged();
                }
            }
        }

        public ProductsViewModel(
            ProductService productService,
            ProducerService producerService,
            INavigationService navigationService,
            IDialogService dialogService,
            Func<ProductsViewModel, ProducerService, INavigationService, IDialogService, int?, EditProductViewModel> editViewModelFactory)
        {
            _productService = productService;
            _producerService = producerService;
            _navigationService = navigationService;
            _dialogService = dialogService;
            _editViewModelFactory = editViewModelFactory;

            AddProductCommand = new Command(async () => await OnAddProduct());
            EditProductCommand = new Command<int>(async (id) => await OnEditProduct(id));
            DeleteProductCommand = new Command<int>(async (id) => await OnDeleteProduct(id));
        }

        private async Task OnAddProduct()
        {
            var viewModel = _editViewModelFactory(this, _producerService, _navigationService, _dialogService, null);
            await _navigationService.PushModalAsync(new EditProductPage(viewModel));
        }

        private async Task OnEditProduct(int id)
        {
            var viewModel = _editViewModelFactory(this, _producerService, _navigationService, _dialogService, id);
            await _navigationService.PushModalAsync(new EditProductPage(viewModel));
        }

        private async Task OnDeleteProduct(int id)
        {
            try
            {
                bool confirm = await _dialogService.DisplayConfirmation("Confirm", "Delete this product?");
                if (confirm)
                {
                    await DeleteProduct(id);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception in OnDeleteProduct: {ex}");
                await _dialogService.DisplayAlert("Error", "Failed to delete product. Please try again.");
            }
        }

        public async Task LoadData()
        {
            if (IsBusy) return;

            try
            {
                IsBusy = true;
                var products = await _productService.GetProductsAsync();
                
                await MainThread.InvokeOnMainThreadAsync(() =>
                {
                    Products.Clear();
                    foreach (var p in products)
                    {
                        Products.Add(p);
                    }
                    OnPropertyChanged(nameof(Products));
                });
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error in LoadData: {ex}");
                await _dialogService.DisplayAlert("Error", "Failed to load products. Please try again.");
            }
            finally
            {
                IsBusy = false;
            }
        }

        public async Task AddProduct(string name, int producerId)
        {
            if (IsBusy) return;

            try
            {
                IsBusy = true;
                var product = new Models.Product { Name = name, ProducerId = producerId };
                if (await _productService.AddProductAsync(product))
                {
                    await LoadData();
                }
                else
                {
                    await _dialogService.DisplayAlert("Error", "Failed to add product. Please try again.");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error in AddProduct: {ex}");
                await _dialogService.DisplayAlert("Error", "An error occurred while adding the product.");
            }
            finally
            {
                IsBusy = false;
            }
        }

        public async Task UpdateProduct(int id, string name, int producerId)
        {
            if (IsBusy) return;

            try
            {
                IsBusy = true;
                var product = Products.FirstOrDefault(x => x.Id == id);
                if (product != null)
                {
                    product.Name = name;
                    product.ProducerId = producerId;
                    if (await _productService.UpdateProductAsync(product))
                    {
                        await LoadData();
                    }
                    else
                    {
                        await _dialogService.DisplayAlert("Error", "Failed to update product. Please try again.");
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error in UpdateProduct: {ex}");
                await _dialogService.DisplayAlert("Error", "An error occurred while updating the product.");
            }
            finally
            {
                IsBusy = false;
            }
        }

        public async Task DeleteProduct(int id)
        {
            if (IsBusy) return;

            try
            {
                IsBusy = true;
                var success = await _productService.DeleteProductAsync(id);
                
                await LoadData();
                
                if (!success)
                {
                    await _dialogService.DisplayAlert("Error", "Failed to delete product. Please try again.");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error in DeleteProduct: {ex}");
                await _dialogService.DisplayAlert("Error", "An error occurred while deleting the product.");
                await LoadData();
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}

