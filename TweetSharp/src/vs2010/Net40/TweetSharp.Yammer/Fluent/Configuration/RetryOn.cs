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

namespace TweetSharp.Yammer.Fluent
{
    /// <summary>
    /// Sets conditions on which TweetSharp should retry a failed query (can be combined).
    /// </summary>
    [Flags]
    public enum RetryOn
    {
        /// <summary>
        /// Never retry failed queries
        /// </summary>
        Never = 0,
        /// <summary>
        /// Retry when the service returns an error result.
        /// </summary>
        ServiceError = 2,
        /// <summary>
        /// Retry when the request times out
        /// </summary>
        Timeout = 4,
        /// <summary>
        /// Retry on any network related error (includes timeout)
        /// </summary>
        Network = 12, // includes Timeout
        /// <summary>
        /// Retry when the connection is closed unexpectedly
        /// </summary>
        ConnectionClosed = 24
    }
}