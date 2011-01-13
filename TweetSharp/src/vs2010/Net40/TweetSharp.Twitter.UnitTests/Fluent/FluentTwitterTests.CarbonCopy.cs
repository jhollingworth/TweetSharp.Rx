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

#if !Mono && !Smartphone
namespace TweetSharp.Twitter.UnitTests.Fluent
{
    // [DC]: Carbon copy services are part of TweetSharp.Extras
    partial class FluentTwitterTests
    {
        [Test]
        [Category("CarbonCopies")]
        [Ignore("Facebook sandbox mode broken - posts to live profile")]
        public void Can_copy_status_update_to_facebook()
        {
            if (!ALLOW_POSTS)
            {
                Assert.Ignore("This test makes a live status update");
            }

            var query = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .Statuses().Update(string.Format("Sending this tweet to Twitter and Facebook! (at {0}", DateTime.Now.ToShortTimeString()))
                .CopyToFacebook(FACEBOOK_API_KEY, FACEBOOK_SESSION_KEY, FACEBOOK_SESSION_SECRET);

            var response = query.Request();
            Assert.IsTrue(response.ResponseHttpStatusCode == 200);
        }

        [Test]
        [Category("CarbonCopies")]
        public void Can_copy_status_update_to_myspace()
        {
            if (!ALLOW_POSTS)
            {
                Assert.Ignore("This test makes a live status update");
            }

            var query = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .Statuses().Update(string.Format("Sending this tweet to Twitter and MySpace! at {0}", DateTime.Now.ToShortDateString()))
                .CopyToMySpace(MYSPACE_USER_ID, MYSPACE_CONSUMER_KEY, MYSPACE_CONSUMER_SECRET, MYSPACE_ACCESS_TOKEN, MYSPACE_TOKEN_SECRET);
           
            var response = query.Request(); 
            Assert.IsTrue(response.ResponseHttpStatusCode == 200);
        }

        [Test]
        [Ignore("Facebook sandbox mode broken - posts to live profile")]
        public void Can_copy_status_update_to_facebook_and_myspace()
        {
            if (!ALLOW_POSTS)
            {
                Assert.Ignore("This test makes a live status update");
            }

            var query = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .Statuses().Update("Sending this tweet to Twitter, Facebook, and MySpace!")
                .CopyToMySpace(MYSPACE_USER_ID, MYSPACE_CONSUMER_KEY, MYSPACE_CONSUMER_SECRET, MYSPACE_ACCESS_TOKEN, MYSPACE_TOKEN_SECRET)
                .CopyToFacebook(FACEBOOK_API_KEY, FACEBOOK_SESSION_KEY, FACEBOOK_SESSION_SECRET);
            
            var response = query.Request();
            Assert.IsFalse(response.IsTwitterError);
            Assert.IsTrue(response.ResponseHttpStatusCode == 200);  
        }
    }
}
#endif