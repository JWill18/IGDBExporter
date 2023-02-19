namespace IGDB_ExporterData.Export
{
    public interface IExporter
    {
        public void WriteDataToFile<T>(IList<T> dataList, string fileName, bool prettyPrint = false) where T : IDataExport;
    }
}
