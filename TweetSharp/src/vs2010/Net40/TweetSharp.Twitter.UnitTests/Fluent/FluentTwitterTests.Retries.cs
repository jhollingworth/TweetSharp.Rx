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

using TweetSharp.Extensions;
using NUnit.Framework;
using TweetSharp.Twitter.Fluent;
using TweetSharp.Twitter.Model;

namespace TweetSharp.Twitter.UnitTests.Fluent
{
    partial class FluentTwitterTests
    {
        [Test]
        [Category("Retries")]
        public void Can_get_failwhale_response_from_failwhale_server()
        {
            if (string.IsNullOrEmpty(TWITTER_FAILWHALE_PROXY))
            {
                Assert.Ignore("No failwhale server defined in setup file.");
            }

            var twitter = FluentTwitter.CreateRequest()
                .Configuration.UseTransparentProxy(TWITTER_FAILWHALE_PROXY)
                .Direct("Halloo.xml");

            var result = twitter.Request();
            Assert.IsTrue(result.IsFailWhale);
        }

        [Test]
        [Category("Retries")]
        public void Can_retry_from_async_delete()
        {
            if (string.IsNullOrEmpty(TWITTER_FAILWHALE_PROXY))
            {
                Assert.Ignore("No failwhale server defined in setup file.");
            }

            const int retries = 3;
            
            TwitterResult twitterResult = null;
            var twitter = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .Configuration.UseTransparentProxy(TWITTER_FAILWHALE_PROXY)
                .Configuration.UseAutomaticRetries(RetryOn.ServiceError, retries)
                .Lists().DeleteList(TWITTER_USERNAME, 12345L)
                .CallbackTo((s, r, u) =>
                                {
                                    twitterResult = r;
                                });

            var asyncResult = twitter.BeginRequest();
            var finished = asyncResult.AsyncWaitHandle.WaitOne();
            Assert.IsTrue(finished, "Timed out waiting for the wait handle");

            Assert.IsNotNull(twitterResult, "Result was null");
            //our test server throws a 402-method not allowed;
            //that shows up as twitter error; good enough
            Assert.AreEqual(retries, twitterResult.Retries, "Retry count mismatch");
        }

        [Test]
        [Category("Retries")]
        public void Can_retry_from_async_get()
        {
            if (string.IsNullOrEmpty(TWITTER_FAILWHALE_PROXY))
            {
                Assert.Ignore("No failwhale server defined in setup file.");
            }

            const int retries = 5;
            
            TwitterResult twitterResult = null;
            var twitter = FluentTwitter.CreateRequest()
                .Configuration.UseTransparentProxy(TWITTER_FAILWHALE_PROXY)
                .Configuration.UseAutomaticRetries(RetryOn.FailWhaleOrNetwork, retries)
                .Statuses().OnHomeTimeline().AsJson()
                .CallbackTo((s, r, u) =>
                                {
                                    twitterResult = r;
                                });

            var asyncResult = twitter.BeginRequest();
            var finished = asyncResult.AsyncWaitHandle.WaitOne();
            Assert.IsTrue(finished, "Timed out waiting for wait handle");
            Assert.IsNotNull(twitterResult);
            Assert.AreEqual(retries, twitterResult.Retries, "Retry count mismatch");
            twitter.Root.Cancel();
        }

        [Test]
        [Category("Retries")]
        public void Can_retry_from_async_post()
        {
            if (string.IsNullOrEmpty(TWITTER_FAILWHALE_PROXY))
            {
                Assert.Ignore("No failwhale server defined in setup file.");
            }

            const int retries = 5;
            
            TwitterResult twitterResult = null;
            var twitter = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .Configuration.UseTransparentProxy(TWITTER_FAILWHALE_PROXY)
                .Configuration.UseAutomaticRetries(RetryOn.FailWhaleOrNetwork, retries)
                .Statuses().Update("not a real status update")
                .CallbackTo((s, r, u) =>
                                {
                                    twitterResult = r;
                                });

            var asyncResult = twitter.BeginRequest();
            asyncResult.AsyncWaitHandle.WaitOne();

            Assert.IsNotNull(twitterResult);
            Assert.IsTrue(twitterResult.IsFailWhale);
            Assert.AreEqual(retries, twitterResult.Retries);
        }

        [Test]
        [Category("Retries")]
        public void Can_retry_on_synchronous_get()
        {
            if (string.IsNullOrEmpty(TWITTER_FAILWHALE_PROXY))
            {
                Assert.Ignore("No failwhale server defined in setup file.");
            }
            const int retries = 5;

            var twitter = FluentTwitter.CreateRequest()
                .Configuration.UseTransparentProxy(TWITTER_FAILWHALE_PROXY)
                .Configuration.UseAutomaticRetries(RetryOn.FailWhaleOrNetwork, retries)
                .Statuses().OnFriendsTimeline().AsJson();

            var result = twitter.Request();
            Assert.IsTrue(result.IsFailWhale, "Final result was not a failure, expected failwhale");
            Assert.AreEqual(retries, result.Retries);
        }

        [Test]
        [Category("Retries")]
        public void Can_retry_on_timeout()
        {
            if (string.IsNullOrEmpty(TWITTER_FAILWHALE_PROXY))
            {
                Assert.Ignore("No failwhale server defined in setup file.");
            }

            const int retries = 1;

            var twitter = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .Configuration.UseTransparentProxy(TWITTER_FAILWHALE_PROXY)
                .Configuration.UseAutomaticRetries(RetryOn.Timeout, retries)
                .Configuration.TimeoutAfter(3.Seconds())
                .Direct("/timeout.php?delay=10").Get();

            var result = twitter.Request();
            Assert.IsTrue(result.TimedOut, "Final result was not a timeout, expected timeout");
            Assert.AreEqual(retries, result.Retries, "Retry count mismatch");
        }

        [Test]
        [Category("Retries")]
        public void Can_set_retries_in_configuration_and_retry_on_error()
        {
            if (string.IsNullOrEmpty(TWITTER_FAILWHALE_PROXY))
            {
                Assert.Ignore("No failwhale server defined in setup file.");
            }
            const int retries = 5;

            var twitter = FluentTwitter.CreateRequest()
                .Configuration.UseTransparentProxy(TWITTER_FAILWHALE_PROXY)
                .Configuration.UseAutomaticRetries(RetryOn.FailWhaleOrNetwork, retries)
                .Statuses().OnPublicTimeline().AsJson();

            var result = twitter.Request();
            Assert.IsTrue(result.IsFailWhale, "Final result was not a failure, expected failwhale");
            Assert.AreEqual(retries, result.Retries, "Expected the retry count to be {0}, but it was {1}", retries,
                            result.Retries);
        }
    }
}