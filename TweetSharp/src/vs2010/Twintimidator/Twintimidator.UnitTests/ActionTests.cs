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
using TweetSharp.Fluent;
using NUnit.Framework;
using TweetSharp.Twitter.Extensions;
using TweetSharp.Twitter.Fluent;
using TweetSharp.Twitter.Model;
using Twintimidator.Accounts;
using Twintimidator.Actions;

namespace Twintimidator.UnitTests
{
    [TestFixture]
    public class ActionTests
    {
        private readonly string userName = "";
        private readonly string password = "";

        private readonly string userName2 = "";
        private readonly string password2 = "";

        [Test]
        public void Can_assign_user()
        {
            var action = new TwitterAction<TwitterUser>();
            Assert.IsFalse(string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password),
                           "Specify a username and password to run this test");
            var user = new UserAccount {AccountType = AccountType.BasicAuth, UserName = userName, Password = password};
            action.User = user;
            Assert.AreEqual(user, action.User);
        }

        [Test]
        public void Can_be_reused_by_different_users()
        {
            var action = Can_create_complete_action();
            Assert.IsFalse(string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password),
                           "Specify a username and password to run this test");
            var user = new UserAccount {AccountType = AccountType.BasicAuth, UserName = userName, Password = password};

            var result = action.Execute();
            Assert.IsTrue(result.Success, "VerifyCredentials for {0} failed", action.User.UserName);
            var user1 = result.ReturnValue;
            Assert.IsNotNull(user1);


            Assert.IsFalse(string.IsNullOrEmpty(userName2) || string.IsNullOrEmpty(password2),
                           "Specify a second username and password to run this test");
            action.User = new UserAccount
                              {AccountType = AccountType.BasicAuth, UserName = userName2, Password = password2};

            var result2 = action.Execute();

            Assert.IsTrue(result2.Success);
            Assert.IsTrue(result.Success, "VerifyCredentials for {0} failed", action.User.UserName);
            var user2 = result2.ReturnValue;
            Assert.IsNotNull(user2);

            Assert.AreNotEqual(user1.ScreenName, user2.ScreenName);
        }

        [Test]
        public TwitterAction<IEnumerable<TwitterStatus>> Can_create_action_without_assigning_user()
        {
            var action = new TwitterAction<IEnumerable<TwitterStatus>>();
            action.QueryMethod = () => action.GetBaseQuery().Statuses().OnFriendsTimeline().AsJson();
            action.ConvertReturnValueMethod = s => s.AsStatuses();
            action.EvaluateConvertedReturnValueMethod = result => result.ReturnValue != null;
            Assert.IsNotNull(action);
            return action;
        }

        [Test]
        public TwitterAction<TwitterUser> Can_create_complete_action()
        {
            var action = Can_create_query_and_result_func();
            action.EvaluateConvertedReturnValueMethod = result => result.ReturnValue != null;
            Assert.IsNotNull(action.EvaluateConvertedReturnValueMethod);
            return action;
        }

        [Test]
        public TwitterAction<TwitterUser> Can_create_query()
        {
            var action = new TwitterAction<TwitterUser>();
            Assert.IsFalse(string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password),
                           "Specify a username and password to run this test");
            var user = new UserAccount {AccountType = AccountType.BasicAuth, UserName = userName, Password = password};

            action.User = user;
            action.QueryMethod = () => action.GetBaseQuery().Account().VerifyCredentials().AsJson();
            Assert.IsNotNull(action.QueryMethod);
            return action;
        }

        [Test]
        public TwitterAction<TwitterUser> Can_create_query_and_result_func()
        {
            var action = Can_create_query();
            Func<TwitterResult, TwitterUser> parseFunc = r => r.AsUser();
            action.ConvertReturnValueMethod = parseFunc;
            Assert.IsNotNull(action.ConvertReturnValueMethod);
            return action;
        }

        [Test]
        public void Can_create_twitter_action()
        {
            var action = new TwitterAction<TwitterUser>();
            Assert.IsNotNull(action);
        }

        [Test]
        public void Can_execute_query()
        {
            var action = Can_create_query_and_result_func();
            action.EvaluateConvertedReturnValueMethod = (twitterResult) => twitterResult.ReturnValue != null;
            var result = action.Execute();
            Assert.IsTrue(result.Success);
            Assert.IsNotNull(result.ReturnValue);
            Assert.IsTrue(string.IsNullOrEmpty(result.ErrorMessage));
        }

        [Test]
        public void Can_execute_query_asynchronously()
        {
            var action = Can_create_query_and_result_func();
            action.EvaluateConvertedReturnValueMethod = (twitterResult) => twitterResult.ReturnValue != null;
            var block = new AutoResetEvent(false);
            try
            {
                action.ExecuteAsync(result =>
                                        {
                                            try
                                            {
                                                Assert.IsTrue(result.Success);
                                                //Assert.IsNotNull(result.ReturnValue);
                                                Assert.IsTrue(string.IsNullOrEmpty(result.ErrorMessage));
                                            }
                                            finally
                                            {
                                                block.Set();
                                            }
                                        });

                block.WaitOne();
            }
            catch
            {
                block.Set();
                throw;
            }
        }

        [Test]
        public void Can_get_base_query()
        {
            var action = new TwitterAction<TwitterUser>();
            Assert.IsFalse(string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password),
                           "Specify a username and password to run this test");
            var user = new UserAccount {AccountType = AccountType.BasicAuth, UserName = userName, Password = password};

            action.User = user;
            var baseQuery = action.GetBaseQuery();
            Assert.AreEqual(baseQuery.Authentication.Mode, AuthenticationMode.Basic);
        }

        [Test]
        [ExpectedException(typeof (UserNotSpecifiedException))]
        public void Throws_custom_exception_when_no_user_specified()
        {
            var action = Can_create_action_without_assigning_user();
            action.Execute();
        }

        [Test]
        [ExpectedException(typeof (InvalidOperationException))]
        public void Throws_if_no_converion_method_set()
        {
            var action = new TwitterAction<IEnumerable<TwitterStatus>>();
            action.QueryMethod = () => action.GetBaseQuery().Statuses().OnFriendsTimeline().AsJson();
            action.Execute();
        }

        [Test]
        [ExpectedException(typeof (InvalidOperationException))]
        public void Throws_if_no_evaluation_method_set()
        {
            var action = new TwitterAction<IEnumerable<TwitterStatus>>();
            action.QueryMethod = () => action.GetBaseQuery().Statuses().OnFriendsTimeline().AsJson();
            action.ConvertReturnValueMethod = (result) => result.AsStatuses();
            action.Execute();
        }

        [Test]
        [ExpectedException(typeof (InvalidOperationException))]
        public void Throws_if_no_query_set()
        {
            var action = new TwitterAction<IEnumerable<TwitterStatus>>();
            action.Execute();
        }
    }
}