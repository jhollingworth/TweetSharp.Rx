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
using NUnit.Framework;
using Twintimidator.Accounts;

namespace Twintimidator.UnitTests
{
    [TestFixture]
    public class UserAccountTests
    {
        [Test]
        public void Are_equivalent_if_user_and_type_match_using_equality_operator()
        {
            const string USER = "TerminatorX";
            var account1 = new UserAccount
                               {
                                   UserName = USER,
                                   AccountType = AccountType.BasicAuth
                               };
            var account2 = new UserAccount
                               {
                                   UserName = USER,
                                   AccountType = AccountType.BasicAuth
                               };
            account1.Password = "mulder";
            account2.Password = "scully";

            Assert.IsTrue(account1 == account2);
        }

        [Test]
        public void Are_equivalent_if_user_and_type_match_using_equals_method()
        {
            const string USER = "TerminatorX";
            var account1 = new UserAccount
                               {
                                   UserName = USER,
                                   AccountType = AccountType.BasicAuth
                               };
            var account2 = new UserAccount
                               {
                                   UserName = USER,
                                   AccountType = AccountType.BasicAuth
                               };
            account1.Password = "mulder";
            account2.Password = "scully";

            Assert.IsTrue(account1.Equals(account2));
        }

        [Test]
        public void Can_assign_password_to_basic_auth_account()
        {
            const string PASS = "ThisOneIsFine";
            var account = new UserAccount
                              {
                                  AccountType = AccountType.BasicAuth,
                                  Password = PASS
                              };
            Assert.AreEqual(PASS, account.Password);
        }

        [Test]
        public void Can_assign_tokens_to_basic_auth_account()
        {
            const string TOKEN = "ToKeN";
            const string SECRET = "ToKeNsEcRet";
            var account = new UserAccount
                              {
                                  AccountType = AccountType.OAuth,
                                  OAuthToken = TOKEN,
                                  OAuthTokenSecret = SECRET
                              };
            Assert.AreEqual(TOKEN, account.OAuthToken);
            Assert.AreEqual(SECRET, account.OAuthTokenSecret);
        }

        [Test]
        public void Can_instantiate_user_account()
        {
            var account = new UserAccount();
            Assert.IsNotNull(account);
        }

        [Test]
        public void Can_set_and_get_account_type()
        {
            var account = new UserAccount {AccountType = AccountType.BasicAuth};
            Assert.AreEqual(AccountType.BasicAuth, account.AccountType);
        }

        [Test]
        public void Can_set_and_get_password()
        {
            const string PASS = "boogity";
            var account = new UserAccount {Password = PASS};
            Assert.AreEqual(PASS, account.Password);
        }

        [Test]
        public void Can_set_and_get_username()
        {
            const string USER = "testUser";
            var account = new UserAccount {UserName = USER};
            Assert.AreEqual(USER, account.UserName);
        }

        [Test]
        [ExpectedException(typeof (InvalidOperationException))]
        public void Cant_assign_password_to_o_auth_account()
        {
            var account = new UserAccount
                              {
                                  AccountType = AccountType.OAuth,
                                  Password = "Shouldn'tSetPasswordOnOAuthAccount"
                              };

            Assert.IsNotNull(account);
        }

        [Test]
        [ExpectedException(typeof (InvalidOperationException))]
        public void Cant_assign_tokens_to_basic_auth_account()
        {
            var account = new UserAccount
                              {
                                  AccountType = AccountType.BasicAuth,
                                  OAuthToken = "token",
                                  OAuthTokenSecret = "Secret"
                              };
        }

        [Test]
        [ExpectedException(typeof (InvalidOperationException))]
        public void Cant_get_password_from_o_auth_account()
        {
            const string TOKEN = "ToKeN";
            const string SECRET = "ToKeNsEcRet";
            var account = new UserAccount
                              {
                                  AccountType = AccountType.OAuth,
                                  OAuthToken = TOKEN,
                                  OAuthTokenSecret = SECRET
                              };
            var password = account.Password;
        }

        [Test]
        [ExpectedException(typeof (InvalidOperationException))]
        public void Cant_get_tokens_from_basic_account()
        {
            const string PASS = "pa55w0rd";
            var account = new UserAccount
                              {
                                  AccountType = AccountType.BasicAuth,
                                  Password = PASS
                              };
            var token = account.OAuthToken;
        }

        [Test]
        public void Compares_correctly_with_null_when_not_null_using_operator()
        {
            var account1 = new UserAccount
                               {
                                   UserName = "bob",
                                   AccountType = AccountType.BasicAuth,
                                   Password = "doug"
                               };
            Assert.IsTrue(account1 != null);
        }

        [Test]
        public void Compares_correctly_with_null_when_null_using_operator()
        {
            const UserAccount account1 = null;
            Assert.IsTrue(account1 == null);
        }
    }
}