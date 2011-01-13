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
using TweetSharp.Twitter.Extensions;
using TweetSharp.Twitter.Fluent;
using TweetSharp.Twitter.Model;
using NUnit.Framework;
using TweetSharp.Twitter.UnitTests.Helpers;

namespace TweetSharp.Twitter.UnitTests.Fluent
{
    partial class FluentTwitterTests
    {
        private TwitterList CreateTemporaryList()
        {
            var query = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .Lists().CreatePublicList(TWITTER_USERNAME, TWITTER_TEMPORARY_LIST_SLUG, "test list")
                .AsJson();

            var response = query.Request();
            IgnoreFailWhales(response);
            return response.AsList();
        }

        private TwitterList DeleteList(TwitterList list)
        {
            var query = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .Lists().DeleteList(TWITTER_USERNAME, list.Id)
                .AsJson();

            var response = query.Request();
            IgnoreFailWhales(response);
            return response.AsList();
        }

        private void EnsureListsCapacity()
        {
            var myLists = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .Lists().GetListsBy(TWITTER_USERNAME).AsJson();

            var response = myLists.Request();
            if (response.IsFailWhale)
            {
                Assert.Ignore("Failwhale ahoy");
            }
            var lists = response.AsLists();
            Assert.IsNotNull(lists);

            if (lists.Count() >= 20)
            {
                Assert.Ignore("Cannot perform this test if the authenticating user already owns 20 lists");
            }
        }

        private void RemoveMemberBySlug(TwitterUser user)
        {
            RemoveMember(user, true);
        }

        private void RemoveMemberById(TwitterUser user)
        {
            RemoveMember(user, false);
        }

        private void RemoveMember(TwitterUser user, bool useSlug)
        {
            var removeFromList = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .Lists();

            ITwitterLeafNodeXmlJson query = useSlug
                                                ? removeFromList.RemoveMemberFrom(TWITTER_USERNAME,
                                                                                  TWITTER_PERMANENT_LIST_SLUG, user.Id)
                                                : removeFromList.RemoveMemberFrom(TWITTER_USERNAME,
                                                                                  TWITTER_PERMANENT_LIST_ID, user.Id);

            var removeUser = query.AsJson().Request();
            Assert.IsNotNull(removeUser);
            IgnoreFailWhales(removeUser);
            var listChanged = removeUser.AsList();
            Assert.IsNotNull(listChanged);
        }

        [Test]
        [Category("Lists")]
        public void Can_add_list_member_with_list_id()
        {
            Assert.IsFalse(
                              string.IsNullOrEmpty(TWITTER_NON_MEMBER_OF_PERM_LIST),
                              "Specify a value for 'nonListMember' in setup.xml to run this test");

            var response = FluentTwitter.CreateRequest()
                .Users().ShowProfileFor(TWITTER_NON_MEMBER_OF_PERM_LIST)
                .Request();
            IgnoreFailWhales(response);
            var user = response.AsUser();

            Assert.IsNotNull(user);

            // note you need to pass the ID to this method, you can't use screen name
            var query = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .Lists().AddMemberTo(TWITTER_USERNAME, TWITTER_PERMANENT_LIST_ID, user.Id)
                .AsJson();

            var addUser = query.Request();
            Assert.IsNotNull(addUser);
            IgnoreFailWhales(addUser);

            var listChanged = addUser.AsList();
            Assert.IsNotNull(listChanged);

            RemoveMemberById(user);
        }

        [Test]
        [Category("Lists")]
        public void Can_add_member_with_list_slug()
        {
            Assert.IsFalse(
                              string.IsNullOrEmpty(TWITTER_NON_MEMBER_OF_PERM_LIST),
                              "Specify a value for 'nonListMember' in setup.xml to run this test");

            // note you need to pass the ID to this method, you can't use screen name
            var response = FluentTwitter.CreateRequest()
                .Users().ShowProfileFor(TWITTER_NON_MEMBER_OF_PERM_LIST)
                .Request();
            
            IgnoreFailWhales(response);
            
            var user = response.AsUser();

            Assert.IsNotNull(user);

            var addToList = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .Lists().AddMemberTo(TWITTER_USERNAME, TWITTER_PERMANENT_LIST_SLUG, user.Id)
                .AsJson();

            var addUser = addToList.Request();
            Assert.IsNotNull(addUser);

            var listChanged = addUser.AsList();
            Assert.IsNotNull(listChanged);

            RemoveMemberBySlug(user);
        }

