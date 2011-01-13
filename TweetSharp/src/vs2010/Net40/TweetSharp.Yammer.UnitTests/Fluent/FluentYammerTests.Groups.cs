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
using TweetSharp.Fluent;
using TweetSharp.Model;
using NUnit.Framework;
using TweetSharp.Yammer.Extensions;
using TweetSharp.Yammer.Fluent;
using TweetSharp.Yammer.Model;

namespace TweetSharp.Yammer.UnitTests.Fluent
{
    partial class FluentYammerTests
    {
        [Test]
        [Category("Groups")]
        public void Can_create_private_group()
        {
            var token = LoadToken("access");
            var yammer = FluentYammer.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, token.Token, token.TokenSecret)
                .Groups()
                .Create(
                           string.Format("Private GroupFrom{0} at {1}", DateTime.Now.ToLongDateString(),
                                         DateTime.Now.ToShortTimeString()), YammerGroupPrivacy.Private)
                .AsJson();

            var response = yammer.Request();
            Assert.IsNotNull(response);
            Assert.IsNull(response.Exception);
            Assert.IsFalse(response.IsYammerError);
        }

        [Test]
        [Category("Groups")]
        public void Can_create_public_group()
        {
            var token = LoadToken("access");
            var yammer = FluentYammer.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, token.Token, token.TokenSecret)
                .Groups()
                .Create(string.Format("GroupFrom{0} at {1}", DateTime.Now.ToLongDateString(),
                                      DateTime.Now.ToShortTimeString()))
                .AsJson();

