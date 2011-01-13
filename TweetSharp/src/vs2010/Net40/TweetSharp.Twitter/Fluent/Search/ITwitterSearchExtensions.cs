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

namespace TweetSharp.Twitter.Fluent
{
    /// <summary>
    /// Fluent interface methods for the search api
    /// </summary>
    public static class ITwitterSearchExtensions
    {
        /// <summary>
        /// Perform a query via the search api
        /// </summary>
        /// <param name="instance">the instance</param>
        /// <returns></returns>
        public static ITwitterSearchQuery Query(this ITwitterSearch instance)
        {
            instance.Root.Parameters.Action = "search";
            return new TwitterSearchQuery(instance.Root);
        }

        /// <summary>
        /// Request the current or historical trending topics 
        /// </summary>
        /// <param name="instance">the instance</param>
        /// <returns></returns>
        public static ITwitterSearchTrends Trends(this ITwitterSearch instance)
        {
            instance.Root.Parameters.Action = "trends";
            return new TwitterSearchTrends(instance.Root);
        }
    }
}