        [Test]
        [Category("Lists")]
        public void Can_delete_all_temporary_lists()
        {
            var myLists = FluentTwitter.CreateRequest()
               .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
               .Lists().GetListsBy(TWITTER_USERNAME).AsJson();

            var response = myLists.Request();
            IgnoreFailWhales(response);
            var lists = response.AsLists();
            foreach (var list in lists.Where(l => l.Slug.ToLower().Contains(TWITTER_TEMPORARY_LIST_SLUG.ToLower())))
            {
                DeleteList(list);
            }
        }

        [Test]
        [Category("Lists")]
        public void Can_create_and_then_delete_list_by_slug()
        {
            EnsureListsCapacity();

            Assert.IsFalse(
                              string.IsNullOrEmpty(TWITTER_TEMPORARY_LIST_SLUG),
                              "Specify a value for 'tempListName' in setup.xml to run this test.");

            var list = CreateTemporaryList();

            var query = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .Lists().DeleteList(TWITTER_USERNAME, list.Slug)
                .AsJson();

            var response = query.Request();
            IgnoreFailWhales(response);
            var deleted = response.AsList();
            Assert.IsNotNull(deleted);
            Assert.AreEqual(deleted.Id, list.Id);
        }

        [Test]
        [Category("Lists")]
        public void Can_create_public_list()
        {
            EnsureListsCapacity();

            var list = default(TwitterList);
            try
            {
                Assert.IsFalse(
                                  string.IsNullOrEmpty(TWITTER_TEMPORARY_LIST_SLUG),
                                  "Specify a value for 'tempListName' in setup.xml to run this test.");

                list = CreateTemporaryList();
                Assert.IsNotNull(list);
                Assert.AreEqual(TWITTER_TEMPORARY_LIST_SLUG, list.Name);
            }
            finally
            {
                DeleteList(list);
            }
        }

        [Test]
        [Category("Lists")]
        public void Can_delete_list_by_slug_with_oauth()
        {
            Assert.IsFalse(
                              string.IsNullOrEmpty(TWITTER_TEMPORARY_LIST_SLUG),
                              "Specify a value for 'tempListName' in setup.xml to run this test.");

            var list = CreateTemporaryList();

            // load access token from a previous test
            var token = LoadToken("access");
            if (token == null || string.IsNullOrEmpty(token.Token) || string.IsNullOrEmpty(token.TokenSecret))
            {
                Assert.Ignore("OAuth not configured");
            }
            var query = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY,
                                  OAUTH_CONSUMER_SECRET,
                                  token.Token,
                                  token.TokenSecret)
                .Lists().DeleteList(TWITTER_USERNAME, list.Slug)
                .AsJson();

            var response = query.Request();
            IgnoreFailWhales(response);

            var deleted = response.AsList();
            Assert.IsNotNull(deleted);
            Assert.AreEqual(deleted.Slug, list.Slug);
        }

        [Test]
        [Category("Lists")]
        public void Can_follow_and_unfollow_list()
        {
            Assert.IsFalse(
                              string.IsNullOrEmpty(TWITTER_FOLLOW_LIST_OWNER),
                              "Specify a value for 'followListOwner' in setup.xml to run this test");

            Assert.IsFalse(
                              string.IsNullOrEmpty(TWITTER_FOLLOW_LIST_SLUG),
                              "Specify a value for 'followListSlug' in setup.xml to run this test");

            var follow = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .Lists().Follow(TWITTER_FOLLOW_LIST_OWNER, TWITTER_FOLLOW_LIST_SLUG)
                .AsJson();

            var addMe = follow.Request();
            Assert.IsNotNull(addMe);
            IgnoreFailWhales(addMe);
            var listChanged = addMe.AsList();
            Assert.IsNotNull(listChanged);

            var unfollow = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .Lists().Unfollow(TWITTER_FOLLOW_LIST_OWNER, TWITTER_FOLLOW_LIST_SLUG)
                .AsJson();

            var removeMe = unfollow.Request();
            Assert.IsNotNull(removeMe);

            listChanged = removeMe.AsList();
            Assert.IsNotNull(listChanged);
        }

