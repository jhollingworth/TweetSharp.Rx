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
using TweetSharp.Extensions;
using NUnit.Framework;
using TweetSharp.Twitter.Extensions;
using TweetSharp.Twitter.Fluent;
using TweetSharp.Twitter.Model;

namespace TweetSharp.Twitter.UnitTests.Fluent
{
    partial class FluentTwitterTests
    {
        [Test]
        [Category("Streaming")]
        public void Can_build_stream_urls()
        {
            var url = FluentTwitter.CreateRequest()
                .Stream().FromSample().AsXml().AsUrl();

            Assert.IsNotNull(url);
            Assert.IsTrue(url.Contains("xml"));
            Console.WriteLine(url);

            url = FluentTwitter.CreateRequest()
                .Stream().FromRetweet().AsUrl();

            Assert.IsNotNull(url);
            Assert.IsTrue(url.Contains("json"));
            Console.WriteLine(url);

            url = FluentTwitter.CreateRequest()
                .Stream().FromFilter().Following(12, 13, 15, 16, 20, 87)
                .AsUrl();

            Assert.IsNotNull(url);
            Assert.IsTrue(url.Contains("follow"));
            Console.WriteLine(url);

            url = FluentTwitter.CreateRequest()
                .Stream().FromFilter().Tracking("basketball", "football", "baseball", "footy", "soccer")
                .AsUrl();

            Assert.IsNotNull(url);
            Assert.IsTrue(url.Contains("track"));
            Console.WriteLine(url);

            url = FluentTwitter.CreateRequest()
                .Stream().FromFilter().WithBacklog(10000)
                .AsUrl();

            Assert.IsNotNull(url);
            Assert.IsTrue(url.Contains("count"));
            Console.WriteLine(url);

            url = FluentTwitter.CreateRequest()
                .Stream().FromFilter().DelimitedBy(10)
                .AsUrl();

            Assert.IsNotNull(url);
            Assert.IsTrue(url.Contains("delimited"));
            Console.WriteLine(url);

            url = FluentTwitter.CreateRequest() // -122.75,36.8 -121.75,37.8
                .Stream().FromFilter().Within(new TwitterGeoLocation(36.8, -122.75), new TwitterGeoLocation(37.8, -122.75))
                .AsUrl();

            Assert.IsNotNull(url);
            Assert.IsTrue(url.Contains("locations"));
            Console.WriteLine(url);

            url = FluentTwitter.CreateRequest() // -122.75,36.8 -121.75,37.8
                .Stream().FromFilter()
                .DelimitedBy(10)
                .WithBacklog(10000)
                .Following(12, 13, 15, 16, 20, 87)
                .Tracking("basketball", "football", "baseball", "footy", "soccer")
                .Within(new TwitterGeoLocation(36.8, -122.75), new TwitterGeoLocation(37.8, -122.75))
                .AsUrl();

            Assert.IsNotNull(url);
            Assert.IsTrue(url.Contains("locations"));
            Assert.IsTrue(url.Contains("delimited"));
            Assert.IsTrue(url.Contains("count"));
            Assert.IsTrue(url.Contains("track"));
            Assert.IsTrue(url.Contains("follow"));
            Assert.IsTrue(url.Contains("json"));
            Assert.IsTrue(Uri.IsWellFormedUriString(url, UriKind.Absolute));
            Console.WriteLine(url);
        }

        [Test]
        [Category("Streaming")]
        [ExpectedException(typeof (TweetSharpException))]
        public void Can_fail_to_stream_when_calling_sequentially()
        {
            var twitter = FluentTwitter.CreateRequest()
                .Configuration.TimeoutAfter(5.Milliseconds())
                .Configuration.UseAutomaticRetries(RetryOn.ConnectionClosed, 2)
                .Stream().FromSample().For(1.Minute())
                .CallbackTo((s, r, u) => { });

            twitter.Request();
        }

