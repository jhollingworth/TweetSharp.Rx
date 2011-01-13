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
using TweetSharp.Twitter.Extensions;
using TweetSharp.Twitter.Fluent;
using TweetSharp.Twitter.Model;

namespace TweetSharp.Twitter.UnitTests.Fluent
{
    partial class FluentTwitterTests
    {
        [Test]
        [Category("Favorites")]
        public void Can_favorite_a_status_then_unfavorite_it()
        {
            // Get someone's most recent status first
            var responseStatus = FluentTwitter.CreateRequest()
                .Users().ShowProfileFor(TWITTER_CELEBRITY_SCREEN_NAME).AsJson()
                .Request();

            var profile = responseStatus.AsUser();
            var profileStatusId = profile.Status.Id;

            var favorite = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .Favorites().Favorite(profileStatusId)
                .AsJson();

            // Used to throw 403 if already a favorite; doesn't anymore
            try
            {
                var responseFave = favorite.Request();
                Assert.IsNotNull(responseFave);

                var faved = responseFave.AsStatus();
                Assert.IsNotNull(faved);
                Assert.AreEqual((int) profileStatusId, (int) faved.Id);
            }
            catch (Exception)
            {
                Console.WriteLine("Received error; already a favorite");
            }

            var unfavorite = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .Favorites().Unfavorite(profileStatusId)
                .AsJson();

            var responseUnfave = unfavorite.Request();
            Assert.IsNotNull(responseUnfave);

            var unfaved = responseUnfave.AsStatus();
            Assert.IsNotNull(unfaved);
            Assert.AreEqual((int) profileStatusId, (int) unfaved.Id);
        }

        [Test]
        [Category("Favorites")]
        public void Can_request_favorites_for_another_user()
        {
            var twitter = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .Favorites()
                .GetFavoritesFor(TWITTER_RECIPIENT_SCREEN_NAME)
                .AsJson();

            Console.WriteLine(twitter.ToString());

            var response = twitter.Request();
            IgnoreFailWhales(response);

            var favorites = response.AsStatuses();
            Assert.IsNotNull(favorites);
        }

        [Test]
        [Category("Favorites")]
        public void Can_request_favorites_for_authenticating_user()
        {
            var twitter = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .Favorites()
                .GetFavorites()
                .AsJson();

            Console.WriteLine(twitter.ToString());

            var response = twitter.Request();
            IgnoreFailWhales(response);

            var favorites = response.AsStatuses();
            Assert.IsNotNull(favorites);
        }
    }
}