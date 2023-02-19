using Newtonsoft.Json;

namespace IGDB_ExporterData.Data
{
    public class AgeRating : IGDBObject
    {
        [JsonProperty("category")]
        public int? Category { get; set; }
        
        [JsonProperty("rating")]
        public int? Rating { get; set; }

        public override IList<string> ExportToPrettyPrintStringList()
        {
            // Items must match the header order for GetPropertyHeaders()
            // Use | delimiter for arrays and store them in the same slot
            var export = new List<string>();

            // IGDB_ID
            export.Add(IGDB_ID.ToString());

            // Category
            if (Category != null)
                export.Add(GetCategoryPrettyPrint());
            else
                export.Add("");

            // Rating
            if (Rating != null)
                export.Add(GetRatingPrettyPrint());
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

            // Category
            if (Category != null)
                export.Add(Category.ToString() ?? "");
            else
                export.Add("");

            // Rating
            if (Rating != null)
                export.Add(Rating.ToString() ?? "");
            else
                export.Add("");

            return export;
        }

        public override IList<string> GetPropertyHeaders()
        {
            return new string[] { "IGDB ID", "Category", "Rating" };
        }

        public string GetCategoryPrettyPrint()
        {
            switch(Category)
            {
                case 1:
                    return "ESRB";
                case 2:
                    return "PEGI";
                case 3:
                    return "CERO";
                case 4:
                    return "USK";
                case 5:
                    return "GRAC";
                case 6:
                    return "CLASS_IND";
                case 7:
                    return "ACB";
            }

            return "";
        }

        public string GetRatingPrettyPrint()
        {
            switch (Rating)
            {
                case 1:
                    return "Three";
                case 2:
                    return "Seven";
                case 3:
                    return "Twelve";
                case 4:
                    return "Sixteen";
                case 5:
                    return "Eighteen";
                case 6:
                    return "RP";
                case 7:
                    return "EC";
                case 8:
                    return "E";
                case 9:
                    return "E10";
                case 10:
                    return "T";
                case 11:
                    return "M";
                case 12:
                    return "AO";
                case 13:
                    return "CERO_A";
                case 14:
                    return "CERO_B";
                case 15:
                    return "CERO_C";
                case 16:
                    return "CERO_D";
                case 17:
                    return "CERO_Z";
                case 18:
                    return "USK_0";
                case 19:
                    return "USK_6";
                case 20:
                    return "USK_12";
                case 21:
                    return "USK_18";
                case 22:
                    return "GRAC_ALL";
                case 23:
                    return "GRAC_Twelve";
                case 24:
                    return "GRAC_Fifteen";
                case 25:
                    return "GRAC_Eighteen";
                case 26:
                    return "GRAC_TESTING";
                case 27:
                    return "CLASS_IND_L";
                case 28:
                    return "CLASS_IND_Ten";
                case 29:
                    return "CLASS_IND_Twelve";
                case 30:
                    return "CLASS_IND_Fourteen";
                case 31:
                    return "CLASS_IND_Sixteen";
                case 32:
                    return "CLASS_IND_Eighteen";
                case 33:
                    return "ACB_G";
                case 34:
                    return "ACB_PG";
                case 35:
                    return "ACB_M";
                case 36:
                    return "ACB_MA15";
                case 37:
                    return "ACB_R18";
                case 38:
                    return "ACB_RC";
            }

            return "";
        }
    }
}
