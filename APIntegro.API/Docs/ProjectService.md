# Service: ProjectService

## Overview
>The ProjectService class provides methods for managing projects, including creation, retrieval, updating, and deletion.

## Dependencies
>-IProjectHandler: Handler interface for project-related operations.
>-ISMSService: Service for sending SMS notifications.
>-LoginSession: Session information for user authentication.

## Constructor
>-Parameters:
>- projectHandler: Instance of IProjectHandler for handling project operations.
>- smsService: Instance of ISMSService for sending notifications.
>- session: Instance of LoginSession for user authentication.

## Methods

#### FindProject(string projectId)
>Description:
>Retrieves a project by its ID.

>Parameters:
>- projectId: ID of the project to retrieve.

>Returns:
>Task< Project > representing the retrieved project.

>___

>Code:
```csharp
public async Task<Project> FindProject(string projectId)
{
    return await _projectHandler.GetProject(_session.User.sessionName, projectId);
}
```

#### FindAllProjects()
>Description:
>Retrieves all projects.

>Returns:
>Task< IEnumerable< Project > > representing a collection of projects.

>___

>Code:
```csharp
public async Task<IEnumerable<Project>> FindAllProjects()
{
    return await _projectHandler.GetAllProjects(_session.User.sessionName);
}
```

#### ProjectsCount()
>Description:
>Retrieves the count of projects.

>Returns:
>Task< int > representing the count of projects.

>___

>Code:
```csharp
public async Task<int> ProjectsCount()
{
    var result = await _projectHandler.GetAllProjects(_session.User.sessionName);
    return result.Count();
}
```

#### ProjectsStatesChart()
>Description:
>Generates a chart of project states.

>Returns:
>Task< Dictionary< string, double > > representing project states and their counts.

>___

>Code:
```csharp
public async Task<Dictionary<string, double>> ProjectsStatesChart()
{
    var projects = await _projectHandler.GetAllProjects(_session.User.sessionName);

    var result = projects
    .GroupBy(p => p.projectstatus)
    .ToDictionary(g => g.Key, g => (double)g.Count());

    return result;
}
```

#### CreateProject(Project project)
>Description:
>Creates a new project.

>Parameters:
>- project: Project object to create.

>Returns:
>Task< Project > representing the created project.

>___

>Code:
```csharp
public async Task<Project> CreateProject(Project project)
{
    var createdProject = await _projectHandler.PostProject(_session.User.sessionName, project);
    return createdProject;
}
```

#### UpdateProject(Project request)
>Description:
>Updates an existing project.

>Parameters:
>- request: Updated project object.

>Returns:
>Task< Project > representing the updated project.

>___

>Code:
```csharp
public async Task<Project> UpdateProject(Project request)
{
    var project = await FindProject(request.id);
    var updatedProject = await _projectHandler.PutProject(_session.User.sessionName, request);

    await AlertForNewChanges(project, updatedProject);

    return updatedProject;
}
```

#### DeleteProject(string projectId)
>Description:
>Deletes a project.

>Parameters:
>- projectId: ID of the project to delete.

>Returns:
>Task< bool > indicating the success of the deletion operation.

>___

>Code:
```csharp
public async Task<bool> DeleteProject(string projectId)
{
    return await _projectHandler.RemoveProject(_session.User.sessionName, projectId);
}
```

#### ParseProgress(string progress)
>Description:
>Parses project progress from string format.

>Parameters:
>- progress: Progress string to parse.

>Returns:
>Parsed byte value representing progress.

>___

>Code:
```csharp
private static byte ParseProgress(string progress)
{
    if (progress is not null && progress.EndsWith("%") && byte.TryParse(progress.TrimEnd('%'), out byte result))
    {
        return result;
    }
    return 0;
}
```

#### AlertForNewChanges(Project project, Project updatedProject)
>Description:
>Sends notifications for changes in project details.

>Parameters:
>- project: Original project object.
>- updatedProject: Updated project object.

>___

>Code:
```csharp
private async Task AlertForNewChanges(Project project, Project updatedProject)
{
    if (project is null || updatedProject is null) return;

    byte oldProgress = ParseProgress(project.progress);
    byte newProgress = ParseProgress(updatedProject.progress);
     
    string? progressMessage = (newProgress > oldProgress)
        ? $"The project progress has increased from \"{oldProgress}%\" to \"{newProgress}%\"."
        : null;

    string? statusMessage = (project.projectstatus != updatedProject.projectstatus)
        ? $"The project status has changed from \"{project.projectstatus}\" to \"{updatedProject.projectstatus}\"."
        : null;

    string? priorityMessage = (project.projectpriority != updatedProject.projectpriority)
        ? $"The project priority has moved from \"{project.projectpriority}\" to \"{updatedProject.projectpriority}\"."
        : null;

    if (progressMessage is null && statusMessage is null && priorityMessage is null) return;

    await _smsService.SendAsync(
            message: $"Project {updatedProject.projectname} update status:\n{progressMessage}\n{statusMessage}\n{priorityMessage}",
            to: "+212 XXX XXX XXX"
    );
}
```

## Usage
>1. Inject IProjectHandler, ISMSService, and LoginSession dependencies into the ProjectService constructor.
>2. Use the provided methods to manage projects, including creation, retrieval, updating, and deletion.
>3. Handle returned tasks asynchronously for efficient processing.

## Notes
>Ensure proper handling of project-related operations and user sessions for security and data integrity.

>Use appropriate error handling and logging mechanisms for robust service functionality.

>Customize notification logic in AlertForNewChanges as per specific project requirements.