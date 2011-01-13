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
using Hammock.Caching;
using TweetSharp.Extensions;
using TweetSharp.UnitTests.Base;
using NUnit.Framework;
using TweetSharp.Twitter.Fluent;
using TweetSharp.Twitter.Extensions;

namespace TweetSharp.Twitter.UnitTests.Fluent
{
    [TestFixture(
        Description = "You need to explicitly provide values for the constants used in Twitter communication tests")]
    public partial class FluentTwitterTests : TwitterTestBase
    {
        [Test]
        [Category("Requests")]
        public void Can_request_public_timeline_async()
        {
            var twitter = FluentTwitter.CreateRequest()
                .Statuses().OnPublicTimeline()
                .AsJson()
                .CallbackTo((s, r, u) =>
                                {
                                    Assert.IsNotNull(r.Response);
                                    var statuses = r.AsStatuses();
                                    Assert.IsNotNull(statuses);
                                });

            var asyncResult = twitter.BeginRequest();
            var finished = asyncResult.AsyncWaitHandle.WaitOne((int)10.Seconds().TotalMilliseconds, true);
            Assert.IsTrue(finished, "Timed out waiting for async wait handle");

            Console.WriteLine(twitter.ToString());
        }

        [Test]
        [Category("Requests")]
        public void Can_fail_request_with_friendly_error_class_and_get()
        {
            var twitter = FluentTwitter.CreateRequest()
                .Statuses().OnUserTimeline()
                .AsJson();

            var response = twitter.Request();
            IgnoreFailWhales(response);

            // Option 1: Inspect result
            var hasError = response.IsTwitterError;
            Assert.IsTrue(hasError, "Request did not result in a 401 Unauthorized");
            
            // Option 2: Cast an error
            var error = response.AsError();
            Assert.IsNotNull(error, "Error was identified but could not be cast.");

            Console.WriteLine("{0}: '{1}'", error.Request, error.ErrorMessage);
        }

        [Test]
        [Category("Requests")]
        public void Can_fail_request_with_friendly_error_class_and_post()
        {
            var twitter = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .DirectMessages()
                .Destroy(456)
                .AsJson();

            var response = twitter.Request();
            IgnoreFailWhales(response);

            // Option 1: Inspect result
            var hasError = response.IsTwitterError;
            Assert.IsTrue(hasError, "Request did not result in a 401 Unauthorized");

            // Option 2: Cast an error
            var error = response.AsError();
            
            if(error == null)
            {
                // Option 3: The server returned the new, unannounced
            }
            Assert.IsNotNull(error, "Error was identified but could not be cast.");



            Console.WriteLine("{0}: '{1}'", error.Request, error.ErrorMessage);
        }

        [Test]
        [Category("Requests")]
        public void Can_make_direct_request()
        {
            var twitter = FluentTwitter.CreateRequest()
                .Direct("/users/show/bob.xml");

            Console.WriteLine(twitter.AsUrl());

            var response = twitter.Request();

            IgnoreFailWhales(response);
            Assert.IsNotNull(response.AsUser());
        }

        [Test]
        [Category("Requests")]
        public void Can_make_direct_request_with_query_string()
        {
            if (!ALLOW_POSTS)
            {
                Assert.Ignore("This test makes a live status update");
            }
            var twitter = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .Direct("/statuses/update.xml?status=It's time to test direct URLs at" + DateTime.Now.ToShortTimeString())
                .Post();

            Console.WriteLine(twitter.AsUrl());

            var response = twitter.Request();

            IgnoreFailWhales(response);
            Assert.IsNotNull(response.AsStatus());
        }

        [Test]
        [Category("Requests")]
        public void Can_get_exception_info_from_request_that_fails()
        {
            if (string.IsNullOrEmpty(TWITTER_FAILWHALE_PROXY))
            {
                Assert.Ignore("No failwhale server defined in setup file.");
            }
            var twitter = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .Configuration.UseTransparentProxy(TWITTER_FAILWHALE_PROXY)
                .Account().VerifyCredentials();

            Console.WriteLine(twitter.AsUrl());

            var response = twitter.Request();
            IgnoreFailWhales(response);
            Assert.IsNotNull(response.Exception, "The response object's Exception property should have a value");
        }

        [Test]
        [Category("Requests")]
        public void Can_get_exception_info_from_delete_request_that_fails()
        {
            if (string.IsNullOrEmpty(TWITTER_FAILWHALE_PROXY))
            {
                Assert.Ignore("No failwhale server defined in setup file.");
            }
            var twitter = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .Configuration.UseTransparentProxy(TWITTER_FAILWHALE_PROXY)
                .Lists().DeleteList(TWITTER_USERNAME, "whocares").AsJson();

            Console.WriteLine(twitter.AsUrl());

            var response = twitter.Request();
            IgnoreFailWhales(response);
            Assert.IsNotNull(response.Exception, "The response object's Exception property should have a value");
        }

