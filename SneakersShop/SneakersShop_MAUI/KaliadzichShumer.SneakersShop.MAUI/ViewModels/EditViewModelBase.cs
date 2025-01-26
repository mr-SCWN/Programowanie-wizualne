using System.Windows.Input;
using KaliadzichShumer.SneakersShop.MAUI.Services;

namespace KaliadzichShumer.SneakersShop.MAUI.ViewModels
{
    public abstract class EditViewModelBase : BindableObject
    {
        private readonly INavigationService _navigationService;
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

        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        protected EditViewModelBase(INavigationService navigationService)
        {
            _navigationService = navigationService;
            SaveCommand = new Command(async () => await OnSave(), () => !IsBusy);
            CancelCommand = new Command(async () => await OnCancel());
        }

        protected abstract Task OnSave();

        protected virtual async Task OnCancel()
        {
            await _navigationService.PopModalAsync();
        }
    }
} 