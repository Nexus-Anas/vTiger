using APIntegro.Application.IHandlers;
using APIntegro.Application.Interfaces;
using APIntegro.Application.Services.Authentication;
using APIntegro.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace APIntegro.Application.Services.WorkFlows;

public class ProjectWorkFlow : BackgroundService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly TimeSpan _checkInterval = TimeSpan.FromDays(30);
    private readonly LoginSession _session;
    private readonly ISMSService _smsService;
    private readonly IMailingHandler _emailHandler;
    private static IEnumerable<Project>? _projects;

    public ProjectWorkFlow(IServiceScopeFactory serviceScopeFactory, LoginSession session, ISMSService smsService, IMailingHandler emailHandler)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _session = session;
        _smsService = smsService;
        _emailHandler = emailHandler;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await FetchProjects();
            await CheckForProjectDeadline(_projects);
            await CheckForForgottenProjects(_projects);

            await Task.Delay(_checkInterval, stoppingToken);
        }
    }


    private async Task FetchProjects()
    {
        if (_session.User is null) return;

        using var scope = _serviceScopeFactory.CreateScope();
        var projectService = scope.ServiceProvider.GetRequiredService<IProjectService>();
        _projects = await projectService.FindAllProjects();
    }

    private async Task CheckForProjectDeadline(IEnumerable<Project>? projects)
    {
        if (_session.User is null || _projects is null) return;

        foreach (var project in projects.Where(p => Convert.ToDateTime(p.actualenddate) > DateTime.Now && Convert.ToDateTime(p.actualenddate).AddDays(-15) <= DateTime.Now))
        {
            await _smsService.SendAsync(
                message: $"Reminder:\nThe project {project.projectname} will end soon!",
                to: "+212639757824"
            );
            var newEmail = new Email(
                    SendTo: "mohcinelaaroubi@gmail.com",
                    Subject: $"{project.projectname} deadline",
                    Body: $"Hi,\n{project.projectname} will end soon!"
                );
            await _emailHandler.SendEmail(newEmail);
        }
    }

    private async Task CheckForForgottenProjects(IEnumerable<Project>? projects)
    {
        if (_session.User is null || _projects is null) return;

        foreach (var project in projects.Where(p => DateTime.Now.AddDays(-30) >= Convert.ToDateTime(p.modifiedtime)))
        {
            await _smsService.SendAsync(
                message: $"Reminder:\nIt's been a month since {project.projectname} has been updated.",
                to: "+212639757824"
            );

            var newEmail = new Email(
                    SendTo: "mohcinelaaroubi@gmail.com",
                    Subject: $"{project.projectname} status",
                    Body: $"Hi,\n Reminder:\nIt's been a month since {project.projectname} has been updated."
                );
            await _emailHandler.SendEmail(newEmail);
        }
    }
}