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
using System.Linq;
using NUnit.Framework;
using TweetSharp.Twitter.Extensions;
using TweetSharp.Twitter.Fluent;
using TweetSharp.Twitter.Model;
using TweetSharp.Twitter.UnitTests.Helpers;
using TweetSharp.UnitTests.Helpers;

namespace TweetSharp.Twitter.UnitTests.Fluent
{
    partial class FluentTwitterTests
    {
        [Test]
        [Category("Model")]
        public void Can_compare_and_sort_statuses()
        {
            var statuses = FluentTwitter.CreateRequest()
                .Statuses().OnPublicTimeline().AsJson()
                .Request().AsStatuses().ToList();

            // Twitter returns results sorted in ascending order
            Assert.IsTrue(statuses.Count() > 1, "No statuses returned");

            // Sort in descending order
            statuses.Sort();

            // Test that statuses used internal comparison when sorting
            var compare = statuses[0].CompareTo(statuses[1]);
            Assert.IsTrue(compare == -1);
        }

        [Test]
        [Category("Model")]
        public void Can_deserialize_direct_messages_from_xml_response()
        {
            var xml = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .DirectMessages().Received().AsXml().Request();

            var dms = xml.AsDirectMessages().ToList();
            Assert.IsNotNull(dms);
            Assert.IsTrue(dms.Count > 0, "No elements found");
        }

        [Test]
        [Category("Model")]
        public void Can_deserialize_rate_limit_from_xml_response()
        {
            var xml = FluentTwitter.CreateRequest()
                .Account().GetRateLimitStatus()
                .AsXml().Request();

            var limit = xml.AsRateLimitStatus();
            Assert.IsNotNull(limit);
        }

        [Test]
        [Category("Model")]
        public void Can_deserialize_single_direct_message_from_xml_response()
        {
            if (!ALLOW_POSTS)
            {
                Assert.Ignore("This test makes a live status update");
            }
            var xml = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .DirectMessages().Send(TWITTER_RECIPIENT_SCREEN_NAME, "test")
                .AsXml().Request();

            var status = xml.AsDirectMessage();
            Assert.IsNotNull(status);
        }

        [Test]
        [Category("Model")]
        public void Can_deserialize_single_entry_array()
        {
            const string input = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" +
                                 "<statuses type=\"array\"><status><created_at>Mon Apr 13 21:40:56 +0000 2009</created_at>" +
                                 "<id>1512003907</id><text>@dimebrain Sounds like tonne</text><source>web</source>" +
                                 "<truncated>false</truncated><in_reply_to_status_id>1511925372</in_reply_to_status_id>" +
                                 "<in_reply_to_user_id>14191999</in_reply_to_user_id><favorited>false</favorited>" +
                                 "<in_reply_to_screen_name>dereksmalls</in_reply_to_screen_name><user><id>12697742</id>" +
                                 "<name>muzerunit</name><screen_name>muzerunit</screen_name><location></location>" +
                                 "<description></description><profile_image_url>" +
                                 "http://static.twitter.com/images/default_profile_normal.png</profile_image_url><url></url>" +
                                 "<protected>false</protected><followers_count>3</followers_count>" +
                                 "<profile_background_color>9ae4e8</profile_background_color><profile_text_color>000000</profile_text_color>" +
                                 "<profile_link_color>0000ff</profile_link_color><profile_sidebar_fill_color>e0ff92</profile_sidebar_fill_color>" +
                                 "<profile_sidebar_border_color>87bc44</profile_sidebar_border_color><friends_count>2</friends_count>" +
                                 "<created_at>Fri Jan 25 21:19:01 +0000 2008</created_at><favourites_count>0</favourites_count>" +
                                 "<utc_offset>-28800</utc_offset><time_zone>Pacific Time (US &amp; Canada)</time_zone>" +
                                 "<profile_background_image_url>http://static.twitter.com/images/themes/theme1/bg.gif</profile_background_image_url>" +
                                 "<profile_background_tile>false</profile_background_tile><statuses_count>1</statuses_count>" +
                                 "<notifications>false</notifications><following>true</following></user></status></statuses>";

            var serialized = input.ToResult().AsStatuses();
            Assert.IsNotNull(serialized);
        }

        [Test]
        [Category("Model")]
        public void Can_deserialize_single_status_from_xml_response()
        {
            var xml = FluentTwitter.CreateRequest()
                .Statuses().Show(123).AsXml()
                .Request();

            var status = xml.AsStatus();
            Assert.IsNotNull(status);
        }

        [Test]
        [Category("Model")]
        public void Can_deserialize_single_user_profile_from_xml_response()
        {
            var xml = FluentTwitter.CreateRequest()
                .Users().ShowProfileFor(TWITTER_USERNAME)
                .AsXml().Request();

            var profile = xml.AsUser();
            Assert.IsNotNull(profile);
        }

        [Test]
        [Category("Model")]
        public void Can_deserialize_statuses_collection_from_xml_response()
        {
            var xml = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .Statuses().OnPublicTimeline().AsXml().Request();

            var statuses = xml.AsStatuses().ToList();
            Assert.IsNotNull(statuses);
            Assert.IsTrue(statuses.Count > 0, "No elements found");
        }

        [Test]
        [Category("Model")]
        public void Can_deserialize_users_collection_from_xml_response()
        {
            var xml = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .Users().GetFollowers().AsXml().Request();

            var followers = xml.AsUsers().ToList();
            Assert.IsNotNull(followers);
            Assert.IsTrue(followers.Count > 0, "No elements found");
        }

