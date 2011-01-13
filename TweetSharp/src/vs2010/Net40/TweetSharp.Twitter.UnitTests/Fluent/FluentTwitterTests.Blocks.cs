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

using System.Collections.Generic;
using NUnit.Framework;
using TweetSharp.Twitter.Extensions;
using TweetSharp.Twitter.Fluent;
using TweetSharp.Twitter.Model;

namespace TweetSharp.Twitter.UnitTests.Fluent
{
    partial class FluentTwitterTests
    {
        [Test]
        [Category("Blocks")]
        public void Can_block_user_and_then_unblock_them()
        {
            var block = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .Blocking().Block(TWITTER_CELEBRITY_SCREEN_NAME)
                .AsJson();

            var response = block.Request();
            IgnoreFailWhales(response);
            Assert.AreEqual(
                               TWITTER_CELEBRITY_SCREEN_NAME.ToLower(),
                               response.AsUser().ScreenName.ToLower());

            var unblock = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .Blocking().Unblock(TWITTER_CELEBRITY_SCREEN_NAME)
                .AsJson();

            response = unblock.Request();
            IgnoreFailWhales(response);
            Assert.AreEqual(
                               TWITTER_CELEBRITY_SCREEN_NAME.ToLower(),
                               response.AsUser().ScreenName.ToLower());
        }

        [Test]
        [Category("Blocks")]
        public void Can_get_block_list()
        {
            var blocks = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .Blocking().ListUsers()
                .AsJson();

            var result = blocks.Request();

            var users = result.AsUsers();

            Assert.IsNotNull(users);
        }

        [Test]
        [Category("Blocks")]
        public void Can_get_block_list_ids()
        {
            var blocks = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .Blocking().ListIds()
                .AsJson();

            var result = blocks.Request();

            var users = result.As<List<int>>();

            Assert.IsNotNull(users);
        }
    }
}