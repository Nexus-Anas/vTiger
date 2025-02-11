using ClosedXML.Excel;
using MudBlazor;

namespace APIntegro.WEB.Pages.Contacts;

public partial class ContactToolBar
{
    private async Task ExportToExcel()
    {
        if (Contacts is null) return;
        var wb = new XLWorkbook();
        var ws = wb.Worksheets.Add("Contacts");

        // Add headers horizontally
        var headers = new[] { "First Name", "Last Name", "Phone", "Email", "Birthday", "Department" };
        for (int i = 0; i < headers.Length; i++)
        {
            ws.Cell(1, i + 1).Value = headers[i];
        }

        // Add data from Projects vertically
        var contactsData = Contacts.Select(p => new[] { p.firstname, p.lastname, p.phone, p.email, p.birthday, p.department }).ToList();
        for (int i = 0; i < contactsData.Count; i++)
        {
            ws.Cell(i + 2, 1).Value = contactsData[i][0]; // First Name
            ws.Cell(i + 2, 2).Value = contactsData[i][1]; // Last Name
            ws.Cell(i + 2, 3).Value = contactsData[i][2]; // Phone
            ws.Cell(i + 2, 4).Value = contactsData[i][3]; // Email
            ws.Cell(i + 2, 5).Value = contactsData[i][4]; // Birthday
            ws.Cell(i + 2, 6).Value = contactsData[i][5]; // Department
        }

        // Generate file name with timestamp
        var fileName = $"Contacts_{DateTime.Now:yyyyMMddHHmmss}.xlsx";

        // Save the Excel file
        using var stream = new MemoryStream();

        wb.SaveAs(stream);
        var content = stream.ToArray();

        await using var fileStream = new FileStream(fileName, FileMode.Create, FileAccess.Write);
        await fileStream.WriteAsync(content, 0, content.Length);

        Snackbar.Add("Exported to Excel successfully!", Severity.Success);
    }
}