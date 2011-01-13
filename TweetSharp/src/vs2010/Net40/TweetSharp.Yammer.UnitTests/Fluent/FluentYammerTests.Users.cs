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
using TweetSharp.Yammer.Extensions;
using TweetSharp.Yammer.Fluent;
using TweetSharp.Yammer.Model;

namespace TweetSharp.Yammer.UnitTests.Fluent
{
    partial class FluentYammerTests
    {
        private YammerUser GetFakeUserData()
        {
            var emails = new List<YammerEmailAddress>
                             {
                                 new YammerEmailAddress
                                     {Address = "fakeUser@" + YAMMER_DOMAIN, EmailType = "Primary"}
                             };
            var school1 = new YammerSchool
                              {
                                  Degree = "B.Sc.",
                                  Description = "It was fun",
                                  EndYear = 2001,
                                  StartYear = 1997,
                                  School = "Harvard"
                              };
            var school2 = new YammerSchool
                              {
                                  Degree = "Ph.D",
                                  Description = "It was less fun",
                                  EndYear = 2003,
                                  StartYear = 2001,
                                  School = "Yale"
                              };
            var schools = new List<YammerSchool> {school1, school2};
            var company = new YammerPreviousCompany
                              {
                                  Description = "Robble Robble",
                                  Employer = "McDonalds",
                                  Position = "Hamburglar"
                              };
            var companies = new List<YammerPreviousCompany> {company};
            var imInfo = new YammerImInfo {Provider = "jabber", UserName = "jabby@jabber.jab"};
            var phones = new List<YammerPhoneNumber>
                             {
                                 new YammerPhoneNumber {Number = "902.555.1434", NumberType = "Work"}
                             };

            var contact = new YammerContactInfo
                              {
                                  EmailAddresses = emails,
                                  Im = imInfo,
                                  PhoneNumbers = phones
                              };

            return new YammerUser
                       {
                           Expertise = "Networks",
                           FullName = "Roger Wilco",
                           JobTitle = "Astronaut",
                           Location = "upstairs",
                           ContactInfo = contact,
                           Interests = "trains",
                           Summary = "Cool cat",
                           Schools = schools,
                           PreviousCompanies = companies
                       };
        }

        private YammerUser GetCurrentUser()
        {
            var token = LoadToken("access");
            var yammer = FluentYammer.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, token.Token, token.TokenSecret)
                .Users()
                .Current().AsJson();

            var response = yammer.Request();
            Assert.IsNotNull(response);
            Assert.IsNull(response.Exception);
            Assert.IsFalse(response.IsYammerError);
            return response.AsUser();
        }

        [Test]
        [Category("YammerUsers")]
        [Ignore("Administrator access required")]
        public void Can_create_new_user_from_email_address()
        {
            //todo: put the email address to create in the setup file
            var token = LoadToken("access");
            var yammer = FluentYammer.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, token.Token, token.TokenSecret)
                .Users()
                .Create("theNewGuy@" + YAMMER_DOMAIN).AsJson();

