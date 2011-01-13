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
using Hammock.Authentication.OAuth;
using NUnit.Framework;
using TweetSharp.Twitter.Fluent;
using TweetSharp.Twitter.Extensions;

namespace TweetSharp.Twitter.UnitTests.Fluent
{
    partial class FluentTwitterTests
    {
        [Test]
        [Category("Authentication")]
        [Ignore("OAuth already configured")]
        public void Can_get_request_token_over_https()
        {
            var twitter = FluentTwitter.CreateRequest()
                .Configuration.UseTransparentProxy("https://api.twitter.com")
                .Authentication.GetRequestToken(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET);

            var response = twitter.Request();
            IgnoreFailWhales(response);

            var token = response.AsToken();
            Assert.IsNotNull(token);
            Assert.IsTrue(token.Token != null);
            Assert.IsTrue(token.TokenSecret != null);

            // store request token and secret to XML for another unit test to use
            SaveToken(token, "request");

            Console.WriteLine("Token: {0}", token.Token);
            Console.WriteLine("Token Secret: {0}", token.TokenSecret);
        }

        [Test]
        [Category("Authentication")]
        [Ignore("OAuth already configured")]
        public void Can_get_user_authorized_access_token()
        {
            // load request token from a previous test
            var requestToken = LoadToken("request");
            var verifier = LoadPin();

            var twitter = FluentTwitter.CreateRequest()
                .Authentication.GetAccessToken(OAUTH_CONSUMER_KEY,
                                               OAUTH_CONSUMER_SECRET,
                                               requestToken.Token,
                                               verifier);

            var response = twitter.Request();
            IgnoreFailWhales(response);

            var accessToken = response.AsToken();
            Assert.IsNotNull(accessToken);
            Assert.IsTrue(accessToken.Token != null);
            Assert.IsTrue(accessToken.TokenSecret != null);

            SaveToken(accessToken, "access");
        }

#if !Smartphone
        [Test]
        [Category("Authentication")]
        [Ignore("OAuth already configured")]
        public void Can_redirect_to_authorization_page_from_desktop_and_exchange_access_token()
        {
            // load request token from a previous test
            var token = LoadToken("request");

            // setting this method will launch the authorization page out of band
            var twitter = FluentTwitter.CreateRequest()
                .Authentication.AuthorizeDesktop(OAUTH_CONSUMER_KEY,
                                                 OAUTH_CONSUMER_SECRET,
                                                 token.Token);

            //There is no good way to automate this, so set this value via the debugger
            string verifier = ""; //<-- set this via the debugger 
            Assert.IsNotEmpty(verifier, "You have to set the provided PIN in the debugger.");
            SavePin(verifier);
            twitter.Authentication.SetVerifier(verifier);

            // the request will convert to an access token exchange request,
            // so this is a good place to place a breakpoint
            var response = twitter.Request();
            IgnoreFailWhales(response);

            var error = response.AsError();
            Assert.IsNull(error, "Twitter deemed the user has not authorized this token exchange.");

            var accessToken = response.AsToken();
            Assert.IsNotNull(accessToken);
            Assert.IsTrue(accessToken.Token != null);
            Assert.IsTrue(accessToken.TokenSecret != null);

            SaveToken(accessToken, "access");
            Console.WriteLine("Token: {0}", accessToken.Token);
            Console.WriteLine("Token Secret: {0}", accessToken.TokenSecret);
        }
#endif

        [Test]
        [Category("Authentication")]
        [Ignore("OAuth already configured")]
        public void Can_use_access_token_with_get_request()
        {
            // load access token from a previous test
            var token = LoadToken("access");

            var twitter = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY,
                                  OAUTH_CONSUMER_SECRET,
                                  token.Token,
                                  token.TokenSecret)
                .DirectMessages().Sent();

            var response = twitter.Request();
            IgnoreFailWhales(response);

            var error = response.AsError();
            Assert.IsNull(error);
        }

        [Test]
        [Category("Authentication")]
        [Ignore("OAuth already configured")]
        public void Can_use_access_token_with_another_get_request()
        {
            // load access token from a previous test
            var token = LoadToken("access");

            var twitter = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY,
                                  OAUTH_CONSUMER_SECRET,
                                  token.Token,
                                  token.TokenSecret)
                .Statuses().Mentions();

            var response = twitter.Request();
            IgnoreFailWhales(response);

