using IGDB_ExporterData.Export;
using IGDB_ExporterData.Utility;
using Newtonsoft.Json;

namespace IGDB_ExporterData.Data
{
    [JsonConverter(typeof(JsonPathConverter))]
    public abstract class IGDBObject: IDataExport
    {
        [JsonProperty("id")]
        public int IGDB_ID { get; set; }

        public abstract IList<string> ExportToPrettyPrintStringList();

        public abstract IList<string> ExportToStringList();

        public abstract IList<string> GetPropertyHeaders();
    }
}
