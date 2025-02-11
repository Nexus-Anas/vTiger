using APIntegro.WEB.Data;
using APIntegro.WEB.Interfaces;
using APIntegro.WEB.Mapping;
using APIntegro.WEB.Services;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);


ConfigureServices(builder);

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();


static void ConfigureServices(WebApplicationBuilder builder)
{
    builder.Services.AddRazorPages();
    builder.Services.AddServerSideBlazor();
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
    builder.Services.AddScoped<IAuthService, AuthService>();
    builder.Services.AddScoped<IProjectService, ProjectService>();
    builder.Services.AddScoped<IProjectTaskService, ProjectTaskService>();
    builder.Services.AddScoped<IContactService, ContactService>();
    builder.Services.AddScoped<IOrganizationService, OrganizationService>();

    builder.Services.AddHttpClient<AuthService>(client =>
    {
        client.BaseAddress = new Uri(builder.Configuration.GetValue<string>("ApiSettings:APIntegroUrl"));
    });
}