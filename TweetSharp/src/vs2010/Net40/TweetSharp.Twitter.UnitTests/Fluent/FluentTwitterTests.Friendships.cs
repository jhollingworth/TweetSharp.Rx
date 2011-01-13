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
using NUnit.Framework;
using TweetSharp.Twitter.Extensions;
using TweetSharp.Twitter.Fluent;
using TweetSharp.Twitter.Model;
using TweetSharp.Twitter.UnitTests.Helpers;

namespace TweetSharp.Twitter.UnitTests.Fluent
{
    partial class FluentTwitterTests
    {
        private TwitterResult Unfollow(string userScreenName)
        {
            var leave = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .Friendships().Destroy(userScreenName).AsJson();

            return leave.Request();
        }

        [Test(Description = "This test assumes you don't have a prior friendship with TWITTER_CELEBRITY_SCREEN_NAME")]
        [Category("Friendships")]
        public void Can_create_friendship_and_destroy_it()
        {
            var unBefriend = false;
            try
            {
                var follow = FluentTwitter.CreateRequest()
                    .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                    .Friendships().Befriend(TWITTER_CELEBRITY_SCREEN_NAME)
                    .WithNotifications().AsJson();

                Console.WriteLine(follow.ToString());

                var response = follow.Request();
                Assert.IsFalse(response.IsTwitterError, string.Format("Twitter returned error {0}", response.AsError()));

                var celebrity = response.AsUser();
                if (response.IsTwitterError)
                {
                    Console.WriteLine(string.Format(
                        "You may have already been following {0}; you should run this test again",TWITTER_CELEBRITY_SCREEN_NAME)
                        );
                }
                else
                {
                    Assert.IsNotNull(celebrity, "Parsing response 'AsUser' returned null.");
                    unBefriend = true;
                }
            }
            finally
            {
                if (unBefriend)
                {
                    Thread.Sleep(4000); //give it time to remember 
                    var response = Unfollow(TWITTER_CELEBRITY_SCREEN_NAME);
                    Assert.IsFalse(response.IsTwitterError,
                                   string.Format("Request to unfriend {0} returned twitter error\n{1}",
                                                 TWITTER_CELEBRITY_SCREEN_NAME, response.AsError()));

                    var celebrity = response.AsUser();
                    Assert.IsNotNull(celebrity);
                }
            }
        }

        [Test(Description = "This test assumes you don't have a prior friendship with TWITTER_CELEBRITY_SCREEN_NAME.")]
        [Category("Friendships")]
        [Ignore("Twitter behavior is inconsistent")]
        public void Can_create_friendship_and_fail_on_double_request()
        {
            try
            {
                var twitter = FluentTwitter.CreateRequest()
                    .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                    .Friendships()
                    .Befriend(TWITTER_CELEBRITY_SCREEN_NAME)
                    .WithNotifications()
                    .AsJson();

                Console.WriteLine(twitter.ToString());

                // Used to throw 403 on double,
                // then used to send back the user either way,
                // now throws 403 again
                twitter.Request();
                var response = twitter.Request();
                var error = response.AsError();
                Assert.IsNotNull(error);
                Assert.IsTrue(error.ErrorMessage.Contains("already"));
            }
            finally
            {
                Thread.Sleep(4000); //give twitter time to propagate
                var response = Unfollow(TWITTER_CELEBRITY_SCREEN_NAME);
                IgnoreFailWhales(response);

                var celebrity = response.AsUser();
                Assert.IsNotNull(celebrity);
            }
        }

