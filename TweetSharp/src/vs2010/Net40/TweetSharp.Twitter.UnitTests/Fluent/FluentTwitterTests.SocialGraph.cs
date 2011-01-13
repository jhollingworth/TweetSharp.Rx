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
    public partial class FluentTwitterTests
    {
        [Test]
        [Category("SocialGraph")]
        public void Can_get_followers_for_other()
        {
            var twitter = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .SocialGraph().Ids().ForFollowersOf(TWITTER_CELEBRITY_SCREEN_NAME)
                .AsJson();

            var response = twitter.Request();
            IgnoreFailWhales(response);

            var ids = response.As<List<int>>();
            Assert.IsTrue(ids.Count > 0, "list had no elements");

            foreach (var id in ids)
            {
                Console.WriteLine(id);
            }
        }

        [Test]
        [Category("SocialGraph")]
        public void Can_get_followers_ids()
        {
            var twitter = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .SocialGraph().Ids().ForFollowers()
                .AsJson();

            var response = twitter.Request();
            IgnoreFailWhales(response);

            var ids = response.As<List<int>>();
            Assert.IsTrue(ids.Count > 0, "list had no elements");
            foreach (var id in ids)
            {
                Console.WriteLine(id);
            }
        }

        [Test]
        [Category("SocialGraph")]
        public void Can_get_friends_ids()
        {
            var twitter = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .SocialGraph().Ids().ForFriends()
                .AsJson();

            var response = twitter.Request();
            IgnoreFailWhales(response);

            var ids = response.As<List<int>>();
            Assert.IsTrue(ids.Count > 0, "list had no elements");

            foreach (var id in ids)
            {
                Console.WriteLine(id);
            }
        }

        [Test]
        [Category("SocialGraph")]
        public void Can_get_friends_ids_for_other()
        {
            var twitter = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .SocialGraph().Ids().ForFriendsOf(TWITTER_CELEBRITY_SCREEN_NAME)
                .AsJson();

            var response = twitter.Request();
            IgnoreFailWhales(response);

            var ids = response.As<List<int>>();
            Assert.IsTrue(ids.Count > 0, "list had no elements");

            foreach (var id in ids)
            {
                Console.WriteLine(id);
            }
        }

        [Test]
        [Category("SocialGraph")]
        public void Can_get_paged_follower_ids_for_other()
        {
            //Using a user with more than 5000 followers
            var twitter = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .SocialGraph().Ids().ForFollowersOf("ev")
                .CreateCursor()
                .AsXml(); //<-use xml for this fetch and json for the rest to make sure both can parse

            var response = twitter.Request();
            IgnoreFailWhales(response);

            //We need to retrieve the next_cursor from the response to be used in the request for the next page
            var cursor = response.AsNextCursor();
            Assert.IsNotNull(cursor);

            var ids = response.AsIds();

            //Each page should contain up to 5000 ids (minus those of users with suspended accounts)
            Assert.Greater(ids.Count, 0, "list had no elements");

            //Looping through 2 pages. For practical use the follower count could be used:
            //var maxPages = Math.Ceiling(user.FollowersCount / 5000m);
            //for (var i = 0; i < maxPages; i++)
            for (var i = 0; i < 2; i++)
            {
                var pagedTwitter = FluentTwitter.CreateRequest()
                    .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                    .SocialGraph().Ids().ForFollowersOf("ev")
                    .GetCursor(cursor.Value)
                    .AsJson();
                var pagedResponse = pagedTwitter.Request();
                ids.AddRange(pagedResponse.AsIds());
                cursor = pagedResponse.AsNextCursor();
            }
            Assert.Greater(ids.Distinct().Count(), 5000, "list had fewer than expected distinct elements");
        }
    }
}