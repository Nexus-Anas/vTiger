using ClosedXML.Excel;
using MudBlazor;

namespace APIntegro.MOBILE.Pages.ProjectTasks;

public partial class ProjectTaskToolBar
{
    private async Task ExportToExcel()
    {
        if (ProjectTasks is null) return;
        var wb = new XLWorkbook();
        var ws = wb.Worksheets.Add("Project Tasks");

        // Add headers horizontally
        var headers = new[] { "Task Name", "Task Type", "Priority", "Project ID", "Progress", "Start Date", "End Date", "Status" };
        for (int i = 0; i < headers.Length; i++)
        {
            ws.Cell(1, i + 1).Value = headers[i];
        }

        // Add data from Projects vertically
        var projectsData = ProjectTasks.Select(p => new[] { p.projecttaskname, p.projecttasktype, p.projecttaskpriority, p.projectid, p.projecttaskprogress, p.startdate, p.enddate, p.projecttaskstatus }).ToList();
        for (int i = 0; i < projectsData.Count; i++)
        {
            ws.Cell(i + 2, 1).Value = projectsData[i][0]; // Task Name
            ws.Cell(i + 2, 2).Value = projectsData[i][1]; // Task Type
            ws.Cell(i + 2, 3).Value = projectsData[i][2]; // Priority
            ws.Cell(i + 2, 4).Value = projectsData[i][3]; // Project ID
            ws.Cell(i + 2, 5).Value = projectsData[i][4]; // Progress
            ws.Cell(i + 2, 6).Value = projectsData[i][5]; // Start Date
            ws.Cell(i + 2, 7).Value = projectsData[i][6]; // End Date
            ws.Cell(i + 2, 8).Value = projectsData[i][7]; // Status
        }

        // Generate file name with timestamp
        var fileName = $"Project_Tasks_{DateTime.Now:yyyyMMddHHmmss}.xlsx";

        // Save the Excel file
        using var stream = new MemoryStream();

        wb.SaveAs(stream);
        var content = stream.ToArray();

        await using var fileStream = new FileStream(fileName, FileMode.Create, FileAccess.Write);
        await fileStream.WriteAsync(content, 0, content.Length);

        Snackbar.Add("Exported to Excel successfully!", Severity.Success);
    }
}