        [Test]
        [Category("Lists")]
        public void Can_get_and_page_list_members()
        {
            Assert.IsTrue(
                             TWITTER_PERMANENT_LIST_ID != 0,
                             "Specify a value for 'listId' in setup.xml to run this test.");

            var query = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .Lists().GetMembersOf(TWITTER_USERNAME, TWITTER_PERMANENT_LIST_ID).CreateCursor()
                .AsJson();

            var url = query.AsUrl();
            Console.WriteLine(url);

            var response = query.Request();
            IgnoreFailWhales(response);

            if (response.ResponseHttpStatusCode == 404)
            {
                Assert.Ignore("Could not find the specified list; aborting test.");
            }

            var lists = response.AsUsers();
            Assert.IsNotNull(lists);
        }

        [Test]
        [Category("Lists")]
        public void Can_get_and_page_list_members_with_slug()
        {
            Assert.IsFalse(
                              string.IsNullOrEmpty(TWITTER_PERMANENT_LIST_SLUG),
                              "Specify a value for 'listName' in setup.xml to run this test.");

            var query = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .Lists().GetMembersOf(TWITTER_USERNAME, TWITTER_PERMANENT_LIST_SLUG).CreateCursor()
                .AsJson();

            var url = query.AsUrl();
            Console.WriteLine(url);

            var response = query.Request();
            IgnoreFailWhales(response);

            if (response.ResponseHttpStatusCode == 404)
            {
                Assert.Ignore("Could not find the specified list; aborting test.");
            }

            var members = response.AsUsers();
            Assert.IsNotNull(members);
        }

        [Test]
        [Category("Lists")]
        public void Can_get_list_by_id()
        {
            var query = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .Lists().GetListBy(TWITTER_USERNAME, TWITTER_PERMANENT_LIST_ID)
                .AsJson();

            var url = query.AsUrl();
            Console.WriteLine(url);

            var response = query.Request();
            IgnoreFailWhales(response);

            var list = response.AsList();
            Assert.IsNotNull(list);
            Assert.AreEqual(TWITTER_PERMANENT_LIST_SLUG, list.Slug);
        }

        [Test]
        [Category("Lists")]
        public void Can_get_list_by_slug()
        {
            var query = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .Lists().GetListBy(TWITTER_USERNAME, TWITTER_PERMANENT_LIST_SLUG)
                .AsJson();

            var response = query.Request();
            IgnoreFailWhales(response);

            var list = response.AsList();
            Assert.IsNotNull(list);
            Assert.AreEqual(TWITTER_PERMANENT_LIST_SLUG, list.Slug);
        }

        [Test]
        [Category("Lists")]
        public void Can_get_list_by_slug_as_xml()
        {
            var query = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .Lists().GetListBy(TWITTER_USERNAME, TWITTER_PERMANENT_LIST_SLUG)
                .AsXml();

            var response = query.Request();
            IgnoreFailWhales(response);

            var list = response.AsList();
            Assert.IsNotNull(list);
            Assert.AreEqual(TWITTER_PERMANENT_LIST_SLUG, list.Slug);
        }

        [Test]
        [Category("Lists")]
        public void Can_get_list_memberships()
        {
            var query = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .Lists().GetMemberships(TWITTER_USERNAME).AsJson();

            var url = query.AsUrl();
            Console.WriteLine(url);

            var response = query.Request();
            IgnoreFailWhales(response);

            var statuses = response.AsLists();
            Assert.IsNotNull(statuses);
        }

