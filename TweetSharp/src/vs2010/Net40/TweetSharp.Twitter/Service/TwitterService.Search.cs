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
using Hammock.Authentication.OAuth;
using TweetSharp.Twitter.Fluent;
using TweetSharp.Twitter.Model;
using TwitterSearchTrends = TweetSharp.Twitter.Model.TwitterSearchTrends;

namespace TweetSharp.Twitter.Service
{
    partial class TwitterService : ITwitterService
    {
#if !SILVERLIGHT

        #region Search

        /// <summary>
        /// Searches for tweets that match the Twitter search phrase provided.
        /// </summary>
        /// <param name="phrase">The phrase.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-Search-API-Method%3A-search"/>
        public TwitterSearchResult SearchForTweets(string phrase)
        {
            return WithTweetSharp<TwitterSearchResult>(
                q => q.Search().Query().Containing(phrase)
                );
        }

        /// <summary>
        /// Searches for tweets that match the Twitter search phrase provided.
        /// Includes additional search options to narrow query results.
        /// </summary>
        /// <param name="phrase">The phrase.</param>
        /// <param name="options">The options.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-Search-API-Method%3A-search"/>
        public TwitterSearchResult SearchForTweets(string phrase, TwitterSearchOptions options)
        {
            return WithTweetSharp<TwitterSearchResult>(
                q => q.Search().Query().Containing(phrase)
                         .InLanguage(options.LanguageIso)
                         .InLocale(options.LocaleIso)
                         .Within(options.LocationRadiusMiles)
                         .Of(options.Location)
                );
        }

        /// <summary>
        /// Searches for tweets that match the Twitter search phrase provided.
        /// </summary>
        /// <param name="phrase">The phrase.</param>
        /// <param name="page">The expected page.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-Search-API-Method%3A-search"/>
        public TwitterSearchResult SearchForTweets(string phrase, int page)
        {
            return WithTweetSharp<TwitterSearchResult>(
                q => q.Search().Query().Containing(phrase).Skip(page)
                );
        }

        /// <summary>
        /// Searches for tweets that match the Twitter search phrase provided.
        /// Includes additional search options to narrow query results.
        /// </summary>
        /// <param name="phrase">The phrase.</param>
        /// <param name="page">The expected page.</param>
        /// <param name="options">The options.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-Search-API-Method%3A-search"/>
        public TwitterSearchResult SearchForTweets(string phrase, int page, TwitterSearchOptions options)
        {
            return WithTweetSharp<TwitterSearchResult>(
                q => q.Search().Query().Containing(phrase)
                         .Skip(page)
                         .InLanguage(options.LanguageIso)
                         .InLocale(options.LocaleIso)
                         .Within(options.LocationRadiusMiles)
                         .Of(options.Location)
                );
        }

        /// <summary>
        /// Searches for tweets that match the Twitter search phrase provided.
        /// </summary>
        /// <param name="phrase">The phrase.</param>
        /// <param name="page">The expected page.</param>
        /// <param name="count">The expected number of results.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-Search-API-Method%3A-search" />
        public TwitterSearchResult SearchForTweets(string phrase, int page, int count)
        {
            var query = Uri.EscapeDataString(phrase);
            return WithTweetSharp<TwitterSearchResult>(
                q => q.Search().Query().Containing(query)
                    .Skip(page).Take(count)
                );
        }

        /// <summary>
        /// Searches for tweets that match the Twitter search phrase provided.
        /// Includes additional search options to narrow query results.
        /// </summary>
        /// <param name="phrase">The phrase.</param>
        /// <param name="page">The expected page.</param>
        /// <param name="count">The expected number of results.</param>
        /// <param name="options">The options.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-Search-API-Method%3A-search"/>
        public TwitterSearchResult SearchForTweets(string phrase, int page, int count, TwitterSearchOptions options)
        {
            return WithTweetSharp<TwitterSearchResult>(
                q => q.Search().Query().Containing(phrase)
                         .Skip(page).Take(count)
                         .InLanguage(options.LanguageIso)
                         .InLocale(options.LocaleIso)
                         .Within(options.LocationRadiusMiles)
                         .Of(options.Location)
                );
        }

