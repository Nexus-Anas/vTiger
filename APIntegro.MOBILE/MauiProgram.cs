using Microsoft.Extensions.Logging;
using APIntegro.MOBILE.Data;
using APIntegro.MOBILE.Interfaces;
using APIntegro.MOBILE.Mapping;
using APIntegro.MOBILE.Services;
using MudBlazor.Services;

namespace APIntegro.MOBILE;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            });

        ConfigureServices(builder);

#if DEBUG
		builder.Services.AddBlazorWebViewDeveloperTools();
		builder.Logging.AddDebug();
#endif

        return builder.Build();
    }


    static void ConfigureServices(MauiAppBuilder builder)
    {
        builder.Services.AddMauiBlazorWebView();
        builder.Services.AddMudServices(config =>
        {
            config.SnackbarConfiguration.VisibleStateDuration = 2000;
            config.SnackbarConfiguration.ShowTransitionDuration = 400;
            config.SnackbarConfiguration.HideTransitionDuration = 300;
            config.SnackbarConfiguration.PreventDuplicates = false;
            config.SnackbarConfiguration.NewestOnTop = true;
        });


        //AutoMapper
        builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

        //Dependency Injection
        builder.Services.AddSingleton<LoginSession>();
        builder.Services.AddSingleton<IAuthService, AuthService>();
        builder.Services.AddSingleton<IProjectService, ProjectService>();
        builder.Services.AddSingleton<IProjectTaskService, ProjectTaskService>();
        builder.Services.AddSingleton<IContactService, ContactService>();
        builder.Services.AddSingleton<IOrganizationService, OrganizationService>();
        builder.Services.AddScoped<HttpClient>();
    }
}
