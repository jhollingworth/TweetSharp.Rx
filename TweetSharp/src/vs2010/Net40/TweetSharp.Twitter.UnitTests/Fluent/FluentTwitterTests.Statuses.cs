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
using System.IO;
using System.Linq;
using TweetSharp.Extensions;
using NUnit.Framework;
using TweetSharp.Twitter.Fluent;
using TweetSharp.Twitter.Extensions;
using TweetSharp.Twitter.UnitTests.Helpers;

namespace TweetSharp.Twitter.UnitTests.Fluent
{
    partial class FluentTwitterTests
    {
        [Test]
        [Category("Statuses")]
        public void Can_show_specific_status()
        {
            var twitter = FluentTwitter.CreateRequest()
                .Statuses().Show(123).AsJson();

            Console.WriteLine(twitter.ToString());

            var response = twitter.Request();
            IgnoreFailWhales(response);

            var status = response.AsStatus();
            Assert.IsNotNull(status);
        }

        [Test]
        [Category("Statuses")]
        public void Can_update_status()
        {
            if (!ALLOW_POSTS)
            {
                Assert.Ignore("This test makes a live status update");
            }
            const string text = "status has a #hashtag";
            var twitter = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .Statuses().Update(text).AsJson();

            Console.WriteLine(twitter.ToString());

            var response = twitter.Request();
            IgnoreFailWhales(response);

            var status = response.AsStatus();
            Assert.IsNotNull(status);
            Assert.AreEqual(text, status.Text);
        }

        [Test]
        [Category("Statuses")]
        public void Can_update_status_in_japanese()
        {
            if (!ALLOW_POSTS)
            {
                Assert.Ignore("This test makes a live status update");
            }
            string text = "羽翹彈唱 - 最後一課 " + DateTime.Now.ToShortDateString();
            var token = LoadToken("access");
            var twitter = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY,
                                  OAUTH_CONSUMER_SECRET,
                                  token.Token,
                                  token.TokenSecret)
                .Statuses().Update(text).AsJson();

            Console.WriteLine(twitter.ToString());

            var response = twitter.Request();
            IgnoreFailWhales(response);

            var status = response.AsStatus();
            Assert.IsNotNull(status);
            Assert.AreEqual(text, status.Text);
        }

        [Test]
        [Category("Statuses")]
        [ExpectedException(typeof(ArgumentException))]
        public void Can_fail_on_status_that_is_too_short()
        {
            var text = String.Empty;

            var twitter = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token,
                                  TWITTER_OAUTH.TokenSecret)
                .Statuses()
                .Update(text)
                .AsJson();