        /// <summary>
        /// Searches for tweets that match the Twitter search phrase provided.
        /// Limited to tweets after a given ID.
        /// </summary>
        /// <param name="sinceId">The since ID.</param>
        /// <param name="phrase">The phrase.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-Search-API-Method%3A-search"/>
        public TwitterSearchResult SearchForTweetsSince(long sinceId, string phrase)
        {
            return WithTweetSharp<TwitterSearchResult>(
                q => q.Search().Query().Containing(phrase).Since(sinceId)
                );
        }

        /// <summary>
        /// Searches for tweets that match the Twitter search phrase provided.
        /// Limited to tweets after a given ID.
        /// Includes additional search options to narrow query results.
        /// </summary>
        /// <param name="sinceId">The since ID.</param>
        /// <param name="phrase">The phrase.</param>
        /// <param name="options">The options.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-Search-API-Method%3A-search"/>
        public TwitterSearchResult SearchForTweetsSince(long sinceId, string phrase, TwitterSearchOptions options)
        {
            return WithTweetSharp<TwitterSearchResult>(
                q => q.Search().Query().Containing(phrase)
                         .Since(sinceId)
                         .InLanguage(options.LanguageIso)
                         .InLocale(options.LocaleIso)
                         .Within(options.LocationRadiusMiles)
                         .Of(options.Location)
                );
        }

        /// <summary>
        /// Searches for tweets that match the Twitter search phrase provided.
        /// Limited to tweets after a given ID.
        /// </summary>
        /// <param name="sinceId">The since ID.</param>
        /// <param name="phrase">The phrase.</param>
        /// <param name="page">The expected page.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-Search-API-Method%3A-search"/>
        public TwitterSearchResult SearchForTweetsSince(long sinceId, string phrase, int page)
        {
            return WithTweetSharp<TwitterSearchResult>(
                q => q.Search().Query().Containing(phrase)
                         .Since(sinceId).Skip(page)
                );
        }

        /// <summary>
        /// Searches for tweets that match the Twitter search phrase provided.
        /// Limited to tweets after a given ID.
        /// Includes additional search options to narrow query results.        
        /// </summary>
        /// <param name="sinceId">The since ID.</param>
        /// <param name="phrase">The phrase.</param>
        /// <param name="page">The expected page.</param>
        /// <param name="options">The options.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-Search-API-Method%3A-search"/>
        public TwitterSearchResult SearchForTweetsSince(long sinceId, string phrase, int page, TwitterSearchOptions options)
        {
            return WithTweetSharp<TwitterSearchResult>(
                q => q.Search().Query().Containing(phrase)
                         .Since(sinceId).Skip(page)
                         .InLanguage(options.LanguageIso)
                         .InLocale(options.LocaleIso)
                         .Within(options.LocationRadiusMiles)
                         .Of(options.Location)
                );
        }

        /// <summary>
        /// Searches for tweets that match the Twitter search phrase provided.
        /// Limited to tweets after a given ID.
        /// </summary>
        /// <param name="sinceId">The since ID.</param>
        /// <param name="phrase">The phrase.</param>
        /// <param name="page">The expected page.</param>
        /// <param name="count">The expected number of results.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-Search-API-Method%3A-search"/>
        public TwitterSearchResult SearchForTweetsSince(long sinceId, string phrase, int page, int count)
        {
            return WithTweetSharp<TwitterSearchResult>(
                q => q.Search().Query().Containing(phrase)
                         .Since(sinceId).Skip(page).Take(count)
                );
        }

        /// <summary>
        /// Searches for tweets that match the Twitter search phrase provided.
        /// Limited to tweets after a given ID.
        /// Includes additional search options to narrow query results.
        /// </summary>
        /// <param name="sinceId">The since ID.</param>
        /// <param name="phrase">The phrase.</param>
        /// <param name="page">The expected page.</param>
        /// <param name="count">The expected number of results.</param>
        /// <param name="options">The options.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-Search-API-Method%3A-search"/>
        public TwitterSearchResult SearchForTweetsSince(long sinceId, string phrase, int page, int count, TwitterSearchOptions options)
        {
            return WithTweetSharp<TwitterSearchResult>(
                q => q.Search().Query().Containing(phrase)
                         .Since(sinceId).Skip(page).Take(count)
                         .InLanguage(options.LanguageIso)
                         .InLocale(options.LocaleIso)
                         .Within(options.LocationRadiusMiles)
                         .Of(options.Location)
                );
        }