        [Test]
        [Category("Streaming")]
        [ExpectedException(typeof (TweetSharpException))]
        public void Can_fail_to_stream_when_calling_without_callback()
        {
            var twitter = FluentTwitter.CreateRequest()
                .Configuration.TimeoutAfter(5.Milliseconds())
                .Configuration.UseAutomaticRetries(RetryOn.ConnectionClosed, 2)
                .Stream().FromSample().For(1.Minute());

            var asyncResult = twitter.BeginRequest();
            var finished = asyncResult.AsyncWaitHandle.WaitOne((int)15.Seconds().TotalMilliseconds, true);
            Assert.IsTrue(finished, "Timed out waiting for async wait handle");
        }

        [Test]
        [Category("Streaming")]
        public void Can_stream_from_filter_with_criteria()
        {
            var twitter = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .Configuration.TimeoutAfter(1.Minute()) // This must be larger than the default 5ms timeout or the request will fail
                .Configuration.UseAutomaticRetries(RetryOn.ConnectionClosed, 2)
                .Stream().FromFilter().For(10.Seconds()).Tracking("Twitter")
                .CallbackTo((s, r, u) =>
                                {
                                    var statuses = r.AsStatuses();
                                    Assert.IsNotNull(statuses);
                                    foreach (var status in statuses)
                                    {
                                        Console.WriteLine("{0}: {1}", status.User.ScreenName, status.Text);
                                    }
                                });

            var asyncResult = twitter.BeginRequest();
            var finished = asyncResult.AsyncWaitHandle.WaitOne((int)15.Seconds().TotalMilliseconds, true);
            Assert.IsTrue(finished, "Timed out waiting for async wait handle");
        }

        // {"delete":{"status":{"id":12902837201,"user_id":59554103}}}

        [Test]
        [Category("Streaming")]
        public void Can_stream_from_sample()
        {
            var twitter = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .Configuration.TimeoutAfter(1.Minute())
                // This must be larger than the default 5ms timeout or the request will fail
                .Configuration.UseAutomaticRetries(RetryOn.ConnectionClosed, 2)
                .Stream().FromSample().For(10.Seconds())
                .CallbackTo((s, r, u) =>
                                {
                                    var statuses = r.AsStatuses();
                                    Assert.IsNotNull(statuses);
                                    foreach (var status in statuses)
                                    {
                                        Console.WriteLine("{0}: {1}", status.User.ScreenName, status.Text);
                                    }
                                });

            var asyncResult = twitter.BeginRequest();
            var finished = asyncResult.AsyncWaitHandle.WaitOne((int)15.Seconds().TotalMilliseconds, true);
            Assert.IsTrue(finished, "Timed out waiting for async wait handle");
        }

        [Test]
        [Category("Streaming")]
        public void Can_stream_from_sample_and_cancel()
        {
            var results = 0;
            var twitter = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .Configuration.TimeoutAfter(1.Minute())
                // This must be larger than the default 5ms timeout or the request will fail
                .Configuration.UseAutomaticRetries(RetryOn.ConnectionClosed, 2)
                .Stream().FromSample().For(10.Seconds());

            twitter.CallbackTo((s, r, u) =>
                {
                    var statuses = r.AsStatuses();
                    Assert.IsNotNull(statuses);
                    foreach (var status in statuses)
                    {
                        Console.WriteLine("{0}: {1}", status.User.ScreenName, status.Text);
                    }
                    if(results == 5)
                    {
                        twitter.Root.Cancel();
                    }
                    else
                    {
                        results++;
                    }

                });

            var asyncResult = twitter.BeginRequest();
            var finished = asyncResult.AsyncWaitHandle.WaitOne((int)15.Seconds().TotalMilliseconds, true);
            Assert.IsTrue(finished, "Timed out waiting for async wait handle");
            Assert.IsTrue(results == 5, "Streaming did not cancel after allotted callbacks");
        }
    }
}