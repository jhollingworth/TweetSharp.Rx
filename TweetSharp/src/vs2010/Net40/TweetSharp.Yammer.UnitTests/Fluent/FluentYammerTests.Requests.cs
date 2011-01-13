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
using System.Threading;
using TweetSharp.Extensions;
using NUnit.Framework;
using TweetSharp.Yammer.Extensions;
using TweetSharp.Yammer.Fluent;

namespace TweetSharp.Yammer.UnitTests.Fluent
{
    partial class FluentYammerTests
    {
        [Test]
        [Category("YammerRequests")]
        public void Can_request_all_messages_async()
        {
            var token = LoadToken("access");
            var yammer = FluentYammer.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, token.Token, token.TokenSecret)
                .Messages().All()
                .AsJson()
                .CallbackTo((s, r, u) =>
                                {
                                    Assert.IsNotNull(r.Response);
                                    var statuses = r.AsMessages();
                                    Assert.IsNotNull(statuses);
                                });

            var asyncResult = yammer.BeginRequest();
            var finished = asyncResult.AsyncWaitHandle.WaitOne((int)10.Seconds().TotalMilliseconds, true);
            Assert.IsTrue(finished, "Timed out waiting for async wait handle");

            Console.WriteLine(yammer.ToString());
        }

#if !Smartphone
        //doesn't support complex cache with expiry
        [Test]
        [Category("Requests")]
        public void Can_request_http_get_async_with_cache_and_set_response()
        {

            var token = LoadToken("access");
            // Call with huge cache
            var yammer = FluentYammer.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, token.Token, token.TokenSecret)
                .Configuration.CacheUntil(60.Seconds().FromNow())
                .Messages().All().NewerThan(YAMMER_PREVIOUS_MESSAGE_ID);
                
            var asyncResult = yammer.BeginRequest();
            var result = yammer.EndRequest(asyncResult);
            Assert.IsNotNull(result.WebResponse);

            asyncResult = yammer.BeginRequest();
            var result2 = yammer.EndRequest(asyncResult);

            // Second call came from cache
            Assert.IsTrue(result2.IsFromCache);
            Console.WriteLine(yammer.ToString());


        }
#endif
    }
}