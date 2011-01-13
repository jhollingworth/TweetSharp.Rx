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

using TweetSharp.Fluent;
using NUnit.Framework;
using TweetSharp.Yammer.Extensions;
using TweetSharp.Yammer.Fluent;

namespace TweetSharp.Yammer.UnitTests.Fluent
{
    public partial class FluentYammerTests
    {
        [Test]
        [Category("YammerSubscriptions")]
        public void Can_query_if_user_subscribes_to_other_user_returning_json()
        {
            var token = LoadToken("access");
            var yammer = FluentYammer.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, token.Token, token.TokenSecret)
                .Subscriptions()
                .GetSubscriptionToUser(YAMMER_USER_ID)
                .AsJson();

            var response = yammer.Request();
            Assert.IsNotNull(response);
            //will return 404 if the sub doesn't exist
            Assert.IsTrue(response.Exception == null || response.ResponseHttpStatusCode == 404);

            if (response.Exception == null)
            {
                var sub = response.AsSubscription();
                Assert.IsNotNull(sub);
            }
        }

        [Test]
        [Category("YammerSubscriptions")]
        public void Can_query_if_user_subscribes_to_other_user_returning_xml()
        {
            var token = LoadToken("access");
            var yammer = FluentYammer.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, token.Token, token.TokenSecret)
                .Subscriptions()
                .GetSubscriptionToUser(YAMMER_USER_ID)
                .AsXml();

            var response = yammer.Request();
            Assert.IsNotNull(response);
            //will return 404 if the sub doesn't exist
            Assert.IsTrue(response.Exception == null || response.ResponseHttpStatusCode == 404);

            if (response.Exception == null)
            {
                var sub = response.AsSubscription();
                Assert.IsNotNull(sub);
            }
        }

        [Test]
        [Category("YammerSubscriptions")]
        public void Can_query_if_user_subscribes_to_tag_returning_json()
        {
            var token = LoadToken("access");
            var yammer = FluentYammer.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, token.Token, token.TokenSecret)
                .Subscriptions()
                .GetSubscriptionToTag(YAMMER_TAG_ID)
                .AsJson();

            var response = yammer.Request();
            Assert.IsNotNull(response);
            //will return 404 if the sub doesn't exist
            Assert.IsTrue(response.Exception == null || response.ResponseHttpStatusCode == 404);

            if (response.Exception == null)
            {
                var sub = response.AsSubscription();
                Assert.IsNotNull(sub);
            }
        }

        [Test]
        [Category("YammerSubscriptions")]
        public void Can_query_if_user_subscribes_to_tag_returning_xml()
        {
            var token = LoadToken("access");
            var yammer = FluentYammer.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, token.Token, token.TokenSecret)
                .Subscriptions()
                .GetSubscriptionToTag(YAMMER_TAG_ID)
                .AsXml();

            var response = yammer.Request();
            Assert.IsNotNull(response);
            //will return 404 if the sub doesn't exist
            Assert.IsTrue(response.Exception == null || response.ResponseHttpStatusCode == 404);

            if (response.Exception == null)
            {
                var sub = response.AsSubscription();
                Assert.IsNotNull(sub);
            }
        }

        [Test]
        [Category("YammerSubscriptions")]
        public void Can_subscribe_to_tag()
        {
            var token = LoadToken("access");
            var yammer = FluentYammer.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, token.Token, token.TokenSecret)
                .Subscriptions()
                .SubscribeToTag(YAMMER_TAG_ID);

            var response = yammer.Request();
            Assert.IsNotNull(response);
            Assert.IsNull(response.Exception);
        }

        [Test]
        [Category("YammerSubscriptions")]
        public void Can_subscribe_to_user()
        {
            var token = LoadToken("access");
            var yammer = FluentYammer.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, token.Token, token.TokenSecret)
                .Subscriptions()
                .SubscribeToUser(YAMMER_USER_ID);

            var response = yammer.Request();
            Assert.IsNotNull(response);
            Assert.IsNull(response.Exception);
        }

        [Test]
        [Category("YammerSubscriptions")]
        public void Can_unsubscribe_from_tag()
        {
            var token = LoadToken("access");
            var yammer = FluentYammer.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, token.Token, token.TokenSecret)
                .Subscriptions()
                .UnsubscribeFromTag(YAMMER_TAG_ID);

            var response = yammer.Request();
            Assert.IsNotNull(response);
            Assert.IsNull(response.Exception);
        }

        [Test]
        [Category("YammerSubscriptions")]
        public void Can_unsubscribe_from_user()
        {
            var token = LoadToken("access");
            var yammer = FluentYammer.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, token.Token, token.TokenSecret)
                .Subscriptions()
                .UnsubscribeFromUser(YAMMER_USER_ID);

            var response = yammer.Request();
            Assert.IsNotNull(response);
            Assert.IsNull(response.Exception);
        }
    }
}