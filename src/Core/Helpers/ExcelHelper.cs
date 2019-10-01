using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Core.Helpers
{
    public static class ExcelHelper
    {
        public static byte[] ExportEntity<T>(IEnumerable<T> data, ICollection<string> exceptFields = null, string worksheetsName = "Relatório") where T : class, new()
        {
            ExcelPackage excel = new ExcelPackage();
            var workSheet = excel.Workbook.Worksheets.Add(worksheetsName);
            workSheet.TabColor = System.Drawing.Color.Black;
            workSheet.DefaultRowHeight = 12;

            workSheet.Row(1).Height = 20;
            workSheet.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Row(1).Style.Font.Bold = true;

            var props = new List<PropertyInfo>();
            foreach (PropertyInfo prop in typeof(T).GetProperties())
            {
                if (exceptFields == null || !exceptFields.Contains(prop.Name))
                {
                    props.Add(prop);
                }
            }

            var model = new T();
            int countProp = 1;
            foreach (var prop in props)
            {
                workSheet.Cells[1, countProp].Value = model.DisplayNameFor(prop);
                workSheet.Column(1).BestFit = true;
                countProp++;
            }

            int recordIndex = 2;
            foreach (var itemRelatorio in data)
            {
                int countProp2 = 1;
                foreach (var prop in props)
                {
                    workSheet.Cells[recordIndex, countProp2].Value = prop.GetValue(itemRelatorio, null);
                    countProp2++;
                }
                recordIndex++;
            }

            byte[] filecontent = excel.GetAsByteArray();

            return filecontent;
        }
    }
}