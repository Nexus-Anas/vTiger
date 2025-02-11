# Background Service: ProjectWorkFlow

## Overview
>The ProjectWorkFlow background service manages project workflows by periodically checking project deadlines and updates.

## Dependencies
>-IServiceScopeFactory: Factory for creating service scopes.
>-LoginSession: Session information for the current user.
>-ISMSService: Service for sending SMS messages.

## Configuration
>-Check Interval: The service checks for project updates and deadlines every 30 days.

## Methods

#### ExecuteAsync(CancellationToken stoppingToken)
>Description: Executes the background service logic.

>Parameters:
>- stoppingToken: CancellationToken to monitor for service stop requests.

>___

>Code:
```csharp
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

```

#### FetchProjects()
>Description:
>Fetches all projects from the database.

>Dependencies: Requires a valid user session.

>Actions:
>- Retrieves projects using the IProjectService.
>- Updates the _projects variable with the fetched projects.

>___

>Code:
```csharp
private async Task FetchProjects()
{
    if (_session.User is null) return;

    using var scope = _serviceScopeFactory.CreateScope();
    var projectService = scope.ServiceProvider.GetRequiredService<IProjectService>();
    _projects = await projectService.FindAllProjects();
}

```

#### CheckForProjectDeadline(IEnumerable<Project>? projects)
>Description:
>Checks for projects nearing their end dates.

>Dependencies: Requires a valid user session and fetched projects.

>Actions:
>- Iterates through projects to find those with end dates approaching.
>- Sends SMS reminders for impending project deadlines.

>___

>Code:
```csharp
private async Task CheckForProjectDeadline(IEnumerable<Project>? projects)
{
    if (_session.User is null || _projects is null) return;

    foreach (var project in projects.Where(p => Convert.ToDateTime(p.actualenddate) > DateTime.Now && Convert.ToDateTime(p.actualenddate).AddDays(-15) <= DateTime.Now))
    {
        await _smsService.SendAsync(
            message: $"Reminder:\nThe project {project.projectname} will end soon!",
            to: "+212 XXX XXX XXX"
        );
    }
}

```

#### CheckForForgottenProjects(IEnumerable<Project>? projects)
>Description:
>Checks for projects not updated within the last 30 days.

>Dependencies: Requires a valid user session and fetched projects.

>Actions:
>- Iterates through projects to find those not updated within the specified duration.
>- Sends SMS reminders for stagnant projects.

>___

>Code:
```csharp
private async Task CheckForForgottenProjects(IEnumerable<Project>? projects)
{
    if (_session.User is null || _projects is null) return;

    foreach (var project in projects.Where(p => DateTime.Now.AddDays(-30) >= Convert.ToDateTime(p.modifiedtime)))
    {
        await _smsService.SendAsync(
            message: $"Reminder:\nIt's been a month since {project.projectname} has been updated.",
            to: "+212 XXX XXX XXX"
        );
    }
}


```

## Usage
>1. Register the ProjectWorkFlow as a background service in your application startup.

>2. Ensure proper configuration of dependencies (IServiceScopeFactory, LoginSession, ISMSService).

>3. Start the background service during application initialization.

## Notes
>The service operates at a predefined interval of 30 days for efficiency and performance optimization.

>Ensure that the required dependencies are injected correctly to enable seamless functionality.

>Adjust the check interval or logic as per specific project requirements.