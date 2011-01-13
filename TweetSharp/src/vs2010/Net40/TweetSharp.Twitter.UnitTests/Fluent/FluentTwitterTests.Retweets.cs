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
using TweetSharp.Twitter.Fluent;
using TweetSharp.Twitter.Model;
using TweetSharp.Twitter.Extensions;

namespace TweetSharp.Twitter.UnitTests.Fluent
{
    partial class FluentTwitterTests
    {
        [Test]
        [Category("Retweets")]
        public void Can_deserialize_statuses_with_retweeted_status()
        {
            var twitter = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .Statuses().RetweetedToMe()
                .AsJson();

            Console.WriteLine(twitter.AsUrl());

            var response = twitter.Request();
            var statuses = !response.IsTwitterError ? response.AsStatuses() : null;
            Assert.IsNotNull(statuses);
        }

        [Test]
        [Category("Retweets")]
        public void Can_get_home_timeline()
        {
            var twitter = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .Statuses().OnFriendsTimeline()
                .AsJson();

            Console.WriteLine(twitter.AsUrl());

            var response = twitter.Request();
            var statuses = !response.IsTwitterError ? response.AsStatuses() : null;
            Assert.IsNotNull(statuses);
        }

        [Test]
        [Category("Retweets")]
        public void Can_get_retweets_by_me()
        {
            var twitter = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .Statuses().RetweetedByMe()
                .AsJson();

            Console.WriteLine(twitter.AsUrl());

            var response = twitter.Request();
            var statuses = !response.IsTwitterError ? response.AsStatuses() : null;
            Assert.IsNotNull(statuses);
        }

        [Test]
        [Category("Retweets")]
        public void Can_get_retweets_by_status_id()
        {
            var query = FluentTwitter.CreateRequest()
                .Users().ShowProfileFor(TWITTER_USERNAME)
                .Request();

            var result = query.AsUser();

            var statusId = result.Status.Id;

            var twitter = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .Statuses().RetweetsOf(statusId)
                .AsJson();

            Console.WriteLine(twitter.AsUrl());

            var response = twitter.Request();
            var statuses = !response.IsTwitterError ? response.AsStatuses() : null;
            Assert.IsNotNull(statuses);
        }

        [Test]
        [Category("Retweets")]
        public void Can_get_retweets_of_me()
        {
            var twitter = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .Statuses().RetweetsOfMe()
                .AsJson();

            Console.WriteLine(twitter.AsUrl());

            var response = twitter.Request();
            var statuses = !response.IsTwitterError ? response.AsStatuses() : null;
            Assert.IsNotNull(statuses);
        }

        [Test]
        [Category("Retweets")]
        public void Can_get_retweets_to_me()
        {
            var twitter = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .Statuses().RetweetedToMe()
                .AsJson();

            Console.WriteLine(twitter.AsUrl());

            var response = twitter.Request();
            var statuses = !response.IsTwitterError ? response.AsStatuses() : null;
            Assert.IsNotNull(statuses);
        }

        [Test]
        [Category("Retweets")]
        public void Can_retweet_using_native_mode()
        {
            if (!ALLOW_POSTS)
            {
                Assert.Ignore("This test makes a live status update");
            }
            var random = new Random();
            var retries = 5;
            TwitterStatus status = null;
            while (retries > 0 && status == null)
            {
                var twitter = FluentTwitter.CreateRequest()
                    .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                    .Statuses().Retweet(7098366728 + random.Next(50000))
                    .AsJson();

                Console.WriteLine(twitter.AsUrl());

                var response = twitter.Request();
                status = !response.IsTwitterError ? response.AsStatus() : null;
                retries--;
            }
            Assert.IsNotNull(status);
        }
    }
}