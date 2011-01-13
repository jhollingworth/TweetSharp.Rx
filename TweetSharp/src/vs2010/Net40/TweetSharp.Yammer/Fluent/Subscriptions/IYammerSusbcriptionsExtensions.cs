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

using Hammock.Web;
using TweetSharp.Core;
using TweetSharp.Yammer.Model;


namespace TweetSharp.Yammer.Fluent
{
    /// <summary>
    /// Extension methods for accessing the subscriptions subset of the Yammer REST API.
    /// </summary>
    public static class YammerSusbcriptionsExtensions
    {
        /// <summary>
        /// Query for an existing subscription to another <see cref="YammerUser"/>
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="otherUserId">Id of the user to check</param>
        /// <returns></returns>
        public static IYammerSubscriptionsToUser GetSubscriptionToUser(this IYammerSubscriptions instance,
                                                                       long otherUserId)
        {
            instance.Root.Parameters.Action = "to_user";
            instance.Root.Parameters.Id = otherUserId;
            return new YammerSubscriptionsToUser(instance.Root);
        }

        /// <summary>
        /// Query for an existing subscription to a <see cref="YammerTag"/>. 
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="tagId">Id of the tag to check.</param>
        /// <returns></returns>
        public static IYammerSubscriptionsToTag GetSubscriptionToTag(this IYammerSubscriptions instance, long tagId)
        {
            instance.Root.Parameters.Action = "to_tag";
            instance.Root.Parameters.Id = tagId;
            return new YammerSubscriptionsToTag(instance.Root);
        }

        /// <summary>
        /// Subscribes to (follows) all messages with a specific tag.
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="tagId">The id of the tag to follow.</param>
        /// <returns></returns>
        public static IYammerSubscriptionCreate SubscribeToTag(this IYammerSubscriptions instance, long tagId)
        {
            instance.Root.Method = WebMethod.Post;
            instance.Root.Parameters.TargetId = tagId;
            instance.Root.Parameters.TargetType = "tag";
            return new YammerSubscriptionCreate(instance.Root);
        }

        /// <summary>
        /// Subscribes to (follows) another user's updates. 
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="userId">The Id of the user to follow.</param>
        /// <returns></returns>
        public static IYammerSubscriptionCreate SubscribeToUser(this IYammerSubscriptions instance, long userId)
        {
            instance.Root.Method = WebMethod.Post;
            instance.Root.Parameters.TargetId = userId;
            instance.Root.Parameters.TargetType = "user";
            return new YammerSubscriptionCreate(instance.Root);
        }

        /// <summary>
        /// Unsubscribes from a tag.
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="tagId">The id of the tag from which to unsubscribe.</param>
        /// <returns></returns>
        public static IYammerSubscriptionDelete UnsubscribeFromTag(this IYammerSubscriptions instance, long tagId)
        {
            instance.Root.Method = WebMethod.Delete;
            instance.Root.Parameters.TargetId = tagId;
            instance.Root.Parameters.TargetType = "tag";
            instance.Root.Format = WebFormat.None;
            return new YammerSubscriptionDelete(instance.Root);
        }

        /// <summary>
        /// Unsubscribes from another user's messages.
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="userId">The id of the user from which to unsubscribe.</param>
        /// <returns></returns>
        public static IYammerSubscriptionDelete UnsubscribeFromUser(this IYammerSubscriptions instance, long userId)
        {
            instance.Root.Method = WebMethod.Delete;
            instance.Root.Parameters.TargetId = userId;
            instance.Root.Parameters.TargetType = "user";
            instance.Root.Format = WebFormat.None;
            return new YammerSubscriptionDelete(instance.Root);
        }
    }
}