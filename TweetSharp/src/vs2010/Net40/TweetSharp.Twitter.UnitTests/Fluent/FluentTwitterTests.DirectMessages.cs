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

using System.Linq;
using NUnit.Framework;
using TweetSharp.Twitter.Fluent;
using TweetSharp.Twitter.Model;
using TweetSharp.Twitter.Extensions;

namespace TweetSharp.Twitter.UnitTests.Fluent
{
    public partial class FluentTwitterTests
    {
        

        [Test]
        [Category("DirectMessages")]
        public void Can_request_received_messages_by_authenticated_user()
        {
            var twitter = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .DirectMessages().Received()
                .AsJson();

            var response = twitter.Request();
            IgnoreFailWhales(response);

            var messages = response.AsDirectMessages();
            Assert.IsNotNull(messages);
        }

        [Test]
        [Category("DirectMessages")]
        public void Can_request_sent_messages_from_authenticated_user()
        {
            var twitter = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .DirectMessages().Sent()
                .AsJson();

            var response = twitter.Request();
            IgnoreFailWhales(response);

            var messages = response.AsDirectMessages();
            Assert.IsNotNull(messages);
        }


        [Test]
        [Category("DirectMessages")]
        public void Can_send_direct_message_and_destroy_it()
        {
            if (!ALLOW_POSTS)
            {
                Assert.Ignore("This test makes a live status update");
            }

            var send = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .DirectMessages().Send(TWITTER_RECIPIENT_SCREEN_NAME, "test dm")
                .AsJson();

            var response = send.Request();
            IgnoreFailWhales(response);

            var message = response.AsDirectMessage();
            Assert.IsNotNull(message);

            var destroy = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .DirectMessages().Destroy(message.Id)
                .AsJson();

            response = destroy.Request();
            IgnoreFailWhales(response);
            Assert.AreEqual(message.Text, response.AsDirectMessage().Text);
        }
    }
}