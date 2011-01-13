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
using NUnit.Framework;
using TweetSharp.Twitter.Fluent;
using TweetSharp.Twitter.Model;
#if Smartphone
using Uri = System.Uri;
#endif

namespace TweetSharp.Twitter.UnitTests.Model
{
    [TestFixture]
    public class TwitterStatusTests
    {
        [Test]
        public void Can_parse_artifacts()
        {
            var tweet = new TwitterStatus
                            {
                                Text = "RT @jdiller: Blogged: Manage Your Friends and Followers With TweetSharp - Part 4 - UI : http://is.gd/79omj #omg #lol"
                            };

            var html = tweet.TextHtml;
            var mentions = tweet.TextMentions;
            IEnumerable<Uri> urls = tweet.TextLinks;
            var hashtags = tweet.TextHashtags;

            Console.WriteLine(html);

            Assert.IsNotNull(html);
            Assert.IsTrue(mentions.Count() == 1);
            Assert.IsTrue(mentions.First().Equals("jdiller"));
            Assert.IsTrue(urls.Count() == 1);
            Assert.IsTrue(urls.First().ToString().Equals("http://is.gd/79omj"));
            Assert.IsTrue(hashtags.Count() == 2);
            Assert.IsTrue(hashtags.First().Equals("#omg"));
        }

        //[jd] See CodePlex work item 14
        [Test]
        public void Can_gracefully_handle_bad_urls()
        {
            var tweet = new TwitterStatus
                            {
                                Text =
                                    "This tweet has a bad url !!!!://www.mtvla.com/"
                            };

            IEnumerable<Uri> urls = tweet.TextLinks;
            Assert.IsFalse( urls.Any());

        }
    }
}