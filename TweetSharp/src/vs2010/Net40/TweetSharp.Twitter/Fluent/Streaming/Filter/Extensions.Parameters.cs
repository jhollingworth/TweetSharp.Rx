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

using System.Collections.Generic;
using System.Linq;
using TweetSharp.Twitter.Model;

namespace TweetSharp.Twitter.Fluent
{
    public partial class Extensions
    {
        /// <summary>
        /// Requests a backlog of tweets before streaming live.
        /// Use a value between -150,000 and 150,000.
        /// If a negative value is passed, the stream will close after the backlog is sent.
        /// <remarks>
        /// You must have elevated access (greater than "default") to use this parameter.
        /// </remarks>
        /// </summary>
        /// <seealso href="http://apiwiki.twitter.com/Streaming-API-Documentation" />
        /// <param name="instance">The query chain.</param>
        /// <param name="count">The number of previous statuses to backlog.</param>
        /// <returns>The query chain.</returns>
        public static ITwitterStreamingFilter WithBacklog(this ITwitterStreamingFilter instance, int count)
        {
            if (count < -150000 && count != 0) count = -150000;
            if (count > 150000) count = 150000;

            instance.Root.StreamingParameters.Count = count;
            return instance;
        }

        /// <summary>
        /// Defines the number of spaces between streaming results.
        /// Generally, you do not need to set this.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="length">The length.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Streaming-API-Documentation" />
        public static ITwitterStreamingFilter DelimitedBy(this ITwitterStreamingFilter instance, int length)
        {
            instance.Root.StreamingParameters.Length = length;
            return instance;
        }

        /// <summary>
        /// Specifies a user filter for streaming.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="users">The users.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Streaming-API-Documentation" />
        public static ITwitterStreamingFilter Following(this ITwitterStreamingFilter instance,
                                                        IEnumerable<TwitterUser> users)
        {
            instance.Root.StreamingParameters.UserIds = users.Select(u => u.Id);
            return instance;
        }

        /// <summary>
        /// Specifies a user filter for streaming.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="userIds">The user IDs.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Streaming-API-Documentation" />
        public static ITwitterStreamingFilter Following(this ITwitterStreamingFilter instance, IEnumerable<int> userIds)
        {
            instance.Root.StreamingParameters.UserIds = userIds;
            return instance;
        }

        /// <summary>
        /// Specifies a user filter for streaming.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="users">The users.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Streaming-API-Documentation" />
        public static ITwitterStreamingFilter Following(this ITwitterStreamingFilter instance,
                                                        params TwitterUser[] users)
        {
            instance.Root.StreamingParameters.UserIds = users.Select(u => u.Id);
            return instance;
        }

        /// <summary>
        /// Specifies a user filter for streaming.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="userIds">The user IDs.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Streaming-API-Documentation" />
        public static ITwitterStreamingFilter Following(this ITwitterStreamingFilter instance, params int[] userIds)
        {
            instance.Root.StreamingParameters.UserIds = userIds;
            return instance;
        }

        /// <summary>
        /// Specifies keywords to filter the stream.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="keywords">The keywords.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Streaming-API-Documentation" />
        public static ITwitterStreamingFilter Tracking(this ITwitterStreamingFilter instance, IEnumerable<string> keywords)
        {
            keywords = keywords.Select(k => k.Substring(30));
            instance.Root.StreamingParameters.Keywords = keywords;
            return instance;
        }

        /// <summary>
        /// Specifies keywords to filter the stream.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="keywords">The keywords.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Streaming-API-Documentation" />
        public static ITwitterStreamingFilter Tracking(this ITwitterStreamingFilter instance, params string[] keywords)
        {
            keywords = keywords.Select(k => k.Length > 30 ? k.Substring(30) : k).ToArray();
            instance.Root.StreamingParameters.Keywords = keywords;
            return instance;
        }

        /// <summary>
        /// Specifies a range of geo-locations to filter the stream.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="locations">The locations.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Streaming-API-Documentation" />
        public static ITwitterStreamingFilter Within(this ITwitterStreamingFilter instance,
                                                     IEnumerable<TwitterGeoLocation> locations)
        {
            if (locations.Count() > 10)
            {
                locations = locations.Take(10);
            }

            instance.Root.StreamingParameters.Locations = locations;
            return instance;
        }

        /// <summary>
        /// Specifies a range of geo-locations to filter the stream.
        /// Uses bounding boxes.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="southWest">The south west bounding box.</param>
        /// <param name="northEast">The north east bounding box.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Streaming-API-Documentation" />
        public static ITwitterStreamingFilter Within(this ITwitterStreamingFilter instance, TwitterGeoLocation southWest, TwitterGeoLocation northEast)
        {
            return instance.Within(new[] {southWest, northEast});
        }

        /// <summary>
        /// Specifies a range of geo-locations to filter the stream.
        /// Uses bounding boxes.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="locations">The bounding box locations.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Streaming-API-Documentation" />
        public static ITwitterStreamingFilter Within(this ITwitterStreamingFilter instance, params TwitterGeoLocation[] locations)
        {
            if (locations.Count() > 10)
            {
                locations = locations.ToList().Take(10).ToArray();
            }

            instance.Root.StreamingParameters.Locations = locations;
            return instance;
        }
    }
}