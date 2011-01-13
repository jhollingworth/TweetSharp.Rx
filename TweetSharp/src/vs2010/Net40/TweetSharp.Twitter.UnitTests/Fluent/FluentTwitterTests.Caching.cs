using System;
using NUnit.Framework;
using TweetSharp.Extensions;
using TweetSharp.Twitter.Extensions;
using TweetSharp.Twitter.Fluent;

namespace TweetSharp.Twitter.UnitTests.Fluent
{
    partial class FluentTwitterTests
    {
#if !Smartphone // doesn't support complex cache with expiry
        [Test]
        [Category("Caching")]
        [Category("Async")]
        public void Can_request_http_get_async_with_cache_and_set_response()
        {
            var callbackReached = false;
            var twitter = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .Configuration.CacheUntil(60.Seconds().FromNow())
                .Statuses().OnPublicTimeline().AsJson()
                .CallbackTo((s, r, u) =>
                {
                    Assert.IsNotNull(r.Response);
                    var statuses = r.AsStatuses();
                    Assert.IsNotNull(statuses);
                    callbackReached = true;
                });

            var asyncResult = twitter.BeginRequest();
            var finished = asyncResult.AsyncWaitHandle.WaitOne((int) 10.Seconds().TotalMilliseconds, true);
            Assert.IsTrue(finished, "Timed out waiting for async call to complete");
            var result = twitter.EndRequest(asyncResult);
            Assert.IsTrue(callbackReached, "Did not hit callback on first request");
            Assert.IsNotNull(result.WebResponse, "First call wasn't from web");

            asyncResult = twitter.BeginRequest();
            result = twitter.EndRequest(asyncResult);

            Assert.IsNull(result.WebResponse, "Second call wasn't from cache");
            Console.WriteLine(twitter.ToString());
        }
#endif
    }
}
