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
using System.Diagnostics;
using System.Linq;
using System.Threading;
using Hammock.Caching;
using TweetSharp.Extensions;
using TweetSharp.Fluent;
using NUnit.Framework;
using Hammock.Tasks;
using TweetSharp.Twitter.Fluent;
using TweetSharp.Twitter.Model;
using TweetSharp.Twitter.Extensions;

namespace TweetSharp.Twitter.UnitTests.Fluent
{
    partial class FluentTwitterTests
    {
        [Test]
        [Category("Configuration")]
        [Ignore("Rate limiting is not implemented yet.")]
        public void Can_limit_queries_to_current_rates()
        {
            Assert.Ignore("Rate limiting is not implemented yet.");
        }

#if !Smartphone
        [Test]
        [Category("Configuration")]
        public void Can_request_http_get_with_absolute_expiry_caching()
        {
            var twitter = FluentTwitter.CreateRequest()
                .Configuration.CacheUntil(5.Seconds().FromNow())
                .Statuses().OnPublicTimeline()
                .AsJson();

            // should come from the web
            var uncached = twitter.Request();
            Assert.IsNotNull(uncached);
            Assert.IsNotNull(uncached.WebResponse, "Request was expected to come from web");
                               
            var users = uncached.AsStatuses();
            Assert.IsNotNull(users);

            // should come from cache
            var cached = twitter.Request();
            Assert.That(cached.IsFromCache, "Request was expected to come from cache");

            // expire the cache
            Thread.Sleep((int)6.Seconds().TotalMilliseconds);

            // should come from the web
            var expired = twitter.Request();
            Assert.IsFalse(expired.IsFromCache, "Cache should have expired, but didn't");
            Assert.IsNotNull(expired.WebResponse);
        }

        [Test]
        [Category("Configuration")]
        public void Can_request_http_get_with_sliding_expiry_caching()
        {
            var twitter = FluentTwitter.CreateRequest()
                .Configuration.CacheForInactivityOf(2.Seconds())
                .Statuses().OnPublicTimeline()
                .AsJson();

            // should come from the web
            var response = twitter.Request();
            IgnoreFailWhales(response);
            Assert.IsNotNull(response.WebResponse);

            var users = response.AsStatuses();
            Assert.IsNotNull(users);

            // should come from cache
            var cached = twitter.Request();
            Assert.IsNotNull(cached);
            Assert.That(cached.IsFromCache);

            // expire the cache
            Thread.Sleep((int)3.Seconds().TotalMilliseconds);

            // should come from the web
            var uncached = twitter.Request();
            Assert.IsNotNull(uncached);
            Assert.IsFalse(uncached.IsFromCache, "Cache should have expired, but didn't");
        }

        [Test]
        [Category("Configuration")]
        public void Can_request_http_get_with_sliding_expiry_caching_async()
        {
            var twitter = FluentTwitter.CreateRequest()
                .Configuration.UseGzipCompression()
                .Configuration.UseTransparentProxy("http://twitter.com")
                .Configuration.CacheForInactivityOf(20.Seconds())
                .Statuses().OnPublicTimeline().AsJson()
                .CallbackTo((s, r, u) =>
                                {

                                    Assert.IsNotNull(r.Response,
                                                     "Response was supposed to be valid but was null");
                                    var statuses = r.AsStatuses();
                                    Assert.IsNotNull(statuses,
                                                     string.Format("Could not parse JSON to user collection: {0}",
                                                                   r.Response));
                                });


            var asyncResult = twitter.BeginRequest();
            var result = twitter.EndRequest(asyncResult);

            Assert.IsNotNull(result.WebResponse, "Cache should be empty, but wasn't, or response was in error.");
            Console.WriteLine(twitter.ToString());

            // same query, different operation mode
            var response = twitter.Request();
            Assert.IsNotNull(response, "Response to query was null");
            Assert.That(response.IsFromCache, "Cache should be primed but was empty");

            // should come from cache
            var cached = twitter.Request();
            Assert.IsNotNull(cached, "Response to query was null");
            Assert.That(response.IsFromCache, "Cache should contain async query but doesn't");
        }

