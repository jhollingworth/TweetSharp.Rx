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

using System.Linq;
using NUnit.Framework;
using Twintimidator.Accounts;

namespace Twintimidator.UnitTests
{
    [TestFixture]
    public class UserAccountControllerTests
    {
        private IUserAccountController m_Controller;
        private bool m_AddedEventFired;
        private bool m_RemovedEventFired;
        private IUserAccount m_LastAddedAccount;
        private IUserAccount m_LastRemovedAccount;

        [TestFixtureSetUp]
        public void Setup()
        {
            m_Controller = new UserAccountController();
            m_Controller.UserAdded += account =>
                                          {
                                              m_AddedEventFired = true;
                                              m_LastAddedAccount = account;
                                          };

            m_Controller.UserRemoved += account =>
                                            {
                                                m_RemovedEventFired = true;
                                                m_LastRemovedAccount = account;
                                            };
        }

        [Test]
        public void Can_assign_null_to_users_collection()
        {
            m_Controller.AddRange(null);
        }

        [Test]
        public void Can_create_basic_account()
        {
            var success = m_Controller.CreateBasicAuthUser("Frank", "chipButty");
            Assert.IsTrue(success);
        }

        [Test]
        public void Can_create_o_auth_account()
        {
            var success = m_Controller.CreateOAuthUser("Fred", "abcdefg", "hijklmn");
            Assert.IsTrue(success);
        }

        [Test]
        public void Dissalows_duplicate_accounts()
        {
            const string USER = "Pizza";
            const string PASS = "XZheese";
            var success = m_Controller.CreateBasicAuthUser(USER, PASS);
            var doubleSuccess = m_Controller.CreateBasicAuthUser(USER, PASS);
            Assert.IsTrue(success);
            Assert.IsFalse(doubleSuccess);
        }

        [Test]
        public void Enumeration_contains_user_after_creation()
        {
            m_AddedEventFired = false;
            m_LastAddedAccount = null;
            const string USER = "nestle";
            const string PASS = "bunny";
            var success = m_Controller.CreateBasicAuthUser(USER, PASS);
            Assert.IsTrue(success);
            Assert.IsTrue(m_AddedEventFired);
            Assert.IsNotNull(m_LastAddedAccount);
            Assert.IsTrue(m_Controller.Contains(m_LastAddedAccount));
        }

        [Test]
        public void Fires_event_on_user_creation()
        {
            m_AddedEventFired = false;
            m_LastAddedAccount = null;
            const string USER = "Fred";
            const string PASS = "Barney";
            var success = m_Controller.CreateBasicAuthUser(USER, PASS);
            Assert.IsTrue(success);
            Assert.IsTrue(m_AddedEventFired);
            Assert.IsNotNull(m_LastAddedAccount);
            Assert.AreEqual(USER, m_LastAddedAccount.UserName);
            Assert.AreEqual(PASS, m_LastAddedAccount.Password);
        }

        [Test]
        public void Fires_event_on_user_deletion()
        {
            m_AddedEventFired = false;
            m_LastAddedAccount = null;
            const string USER = "CookieMonster";
            const string PASS = "bockbock";
            var success = m_Controller.CreateBasicAuthUser(USER, PASS);
            Assert.IsTrue(success);
            Assert.IsTrue(m_AddedEventFired);
            Assert.IsNotNull(m_LastAddedAccount);

            m_RemovedEventFired = false;
            var account = m_LastAddedAccount;
            var removeSuccess = m_Controller.RemoveUser(m_LastAddedAccount);
            Assert.IsTrue(removeSuccess);
            Assert.IsTrue(m_RemovedEventFired);
            Assert.IsNotNull(m_LastRemovedAccount);
            Assert.AreEqual(account, m_LastRemovedAccount);
        }
    }
}