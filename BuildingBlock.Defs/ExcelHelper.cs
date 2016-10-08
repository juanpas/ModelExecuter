using System.Data;
using Excel = Aspose.Cells;

namespace ModelExecuter.Defs
{
    public static class ExcelHelper
    {

        public static DataTable GetTableFromWorkSheet(string fileName, string worksheetName)
        {
            Excel.Workbook workbook = new Excel.Workbook(fileName);

            Excel.Worksheet worksheet = workbook.Worksheets[worksheetName];

            Excel.Range range = GetUsedRange(worksheet);

            DataTable result = worksheet.Cells.ExportDataTable(0, 0, range.RowCount, range.ColumnCount, true);

            return result;
        }


        private static Excel.Range GetUsedRange(Excel.Worksheet worksheet)
        {
            const int MAX_COLUMNS = 20;
            const int MAX_ROWS = 200;

            int maxUsedRow = 0;
            int maxUsedColumn = 0;

            Excel.Range initialRange = worksheet.Cells.CreateRange(0, 0, MAX_ROWS, MAX_COLUMNS);

            for(int counter=0; counter<MAX_COLUMNS; counter++)
            {
                maxUsedColumn = counter;

                if (worksheet.Cells[0, counter].Value == null || worksheet.Cells[0, counter].Value.ToString().Length == 0)
                    break;
            }

            for(int counter=0; counter<MAX_ROWS; counter++)
            {
                maxUsedRow = counter;

                if (worksheet.Cells[counter, 0].Value == null || worksheet.Cells[counter, 0].Value.ToString().Length == 0)
                    break;
            }

            Excel.Range result = worksheet.Cells.CreateRange(0, 0, maxUsedRow, maxUsedColumn);

            return result;
        }

    }
}