            var response = yammer.Request();
            Assert.IsNotNull(response);
            Assert.IsNull(response.Exception);
            Assert.IsFalse(response.IsYammerError);
        }

        [Test]
        [Category("Groups")]
        public void Can_get_all_groups_as_json()
        {
            var token = LoadToken("access");
            var yammer = FluentYammer.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, token.Token, token.TokenSecret)
                .Groups().AsJson();
            var response = yammer.Request();
            Assert.IsNotNull(response);
            Assert.IsNull(response.Exception);
            Assert.IsFalse(response.IsYammerError);
            var groups = response.AsGroups();
            Assert.IsNotNull(groups);
        }

        [Test]
        [Category("Groups")]
        public void Can_get_all_groups_as_xml()
        {
            var token = LoadToken("access");
            var yammer = FluentYammer.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, token.Token, token.TokenSecret)
                .Groups().All().AsXml();
            var response = yammer.Request();
            Assert.IsNotNull(response);
            Assert.IsNull(response.Exception);
            Assert.IsFalse(response.IsYammerError);
            var groups = response.AsGroups();
            Assert.IsNotNull(groups);
        }

        [Test]
        [Category("Groups")]
        public void Can_get_groups_sorted()
        {
            var token = LoadToken("access");
            var yammer = FluentYammer.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, token.Token, token.TokenSecret)
                .Groups().SortedBy(SortGroupsBy.Messages).AsJson();

            var response = yammer.Request();
            Assert.IsNotNull(response);
            Assert.IsNull(response.Exception);
            Assert.IsFalse(response.IsYammerError);
            var groups = response.AsGroups();
            Assert.IsNotNull(groups);
            var last = int.MaxValue;
            foreach (var g in groups)
            {
                Assert.LessOrEqual(g.Stats.Updates, last);
                last = g.Stats.Updates;
            }
        }

        [Test]
        [Category("Groups")]
        public void Can_get_groups_sorted_in_reverse()
        {
            var token = LoadToken("access");
            var yammer = FluentYammer.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, token.Token, token.TokenSecret)
                .Groups().SortedBy(SortGroupsBy.Messages).Reverse().AsJson();

            var response = yammer.Request();
            Assert.IsNotNull(response);
            Assert.IsNull(response.Exception);
            Assert.IsFalse(response.IsYammerError);
            var groups = response.AsGroups();
            Assert.IsNotNull(groups);

            var last = int.MinValue;
            foreach (var g in groups)
            {
                Assert.GreaterOrEqual(g.Stats.Updates, last);
                last = g.Stats.Updates;
            }
        }

        [Test]
        [Category("Groups")]
        public void Can_get_groups_starting_with_specific_letter()
        {
            var token = LoadToken("access");
            var yammer = FluentYammer.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, token.Token, token.TokenSecret)
                .Groups().StartingWith('L').AsJson();

            var response = yammer.Request();
            Assert.IsNotNull(response);
            Assert.IsNull(response.Exception);
            Assert.IsFalse(response.IsYammerError);
            var groups = response.AsGroups();
            Assert.IsNotNull(groups);
            groups.ToList().ForEach(l => Assert.IsTrue(l.Name.ToLower().StartsWith("l")));
        }

        [Test]
        [Category("Users")]
        public void Can_page_groups()
        {
            var pp = 1;
            const int groupsPerPage = 50;
            var lastgroups = groupsPerPage;
            while (lastgroups >= groupsPerPage)
            {
                var token = LoadToken("access");
                var yammer = FluentYammer.CreateRequest()
                    .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, token.Token, token.TokenSecret)
                    .Groups().Page(pp);

                var response = yammer.Request();
                Assert.IsNotNull(response);
                Assert.IsNull(response.Exception);
                Assert.IsFalse(response.IsYammerError);
                var groups = response.AsGroups();
                lastgroups = groups.Count();
                pp++;
            }
        }

        [Test]
        [Category("Groups")]
        [Ignore("Always returns 401 - Yammer bug?")]
        public void Can_update_a_group_name()
        {
            var groupName = string.Format("Private GroupFrom{0} at {1}", DateTime.Now.ToLongDateString(),
                                          DateTime.Now.ToShortTimeString());
            //create a group
            var token = LoadToken("access");
            var yammer = FluentYammer.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, token.Token, token.TokenSecret)
                .Groups()
                .Create(groupName, YammerGroupPrivacy.Private)
                .AsJson();

            var response = yammer.Request();
            Assert.IsNotNull(response);
            Assert.IsNull(response.Exception);
            Assert.IsFalse(response.IsYammerError);

            //group is not returned when created, so fetch list of groups to find its id
            var y2 = FluentYammer.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, token.Token, token.TokenSecret)
                .Groups().StartingWith(groupName[0])
                .AsJson();
            response = y2.Request();

            var groups = response.AsGroups();
            var ids = (from g in groups where g.FullName == groupName select g.Id);
            var id = ids.FirstOrDefault();

            //update the new group with a new name
            var y3 = FluentYammer.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, token.Token, token.TokenSecret)
                .Groups().Update(id).SetName("Updated " + groupName)
                .AsJson();

            response = y3.Request();
            Assert.IsNotNull(response);
            Assert.IsNull(response.Exception);
            Assert.IsFalse(response.IsYammerError);
        }


        [Test]
        [Category("Groups")]
        [Ignore("Admin access required")]
        public void Can_update_a_group_privacy()
        {
            var groupName = string.Format("Group that starts private and becomes public from{0} at {1}",
                                          DateTime.Now.ToShortDateString(),
                                          DateTime.Now.ToShortTimeString());
            //create a group
            var token = LoadToken("access");
            var yammer = FluentYammer.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, token.Token, token.TokenSecret)
                .Groups()
                .Create(groupName, YammerGroupPrivacy.Private)
                .AsJson();

            var response = yammer.Request();
            Assert.IsNotNull(response);
            Assert.IsNull(response.Exception);
            Assert.IsFalse(response.IsYammerError);

            //group is not returned when created, so fetch list of groups to find its id
            var y2 = FluentYammer.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, token.Token, token.TokenSecret)
                .Groups().StartingWith(groupName[0])
                .AsJson();
            response = y2.Request();

            var groups = response.AsGroups();
            var matches = (from g in groups where g.FullName == groupName select g);
            var group = matches.FirstOrDefault();

            //update the new group with new privacy settings
            var y3 = FluentYammer.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, token.Token, token.TokenSecret)
                .Groups().Update(group).SetPrivacy(YammerGroupPrivacy.Public)
                .AsJson();

            response = y3.Request();
            Assert.IsNotNull(response);
            Assert.IsNull(response.Exception);
            Assert.IsFalse(response.IsYammerError);
        }
    }
}