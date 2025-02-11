using APIntegro.Application.IHandlers;
using APIntegro.Application.Interfaces;
using APIntegro.Application.Services.Authentication;
using Microsoft.Extensions.Logging;
using Quartz;
using Microsoft.Extensions.Configuration;
using APIntegro.Domain.Entities;

namespace APIntegro.Application.Services.WorkFlows;

public class TaskCompletionNotifierService 
{
    private readonly IAuthenticationService _authService;
    private readonly IProjectTaskService _projectTaskService;
    private readonly IMailingHandler _mailingHandler;
    private readonly ILogger<TaskCompletionNotifierService> _logger;

    public TaskCompletionNotifierService(IProjectTaskService projectTaskService, IAuthenticationService authService, ILogger<TaskCompletionNotifierService> logger, IMailingHandler mailingHandler, IConfiguration configuration)
    {
        _projectTaskService = projectTaskService;
        _authService = authService;
        _logger = logger;
        _mailingHandler = mailingHandler;
    }

    //public async Task Execute(IJobExecutionContext context)
    //{
    //    try
    //    {
    //        var loginResult = await _authService.Login(new LoginRequest("admin", "D7V3BBzwKm54scgb"));

    //        // Retrieve all tasks
    //        var allTasks = await _projectTaskService.FindAllProjectTasks(loginResult.Result.sessionName);

    //        // Filter for completed tasks
    //        var completedTasks = allTasks.Where(task => task.projecttaskstatus == "Completed");

    //        // Send notifications
    //        foreach (var task in completedTasks)
    //        {
    //            // Send email notification
    //            _logger.LogInformation($"Task name: {task.projecttaskname}");  
                
    //            var newEmail = new Email(
    //                    SendTo : "mohcinelaaroubi@gmail.com",
    //                    Subject : " test email",
    //                    Body : "its the first mail using mailkit"
    //                );

    //            await _mailingHandler.SendEmail(newEmail);
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.LogError(ex, "An error occurred while processing completed tasks."); 
    //        Console.WriteLine(ex.ToString());
    //    }
    //}
}
