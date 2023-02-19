using Newtonsoft.Json;

namespace IGDB_ExporterData.Data
{
    public class IGDBSimpleObject : IGDBObject
    {
        [JsonProperty("name")]
        public string? Name { get; set; }

        [JsonProperty("url")]
        public string? Url { get; set; }

        public override IList<string> ExportToPrettyPrintStringList()
        {
            return ExportToStringList(); // There is no difference in pretty print vs normal export
        }

        public override IList<string> ExportToStringList()
        {
            // Items must match the header order for GetPropertyHeaders()
            // Use | delimiter for arrays and store them in the same slot
            var export = new List<string>();

            // IGDB_ID
            export.Add(IGDB_ID.ToString());

            // Name
            export.Add(Name ?? "");

            // Url
            export.Add(Url ?? "");

            return export;
        }

        public override IList<string> GetPropertyHeaders()
        {
            return new string[] { "IGDB ID", "Name", "Url"};
        }
    }
}