        [Test]
        [Category("Lists")]
        public void Can_get_list_subscribers()
        {
            var query = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .Lists().GetSubscribersOf(TWITTER_USERNAME, TWITTER_PERMANENT_LIST_ID)
                .AsJson();

            var url = query.AsUrl();
            Console.WriteLine(url);

            var response = query.Request();
            IgnoreFailWhales(response);

            var subscribers = response.AsUsers();
            Assert.IsNotNull(subscribers);
        }

        [Test]
        [Category("Lists")]
        public void Can_get_list_subscriptions()
        {
            var query = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .Lists().GetSubscriptions(TWITTER_USERNAME)
                .AsJson();

            var url = query.AsUrl();
            Console.WriteLine(url);

            var response = query.Request();
            IgnoreFailWhales(response);

            var lists = response.AsLists();
            Assert.IsNotNull(lists);
        }

        [Test]
        [Category("Lists")]
        public void Can_get_list_timeline_lists_style()
        {
            Assert.IsTrue(TWITTER_PERMANENT_LIST_ID != 0,
                          "Specify a value for 'listId' in setup.xml to run this test.");

            var query = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .Lists().GetStatuses(TWITTER_USERNAME, TWITTER_PERMANENT_LIST_ID).AsJson();

            var response = query.Request();
            IgnoreFailWhales(response);

            var statuses = response.AsStatuses();
            Assert.IsNotNull(statuses);
        }

        [Test]
        [Category("Lists")]
        public void Can_get_list_timeline_lists_style_with_slug()
        {
            Assert.IsFalse(string.IsNullOrEmpty(TWITTER_PERMANENT_LIST_SLUG),
                           "Specify a value for 'listname' in setup.xml to run this test.");
            var query = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .Lists().GetStatuses(TWITTER_USERNAME, TWITTER_PERMANENT_LIST_SLUG).AsJson();

            var response = query.Request();
            IgnoreFailWhales(response);

            var statuses = response.AsStatuses();
            Assert.IsNotNull(statuses);
        }

        [Test]
        [Category("Lists")]
        public void Can_get_list_timeline_statuses_style()
        {
            Assert.IsTrue(
                             TWITTER_PERMANENT_LIST_ID != 0,
                             "Specify a value for 'listId' in setup.xml to run this test.");

            var query = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .Statuses().OnListTimeline(TWITTER_USERNAME, TWITTER_PERMANENT_LIST_ID).AsJson();

            var response = query.Request();
            IgnoreFailWhales(response);

            var statuses = response.AsStatuses();
            Assert.IsNotNull(statuses);
        }

        [Test]
        [Category("Lists")]
        public void Can_get_list_timeline_statuses_style_with_slug()
        {
            Assert.IsFalse(
                              string.IsNullOrEmpty(TWITTER_PERMANENT_LIST_SLUG),
                              "Specify a value for 'listName' in setup.xml to run this test.");

            var query = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .Statuses().OnListTimeline(TWITTER_USERNAME, TWITTER_PERMANENT_LIST_SLUG).AsJson();

            var response = query.Request();
            IgnoreFailWhales(response);

            var statuses = response.AsStatuses();
            Assert.IsNotNull(statuses);
        }

        [Test]
        [Category("Lists")]
        public void Can_get_lists_first_page_for_user()
        {
            var query = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .Lists().GetListsBy(TWITTER_USERNAME)
                .CreateCursor()
                .AsJson();

            var response = query.Request();
            IgnoreFailWhales(response);

            var lists = response.AsLists();
            Assert.IsNotNull(lists);
            Assert.IsTrue(lists.Count() > 0);
        }

        [Test]
        [Category("Lists")]
        public void Can_get_lists_in_xml_and_json()
        {
            var json = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .Lists().GetListsBy("danielcrenna")
                .CreateCursor().AsJson();

            var xml = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .Lists().GetListsBy("danielcrenna")
                .CreateCursor().AsXml();

            var jsonResponse = json.Request();
            Assert.IsNotNull(jsonResponse);
            IgnoreFailWhales(jsonResponse);

            var xmlResponse = xml.Request();
            Assert.IsNotNull(xmlResponse);
            IgnoreFailWhales(xmlResponse);

            var jsonLists = jsonResponse.AsLists();
            Assert.IsNotNull(jsonLists);
            
            var xmlJson = Core.Extensions.ToJson(xmlResponse);
            var xmlLists = xmlJson.ToResult().AsLists();
            Assert.IsNotNull(xmlLists);

            Assert.AreEqual(jsonLists.Count(), xmlLists.Count());
        }

