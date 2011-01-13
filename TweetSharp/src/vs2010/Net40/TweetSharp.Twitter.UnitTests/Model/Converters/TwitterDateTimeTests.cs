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
using System.Threading;
using TweetSharp.Extensions;
using NUnit.Framework;
using TweetSharp.Twitter.Model;

namespace TweetSharp.Twitter.UnitTests.Model.Converters
{
    [TestFixture]
    public class TwitterDateTimeTests
    {
        [Test]
        public void Can_access_safely_from_multiple_threads()
        {
            var threads = new List<Thread>
                              {
                                  new Thread(Can_get_correct_relative_time_from_local_time),
                                  new Thread(Can_get_correct_relative_time_from_utc_time),
                                  new Thread(Can_create_instance_from_string_from_search_result),
                                  new Thread(Can_create_instance_from_string),
                                  new Thread(Can_convert_instance_from_datetime),
                                  new Thread(Can_convert_instance_from_datetime),
                                  new Thread(Can_get_correct_relative_time_from_local_time),
                                  new Thread(Can_get_correct_relative_time_from_utc_time),
                                  new Thread(Can_create_instance_from_string_from_search_result),
                                  new Thread(Can_create_instance_from_string),
                                  new Thread(Can_convert_instance_from_datetime),
                                  new Thread(Can_convert_instance_from_datetime)

                              };
            threads.ForEach(t => t.Start());
        }

        [Test]
        public void Can_convert_instance_from_datetime()
        {
            const string expected = "Fri Dec 19 17:24:18 +0000 2008";
            var time = new DateTime(2008, 12, 19, 17, 24, 18, 0, DateTimeKind.Utc);

            var actual = TwitterDateTime.ConvertFromDateTime(time, TwitterDateFormat.RestApi);

            Assert.AreEqual(expected, actual);
            Console.WriteLine(actual);
        }

        [Test]
        public void Can_convert_instance_to_datetime()
        {
            const string json = "Fri Dec 19 17:24:18 +0000 2008";
            var date = TwitterDateTime.ConvertToDateTime(json);
            var instance = date;

            Console.WriteLine(date);
            Assert.IsNotNull(instance);
        }

        [Test]
        public void Can_create_instance_from_string()
        {
            const string json = "Fri Dec 19 17:24:18 +0000 2008";
            var date = TwitterDateTime.ConvertToTwitterDateTime(json);

            Console.WriteLine(date.ToString());
            Assert.AreEqual(json, date.ToString());
        }

        [Test]
        public void Can_create_instance_from_string_from_search_result()
        {
            const string json = "Tue, 13 Jan 2009 18:10:17 +0000";
            var date = TwitterDateTime.ConvertToTwitterDateTime(json);

            Console.WriteLine(date.ToString());
            Assert.AreEqual(json, date.ToString());
        }

        [Test]
        public void Can_get_correct_relative_time_from_local_time()
        {
            var oneHourAgo = DateTime.Now - new TimeSpan(0, 1, 0, 0);
            var str = oneHourAgo.ToRelativeTime();
            Assert.AreEqual(str.ToLower(), "1 hour ago");
        }

        [Test]
        public void Can_get_correct_relative_time_from_utc_time()
        {
            var oneHourAgo = DateTime.Now - new TimeSpan(0, 1, 0, 0);
            var oneHourAgoUtc = oneHourAgo.ToUniversalTime();

            var str = oneHourAgoUtc.ToRelativeTime();
            Assert.AreEqual(str.ToLower(), "1 hour ago");
        }
    }
}