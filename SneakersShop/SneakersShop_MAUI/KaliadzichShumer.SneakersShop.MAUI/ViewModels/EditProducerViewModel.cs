using KaliadzichShumer.SneakersShop.MAUI.Models;
using KaliadzichShumer.SneakersShop.MAUI.Services;

namespace KaliadzichShumer.SneakersShop.MAUI.ViewModels{
    public class EditProducerViewModel : EditViewModelBase {
        private readonly ProducersViewModel _producersViewModel;
        private readonly IDialogService _dialogService;
        private readonly INavigationService _navigationService;
        private readonly int? _producerId;

        private string _country;
        public string Country
        {
            get => _country;
            set
            {
                if (_country != value)
                {
                    _country = value;
                    OnPropertyChanged();
                }
            }
        }

        public EditProducerViewModel(
            ProducersViewModel producersViewModel,
            INavigationService navigationService,
            IDialogService dialogService,
            int? producerId = null) : base(navigationService)
        {
            _producersViewModel = producersViewModel;
            _dialogService = dialogService;
            _navigationService = navigationService;
            _producerId = producerId;

            if (producerId.HasValue) {
                var producer = _producersViewModel.Producers.FirstOrDefault(p => p.Id == producerId.Value);
                if (producer != null)
                {
                    Name = producer.Name;
                    Country =  producer.Country;
                }
            }
        }

        protected override async Task OnSave() {
            if (IsBusy) return;

            try {
                IsBusy = true;

                if (string.IsNullOrEmpty(Name)) {
                    await _dialogService.DisplayAlert("Error", "Please enter producer name", "OK");
                    return;
                }

                bool success;
                if (_producerId.HasValue){
                    success = await _producersViewModel.UpdateProducer(_producerId.Value, Name, Country);
                }  else {
                        success = await _producersViewModel.AddProducer(Name, Country);
                }
                   

                if (success) {
                    await _navigationService.PopModalAsync();
                }
            }  catch (Exception ex) {
                await _dialogService.DisplayAlert("Error", "Failed to save producer. Please try again.", "OK");
            } finally  {
                IsBusy = false;
            }
        }
    }
} 