        [Test]
        [Category("Requests")]
        public void Can_get_exception_info_from_post_request_that_fails()
        {
            if (string.IsNullOrEmpty(TWITTER_FAILWHALE_PROXY))
            {
                Assert.Ignore("No failwhale server defined in setup file.");
            }
            var twitter = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .Configuration.UseTransparentProxy(TWITTER_FAILWHALE_PROXY)
                .Statuses().Update("Not a real status update").AsJson();

            Console.WriteLine(twitter.AsUrl());

            var response = twitter.Request();
            IgnoreFailWhales(response);
            Assert.IsNotNull(response.Exception, "The response object's Exception property should have a value");
        }

        [Test]
        [Category("Requests")]
        public void Can_get_exception_info_from_request_with_oauth()
        {
            if (string.IsNullOrEmpty(TWITTER_FAILWHALE_PROXY))
            {
                Assert.Ignore("No failwhale server defined in setup file.");
            }
            // load access token from a previous test
            var token = LoadToken("access");
            var twitter = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, token.Token, token.TokenSecret)
                .Configuration.UseTransparentProxy(TWITTER_FAILWHALE_PROXY)
                .Account().VerifyCredentials();

            Console.WriteLine(twitter.AsUrl());

            var response = twitter.Request();
            IgnoreFailWhales(response);
            Assert.IsNotNull(response.Exception, "The response object's Exception property should have a value");
        }

        [Test]
        [Category("Requests")]
        public void Can_get_exception_info_from_request_with_caching_and_oauth()
        {
            if (string.IsNullOrEmpty(TWITTER_FAILWHALE_PROXY))
            {
                Assert.Ignore("No failwhale server defined in setup file.");
            }
            // load access token from a previous test
            var token = LoadToken("access");
            var twitter = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, token.Token, token.TokenSecret)
                .Configuration.CacheWith(CacheFactory.InMemoryCache)
                .Configuration.UseTransparentProxy(TWITTER_FAILWHALE_PROXY)
                .Account().VerifyCredentials();

            Console.WriteLine(twitter.AsUrl());

