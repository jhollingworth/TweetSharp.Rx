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
using Hammock.Attributes.Specialized;
using TweetSharp.Fluent;

namespace TweetSharp
{
     /// <summary>
     /// Client info class for the test runner.
     /// </summary>
    public class TestTwitterClientInfo : TwitterClientInfo
    {
        /// <summary>
        /// Gets the meta header for acceptable Twitter content types returned by the API.
        /// This is used mainly to guarantee that tests work with URL-encoded values,
        /// though this should be vestigial by now as Twitter has promised to return
        /// this value for all real Accepts responses.
        /// </summary>
        /// <value>The twitter accepts.</value>
        [Header("X-Twitter-Content-Type-Accepts")]
        public string TwitterAccepts { get { return "application/x-www-form-urlencoded"; } }
    }

#if(!SILVERLIGHT)
    ///<summary>
    /// This class provides meta-data for your specific Twitter application, that is
    /// used to identify your client to Twitter, store OAuth credentials for all future
    /// request, and in some cases define a transparent proxy to redirect API calls to.
    ///</summary>
    [Serializable]
#endif

    public class TwitterClientInfo : IClientInfo
    {
#if SILVERLIGHT
        // Proxy Request Headers
        private const string ProxyAcceptEncodingHeader = "X-Twitter-Accept";
        private const string ProxyAgentHeader = "X-Twitter-Agent";
        private const string ProxyAuthorizationHeader = "X-Twitter-Auth";
        private const string ProxyMethodHeader = "X-Twitter-Method";

        public TwitterClientInfo()
        {
            SilverlightAcceptEncodingHeader = ProxyAcceptEncodingHeader;
            SilverlightAgentHeader = ProxyAgentHeader;
            SilverlightMethodHeader = ProxyMethodHeader;
            SilverlightAuthorizationHeader = ProxyAuthorizationHeader;
        }
#endif
        /// <summary>
        /// This is the name of your client application. It is used to
        /// identify your client when a user updates their status, or when
        /// your application makes a Twitter Search API request.
        /// </summary>
        [UserAgent, Header("X-Twitter-Client")]
        public string ClientName { get; set; }

        /// <summary>
        /// This is the version of your application. This is meta-data only,
        /// and not used by Twitter for client processing.
        /// </summary>
        [Header("X-Twitter-Version")]
        public string ClientVersion { get; set; }

        /// <summary>
        /// This is the URL of your application. This is meta-data only,
        /// and not used by Twitter for client processing. Your application's URL
        /// is stored by Twitter when you apply for a 'Source' attribute or register
        /// your application for OAuth.
        /// </summary>
        [Header("X-Twitter-URL")]
        public string ClientUrl { get; set; }

        /// <summary>
        /// If your client is using OAuth authentication, this value should be set
        /// to the value of your consumer key. This avoids having to provide the key
        /// in every query.
        /// </summary>
        public string ConsumerKey { get; set; }

        /// <summary>
        /// If your client is using OAuth authentication, this value should be set
        /// to the value of your consumer secret. This avoids having to provide the secret
        /// in every query.
        /// </summary>
        public string ConsumerSecret { get; set; }

        /// <summary>
        /// Since you are communicating from the client-side, this value should point to a 
        /// proxy that is configured to work transparently (API methods are identical other
        /// than the domain), allow cross-domain access, and understand TweetSharp custom 
        /// headers.
        /// </summary>
        public string TransparentProxy { get; set; }

#if SILVERLIGHT
        /// <summary>
        /// Use this flag to tell TweetSharp that it is okay to set the Authorization header
        /// directly in Silverlight applications. This will allow cross-browser HTTP requests
        /// in Silverlight 4 when running out of browser.
        /// </summary>
        public bool HasElevatedPermissions { get; set; }

        public string SilverlightAcceptEncodingHeader { get; set; }
        public string SilverlightAgentHeader { get; set; }
        public string SilverlightAuthorizationHeader { get; set; }
        public string SilverlightMethodHeader { get; set; }
        public string SilverlightQueryHeader { get; set; }
#endif
    }

    internal class TwitterClientInfoInternal : IClientWebQueryInfo
    {
        [UserAgent, Header("X-Twitter-Client")]
        public string ClientName { get; set; }

        [Header("X-Twitter-Version")]
        public string ClientVersion { get; set; }
        
        [Header("X-Twitter-URL")]
        public string ClientUrl { get; set; }

        public string ConsumerKey { get; set; }
        public string ConsumerSecret { get; set; }

        public string TransparentProxy { get; set; }

#if SILVERLIGHT
        public bool HasElevatedPermissions { get; set; }
#endif
        public TwitterClientInfoInternal(IClientInfo info)
        {
            ClientName = info.ClientName;
            ClientVersion = info.ClientVersion;
            ClientUrl = info.ClientUrl;
            ConsumerKey = info.ConsumerKey;
            ConsumerSecret = info.ConsumerSecret;
#if SILVERLIGHT
            HasElevatedPermissions = info.HasElevatedPermissions;
#endif
        }
    }
}