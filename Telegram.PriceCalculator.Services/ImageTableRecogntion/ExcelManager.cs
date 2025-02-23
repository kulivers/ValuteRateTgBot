using System.Data;
using OfficeOpenXml;

namespace DocumentsManagement;

public class ExcelManager
{
    public static string TempFolder = Path.Combine(Path.GetTempPath(), "tgBot");

    public ExcelManager()
    {
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
    }

    public string CreateStreamFromTable(Dictionary<int, List<string>> table)
    {
        using var package = new ExcelPackage();
        package.Workbook.Worksheets.Add("Sheet1");
        var worksheet = package.Workbook.Worksheets["Sheet1"];
        var cells = worksheet.Cells;
        var columnWidths = new Dictionary<int, int>();
        foreach (var (rowI, values) in table)
        {
            var rowExcelI = rowI + 1;
            for (var colI = 0; colI < values.Count; colI++)
            {
                var colExcelI = colI + 1;
                var value = values[colI];
                var curCell = cells[rowExcelI, colExcelI];
                curCell.Value = value;
                if (value.Contains(Environment.NewLine) || value.Contains('\n'))
                {
                    curCell.Style.WrapText = true;
                }

                if (columnWidths.TryGetValue(colExcelI, out var maxWidth))
                {
                    if (maxWidth < value.Length)
                    {
                        columnWidths[colExcelI] = value.Length + 5;
                    }
                }
                else
                {
                    columnWidths[colExcelI] = value.Length + 5;
                }
            }
        }

        foreach (var (idx, width) in columnWidths)
        {
            worksheet.Columns[idx].Width = width;
        }

        //worksheet.Columns.AutoFit();
        // worksheet.Columns[4].Width = 43;
        var data = package.GetAsByteArray();
        var randomFileName = Path.GetRandomFileName();
        string path = Path.Combine(TempFolder, randomFileName);
        File.WriteAllBytes(path, data);
        return path;
    }
}
