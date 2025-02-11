using APIntegro.Application.IHandlers;
using APIntegro.Infrastructure.Handlers;
using Microsoft.Extensions.DependencyInjection;


namespace APIntegro.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        //Dependency Injection
        services.AddScoped<IApiHandler, ApiHandler>();
        services.AddScoped<IAuthHandler, AuthHandler>();
        services.AddScoped<IProjectHandler, ProjectHandler>();
        services.AddScoped<IContactHandler, ContactHandler>();
        services.AddScoped<IProjectTaskHandler, ProjectTaskHandler>();
        services.AddScoped<IOrganizationHandler, OrganizationHandler>();
        services.AddScoped<IProjectMilestoneHandler, ProjectMilestoneHandler>();
        services.AddTransient<IMailingHandler, MailingHandler>();

        return services;
    }
}