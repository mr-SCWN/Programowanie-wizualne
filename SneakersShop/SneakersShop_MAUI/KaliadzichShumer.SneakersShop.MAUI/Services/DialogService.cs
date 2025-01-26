namespace KaliadzichShumer.SneakersShop.MAUI.Services
{
    public class DialogService : IDialogService
    {
        public async Task<bool> DisplayConfirmation(string title, string message, string accept = "Yes", string cancel = "No")
        {
            return await Application.Current.MainPage.DisplayAlert(title, message, accept, cancel);
        }

        public async Task DisplayAlert(string title, string message, string accept = "OK")
        {
            await Application.Current.MainPage.DisplayAlert(title, message, accept);
        }
    }
} 