            Console.WriteLine(twitter.ToString());
            twitter.Request();
        }

        [Test]
        [Category("Statuses")]
        public void Can_get_mentions()
        {
            var twitter = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .Statuses().Mentions()
                .AsXml();

            Console.WriteLine(twitter.AsUrl());

            var response = twitter.Request();
            IgnoreFailWhales(response);

            var status = response.AsStatuses();
            Assert.IsNotNull(status);
        }

        [Test]
        [Category("Statuses")]
        public void Can_get_replies_with_count_limit()
        {
            var twitter = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .Statuses().Mentions()
                .Take(1).AsJson();

            Console.WriteLine(twitter.ToString());

            var response = twitter.Request();
            IgnoreFailWhales(response);

            var statuses = response.AsStatuses();
            if (!statuses.Any())
            {
                Assert.Ignore("No replies found");
            }
            Assert.IsTrue(statuses.Count() == 1, "Did not retrieve a count limited result");
        }

        [Test]
        [Category("Statuses")]
        public void Can_destroy_status()
        {
            if (!ALLOW_POSTS)
            {
                Assert.Ignore("This test makes a live status update");
            }
            var create = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .Statuses().Update("This status is destined for destruction.")
                .AsJson();

            Console.WriteLine(create.ToString());

            var response = create.Request();
            IgnoreFailWhales(response);

            var status = response.AsStatus();
            Assert.IsNotNull(status);

            var destroy = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .Statuses().Destroy(status.Id)
                .AsJson();

            response = destroy.Request();
            IgnoreFailWhales(response);
        }

        [Test]
        [Category("Statuses")]
        public void Can_get_friends_timeline_with_since_id()
        {
            var twitter = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .Statuses().OnFriendsTimeline()
                .Since(200)
                .AsJson();

            Console.WriteLine(twitter.ToString());

            var response = twitter.Request();
            IgnoreFailWhales(response);
        }

        [Test]
        [Category("Statuses")]
        public void Can_get_friends_timeline_with_since_id_using_oauth()
        {
            var token = LoadToken("access");
            var twitter = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, token.Token, token.TokenSecret)
                .Statuses().OnFriendsTimeline()
                .Since(200)
                .AsJson();

            Console.WriteLine(twitter.ToString());

            var response = twitter.Request();
            IgnoreFailWhales(response);
            Assert.That(response.ResponseHttpStatusCode == 200);
        }

        [Test]
        [Category("Statuses")]
        public void Can_get_friends_timeline_with_count()
        {
            var twitter = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .Statuses().OnFriendsTimeline()
                .Take(5)
                .AsJson();

            Console.WriteLine(twitter.ToString());

            var response = twitter.Request();
            IgnoreFailWhales(response);

            var count = response.AsStatuses().Count();
            Assert.LessOrEqual(count, 5);
        }

        [Test]
        [Category("Statuses")]
        public void Can_get_home_timeline_with_count()
        {
            var twitter = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .Statuses().OnHomeTimeline()
                .Take(5).AsJson();

            Console.WriteLine(twitter.ToString());

            var response = twitter.Request();
            IgnoreFailWhales(response);

            var count = response.AsStatuses().Count();
            Assert.LessOrEqual(count, 5);
        }

        [Test]
        [Category("Statuses")]
        public void Can_get_users_timeline_with_screenname()
        {
            var twitter = FluentTwitter.CreateRequest()
                .Statuses().OnUserTimeline().For(TWITTER_RECIPIENT_SCREEN_NAME)
                .AsJson();

            Console.WriteLine(twitter.ToString());

            var response = twitter.Request();
            IgnoreFailWhales(response);

            var count = response.AsStatuses().Count();
            Assert.LessOrEqual(count, 20);
        }

        [Test]
        [Category("Statuses")]
        public void Can_get_users_timeline_with_id()
        {
            var twitter = FluentTwitter.CreateRequest()
                .Statuses()
                .OnUserTimeline().For(TWITTER_RECIPIENT_ID)
                .AsJson();

            Console.WriteLine(twitter.ToString());

            var response = twitter.Request();
            IgnoreFailWhales(response);

            var count = response.AsStatuses().Count();
            Assert.LessOrEqual(count, 20);
        }

        [Test]
        [Category("Statuses")]
        [ExpectedException(typeof(TweetSharpException))]
        public void Can_fail_on_status_that_is_too_long()
        {
            const string text = "123456789012345678901234567890123456789012345678901234567890" +
                                "123456789012345678901234567890123456789012345678901234567890" +
                                "123456789012345678901234567890123456789012345678901234567890" +
                                "123456789012345678901234567890123456789012345678901234567890" +
                                "123456789012345678901234567890123456789012345678901234567890";

            var twitter = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .Statuses()
                .Update(text)
                .AsJson();

            Console.WriteLine(twitter.ToString());

            twitter.Request();
        }

        [Test]
        [Category("Statuses")]
        public void Can_get_public_timeline()
        {
            var twitter = FluentTwitter.CreateRequest()
                .Statuses().OnPublicTimeline()
                .AsJson();

            Console.WriteLine(twitter.ToString());

            var response = twitter.Request();
            IgnoreFailWhales(response);

            var statuses = response.AsStatuses();
            Assert.IsNotNull(statuses);

            foreach (var status in statuses)
            {
                Console.WriteLine(status.Text);
            }
        }

        [Test]
        [Category("Statuses")]
        [Ignore("This test makes 100 requests in a row")]
        public void Can_get_statuses_reliably()
        {
            for (var i = 0; i < 100; i++)
            {
                var results = FluentTwitter.CreateRequest()
                    .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                    .Statuses().OnFriendsTimeline().AsJson().Request();

                var response = results.AsStatuses();
                Console.WriteLine("{0} statuses retrieved", response.Count());
            }
        }

        [Test]
        [Category("Statuses")]
        public void Can_deserialize_response_text_to_statuses_with_full_user_detail()
        {
            var twitter = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .Statuses().OnUserTimeline()
                .AsXml();

            // Get my posts from twitter
            var response = twitter.Request();

            // Convert them to data classes
            var statuses = response.AsStatuses();
            Assert.IsNotNull(statuses);
        }

        [Test]
        [Category("Statuses")]
        public void Can_get_user_timeline_before_status_id()
        {
            const int id = 1497943023;

            var twitter = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .Statuses().OnUserTimeline()
                .Before(id);

            Console.WriteLine(twitter.AsUrl());
            var response = twitter.Request();
            IgnoreFailWhales(response);
            var statuses = response.AsStatuses();
            Assert.IsNotNull(statuses, "Could not cast response to statuses");

            foreach (var status in statuses)
            {
                Assert.IsTrue(status.Id <= id, "Returned status had greater ID than the max");
            }
        }

        [Test]
        [Category("Statuses")]
        public void Can_get_friends_timeline_before_status_id()
        {
            // Get last status ID to use in test
            var initial = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .Statuses().OnFriendsTimeline().AsJson()
                .Request().AsStatuses();

            if (initial == null)
            {
                Assert.Ignore(
                                 "No statuses could be found for the testing account's friends timeline. Check other tests or re-run.");
            }
            else
            {
                var firstStatus = initial.FirstOrDefault();
                if (firstStatus == null)
                {
                    Assert.Ignore(
                                     "No statuses could be found for the testing account's friends timeline. Check other tests or re-run.");
                }
                else
                {
                    var id = firstStatus.Id;

                    var twitter = FluentTwitter.CreateRequest()
                        .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                        .Statuses().OnFriendsTimeline()
                        .Before(id);

                    Console.WriteLine(twitter.AsUrl());
                    var response = twitter.Request();
                    var statuses = response.AsStatuses();
                    Assert.IsNotNull(statuses, "Could not cast response to statuses");

                    foreach (var status in statuses)
                    {
                        Assert.IsTrue(status.Id <= id, "Returned status had greater ID than the max");
                    }
                }
            }
        }

        [Test]
        [Category("Statuses")]
        public void Can_filter_status_entities_after_fetching()
        {
            var twitter = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .Statuses().OnFriendsTimeline()
                .Take(200);

            var response = twitter.Request();
            var statuses = response.AsStatuses();
            Assert.IsNotNull(statuses);
            var filtered = statuses.Since(7.Days().Ago());
            Assert.IsNotNull(filtered);
        }


        [Test]
        [Category("Statuses")]
        public void Can_fetch_large_home_timeline_with_json()
        {
            var twitter = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .Statuses().OnHomeTimeline().Take(200)
                .AsJson();

            var response = twitter.Request();
            var statuses = response.AsStatuses();
            Assert.IsNotNull(statuses);
        }

        [Test]
        [Category("Statuses")]
        public void Can_fetch_large_home_timeline_with_xml()
        {
            var twitter = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .Statuses().OnHomeTimeline().Take(200)
                .AsXml();

            var response = twitter.Request();
            var statuses = response.AsStatuses();
            Assert.IsNotNull(statuses);
        }

        [Test]
        [Category("Statuses")]
        public void Can_fetch_empty_timeline_and_not_throw()
        {
            var twitter = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .Statuses().OnHomeTimeline();

            var response = twitter.Request();
            var statuses = response.AsStatuses();
            Assert.IsNotNull(statuses);
            
            if (!statuses.Any())
            {
                return;
            }

            var lastId = statuses.OrderBy(s => s.Id).Last().Id;
            twitter = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .Statuses().OnHomeTimeline().Since(lastId);
            response = twitter.Request();
            var newStatuses = response.AsStatuses();
            Assert.IsNotNull(newStatuses);
        }

        [Test]
        [Category("Statuses")]
        public void Can_get_empty_collection_and_deserialize_it()
        {
            var latest = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .Statuses().OnHomeTimeline();

            var response = latest.Request();
            if(response.IsTwitterError)
            {
                Assert.Ignore("Can't continue test due to Twitter failure.");
            }

            var sinceId = response.AsStatuses().First().Id;
            var since = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .Statuses().OnHomeTimeline().Since(sinceId);

            response = since.Request();
            if(!response.Response.Equals("[]"))
            {
                Assert.Ignore("A new tweet came back in the time we requested; try another time.");
            }

            var statuses = response.AsStatuses();
            Assert.IsNotNull(statuses);
            Assert.AreEqual(0, statuses.Count());
        }

#if !Smartphone
        [Test]
        [Category("Statuses")]
        public void Can_get_large_follower_set_from_friends_timeline()
        {
            Assert.IsTrue(File.Exists("Samples/large-list.json"));
            var f = File.Open("Samples/large-list.json", FileMode.Open, FileAccess.Read);
            var reader = new StreamReader(f);
            var json = reader.ReadToEnd();
            reader.Dispose();
            f.Dispose();

            var statuses = json.ToResult().AsStatuses();
            Assert.IsNotNull(statuses);
            Console.WriteLine(statuses.Count());
        }
#endif
    }
}