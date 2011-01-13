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
using System.Security;
using NUnit.Framework;
using TweetSharp.Twitter.Fluent;
using TweetSharp.Twitter.Model;
using TweetSharp.Twitter.Extensions;

namespace TweetSharp.Twitter.UnitTests.Fluent
{
    partial class FluentTwitterTests
    {
        [Test]
        [Category("Geo")]
        public void Can_correctly_assign_location_to_status_update_url()
        {
            var twitter = FluentTwitter.CreateRequest()
                .Statuses().Update("Testing geo-tagging, everybody!")
                .From(37.78029, -122.39697)
                .AsJson();

            Console.WriteLine(twitter.AsUrl());

            Assert.IsTrue(twitter.AsUrl().Contains("lat="));
            Assert.IsTrue(twitter.AsUrl().Contains("&long="));
        }

        [Test]
        [Category("Geo")]
        public void Can_send_geo_tagged_tweet()
        {
            if (!ALLOW_POSTS)
            {
                Assert.Ignore("This test makes a live status update");
            }
            var twitter = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .Statuses().Update("Testing geo-tagging, everybody!")
                .From(37.78029, -122.39697)
                .AsJson();

            Console.WriteLine(twitter.AsUrl());

            Assert.IsTrue(twitter.AsUrl().Contains("lat="));
            Assert.IsTrue(twitter.AsUrl().Contains("&long="));

            var response = twitter.Request();
            Assert.IsNotNull(response);

            var status = response.AsStatus();
            Assert.IsNotNull(status);
        }

        [Test]
        [Category("Geo")]
        public void Can_deserialize_geo_elements_status_without_geo_data()
        {
            // Twitter does not return the geo-tag data in the Status element
            // of a User response, so we are currently guaranteed to not get
            // geo-data back with this test

            var twitter = FluentTwitter.CreateRequest()
                .Users().ShowProfileFor(TWITTER_USERNAME)
                .Request();

            var user = twitter.AsUser();
            var status = user.Status;

            Console.WriteLine("User's geo enabled status: {0}", user.IsGeoEnabled);
            Console.WriteLine("Status's geo data: {0}, {1}",
                              status.Location != null ? status.Location.Coordinates.Latitude.ToString() : "null",
                              status.Location != null ? status.Location.Coordinates.Longitude.ToString() : "null");
        }

