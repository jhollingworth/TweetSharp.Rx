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

namespace TweetSharp.Fluent
{
#if(!SILVERLIGHT)
    /// <summary>
    /// Stores OAuth authentication details for a query.
    /// </summary>
    [Serializable]
#endif
    public class FluentBaseOAuth : IFluentBaseOAuth
    {
        #region IFluentBaseOAuth Members

        /// <summary>
        /// Gets or sets the action name for the OAuth transaction.
        /// </summary>
        public string Action { get; set; }

        /// <summary>
        /// Gets or sets the ConsumerKey for the OAuth transaction.
        /// </summary>
        public string ConsumerKey { get; set; }

        /// <summary>
        /// Gets or sets the ConsumerSecret for the OAuth transaction.
        /// </summary> 
        public string ConsumerSecret { get; set; }

        /// <summary>
        /// Gets or sets the Token (Access token for protected resources, Request token during OAuth negotiation).
        /// </summary>
        public string Token { get; set; }

        /// Gets or sets the Token (Access token for protected resources, Request token during OAuth negotiation).
        public string TokenSecret { get; set; }

        /// <summary>
        /// Gets or sets the callback URL that users will be redirected to once the oauth operations on the provider's web.
        /// site have been completed.  
        /// </summary>
        public string Callback { get; set; }

        /// <summary>
        /// Gets or sets the verification code provided to the user on the provider's website after the application.
        /// is authorized
        /// </summary>
        public string Verifier { get; set; }

        /// <summary>
        /// Gets or Sets the client user name - used for xAuth (browerless OAuth).
        /// </summary>
        public string ClientUsername { get; set; }

        /// <summary>
        /// Gets or sets the client password - used for xAuth (browserless OAuth).
        /// </summary>
        public string ClientPassword { get; set; }

        #endregion
    }
}