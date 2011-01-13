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

namespace TweetSharp.Twitter.Fluent
{
    ///<summary>
    /// Conditions that will cause the query to be retried. Can be combined
    ///</summary>
    [Flags]
    public enum RetryOn
    {
        /// <summary>
        /// The query should not be retried
        /// </summary>
        Never = 0,
        /// <summary>
        /// The query should be retried when twitter responsds with the FailWhale page
        /// </summary>
        FailWhale = 1,
        /// <summary>
        /// The query should be retried with the service responds with a non-success status
        /// </summary>
        ServiceError = 2,
        /// <summary>
        /// The query should be retried when it times out
        /// </summary>
        Timeout = 4,
        /// <summary>
        /// The query should be retired when it times out or another network error occurs
        /// </summary>
        Network = 12, //includes Timeout
        /// <summary>
        /// The query should be retried when it times out, returns a failwhale, or another network error occurs
        /// </summary>
        FailWhaleOrNetwork = FailWhale | Network,
        /// <summary>
        /// The query should be retried when the connection is forcibly closed by the server
        /// </summary>
        ConnectionClosed = 24
    }
}