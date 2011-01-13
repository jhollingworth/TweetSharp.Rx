using System;

namespace TweetSharp.Twitter.Fluent
{
#if !SILVERLIGHT
    /// <summary>
    /// The type of search result requested in query.
    /// <seealso href="http://apiwiki.twitter.com/Twitter-Search-API-Method%3A-search" />
    /// </summary>
    [Serializable]
#endif
    public enum SearchResultType
    {
        /// <summary>
        /// Include both popular and real time results in the response.
        /// </summary>
        Mixed,
        /// <summary>
        /// The current default value. Return only the most recent results in the response.
        /// </summary>
        Recent,
        /// <summary>
        /// Return only the most popular results in the response.
        /// </summary>
        Popular
    }
}