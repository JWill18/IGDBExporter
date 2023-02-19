namespace IGDB_DataQuery.Authorization
{
    /// <summary>
    /// Used to Pass parameters to the Get request for Authorization.
    /// </summary>
    public class IGDBAuthParameters
    {
        /// <summary>
        /// Represents the client ID of the application. Can be found in the Twitch Developer portal
        /// </summary>
        public string? ClientId { get; set; }

        /// <summary>
        /// Represent the client secret that is tied to the Client ID. Can be found in the Twitch Developer portal and should not be shared.
        /// </summary>
        public string? ClientSecret { get; set; }

        /// <summary>
        /// The type of permissions that are being requested.
        /// </summary>
        public string? GrantType { get; set; }

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="clientId">Client ID associated with the application</param>
        /// <param name="clientSecret">Secret that is tied to the Client ID</param>
        /// <param name="grantType">Type of permissions that are being requested</param>
        public IGDBAuthParameters(string? clientId, string? clientSecret, string? grantType)
        {
            ClientId = clientId;
            ClientSecret = clientSecret;
            GrantType = grantType;
        }

        /// <summary>
        /// Overriding the ToString to create text that can be added in the query parameters of a URL
        /// </summary>
        /// <returns>A URL friendly string</returns>
        public override string ToString()
        {
            return $"client_id={ClientId}&client_secret={ClientSecret}&grant_type={GrantType}";
        }
    }
}
