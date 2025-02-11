using APIntegro.Domain.Entities;
using TrelloDotNet.Model;

namespace APIntegro.Application.Interfaces;

public interface ITrelloService
{
    Task<List<Board>> GetBoards();
    Task<Board?> GetBoard(Project project);
    Task CreateBoard(string projectName);
    Task UpdateBoardName(string oldProjectName, string newProjectName);
    Task DeleteBoard(string projectId);
    Task CreateCard(ProjectTask projectTask, List trelloList);
    Task UpdateCard(ProjectTask existingTask, ProjectTask updatedTask);
    Task DeleteCard(string projectTaskId);
    Task<Card?> GetCard(ProjectTask projectTask);
    Task<List<List>> GetBoardLists(Board board);
}