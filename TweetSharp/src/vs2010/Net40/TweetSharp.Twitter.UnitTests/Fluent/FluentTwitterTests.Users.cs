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
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using TweetSharp.Twitter.Extensions;
using TweetSharp.Twitter.Fluent;
using TweetSharp.Twitter.Model;

namespace TweetSharp.Twitter.UnitTests.Fluent
{
    partial class FluentTwitterTests
    {
        [Test]
        [Category("Users")]
        public void Can_disambiguate_numeric_ids()
        {
            var twitter = FluentTwitter.CreateRequest()
                .Users().ShowProfileFor(12345).AsJson();

            Console.WriteLine(twitter.ToString());

            var response = twitter.Request();
            IgnoreFailWhales(response);

            var user = response.AsUser();
            Assert.AreEqual(12345, user.Id);
        }

        [Test]
        [Category("Users")]
        public void Can_disambiguate_numeric_screen_names()
        {
            var twitter = FluentTwitter.CreateRequest()
                .Users().ShowProfileFor("413").AsJson();

            Console.WriteLine(twitter.ToString());

            var response = twitter.Request();
            IgnoreFailWhales(response);

            var user = response.AsUser();
            Assert.AreEqual("413", user.ScreenName);
        }

        [Test]
        [Category("Users")]
        public void Can_get_followers()
        {
            var twitter = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .Users().GetFollowers()
                .AsJson();

            Console.WriteLine(twitter.ToString());

            var response = twitter.Request();
            IgnoreFailWhales(response);

            var followers = response.AsUsers();
            Assert.IsNotNull(followers);
        }

        [Test]
        [Category("Users")]
        public void Can_get_followers_with_new_cursor()
        {
            var twitter = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .Users()
                .GetFollowers()
                .CreateCursor()
                .AsXml();

            Console.WriteLine(twitter.ToString());

            var response = twitter.Request();
            IgnoreFailWhales(response);

            var friends = response.AsUsers();
            var cursor = response.AsNextCursor();
            Assert.IsNotNull(friends);
            Assert.IsNotNull(cursor);
        }

        [Test]
        [Category("Users")]
        public void Can_get_friends()
        {
            var twitter = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .Users()
                .GetFriends()
                .AsXml();

            Console.WriteLine(twitter.ToString());

            var response = twitter.Request();
            IgnoreFailWhales(response);

            var friends = response.AsUsers();
            Assert.IsNotNull(friends);
        }

        [Test]
        [Category("Users")]
        public void Can_get_friends_with_new_cursor()
        {
            var twitter = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .Users()
                .GetFriends()
                .CreateCursor()
                .AsJson();

            Console.WriteLine(twitter.ToString());

            var response = twitter.Request();
            IgnoreFailWhales(response);

            var friends = response.AsUsers();
            var cursor = response.AsNextCursor();
            Assert.IsNotNull(friends);
            Assert.IsNotNull(cursor);
        }

        [Test]
        [Category("Users")]
        public void Can_get_multipage_followers()
        {
            // get the more detailed data class for a user for followers/friends
            var request = FluentTwitter.CreateRequest()
                .Users().ShowProfileFor("Scobleizer").AsJson()
                .Request();

            var scoble = request.AsUser();

            var followersCount = scoble.FollowersCount;
            var friendsCount = scoble.FriendsCount;
            var pages = Math.Ceiling(((double)(friendsCount / 100m)));

            Console.WriteLine("{0} has {1} followers, and {2} friends on {3} pages",
                              scoble.ScreenName,
                              followersCount,
                              friendsCount,
                              pages
                );

            long? cursor = -1;
            const int ceiling = 3;
            var results = new List<TwitterUser>();
            for (var i = 1; i <= ceiling; i++)
            {
                var query = FluentTwitter.CreateRequest()
                    .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                    .Users().GetFollowers().For(scoble.Id).GetCursor(cursor.Value)
                    .Request();

                cursor = query.AsNextCursor();
                var followers = query.AsUsers();
                if (query.IsTwitterError)
                {
                    Assert.Ignore("Twitter error occured.");
                }
                Assert.IsNotNull(followers, "AsUsers() returned null. Error:", query.AsError());
                foreach (var follower in followers)
                {
                    var isUnique = !results.Contains(follower);
                    Assert.IsTrue(isUnique, "Non-unique paging result found");
                }

                results.AddRange(followers);
            }
        }

