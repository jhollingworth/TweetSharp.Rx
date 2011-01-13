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
using System.Collections;
using System.Collections.Generic;

namespace Twintimidator.Accounts
{
    public class UserAccountController : IUserAccountController
    {
        private readonly List<IUserAccount> m_Accounts = new List<IUserAccount>();

        #region IUserAccountController Members

        public void AddRange(IEnumerable<IUserAccount> accounts)
        {
            if (accounts != null)
            {
                m_Accounts.AddRange(accounts);
            }
        }

        public event Action<IUserAccount> UserAdded;
        public event Action<IUserAccount> UserRemoved;

        public bool CreateBasicAuthUser(string name, string password)
        {
            var acc = new UserAccount {AccountType = AccountType.BasicAuth, UserName = name, Password = password};
            if (m_Accounts.Contains(acc))
            {
                return false;
            }
            m_Accounts.Add(acc);
            OnUserAdded(acc);
            return true;
        }

        public bool CreateOAuthUser(string name, string token, string tokenSecret)
        {
            var acc = new UserAccount
                          {
                              AccountType = AccountType.OAuth,
                              UserName = name,
                              OAuthToken = token,
                              OAuthTokenSecret = tokenSecret
                          };
            if (m_Accounts.Contains(acc))
            {
                return false;
            }
            m_Accounts.Add(acc);
            OnUserAdded(acc);
            return true;
        }

        public bool RemoveUser(IUserAccount account)
        {
            if (m_Accounts.Contains(account))
            {
                m_Accounts.Remove(account);
                OnUserRemoved(account);
                return true;
            }
            return false;
        }

        public IEnumerator<IUserAccount> GetEnumerator()
        {
            return m_Accounts.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return m_Accounts.GetEnumerator();
        }

        #endregion

        protected void OnUserAdded(IUserAccount account)
        {
            if (UserAdded != null)
            {
                UserAdded(account);
            }
        }

        protected void OnUserRemoved(IUserAccount account)
        {
            if (UserRemoved != null)
            {
                UserRemoved(account);
            }
        }
    }
}