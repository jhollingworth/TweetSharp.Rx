#region License

// TweetSharp
// Copyright (c) 2010 Daniel Crenna and Jason Diller
// 
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

#endregion

using System;

namespace TweetSharp.Twitter.Fluent
{
#if(!SILVERLIGHT)
    ///<summary>
    /// Parameters for queries against the Twitter search api
    ///</summary>
    [Serializable]
#endif

    public class FluentTwitterSearchParameters : IFluentTwitterSearchParameters
    {
        #region IFluentTwitterSearchParameters Members
        /// <summary>
        /// Gets or sets the phrase to search for
        /// </summary>
        public string SearchPhrase { get; set; }
        /// <summary>
        /// Gets or set a phrase that search results should not contain
        /// </summary>
        public string SearchWithoutPhrase { get; set; }
        /// <summary>
        /// Gets or sets the result type expected in search results.
        /// </summary>
        public SearchResultType? SearchResultType { get; set; }
        /// <summary>
        /// Gets or sets the user name to search for statuses from
        /// </summary>
        public string SearchFromUser { get; set; }
        /// <summary>
        /// Gets or sets the user name to search for statuses to
        /// </summary>
        public string SearchToUser { get; set; }
        /// <summary>
        /// Gets or sets the hash tag to search for
        /// </summary>
        public string SearchHashTag { get; set; }
        /// <summary>
        /// Gets or sets the username that search results should reference
        /// </summary>
        public string SearchReferences { get; set; }
        /// <summary>
        /// gets or sets the location name to search for statuses near
        /// </summary>
        public string SearchNear { get; set; }
        /// <summary>
        /// gets or sets the radius to use for location-based searches
        /// </summary>
        public double? SearchMiles { get; set; }
        /// <summary>
        /// Gets or sets the baseline time to search for statuses after
        /// </summary>
        public DateTime? SearchSince { get; set; }
        /// <summary>
        /// Gets or sets the time to search for statuses before
        /// </summary>
        public DateTime? SearchSinceUntil { get; set; }
        /// <summary>
        /// Gets or sets the date to search for statuses posted on
        /// </summary>
        public DateTime? SearchDate { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether or not to limit search results 
        /// to those expressing a negative sentiment
        /// </summary>
        public bool? SearchNegativity { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether or not to limit search results 
        /// to those expressing a positive sentiment
        /// </summary>
        public bool? SearchPositivity { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether or not to limit search results 
        /// to those containing links
        /// </summary>
        public bool? SearchContainingLinks { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether or not to limit search results 
        /// to those referencing current trend
        /// </summary>
        public bool? SearchCurrentTrends { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether or not to exclude 
        /// trends containing hashtags
        /// </summary>
        public bool? SearchExcludesHashtags { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether or not to fetch 
        /// daily trends
        /// </summary>
        public bool? SearchDailyTrends { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether or not to fetch 
        /// weekly trends
        /// </summary>
        public bool? SearchWeeklyTrends { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether or not to prepend "&lt;user&gt;:" to search results 
        /// </summary>
        public bool? SearchShowUser { get; set; }
        /// <summary>
        /// Gets or sets the desired language for search results
        /// </summary>
        public string SearchLanguage { get; set; }
        /// <summary>
        /// gets or sets the originating language of the query
        /// </summary>
        public string SearchLocale { get; set; }
        /// <summary>
        /// Gets or sets the latitude to use as the center of the search
        /// </summary>
        public double? SearchGeoLatitude { get; set; }
        /// <summary>
        /// Gets or sets the longitude to use as the center of the search
        /// </summary>
        public double? SearchGeoLongitude { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether or not to limit search results to 
        /// statuses containing a question
        /// </summary>
        public bool? SearchQuestion { get; set; }

        #endregion
    }
}