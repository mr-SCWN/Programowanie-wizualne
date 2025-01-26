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
    public class ProducersViewModel : BindableObject {
        private readonly ProducerService _producerService;
        private readonly INavigationService _navigationService;
        private readonly IDialogService _dialogService;
        private readonly Func<ProducersViewModel, INavigationService, IDialogService, int?, EditProducerViewModel> _editViewModelFactory;
        public ObservableCollection<Producer> Producers { get; } = new();

        public ICommand AddProducerCommand { get; }
        public ICommand EditProducerCommand { get; }
        public ICommand DeleteProducerCommand { get; }

        private bool _isBusy;
        public bool IsBusy {
            get => _isBusy;
            set
            {
                if (_isBusy != value) {
                    _isBusy = value;
                    OnPropertyChanged();
                }
            }
        }

        public ProducersViewModel(
            ProducerService producerService,
            INavigationService navigationService,
            IDialogService dialogService,
            Func<ProducersViewModel, INavigationService, IDialogService, int?, EditProducerViewModel> editViewModelFactory)
        {
            _producerService = producerService;
            _navigationService = navigationService;
            _dialogService = dialogService;
            _editViewModelFactory = editViewModelFactory;

            AddProducerCommand = new Command(async () => await OnAddProducer());
            EditProducerCommand = new Command<int>(async (id) => await OnEditProducer(id));
            DeleteProducerCommand = new Command<int>(async (id) => await OnDeleteProducer(id));
        }

        private async Task OnAddProducer(){
            var viewModel = _editViewModelFactory(this, _navigationService, _dialogService, null);
            await _navigationService.PushModalAsync(new EditProducerPage(viewModel));
        }

        private async Task OnEditProducer(int id){
            var viewModel = _editViewModelFactory(this, _navigationService, _dialogService, id);
            await _navigationService.PushModalAsync(new EditProducerPage(viewModel));
        }

        private async Task OnDeleteProducer(int id) {
            try {
                bool confirm = await _dialogService.DisplayConfirmation("Confirm", "Delete this producer?");
                if (confirm) {
                    await DeleteProducer(id);
                }
            }  catch (Exception ex)  {
                Debug.WriteLine($"Exception in OnDeleteProducer: {ex}");
                await _dialogService.DisplayAlert("Error", "Failed to delete producer. Please try again.");
            }
        }

        public async Task LoadData() {
            if (IsBusy) {
                return;
            }

            try  {
                IsBusy = true;
                var producers = await _producerService.GetProducersAsync();
                
                await MainThread.InvokeOnMainThreadAsync(() =>
                {
                    Producers.Clear();
                    foreach (var p in producers)
                    {
                        Producers.Add(p);
                    }
                    OnPropertyChanged(nameof(Producers));
                });
            }
            catch (Exception ex){
                Debug.WriteLine($"Error in LoadData: {ex}");
                await _dialogService.DisplayAlert("Error", "Failed to load producers. Please try again.");
            }  finally{
                IsBusy = false;
            }
        }

        public async Task<bool> AddProducer(string name)  {
            if (IsBusy) {
                return false;
            }

            try {
                IsBusy = true;
                var producer = new Models.Producer { Name = name };
                await _dialogService.DisplayAlert("Debug", $"Attempting to add producer: {name}");
                
                if (await _producerService.AddProducerAsync(producer))
                {
                    await LoadData();
                    return true;
                }
                else
                {
                    await _dialogService.DisplayAlert("Error", "Failed to add producer. Please try again.");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error in AddProducer: {ex}");
                await _dialogService.DisplayAlert(
                    "Error", 
                    $"An error occurred while adding the producer.\nError details:\n{ex.Message}\n\nInner error:\n{ex.InnerException?.Message ?? "None"}");
                return false;
            } finally {
                IsBusy = false;
            }
        }

        public async Task<bool> UpdateProducer(int id, string name) {
            if (IsBusy) return false;

            try
            {
                IsBusy = true;
                var producer = Producers.FirstOrDefault(x => x.Id == id);
                if (producer != null)
                {
                    producer.Name = name;
                    if (await _producerService.UpdateProducerAsync(producer))  {
                        await LoadData();
                        return true;
                    }
                    else
                    {
                        await _dialogService.DisplayAlert("Error", "Failed to update producer. Please try again.");
                        return false;
                    }
                }
                return false;
            } catch (Exception ex)  {
                Debug.WriteLine($"Error in UpdateProducer: {ex}");
                await _dialogService.DisplayAlert("Error", "An error occurred while updating the producer.");
                return false;
            } finally {
                IsBusy = false;
            }
        }

        public async Task<bool> DeleteProducer(int id)
        {
            if (IsBusy){
                return false;
            } 
            try
            {
                IsBusy = true;
                var success = await _producerService.DeleteProducerAsync(id);
                
                await LoadData();
                
                if (!success) {
                    await _dialogService.DisplayAlert("Error", "Failed to delete producer. Please try again.");
                }
                
                return success;
            } catch (Exception ex)  {
                Debug.WriteLine($"Error in DeleteProducer: {ex}");
                await _dialogService.DisplayAlert("Error", "An error occurred while deleting the producer.");
                await LoadData();
                return false;
            } finally  {
                IsBusy = false;
            }
        }
    }
}
