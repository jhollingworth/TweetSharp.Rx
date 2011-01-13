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

#if !Smartphone && !SILVERLIGHT
using System.Net.Mail;
#endif
using System.Collections.Generic;
using System.Linq;

namespace TweetSharp.Twitter.Fluent
{
    /// <summary>
    /// Query expressions for the Twitter API users resource.
    /// </summary>
    public static class ITwitterUsersExtensions
    {
        /// <summary>
        /// Gets the friends of the authenticated user.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns></returns>
        public static ITwitterUserFriends GetFriends(this ITwitterUsers instance)
        {
            instance.Root.Parameters.Activity = "statuses";
            instance.Root.Parameters.Action = "friends";
            return new TwitterUserFriends(instance.Root);
        }

        /// <summary>
        /// Gets the followers of the authenticated user.
        /// </summary>
        public static ITwitterUserFollowers GetFollowers(this ITwitterUsers instance)
        {
            instance.Root.Parameters.Activity = "statuses";
            instance.Root.Parameters.Action = "followers";
            return new TwitterUserFollowers(instance.Root);
        }

        /// <summary>
        /// Shows the profile for the specified user by ID.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="id">The user ID.</param>
        /// <returns></returns>
        public static ITwitterUsersShow ShowProfileFor(this ITwitterUsers instance, int id)
        {
            instance.Root.Parameters.UserId = id;
            instance.Root.Parameters.Action = "show";
            return new TwitterUsersShow(instance.Root);
        }

        /// <summary>
        /// Shows the profile for the specified user by screen name.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="screenName">The user screen name.</param>
        /// <returns></returns>
        public static ITwitterUsersShow ShowProfileFor(this ITwitterUsers instance, string screenName)
        {
            instance.Root.Parameters.UserScreenName = screenName;
            instance.Root.Parameters.Action = "show";
            return new TwitterUsersShow(instance.Root);
        }

        /// <summary>
        /// Performs a user search given a query.
        /// This mimics Twitter's "People Search" web feature.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="query">The user search query.</param>
        /// <returns></returns>
        public static ITwitterUsersSearch SearchFor(this ITwitterUsers instance, string query)
        {
            instance.Root.Parameters.Action = "search";
            instance.Root.Parameters.UserSearch = query;

            return new TwitterUsersSearch(instance.Root);
        }

#if !Smartphone && !SILVERLIGHT
        /// <summary>
        /// Shows the profile for a specified user.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="email">The user's email.</param>
        /// <returns></returns>
        public static ITwitterUsersShow ShowProfileFor(this ITwitterUsers instance, MailAddress email)
        {
            instance.Root.Parameters.Action = "show";
            instance.Root.Parameters.Email = email.Address;
            return new TwitterUsersShow(instance.Root);
        }
#endif
        /// <summary>
        /// Searches for a specific user given a set of screen names.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="screenNames">The screen names to search for.</param>
        /// <returns></returns>
        public static ITwitterUsersLookup Lookup(this ITwitterUsers instance, IEnumerable<string> screenNames)
        {
            instance.Root.Parameters.Action = "lookup";
            instance.Root.Parameters.LookupScreenNames = screenNames;
            return new TwitterUsersLookup(instance.Root);
        }

        /// <summary>
        /// Searches for a specific user given a set of screen names.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="screenNames">The screen names.</param>
        /// <returns></returns>
        public static ITwitterUsersLookup Lookup(this ITwitterUsers instance, params string[] screenNames)
        {
            return instance.Lookup(screenNames.AsEnumerable());
        }

        /// <summary>
        /// Searches for a specific user given a set of screen names, and user IDs.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="screenNames">The screen names.</param>
        /// <param name="userIds">The user IDs.</param>
        /// <returns></returns>
        public static ITwitterUsersLookup Lookup(this ITwitterUsers instance, IEnumerable<string> screenNames, IEnumerable<int> userIds)
        {
            instance.Root.Parameters.Action = "lookup";
            instance.Root.Parameters.LookupScreenNames = screenNames;
            instance.Root.Parameters.LookupUserIds = userIds;
            return new TwitterUsersLookup(instance.Root);
        }

        /// <summary>
        /// Searches for a specific user given a set of user IDs.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="userIds">The user IDs.</param>
        /// <returns></returns>
        public static ITwitterUsersLookup Lookup(this ITwitterUsers instance, IEnumerable<int> userIds)
        {
            instance.Root.Parameters.Action = "lookup";
            instance.Root.Parameters.LookupUserIds = userIds;
            return new TwitterUsersLookup(instance.Root);
        }

        /// <summary>
        /// Searches for a specific user given a set of user IDs.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="userIds">The user ids.</param>
        /// <returns></returns>
        public static ITwitterUsersLookup Lookup(this ITwitterUsers instance, params int[] userIds)
        {
            return instance.Lookup(userIds.AsEnumerable());
        }
    }
}