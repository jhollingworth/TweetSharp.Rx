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
using TweetSharp.Model;
using NUnit.Framework;
using TweetSharp.Yammer.Extensions;
using TweetSharp.Yammer.Fluent;
using TweetSharp.Yammer.Model;

namespace TweetSharp.Yammer.UnitTests.Fluent
{
    partial class FluentYammerTests
    {
        [Test]
        [Category("YammerOrgChart")]
        public void Can_add_all_at_once()
        {
            var user = GetCurrentUser();
            var token = LoadToken("access");
            var yammer = FluentYammer.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, token.Token, token.TokenSecret)
                .Relationships()
                .Create(user.Id, "coolchester@" + YAMMER_DOMAIN, "floormopper@" + YAMMER_DOMAIN,
                        "beancounter@" + YAMMER_DOMAIN);

            var response = yammer.Request();
            Assert.IsNotNull(response);
            Assert.IsNull(response.Exception);
            Assert.IsFalse(response.IsYammerError);
        }

        [Test]
        [Category("YammerOrgChart")]
        public void Can_add_colleague()
        {
            var user = GetCurrentUser();
            var token = LoadToken("access");
            var yammer = FluentYammer.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, token.Token, token.TokenSecret)
                .Relationships()
                .AddColleague(user.Id, "peerman@" + YAMMER_DOMAIN);

            var response = yammer.Request();
            Assert.IsNotNull(response);
            Assert.IsNull(response.Exception);
            Assert.IsFalse(response.IsYammerError);
        }

        [Test]
        [Category("YammerOrgChart")]
        public void Can_add_subordinate()
        {
            var user = GetCurrentUser();
            var token = LoadToken("access");
            var yammer = FluentYammer.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, token.Token, token.TokenSecret)
                .Relationships()
                .AddSubordinate(user.Id, "flunkie@" + YAMMER_DOMAIN);


            var response = yammer.Request();
            Assert.IsNotNull(response);
            Assert.IsNull(response.Exception);
            Assert.IsFalse(response.IsYammerError);
        }

        [Test]
        [Category("YammerOrgChart")]
        public void Can_add_superior()
        {
            var user = GetCurrentUser();
            var token = LoadToken("access");
            var yammer = FluentYammer.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, token.Token, token.TokenSecret)
                .Relationships()
                .AddSuperior(user.Id, "bossman@" + YAMMER_DOMAIN);


            var response = yammer.Request();
            Assert.IsNotNull(response);
            Assert.IsNull(response.Exception);
            Assert.IsFalse(response.IsYammerError);
        }

        [Test]
        [Category("YammerOrgChart")]
        [Ignore("Doesn't appear to work on Yammer's end")]
        public void Can_destroy_relationship()
        {
#pragma warning disable 0618 //used intentionally here as a test case
            var user = GetCurrentUser();
            var token = LoadToken("access");
            var yammer = FluentYammer.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, token.Token, token.TokenSecret)
                .Relationships()
                .Destroy(user.Id, YAMMER_USER_ID, OrgChartRelationshipType.Colleague);
#pragma warning restore 0618

            var response = yammer.Request();
            Assert.IsNotNull(response);
            Assert.IsNull(response.Exception);
            Assert.IsFalse(response.IsYammerError);
        }

        [Test]
        [Category("YammerOrgChart")]
        public void Can_show_relationships()
        {
            var user = GetCurrentUser();
            var token = LoadToken("access");
            var yammer = FluentYammer.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, token.Token, token.TokenSecret)
                .Relationships()
                .Show(user.Id).AsXml();

            var response = yammer.Request();
            Assert.IsNotNull(response);
            Assert.IsNull(response.Exception);
            Assert.IsFalse(response.IsYammerError);
            var rels = response.AsRelationships();
            Assert.IsNotNull(rels);
        }
    }
}