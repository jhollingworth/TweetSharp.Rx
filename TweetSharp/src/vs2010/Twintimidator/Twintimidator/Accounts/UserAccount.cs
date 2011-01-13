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

namespace Twintimidator.Accounts
{
    public enum AccountType
    {
        BasicAuth,
        OAuth
    }

    [Serializable]
    public class UserAccount : IUserAccount
    {
        private string m_OAuthToken;
        private string m_OAuthTokenSecret;
        private string m_Password;

        #region IUserAccount Members

        public AccountType AccountType { get; set; }
        public string UserName { get; set; }

        public string Password
        {
            get
            {
                if (AccountType == AccountType.OAuth)
                {
                    throw new InvalidOperationException("Account type is OAuth");
                }
                return m_Password;
            }
            set
            {
                if (AccountType == AccountType.OAuth)
                {
                    throw new InvalidOperationException("Account type is OAuth");
                }
                m_Password = value;
            }
        }

        public string OAuthToken
        {
            get
            {
                if (AccountType == AccountType.BasicAuth)
                {
                    throw new InvalidOperationException("Account type is Basic");
                }
                return m_OAuthToken;
            }
            set
            {
                if (AccountType == AccountType.BasicAuth)
                {
                    throw new InvalidOperationException("Account type is basic");
                }
                m_OAuthToken = value;
            }
        }

        public string OAuthTokenSecret
        {
            get
            {
                if (AccountType == AccountType.BasicAuth)
                {
                    throw new InvalidOperationException("Account type is Basic");
                }
                return m_OAuthTokenSecret;
            }
            set
            {
                if (AccountType == AccountType.BasicAuth)
                {
                    throw new InvalidOperationException("Account type is basic");
                }
                m_OAuthTokenSecret = value;
            }
        }

        public override string ToString()
        {
            return string.Format("{0} [{1}]", UserName, AccountType);
        }

        #endregion

        // override object.Equals
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            var user2 = obj as UserAccount;
            if (user2 == null)
            {
                return false;
            }

            return AccountType == user2.AccountType && UserName == user2.UserName;
        }

        public static bool operator ==(UserAccount account1, UserAccount account2)
        {
            if (ReferenceEquals(account1, null) && ReferenceEquals(account2, null))
            {
                return true;
            }
            if (ReferenceEquals(account1, null) || ReferenceEquals(account2, null))
            {
                return false;
            }
            return account1.Equals(account2);
        }

        public static bool operator !=(UserAccount account1, UserAccount account2)
        {
            return !(account1 == account2);
        }

        public override int GetHashCode()
        {
            return UserName.GetHashCode() + (int) AccountType;
        }
    }
}