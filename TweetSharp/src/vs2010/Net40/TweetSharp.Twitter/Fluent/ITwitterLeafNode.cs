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
using TweetSharp.Fluent;
using TweetSharp.Twitter.Model;

namespace TweetSharp.Twitter.Fluent
{
    /// <summary>
    /// The interface describing a Twitter leaf node in the fluent expression tree.
    /// </summary>
    public interface ITwitterLeafNode : ITwitterNode, IRequestPattern<TwitterResult>
    {
        /// <summary>
        /// Converts the query into a URL representation.
        /// </summary>
        /// <returns></returns>
        string AsUrl();

        /// <summary>
        /// Authenticates as a user using real credentials.
        /// This is facilitated by HTTP Basic Authorization, that
        /// URL-encodes a username and password in the Authorization request header.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        [Obsolete]
        ITwitterLeafNode AuthenticateAs(string username, string password);

        /// <summary>
        /// Authenticates as a user using delegated token-based credentials.
        /// This is facilitated by OAuth v1.0a, that passes encrypted
        /// variables in the Authorization request header.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="tokenSecret">The token secret.</param>
        /// <returns></returns>
        ITwitterLeafNode AuthenticateWith(string token, string tokenSecret);

        /// <summary>
        /// Calling this method will establish the asynchronous callback used when the request receives a response.
        /// </summary>
        /// <param name="callback">The callback.</param>
        /// <returns></returns>
        ITwitterLeafNode CallbackTo(TwitterWebCallback callback);

        /// <summary>
        /// Repeats an asynchronous query for the allotted time span.
        /// </summary>
        /// <param name="timeSpan">The time span.</param>
        /// <returns></returns>
        ITwitterLeafNode RepeatEvery(TimeSpan timeSpan);

        /// <summary>
        /// Repeats an asynchronous query for the allotted time span,
        /// Up to the number of times specified.
        /// </summary>
        /// <param name="timeSpan">The time span.</param>
        /// <param name="times">The times.</param>
        /// <returns></returns>
        ITwitterLeafNode RepeatAfter(TimeSpan timeSpan, int times);
    }
}