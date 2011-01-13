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
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using TweetSharp.Extensions;
using NUnit.Framework;
using TweetSharp.Twitter.Fluent;
using TweetSharp.Twitter.Model;
using TweetSharp.Twitter.Extensions;

namespace TweetSharp.Twitter.UnitTests.Fluent
{
    partial class FluentTwitterTests
    {
        [Test]
        [Category("Search")]
        public void Can_search_for_phrase()
        {
            // build your query 
            var search = FluentTwitter.CreateRequest()
                .Search().Query()
                .Containing(TWITTER_USERNAME)
                .AsJson();

            Console.WriteLine(search.ToString());

            // fetch the results from twitter
            var response = search.Request();
            IgnoreFailWhales(response);

            // cast the result into a friendly data class
            var results = response.AsSearchResult();

            // work with objects
            foreach (var result in results.Statuses)
            {
                Console.WriteLine(result.Text);
            }
        }

        [Test]
        [Category("Search")]
        public void Can_search_for_messages_from_user()
        {
            var search = FluentTwitter.CreateRequest()
                .Search().Query().FromUser(TWITTER_USERNAME);

            Console.WriteLine(search.ToString());

            var response = search.Request();
            IgnoreFailWhales(response);

            var results = response.AsSearchResult();
            Assert.IsNotNull(results);
            Assert.IsTrue(results.Statuses.Count > 0, "Query returned, but with no results");
        }

        [Test]
        [Category("Search")]
        public void Can_search_for_hash_tag_and_receive_custom_class()
        {
            var twitter = FluentTwitter.CreateRequest()
                .Search().Query().ContainingHashTag("nyc")
                .Take(100).AsJson();

            var response = twitter.Request();
            IgnoreFailWhales(response);

            var results = response.AsSearchResult();
            Assert.IsTrue(results.Statuses.Count > 0, "Query returned, but with no results");
        }

        [Test]
        [Category("Search")]
        public void Can_search_for_messages_to_user()
        {
            var search = FluentTwitter.CreateRequest()
                .Search().Query().ToUser(TWITTER_CELEBRITY_SCREEN_NAME)
                .AsJson();

            Console.WriteLine(search.ToString());

            var response = search.Request();
            IgnoreFailWhales(response);

            var results = response.AsSearchResult();
            Assert.IsTrue(results.Statuses.Count > 0, "Query returned, but with no results");
        }

        [Test]
        [Category("Search")]
        public void Can_search_for_hash_tag()
        {
            var search = FluentTwitter.CreateRequest()
                .Search().Query().ContainingHashTag("nyc")
                .AsJson();

            Console.WriteLine(search.ToString());

            var response = search.Request();
            IgnoreFailWhales(response);

            var results = response.AsSearchResult();
            Assert.IsTrue(results.Statuses.Count > 0, "Query returned, but with no results");
        }

        [Test]
        [Category("Search")]
        public void Can_search_for_language()
        {
            var search = FluentTwitter.CreateRequest()
                .Search().Query().Containing("twitter")
                .InLanguage("fr")
                .AsJson();

            Console.WriteLine(search.ToString());

            var response = search.Request();
            IgnoreFailWhales(response);

            var results = response.AsSearchResult();
            Assert.IsTrue(results.Statuses.Count > 0, "Query returned, but with no results");
        }

        [Test]
        [Category("Search")]
        public void Can_search_for_geocode()
        {
            var search = FluentTwitter.CreateRequest()
                .Search().Query().Within(100).Of(37.329492, -122.03167);

            Console.WriteLine(search.ToString());

            var response = search.Request();
            IgnoreFailWhales(response);

            var results = response.AsSearchResult();
            Assert.IsTrue(results.Statuses.Count > 0, "Query returned, but with no results");
        }

#if !Smartphone
        [Test]
        [Category("Search")]
        public void Can_search_for_geocode_using_culture_that_uses_commas_for_decimals()
        {
            var previousCulture = Thread.CurrentThread.CurrentCulture; 
            Thread.CurrentThread.CurrentCulture = new CultureInfo("de-DE");
            var s = string.Format("{0}", 25.25F); 
            Assert.AreEqual(s, "25,25");
            Can_search_for_geocode();
            Thread.CurrentThread.CurrentCulture = previousCulture;
        }
#endif

        [Test]
        [Category("Search")]
        public void Should_distinguish_operators_from_parameters()
        {
            var search = FluentTwitter.CreateRequest()
                .Search().Query().Containing("twitter")
                .InLanguage("en").AsJson();

            Console.WriteLine(search.ToString());

            var response = search.Request();
            IgnoreFailWhales(response);

            var results = response.AsSearchResult();
            Assert.IsTrue(results.Statuses.Count > 0, "Query returned, but with no results");
        }

