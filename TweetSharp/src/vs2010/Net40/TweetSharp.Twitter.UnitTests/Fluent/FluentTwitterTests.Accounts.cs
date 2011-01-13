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
using System.Net;
using NUnit.Framework;
using TweetSharp.Twitter.Extensions;
using TweetSharp.Twitter.Fluent;
using TweetSharp.Twitter.Model;

namespace TweetSharp.Twitter.UnitTests.Fluent
{
    public partial class FluentTwitterTests
    {
        [Test]
        [Category("Account")]
        public void Can_end_session()
        {
            var twitter = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .Account().EndSession()
                .AsJson();

            Console.WriteLine(twitter.ToString());

            var response = twitter.Request();
            IgnoreFailWhales(response);

            var error = response.AsError();
            if (error != null)
            {
                Assert.AreEqual(error.ErrorMessage, "Logged out.");
            }
        }

        [Test]
        [Category("Account")]
        public void Can_get_rate_limit_status_for_authenticated_user()
        {
            var twitter = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .Account().GetRateLimitStatus()
                .AsJson();

            Console.WriteLine(twitter.ToString());

            var response = twitter.Request();
            IgnoreFailWhales(response);

            var status = response.AsRateLimitStatus();
            Assert.IsNotNull(status);

            Console.WriteLine("Remaining requests for @{0}: {1} / {2}", TWITTER_USERNAME, status.RemainingHits,
                              status.HourlyLimit);
        }

        [Test]
        [Category("Account")]
        public void Can_get_rate_limit_status_for_ip_address()
        {
            var twitter = FluentTwitter.CreateRequest()
                .Account().GetRateLimitStatus()
                .AsJson();

            Console.WriteLine(twitter.ToString());

            var response = twitter.Request();
            IgnoreFailWhales(response);

            var status = response.AsRateLimitStatus();
            Assert.IsNotNull(status);

            Console.WriteLine("Remaining requests this IP: {0} / {1}", status.RemainingHits, status.HourlyLimit);
        }

        [Test]
        [Category("Account")]
        public void Can_update_background_image()
        {
            if (!ALLOW_POSTS)
            {
                Assert.Ignore("Changes your profile data to include our identity");
            }
            var update = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .Account().UpdateProfileBackgroundImage("background.png")
                .AsJson();

            var response = update.Request();
            IgnoreFailWhales(response);
        }

        [Test]
        [Category("Account")]
        public void Can_update_delivery_device()
        {
            if (!ALLOW_POSTS)
            {
                Assert.Ignore("Changes your profile data");
            }
            var twitter = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .Account().UpdateDeliveryDeviceTo(TwitterDeliveryDevice.None)
                .AsJson();

            Console.WriteLine(twitter.ToString());

            var response = twitter.Request();
            IgnoreFailWhales(response);

            var profile = response.AsUser();
            Assert.IsNotNull(profile);
        }

        [Test]
        [Category("Account")]
        public void Can_update_profile()
        {
            if (!ALLOW_POSTS)
            {
                Assert.Ignore("Changes your profile data");
            }

            // Get profile first
            var original = FluentTwitter.CreateRequest()
                .Users().ShowProfileFor(TWITTER_USERNAME)
                .AsJson().Request().AsUser();

            Assert.IsNotNull(original);
            Assert.IsInstanceOfType(typeof (TwitterUser), original);

            var update = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .Account().UpdateProfile()
                .UpdateDescription("test description")
                .UpdateLocation("test location")
                .AsJson();

            Console.WriteLine(update.ToString());

            var response = update.Request();
            IgnoreFailWhales(response);

            var updated = response.AsUser();
            Assert.IsInstanceOfType(typeof (TwitterUser), updated);

            //  Revert back to old values
            var revert = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .Account().UpdateProfile()
                .UpdateDescription(original.Description)
                .UpdateLocation(original.Location)
                .AsJson();

            Console.WriteLine(revert.ToString());

            response = revert.Request();
            Assert.IsNotNull(revert);

            updated = response.AsUser();
            Assert.AreEqual(updated.Description, original.Description);
            Assert.AreEqual(updated.Location, original.Location);
        }

        [Test]
        [Category("Account")]
        public void Can_update_profile_colors()
        {
            if (!ALLOW_POSTS)
            {
                Assert.Ignore("Changes your profile data");
            }
            // Get profile first
            var result = FluentTwitter.CreateRequest()
                .Users().ShowProfileFor(TWITTER_USERNAME)
                .AsJson().Request();
            IgnoreFailWhales(result);
            var original = result.AsUser(); 
            Assert.IsNotNull(original);
            Assert.IsInstanceOfType(typeof (TwitterUser), original);

            var update = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .Account().UpdateProfileColors()
                .UpdateProfileLinkColor("#0084B4") // '#' is ignored
                .AsJson();

            Console.WriteLine(update.ToString());

            var response = update.Request();
            IgnoreFailWhales(response);

            var updated = response.AsUser();
            Assert.IsNotNull(updated);

            //  Revert back to old values
            var revert = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .Account().UpdateProfileColors()
                .UpdateProfileLinkColor(original.ProfileLinkColor)
                .AsJson();

            Console.WriteLine(revert.ToString());

            response = revert.Request();
            IgnoreFailWhales(response);

            updated = response.AsUser();
            Assert.AreEqual(updated.ProfileLinkColor, original.ProfileLinkColor);
        }

        [Test]
        [Category("Account")]
        public void Can_update_profile_image()
        {
            if (!ALLOW_POSTS)
            {
                Assert.Ignore("Changes your profile data to include our identity");
            }
            var update = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .Account().UpdateProfileImage("avatar.jpg")
                .AsJson();

            var response = update.Request();
            IgnoreFailWhales(response);
            Assert.IsNotNull(response.WebResponse,
                             string.Format("Response should have returned but was {0}", response.WebResponse));

            Console.WriteLine(update.ToString());
            Console.WriteLine("HTTP Status was {0}", ((HttpWebResponse) response.WebResponse).StatusCode);
        }

        [Test]
        [Category("Account")]
        public void Can_verify_credentials()
        {
            var twitter = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .Account().VerifyCredentials()
                .AsJson();

            Console.WriteLine(twitter.ToString());

            var response = twitter.Request();
            IgnoreFailWhales(response);

            var profile = response.AsUser();
            Assert.IsNotNull(profile);
            Assert.AreEqual(TWITTER_USERNAME.ToLower(), profile.ScreenName.ToLower());
        }
    }
}