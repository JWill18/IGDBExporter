namespace IGDB_DataQuery.Authorization
{
    /// <summary>
    /// Represents the HTTP Json Response sent when getting authorization to use the app
    /// </summary>
    public class IGDBAuthResponse
    {
        /// <summary>
        /// Access Token used by HTTP Requests to prove authentication. Do not print openly
        /// </summary>
        public string? access_token { get; set; }

        /// <summary>
        /// The amount of seconds before access_token expires
        /// </summary>
        public int expires_in { get; set; }

        /// <summary>
        /// The type of access_token that was returned
        /// </summary>
        public string? token_type { get; set; }
    }
}
