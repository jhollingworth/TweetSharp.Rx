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
using System.Collections.Generic;
using System.Linq;
using TweetSharp.Model;
using NUnit.Framework;
using TweetSharp.Twitter.Extensions;
using TweetSharp.Twitter.Fluent;
using TweetSharp.Twitter.Model;

namespace TweetSharp.Twitter.UnitTests.Fluent
{
    partial class FluentTwitterTests
    {
        [Test]
        [Category("Mocks")]
        public void Can_mock_sequential_get_request_as_json()
        {
            var one = new TwitterStatus {Id = 12345, Text = "one"};
            var two = new TwitterStatus {Id = 34567, Text = "two"};

            var query = FluentTwitter.CreateRequest()
                .Statuses().OnPublicTimeline().AsJson()
                .Expect(one, two);

            var response = query.Request();
            IgnoreFailWhales(response);

            var statuses = response.AsStatuses();
            Assert.IsNotNull(statuses);

            var list = statuses.ToList();
            Assert.AreEqual(2, statuses.Count());
            Assert.Contains(one, list);
            Assert.Contains(two, list);
        }

        [Test]
        [Category("Mocks")]
        [Category("Async")]
        public void Can_mock_sequential_get_request_as_json_async()
        {
            var one = new TwitterStatus { Id = 12345, Text = "one" };
            var two = new TwitterStatus { Id = 34567, Text = "two" };

            var query = FluentTwitter.CreateRequest()
                .Statuses().OnPublicTimeline().AsJson()
                .Expect(one, two);

            var asyncResult = query.BeginRequest();
            var response = query.EndRequest(asyncResult);
            IgnoreFailWhales(response);

            var statuses = response.AsStatuses();
            Assert.IsNotNull(statuses);

            var list = statuses.ToList();
            Assert.AreEqual(2, statuses.Count());
            Assert.Contains(one, list);
            Assert.Contains(two, list);
        }

        [Test]
        [Category("Mocks")]
        [Ignore("In development")]
        public void Can_mock_sequential_post_request_as_json()
        {
            var one = new TwitterStatus {Id = 12345, Text = "one"};
            var two = new TwitterStatus {Id = 34567, Text = "two"};

            var query = FluentTwitter.CreateRequest()
                .Statuses().Update("Chicken")
                .Expect(one, two);

            var response = query.Request();
            Assert.IsNotNull(response, "Result was null");

            var statuses = response.AsStatuses();
            Assert.IsNotNull(statuses, "Expected entities were null");

            var list = statuses.ToList();
            Assert.AreEqual(2, statuses.Count());
            Assert.Contains(one, list);
            Assert.Contains(two, list);
        }

        [Test]
        [Category("Mocks")]
        public void Can_round_trip_status_collection()
        {
            var one = new TwitterStatus {Id = 12345, Text = "one", CreatedDate = DateTime.UtcNow};
            var two = new TwitterStatus {Id = 34567, Text = "two", CreatedDate = DateTime.UtcNow};

            var list = new List<IModel> {one, two};
            var json = Core.Extensions.ToJson(list);

            Console.WriteLine(json);
        }
    }
}