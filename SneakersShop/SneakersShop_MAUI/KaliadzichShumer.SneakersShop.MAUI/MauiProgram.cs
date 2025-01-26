using Microsoft.Extensions.Logging;
using KaliadzichShumer.SneakersShop.MAUI.Services;
using KaliadzichShumer.SneakersShop.MAUI.ViewModels;
using KaliadzichShumer.SneakersShop.MAUI.Views;
using System.Net.Http;
using System.Net.Http.Headers;

namespace KaliadzichShumer.SneakersShop.MAUI;

public static class MauiProgram
{
    private const string ApiBaseUrl = "http://localhost:5000";

    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        builder.Services.AddSingleton<HttpClient>(sp =>
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(ApiBaseUrl);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return client;
        });

        builder.Services.AddSingleton<INavigationService, NavigationService>();
        builder.Services.AddSingleton<IDialogService, DialogService>();

        builder.Services.AddSingleton<ProductService>();
        builder.Services.AddSingleton<ProducerService>();

        builder.Services.AddTransient<Func<ProducersViewModel, INavigationService, IDialogService, int?, EditProducerViewModel>>(sp =>
            (producersViewModel, navigationService, dialogService, id) =>
                new EditProducerViewModel(producersViewModel, navigationService, dialogService, id));

        builder.Services.AddTransient<Func<ProductsViewModel, ProducerService, INavigationService, IDialogService, int?, EditProductViewModel>>(sp =>
            (productsViewModel, producerService, navigationService, dialogService, id) =>
                new EditProductViewModel(productsViewModel, producerService, navigationService, dialogService, id));

        builder.Services.AddSingleton<ProductsViewModel>();
        builder.Services.AddSingleton<ProducersViewModel>();

        builder.Services.AddTransient<ProductsPage>();
        builder.Services.AddTransient<ProducersPage>();
        builder.Services.AddTransient<EditProductPage>();
        builder.Services.AddTransient<EditProducerPage>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
