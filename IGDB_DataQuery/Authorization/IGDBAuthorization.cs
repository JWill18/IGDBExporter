using System.Net;
using Newtonsoft.Json;

namespace IGDB_DataQuery.Authorization
{
    /// <summary>
    /// Authenticates the application and is necessary before making Query Requests to IGDB
    /// </summary>
    public static class IGDBAuthorization
    {
        /// <summary>
        /// The base url for Twitch OAuth
        /// </summary>
        public static string BaseTwitchAuthUrl = "https://id.twitch.tv/oauth2/";

        /// <summary>
        /// Sends a request to Twitch to get an access token. IGDB Credentials should be filled out before calling this.
        /// </summary>
        /// <param name="client">A HTTPClient to use to send the request. Best Practice recommends only one HTTPClient per application so IGDBAuthorization does not create one of its own.</param>
        public static async Task GetAuthorization(HttpClient client, IGDBAuthParameters parameters)
        {
            HttpRequestMessage authRequest = new HttpRequestMessage(HttpMethod.Post, BaseTwitchAuthUrl + $"token?{parameters}");
            using (var response = await client.SendAsync(authRequest))
            {
                HandleJsonAuthResponse(response);
            }
        }

        /// <summary>
        /// Parses out the response by Authentication request. Throws error if authentication failed
        /// </summary>
        /// <param name="response">The response given by the HTTP Request for authentication</param>
        private static void HandleJsonAuthResponse(HttpResponseMessage response)
        {
            if (response.StatusCode == HttpStatusCode.OK)
            {
                using (var jsonString = response.Content.ReadAsStringAsync())
                {
                    var jsonResponse = JsonConvert.DeserializeObject<IGDBAuthResponse>(jsonString.Result);

                    if (jsonResponse != null)
                    {
                        if (jsonResponse.access_token != null)
                            IGDBCreditentials.AccessToken = jsonResponse.access_token;

                        if (jsonResponse.token_type != null)
                            IGDBCreditentials.TokenType = jsonResponse.token_type;
                    }
                }
            }
            else
            {
                var errorMessage = response.Content.ReadAsStringAsync();
                errorMessage.Wait();

                throw new HttpRequestException($"Failed to Authenticate the Application. See response for more information. {errorMessage.Result}", null, response.StatusCode);
            }
        }
    }
}