        [Test]
        [Category("Users")]
        public void Can_move_forward_with_user_cursor()
        {
            var twitter = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .Users()
                .GetFriends().For(TWITTER_CELEBRITY_SCREEN_NAME)
                .CreateCursor()
                .AsXml();

            Console.WriteLine(twitter.ToString());

            var response = twitter.Request();
            IgnoreFailWhales(response);

            var friends = response.AsUsers();
            Assert.IsNotNull(friends);

            var nextCursor = response.AsNextCursor();
            Assert.IsNotNull(nextCursor);
            Assert.IsTrue(nextCursor.Value != 0, "Use a user with >100 friends for this test");

            //get next page
            twitter = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .Users()
                .GetFriends().For(TWITTER_CELEBRITY_SCREEN_NAME)
                .GetCursor(nextCursor.Value)
                .AsJson();

            var secondResponse = twitter.Request();
            Assert.IsNotNull(secondResponse);

            var secondCursor = secondResponse.AsUsers();
            Assert.IsNotNull(secondCursor);
            var previous = secondResponse.AsPreviousCursor();
            Assert.IsNotNull(previous);
            Assert.IsTrue(previous.Value != 0);
        }

        [Test]
        [Category("Users")]
        public void Can_get_user_profile()
        {
            var twitter = FluentTwitter.CreateRequest()
                .Users().ShowProfileFor(TWITTER_RECIPIENT_SCREEN_NAME)
                .AsXml();

            Console.WriteLine(twitter.ToString());

            var response = twitter.Request();
            IgnoreFailWhales(response);

            var profile = response.AsUser();
            Assert.IsNotNull(profile);
        }

        [Test]
        [Category("Users")]
        public void Can_search_for_user()
        {
            var search = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .Users().SearchFor("Daniel Crenna");

            var url = search.AsUrl();
            Console.WriteLine(url);

            var result = search.Request();
            IgnoreFailWhales(result);

            var users = result.AsUsers();
            Assert.IsNotNull(users);
            Assert.IsTrue(users.Count() == 1, "Did not return a single result for a user search expecting it.");
        }

        [Test]
        [Category("Users")]
        public void Can_search_for_users_with_parameters()
        {
            var search = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .Users().SearchFor("Daniel") // required parameter  
                .Page(1).Count(5); // optional parameters

            var url = search.AsUrl();
            Console.WriteLine(url);

            var result = search.Request();
            IgnoreFailWhales(result);

            var users = result.AsUsers();
            Assert.IsNotNull(users);
            Assert.IsTrue(users.Count() > 1, "Did not return multiple results for a user search expecting it");
        }

        [Test]
        [Category("Users")]
        public void Can_lookup_users_by_screen_name()
        {
            var lookup = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .Users().Lookup("danielcrenna", "jdiller");

            var url = lookup.AsUrl();
            Console.WriteLine(url);

            var result = lookup.Request();
            IgnoreFailWhales(result);

            var users = result.AsUsers();
            Assert.IsNotNull(users);
            Assert.IsTrue(users.Count() == 2, "Did not return multiple results for a user search expecting it");

            foreach(var user in users)
            {
                Console.WriteLine(user.ScreenName + ":" + user.Id);
            }
        }

        [Test]
        [Category("Users")]
        public void Can_lookup_users_by_id()
        {
            var lookup = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .Users().Lookup(695523, 11173402);

            var url = lookup.AsUrl();
            Console.WriteLine(url);

            var result = lookup.Request();
            IgnoreFailWhales(result);

            var users = result.AsUsers();
            Assert.IsNotNull(users);
            Assert.IsTrue(users.Count() == 2, "Did not return multiple results for a user search expecting it");

            foreach(var user in users)
            {
                Console.WriteLine(user.ScreenName + ":" + user.Id);
            }
        }

        [Test]
        [Category("Users")]
        public void Can_lookup_users_by_screen_name_and_id()
        {
            var lookup = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .Users().Lookup(new[] { TWITTER_CELEBRITY_SCREEN_NAME }, new[] {695523, 11173402});

            var url = lookup.AsUrl();
            Console.WriteLine(url);

            var result = lookup.Request();
            IgnoreFailWhales(result);

            var users = result.AsUsers();
            Assert.IsNotNull(users);
            Assert.IsTrue(users.Count() == 3, "Did not return multiple results for a user search expecting it");

            foreach (var user in users)
            {
                Console.WriteLine(user.ScreenName + ":" + user.Id);
            }
        }
    }
}