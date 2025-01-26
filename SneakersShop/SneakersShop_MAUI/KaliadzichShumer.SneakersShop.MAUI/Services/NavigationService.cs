namespace KaliadzichShumer.SneakersShop.MAUI.Services
{
    public class NavigationService : INavigationService
    {
        public async Task PushModalAsync(Page page)
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(page);
        }

        public async Task PopModalAsync()
        {
            await Application.Current.MainPage.Navigation.PopModalAsync();
        }
    }
} 