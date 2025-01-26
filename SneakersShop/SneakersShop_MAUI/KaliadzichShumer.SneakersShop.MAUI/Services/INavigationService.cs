namespace KaliadzichShumer.SneakersShop.MAUI.Services
{
    public interface INavigationService
    {
        Task PushModalAsync(Page page);
        Task PopModalAsync();
    }
} 