using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using KaliadzichShumer.SneakersShop.BLC;
using KaliadzichShumer.SneakersShop.INTERFACES;

namespace KaliadzichShumer.SneakersShop.UI.WPF.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private IBL _bl;

        private ObservableCollection<object> _manufacturers;
        public ObservableCollection<object> Manufacturers
        {
            get => _manufacturers;
            set { _manufacturers = value; OnPropertyChanged(); }
        }

        private ObservableCollection<object> _sneakers;
        public ObservableCollection<object> Sneakers
        {
            get => _sneakers;
            set { _sneakers = value; OnPropertyChanged(); }
        }

        public ICommand RefreshCommand { get; }

        public MainViewModel()
        {
            // IF we use MOCK:
            string daoAssembly = "KaliadzichShumer.SneakersShop.DAO.Mock";
            string daoType = "KaliadzichShumer.SneakersShop.DAO.Mock.MockDAO";

            // or File/SQL 
            _bl = new BusinessLogicController(daoAssembly, daoType);

            RefreshCommand = new RelayCommand(_ => RefreshData());

            // Load data
            RefreshData();
        }

        private void RefreshData()
        {
            Manufacturers = new ObservableCollection<object>(_bl.GetAllManufacturers());
            Sneakers = new ObservableCollection<object>(_bl.GetAllSneakers());
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }

    // Realisation commands
    public class RelayCommand : ICommand
    {
        private readonly System.Action<object> _execute;
        private readonly System.Func<object, bool> _canExecute;

        public RelayCommand(System.Action<object> execute, System.Func<object, bool> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter) => _canExecute == null || _canExecute(parameter);
        public void Execute(object parameter) => _execute(parameter);
        public event System.EventHandler CanExecuteChanged
        {
            add { System.CommandManager.RequerySuggested += value; }
            remove { System.CommandManager.RequerySuggested -= value; }
        }
    }
}
