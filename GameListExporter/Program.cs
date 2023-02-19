using IGDB_ExporterData.Data;
using IGDB_ExporterData.Export;
using IGDB_DataQuery;
using IGDB_DataQuery.Authorization;
using Newtonsoft.Json;

public class IGDB_Exporter
{
    private static readonly List<string> GameQueryFields = new() { "name", "cover.url", "cover.width", "cover.height", "genres.name", "genres.url", "first_release_date", "age_ratings.category", "age_ratings.rating", "platforms.name", "platforms.generation", "platforms.platform_family.name", "platforms.url", "platforms.platform_logo.width", "platforms.platform_logo.height", "platforms.platform_logo.url", "player_perspectives.name", "player_perspectives.url", "screenshots.height", "screenshots.width", "screenshots.url", "summary", "total_rating", "total_rating_count", "url" };
    private static readonly List<string> PlatformQueryFields = new() { "name", "generation", "platform_family.name", "url", "platform_logo.width", "platform_logo.height", "platform_logo.url" };
    private static readonly List<string> GenreQueryFields = new() { "name", "url" };
    private static readonly List<string> AgeRatingQueryFields = new() { "age_ratings.category", "age_ratings.rating" };
    private static readonly List<string> GameAgeRatingGroups = new() { "ESRB", "PEGI", "CERO", "USK", "GRAC", "CLASS_IND", "ACB" };
    static async Task Main(string[] args)
    {
        CheckCredentials();

        Console.WriteLine("// Welcome to the IGDB Exporter!");
        
        // Asks user for query input
        var queryType = GetInputQueryType();

        var exportType = GetExportType();
        var fileString = GetInputSimpleString("\nPlease specify the filepath and file name of the file you wish to create.\nFile extension is optional.");
        
        var limit = GetInputLimit("\nPlease specify how many you would like to query. Do not put anything to pull all available records");

        using HttpClient client = new();

        // Get Authentication from Twitch to access IGDB endpoints
        var parameters = new IGDBAuthParameters(IGDBCreditentials.ClientCode, IGDBCreditentials.ClientSecret, IGDBCreditentials.GrantType);

        try
        {
            Console.WriteLine("\nGetting Authorization to query IGDB...");
            await IGDBAuthorization.GetAuthorization(client, parameters);

            // If we got permission, grab list of games. Else error out.
            if (!string.IsNullOrEmpty(IGDBCreditentials.AccessToken))
            {
                string filters = "";
                List<string> queryFields = new List<string>();
                IEnumerable<IDataExport> results = new List<IDataExport>();
                switch (queryType)
                {
                    case IGDBQueryType.Games:
                        queryFields = GameQueryFields;
                        filters = GetGameInputFilters();
                        results = await ResultFromIGDBIterator<Game>(client, queryType, queryFields, filters, limit);
                        break;
                    case IGDBQueryType.Platforms:
                        queryFields = PlatformQueryFields;
                        filters = GetPlatformInputFilters();
                        results = await ResultFromIGDBIterator<Platform>(client, queryType, queryFields, filters, limit);
                        break;
                    case IGDBQueryType.Covers:
                        queryFields = new() { "*" };
                        results = await ResultFromIGDBIterator<Cover>(client, queryType, queryFields, "", limit);
                        break;
                    case IGDBQueryType.Genres:
                        queryFields = GenreQueryFields;
                        var genreResults = await ResultFromIGDBIterator<IGDBSimpleObject>(client, queryType, queryFields, "", limit);
                        break;
                    case IGDBQueryType.AgeRatings:
                        queryFields = AgeRatingQueryFields;
                        results = await ResultFromIGDBIterator<AgeRating>(client, queryType, queryFields, "", limit);
                        break;
                    default:
                        break;
                }

                switch (exportType)
                {
                    case ExportType.Excel:
                        if (!fileString.EndsWith(".xlsx"))
                            fileString += ".xlsx";
                        break;
                    case ExportType.CSV:
                        if (!fileString.EndsWith(".csv"))
                            fileString += ".csv";
                        break;
                    case ExportType.Json:
                        if (!fileString.EndsWith(".json"))
                            fileString += ".json";
                        break;
                    case ExportType.Txt:
                        if (!fileString.EndsWith(".txt"))
                            fileString += ".txt";
                        break;
                }

                bool prettyPrint = false;

                // Currently Json exporter does not support pretty print
                if (exportType != ExportType.Json)
                    prettyPrint = GetInputSimpleDecision("\nDo you want data to be exported as pretty print? y/n");

                SaveData(results, fileString, exportType, prettyPrint);
            }
            else
            {
                throw new Exception("Failed to Authorize App, Closing application....");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            Environment.Exit(1);
        }
    }

    private static void CheckCredentials()
    {
        try
        {
            // Read a text file line by line.
            string[] lines = File.ReadAllLines("AppCredentials.txt");

            // Grab credentials
            IGDBCreditentials.ClientCode = lines[0].Split('|')[1];
            IGDBCreditentials.ClientSecret = lines[1].Split('|')[1];
            IGDBCreditentials.GrantType = lines[2].Split('|')[1];
        }
        catch (Exception e)
        {
            Console.WriteLine("Failed to configure credentials from file. See message for more information:" + "\n" + e.Message);
            Environment.Exit(1);
        }
    }

    private static IGDBQueryType GetInputQueryType()
    {
        do
        {
            Console.WriteLine("\nPlease choice what you are wanting to export.");

            var queryTypes = Enum.GetValues(typeof(IGDBQueryType));
            for (int i = 0; i < queryTypes.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {queryTypes.GetValue(i)}");
            }
            var queryTypeString = Console.ReadLine();
            try
            {
                var queryTypeNum = int.Parse(queryTypeString);

                if (Enum.IsDefined(typeof(IGDBQueryType), queryTypeNum - 1))
                {
                    return (IGDBQueryType)queryTypeNum - 1;
                }
                else
                {
                    throw new Exception("\nInvalid Query Type selection. Please try again.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        } while (true);
    }

    private static ExportType GetExportType()
    {
        do
        {
            Console.WriteLine("\nPlease choice what you are wanting to export.");

            var exportTypes = Enum.GetValues(typeof(ExportType));
            for (int i = 0; i < exportTypes.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {exportTypes.GetValue(i)}");
            }
            var exportTypeString = Console.ReadLine();
            try
            {
                var exportTypeNum = int.Parse(exportTypeString);

                if (Enum.IsDefined(typeof(IGDBQueryType), exportTypeNum - 1))
                {
                    return (ExportType)exportTypeNum - 1;
                }
                else
                {
                    throw new Exception("\nInvalid Export Type selection. Please try again.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        } while (true);
    }

    private static string GetGameInputFilters()
    {
        string filters = "";

        var searchSpecificGameName = GetInputSimpleDecision("\nDo you have a specific game you are looking for? Y/N");
        var searchSpecificConsoleName = GetInputSimpleDecision("\nDo you have a specific console you are looking for? Y/N");
        var searchSpecificRegionCode = GetInputSimpleDecision("\nAre you looking for a specific region (based on Region Rating Boards)? Y/N");

        if (searchSpecificGameName)
        {
            var gameName = GetInputSimpleString("\nPlease enter the name of the game you want to search for:");
            if (!string.IsNullOrEmpty(gameName))
                filters += $"name = \"{gameName}\"";
        }
        
        if(searchSpecificConsoleName)
        {
            if (!string.IsNullOrEmpty(filters))
                filters += " & ";

            var consoleName = GetInputSimpleString("\nPlease enter the name of the platform you want to search for:");

            if (!string.IsNullOrEmpty(consoleName))
                filters += $"platforms.name = \"{consoleName}\"";
        }

        if (searchSpecificRegionCode)
        {
            if (!string.IsNullOrEmpty(filters))
                filters += " & ";

            var regionId = GetInputNumberDecision("\nPlease select the region you are looking for:", GameAgeRatingGroups);

            filters += $"age_ratings.category  = {regionId}";
        }

        return filters;
    }

    private static string GetPlatformInputFilters()
    {
        string filters = "";

        var searchSpecificGameName = GetInputSimpleDecision("\nDo you have a specific platform you are looking for? Y/N");

        if (searchSpecificGameName)
        {
            var gameName = GetInputSimpleString("\nPlease enter the name of the game you want to search for:");
            if (!string.IsNullOrEmpty(gameName))
                filters += $"name = \"{gameName}\"";
        }

        return filters;
    }

    private static bool GetInputSimpleDecision(string questionText)
    {
        do
        {
            Console.WriteLine(questionText);

            try
            {
                var specificGameSearchDecision = Console.ReadLine();

                if (specificGameSearchDecision.Trim().ToLower()[0] == 'y')
                {
                    return true;
                }
                else if (specificGameSearchDecision.Trim().ToLower()[0] == 'n')
                {
                    return false;
                }
                else
                {
                    throw new Exception("Invalid decision. Please try again.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        } while (true);
    }

    private static string GetInputSimpleString(string questionText)
    {
        do
        {
            Console.WriteLine(questionText);

            try
            {
                return Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        } while (true);
    }

    private static int GetInputLimit(string questionText)
    {
        do
        {
            Console.WriteLine(questionText);

            try
            {
                var stringInput = Console.ReadLine();
                int intInput = int.MaxValue;

                if (!string.IsNullOrEmpty(stringInput))
                     intInput = int.Parse(stringInput);
                return intInput;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        } while (true);
    }

    private static int GetInputNumberDecision(string questionText, List<string> numberDecisions)
    {
        do
        {
            Console.WriteLine(questionText);

            for(int i = 1; i <= numberDecisions.Count(); i++)
            {
                Console.WriteLine($"{i}. {numberDecisions[i-1]}");
            }

            try
            {
                var stringAnswer = Console.ReadLine();
                var intAnswer = int.Parse(stringAnswer);

                return intAnswer;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        } while (true);
    }

    private static async Task<List<T>> GetResultsFromIGDB<T>(HttpClient client, IGDBQueryType queryType, IEnumerable<string> queryFields, string filters, int limit, int offset=0)
    {
        // Create Query URL to grab list of results from IGDB
        var baseUrl = IGDBQuery.GetQueryTypeUrl(queryType);
        var bodyRequest = new IGDBQueryBodyParameters(queryFields, filters, "id asc", limit > 500 ? 500 : limit, offset);
        var platformRequest = IGDBQuery.CreateIGDBRequest(baseUrl, bodyRequest);

        // Send and recieve response from IGDB
        var response = await IGDBQuery.SendIGDBRequestAsync(platformRequest, client);
        var contentResponse = await response.Content.ReadAsStringAsync();

#pragma warning disable CS8603 // Possible null reference return.
        return JsonConvert.DeserializeObject<List<T>>(contentResponse);
#pragma warning restore CS8603 // Possible null reference return.
    }

    private static async Task<IEnumerable<T>> ResultFromIGDBIterator<T>(HttpClient client, IGDBQueryType queryType, IEnumerable<string> queryFields, string filters, int limit) where T : IDataExport
    {
        List<T> list = new List<T>();
        List<T> queryResults = new List<T>();
        int offset = 0;
        bool foundAllResults = false;

        Console.WriteLine("\nStarting up query. Please wait....");
        do
        {
            var numberOfResultsRequested = limit == int.MaxValue ? "All of the" : $"{limit - list.Count} more";
            Console.WriteLine($"\nCurrently have {list.Count} results...Attempting to Grab {numberOfResultsRequested} results...");
            queryResults = await GetResultsFromIGDB<T>(client, queryType, queryFields, filters, limit - list.Count, offset);
            if (queryResults.Count > 0)
            {
                list.AddRange(queryResults);
                offset = list.Count - 1; // To account for 0 index

                if ((queryResults.Count % 500 != 0 && limit >= 500) || queryResults.Count <= limit && limit < 500)
                    foundAllResults = true;
            }
            else
            {
                foundAllResults = true;
                break;
            }
        } while (!foundAllResults);
        Console.WriteLine($"\nFound {list.Count} results...");
        return list;
    }

    private static void SaveData(IEnumerable<IDataExport> objects, string filePath, ExportType exportType, bool prettyPrint)
    {
        IExporter exporter;

        switch (exportType)
        {
            case ExportType.Excel:
                exporter = new ExcelExporter();
                break;
            case ExportType.CSV:
                exporter = new CsvExporter();
                break;
            case ExportType.Json:
                FileInfo fileInfo = new FileInfo(filePath);
                if (!fileInfo.Directory.Exists) fileInfo.Directory.Create();
                exporter = new JsonExporter();
                break;
            default:
                exporter = new TextExporter();
                break;
        }
        
        exporter.WriteDataToFile(objects.ToList(), filePath, prettyPrint);

    }
}
