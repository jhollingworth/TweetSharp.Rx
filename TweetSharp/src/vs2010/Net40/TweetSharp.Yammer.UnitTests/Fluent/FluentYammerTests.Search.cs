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
        [Category("YammerSearch")]
        public void Can_search_for_string_returning_json()
        {
            var token = LoadToken("access");
            var yammer = FluentYammer.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, token.Token, token.TokenSecret)
                .Search()
                .ForContent("tweetsharp").AsJson();

            var response = yammer.Request();
            Assert.IsNotNull(response);
            Assert.IsNull(response.Exception);
            Assert.IsFalse(response.IsYammerError);
            var result = response.AsSearchResult();
            Assert.IsNotNull(result);
            result.Messages.ToList().ForEach(m => Assert.IsTrue(m.Body.Plain.Contains("tweetsharp")));
        }
    }
}