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
    /// <summary>
    /// Parameters for the Twitter Search Api
    /// </summary>
    public interface IFluentTwitterSearchParameters
    {
        /// <summary>
        /// Gets or sets the phrase to search for
        /// </summary>
        string SearchPhrase { get; set; }
        /// <summary>
        /// Gets or sets the screenname of a user whose tweets you want to search
        /// </summary>
        string SearchFromUser { get; set; }
        /// <summary>
        /// Gets or sets the screenname of a user to whom search results are in reply to
        /// </summary>
        string SearchToUser { get; set; }
        /// <summary>
        /// Gets or sets the hash tag to search for
        /// </summary>
        string SearchHashTag { get; set; }
        /// <summary>
        /// Gets or sets the screenname of a user who is mentioned in the search results
        /// </summary>
        string SearchReferences { get; set; }
        /// <summary>
        /// Gets or sets a place name from near where the search results should originate
        /// </summary>
        string SearchNear { get; set; }
        /// <summary>
        /// Gets or sets the number of miles to use as a search radius
        /// </summary>
        double? SearchMiles { get; set; }
        /// <summary>
        /// Gets or sets a begin-date from which to limit search results
        /// </summary>
        DateTime? SearchSince { get; set; }
        /// <summary>
        /// Gets or sets an end-date to which to limit search results
        /// </summary>
        DateTime? SearchSinceUntil { get; set; }
        /// <summary>
        /// Gets or sets a specific date to which to limit search results 
        /// </summary>
        DateTime? SearchDate { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether or not to limit search results to those that are negative in tone
        /// </summary>
        bool? SearchNegativity { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether or not to limit search results to those that are positive in tone
        /// </summary>
        bool? SearchPositivity { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether or not to limit search results to those that contain links
        /// </summary>
        bool? SearchContainingLinks { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether or not to prepend &lt;user&gt;: to the search results
        /// </summary>
        bool? SearchShowUser { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether or not to limit search results to those containing a question
        /// </summary>
        bool? SearchQuestion { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether or not to fetch current trends
        /// </summary>
        bool? SearchCurrentTrends { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether or not to exclude hash-tag trends from the requested trends 
        /// </summary>
        bool? SearchExcludesHashtags { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether or not to get trends for a specific day
        /// </summary>
        bool? SearchDailyTrends { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether or not to get trends for a specific week
        /// </summary>
        bool? SearchWeeklyTrends { get; set; }
        /// <summary>
        /// Gets or sets a the desired language to which to limit search results
        /// Uses the two-letter ISO code, i.e. "en" to represent the language.
        /// </summary>
        string SearchLanguage { get; set; }
        /// <summary>
        /// Gets or sets the language code indicating what language the search query itself is in. 
        /// Currently only "ja" is effective.
        /// </summary>
        string SearchLocale { get; set; }
        /// <summary>
        /// Gets or sets the latitude for location-based searches
        /// </summary>
        double? SearchGeoLatitude { get; set; }
        /// <summary>
        /// Gets or sets the longitude for location-based searches
        /// </summary>
        double? SearchGeoLongitude { get; set; }
        /// <summary>
        /// Gets or sets a phrase which should not be included in search results. 
        /// </summary>
        string SearchWithoutPhrase { get; set; }
        /// <summary>
        /// Gets or sets the result type expected in search results.
        /// </summary>
        SearchResultType? SearchResultType { get; set; }
    }
}