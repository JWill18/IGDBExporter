using Newtonsoft.Json;

namespace IGDB_ExporterData.Data
{
    public class Platform : IGDBObject
    {
        [JsonProperty("name")]
        public string? Name { get; set; }

        [JsonProperty("generation")]
        public int? Generation { get; set; }

        [JsonProperty("platform_family.name")]
        public string? PlatformFamily { get; set; }

        [JsonProperty("url")]
        public string? PlatformURL { get; set; }

        [JsonProperty("platform_logo")]
        public IGDBImage? Logo { get; set; }

        public override IList<string> ExportToPrettyPrintStringList()
        {
            // Items must match the header order for GetPropertyHeaders()
            // Use | delimiter for arrays and store them in the same slot
            var export = new List<string>();

            // IGDB_ID
            export.Add(IGDB_ID.ToString());

            // Name
            export.Add(Name ?? "");

            // Generation
            if (Generation != null)
                export.Add(Generation.ToString() ?? "");
            else
                export.Add("");

            // Platform Family
            export.Add(PlatformFamily ?? "");

            // Platform Url
            export.Add(PlatformURL ?? "");

            // Logo
            if (Logo != null)
                export.Add(Logo.LogoUrl ?? "");
            else
                export.Add("");

            return export;
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

            // Generation
            if (Generation != null)
                export.Add(Generation.ToString() ?? "");
            else
                export.Add("");

            // Platform Family
            export.Add(PlatformFamily ?? "");

            // Platform Url
            export.Add(PlatformURL ?? "");

            // Logo
            if (Logo != null)
                export.Add(Logo.IGDB_ID.ToString() ?? "");
            else
                export.Add("");

            return export;
        }

        public override IList<string> GetPropertyHeaders()
        {
            return new string[] { "IGDB ID", "Name", "Generation", "Platform Family", "Platform Url", "Logo"};
        }
    }
}