        [Test]
        [Category("Configuration")]
        public void Can_request_http_post_with_sliding_expiry_caching()
        {
            var location = default(string);

            try
            {
                var user = FluentTwitter.CreateRequest()
                    .Users().ShowProfileFor(TWITTER_USERNAME).AsJson()
                    .Request().AsUser();

                Assert.IsNotNull(user, "Failed to retrieve a Twitter user for this test");

                location = user.Location;

                var updateLocation = FluentTwitter.CreateRequest()
                   .Configuration.CacheForInactivityOf(20.Seconds())
                   .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                   .Account().UpdateProfile().UpdateLocation("Transylvania")
                   .AsJson();
                
                // should come from web
                var response = updateLocation.Request();
                Assert.IsNotNull(response, "Query returned null results");
                Assert.IsFalse(response.IsFromCache, "Cache should be empty, but wasn't, or response was in error.");

                // should come from cache
                var cached = updateLocation.Request();
                Assert.IsNotNull(cached);
                Assert.That(cached.IsFromCache, "Cache should be primed, but was empty");
            }
            finally
            {
                if (!String.IsNullOrEmpty(location))
                {
                    FluentTwitter.CreateRequest()
                        .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                        .Account().UpdateProfile().UpdateLocation(location)
                        .AsJson().Request();
                }
            }
        }
#endif

        [Test]
        [Category("Configuration")]
        public void Can_request_public_timeline_with_simple_caching()
        {
            var twitter = FluentTwitter.CreateRequest()
                .Configuration.CacheWith(CacheFactory.InMemoryCache)
                .Statuses().OnPublicTimeline()
                .AsJson();

            Console.WriteLine(twitter.ToString());

            var response = twitter.Request();
            IgnoreFailWhales(response);
            Assert.IsNotNull(response.WebResponse); // came from the web

            var users = response.AsStatuses();
            Assert.IsNotNull(users);

            var cached = twitter.Request();
            Assert.IsNotNull(cached);
            Assert.That(cached.IsFromCache); // came from the cache
        }

#if !ClientProfiles
        [Test]
        [Category("Configuration")]
        public void Can_truncate_statuses_with_word_splitting()
        {
            const string text = "123456789012345678901234567890123456789012345678901234567890" +
                                "123456789012345678901234567890123456789012345678901234567890" +
                                "1234567890123456789012345678901 3456789012345678901234567890" +
                                "123456789012345678901234567890123456789012345678901234567890" +
                                "123456789012345678901234567890123456789012345678901234567890";

            var twitter = FluentTwitter.CreateRequest()
                .Configuration.UseUpdateTruncation()
                .Statuses().Update(text)
                .AsJson();

            ((FluentTwitter) twitter.Root).ValidateUpdateText();
            Assert.AreEqual(140, twitter.Root.Parameters.Text.Length);
        }
#endif

        [Test]
        [Category("Configuration")]
        public void Can_declare_web_proxy_and_pass_authentication_info()
        {
            var twitter = FluentTwitter.CreateRequest()
                .Configuration.UseProxy("http://twitter.com")
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .Users().GetFollowers()
                .AsJson();

            twitter.Request();
        }

        [Test]
        [Category("Configuration")]
        public void Can_set_client_info_globally()
        {
            var clientInfo = new TwitterClientInfo
                                 {
                                     ClientName = "sillyapp",
                                     ClientUrl = "http://sillyapp.com",
                                     ClientVersion = "1.0.0.0"
                                 };

            FluentBase<TwitterResult>.SetClientInfo(clientInfo);

            var query = FluentTwitter.CreateRequest()
                .Statuses().OnPublicTimeline().AsJson();

            Assert.AreEqual(clientInfo, query.Root.ClientInfo);
        }

        [Test]
        [Category("Configuration")]
        public void Can_use_compression_for_get_requests()
        {
            var twitter = FluentTwitter.CreateRequest()
                .Configuration.UseGzipCompression()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .Statuses().OnFriendsTimeline().Take(200)
                .AsJson();

            var response = twitter.Request();
            Assert.IsNotNull(response, "REST response was null");
            Assert.IsNotNull(response.WebResponse, "Internal WebResponse was null");

            var headers = response.WebResponse.Headers;
            var encoding = headers["ETag"];
            Assert.IsTrue(encoding.Contains("gzip"), "ETag did not reference a gzip'd entity");
        }