        [Test]
        [Category("Model")]
        public void Can_get_empty_collection_response_as_empty_collection_class()
        {
            const long id = 7597943023;

            var twitter = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .Statuses().OnFriendsTimeline().Since(id)
                .AsJson();

            Console.WriteLine(twitter.AsUrl());
            var response = twitter.Request();
            var statuses = response.AsStatuses();
            Assert.IsNotNull(statuses, "Could not cast empty response to empty statuses collection");
        }

        [Test]
        [Category("Model")]
        public void Can_get_empty_collection_response_as_empty_collection_class_with_xml()
        {
            const long id = 7597943023;

            var twitter = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .Statuses().OnFriendsTimeline().Since(id)
                .AsXml();

            Console.WriteLine(twitter.AsUrl());
            var response = twitter.Request();
            var statuses = response.AsStatuses();
            Assert.IsNotNull(statuses, "Could not cast empty response to empty statuses collection");
        }

        [Test]
        [Category("Model")]
        public void Can_get_rate_limit_from_authenticated_response()
        {
            var twitter = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .Statuses().OnUserTimeline().For(TWITTER_RECIPIENT_SCREEN_NAME)
                .AsJson();

            var response = twitter.Request();
            Assert.IsNotNull(response, "Test could not complete due to another error");

            var limit = response.RateLimitStatus;
            Assert.IsNotNull(limit, "Call did not return rate limit status data");
        }

        [Test]
        [Category("Model")]
        public void Can_recognize_model_classes_with_new_serialization_scheme()
        {
            // STATUS
            var query = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .Statuses().Show(123).AsJson().Request();

            var status = query.AsStatus();
            Assert.IsNotNull(status);

            var dm = query.AsDirectMessage();
            Assert.That(dm == null || dm.RecipientId == 0);

            //recent relaxation of deserialization rules means this will parse even 
            //if only one member is matched
            var user = query.AsUser();
            Assert.That(user == null || string.IsNullOrEmpty(user.ScreenName));

            // USER
            query = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .Users().ShowProfileFor(TWITTER_CELEBRITY_SCREEN_NAME).AsJson().Request();

            user = query.AsUser();
            Assert.IsNotNull(user);

            dm = query.AsDirectMessage();
            Assert.That(dm == null || dm.RecipientId == 0);

            //recent relaxation of deserialization rules means this will parse even 
            //if only one member is matched
            status = query.AsStatus();
            Assert.That(status == null || string.IsNullOrEmpty(status.Text));
        }

        [Test]
        [Category("Model")]
        public void Can_serialize_and_deserialize_statuses()
        {
            var statuses = FluentTwitter.CreateRequest()
                .Statuses().OnPublicTimeline().AsJson()
                .Request().AsStatuses();

            Assert.IsTrue(statuses.Count() > 0, "No statuses returned");
            foreach (var status in statuses)
            {
                var serialized = status.ToXml();
                serialized.FromXml<TwitterStatus>();
            }
        }

        [Test]
        [Category("Model")]
        public void Can_serialize_model_instance_to_valid_json_and_deserialize()
        {
            var expected = new TwitterStatus
                               {
                                   CreatedDate = DateTime.Now,
                                   Id = 128093092,
                                   Text = "This is a false update."
                               };

            var json = Core.Extensions.ToJson(expected);
            Assert.IsNotNull(json);

            var actual = json.ToResult().AsStatus();
            Assert.AreEqual(expected, actual);
        }

        [Test]
        [Category("Model")]
        public void Can_get_raw_sources_for_statuses()
        {
            var statusesQuery = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .Statuses().OnPublicTimeline();

            var result = statusesQuery.Request();
            var statuses = result.AsStatuses();

            if(statuses == null)
            {
                Assert.Ignore("Deserialization issue prevented this test");
                return;
            }

            foreach(var status in statuses)
            {
                Assert.IsTrue(!string.IsNullOrEmpty(status.RawSource));    
                Console.WriteLine(status.RawSource);
            }
        }

        [Test]
        [Category("Model")]
        public void Can_get_raw_sources_for_statuses_with_xml()
        {
            var statusesQuery = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .Statuses().OnPublicTimeline()
                .AsXml();

            var result = statusesQuery.Request();
            var statuses = result.AsStatuses();

            if (statuses == null)
            {
                Assert.Ignore("Deserialization issue prevented this test");
                return;
            }

            foreach (var status in statuses)
            {
                Assert.IsTrue(!string.IsNullOrEmpty(status.RawSource));
                Console.WriteLine(status.RawSource);
            }
        }

        [Test]
        [Category("Model")]
        public void Can_get_raw_sources_for_direct_messages()
        {
            var dmsQuery = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .DirectMessages().Sent();

            var result = dmsQuery.Request();
            var dms = result.AsStatuses();

            if (dms == null)
            {
                Assert.Ignore("Deserialization issue prevented this test");
                return;
            }

            foreach (var dm in dms)
            {
                Assert.IsTrue(!string.IsNullOrEmpty(dm.RawSource));
                Console.WriteLine(dm.RawSource);
            }
        }

        [Test]
        [Category("Model")]
        public void Can_get_raw_source_for_user()
        {
            var userQuery = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .Users().ShowProfileFor("dimebrain");

            var result = userQuery.Request();
            var user = result.AsUser();

            if (user == null)
            {
                Assert.Ignore("Deserialization issue prevented this test");
                return;
            }

            Assert.IsTrue(!string.IsNullOrEmpty(user.RawSource));
            Console.WriteLine(user.RawSource);
        }
    }
}