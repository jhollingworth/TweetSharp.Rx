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
using Twintimidator.Actions;

namespace Twintimidator.UnitTests
{
    [TestFixture]
    public class StandardActionsTests
    {
        private const string userName = "";
        private const string password = "";

        [Test]
        public void Can_get_actions()
        {
            var cnt = StandardActions.Actions.Count();
            Assert.IsTrue(cnt > 0);
        }

        [Test]
        public void Can_invoke_generically()
        {
            Assert.IsFalse(string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password),
                           "Specify a username and password to run this test");
            var user = new UserAccount {AccountType = AccountType.BasicAuth, UserName = userName, Password = password};

            foreach (var action in StandardActions.Actions)
            {
                var result = action.ExecuteAs(user);
            }
        }
    }
}