        [Test]
        [Category("Search")]
        public void Should_distinguish_operators_from_parameters_in_complex_query()
        {
            var search = FluentTwitter.CreateRequest()
                .Search().Query().Containing("twitter")
                .SinceUntil(2.Days().Ago()) // operator
                .ContainingLinks() // operator
                .Take(5) // parameter
                .InLanguage("en") // parameter
                .AsJson();

            Console.WriteLine(search.ToString());
            Console.WriteLine(search.AsUrl());

            var response = search.Request();
            IgnoreFailWhales(response);

            var results = response.AsSearchResult();
            Assert.IsNotNull(results);
        }

        [Test]
        [Category("Search")]
        public void Can_search_and_create_data_class()
        {
            var search = FluentTwitter.CreateRequest()
                .Search().Query().Containing("twitter").InLanguage("en")
                .AsJson();

            Console.WriteLine(search.ToString());

            var response = search.Request();
            IgnoreFailWhales(response);

            var results = response.AsSearchResult();
            Assert.IsTrue(results.Statuses.Count > 0, "Query returned, but with no results");
        }

        [Test]
        [Category("Search")]
        public void Can_search_with_geo_code_correctly()
        {
            var search = FluentTwitter.CreateRequest()
                .Search().Query()
                .Containing("vegas")
                .Take(10)
                .Within(90).Of(36.16, -115.14)
                .AsJson();

            var response = search.Request();

            Console.WriteLine(search.AsUrl());
            IgnoreFailWhales(response);

            var result = response.AsSearchResult();
            Assert.IsTrue(result.Statuses.Count > 0);
            foreach (var status in result.Statuses)
            {
                Console.WriteLine(status.Text);
            }
        }

        [Test]
        [Category("Search")]
        public void Can_search_with_since_id()
        {
            var search = FluentTwitter.CreateRequest()
                .Search().Query().Containing("twitter")
                .Since(12345678);

            var response = search.Request();
            IgnoreFailWhales(response);
        }

        [Test]
        [Category("Search")]
        public void Can_implictly_cast_from_search_status_to_rough_status()
        {
            var search = FluentTwitter.CreateRequest()
                .Search().Query().Containing("twitter")
                .Since(12345678);

            var results = search.Request().AsSearchResult();
            foreach (var result in results.Statuses)
            {
                // implicit cast
                TwitterStatus status = result;
                Assert.IsNotNull(status);
            }
        }

        [Test]
        [Category("Search")]
        public void Can_search_and_return_source()
        {
            var search = FluentTwitter.CreateRequest()
                .Search().Query().Containing("twitter")
                .AsJson().Request();

            var result = search.AsSearchResult();
            foreach (var status in result.Statuses)
            {
                Assert.IsNotNull(status.Source);
                Console.WriteLine(HttpUtility.UrlDecode(status.Source));
            }
        }

#if !Smartphone
        [Test]
        [Category("Search")]
        public void Can_page_search_results()
        {
            const int tweetsPerPage = 100;
            var results = new HashSet<TwitterSearchStatus>();
            var ceiling = Math.Ceiling((double)1500m / tweetsPerPage);
            
            // Twitter API allows up to 1500 total results
            for (var i = 1; i <= ceiling; i++)
            {
                var search = FluentTwitter.CreateRequest()
                    .Search().Query().Containing("twitter")
                    .Take(tweetsPerPage) // you define the tweets per page 
                    .Skip(i) // the current page number, 1-based
                    .Since(DateTime.Now - 1.Day() )
                    .AsJson();

                var response = search.Request();

                // contains meta-data about the search result
                var searchResults = response.AsSearchResult();

                foreach (var status in searchResults.Statuses)
                {
                    results.Add(status);
                }
            }

            Console.WriteLine(results.Count + " unique results found while paging");
        }
#endif

        [Test]
        [Category("Search")]
        public void Can_set_user_agent_from_client_info()
        {
            var search = FluentTwitter.CreateRequest()
                .Search().Query().Containing("twitter")
                .AsJson();

            search.Request();
        }

#if !Mono
        [Test]
        [Category("Search")]
        public void Can_get_trends()
        {
            var search = FluentTwitter.CreateRequest()
                .Search().Trends()
                .AsJson();

            Console.WriteLine(search.AsUrl());

            var response = search.Request();
            IgnoreFailWhales(response);

            var trends = response.AsSearchTrends();
            Assert.IsNotNull(trends);
            Console.WriteLine(response);
        }

