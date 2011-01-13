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

using TweetSharp.Core.Extensions;
using TweetSharp.Twitter.Model;

namespace TweetSharp.Twitter.Fluent
{
    /// <summary>
    /// Query expressions for the Twitter API statuses resource.
    /// </summary>
    public static class TwitterStatusesExtensions
    {
        /// <summary>
        /// Retrieves a selection of the most recent 20 public tweets.
        /// This query is cached by Twitter for 60 seconds.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns></returns>
        public static ITwitterPublicTimeline OnPublicTimeline(this ITwitterStatuses instance)
        {
            instance.Root.Parameters.Action = "public_timeline";
            return new TwitterPublicTimeline(instance.Root);
        }

        /// <summary>
        /// Retrieves the authenticating user's friends' timelines.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns></returns>
        public static ITwitterFriendsTimeline OnFriendsTimeline(this ITwitterStatuses instance)
        {
            instance.Root.Parameters.Action = "friends_timeline";
            return new TwitterFriendsTimeline(instance.Root);
        }

        /// <summary>
        /// Retrieves the authenticating user's own user timeline.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns></returns>
        public static ITwitterUserTimeline OnUserTimeline(this ITwitterStatuses instance)
        {
            instance.Root.Parameters.Action = "user_timeline";
            return new TwitterUserTimeline(instance.Root);
        }

        /// <summary>
        /// Retrieves the authenticating user's home timeline.
        /// The home timeline includes friends' timelines as well as retweets.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns></returns>
        public static ITwitterHomeTimeline OnHomeTimeline(this ITwitterStatuses instance)
        {
            instance.Root.Parameters.Action = "home_timeline";
            return new TwitterHomeTimeline(instance.Root);
        }

        /// <summary>
        /// Retrieves the timeline of a given list.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="listOwnerScreenName">The list owner's screen name.</param>
        /// <param name="listSlug">The list slug.</param>
        /// <returns></returns>
        public static ITwitterListTimeline OnListTimeline(this ITwitterStatuses instance, string listOwnerScreenName, string listSlug)
        {
            instance.Root.Parameters.Activity = "lists";
            instance.Root.Parameters.Action = "statuses";
            instance.Root.Parameters.ListSlug = listSlug;
            instance.Root.Parameters.UserScreenName = listOwnerScreenName;
            return new TwitterListTimeline(instance.Root);
        }

        /// <summary>
        /// Retrieves the timeline of a given list.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="listOwnerScreenName">The list owner's screen name.</param>
        /// <param name="listId">The list ID.</param>
        /// <returns></returns>
        public static ITwitterListTimeline OnListTimeline(this ITwitterStatuses instance, string listOwnerScreenName, int listId)
        {
            instance.Root.Parameters.Activity = "lists";
            instance.Root.Parameters.Action = "statuses";
            instance.Root.Parameters.ListId = listId;
            instance.Root.Parameters.UserScreenName = listOwnerScreenName;
            return new TwitterListTimeline(instance.Root);
        }

        /// <summary>
        /// Retrieves the timeline of a given list.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="listOwnerScreenName">The list owner's screen name.</param>
        /// <param name="listId">The list ID.</param>
        /// <returns></returns>
        public static ITwitterListTimeline OnListTimeline(this ITwitterStatuses instance, string listOwnerScreenName, long listId)
        {
            instance.Root.Parameters.Activity = "lists";
            instance.Root.Parameters.Action = "statuses";
            instance.Root.Parameters.ListId = listId;
            instance.Root.Parameters.UserScreenName = listOwnerScreenName;
            return new TwitterListTimeline(instance.Root);
        }

        /// <summary>
        /// Gets a specific <see cref="TwitterStatus" />.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="id">The status ID.</param>
        /// <returns></returns>
        public static ITwitterStatusShow Show(this ITwitterStatuses instance, long id)
        {
            instance.Root.Parameters.Action = "show";
            instance.Root.Parameters.Id = id;
            return new TwitterStatusShow(instance.Root);
        }

        /// <summary>
        /// Posts a new <see cref="TwitterStatus" /> update.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="text">The text.</param>
        /// <returns></returns>
        public static ITwitterStatusUpdate Update(this ITwitterStatuses instance, string text)
        {
            instance.Root.Parameters.Action = "update";
            instance.Root.Parameters.Text = text;
            return new TwitterStatusUpdate(instance.Root);
        }

        /// <summary>
        /// Posts a new <see cref="TwitterStatus"/> update with geo-tagged information.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="text">The text.</param>
        /// <param name="latitude">The latitude.</param>
        /// <param name="longitude">The longitude.</param>
        /// <returns></returns>
        public static ITwitterStatusUpdate Update(this ITwitterStatuses instance, string text, double latitude, double longitude)
        {
            var location = new TwitterGeoLocation
                               {
                                   Coordinates =
                                       new TwitterGeoLocation.GeoCoordinates
                                           {
                                               Latitude = latitude,
                                               Longitude = longitude
                                           }
                               };
            return instance.Update(text, location);
        }

