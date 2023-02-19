namespace IGDB_DataQuery
{
    /// <summary>
    /// Body Parameters that are sent in a IGDBQuery object. Filters database queries
    /// </summary>
    public class IGDBQueryBodyParameters
    {
        /// <summary>
        /// What fields to grab. Set to * to grab all fields in requested query
        /// https://api-docs.igdb.com/?javascript#fields
        /// </summary>
        public IEnumerable<string>? Fields { get; set; }

        /// <summary>
        /// Conditional requirements similar to SQL
        /// https://api-docs.igdb.com/?javascript#filters
        /// </summary>
        public string? Where { get; set; }

        /// <summary>
        /// Sort algorithm for the query results. Similar to SQL.
        /// https://api-docs.igdb.com/?javascript#sorting
        /// </summary>
        public string? Sort { get; set; }

        /// <summary>
        /// Limit to how many results to get. Can be null as IGDB will return 10 results by default. Maximum amount is 500
        /// https://api-docs.igdb.com/?javascript#search-1
        /// </summary>
        public int LimitAmount { get; set; }

        /// <summary>
        /// Offsets how many results to skip before the query starts returning results. Useful for pagination
        /// </summary>
        public int Offset { get; set; }

        /// <summary>
        /// Creates a Body Parameter object that can be used with IGDBQuery to help filter results
        /// </summary>
        /// <param name="fields">Fields that the query should grab</param>
        /// <param name="where">Conditional statement to help filter results</param>
        /// <param name="sort">Fields and direction results should be sorted</param>
        /// <param name="limitAmount">How many results should be returned. Max is 500 and default is 10</param>
        public IGDBQueryBodyParameters(IEnumerable<string>? fields, string? where, string? sort, int limitAmount = 10, int offsetAmount = 0)
        {
            Fields = fields;
            LimitAmount = limitAmount;
            Where = where;
            Sort = sort;
            Offset = offsetAmount;
        }

        /// <summary>
        /// Used to move the offset of the results queried while still using the same parameters object
        /// </summary>
        /// <param name="amount">The amount to increase the existing offset by</param>
        public void IncreaseOffset(int amount)
        {
            Offset += amount;
        }

        /// <summary>
        /// Takes properties and creates a body string needed for IGDB to ingest when sent via HTTP
        /// </summary>
        /// <returns>String ready to be added to the body content of a HTTP Request</returns>
        public override string ToString()
        {
            var bodyString = "fields ";

            if (Fields != null)
                bodyString += $"{string.Join(",", Fields)};";
            else
                bodyString += "*;";

            if (!string.IsNullOrEmpty(Where))
                bodyString += $" where {Where};";

            if (!string.IsNullOrEmpty(Sort))
                bodyString += $" sort {Sort};";

            bodyString += $" limit {LimitAmount};";
            bodyString += $" offset {Offset};";

            return bodyString;
        }
    }
}
