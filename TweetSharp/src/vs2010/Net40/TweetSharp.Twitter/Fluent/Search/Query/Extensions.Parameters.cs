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
using System.Globalization;
using TweetSharp.Twitter.Model;

namespace TweetSharp.Twitter.Fluent
{
    /// <summary>
    /// Extension methods for search parameters.
    /// </summary>
    partial class Extensions
    {
        /// <summary>
        /// Searches for tweets in a given language.
        /// Uses the two-letter ISO code, i.e. "en" to represent the language.
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="languageCode"></param>
        /// <returns></returns>
        public static ITwitterSearchQuery InLanguage(this ITwitterSearchQuery instance, string languageCode)
        {
            instance.Root.SearchParameters.SearchLanguage = languageCode;
            return instance;
        }

        /// <summary>
        /// Searches for tweets in a given language. 
        /// Uses the two-letter ISO code, i.e. "en" to represent the language.
        /// Uses the given CultureInfo to locate a two-letter ISO code for the language.
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="cultureInfo">The <see cref="CultureInfo" /> from which to derive a two-letter ISO code</param>
        /// <returns></returns>
        public static ITwitterSearchQuery InLanguage(this ITwitterSearchQuery instance, CultureInfo cultureInfo)
        {
            instance.Root.SearchParameters.SearchLanguage = cultureInfo.TwoLetterISOLanguageName;
            return instance;
        }

        /// <summary>
        /// Searches for tweets from a given language. For client searches.
        /// Uses the two-letter ISO code, i.e. "en" to represent the language.
        /// Currently only "ja" is effective.
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="languageCode">The two-letter ISO code representing the language</param>
        /// <returns></returns>
        public static ITwitterSearchQuery InLocale(this ITwitterSearchQuery instance, string languageCode)
        {
            instance.Root.SearchParameters.SearchLocale = languageCode;
            return instance;
        }

        /// <summary>
        /// Indicates the search query phrase itself is in a given language.
        /// Uses the two-letter ISO code, i.e. "en" to represent the language.
        /// Currently only "ja" is effective.
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="cultureInfo">The two-letter ISO code representing the language</param>
        /// <returns></returns>
        public static ITwitterSearchQuery InLocale(this ITwitterSearchQuery instance, CultureInfo cultureInfo)
        {
            instance.Root.SearchParameters.SearchLocale = cultureInfo.TwoLetterISOLanguageName;
            return instance;
        }

        /// <summary>
        /// Specifies the number of tweets to return per page result.
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="tweetsPerPage"></param>
        /// <returns></returns>
        public static ITwitterSearchQuery Take(this ITwitterSearchQuery instance, int tweetsPerPage)
        {
            instance.Root.Parameters.ReturnPerPage = tweetsPerPage;
            return instance;
        }

        /// <summary>
        /// Specifies the page of tweets to return. Omitting this expression is equivalent to
        /// requesting the first page. The number of tweets per page is either a Twitter default,
        /// or the value provided using <seealso cref="Take(ITwitterSearchQuery,int)" />.
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="page">The requested page number</param>
        /// <returns></returns>
        public static ITwitterSearchQuery Skip(this ITwitterSearchQuery instance, int page)
        {
            instance.Root.Parameters.Page = page;
            return instance;
        }

        /// <summary>
        /// Returns only tweets before the given status ID.
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="id">The status ID</param>
        /// <returns></returns>
        public static ITwitterSearchQuery Before(this ITwitterSearchQuery instance, long id)
        {
            instance.Root.Parameters.MaxId = id;
            return instance;
        }

        /// <summary>
        /// Returns only tweets before the given status ID.
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="id">The status ID</param>
        /// <returns></returns>
        public static ITwitterSearchQuery Before(this ITwitterSearchQuery instance, int id)
        {
            instance.Root.Parameters.MaxId = id;
            return instance;
        }

        /// <summary>
        /// Returns only tweets since the last status ID.
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="id">The status ID</param>
        /// <returns></returns>
        public static ITwitterSearchQuery Since(this ITwitterSearchQuery instance, int id)
        {
            instance.Root.Parameters.SinceId = id;
            return instance;
        }

        /// <summary>
        /// Returns only tweets since the last status ID.
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="id">The status ID</param>
        /// <returns></returns>
        public static ITwitterSearchQuery Since(this ITwitterSearchQuery instance, long id)
        {
            instance.Root.Parameters.SinceId = id;
            return instance;
        }
        
        /// <summary>
        /// Searches tweets within a given mile radius. This method must be used with the
        /// <seealso cref="Of(ITwitterSearchQuery,double,double)"/> expression to provide a complete
        /// command. This will likely change with evolution of the Twitter API.
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="miles"></param>
        /// <returns></returns>
        public static ITwitterSearchQuery Within(this ITwitterSearchQuery instance, double miles)
        {
            instance.Root.SearchParameters.SearchMiles = miles;
            return instance;
        }

        /// <summary>
        /// Returns only tweets that fall on or after the given date. Use the convenient
        /// extension methods provided for spanning time, i.e.
        /// <example>
        ///     .Since(10.Days().Ago())
        /// </example>
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public static ITwitterSearchQuery Since(this ITwitterSearchQuery instance, DateTime date)
        {
            instance.Root.SearchParameters.SearchSince = date;
            return instance;
        }

        /// <summary>
        /// Returns only tweets that fall on or before a given date. Use the convenient
        /// extension methods provided for spanning time, i.e.
        /// <example>
        ///     .SinceUntil(30.Minutes().Ago())
        /// </example>
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public static ITwitterSearchQuery SinceUntil(this ITwitterSearchQuery instance, DateTime date)
        {
            instance.Root.SearchParameters.SearchSinceUntil = date;
            return instance;
        }

        /// <summary>
        /// Use this method combined with <code>Within(double miles)</code> to search areas around geo locations
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        /// <returns></returns>
        public static ITwitterSearchQuery Of(this ITwitterSearchQuery instance, double latitude, double longitude)
        {
            instance.Root.SearchParameters.SearchGeoLatitude = latitude;
            instance.Root.SearchParameters.SearchGeoLongitude = longitude;
            return instance;
        }

        /// <summary>
        /// Use this method combined with <code>Within(double miles)</code> to search areas around geo locations
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="location">The <see cref="TwitterGeoLocation" /> to constrain the search with</param>
        /// <returns></returns>
        public static ITwitterSearchQuery Of(this ITwitterSearchQuery instance, TwitterGeoLocation location)
        {
            instance.Root.SearchParameters.SearchGeoLatitude = Convert.ToDouble(
                location.Coordinates.Latitude, CultureInfo.InvariantCulture
                );
            instance.Root.SearchParameters.SearchGeoLongitude = Convert.ToDouble(
                location.Coordinates.Longitude, CultureInfo.InvariantCulture
                );
            return instance;
        }

        /// <summary>
        /// Use this method to designate what type of search results are desired with a query.
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static ITwitterSearchQuery WithResultType(this ITwitterSearchQuery instance, SearchResultType type)
        {
            instance.Root.SearchParameters.SearchResultType = type;
            return instance;
        }
    }
}