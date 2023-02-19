namespace IGDB_ExporterData.Export
{
    public interface IDataExport
    {
        public IList<string> ExportToStringList();

        public IList<string> ExportToPrettyPrintStringList();

        public IList<string> GetPropertyHeaders();
    }

    public enum ExportType
    {
        Excel,
        CSV,
        Json,
        Txt
    }
}