        [Test]
        [Category("Search")]
        public void Can_get_current_trends()
        {
            var search = FluentTwitter.CreateRequest()
                .Search().Trends().Current()
                .AsJson();

            Console.WriteLine(search.AsUrl());

            var response = search.Request();
            IgnoreFailWhales(response);

            var trends = response.AsSearchTrends();
            Assert.IsNotNull(trends);
            Console.WriteLine(response);
        }

        [Test]
        [Category("Search")]
        public void Can_get_daily_trends()
        {
            var search = FluentTwitter.CreateRequest()
                .Search().Trends().Daily()
                .AsJson();

            Console.WriteLine(search.AsUrl());

            var response = search.Request();
            IgnoreFailWhales(response);

            var trends = response.AsSearchTrends();
            Assert.IsNotNull(trends);
            Assert.IsTrue(trends.Trends.Count > 20);

            Assert.AreNotEqual(trends.SearchDate, DateTime.MinValue);
            var dates = (from t in trends.Trends select t.TrendingAsOf).Distinct();
            Assert.IsTrue(dates.Count() > 1);

            Console.WriteLine(response);
        }

        [Test]
        [Category("Search")]
        public void Can_get_weekly_trends()
        {
            var search = FluentTwitter.CreateRequest()
                .Search().Trends().Weekly()
                .AsJson();

            Console.WriteLine(search.AsUrl());

            var response = search.Request();
            IgnoreFailWhales(response);

            var trends = response.AsSearchTrends();
            Assert.IsNotNull(trends);
            Console.WriteLine(response);
        }


        [Test]
        [Category("Search")]
        public void Can_get_historic_weekly_trends()
        {
            var search = FluentTwitter.CreateRequest()
                .Search().Trends().Weekly().On(DateTime.Now - 5.Days())
                .AsJson();

            Console.WriteLine(search.AsUrl());

            var response = search.Request();
            IgnoreFailWhales(response);

            var trends = response.AsSearchTrends();
            Assert.IsNotNull(trends);

            Assert.AreNotEqual(trends.SearchDate, DateTime.MinValue);
            var dates = (from t in trends.Trends select t.TrendingAsOf).Distinct();
            Assert.IsTrue(dates.Count() > 1);


            Console.WriteLine(response);
        }

        [Test]
        [Category("Search")]
        public void Can_get_historic_daily_trends()
        {
            var search = FluentTwitter.CreateRequest()
                .Search().Trends().Daily().On(DateTime.Now - 3.Days())
                .AsJson();

            Console.WriteLine(search.AsUrl());

            var response = search.Request();
            IgnoreFailWhales(response);

            var trends = response.AsSearchTrends();
            Assert.IsNotNull(trends);

            Assert.AreNotEqual(trends.SearchDate, DateTime.MinValue);
            var dates = (from t in trends.Trends select t.TrendingAsOf).Distinct();
            Assert.IsTrue(dates.Count() > 1);


            Console.WriteLine(response);
        }

        [Test]
        [Category("Search")]
        public void Can_get_historic_daily_trends_without_hashtags()
        {
            var search = FluentTwitter.CreateRequest()
                .Search().Trends().Daily().On(DateTime.Now - 1.Days())
                .ExcludeHashtags()
                .AsJson();

            Console.WriteLine(search.AsUrl());

            var response = search.Request();
            IgnoreFailWhales(response);

            var trends = response.AsSearchTrends();
            Assert.IsNotNull(trends);

            Assert.AreNotEqual(trends.SearchDate, DateTime.MinValue);
            var dates = (from t in trends.Trends select t.TrendingAsOf).Distinct();
            Assert.IsTrue(dates.Count() > 1);

            Console.WriteLine(response);
        }
#endif

        [Test]
        [Category("Search")]
        public void Can_search_with_result_type()
        {
            var search = FluentTwitter.CreateRequest()
                .Search().Query().Containing("twitter")
                .WithResultType(SearchResultType.Popular);

            var response = search.Request();
            IgnoreFailWhales(response);

            var results = response.AsSearchResult();
            Assert.IsNotNull(results);
        }

        [Test]
        public void Temp_Test()
        {
            var urls = new List<Uri>();
            var max = 1500;
            var page_size = 100;

            for (int i = 0; i < max; i += page_size)
            {
                var pg = i/page_size + 1; 
                var search = FluentTwitter.CreateRequest()
                    .Search().Query().Containing("Bieber")
                    .Take(page_size).Skip(pg);

                var response = search.Request();
                var result = response.AsSearchResult();
                foreach (var t in result.Statuses)
                {
                    foreach (var link in t.TextLinks)
                    {
                        if (!urls.Contains(link))
                        {
                            urls.Add(link);
                            Console.WriteLine(link);
                        }
                    }
                }
            }
        }
    }
}