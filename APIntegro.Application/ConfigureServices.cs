using APIntegro.Application.Interfaces;
using APIntegro.Application.Mapping;
using APIntegro.Application.Services;
using APIntegro.Application.Services.Authentication;
using APIntegro.Application.Services.Contacts;
using APIntegro.Application.Services.Projects;
using APIntegro.Application.Services.Organizations;
using Microsoft.Extensions.DependencyInjection;
using APIntegro.Application.Services.ProjectTasks;
using APIntegro.Application.Services.ProjectMilestones;
using APIntegro.Application.Services.WorkFlows;
using System.Runtime.CompilerServices;
using Quartz;
using APIntegro.Application.Services.Communication;
using APIntegro.Domain.Entities;
using TrelloDotNet.Model;
using APIntegro.Application.Services.Trello;

namespace APIntegro.Application;
public static class ConfigureServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        //AutoMapper
        services.AddAutoMapper(typeof(AutoMapperProfile));

        //Dependency Injection
        services.AddScoped<IProjectService, ProjectService>();
        services.AddScoped<IProjectTaskService, ProjectTaskService>();
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<IContactService, ContactService>();
        services.AddScoped<IOrganizationService, OrganizationService>();
        services.AddScoped<IProjectMilestoneService, ProjectMilestoneService>();
        services.AddScoped<ITrelloService, TrelloService>();
        services.AddTransient<ISMSService, SMSService>();

        //configure Quertz
        //services.AddQuartz(options =>
        //{
        //    var jobKey = JobKey.Create(nameof(TaskCompletionNotifierService));

        //    options.UseMicrosoftDependencyInjectionJobFactory();

        //    options
        //        .AddJob<TaskCompletionNotifierService>(jobKey)
        //        .AddTrigger(trigger =>
        //            trigger
        //                .ForJob(jobKey)
        //                .WithSimpleSchedule(schedule =>
        //                    schedule.WithIntervalInSeconds(20).RepeatForever()));
        //});
        //services.AddQuartzHostedService();

        return services;
    }
}