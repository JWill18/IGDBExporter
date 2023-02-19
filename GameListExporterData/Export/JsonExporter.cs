using Newtonsoft.Json;

namespace IGDB_ExporterData.Export
{
    public class JsonExporter : IExporter
    {
        public void WriteDataToFile<T>(IList<T> dataList, string fileName, bool prettyPrint = false) where T : IDataExport
        {
            var serializedObj = JsonConvert.SerializeObject(dataList, Formatting.Indented);

            File.WriteAllText(fileName, serializedObj.ToString());
        }
    }
}