            var response = twitter.Request();
            IgnoreFailWhales(response);
            Assert.IsNotNull(response.Exception, "The response object's Exception property should have a value");
        }

        [Test]
        [Category("Requests")]
        public void Can_get_timeout_flag_from_timed_out_query()
        {
            if (string.IsNullOrEmpty(TWITTER_FAILWHALE_PROXY))
            {
                Assert.Ignore("No failwhale server defined in setup file.");
            }

            var twitter = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .Configuration.UseTransparentProxy(TWITTER_FAILWHALE_PROXY)
                .Configuration.TimeoutAfter(3.Seconds())
                .Direct("/timeout.php?delay=10")
                .Get();

            Console.WriteLine(twitter.AsUrl());

            var response = twitter.Request();
            IgnoreFailWhales(response);
            Assert.IsTrue(response.TimedOut, "Expected the query to time out but the 'TimedOut' flag is not set.");
            Assert.IsNotNull(response.Exception, "The response object's Exception property should have a value");
        }

        [Test]
        [Category("Requests")]
        public void Can_provide_timeout_value_for_get_query_and_timeout()
        {
            if (string.IsNullOrEmpty(TWITTER_FAILWHALE_PROXY))
            {
                Assert.Ignore("No failwhale server defined in setup file.");
            }

            var twitter = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .Configuration.UseTransparentProxy(TWITTER_FAILWHALE_PROXY)
                .Configuration.TimeoutAfter(5.Seconds())
                .Direct("/timeout.php?delay=6")
                .Get();

            Console.WriteLine(twitter.AsUrl());

            var response = twitter.Request();
            IgnoreFailWhales(response);
            Assert.IsTrue(response.TimedOut, "Expected the query to time out but the 'TimedOut' flag is not set.");
            Assert.IsNotNull(response.Exception, "The response object's Exception property should have a value");
        }

        [Test]
        [Category("Requests")]
        public void Can_provide_timeout_value_for_post_query_and_timeout()
        {
            if (string.IsNullOrEmpty(TWITTER_FAILWHALE_PROXY))
            {
                Assert.Ignore("No failwhale server defined in setup file.");
            }

            var twitter = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .Configuration.UseTransparentProxy(TWITTER_FAILWHALE_PROXY)
                .Configuration.TimeoutAfter(5.Seconds())
                .Direct("/timeout.php?delay=6")
                .Post();

            Console.WriteLine(twitter.AsUrl());

            var response = twitter.Request();
            IgnoreFailWhales(response);
            Assert.IsTrue(response.TimedOut, "Expected the query to time out but the 'TimedOut' flag is not set.");
            Assert.IsNotNull(response.Exception, "The response object's Exception property should have a value");
        }

        [Test]
        [Category("Requests")]
        public void Can_provide_timeout_value_for_oauth_get_query_and_timeout()
        {
            if (string.IsNullOrEmpty(TWITTER_FAILWHALE_PROXY))
            {
                Assert.Ignore("No failwhale server defined in setup file.");
            }

            // load access token from a previous test
            var token = LoadToken("access");
            var twitter = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, token.Token, token.TokenSecret)
                .Configuration.UseTransparentProxy(TWITTER_FAILWHALE_PROXY)
                .Configuration.TimeoutAfter(5.Seconds())
                .Direct("/timeout.php?delay=6")
                .Get();

            Console.WriteLine(twitter.AsUrl());

            var response = twitter.Request();
            IgnoreFailWhales(response);
            Assert.IsTrue(response.TimedOut, "Expected the query to time out but the 'TimedOut' flag is not set.");
        }

        [Test]
        [Category("Requests")]
        public void Can_provide_timeout_value_for_oauth_post_query_and_timeout()
        {
            if (string.IsNullOrEmpty(TWITTER_FAILWHALE_PROXY))
            {
                Assert.Ignore("No failwhale server defined in setup file.");
            }

            // load access token from a previous test
            var token = LoadToken("access");
            var twitter = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, token.Token, token.TokenSecret)
                .Configuration.UseTransparentProxy(TWITTER_FAILWHALE_PROXY)
                .Configuration.TimeoutAfter(5.Seconds())
                .Direct("/timeout.php?delay=6")
                .Post();

            Console.WriteLine(twitter.AsUrl());

            var response = twitter.Request();
            IgnoreFailWhales(response);
            Assert.IsTrue(response.TimedOut, "Expected the query to time out but the 'TimedOut' flag is not set.");
            Assert.IsNotNull(response.Exception, "The response object's Exception property should have a value");
        }

        [Test]
        [Category("Requests")]
        public void Can_provide_timeout_value_for_async_get_query_and_timeout()
        {
            if (string.IsNullOrEmpty(TWITTER_FAILWHALE_PROXY))
            {
                Assert.Ignore("No failwhale server defined in setup file.");
            }

            var twitter = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .Configuration.UseTransparentProxy(TWITTER_FAILWHALE_PROXY)
                .Configuration.TimeoutAfter(5.Seconds())
                .Direct("/timeout.php?delay=6")
                .Get();

            Console.WriteLine(twitter.AsUrl());

            twitter.CallbackTo((s, r, u) =>
                                   {

                                       Assert.IsNotNull(r);
                                       Assert.IsTrue(r.TimedOut, "Expected the query to time out but the 'TimedOut' flag is not set.");
                                   });

            var asyncResult = twitter.BeginRequest();
            var finished = asyncResult.AsyncWaitHandle.WaitOne((int)10.Seconds().TotalMilliseconds, true);
            Assert.IsTrue(finished, "Timed out waiting for async wait handle");
        }

        [Test]
        [Category("Requests")]
        public void Can_provide_timeout_value_for_async_post_query_and_timeout()
        {
            if (string.IsNullOrEmpty(TWITTER_FAILWHALE_PROXY))
            {
                Assert.Ignore("No failwhale server defined in setup file.");
            }

            var twitter = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .Configuration.UseTransparentProxy(TWITTER_FAILWHALE_PROXY)
                .Configuration.TimeoutAfter(5.Seconds())
                .Direct("/timeout.php?delay=6")
                .Post();

            Console.WriteLine(twitter.AsUrl());

            twitter.CallbackTo((s, r, u) =>
                                   {
                                       Assert.IsNotNull(r);
                                       Assert.IsTrue(r.TimedOut, "Expected the query to time out but the 'TimedOut' flag is not set.");
                                   });

            var asyncResult = twitter.BeginRequest();
            var finished = asyncResult.AsyncWaitHandle.WaitOne((int)10.Seconds().TotalMilliseconds, true);
            Assert.IsTrue(finished, "Timed out waiting for async wait handle");
        }

        [Test]
        [Category("Requests")]
        public void Can_provide_timeout_value_for_async_oauth_post_query_and_timeout()
        {
            if (string.IsNullOrEmpty(TWITTER_FAILWHALE_PROXY))
            {
                Assert.Ignore("No failwhale server defined in setup file.");
            }

            // load access token from a previous test
            var token = LoadToken("access");
            var twitter = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, token.Token, token.TokenSecret)
                .Configuration.UseTransparentProxy(TWITTER_FAILWHALE_PROXY)
                .Configuration.TimeoutAfter(5.Seconds())
                .Direct("/timeout.php?delay=6")
                .Post();

            Console.WriteLine(twitter.AsUrl());

            twitter.CallbackTo((s, r, u) =>
                                   {
                                       Assert.IsNotNull(r);
                                       Assert.IsTrue(r.TimedOut, "Expected the query to time out but the 'TimedOut' flag is not set.");
                                   });

            var asyncResult = twitter.BeginRequest();
            var finished = asyncResult.AsyncWaitHandle.WaitOne((int)10.Seconds().TotalMilliseconds, true);
            Assert.IsTrue(finished, "Timed out waiting for async wait handle");
        }
    }
}