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
using NUnit.Framework;
using TweetSharp.Twitter.Extensions;
using TweetSharp.Twitter.Fluent;
using TweetSharp.Twitter.Model;

namespace TweetSharp.Twitter.UnitTests.Fluent
{
    partial class FluentTwitterTests
    {
        private TwitterLocalTrends QueryLocalTrends(int woeId)
        {
            var query = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .Trends().ByLocation(woeId);

            Console.WriteLine(query.AsUrl());

            var result = query.Request();
            IgnoreFailWhales(result);

            var trends = result.AsLocalTrends();
            Assert.IsNotNull(trends);

            return trends;
        }

        [Test]
        [Category("Trends")]
        public void Can_get_available_local_trends()
        {
            var query = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .Trends().GetAvailable();

            var result = query.Request();

            Console.WriteLine(query.AsUrl());
            IgnoreFailWhales(result);

            var locations = result.AsWhereOnEarthLocations();
            Assert.IsNotNull(locations);
            foreach (var location in locations)
            {
                Console.WriteLine("{0}, {1}", location.Name, location.Country);
            }
        }

        [Test]
        [Category("Trends")]
        public void Can_get_available_local_trends_ordered_by_location()
        {
            var sanFrancisco = new TwitterGeoLocation(37.78029, -122.39697);

            var query = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .Trends().GetAvailable().OrderBy(sanFrancisco);

            Console.WriteLine(query.AsUrl());

            var result = query.Request();

            IgnoreFailWhales(result);

            var locations = result.AsWhereOnEarthLocations();
            Assert.IsNotNull(locations);
            foreach (var location in locations)
            {
                Console.WriteLine("#{0}: {1},{2}", location.WoeId, location.Name, location.Country);
            }
        }

        [Test]
        [Category("Trends")]
        public void Can_get_available_local_trends_ordered_by_location_xml()
        {
            var sanFrancisco = new TwitterGeoLocation(37.78029, -122.39697);

            var query = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .Trends().GetAvailable().OrderBy(sanFrancisco)
                .AsXml();

            Console.WriteLine(query.AsUrl());

            var result = query.Request();

            IgnoreFailWhales(result);

            var locations = result.AsWhereOnEarthLocations();
            Assert.IsNotNull(locations);
            foreach (var location in locations)
            {
                Console.WriteLine("{0},{1}", location.Name, location.Country);
            }
        }

        [Test]
        [Category("Trends")]
        public void Can_get_available_local_trends_xml()
        {
            var query = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .Trends().GetAvailable()
                .AsXml();

            var result = query.Request();

            Console.WriteLine(query.AsUrl());
            IgnoreFailWhales(result);

            var locations = result.AsWhereOnEarthLocations();
            Assert.IsNotNull(locations);
            foreach (var location in locations)
            {
                Console.WriteLine("#{0}: {1},{2}", location.WoeId, location.Name, location.Country);
            }
        }

        [Test]
        [Category("Trends")]
        public void Can_get_global_trends()
        {
            const int _global = 1;

            var trends = QueryLocalTrends(_global);

            Assert.IsNotNull(trends);

            foreach (var trend in trends.Trends)
            {
                Console.WriteLine(trend.Name);
            }
        }

        [Test]
        [Category("Trends")]
        public void Can_get_local_trends_from_woeid()
        {
            const int _sanFrancisco = 2487956;

            var trends = QueryLocalTrends(_sanFrancisco);

            Assert.IsNotNull(trends);

            foreach (var trend in trends.Trends)
            {
                Console.WriteLine(trend.Name);
            }
        }

        [Test]
        [Category("Trends")]
        public void Can_get_local_trends_with_xml()
        {
            const int _sanFrancisco = 2487956;

            var query = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .Trends().ByLocation(_sanFrancisco)
                .AsXml();

            Console.WriteLine(query.AsUrl());

            var result = query.Request();
            IgnoreFailWhales(result);

            var trends = result.AsLocalTrends();
            Assert.IsNotNull(trends);

            foreach (var trend in trends.Trends)
            {
                // [DC] Names go missing if you use XML; schemas don't match
                Console.WriteLine(trend.Query);
            }
        }

        [Test]
        [Category("Trends")]
        public void Can_request_trends_async_without_blocking()
        {
            var blocking = false;
            var twitter = FluentTwitter.CreateRequest()
                .Configuration.UseGzipCompression()
                .Search().Trends().CallbackTo((s, r, u) =>
                {
                    blocking = true; // Should not hit this if not blocking
                });

            var asyncResult = twitter.BeginRequest();
            Assert.IsFalse(blocking);
            var finished = asyncResult.AsyncWaitHandle.WaitOne(10000, true);
            Assert.IsTrue(finished, "Timed out waiting for async wait handle");
           
        }
    }
}