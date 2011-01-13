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
using TweetSharp.Fluent;
using NUnit.Framework;
using TweetSharp.Yammer.Extensions;
using TweetSharp.Yammer.Fluent;

namespace TweetSharp.Yammer.UnitTests.Fluent
{
    partial class FluentYammerTests
    {
        [Test]
        [Category("YammerAutoComplete")]
        public void Can_get_autocomplete_suggestions_as_json()
        {
            var token = LoadToken("access");
            var yammer = FluentYammer.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, token.Token, token.TokenSecret)
                .AutoComplete().GetSuggestions("j").AsJson();

            var response = yammer.Request();
            Assert.IsNotNull(response);
            Assert.IsNull(response.Exception);
            Assert.IsFalse(response.IsYammerError);
            var results = response.AsAutoCompleteSuggestions();
            Assert.IsNotNull(results);
            results.Users.ToList().ForEach(u => Assert.IsTrue(u.Name.StartsWith("j")));
            results.Groups.ToList().ForEach(g => Assert.IsTrue(g.Name.StartsWith("j")));
            results.Tags.ToList().ForEach(t => Assert.IsTrue(t.Name.StartsWith("j")));
        }

        [Test]
        [Category("YammerAutoComplete")]
        public void Can_get_autocomplete_suggestions_as_xml()
        {
            var token = LoadToken("access");
            var yammer = FluentYammer.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, token.Token, token.TokenSecret)
                .AutoComplete().GetSuggestions("j").AsXml();

            var response = yammer.Request();
            Assert.IsNotNull(response);
            Assert.IsNull(response.Exception);
            Assert.IsFalse(response.IsYammerError);
            var results = response.AsAutoCompleteSuggestions();
            Assert.IsNotNull(results);
            results.Users.ToList().ForEach(u => Assert.IsTrue(u.Name.StartsWith("j")));
            results.Groups.ToList().ForEach(g => Assert.IsTrue(g.Name.StartsWith("j")));
            results.Tags.ToList().ForEach(t => Assert.IsTrue(t.Name.StartsWith("j")));
        }
    }
}