        /// <summary>
        /// Searches for tweets that match the Twitter search phrase provided.
        /// Limited to tweets before a given ID.
        /// </summary>
        /// <param name="maxId">The max ID.</param>
        /// <param name="phrase">The phrase.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-Search-API-Method%3A-search"/>
        public TwitterSearchResult SearchForTweetsBefore(long maxId, string phrase)
        {
            return WithTweetSharp<TwitterSearchResult>(
                q => q.Search().Query().Containing(phrase).Before(maxId)
                );
        }

        /// <summary>
        /// Searches for tweets that match the Twitter search phrase provided.
        /// Limited to tweets before a given ID.
        /// Includes additional search options to narrow query results.
        /// </summary>
        /// <param name="maxId">The max ID.</param>
        /// <param name="phrase">The phrase.</param>
        /// <param name="options">The options.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-Search-API-Method%3A-search"/>
        public TwitterSearchResult SearchForTweetsBefore(long maxId, string phrase, TwitterSearchOptions options)
        {
            return WithTweetSharp<TwitterSearchResult>(
                                                          q => q.Search().Query().Containing(phrase)
                                                                   .Before(maxId)
                                                                   .InLanguage(options.LanguageIso)
                                                                   .InLocale(options.LocaleIso)
                                                                   .Within(options.LocationRadiusMiles)
                                                                   .Of(options.Location)
                );
        }

        /// <summary>
        /// Searches for tweets that match the Twitter search phrase provided.
        /// Limited to tweets before a given ID.
        /// </summary>
        /// <param name="maxId">The max ID.</param>
        /// <param name="phrase">The phrase.</param>
        /// <param name="page">The expected page.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-Search-API-Method%3A-search"/>
        public TwitterSearchResult SearchForTweetsBefore(long maxId, string phrase, int page)
        {
            return WithTweetSharp<TwitterSearchResult>(
                q =>
                q.Search().Query().Containing(phrase).Before(maxId).Skip(page)
                );
        }

        /// <summary>
        /// Searches for tweets that match the Twitter search phrase provided.
        /// Limited to tweets before a given ID.
        /// Includes additional search options to narrow query results.
        /// </summary>
        /// <param name="maxId">The max ID.</param>
        /// <param name="phrase">The phrase.</param>
        /// <param name="page">The expected page.</param>
        /// <param name="options">The options.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-Search-API-Method%3A-search"/>
        public TwitterSearchResult SearchForTweetsBefore(long maxId, string phrase, int page, TwitterSearchOptions options)
        {
            return WithTweetSharp<TwitterSearchResult>(
                q => q.Search().Query().Containing(phrase)
                         .Before(maxId).Skip(page)
                         .InLanguage(options.LanguageIso)
                         .InLocale(options.LocaleIso)
                         .Within(options.LocationRadiusMiles)
                         .Of(options.Location)
                );
        }

        /// <summary>
        /// Searches for tweets that match the Twitter search phrase provided.
        /// Limited to tweets before a given ID.
        /// </summary>
        /// <param name="maxId">The max ID.</param>
        /// <param name="phrase">The phrase.</param>
        /// <param name="page">The expected page.</param>
        /// <param name="count">The expected number of results.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-Search-API-Method%3A-search"/>
        public TwitterSearchResult SearchForTweetsBefore(long maxId, string phrase, int page, int count)
        {
            return WithTweetSharp<TwitterSearchResult>(
                q => q.Search().Query().Containing(phrase).Before(maxId).Skip(page).Take(count)
                );
        }

        /// <summary>
        /// Searches for tweets that match the Twitter search phrase provided.
        /// Limited to tweets before a given ID.
        /// Includes additional search options to narrow query results.
        /// </summary>
        /// <param name="maxId">The max ID.</param>
        /// <param name="phrase">The phrase.</param>
        /// <param name="page">The expected page.</param>
        /// <param name="count">The expected number of results.</param>
        /// <param name="options">The options.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-Search-API-Method%3A-search"/>
        public TwitterSearchResult SearchForTweetsBefore(long maxId, string phrase, int page, int count, TwitterSearchOptions options)
        {
            return WithTweetSharp<TwitterSearchResult>(
                q => q.Search().Query().Containing(phrase)
                         .Before(maxId).Skip(page).Take(count)
                         .InLanguage(options.LanguageIso)
                         .InLocale(options.LocaleIso)
                         .Within(options.LocationRadiusMiles)
                         .Of(options.Location)
                );
        }

