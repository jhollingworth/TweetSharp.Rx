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
using TweetSharp.Extensions;
using NUnit.Framework;
using TweetSharp.Twitter.Fluent;
using TweetSharp.Twitter.Model;
using TweetSharp.Twitter.Extensions;

namespace TweetSharp.Twitter.UnitTests.Fluent
{
    partial class FluentTwitterTests
    {
        [Test]
        [Category("Extensions")]
        public void Can_convert_number_to_word()
        {
            var word = 12345.ToWord();
            Console.WriteLine(word);

            Assert.AreEqual("twelve thousand three hundred forty-five", word);
        }

        [Test]
        [Category("Extensions")]
        public void Can_get_dates_in_relative_time()
        {
            var profile = FluentTwitter.CreateRequest()
                .Users().ShowProfileFor(TWITTER_USERNAME).AsJson()
                .Request().AsUser();

            // gets the time of the user's last status in relative to local time
            Console.WriteLine(profile.Status.CreatedDate.ToRelativeTime(false));
            Console.WriteLine(profile.Status.CreatedDate.ToRelativeTime());
        }
    }
}