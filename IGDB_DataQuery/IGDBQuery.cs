using IGDB_DataQuery.Authorization;

namespace IGDB_DataQuery
{
    /// <summary>
    /// Enum to more easily select different types of queries
    /// </summary>
    public enum IGDBQueryType
    {
        Games,
        Platforms,
        Covers,
        Genres,
        AgeRatings
    }

    /// <summary>
    /// Utility to create different kinds of URL requests that point towards IGDB API
    /// For more information: https://api-docs.igdb.com/#endpoints
    /// </summary>
    public static class IGDBQuery
    {
        /// <summary>
        /// Base URL for all IGDB requests
        /// </summary>
        public static string BaseURL = "https://api.igdb.com/v4/";

        /// <summary>
        /// Creates a base url query depending on the type of request
        /// </summary>
        /// <param name="queryType">The type of query to make (Games, Platforms, etc.)</param>
        /// <returns>A base url that points to the requested endpoint</returns>
        public static Uri GetQueryTypeUrl(IGDBQueryType queryType)
        {
            string urlQuery;
            switch (queryType)
            {
                case IGDBQueryType.Games:
                    urlQuery = "games";
                    break;
                case IGDBQueryType.Platforms:
                    urlQuery = "platforms";
                    break;
                case IGDBQueryType.Covers:
                    urlQuery = "covers";
                    break;
                default:
                    urlQuery = "";
                    break;
            }

            return new Uri(BaseURL + urlQuery);
        }

        /// <summary>
        /// Creates a Request object that includes the authorization needed to be recieved by IGDB
        /// </summary>
        /// <param name="requestURL">A URI object that contains the endpoint for IGDB</param>
        /// <param name="bodyParameters">Represents the filters and conditons needed for the query request</param>
        /// <returns>The request message ready to send off to IGDB</returns>
        public static HttpRequestMessage CreateIGDBRequest(Uri requestURL, IGDBQueryBodyParameters bodyParameters)
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, requestURL);
            request.Headers.Add("Accept", "application/json");
            request.Headers.Add("Client-ID", IGDBCreditentials.ClientCode);
            request.Headers.Add("Authorization", IGDBCreditentials.TokenType + " " + IGDBCreditentials.AccessToken);

            var httpContent = new StringContent(bodyParameters.ToString());
            request.Content = httpContent;
            return request;
        }

        /// <summary>
        /// Sends the request using HttpClient and returns the response.
        /// </summary>
        /// <param name="request">The request object that needs to be sent</param>
        /// <param name="client">A HTTPClient to use to send the request. Best Practice recommends only one HTTPClient per application so IGDBAuthorization does not create one of its own.</param>
        /// <returns>The response from the request containing status code and message</returns>
        public static async Task<HttpResponseMessage> SendIGDBRequestAsync(HttpRequestMessage request, HttpClient client)
        {
            return await client.SendAsync(request);
        }
    }
}
