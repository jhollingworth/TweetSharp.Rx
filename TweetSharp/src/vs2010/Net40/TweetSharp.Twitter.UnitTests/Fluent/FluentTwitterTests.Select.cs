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

namespace TweetSharp.Twitter.UnitTests.Fluent
{
    partial class FluentTwitterTests
    {
//        [Test]
//        [Category("Select")]
//        public void Can_select_for_followers()
//        {
//            var select = FluentTwitter.CreateRequest()
//                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
//                .Select("this followers");
//                
//            var response = select.Request();
//            Assert.IsNotNull(select);
//
//            var followers = response.AsUsers();
//            Assert.IsNotNull(followers);
//        }
//
//        [Test]
//        [Category("Select")]
//        public void Can_select_for_favorites()
//        {
//            var select = FluentTwitter.CreateRequest()
//                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
//                .Select("this favorites");
//
//            var response = select.Request();
//            Assert.IsNotNull(select);
//
//            var favorites = response.AsStatuses();
//            Assert.IsNotNull(favorites);
//        }
//
//        [Test]
//        [Category("Select")]
//        public void Can_select_for_statuses()
//        {
//            var select = FluentTwitter.CreateRequest()
//                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
//                .Select("this statuses").Request();
//
//            Assert.IsNotNull(select);
//
//            var statuses = select.AsStatuses();
//            Assert.IsNotNull(statuses);
//        }
//
//        [Test]
//        [Category("Select")]
//        public void Can_select_for_another_users_statuses()
//        {
//            var select = FluentTwitter.CreateRequest()
//                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
//                .Select(String.Format("{0} statuses", TWITTER_RECIPIENT_SCREEN_NAME)).Request();
//
//            Assert.IsNotNull(select);
//
//            var statuses = select.AsStatuses();
//            Assert.IsNotNull(statuses);
//        }
//
//        [Test]
//        [Category("Select")]
//        [Ignore("Not practical yet; white-list required")]
//        public void Can_select_for_followers_favorites()
//        {
//            var select = FluentTwitter.CreateRequest()
//                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
//                .Select("this followers favorites").Request();
//
//            Assert.IsNotNull(select);
//
//            var favorites = select.AsStatuses();
//
//            Assert.IsNotNull(favorites);
//        }
    }
}