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

namespace TweetSharp
{
    /// <summary>
    /// Interface to an object describing your client
    /// </summary>
    public interface IClientInfo
    {
        /// <summary>
        /// This is the name of your client application.
        /// </summary>
        string ClientName { get; set; }

        /// <summary>
        /// This is the version of your application. This is meta-data only,
        /// and not used for client processing.
        /// </summary>
        string ClientVersion { get; set; }

        /// <summary>
        /// This is the URL of your application. This is meta-data only,
        /// and not used for client processing. 
        /// </summary>
        string ClientUrl { get; set; }

        /// <summary>
        /// If your client is using OAuth authentication, this value should be set
        /// to the value of your consumer key. This avoids having to provide the key
        /// in every query.
        /// </summary>
        string ConsumerKey { get; set; }

        /// <summary>
        /// If your client is using OAuth authentication, this value should be set
        /// to the value of your consumer secret. This avoids having to provide the secret
        /// in every query.
        /// </summary>
        string ConsumerSecret { get; set; }

        /// <summary>
        /// Since you are communicating from the client-side, this value should point to a 
        /// proxy that is configured to work transparently (API methods are identical other
        /// than the domain), allow cross-domain access, and understand TweetSharp custom 
        /// headers.
        /// </summary>
        string TransparentProxy { get; set; }

#if SILVERLIGHT
        /// <summary>
        /// Use this flag to tell TweetSharp that it is okay to set the Authorization header
        /// directly in Silverlight applications. This will allow cross-browser HTTP requests
        /// in Silverlight 4 when running out of browser.
        /// </summary>
        bool HasElevatedPermissions { get; set; }

        string SilverlightAcceptEncodingHeader { get; set; }
        string SilverlightAgentHeader { get; set; }
        string SilverlightAuthorizationHeader { get; set; }
        string SilverlightMethodHeader { get; set; }
        string SilverlightQueryHeader { get; set; }
#endif
    }
}