        #endregion

        #region Trends

        /// <summary>
        /// Returns the current top 10 trending topics on Twitter.
        /// </summary>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-Search-API-Method%3A-trends"/>
        public TwitterSearchTrends SearchCurrentTrends()
        {
            return WithTweetSharp<TwitterSearchTrends>(q => q.Search().Trends().Current());
        }

        /// <summary>
        /// Returns the current top 10 trending topics on Twitter,
        /// filtering on trends without hashtags.
        /// </summary>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-Search-API-Method%3A-trends"/>
        public TwitterSearchTrends SearchCurrentTrendsWithoutHashtags()
        {
            return WithTweetSharp<TwitterSearchTrends>(
                q => q.Search().Trends().Current().ExcludeHashtags()
                );
        }

        /// <summary>
        /// Returns the top 20 trending topics for each hour in a given day.
        /// </summary>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-Search-API-Method%3A-trends-daily"/>
        public TwitterSearchTrends SearchDailyTrends()
        {
            return WithTweetSharp<TwitterSearchTrends>(
                q => q.Search().Trends().Daily()
                );
        }

        /// <summary>
        /// Returns the top 20 trending topics for each hour in a given day.
        /// </summary>
        /// <param name="startDate">The start date.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-Search-API-Method%3A-trends-daily"/>
        public TwitterSearchTrends SearchDailyTrends(DateTime startDate)
        {
            return WithTweetSharp<TwitterSearchTrends>(
                q => q.Search().Trends().Daily().On(startDate)
                );
        }

        /// <summary>
        /// Returns the top 20 trending topics for each hour in a given day,
        /// filtering on trends without hashtags.
        /// </summary>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-Search-API-Method%3A-trends-daily"/>
        public TwitterSearchTrends SearchDailyTrendsWithoutHashtags()
        {
            return WithTweetSharp<TwitterSearchTrends>(
                q => q.Search().Trends().Daily().ExcludeHashtags()
                );
        }

        /// <summary>
        /// Returns the top 20 trending topics for each hour in a given day,
        /// filtering on trends without hashtags.
        /// </summary>
        /// <param name="startDate">The start date.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-Search-API-Method%3A-trends-daily"/>
        public TwitterSearchTrends SearchDailyTrendsWithoutHashtags(DateTime startDate)
        {
            return WithTweetSharp<TwitterSearchTrends>(
                q =>
                q.Search().Trends().Daily().On(startDate).ExcludeHashtags()
                );
        }

        /// <summary>
        /// Returns the top 30 trending topics for each day in a given week.
        /// </summary>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-Search-API-Method%3A-trends-weekly"/>
        public TwitterSearchTrends SearchWeeklyTrends()
        {
            return WithTweetSharp<TwitterSearchTrends>(
                q => q.Search().Trends().Weekly()
                );
        }

        /// <summary>
        /// Returns the top 30 trending topics for each day in a given week.
        /// </summary>
        /// <param name="startDate">The start date.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-Search-API-Method%3A-trends-weekly"/>
        public TwitterSearchTrends SearchWeeklyTrends(DateTime startDate)
        {
            return WithTweetSharp<TwitterSearchTrends>(
                q => q.Search().Trends().Weekly().On(startDate)
                );
        }

        /// <summary>
        /// Returns the top 30 trending topics for each day in a given week,
        /// filtering on trends with hashtags.
        /// </summary>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-Search-API-Method%3A-trends-weekly"/>
        public TwitterSearchTrends SearchWeeklyTrendsWithoutHashtags()
        {
            return WithTweetSharp<TwitterSearchTrends>(
                q => q.Search().Trends().Weekly()
                );
        }

        /// <summary>
        /// Returns the top 30 trending topics for each day in a given week,
        /// filtering on trends with hashtags.
        /// </summary>
        /// <param name="startDate">The start date.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-Search-API-Method%3A-trends-weekly"/>
        public TwitterSearchTrends SearchWeeklyTrendsWithoutHashtags(DateTime startDate)
        {
            return WithTweetSharp<TwitterSearchTrends>(
                q =>
                q.Search().Trends().Weekly().On(startDate).ExcludeHashtags()
                );
        }

        #endregion
#endif
    }
}