        /// <summary>
        /// Posts a new <see cref="TwitterStatus"/> update with geo-tagged information.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="text">The text.</param>
        /// <param name="location">The geo location.</param>
        /// <returns></returns>
        public static ITwitterStatusUpdate Update(this ITwitterStatuses instance, string text, TwitterGeoLocation location)
        {
            instance.Root.Parameters.Action = "update";
            instance.Root.Parameters.Text = text;
            instance.Root.Parameters.GeoLocation = location;
            return new TwitterStatusUpdate(instance.Root);
        }

        /// <summary>
        /// Retweets a <see cref="TwitterStatus"/> update.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="status">The status.</param>
        /// <returns></returns>
        public static ITwitterStatusUpdate Retweet(this ITwitterStatuses instance, TwitterStatus status)
        {
            return instance.Retweet(status, RetweetMode.Native);
        }

        /// <summary>
        /// Retweets a <see cref="TwitterStatus"/> update.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="statusId">The status ID.</param>
        /// <returns></returns>
        public static ITwitterStatusUpdate Retweet(this ITwitterStatuses instance, long statusId)
        {
            return instance.Retweet(new TwitterStatus {Id = statusId}, RetweetMode.Native);
        }

        /// <summary>
        /// Retweets a <see cref="TwitterStatus"/> update.
        /// Uses a <see cref="RetweetMode" /> to define the style of retweet used.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="statusId">The status ID.</param>
        /// <param name="mode">The mode.</param>
        /// <returns></returns>
        public static ITwitterStatusUpdate Retweet(this ITwitterStatuses instance, long statusId, RetweetMode mode)
        {
            return instance.Retweet(new TwitterStatus {Id = statusId}, mode);
        }

        /// <summary>
        /// Retweets a <see cref="TwitterStatus"/> update.
        /// Uses a <see cref="RetweetMode"/> to define the style of retweet used.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="status">The status.</param>
        /// <param name="mode">The mode.</param>
        /// <returns></returns>
        public static ITwitterStatusUpdate Retweet(this ITwitterStatuses instance, TwitterStatus status, RetweetMode mode)
        {
            instance.Root.Parameters.Action = "update";
            switch (mode)
            {
                case RetweetMode.Native:
                    instance.Root.Parameters.Action = "retweet";
                    instance.Root.Parameters.Id = status.Id;
                    break;
                case RetweetMode.SymbolPrefix:
                    instance.Root.Parameters.Text =
                        '\u2672'.ToString().Then(" ").Then(status.Text);
                    break;
                case RetweetMode.Prefix:
                    var rt = "@{0} {1}".FormatWith(status.User.ScreenName, status.Text);
                    instance.Root.Parameters.Text = "RT ".Then(rt);
                    break;
                case RetweetMode.Suffix:
                    var via = "{1} (via @{0})".FormatWith(status.User.ScreenName, status.Text);
                    instance.Root.Parameters.Text = via;
                    break;
                default:
                    throw new TweetSharpException("Unknown retweet mode specified.");
            }

            return new TwitterStatusUpdate(instance.Root);
        }

        /// <summary>
        /// Retrieves the retweets of a given status ID.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="statusId">The status ID.</param>
        /// <returns></returns>
        public static ITwitterRetweets RetweetsOf(this ITwitterStatuses instance, long statusId)
        {
            instance.Root.Parameters.Action = "retweets";
            instance.Root.Parameters.Id = statusId;
            return new TwitterRetweets(instance.Root);
        }

        /// <summary>
        /// Retrieves the retweets of a given <see cref="TwitterStatus"/>.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="status">The status.</param>
        /// <returns></returns>
        public static ITwitterRetweets RetweetsOf(this ITwitterStatuses instance, TwitterStatus status)
        {
            return RetweetsOf(instance, status.Id);
        }

        /// <summary>
        /// Retrieves the mentions of the authenticating user.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns></returns>
        public static ITwitterStatusMentions Mentions(this ITwitterStatuses instance)
        {
            instance.Root.Parameters.Action = "mentions";
            return new TwitterStatusReplies(instance.Root);
        }

        /// <summary>
        /// Deletes a <see cref="TwitterStatus"/>.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="id">The status ID.</param>
        /// <returns></returns>
        public static ITwitterStatusDestroy Destroy(this ITwitterStatuses instance, long id)
        {
            instance.Root.Parameters.Action = "destroy";
            instance.Root.Parameters.Id = id;
            return new TwitterStatusDestroy(instance.Root);
        }

        /// <summary>
        /// Retrieves tweets retweeted by the authenticating user.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns></returns>
        public static ITwitterRetweetedByMe RetweetedByMe(this ITwitterStatuses instance)
        {
            instance.Root.Parameters.Action = "retweeted_by_me";
            return new TwitterRetweetedByMe(instance.Root);
        }

        /// <summary>
        /// Retrieves tweets retweeted to the authenticating user.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns></returns>
        public static ITwitterRetweetedToMe RetweetedToMe(this ITwitterStatuses instance)
        {
            instance.Root.Parameters.Action = "retweeted_to_me";
            return new TwitterRetweetedToMe(instance.Root);
        }

        /// <summary>
        /// Retrieves tweets by the authenticated user, and retweeted by others.
        /// </summary>
        public static ITwitterRetweetsOfMe RetweetsOfMe(this ITwitterStatuses instance)
        {
            instance.Root.Parameters.Action = "retweets_of_me";
            return new TwitterRetweetsOfMe(instance.Root);
        }
    }
}