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
    public static partial class Extensions
    {
        /// <summary>
        /// Requests only statuses posted after the status with a specific id
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="id">ID of a status that is the lower limit for requested statuses</param>
        /// <returns></returns>
        public static ITwitterUserTimeline Since(this ITwitterUserTimeline instance, int id)
        {
            instance.Root.Parameters.SinceId = id;
            return instance;
        }
        /// <summary>
        /// Requests only statuses posted after the status with a specific id
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="id">ID of a status that is the lower limit for requested statuses</param>
        /// <returns></returns>
        public static ITwitterUserTimeline Since(this ITwitterUserTimeline instance, long id)
        {
            instance.Root.Parameters.SinceId = id;
            return instance;
        }
        /// <summary>
        /// Requests only statuses posted before the status with a specific id
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="id">ID of a status that is the upper limit for requested statuses</param>
        /// <returns></returns>
        public static ITwitterUserTimeline Before(this ITwitterUserTimeline instance, int id)
        {
            instance.Root.Parameters.MaxId = id;
            return instance;
        }
        /// <summary>
        /// Requests only statuses posted before the status with a specific id
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="id">ID of a status that is the upper limit for requested statuses</param>
        /// <returns></returns>
        public static ITwitterUserTimeline Before(this ITwitterUserTimeline instance, long id)
        {
            instance.Root.Parameters.MaxId = id;
            return instance;
        }
        /// <summary>
        /// Requests a specific page of statuses when paging
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="page">The expected page</param>
        /// <returns></returns>
        public static ITwitterUserTimeline Skip(this ITwitterUserTimeline instance, int page)
        {
            instance.Root.Parameters.Page = page;
            return instance;
        }
        /// <summary>
        /// Requests a specific number of statuses per page.
        /// Restricted by Twitter API pagination limits.
        /// </summary>
        /// <seealso href="http://apiwiki.twitter.com/Things-Every-Developer-Should-Know#6Therearepaginationlimits" />
        /// <remarks>
        /// </remarks>
        /// <param name="instance"></param>
        /// <param name="count">The number of expected results</param>
        /// <returns></returns>
        public static ITwitterUserTimeline Take(this ITwitterUserTimeline instance, int count)
        {
            instance.Root.Parameters.Count = count;
            return instance;
        }

        /// <summary>
        /// This query pertains to the given user.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="screenName">The user screen name.</param>
        /// <returns></returns>
        public static ITwitterUserTimeline For(this ITwitterUserTimeline instance, string screenName)
        {
            instance.Root.Parameters.ScreenName = screenName;
            return instance;
        }

        /// <summary>
        /// This query pertains to the given user.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="id">The user ID.</param>
        /// <returns></returns>
        public static ITwitterUserTimeline For(this ITwitterUserTimeline instance, int id)
        {
            instance.Root.Parameters.Id = id;
            return instance;
        }
    }
}