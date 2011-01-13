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
using TweetSharp.Fluent;
using TweetSharp.Model;
using NUnit.Framework;
using TweetSharp.Yammer.Extensions;
using TweetSharp.Yammer.Fluent;
using TweetSharp.Yammer.Model;

namespace TweetSharp.Yammer.UnitTests.Fluent
{
    [TestFixture]
    public partial class FluentYammerTests : YammerTestBase
    {
        protected string SetupFile
        {
            get { return "setup.xml"; }
        }

        private static void VerifyMetadata(YammerResult response)
        {
            var meta = response.AsResponseMetadata();
            Assert.IsNotNull(meta);
            Assert.Greater(meta.RequestedPollInterval, 0);
            Assert.IsNotNull(meta.UserReferences);
            Assert.IsNotNull(meta.ThreadReferences);
        }

        private static void VerifyMessage(YammerMessage message)
        {
            Assert.Greater(message.Id, 0);
            Assert.Greater(message.CreatedAt, DateTime.MinValue);
            Assert.IsFalse(string.IsNullOrEmpty(message.Url));
            Assert.IsFalse(string.IsNullOrEmpty(message.WebUrl));
        }

        [Test]
        [Ignore("OAuth already configured")]
        public void Can_get_request_token()
        {
            var yammer = FluentYammer.CreateRequest()
                .Authentication.GetRequestToken(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET);

            var response = yammer.Request();
            Assert.IsNotNull(response);

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
        [Ignore("OAuth already configured")]
        public void Can_redirect_to_authorization_page_from_desktop_and_exchange_access_token()
        {
            // load request token from a previous test
            var token = LoadToken("request");

            // setting this method will launch the authorization page out of band
            var yammer = FluentYammer.CreateRequest()
                .Authentication.AuthorizeDesktop(OAUTH_CONSUMER_KEY,
                                                 OAUTH_CONSUMER_SECRET,
                                                 token.Token, token.TokenSecret);

            //OK, this is rather lame, but to work around a flaw in OAuth, Yammer now
            //supplies a 4 character verification code that the human user is supposed to provide 
            //back to the client which needs to be sent back as part of this AuthorizeDesktop
            //call.  This means we can't fully automate this test right now so if you 
            //want to run it, set a breakpoint here, and use the debugger to set the 
            //value of the following variable to to the token that you get from yammer; 

            var verificationCode = ""; //<-- set this via the debugger 
            if (string.IsNullOrEmpty(verificationCode))
            {
                Assert.Ignore("Run this test in the debugger, it requires user interaction - see comments");
            }
            // Assert.IsNotEmpty( verificationCode );
            yammer = yammer.Authentication.SetVerifier(verificationCode);


            // the request will convert to an access token exchange request,
            // so this is a good place to place a breakpoint
            var response = yammer.Request();
            Assert.IsNotNull(response);

            var error = response.AsError();
            Assert.IsNull(error, "Yammer deemed the user has not authorized this token exchange.");

            var accessToken = response.AsToken();
            Assert.IsNotNull(accessToken);
            Assert.IsTrue(accessToken.Token != null);
            Assert.IsTrue(accessToken.TokenSecret != null);

            SaveToken(accessToken, "access");
            Console.WriteLine("Token: {0}", accessToken.Token);
            Console.WriteLine("Token Secret: {0}", accessToken.TokenSecret);
        }

        [Test]
        [Ignore("OAuth already configured")]
        public void Can_use_access_token_with_get_request()
        {
            // load access token from a previous test
            var token = LoadToken("access");

            var yammer = FluentYammer.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY,
                                  OAUTH_CONSUMER_SECRET,
                                  token.Token,
                                  token.TokenSecret)
                .Messages()
                .Received()
                .AsJson();

            var response = yammer.Request();
            Assert.IsNotNull(response);

            var error = response.AsError();
            Assert.IsNull(error, string.Format("yammer returned {0}", error != null ? error.ErrorMessage : ""));
        }
    }
}