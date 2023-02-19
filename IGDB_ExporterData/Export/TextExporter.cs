namespace IGDB_ExporterData.Export
{
    public class TextExporter : IExporter
    {
        public void WriteDataToFile<T>(IList<T> dataList, string fileName, bool prettyPrint = false) where T : IDataExport
        {
            using (StreamWriter file = File.CreateText(fileName))
            {
                file.WriteLine(string.Join(",", dataList.First().GetPropertyHeaders()));
                foreach (var item in dataList)
                {
                    if(prettyPrint)
                        file.WriteLine(string.Join(",", item.ExportToPrettyPrintStringList()));
                    else
                        file.WriteLine(string.Join(",", item.ExportToStringList()));
                }
            }
        }
    }
}
