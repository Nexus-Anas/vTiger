using APIntegro.Application.IHandlers;
using APIntegro.Application.Interfaces;
using APIntegro.Application.Services.Authentication;
using APIntegro.Domain.Entities;
using Microsoft.Extensions.Options;
using TrelloDotNet;
using TrelloDotNet.Model;

namespace APIntegro.Application.Services.Trello;

public class TrelloService : ITrelloService
{
    private readonly IProjectHandler _projectHandler;
    private readonly IProjectTaskHandler _projectTaskHandler;
    private readonly LoginSession _session;
    private readonly TrelloSettings _trelloSettings;
    private readonly TrelloClient _trelloClient;

    public TrelloService
    (
        IProjectHandler projectHandler,
        IProjectTaskHandler projectTaskHandler,
        LoginSession loginSession,
        IOptions<TrelloSettings> trelloSettings
    )
    {
        _projectHandler = projectHandler;
        _projectTaskHandler = projectTaskHandler;
        _session = loginSession;
        _trelloSettings = trelloSettings.Value;

        var clientOptions = new TrelloClientOptions
        {
            AllowDeleteOfBoards = true
        };

        _trelloClient = new TrelloClient(_trelloSettings.AccessKey, _trelloSettings.Token, clientOptions);
    }




    public async Task<List<Board>> GetBoards()
    {
        var boards = new List<Board>();
        boards.AddRange(await _trelloClient.GetBoardsForMemberAsync(_trelloSettings.Member1));
        boards.AddRange(await _trelloClient.GetBoardsForMemberAsync(_trelloSettings.Member2));
        return boards;
    }


    public async Task<Board?> GetBoard(Project project)
    {
        var boards = await GetBoards();
        return boards.FirstOrDefault(b => b.Name.Equals(project.projectname));
    }


    public async Task CreateBoard(string projectName)
    {
        var createdBoard = await _trelloClient.AddBoardAsync(new Board(projectName));
        await CreateBoardLists(createdBoard);
    }


    private async Task CreateBoardLists(Board createdBoard)
    {
        var lists = await _trelloClient.GetListsOnBoardAsync(createdBoard.Id);

        lists.ForEach(list => _trelloClient.ArchiveListAsync(list.Id));

        foreach (var listName in new[] { "Open", "In Progress", "Completed", "Deferred", "Canceled" })
            await _trelloClient.AddListAsync(new List(listName, createdBoard.Id));
    }


    public async Task UpdateBoardName(string oldProjectName, string newProjectName)
    {
        var boards = await GetBoards();
        var board = boards.FirstOrDefault(b => b.Name.Equals(oldProjectName));

        if (board is not null)
        {
            board.Name = newProjectName;
            await _trelloClient.UpdateBoardAsync(board);
        }
    }


    public async Task DeleteBoard(string projectId)
    {
        var project = await _projectHandler.GetProject(_session.User.sessionName ,projectId);
        if (project is null) return;

        var board = await GetBoard(project);
        if (board is null) return;

        await _trelloClient.DeleteBoardAsync(board.Id);
    }


    public async Task CreateCard(ProjectTask projectTask, List trelloList)
    {
        if (projectTask is null || trelloList is null) return;

        var card = new Card(trelloList.Id, projectTask.projecttaskname, projectTask.description);
        await _trelloClient.AddCardAsync(card);
    }

    public async Task UpdateCard(ProjectTask existingTask, ProjectTask updatedTask)
    {
        if (existingTask is null || updatedTask is null) return;

        var card = await GetCard(existingTask);
        if (card is null) return;

        var updatedList = await GetBoardList(updatedTask.projectid, updatedTask.projecttaskstatus);
        if (updatedList is null) return;

        card.ListId = updatedList.Id;
        card.Name = updatedTask.projecttaskname;
        card.Description = updatedTask.description;
        await _trelloClient.UpdateCardAsync(card);
    }

    public async Task DeleteCard(string projectTaskId)
    {
        var projectTask = await _projectTaskHandler.GetProjectTask(_session.User.sessionName, projectTaskId);
        if (projectTask is null) return;

        var card = await GetCard(projectTask);
        if (card is null) return;

        await _trelloClient.DeleteCardAsync(card.Id);
    }

    public async Task<Card?> GetCard(ProjectTask projectTask)
    {
        var parentList = await GetBoardList(projectTask.projectid, projectTask.projecttaskstatus);
        if (parentList is null) return null;

        return (await _trelloClient.GetCardsInListAsync(parentList.Id))
               .FirstOrDefault(c => c.Name.Equals(projectTask.projecttaskname));
    }

    private async Task<List?> GetBoardList(string projectId, string listName)
    {
        var project = await _projectHandler.GetProject(_session.User.sessionName ,projectId);
        if (project is null) return null;

        var parentBoard = (await GetBoards()).FirstOrDefault(b => b.Name.Equals(project.projectname));
        if (parentBoard is null) return null;

        return (await _trelloClient.GetListsOnBoardAsync(parentBoard.Id))
               .FirstOrDefault(l => l.Name.Equals(listName));
    }

    public async Task<List<List>> GetBoardLists(Board board)
        => await _trelloClient.GetListsOnBoardAsync(board.Id);

}