namespace IGDB_DataQuery.Authorization
{
    /// <summary>
    /// Easy to reach access to Credentials when needed. Values should be kept confidential
    /// </summary>
    public static class IGDBCreditentials
    {
        /// <summary>
        /// Client Code supplied by going to the Developer Tab in Twitch. Public ID.
        /// </summary>
        public static string? ClientCode;

        /// <summary>
        /// Secret string supplied by the Developer Tab in Twitch. Do not share and keep confidential.
        /// </summary>
        public static string? ClientSecret;

        /// <summary>
        /// The type of autentication to be used when reaching out to Twitch for authorization
        /// </summary>
        public static string? GrantType;

        /// <summary>
        /// The access token that proves authentication. Returned by the server when successfully authenticated
        /// </summary>
        public static string? AccessToken;

        /// <summary>
        /// The type of token that Access Token represents
        /// </summary>
        public static string? TokenType;
    }
}