        [Test]
        [Category("Geo")]
        public void Can_convert_geo_location_from_json()
        {
            // This is a live sample
            const string json =
                "{\"coordinates\":{\"coordinates\":[-122.39697,37.78029],\"type\":\"Point\"},\"favorited\":false,\"created_at\":\"Sat Mar 06 11:05:55 +0000 2010\",\"truncated\":false,\"geo\":{\"coordinates\":[37.78029,-122.39697],\"type\":\"Point\"},\"in_reply_to_user_id\":null,\"place\":null,\"source\":\"web\",\"in_reply_to_screen_name\":null,\"contributors\":null,\"user\":{\"contributors_enabled\":false,\"created_at\":\"Sat Feb 28 22:29:02 +0000 2009\",\"profile_link_color\":\"FF3300\",\"description\":\"Don't follow me.  I ain't real and I don't say anything interesting.  I'm a bot that runs tests. \",\"geo_enabled\":true,\"profile_background_tile\":false,\"following\":false,\"profile_background_color\":\"709397\",\"verified\":false,\"notifications\":false,\"url\":\"http://www.tweetsharp.com\",\"profile_sidebar_fill_color\":\"A0C5C7\",\"location\":\"Transylvania\",\"time_zone\":\"Eastern Time (US & Canada)\",\"profile_sidebar_border_color\":\"86A4A6\",\"followers_count\":8,\"profile_image_url\":\"http://a1.twimg.com/profile_images/735747418/twitterProfilePhoto_normal.jpg\",\"protected\":false,\"friends_count\":7,\"screen_name\":\"TootTootMcGoot\",\"name\":\"TweetSharpTest Acct\",\"statuses_count\":2084,\"profile_text_color\":\"333333\",\"id\":22302843,\"lang\":\"en\",\"profile_background_image_url\":\"http://a1.twimg.com/profile_background_images/80878882/twitterProfilePhoto.jpg\",\"utc_offset\":-18000,\"favourites_count\":3},\"in_reply_to_status_id\":null,\"id\":10069479042,\"text\":\"Testing geo-tagging, everybody!\"}";

            var result = new TwitterResult {Response = json};
            var status = result.AsStatus();

            Assert.IsNotNull(status, "Status did not deserialize correctly");
            Assert.IsNotNull(status.Location, "Location was null where geo data was present");
            Assert.IsTrue(status.User.IsGeoEnabled.Value);

            Console.WriteLine("User's geo enabled status: {0}", status.User.IsGeoEnabled);
            Console.WriteLine("Status's geo data: {0}, {1}",
                              status.Location.Coordinates.Latitude,
                              status.Location.Coordinates.Longitude);
        }

#if !Smartphone
        [Test]
        [Category("Geo")]
        public void Can_convert_geo_location_from_xml()
        {
            // This is XML with GeoRSS formatting
            var xml =
                "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" +
                "<status>" +
                "    <created_at>Tue Apr 07 22:52:51 +0000 2009</created_at>" +
                "    <id>1472669360</id>" +
                "    <text>At least I can get your humor through tweets. RT @abdur: I don't mean this in a bad way, but genetically speaking your a cul-de-sac.</text>" +
                "    <source>" + SecurityElement.Escape("<a href=\"http://www.tweetdeck.com/\">TweetDeck</a>") +
                "</source>" +
                "    <truncated>false</truncated>" +
                "    <in_reply_to_status_id>1472669230</in_reply_to_status_id>" +
                "    <in_reply_to_user_id>10759032</in_reply_to_user_id>" +
                "    <favorited>false</favorited>" +
                "    <in_reply_to_screen_name></in_reply_to_screen_name>" +
                "    <geo xmlns:georss=\"http://www.georss.org/georss\">" +
                "        <georss:point>37.78029 -122.39697</georss:point>" +
                "    </geo>" +
                "    <user>" +
                "        <id>1401881</id>" +
                "        <name>Doug Williams</name>" +
                "        <screen_name>dougw</screen_name>" +
                "        <location>San Francisco, CA</location>" +
                "        <description>Twitter API Support. Internet, greed, users, dougw and opportunities are my passions.</description>" +
                "        <profile_image_url>http://s3.amazonaws.com/twitter_production/profile_images/59648642/avatar_normal.png</profile_image_url>" +
                "        <url>http://www.igudo.com</url>" +
                "        <protected>false</protected>" +
                "        <followers_count>1027</followers_count>" +
                "        <profile_background_color>9ae4e8</profile_background_color>" +
                "        <profile_text_color>000000</profile_text_color>" +
                "        <profile_link_color>0000ff</profile_link_color>" +
                "        <profile_sidebar_fill_color>e0ff92</profile_sidebar_fill_color>" +
                "        <profile_sidebar_border_color>87bc44</profile_sidebar_border_color>" +
                "        <friends_count>293</friends_count>" +
                "        <created_at>Sun Mar 18 06:42:26 +0000 2007</created_at>" +
                "        <favourites_count>0</favourites_count>" +
                "        <utc_offset>-18000</utc_offset>" +
                "        <time_zone>" + SecurityElement.Escape("Eastern Time (US & Canada)") + "</time_zone>" +
                "        <profile_background_image_url>http://s3.amazonaws.com/twitter_production/profile_background_images/2752608/twitter_bg_grass.jpg</profile_background_image_url>" +
                "        <profile_background_tile>false</profile_background_tile>" +
                "        <statuses_count>3390</statuses_count>" +
                "        <notifications>false</notifications>" +
                "        <following>false</following>" +
                "        <geo_enabled>true</geo_enabled>" +
                "        <verified>true</verified>" +
                "    </user>" +
                "</status>";

            var result = new TwitterResult { Response = xml };
            var status = result.AsStatus();

            Assert.IsNotNull(status);
            Assert.IsNotNull(status.Location);
            Assert.IsTrue(status.User.IsGeoEnabled.Value);

            Console.WriteLine("User's geo enabled status: {0}", status.User.IsGeoEnabled);
            Console.WriteLine("Status's geo data: {0}, {1}",
                              status.Location.Coordinates.Latitude,
                              status.Location.Coordinates.Longitude);
        }
#endif

