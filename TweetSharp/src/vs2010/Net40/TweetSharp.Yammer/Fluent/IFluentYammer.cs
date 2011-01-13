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

using System.ComponentModel;
using Hammock.Web;
using TweetSharp.Core;
using TweetSharp.Fluent;
using TweetSharp.Yammer.Model;

namespace TweetSharp.Yammer.Fluent
{
    /// <summary>
    /// The base fluent query instance for the Yammer API.
    /// </summary>
    public interface IFluentYammer : IFluentBase<YammerResult>
    {
        /// <summary>
        /// Gets or sets the client info.
        /// This is used to set OAuth consumer information
        /// and other metadata on a per-request basis.
        /// </summary>
        /// <value>The client info.</value>
        YammerClientInfo ClientInfo { get; set; }

        /// <summary>
        /// Gets or sets the HTTP method used for this query.
        /// </summary>
        /// <value>The HTTP method.</value>
        [EditorBrowsable(EditorBrowsableState.Never)]
        WebMethod Method { get; set; }
        /// <summary>
        /// Gets or sets the web content format expected in the query response.
        /// </summary>
        /// <value>The format.</value>
        [EditorBrowsable(EditorBrowsableState.Never)]
        WebFormat Format { get; set; }
        
        /// <summary>
        /// Gets or sets the method callback to invoke after a query response.
        /// </summary>
        /// <value>The callback.</value>
        [EditorBrowsable(EditorBrowsableState.Never)]
        YammerWebCallback Callback { get; set; }

        /// <summary>
        /// Gets or sets the authentication info for the query.
        /// </summary>
        /// <value>The authentication.</value>
        [EditorBrowsable(EditorBrowsableState.Never)]
        IFluentYammerAuthentication Authentication { get; set; }
        /// <summary>
        /// Gets the query parameters branch.
        /// </summary>
        /// <value>The parameters.</value>
        [EditorBrowsable(EditorBrowsableState.Never)]
        IFluentYammerParameters Parameters { get; }

        /// <summary>
        /// Gets the query configuration.
        /// </summary>
        /// <value>The configuration.</value>
        [EditorBrowsable(EditorBrowsableState.Never)]
        IFluentYammerConfiguration Configuration { get; }

        /// <summary>
        /// Converts this query node into an API URL representation.
        /// </summary>
        /// <returns></returns>
        string AsUrl();
    }
}