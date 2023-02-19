namespace IGDB_ExporterData.Export
{
    public class CsvExporter : IExporter
    {
        public void WriteDataToFile<T>(IList<T> dataList, string fileName, bool prettyPrint = false) where T : IDataExport
        {
            using (StreamWriter file = File.CreateText(fileName))
            {
                file.WriteLine(string.Join(",", dataList.First().GetPropertyHeaders()));
                foreach (var item in dataList)
                {
                    IList<string> stringList;
                    
                    if (prettyPrint)
                        stringList = item.ExportToPrettyPrintStringList();
                    else
                        stringList = item.ExportToStringList();

                    var lineString = "";

                    for (var i = 0; i < stringList.Count; i++)
                    {
                        lineString += stringList[i];

                        if (i != stringList.Count - 1)
                            lineString += ",";
                    }

                    file.WriteLine(lineString);
                }
            }
        }
    }
}
