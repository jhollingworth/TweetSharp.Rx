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
        [Test(
            Description =
                "This test makes TWITTER_USERNAME a friend of TWITTER_CELEBRITY_SCREEN_NAME before removing them")]
        [Category("Notifications")]
        public void Can_follow_user_notifications_and_then_leave_them()
        {
            var isFriend = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .Friendships().Verify(TWITTER_USERNAME)
                .IsFriendsWith(TWITTER_CELEBRITY_SCREEN_NAME)
                .AsJson();

            var result = isFriend.Request().Response.Equals("false");
            if (result)
            {
                FluentTwitter.CreateRequest()
                    .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                    .Friendships().Befriend(TWITTER_CELEBRITY_SCREEN_NAME)
                    .AsJson().Request();
            }

            Assert.AreEqual(isFriend.Request().Response, "true");

            var follow = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .Notifications().Follow(TWITTER_CELEBRITY_SCREEN_NAME)
                .AsJson();

            Console.WriteLine(follow.ToString());
            var response = follow.Request();

            IgnoreFailWhales(response);
            Assert.AreEqual(TWITTER_CELEBRITY_SCREEN_NAME.ToLower(),
                            response.AsUser().ScreenName.ToLower());

            var leave = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .Notifications().Leave(TWITTER_CELEBRITY_SCREEN_NAME)
                .AsJson();

            response = leave.Request();
            IgnoreFailWhales(response);
            Assert.AreEqual(TWITTER_CELEBRITY_SCREEN_NAME.ToLower(),
                            response.AsUser().ScreenName.ToLower());

            var unfollow = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .Friendships().Destroy(TWITTER_CELEBRITY_SCREEN_NAME);

            response = unfollow.Request();
            IgnoreFailWhales(response);
            Assert.AreEqual(TWITTER_CELEBRITY_SCREEN_NAME.ToLower(),
                            response.AsUser().ScreenName.ToLower());
        }

        [Test]
        [Category("Notifications")]
        public void Should_not_throw_exception_if_attempting_to_follow_or_leave_non_friend()
        {
            var remove = false;
            try
            {
                var verify = FluentTwitter.CreateRequest()
                    .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                    .Friendships()
                    .Verify(TWITTER_USERNAME).IsFriendsWith(TWITTER_CELEBRITY_SCREEN_NAME)
                    .AsJson();

                var exists = verify.Request();

                // No longer returning "true"; returns true now
                // remove = exists.Equals("\"true\"");
                remove = exists.Equals("true");
                if (remove)
                {
                    FluentTwitter.CreateRequest()
                        .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                        .Friendships().Destroy(TWITTER_CELEBRITY_SCREEN_NAME)
                        .AsJson().Request();
                }

                // Following must be done to a current friend
                var follow = FluentTwitter.CreateRequest()
                    .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                    .Notifications().Follow(TWITTER_CELEBRITY_SCREEN_NAME)
                    .AsJson();

                var response = follow.Request();
                var error = response.AsError();
                Assert.IsNotNull(error, "Did not return an error when trying to follow non-friend.");
            }
            finally
            {
                if (remove)
                {
                    FluentTwitter.CreateRequest()
                        .Friendships().Befriend(TWITTER_CELEBRITY_SCREEN_NAME)
                        .AsJson().Request();
                }
            }
        }
    }
}