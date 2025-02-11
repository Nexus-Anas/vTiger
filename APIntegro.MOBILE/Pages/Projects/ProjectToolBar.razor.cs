using ClosedXML.Excel;
using MudBlazor;

namespace APIntegro.MOBILE.Pages.Projects;

public partial class ProjectToolBar
{
    private async Task ExportToExcel()
    {
        if (Projects is null) return;
        var wb = new XLWorkbook();
        var ws = wb.Worksheets.Add("Projects");

        // Add headers horizontally
        var headers = new[] { "Project Name", "Start Date", "Target End Date", "Actual End Date", "Status", "Target Budget (DH)", "Priority", "Progress" };
        for (int i = 0; i < headers.Length; i++)
        {
            ws.Cell(1, i + 1).Value = headers[i];
        }

        // Add data from Projects vertically
        var projectsData = Projects.Select(p => new[] { p.projectname, p.startdate, p.targetenddate, p.actualenddate, p.projectstatus, p.targetbudget, p.projectpriority, p.progress }).ToList();
        for (int i = 0; i < projectsData.Count; i++)
        {
            ws.Cell(i + 2, 1).Value = projectsData[i][0]; // Project Name
            ws.Cell(i + 2, 2).Value = projectsData[i][1]; // Start Date
            ws.Cell(i + 2, 3).Value = projectsData[i][2]; // Target End Date
            ws.Cell(i + 2, 4).Value = projectsData[i][3]; // Actual End Date
            ws.Cell(i + 2, 5).Value = projectsData[i][4]; // Status
            ws.Cell(i + 2, 6).Value = projectsData[i][5]; // Target Budget (DH)
            ws.Cell(i + 2, 7).Value = projectsData[i][6]; // Priority
            ws.Cell(i + 2, 8).Value = projectsData[i][7]; // Progress
        }

        // Generate file name with timestamp
        var fileName = $"Projects_{DateTime.Now:yyyyMMddHHmmss}.xlsx";

        // Save the Excel file
        using var stream = new MemoryStream();

        wb.SaveAs(stream);
        var content = stream.ToArray();

        await using var fileStream = new FileStream(fileName, FileMode.Create, FileAccess.Write);
        await fileStream.WriteAsync(content, 0, content.Length);

        Snackbar.Add("Exported to Excel successfully!", Severity.Success);
    }
}