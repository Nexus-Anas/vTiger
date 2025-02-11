using ClosedXML.Excel;
using MudBlazor;

namespace APIntegro.WEB.Pages.Organizations;

public partial class OrganizationToolBar
{
    private async Task ExportToExcel()
    {
        if (Organizations is null) return;
        var wb = new XLWorkbook();
        var ws = wb.Worksheets.Add("Contacts");

        // Add headers horizontally
        var headers = new[] { "Organization Name", "Phone", "Fax", "Email 1", "Email 2", "Employees", "Annual Revenue" };
        for (int i = 0; i < headers.Length; i++)
        {
            ws.Cell(1, i + 1).Value = headers[i];
        }

        // Add data from Projects vertically
        var organizationsData = Organizations.Select(o => new[] { o.accountname, o.phone, o.fax, o.email1, o.email2, o.employees, o.annual_revenue }).ToList();
        for (int i = 0; i < organizationsData.Count; i++)
        {
            ws.Cell(i + 2, 1).Value = organizationsData[i][0]; // Organization Name
            ws.Cell(i + 2, 2).Value = organizationsData[i][1]; // Phone
            ws.Cell(i + 2, 3).Value = organizationsData[i][2]; // Fax
            ws.Cell(i + 2, 4).Value = organizationsData[i][3]; // Email 1
            ws.Cell(i + 2, 5).Value = organizationsData[i][4]; // Email 2
            ws.Cell(i + 2, 6).Value = organizationsData[i][5]; // Employees
            ws.Cell(i + 2, 7).Value = organizationsData[i][6]; // Annual revenue
        }

        // Generate file name with timestamp
        var fileName = $"Organizations_{DateTime.Now:yyyyMMddHHmmss}.xlsx";

        // Save the Excel file
        using var stream = new MemoryStream();

        wb.SaveAs(stream);
        var content = stream.ToArray();

        await using var fileStream = new FileStream(fileName, FileMode.Create, FileAccess.Write);
        await fileStream.WriteAsync(content, 0, content.Length);

        Snackbar.Add("Exported to Excel successfully!", Severity.Success);
    }
}