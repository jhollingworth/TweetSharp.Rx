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
using System.Linq;
using TweetSharp.Extensions;
using TweetSharp.Twitter.Fluent;
using TweetSharp.Twitter.Model;
using TweetSharp.Twitter.Service;
using TweetSharp.UnitTests.Base;
using NUnit.Framework;

namespace TweetSharp.Twitter.UnitTests.Service
{
    [TestFixture]
    public class TwitterServiceTests : TwitterTestBase
    {
        [Test]
        [Category("TwitterService")]
        public void Can_list_mentions()
        {
            var service = new TwitterService();
            service.AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret);
            var mentions = service.ListTweetsMentioningMe();

            Assert.IsNotNull(mentions);
            Assert.IsTrue(mentions.Count() <= 20);
        }

        [Test]
        [Category("TwitterService")]
        public void Can_list_mentions_and_fail()
        {
            var service = new TwitterService();
            var mentions = service.ListTweetsMentioningMe();

            Assert.IsNull(mentions);
            Assert.IsNotNull(service.Result);
            Assert.IsNotNull(service.Error);
            Assert.IsTrue(service.Result.ResponseHttpStatusCode == 401);
            Console.WriteLine(service.Error.ErrorMessage);
        }

        [Test]
        [Category("TwitterService")]
        public void Can_search_for_hashtag()
        {
            var service = new TwitterService();
            service.AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret);

            var result = service.SearchForTweets("#haiti", 1, 200);
            foreach (var statuses in result.Statuses)
            {
                Console.WriteLine(statuses.Text);
            }
        }

        [Test]
        [Category("TwitterService")]
        public void Can_get_list_of_lists()
        {
            var service = new TwitterService();
            service.AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret);

            var list = service.ListListsFor("dimebrain");
            foreach(var item in list)
            {
                Console.WriteLine(item.Name);
            }
        }

        [Test]
        [Category("TwitterService")]
        public void Can_send_a_tweet()
        {
            if (!ALLOW_POSTS)
            {
                Assert.Ignore("Sends a live status update");
            }

            var service = new TwitterService();
            
            service.AuthenticateWith(
                OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET,
                TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret);

            var tweet = service.SendTweet("test service tweet at " + DateTime.Now.ToShortTimeString());
            Assert.IsNotNull(tweet);

            Console.WriteLine(tweet.Text);
        }

#if !Mono && !Smartphone
        [Test]
        [Category("TwitterService")]
        public void Can_send_a_tweet_and_cc_myspace()
        {
            if (!ALLOW_POSTS)
            {
                Assert.Ignore("Sends a live status update");
            }

            var service = new TwitterService();
            service.AuthenticateWith(
                 OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET,
                 TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret);

            var tweet = service.SendTweetWithCopyToMySpace(string.Format("test twitter service post to twitter and myspace at {0}", DateTime.Now.ToShortTimeString()), MYSPACE_USER_ID, MYSPACE_CONSUMER_KEY, MYSPACE_CONSUMER_SECRET, MYSPACE_ACCESS_TOKEN, MYSPACE_TOKEN_SECRET);
            Assert.IsNotNull(tweet);

            Console.WriteLine(tweet.Text);
        }

        [Test]
        [Category("TwitterService")]
        [Ignore("Facebook sandbox mode broken - posts to live profile")]
        public void Can_send_a_tweet_and_cc_facebook()
        {
            if (!ALLOW_POSTS)
            {
                Assert.Ignore("Sends a live status update");
            }

            var service = new TwitterService();
            service.AuthenticateWith(
                OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET,
                TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret);

            var tweet = service.SendTweetWithCopyToFacebook(string.Format("test twitter service post to twitter and facebook at {0}", DateTime.Now.ToShortTimeString()), FACEBOOK_API_KEY, FACEBOOK_SESSION_KEY, FACEBOOK_SESSION_SECRET);
            Assert.IsNotNull(tweet);

            Console.WriteLine(tweet.Text);
        }
#endif

        [Test]
        [Category("TwitterService")]
        public void Can_get_request_token()
        {
            var service = new TwitterService();
            var token = service.GetRequestToken(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET);
            Assert.IsNotNull(token);
        }

        [Test]
        [Category("TwitterService")]
        [Timeout(15000)]
        public void Can_stream_from_sample()
        {
            var service = new TwitterService();
            service.AuthenticateWith(OAUTH_CONSUMER_KEY,
                                     OAUTH_CONSUMER_SECRET,
                                     TWITTER_OAUTH.Token,
                                     TWITTER_OAUTH.TokenSecret);

            service.StreamResult += ServiceStreamResult;
            
            var result = service.BeginStreamSample(10.Seconds(), 10);
            result.AsyncWaitHandle.WaitOne(); // No timeouts in NETCF
        }

        [Test]
        [Category("TwitterService")]
        [Timeout(15000)]
        public void Can_stream_from_sample_with_single_result()
        {
            var service = new TwitterService();
            service.AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret);
            service.StreamResult += ServiceStreamResult;

            var result = service.BeginStreamSample(10.Seconds(), 1);
            result.AsyncWaitHandle.WaitOne(); // No timeouts in NETCF
        }

        static void ServiceStreamResult(object sender, TwitterStreamResultEventArgs e)
        {
            Assert.IsNotNull(e.Statuses);
            foreach(var status in e.Statuses)
            {
                Console.WriteLine(status.User.ScreenName + ":" + status.Text);
            }
        }
    }
}