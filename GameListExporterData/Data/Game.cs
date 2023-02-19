using Newtonsoft.Json;

namespace IGDB_ExporterData.Data
{
    public class Game : IGDBObject
    {
        [JsonProperty("name")]
        public string? Name { get; set; }

        [JsonProperty("cover")]
        public IGDBImage? Cover { get; set; }

        [JsonProperty("genres")]
        public IEnumerable<IGDBSimpleObject>? Genres { get; set; }

        [JsonProperty("first_release_date")]
        public string? FirstReleaseDate { get; set; }

        [JsonProperty("age_ratings")]
        public IEnumerable<AgeRating>? AgeRatings { get; set; }

        [JsonProperty("platforms")]
        public IEnumerable<Platform>? Platforms { get; set; }

        [JsonProperty("player_perspectives")]
        public IEnumerable<IGDBSimpleObject>? PlayerPerspectives { get; set; }
        
        [JsonProperty("screenshots")]
        public IEnumerable<IGDBImage>? Screenshots { get; set; }

        [JsonProperty("summary")]
        public string? Summary { get; set; }

        [JsonProperty("total_rating")]
        public double? TotalRatingScore { get; set; }

        [JsonProperty("total_rating_count")]
        public int? TotalRatingCount { get; set; }

        [JsonProperty("url")]
        public string? Url { get; set; }

        public override IList<string> ExportToPrettyPrintStringList()
        {
            // Items must match the header order for GetPropertyHeaders()
            // Use | delimiter for arrays and store them in the same slot
            var export = new List<string>();

            // IGDB_ID
            export.Add(IGDB_ID.ToString());

            // Name
            export.Add(Name ?? "");

            // Cover Url
            if (Cover != null)
            {
                export.Add(Cover.LogoUrl ?? "");
                export.Add(Cover.LogoWidth.ToString() ?? "");
                export.Add(Cover.LogoHeight.ToString() ?? "");
            }
            else
            {
                export.Add("");
                export.Add("");
                export.Add("");
            }

            // Genre IDs
            if (Genres != null)
                export.Add(string.Join("|", Genres.Select(g => g.Name)));
            else
                export.Add("");

            // First Release Date
            export.Add(FirstReleaseDate ?? "");

            // Age Ratings IDs
            if (AgeRatings != null)
                export.Add(string.Join("|", AgeRatings.Select(a => a.GetRatingPrettyPrint())));
            else
                export.Add("");

            // Platforms IDs
            if (Platforms != null)
                export.Add(string.Join("|", Platforms.Select(p => p.Name)));
            else
                export.Add("");

            // Player Perspectives IDs
            if (PlayerPerspectives != null)
                export.Add(string.Join("|", PlayerPerspectives.Select(pp => pp.Name)));
            else
                export.Add("");

            // Screenshot IDs
            if (Screenshots != null)
                export.Add(string.Join("|", Screenshots.Select(s => s.LogoUrl)));
            else
                export.Add("");

            // Summary
            export.Add(Summary ?? "");

            // Total Rating Score
            export.Add(TotalRatingScore.ToString() ?? "");

            // Total Rating Count
            export.Add(TotalRatingCount.ToString() ?? "");

            // Url
            export.Add(Url ?? "");

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

            // Cover Url
            if (Cover != null)
            {
                export.Add(Cover.LogoUrl ?? "");
                export.Add(Cover.LogoWidth.ToString() ?? "");
                export.Add(Cover.LogoHeight.ToString() ?? "");
            }
            else
            {
                export.Add("");
                export.Add("");
                export.Add("");
            }

            // Genre IDs
            if (Genres != null)
                export.Add(string.Join("|", Genres.Select(g => g.IGDB_ID.ToString())));
            else
                export.Add("");

            // First Release Date
            export.Add(FirstReleaseDate ?? "");

            // Age Ratings IDs
            if (AgeRatings != null)
                export.Add(string.Join("|", AgeRatings.Select(a => a.IGDB_ID.ToString())));
            else
                export.Add("");

            // Platforms IDs
            if (Platforms != null)
                export.Add(string.Join("|", Platforms.Select(p => p.IGDB_ID.ToString())));
            else
                export.Add("");

            // Player Perspectives IDs
            if (PlayerPerspectives != null)
                export.Add(string.Join("|", PlayerPerspectives.Select(pp => pp.IGDB_ID.ToString())));
            else
                export.Add("");

            // Screenshot IDs
            if (Screenshots != null)
                export.Add(string.Join("|", Screenshots.Select(s => s.IGDB_ID.ToString())));
            else
                export.Add("");

            // Summary
            if (Summary != null)
                export.Add(Summary);

            // Total Rating Score
            export.Add(TotalRatingScore.ToString() ?? "");

            // Total Rating Count
            export.Add(TotalRatingCount.ToString() ?? "");

            // Url
            export.Add(Url ?? "");

            return export;
        }

        public override IList<string> GetPropertyHeaders()
        {
            return new string[] { "IGDB ID", "Name", "Cover Url", "Cover Width", "Cover Height", "Genre IDs", "First Release Date", "Age Ratings IDs", 
                "Platform IDs", "Player Perspectives IDs", "Screenshot IDs", "Summary", "Total Rating Score", "Total Rating Count", "Url"};
        }
    }
}