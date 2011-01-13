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
using System.Threading;
using Hammock.Tasks;
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
        [Category("Rate Limiting")]
        public void Can_create_request_and_limit_by_target_percent()
        {
            //make up a rate limit status with 5 remaining hits
            var resetTime = DateTime.Now + 10.Minutes();
            var fakeRateLimit = new TwitterRateLimitStatus
                                    {
                                        HourlyLimit = 100,
                                        RemainingHits = 10,
                                        ResetTime = resetTime,
                                        ResetTimeInSeconds = (long)(resetTime - DateTime.Now).TotalSeconds
                                    };

            //set up a recurring call to get mentions every 5 seconds
            //but since there are only 10 remaining hits over 10 minutes
            //and we only want to use 20% of the total, tweetsharp should
            //reconfigure the call to reoccur roughly every 5 minutes after the first
            //call completes. 

            var block = new AutoResetEvent(false);
            var twitter = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .Statuses().Mentions()
                .Configuration.UseRateLimiting(20.Percent(), () => fakeRateLimit)
                .RepeatEvery(5.Seconds());

            var count = 0;
            twitter.CallbackTo((s, r, u) =>
                                   {
                                       try
                                       {
                                           count++;
                                       }
                                       finally
                                       {
                                           block.Set();
                                       }
                                   });

            try
            {
                twitter.BeginRequest();
                var signalled = block.WaitOne((int) 7.Seconds().TotalMilliseconds, true);
                Assert.IsTrue(signalled, "Timed out waiting for first occurance");
                var calls = count; 
                //wait 6 seconds to make sure the method wasn't called again
                Thread.Sleep((int)6.Seconds().TotalMilliseconds);
                twitter.Cancel();
                Assert.AreEqual(calls, count);

            }
            finally
            {
                twitter.Cancel();
            }

        }

        [Test]
        [Category("Rate Limiting")]
        public void Can_create_request_and_limit_with_predicate()
        {
            //make up a rate limit status with 5 remaining hits
            var fakeRateLimit = new TwitterRateLimitStatus
                                    {
                                        HourlyLimit = 100,
                                        RemainingHits = 5,
                                        ResetTime = DateTime.Now + 10.Minutes(),
                                    };

            //create a recurring task that is skipped when there are less than 20 
            //calls left in the rate limit status
            var twitter = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .Statuses().Mentions()
                .Configuration.UseRateLimiting(rateLimit => rateLimit.RemainingUses >= 20, () => fakeRateLimit)
                .RepeatEvery(30.Seconds());

            var callbackCount = 0;
            bool skipped = false;
            twitter.CallbackTo((s, r, u)
                               =>
                                   {
                                       callbackCount++;
                                       skipped = r.SkippedDueToRateLimiting;
                                       twitter.Cancel();
                                   });
            try
            {
                var asyncResult = twitter.BeginRequest();
                var finished = asyncResult.AsyncWaitHandle.WaitOne((int)20.Seconds().TotalMilliseconds, true);
                Assert.IsTrue(skipped, "Task was not skipped");
                Assert.IsTrue(finished, "Timed out waiting for async wait handle");
                Assert.AreEqual(1, callbackCount);
            }
            finally
            {
                //cancel the recurring task to stop it
                twitter.Cancel();
            }

        }

        [Test]
        [Category("Rate Limiting")]
        public void Can_create_request_and_limit_with_predicate_fetching_actual_status()
        {
            //function to get current rate limit
            Func<IRateLimitStatus> getRateLimit = () =>
                                                            {
                                                                var rateLimitQuery = FluentTwitter.CreateRequest()
                                                                    .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                                                                    .Account().GetRateLimitStatus().AsJson();
                                                                var response = rateLimitQuery.Request();
                                                                var limit = response.AsRateLimitStatus();
                                                                Assert.IsNotNull(limit);
                                                                return limit;
                                                            };

            //create a recurring task that is always skipped
            var twitter = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .Statuses().Mentions()
                .Configuration.UseRateLimiting(rateLimit => false, getRateLimit)
                .RepeatEvery(30.Seconds());

            var callbackCount = 0;
            twitter.CallbackTo((s, r, u) =>
                                   {
                                       callbackCount++;
                                       Assert.IsTrue(r.SkippedDueToRateLimiting);
                                       twitter.Cancel(); 
                                   });
            try
            {
                var asyncResult = twitter.BeginRequest();
                var finished = asyncResult.AsyncWaitHandle.WaitOne((int)10.Seconds().TotalMilliseconds, true);
                Assert.IsTrue(finished, "Timed out waiting for async wait handle");
                Assert.AreEqual(1, callbackCount);

            }
            finally
            {
                //cancel the recurring task to stop it
                twitter.Cancel();
            }

        }

        [Test]
        [Category("Rate Limiting")]
        public void Can_delay_next_recurrence_until_limit_reset()
        {
            //make up a rate limit status with no remaining hits
            var resetTime = DateTime.Now + 10.Minutes();
            var fakeRateLimit = new TwitterRateLimitStatus
                                    {
                                        HourlyLimit = 100,
                                        RemainingHits = 0,
                                        ResetTime = resetTime,
                                        ResetTimeInSeconds = (long)(resetTime - DateTime.Now).TotalSeconds
                                    };

            //set up a recurring call to get mentions every 5 seconds
            //but since there are no remaining hits tweetsharp should
            //reconfigure the call to wait until the limit resets before
            //continuing

            var twitter = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .Statuses().Mentions()
                .Configuration.UseRateLimiting(20.Percent(), () => fakeRateLimit)
                .RepeatEvery(5.Seconds());

            var count = 0;
            var block = new AutoResetEvent(false);
            twitter.CallbackTo((s, r, u) =>
                                   {
                                       Interlocked.Increment(ref count);
                                       if (!r.SkippedDueToRateLimiting)
                                       {
                                           block.Set();
                                       }
                                   });
            try
            {
                twitter.BeginRequest();
                var signalled = block.WaitOne((int) 10.Seconds().TotalMilliseconds, true);
                Assert.IsTrue(signalled, "Timed out waiting for initial callback");
                Assert.AreEqual(1, count);
                //wait 6 seconds to make sure the call doesn't happen again
                Thread.Sleep((int)6.Seconds().TotalMilliseconds);
                Assert.AreEqual(1, count);
                
                twitter.Cancel();
            }
            finally
            {
                twitter.Cancel();
            }


        }
    }
}