            var error = response.AsError();
            Assert.IsNull(error);
        }

        [Test]
        [Category("Authentication")]
        public void Can_use_access_token_with_post_request()
        {
            if (!ALLOW_POSTS)
            {
                Assert.Ignore("This test makes a live status update");
            }
            // load access token from a previous test
            var token = LoadToken("access");

            var update = Uri.EscapeDataString(string.Format(
                "something #requiring #encoding @! at {0}", DateTime.Now.ToShortTimeString()
                                                            ));
            var twitter = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY,
                                  OAUTH_CONSUMER_SECRET,
                                  token.Token,
                                  token.TokenSecret)
                .Statuses().Update(update)
                .AsJson();

            var response = twitter.Request();
            IgnoreFailWhales(response);

            var status = response.AsStatus();
            Assert.IsNotNull(status);

            var error = response.AsError();
            Assert.IsNull(error);
        }

        [Test]
        [Category("Authentication")]
        public void Can_use_access_token_with_post_request_containing_apostrophe()
        {
            if (!ALLOW_POSTS)
            {
                Assert.Ignore("This test makes a live status update");
            }
            // load access token from a previous test
            var token = LoadToken("access");

            var twitter = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY,
                                  OAUTH_CONSUMER_SECRET,
                                  token.Token,
                                  token.TokenSecret)
                .Statuses().Update("testing y'all")
                .AsJson();

            var response = twitter.Request();
            IgnoreFailWhales(response);

            var status = response.AsStatus();
            Assert.IsNotNull(status);

            var error = response.AsError();
            Assert.IsNull(error);
        }

        [Test]
        [Category("Authentication")]
        public void Can_use_access_token_with_post_request_containing_parens()
        {
            if (!ALLOW_POSTS)
            {
                Assert.Ignore("This test makes a live status update");
            }
            // load access token from a previous test
            var token = LoadToken("access");

            var twitter = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY,
                                  OAUTH_CONSUMER_SECRET,
                                  token.Token,
                                  token.TokenSecret)
                .Statuses().Update("(testing)")
                .AsJson();

            var response = twitter.Request();
            IgnoreFailWhales(response);

            var status = response.AsStatus();
            Assert.IsNotNull(status);

            var error = response.AsError();
            Assert.IsNull(error);
        }

        [Test]
        [Category("Authentication")]
        public void Can_use_access_token_with_post_request_containing_more_punctutation()
        {
            if (!ALLOW_POSTS)
            {
                Assert.Ignore("This test makes a live status update");
            }
            // load access token from a previous test
            var token = LoadToken("access");

            const string sequence = @"!?"";:<>\\|`#$%^&*+-_{}[]";

            var twitter = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY,
                                  OAUTH_CONSUMER_SECRET,
                                  token.Token,
                                  token.TokenSecret)
                .Statuses().Update(sequence)
                .AsJson();

            var response = twitter.Request();
            IgnoreFailWhales(response);

            var status = response.AsStatus();
            Assert.IsNotNull(status);

            var error = response.AsError();
            Assert.IsNull(error);
        }

        [Test]
        [Category("Authentication")]
        public void Can_use_access_token_to_update_bg_image()
        {
            if (!ALLOW_POSTS)
            {
                Assert.Ignore("This test makes a live status update");
            }
            // load access token from a previous test
            var token = LoadToken("access");

            var twitter = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY,
                                  OAUTH_CONSUMER_SECRET,
                                  token.Token,
                                  token.TokenSecret)
                .Account().UpdateProfileBackgroundImage("background.png")
                .AsJson();

            var response = twitter.Request();
            Assert.IsNotNull(response, "Error: {0}", response.AsError());

            var user = response.AsUser();
            Assert.IsNotNull(user);

            var error = response.AsError();
            Assert.IsNull(error);
        }

        [Test]
        [Category("Authentication")]
        [Ignore("OAuth already configured")]
        public void Can_use_access_token_create_friendship_with_gzip()
        {
            //Test for problem reported as Issue #89
            try
            {
                // load access token from a previous test
                var token = LoadToken("access");

                var twitter = FluentTwitter.CreateRequest()
                    .AuthenticateWith(OAUTH_CONSUMER_KEY,
                                      OAUTH_CONSUMER_SECRET,
                                      token.Token,
                                      token.TokenSecret)
                    .Configuration.UseGzipCompression()
                    .Friendships()
                    .Befriend(TWITTER_CELEBRITY_SCREEN_NAME)
                    .WithNotifications()
                    .AsJson();

                Console.WriteLine(twitter.ToString());

                var response = twitter.Request();
                IgnoreFailWhales(response);

                var britney = response.AsUser();
                Assert.IsNotNull(britney);
            }
            catch (WebException)
            {
                Assert.Ignore(string.Format("Adjust reality so that you are not friends with {0} before this test",
                                            TWITTER_CELEBRITY_SCREEN_NAME));
            }
        }

        [Test]
        [Ignore("Twitter has not given us back xAuth privileges yet.")]
        public void Can_get_access_token_with_client_auth()
        {
            /*
            var twitter = FluentTwitter.CreateRequest()
                .Configuration.UseTransparentProxy("https://api.twitter.com/oauth/{0}")
                .Authentication.GetClientAuthAccessToken(OAUTH_CONSUMER_KEY,
                                                         OAUTH_CONSUMER_SECRET,
                                                         TWITTER_USERNAME,
                                                         TWITTER_PASSWORD);

            var response = twitter.Request();
            IgnoreFailWhales(response);

            var token = response.AsToken();
            Assert.IsNotNull(token);
            Assert.IsTrue(token.Token != null);
            Assert.IsTrue(token.TokenSecret != null);

            // store access token and secret to XML for another unit test to use
            SaveToken(token, "access");

            Console.WriteLine("Token: {0}", token.Token);
            Console.WriteLine("Token Secret: {0}", token.TokenSecret);
            */
        }

        [Test]
        [Ignore("OAuth already configured")]
        public void Can_set_oauth_proxy_with_token_requests()
        {
            var twitter = FluentTwitter.CreateRequest()
                .Configuration.UseProxy("https://api.twitter.com/")
                .Authentication.GetRequestToken(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET);

            twitter.Request();
        }
    }
}