        [Test]
        [Category("Geo")]
        public void Can_compare_locations_within_distance()
        {
            var dimebrainHq = new TwitterGeoLocation(45.4235, -75.6979);
            var twitterHq = new TwitterGeoLocation(37.78029, -122.39697);

            var miles = dimebrainHq.MilesFrom(twitterHq);
            var withinRange = dimebrainHq.IsWithin(miles, twitterHq);

            Assert.IsTrue(withinRange);

            var closeEnoughToBike = dimebrainHq.IsWithin(20/*miles*/, twitterHq);
            Assert.IsFalse(closeEnoughToBike);
        }

        [Test]
        public void Can_deserialize_geo_tagging_in_xml_collection()
        {
            var xml =
                @"<?xml version=""1.0"" encoding=""UTF-8""?>
                    <statuses type=""array"">
                    <status>
                      <created_at>Fri Nov 20 00:40:15 +0000 2009</created_at>
                      <id>5874442545</id>
                      <text>test</text>
                      <source>&lt;a href=&quot;http://code.google.com/p/proctweet&quot; rel=&quot;nofollow&quot;&gt;ProcTweet&lt;/a&gt;</source>
                      <truncated>false</truncated>
                      <in_reply_to_status_id></in_reply_to_status_id>
                      <in_reply_to_user_id></in_reply_to_user_id>
                      <favorited>false</favorited>
                      <in_reply_to_screen_name></in_reply_to_screen_name>
                      <user>
                        <id>17590659</id>
                        <name>Procule le Guizou</name>
                        <screen_name>procule</screen_name>
                        <location>Montr&#233;al</location>
                        <description>Je veux, je vis, j'essaie, j'comprends</description>
                        <profile_image_url>http://a3.twimg.com/profile_images/458217617/n713487518_113847_9663_normal.jpg</profile_image_url>
                        <url></url>
                        <protected>false</protected>
                        <followers_count>37</followers_count>
                        <profile_background_color>9AE4E8</profile_background_color>
                        <profile_text_color>333333</profile_text_color>
                        <profile_link_color>0084B4</profile_link_color>
                        <profile_sidebar_fill_color>DDFFCC</profile_sidebar_fill_color>
                        <profile_sidebar_border_color>BDDCAD</profile_sidebar_border_color>
                        <friends_count>37</friends_count>
                        <created_at>Mon Nov 24 11:57:20 +0000 2008</created_at>
                        <favourites_count>3</favourites_count>
                        <utc_offset>-18000</utc_offset>
                        <time_zone>Eastern Time (US &amp; Canada)</time_zone>
                        <profile_background_image_url>http://s.twimg.com/a/1258667182/images/themes/theme16/bg.gif</profile_background_image_url>
                        <profile_background_tile>true</profile_background_tile>
                        <statuses_count>462</statuses_count>
                        <notifications>false</notifications>
                        <geo_enabled>true</geo_enabled>
                        <verified>false</verified>
                        <following>false</following>
                      </user>
                      <geo xmlns:georss=""http://www.georss.org/georss"">
                        <georss:point>45.545447 -73.639076</georss:point>
                      </geo>
                    </status>
                    </statuses>
                    ";

            var result = new TwitterResult {Response = xml};
            var response = result.AsStatuses();
            Assert.IsNotNull(response);
        }
    }
}