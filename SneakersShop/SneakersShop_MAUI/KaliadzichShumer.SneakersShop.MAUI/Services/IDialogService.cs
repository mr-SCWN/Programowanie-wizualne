namespace KaliadzichShumer.SneakersShop.MAUI.Services {
    public interface IDialogService {
        Task <bool> DisplayConfirmation(string title, string message, string accept = "Yes", string cancel = "No");
        Task DisplayAlert(string title, string message, string accept = "OK");
    }
} 