        [Test]
        [Category("Friendships")]
        [Category("Async")]
        [Ignore("Unreliable due to twitter caching")]
        public void Can_create_friendship_async()
        {
            var undoBefriend = false;
            try
            {
                var callbackReached = false;
                var twitter = FluentTwitter.CreateRequest()
                    .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                    .Friendships().Befriend(TWITTER_CELEBRITY_SCREEN_NAME).AsJson()
                    .CallbackTo((s, r, u) =>
                                    {
                                        if(r.IsTwitterError)
                                        {
                                            var message = string.Format(
                                                "Skipping this test: \n'{0}'", r.AsError()
                                                );

                                            Console.WriteLine(message);
                                        }
                                        else
                                        {
                                            var users = r.Response.ToResult().AsUser();
                                            Assert.IsNotNull(users, "Befriend response not successfully parsed 'AsUser'");
                                        }

                                        undoBefriend = true;
                                        callbackReached = true;
                                    });

                var asyncResult = twitter.BeginRequest();
                var result = twitter.EndRequest(asyncResult);
                
                IgnoreFailWhales(result);
                Assert.IsTrue(callbackReached, "Callback wasn't signalled");

                Console.WriteLine(twitter.ToString());
            }
            finally
            {
                if (undoBefriend)
                {
                    Thread.Sleep(6000); // Give twitter time to propagate data
                    var response = Unfollow(TWITTER_CELEBRITY_SCREEN_NAME);
                    Assert.IsFalse(response.IsTwitterError,
                                   string.Format("Request to leave friendship with {0} returned twitter error\n{1}",
                                                 TWITTER_CELEBRITY_SCREEN_NAME, response.AsError()));

                    var recipient = response.AsUser();
                    Assert.IsNotNull(recipient);
                }
            }
        }

        [Test(Description = "This test assumes the two specified test users are not friends")]
        [Category("Friendships")]
        public void Can_deny_friendship()
        {
            var twitter = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .Friendships()
                .Verify(TWITTER_USERNAME).IsFriendsWith(TWITTER_NONFRIEND_SCREEN_NAME)
                .AsJson();

            Console.WriteLine(twitter.ToString());

            var result = twitter.Request();
            Assert.AreEqual("false", result.Response);
        }

        [Test]
        [Category("Friendships")]
        public void Can_show_friendship_data()
        {
            var query = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .Friendships().Show("danielcrenna", "jdiller")
                .AsJson();

            var response = query.Request();

            var friendship = response.AsFriendship();

            Assert.IsNotNull(friendship);
            Assert.IsTrue(friendship.Relationship.Source.Following);
            Assert.IsTrue(friendship.Relationship.Target.Following);

            Console.WriteLine(response);
        }

        [Test]
        [Category("Friendships")]
        [Ignore]
        public void Can_get_incoming_friendship_requests()
        {
            /*
            //ensure there's at least one
            var twitterRequestFriendship = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .Friendships().Befriend(TWITTER_PROTECTEDUSER).AsJson();
            var response = twitterRequestFriendship.Request();
            IgnoreFailWhales(response); //might be a 403, but that just means the request is already pending
            
            //now get list of incoming 
            var twitter = FluentTwitter.CreateRequest()
                .AuthenticateAs(TWITTER_PROTECTEDUSER, TWITTER_PROTECTEDUSER_PASSWORD)
                .Friendships().Incoming().AsJson();
            var nextResponse = twitter.Request();
            IgnoreFailWhales(nextResponse);
            var ids = nextResponse.AsIds(); 
            Assert.IsNotNull(ids);
            Assert.That(ids.Count > 0 );
            */
        }

        [Test]
        [Category("Friendships")]
        public void Can_get_outgoing_friendship_requests()
        {
            //ensure there's at least one
            var twitterRequestFriendship = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .Friendships().Befriend(TWITTER_PROTECTEDUSER).AsJson();
            var response = twitterRequestFriendship.Request();
            IgnoreFailWhales(response); //might be a 403, but that just means the request is already pending

            //now get list of incoming 
            var twitter = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .Friendships().Outgoing().CreateCursor().AsJson();
            var nextResponse = twitter.Request();
            Assert.IsNotNull(nextResponse);
            IgnoreFailWhales(nextResponse);
            var ids = nextResponse.AsIds();
            Assert.IsNotNull(ids);
            Assert.That(ids.Count > 0);

        }
    }
}