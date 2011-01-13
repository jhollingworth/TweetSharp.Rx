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

using System.Web;

namespace TweetSharpMvc.Security
{
    /// <summary>
    /// This is a very basic security implementation for demonstration purposes only.
    /// By holding the twitter username and password in Session, we can pass these through 
    /// to Twitter with requests that require them.
    /// No authentication is performed until a request is made against Twitter.
    /// </summary>
    public static class UserManager
    {
        public static bool HasCredentials
        {
            get { return !string.IsNullOrEmpty(Username); }
        }

        public static string Username
        {
            get
            {
                var username = HttpContext.Current.Session["username"] as string;
                return string.IsNullOrEmpty(username) ? string.Empty : username;
            }
        }

        public static string Password
        {
            get
            {
                var password = HttpContext.Current.Session["password"] as string;
                return string.IsNullOrEmpty(password) ? string.Empty : password;
            }
        }

        public static void Login(string username, string password)
        {
            HttpContext.Current.Session["username"] = username;
            HttpContext.Current.Session["password"] = password;
        }

        public static void SignOut()
        {
            HttpContext.Current.Session["username"] = null;
            HttpContext.Current.Session["password"] = null;
        }
    }
}