        [Test]
        [Category("Lists")]
        public void Can_get_lists_stub_for_user_with_no_lists()
        {
            // http://api.twitter.com/1/user/lists.format
            var query = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .Lists().GetListsBy("garyvee").AsJson();

            var url = query.AsUrl();
            Assert.AreEqual("http://api.twitter.com/1/garyvee/lists.json", url);

            var response = query.Request();
            IgnoreFailWhales(response);
        }

        [Test]
        [Category("Lists")]
        public void Can_page_list_memberships()
        {
            var query = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .Lists().GetMemberships(TWITTER_USERNAME).CreateCursor()
                .AsJson();

            var url = query.AsUrl();
            Console.WriteLine(url);

            var response = query.Request();
            IgnoreFailWhales(response);

            var lists = response.AsLists();
            Assert.IsNotNull(lists);
        }

        [Test]
        [Category("Lists")]
        public void Can_page_list_subscriptions()
        {
            var query = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .Lists().GetSubscriptions(TWITTER_USERNAME).CreateCursor()
                .AsJson();

            var url = query.AsUrl();
            Console.WriteLine(url);

            var response = query.Request();
            IgnoreFailWhales(response);

            var lists = response.AsLists();
            Assert.IsNotNull(lists);
        }

        [Test]
        [Category("Lists")]
        public void Can_page_with_list_subscribers()
        {
            var query = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .Lists().GetSubscribersOf(TWITTER_USERNAME, TWITTER_PERMANENT_LIST_ID)
                .CreateCursor()
                .AsJson();

            var url = query.AsUrl();
            Console.WriteLine(url);

            var response = query.Request();
            IgnoreFailWhales(response);

            var subscribers = response.AsUsers();
            Assert.IsNotNull(subscribers);
        }

        [Test]
        [Category("Lists")]
        public void Can_test_if_user_is_member_of_a_list_when_member_belongs()
        {
            Assert.IsFalse(string.IsNullOrEmpty(TWITTER_MEMBER_OF_PERM_LIST),
                           "Specify a value for 'listMember' in setup.xml to run this test");

            var user = FluentTwitter.CreateRequest()
                .Users().ShowProfileFor(TWITTER_MEMBER_OF_PERM_LIST)
                .Request().AsUser();

            Assert.IsNotNull(user);

            var query = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .Lists().IsUserMemberOf(TWITTER_USERNAME, TWITTER_PERMANENT_LIST_ID, user.Id)
                .AsJson();

            var url = query.AsUrl();
            Console.WriteLine(url);

            var response = query.Request();
            IgnoreFailWhales(response);

            var member = response.AsUser();
            Assert.IsNotNull(member);
        }

        [Test]
        [Category("Lists")]
        public void Can_test_if_user_is_member_of_a_list_when_member_doesnt_belong()
        {
            Assert.IsFalse(string.IsNullOrEmpty(TWITTER_NON_MEMBER_OF_PERM_LIST),
                           "Specify a value for 'nonListMember' in setup.xml to run this test");

            var user = FluentTwitter.CreateRequest()
                .Users().ShowProfileFor(TWITTER_NON_MEMBER_OF_PERM_LIST)
                .Request().AsUser();

            Assert.IsNotNull(user);

            var query = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .Lists().IsUserMemberOf(TWITTER_USERNAME, 8640, user.Id)
                .AsJson();

            var url = query.AsUrl();
            Console.WriteLine(url);

            var response = query.Request();
            IgnoreFailWhales(response);

          
            var member = response.AsUser();
            Assert.That(member == null || string.IsNullOrEmpty(member.ScreenName));
        }

