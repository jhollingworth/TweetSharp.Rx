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
using System.Collections.Generic;
using System.ComponentModel;
using Hammock.Web;
using Hammock.Tasks;
using TweetSharp.Core;
using TweetSharp.Fluent;
using TweetSharp.Model;
using TweetSharp.Twitter.Model;

#if SILVERLIGHT && !WindowsPhone
using HttpUtility = System.Windows.Browser.HttpUtility;
#endif

namespace TweetSharp.Twitter.Fluent
{
    /// <summary>
    /// The base fluent query instance for the Twitter API.
    /// </summary>
    public interface IFluentTwitter : IFluentBase<TwitterResult>
    {
        /// <summary>
        /// Gets or sets the client info.
        /// This is used to set OAuth consumer information
        /// and other metadata on a per-request basis.
        /// </summary>
        /// <value>The client info.</value>
        TwitterClientInfo ClientInfo { get; set; }

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
        TwitterWebCallback Callback { get; set; }

        /// <summary>
        /// Gets or sets the authentication info for the query.
        /// </summary>
        /// <value>The authentication.</value>
        [EditorBrowsable(EditorBrowsableState.Never)]
        IFluentTwitterAuthentication Authentication { get; set; }

        /// <summary>
        /// Gets or sets the external authentication info used for third-party requests.
        /// </summary>
        /// <value>The external authentication.</value>
        [EditorBrowsable(EditorBrowsableState.Never)]
        Dictionary<AuthenticationMode, IExternalAuthenticationDetails> ExternalAuthentication { get; set; }
        
        /// <summary>
        /// Gets or sets the user profile query branch.
        /// </summary>
        /// <value>The profile.</value>
        [EditorBrowsable(EditorBrowsableState.Never)]
        IFluentTwitterProfile Profile { get; }

        /// <summary>
        /// Gets the query configuration.
        /// </summary>
        /// <value>The configuration.</value>
        [EditorBrowsable(EditorBrowsableState.Never)]
        IFluentTwitterConfiguration Configuration { get; }

        /// <summary>
        /// Gets the search parameters branch.
        /// </summary>
        /// <value>The search parameters.</value>
        [EditorBrowsable(EditorBrowsableState.Never)]
        IFluentTwitterSearchParameters SearchParameters { get; }

        /// <summary>
        /// Gets the streaming parameters branch.
        /// </summary>
        /// <value>The streaming parameters.</value>
        [EditorBrowsable(EditorBrowsableState.Never)]
        IFluentTwitterStreamingParameters StreamingParameters { get; }

        /// <summary>
        /// Gets the trends parameters branch.
        /// </summary>
        /// <value>The trends parameters.</value>
        [EditorBrowsable(EditorBrowsableState.Never)]
        IFluentTwitterTrendsParameters TrendsParameters { get; }

        /// <summary>
        /// Gets the query parameters branch.
        /// </summary>
        /// <value>The parameters.</value>
        [EditorBrowsable(EditorBrowsableState.Never)]
        IFluentTwitterParameters Parameters { get; }

        /// <summary>
        /// Gets the currently defined periodic task for this query.
        /// </summary>
        /// <value>The recurring task.</value>
        [EditorBrowsable(EditorBrowsableState.Never)]
        ITimedTask<IRateLimitStatus> RecurringTask { get; }

        /// <summary>
        /// Gets or sets the current rate limiting rule imposed by this query.
        /// </summary>
        /// <value>The rate limiting rule.</value>
        [EditorBrowsable(EditorBrowsableState.Never)]
        IRateLimitingRule<IRateLimitStatus> RateLimitingRule { get; set; }

        /// <summary>
        /// Gets or sets the repeat interval of a recurring task, if any.
        /// </summary>
        /// <value>The repeat interval.</value>
        TimeSpan RepeatInterval { get; set; }

        /// <summary>
        /// Gets or sets the repeat count of a recurring task, if any.
        /// </summary>
        /// <value>The repeat times.</value>
        int RepeatTimes { get; set; }

        /// <summary>
        /// Cancels any recurring or streaming tasks on this query node.
        /// </summary>
        void Cancel();

        /// <summary>
        /// Converts this query node into an API URL representation.
        /// </summary>
        /// <returns></returns>
        string AsUrl();
    }
}