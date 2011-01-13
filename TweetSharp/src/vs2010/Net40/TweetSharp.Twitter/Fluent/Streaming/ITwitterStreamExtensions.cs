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

using TweetSharp.Twitter.Fluent.Streaming.User;

namespace TweetSharp.Twitter.Fluent
{
    /// <summary>
    /// Fluent interface methods for accessing the Streaming API.
    /// </summary>
    public static class ITwitterStreamExtensions
    {
        /// <summary>
        /// Creates a stream from of a random sample of public statuses.
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public static ITwitterStreamingSample FromSample(this ITwitterStream instance)
        {
            instance.Root.Parameters.Action = "sample";
            return new TwitterStreamingSample(instance.Root);
        }

        /// <summary>
        /// Creates a stream based on a search filter.
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public static ITwitterStreamingFilter FromFilter(this ITwitterStream instance)
        {
            instance.Root.Parameters.Action = "filter";
            return new TwitterStreamingFilter(instance.Root);
        }

        /// <summary>
        /// Creates a stream from all public statuses.
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public static ITwitterStreamingFirehose FromFirehose(this ITwitterStream instance)
        {
            instance.Root.Parameters.Action = "firehose";
            return new TwitterStreamingFirehose(instance.Root);
        }

        /// <summary>
        /// Creates a stream from all public retweets of other statuses.
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public static ITwitterStreamingRetweet FromRetweet(this ITwitterStream instance)
        {
            instance.Root.Parameters.Action = "retweet";
            return new TwitterStreamingRetweet(instance.Root);
        }

        public static ITwitterStreamingUser FromUser(this ITwitterStream instance)
        {
            instance.Root.Parameters.Action = "user";
            return new TwitterStreamingUser(instance.Root);
        }
    }
}