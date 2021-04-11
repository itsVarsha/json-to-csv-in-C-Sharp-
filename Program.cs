using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Xml;
using MiNET.Plugins;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Formatting = Newtonsoft.Json.Formatting;

namespace jsonToCSVconverter
{
    class Program {
        

        public static void Main()
        {
            var json = File.ReadAllText("campaignData.json");
            
            jsonStringToCSV(json);
        }
        public static void jsonStringToCSV(string jsonContent)
        {
            var dataTable = (DataTable)JsonConvert.DeserializeObject(jsonContent, (typeof(DataTable)));

            //Datatable to CSV
            var lines = new List<string>();
            string[] columnNames = dataTable.Columns.Cast<DataColumn>().
                                              Select(column => column.ColumnName).
                                              ToArray();
            var header = string.Join(",", columnNames);
            lines.Add(header);
            var valueLines = dataTable.AsEnumerable()
                               .Select(row => string.Join(",", row.ItemArray));
            lines.AddRange(valueLines);
            File.WriteAllLines("Export.csv", lines);
        }
    }
}
