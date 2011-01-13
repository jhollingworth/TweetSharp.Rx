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
using TweetSharp.Twitter.Fluent;

namespace TweetSharp.Twitter.UnitTests.Fluent
{
    public partial class FluentTwitterTests
    {
        [Test]
        [Category("Timed Tasks")]
        [Ignore("This test never ends and is only meant to verify operation of continuous tasks")]
        public void Can_create_continuous_repetitive_task()
        {
            var twitter = FluentTwitter.CreateRequest()
                .Statuses().OnPublicTimeline().AsJson()
                .CallbackTo((s, r, u) => Console.WriteLine("Task executed."))
                .RepeatEvery(3.Seconds());

            var asyncResult = twitter.BeginRequest();
            var finished = asyncResult.AsyncWaitHandle.WaitOne((int)10.Seconds().TotalMilliseconds, true);
            Assert.IsTrue(finished, "Timed out waiting for async wait handle");
        }

        [Test]
        [Category("Timed Tasks")]
        public void Can_create_repetitive_task()
        {
            const int repeat = 5; 
            var count = 0;
            var twitter = FluentTwitter.CreateRequest()
                .Statuses().OnPublicTimeline().AsJson();
            twitter.CallbackTo((s, r, u) =>
                                {
                                    Interlocked.Increment(ref count);
                                    Console.WriteLine("Task returned.");
                                })
                .RepeatAfter(2.Seconds(), repeat);

            var asyncResult = twitter.BeginRequest();
            var finished = asyncResult.AsyncWaitHandle.WaitOne((int)15.Seconds().TotalMilliseconds, true);
            Assert.IsTrue(finished, "Timed out waiting for async wait handle");

            Assert.AreEqual(repeat, count, "Task did not repeat the correct number of times.");
        }
    }
}