        [Test]
        [Category("Configuration")]
        public void Can_avoid_compression_for_get_requests()
        {
            var twitter = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .Statuses().OnUserTimeline()
                .AsJson();

            var response = twitter.Request();
            IgnoreFailWhales(response);
            Assert.IsNotNull(response.WebResponse);

            var headers = response.WebResponse.Headers;
            var encoding = headers["ETag"];
            Assert.IsTrue(!encoding.Contains("gzip"), "ETag referenced a gzip'd entity");
        }

        [Test]
        [Category("Configuration")]
        public void Can_use_compression_for_post_requests()
        {
            if (!ALLOW_POSTS)
            {
                Assert.Ignore("This test posts a status to test compression");
            }
            var twitter = FluentTwitter.CreateRequest()
                .Configuration.UseGzipCompression()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .Statuses().Update("testing compression with posts at " + DateTime.Now.ToShortTimeString())
                .AsJson();

            var response = twitter.Request();
            IgnoreFailWhales(response);
            Assert.IsNotNull(response.WebResponse);

            var headers = response.WebResponse.Headers;
            var encoding = headers["ETag"];
            Assert.IsTrue(encoding.Contains("gzip"), "ETag did not reference a gzip'd entity");
        }

        [Test]
        [Category("Configuration")]
        public void Can_use_transparent_proxy_with_get()
        {
            Assert.IsFalse(
                              string.IsNullOrEmpty(TWITTER_TRANSPARENT_PROXY),
                              "Specify a value for 'proxy' in setup.xml to run this test");

            var twitter = FluentTwitter.CreateRequest()
                .Configuration.UseTransparentProxy(TWITTER_TRANSPARENT_PROXY)
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .Users().GetFollowers()
                .AsJson();

            var response = twitter.Request();
            IgnoreFailWhales(response);

            var followers = response.AsUsers();
            Assert.IsNotNull(followers);
        }

        [Test]
        [Category("Configuration")]
        public void Can_use_transparent_proxy_with_post()
        {
            if (!ALLOW_POSTS)
            {
                Assert.Ignore("This test makes a live status update");
            }

            if (string.IsNullOrEmpty(TWITTER_TRANSPARENT_PROXY))
            {
                Assert.Ignore("Specify a value for 'proxy' in setup.xml to run this test");
            }

            var twitter = FluentTwitter.CreateRequest()
                .Configuration.UseTransparentProxy(TWITTER_TRANSPARENT_PROXY)
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .Statuses().Update("testing posting via proxy");

            var response = twitter.Request();
            IgnoreFailWhales(response);
        }

        [Test]
        [Category("Configuration")]
        public void Can_use_https_with_get()
        {
            var twitter = FluentTwitter.CreateRequest()
                .Configuration.UseHttps()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .Statuses().OnHomeTimeline();

            var url = twitter.AsUrl(); 
            Assert.That(url.StartsWith("https"));

            var response = twitter.Request();
            Assert.That(response.RequestUri.Scheme == "https");
            IgnoreFailWhales(response);
        }

        [Test]
        [Category("Configuration")]
        public void Can_use_https_with_post()
        {
            if (!ALLOW_POSTS)
            {
                Assert.Ignore("This test makes a live status update");
            }

            var twitter = FluentTwitter.CreateRequest()
                .Configuration.UseHttps()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .Statuses().Update("testing posting via ssl");

            var url = twitter.AsUrl();
            Assert.That(url.StartsWith("https"));

            var response = twitter.Request();
            Assert.That(response.ResponseUri.Scheme == "https");
            IgnoreFailWhales(response);
        }

        [Test]
        [Category("Configuration")]
        public void Can_use_transparent_proxy_out_of_silverlight()
        {
            var info = new TwitterClientInfo();
            info.TransparentProxy = "http://identi.ca/api/";

            var identica = FluentTwitter.CreateRequest(info)
                .Statuses().OnPublicTimeline();

            var response = identica.Request();
            var statuses = response.AsStatuses();
            Assert.IsNotNull(statuses);
            Assert.IsTrue(statuses.Count() > 0);
            Trace.WriteLine(response);
        }
    }
}