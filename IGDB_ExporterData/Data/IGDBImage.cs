using Newtonsoft.Json;

namespace IGDB_ExporterData.Data
{
    public class IGDBImage : IGDBObject
    {
        [JsonProperty("width")]
        public int? LogoWidth { get; set; }

        [JsonProperty("height")]
        public int? LogoHeight { get; set; }

        [JsonProperty("url")]
        public string? LogoUrl { get; set; }

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

            // LogoWidth
            if (LogoWidth != null)
                export.Add(LogoWidth.ToString() ?? "");
            else
                export.Add("");

            // LogoHeight
            if (LogoHeight != null)
                export.Add(LogoHeight.ToString() ?? "");
            else
                export.Add("");

            // LogoUrl
            export.Add(LogoUrl ?? "");

            return export;
        }

        public override IList<string> GetPropertyHeaders()
        {
            return new string[] { "IGDB ID", "Image Width", "Image Height", "Url" };
        }
    }
}
