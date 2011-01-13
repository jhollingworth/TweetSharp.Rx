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
using TweetSharp.Yammer.Fluent;

namespace TweetSharp.Yammer.UnitTests.Fluent
{
    public partial class FluentYammerTests
    {
        //todo:
        //it's unclear what the 'suggestions' apis return.  There's no documentation on it, and
        //as far as I can tell, there is no way via the web ui to actually suggest anything
        //(there are invitations, which are covered separately) so none of these apis ever return anything
        //and the 'decline' api hasn't been tested because there's nothing to decline. 
        //
        //consider removing the APIs completely, unless it turns out there's something I missed
        //
        //JD - Feb 13,2010

        [Test]
        [Category("YammerSuggestions")]
        [Ignore("Suggestion apis seem incomplete on yammer's end")]
        public void Can_decline_suggestions()
        {
            var token = LoadToken("access");
            var yammer = FluentYammer.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, token.Token, token.TokenSecret)
                .Suggestions().Decline(123);


            var response = yammer.Request();
            Assert.IsNotNull(response);
            Assert.IsNull(response.Exception);
            Assert.IsFalse(response.IsYammerError);
        }

        [Test]
        [Category("YammerSuggestions")]
        [Ignore("Suggestion apis seem incomplete on yammer's end")]
        public void Can_get_all_suggestions_as_json()
        {
            var token = LoadToken("access");
            var yammer = FluentYammer.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, token.Token, token.TokenSecret)
                .Suggestions().ShowAll().AsJson();


            var response = yammer.Request();
            Assert.IsNotNull(response);
            Assert.IsNull(response.Exception);
            Assert.IsFalse(response.IsYammerError);
        }

        [Test]
        [Category("YammerSuggestions")]
        [Ignore("Suggestion apis seem incomplete on yammer's end")]
        public void Can_get_all_suggestions_as_xml()
        {
            var token = LoadToken("access");
            var yammer = FluentYammer.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, token.Token, token.TokenSecret)
                .Suggestions().ShowAll().AsXml();


            var response = yammer.Request();
            Assert.IsNotNull(response);
            Assert.IsNull(response.Exception);
            Assert.IsFalse(response.IsYammerError);
        }

        [Test]
        [Category("YammerSuggestions")]
        [Ignore("Suggestion apis seem incomplete on yammer's end")]
        public void Can_get_group_suggestions()
        {
            var token = LoadToken("access");
            var yammer = FluentYammer.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, token.Token, token.TokenSecret)
                .Suggestions().ShowGroups().AsJson();


            var response = yammer.Request();
            Assert.IsNotNull(response);
            Assert.IsNull(response.Exception);
            Assert.IsFalse(response.IsYammerError);
        }

        [Test]
        [Category("YammerSuggestions")]
        [Ignore("Suggestion apis seem incomplete on yammer's end")]
        public void Can_get_user_suggestions()
        {
            var token = LoadToken("access");
            var yammer = FluentYammer.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, token.Token, token.TokenSecret)
                .Suggestions().ShowUsers().AsJson();


            var response = yammer.Request();
            Assert.IsNotNull(response);
            Assert.IsNull(response.Exception);
            Assert.IsFalse(response.IsYammerError);
        }
    }
}