        [Test]
        [Category("Lists")]
        public void Can_test_if_user_is_subscriber_of_a_list_when_user_does_not_subscribe()
        {
            Assert.IsFalse(string.IsNullOrEmpty(TWITTER_NON_SUBSCRIBER_OF_PERM_LIST),
                           "Specify a value for 'nonListSubscriber' in setup.xml to run this test");

            var user = FluentTwitter.CreateRequest()
                .Users().ShowProfileFor(TWITTER_NON_SUBSCRIBER_OF_PERM_LIST)
                .Request().AsUser();

            Assert.IsNotNull(user);

            var query = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .Lists().IsUserFollowerOf(TWITTER_USERNAME, TWITTER_PERMANENT_LIST_ID, user.Id)
                .AsJson();

            var url = query.AsUrl();
            Console.WriteLine(url);

            var response = query.Request();
            IgnoreFailWhales(response);

            var subscriber = response.AsUser();
            Assert.That(subscriber == null || string.IsNullOrEmpty(subscriber.ScreenName));
        }

        [Test]
        [Category("Lists")]
        public void Can_test_if_user_is_subscriber_of_a_list_when_user_subscribes()
        {
            Assert.IsFalse(string.IsNullOrEmpty(TWITTER_SUBSCRIBER_OF_PERM_LIST),
                           "Specify a value for 'listSubscriber' in setup.xml to run this test");

            var user = FluentTwitter.CreateRequest()
                .Users().ShowProfileFor(TWITTER_SUBSCRIBER_OF_PERM_LIST)
                .Request().AsUser();

            Assert.IsNotNull(user, "Couldn't fetch the profile for user '{0}'", TWITTER_SUBSCRIBER_OF_PERM_LIST);

            var query = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .Lists().IsUserFollowerOf(TWITTER_USERNAME, TWITTER_PERMANENT_LIST_ID, user.Id)
                .AsJson();

            var url = query.AsUrl();
            Console.WriteLine(url);

            var response = query.Request();
            IgnoreFailWhales(response);

            var subscriber = response.AsUser();
            Assert.IsNotNull(subscriber, "User '{0}' does not appear to be a subscriber of list '{1}",
                             TWITTER_SUBSCRIBER_OF_PERM_LIST, TWITTER_PERMANENT_LIST_ID);
        }

        [Test]
        [Category("Lists")]
        public void Can_update_list()
        {
            var list = CreateTemporaryList();
            Assert.IsNotNull(list);
            TwitterList updated = null;
            try
            {
                list.Mode = list.Mode.Equals("private") ? "public" : "private";

                var query = FluentTwitter.CreateRequest()
                    .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                    .Lists().UpdateList(list).AsXml();

                var response = query.Request();
                IgnoreFailWhales(response);

                updated = response.AsList();
                Assert.IsNotNull(updated);
                Assert.AreEqual(list.Mode, updated.Mode);
            }
            finally
            {
                DeleteList(list);
            }
        }

        [Test]
        [Category("Lists")]
        public void Can_use_classic_paging_with_list_timeline_lists_style()
        {
            Assert.IsTrue(TWITTER_PERMANENT_LIST_ID != 0, "Specify a value for 'listid' in setup.xml to run this test.");

            var query = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .Lists().GetStatuses(TWITTER_USERNAME, TWITTER_PERMANENT_LIST_ID)
                .Skip(2).Take(50);

            var url = query.AsUrl();
            Console.WriteLine(url);

            var response = query.Request();
            IgnoreFailWhales(response);

            var statuses = response.AsStatuses();
            Assert.IsNotNull(statuses);
        }

        [Test]
        [Category("Lists")]
        public void Can_use_classic_paging_with_list_timeline_statuses_style()
        {
            Assert.IsTrue(TWITTER_PERMANENT_LIST_ID != 0, "Specify a value for 'listid' in setup.xml to run this test.");
            var query = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .Statuses().OnListTimeline(TWITTER_USERNAME, TWITTER_PERMANENT_LIST_ID)
                .Skip(2).Take(50);

            var url = query.AsUrl();
            Console.WriteLine(url);

            var response = query.Request();
            IgnoreFailWhales(response);

            var statuses = response.AsStatuses();
            Assert.IsNotNull(statuses);
        }
    }
}