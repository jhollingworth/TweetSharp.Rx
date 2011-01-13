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
using TweetSharp.Twitter.Fluent.Streaming.User;

namespace TweetSharp.Twitter.Fluent
{
    public partial class Extensions
    {
        /// <summary>
        /// Sets the amount of time to listen to the sampling stream for
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="duration">The duration of the streaming operation</param>
        /// <returns></returns>
        public static ITwitterStreamingUser For(this ITwitterStreamingUser instance, TimeSpan duration)
        {
            instance.Root.StreamingParameters.Duration = duration;
            return instance;
        }

        /// <summary>
        /// Sets the number of statuses to wait for from the stream before invoking the callback method
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="resultsPerCallback">The number of results to acquire from the stream between callback invocations</param>
        /// <returns></returns>
        public static ITwitterStreamingUser Take(this ITwitterStreamingUser instance, int resultsPerCallback)
        {
            instance.Root.StreamingParameters.ResultsPerCallback = resultsPerCallback;
            return instance;
        }
    }
}