            var response = yammer.Request();
            Assert.IsNotNull(response);
            Assert.IsNull(response.Exception);
            Assert.IsFalse(response.IsYammerError);
            var user = response.AsUser();
            Assert.IsNotNull(user);
            Assert.IsTrue(user.ContactInfo.EmailAddresses.Where(a => a.Address == YAMMER_USERNAME).Any());
        }

        [Test]
        [Category("YammerUsers")]
        [Ignore("Administrator access required")]
        public void Can_create_new_user_from_user_object()
        {
            //todo: put the user details in the setup file?
            var newUser = GetFakeUserData();

            var token = LoadToken("access");
            var yammer = FluentYammer.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, token.Token, token.TokenSecret)
                .Users()
                .Create(newUser);

            var response = yammer.Request();
            Assert.IsNotNull(response);
            Assert.IsNull(response.Exception);
            Assert.IsFalse(response.IsYammerError);
            var user = response.AsUser();
            Assert.IsNotNull(user);
        }

        [Test]
        [Category("YammerUsers")]
        public void Can_get_all_users_as_json()
        {
            var token = LoadToken("access");
            var yammer = FluentYammer.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, token.Token, token.TokenSecret)
                .Users()
                .All()
                .AsJson();

            var response = yammer.Request();
            Assert.IsNotNull(response);
            Assert.IsFalse(response.IsYammerError);
            Assert.IsNull(response.Exception);
            var users = response.AsUsers();
            Assert.IsNotNull(users);
            Assert.IsTrue(users.Any());
        }

        [Test]
        [Category("YammerUsers")]
        public void Can_get_all_users_as_xml()
        {
            var token = LoadToken("access");
            var yammer = FluentYammer.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, token.Token, token.TokenSecret)
                .Users()
                .All()
                .AsXml();

            var response = yammer.Request();
            Assert.IsNotNull(response);
            Assert.IsFalse(response.IsYammerError);
            Assert.IsNull(response.Exception);
            var users = response.AsUsers();
            Assert.IsNotNull(users);
            Assert.IsTrue(users.Any());
        }

        [Test]
        [Category("YammerUsers")]
        public void Can_get_current_user()
        {
            var user = GetCurrentUser();
            Assert.IsNotNull(user);
        }

        [Test]
        [Category("YammerUsers")]
        public void Can_get_specific_user()
        {
            var token = LoadToken("access");
            var yammer = FluentYammer.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, token.Token, token.TokenSecret)
                .Users()
                .Get(YAMMER_USER_ID).AsXml();

            var response = yammer.Request();
            Assert.IsNotNull(response);
            Assert.IsNull(response.Exception);
            Assert.IsFalse(response.IsYammerError);
            var user = response.AsUser();
            Assert.IsNotNull(user);
        }


        [Test]
        [Category("YammerUsers")]
        public void Can_get_specific_user_by_email_address()
        {
            var token = LoadToken("access");
            var yammer = FluentYammer.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, token.Token, token.TokenSecret)
                .Users()
                .GetByEmail(YAMMER_USERNAME);

            var response = yammer.Request();
            Assert.IsNotNull(response);
            Assert.IsNull(response.Exception);
            Assert.IsFalse(response.IsYammerError);
            var user = response.AsUser();
            Assert.IsNotNull(user);
            Assert.IsTrue(user.ContactInfo.EmailAddresses.Where(a => a.Address == YAMMER_USERNAME).Any());
        }

        [Test]
        [Category("YammerUsers")]
        public void Can_get_users_sorted()
        {
            var token = LoadToken("access");
            var yammer = FluentYammer.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, token.Token, token.TokenSecret)
                .Users().SortedBy(SortUsersBy.Messages).AsJson();

            var response = yammer.Request();
            Assert.IsNotNull(response);
            Assert.IsNull(response.Exception);
            Assert.IsFalse(response.IsYammerError);
            var users = response.AsUsers();
            Assert.IsNotNull(users);
            var last = int.MaxValue;
            foreach (var u in users)
            {
                Assert.LessOrEqual(u.UserStats.Updates, last);
                last = u.UserStats.Updates ?? 0;
            }
        }

        [Test]
        [Category("YammerUsers")]
        public void Can_get_users_sorted_reversed()
        {
            var token = LoadToken("access");
            var yammer = FluentYammer.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, token.Token, token.TokenSecret)
                .Users()
                .SortedBy(SortUsersBy.Messages)
                .Reverse()
                .AsJson();

            var response = yammer.Request();
            Assert.IsNotNull(response);
            Assert.IsNull(response.Exception);
            Assert.IsFalse(response.IsYammerError);
            var users = response.AsUsers();
            Assert.IsNotNull(users);
            var last = int.MinValue;
            foreach (var u in users)
            {
                Assert.GreaterOrEqual(u.UserStats.Updates, last);
                last = u.UserStats.Updates ?? 0;
            }
        }

        [Test]
        [Category("YammerUsers")]
        public void Can_get_users_starting_with_a_letter()
        {
            var token = LoadToken("access");
            var yammer = FluentYammer.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, token.Token, token.TokenSecret)
                .Users()
                .StartingWith('j')
                .AsJson();

            var response = yammer.Request();
            Assert.IsNotNull(response);
            Assert.IsFalse(response.IsYammerError);
            Assert.IsNull(response.Exception);
            var users = response.AsUsers();
            Assert.IsNotNull(users);
            if (users.Any())
            {
                users.ToList().ForEach(user => Assert.IsTrue(user.Name.ToLower()[0] == 'j'));
            }
        }

        [Test]
        [Category("YammerUsers")]
        public void Can_page_users()
        {
            var pp = 1;
            const int usersPerPage = 50;
            var lastUsers = usersPerPage;
            while (lastUsers >= usersPerPage)
            {
                var token = LoadToken("access");
                var yammer = FluentYammer.CreateRequest()
                    .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, token.Token, token.TokenSecret)
                    .Users().Page(pp);

                var response = yammer.Request();
                Assert.IsNotNull(response);
                Assert.IsNull(response.Exception);
                Assert.IsFalse(response.IsYammerError);
                var users = response.AsUsers();
                lastUsers = users.Count();
                pp++;
            }
        }

        [Test]
        [Category("YammerUsers")]
        [Ignore("Requires admin access")]
        public void Can_update_current_user()
        {
            var userData = new YammerUser
                               {
                                   Summary =
                                       string.Format("Updated by unit tests at {0} on {1}",
                                                     DateTime.Now.ToShortTimeString(),
                                                     DateTime.Now.ToShortDateString())
                               };

            var token = LoadToken("access");
            var yammer = FluentYammer.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, token.Token, token.TokenSecret)
                .Users()
                .UpdateCurrent(userData);

            var response = yammer.Request();
            Assert.IsNotNull(response);
            Assert.IsNull(response.Exception);
            Assert.IsFalse(response.IsYammerError);
            var user = response.AsUser();
            Assert.IsNotNull(user);
            Assert.AreEqual(userData.Summary, user.Summary);
        }

        [Test]
        [Category("YammerUsers")]
        [Ignore("Requires admin access")]
        public void Can_update_other_user()
        {
            var userData = new YammerUser
                               {
                                   Summary =
                                       string.Format("Updated by unit tests at {0} on {1}",
                                                     DateTime.Now.ToShortTimeString(),
                                                     DateTime.Now.ToShortDateString())
                               };

            var token = LoadToken("access");
            var yammer = FluentYammer.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, token.Token, token.TokenSecret)
                .Users()
                .UpdateCurrent(userData);

            var response = yammer.Request();
            Assert.IsNotNull(response);
            Assert.IsNull(response.Exception);
            Assert.IsFalse(response.IsYammerError);
            var user = response.AsUser();
            Assert.IsNotNull(user);
            Assert.AreEqual(userData.Summary, user.Summary);
        }
    }
}