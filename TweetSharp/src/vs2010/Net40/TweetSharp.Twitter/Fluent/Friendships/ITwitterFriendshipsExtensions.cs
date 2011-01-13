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
    /// Fluent interface methods for dealing with friendship endpoints
    /// </summary>
    public static class ITwitterFriendshipsExtensions
    {
        /// <summary>
        /// Follows another user from the authenticating user.
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="id">The ID of the user to follow</param>
        /// <returns></returns>
        public static ITwitterFriendshipsCreate Befriend(this ITwitterFriendships instance, int id)
        {
            instance.Root.Parameters.Action = "create";
            instance.Root.Parameters.Id = id;
            return new TwitterFriendshipsCreate(instance.Root);
        }
    
        /// <summary>
        /// Follows another user from the authenticating user.
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="id">The ID of the user to follow</param>
        /// <returns></returns>
        public static ITwitterFriendshipsCreate Befriend(this ITwitterFriendships instance, long id)
        {
            instance.Root.Parameters.Action = "create";
            instance.Root.Parameters.Id = id;
            return new TwitterFriendshipsCreate(instance.Root);
        }

        /// <summary>
        /// Follows another user from the authenticating user.
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="screenName">The user name of the user to follow</param>
        /// <returns></returns>
        public static ITwitterFriendshipsCreate Befriend(this ITwitterFriendships instance, string screenName)
        {
            instance.Root.Parameters.Action = "create";
            instance.Root.Parameters.ScreenName = screenName;
            return new TwitterFriendshipsCreate(instance.Root);
        }

        /// <summary>
        /// Unfollows another user from the authenticating user.
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="id">The ID of the user to unfollow</param>
        /// <returns></returns>
        public static ITwitterFriendshipsDestroy Destroy(this ITwitterFriendships instance, int id)
        {
            instance.Root.Parameters.Action = "destroy";
            instance.Root.Parameters.Id = id;
            return new TwitterFriendshipsDestroy(instance.Root);
        }

        /// <summary>
        /// Unfollows another user from the authenticating user.
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="id">The ID of the user to unfollow</param>
        /// <returns></returns>
        public static ITwitterFriendshipsDestroy Destroy(this ITwitterFriendships instance, long id)
        {
            instance.Root.Parameters.Action = "destroy";
            instance.Root.Parameters.Id = id;
            return new TwitterFriendshipsDestroy(instance.Root);
        }

        /// <summary>
        /// Unfollows another user from the authenticating user.
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="screenName">The user name of the user to unfollow</param>
        /// <returns></returns>
        public static ITwitterFriendshipsDestroy Destroy(this ITwitterFriendships instance, string screenName)
        {
            instance.Root.Parameters.Action = "destroy";
            instance.Root.Parameters.ScreenName = screenName;
            return new TwitterFriendshipsDestroy(instance.Root);
        }

        /// <summary>
        /// Queries whether or not the authenticating user follows another user.
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="screenName">The user name of the user to check for a follower relationship</param>
        /// <seealso cref="ITwitterFriendshipsExtensions.Show(ITwitterFriendships, string)"/>
        /// <returns></returns>
        public static ITwitterFriendshipsExists Verify(this ITwitterFriendships instance, string screenName)
        {
            instance.Root.Parameters.Action = "exists";
            instance.Root.Parameters.UserScreenName = screenName;
            return new TwitterFriendshipsExists(instance.Root);
        }

        /// <summary>
        /// Queries whether or not the authenticating user follows another user.
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="id">The ID of the user to check for a follower relationship</param>
        /// <seealso cref="ITwitterFriendshipsExtensions.Show(ITwitterFriendships, int)"/>
        /// <returns></returns>
        public static ITwitterFriendshipsExists Verify(this ITwitterFriendships instance, int id)
        {
            instance.Root.Parameters.Action = "exists";
            instance.Root.Parameters.UserId = id;
            return new TwitterFriendshipsExists(instance.Root);
        }

        /// <summary>
        /// Queries whether or not the authenticating user follows another user.
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="id">The ID of the user to check for a follower relationship</param>
        /// <seealso cref="ITwitterFriendshipsExtensions.Show(ITwitterFriendships, long)"/>
        /// <returns></returns>
        public static ITwitterFriendshipsExists Verify(this ITwitterFriendships instance, long id)
        {
            instance.Root.Parameters.Action = "exists";
            instance.Root.Parameters.UserId = id;
            return new TwitterFriendshipsExists(instance.Root);
        }


        /// <summary>
        /// Gets detailed information about the relationship between the authenticated user and another user.
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="targetId">Id of the user to get detailed relationship information about</param>
        /// <returns></returns>
        public static ITwitterFriendshipsShow Show(this ITwitterFriendships instance, long targetId)
        {
            instance.Root.Parameters.Action = "show";
            instance.Root.Parameters.TargetId = targetId;
            return new TwitterFriendshipsShow(instance.Root);
        }

        /// <summary>
        /// Gets detailed information about the relationship between the authenticated user and another user.
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="targetId">Id of the user to get detailed relationship information about</param>
        /// <returns></returns>
        public static ITwitterFriendshipsShow Show(this ITwitterFriendships instance, int targetId)
        {
            instance.Root.Parameters.Action = "show";
            instance.Root.Parameters.TargetId = targetId;
            return new TwitterFriendshipsShow(instance.Root);
        }

        /// <summary>
        /// Gets detailed information about the relationship between the authenticated user and another user.
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="targetScreenName">User name of the user to get detailed relationship information about</param>
        /// <returns></returns>
        public static ITwitterFriendshipsShow Show(this ITwitterFriendships instance, string targetScreenName)
        {
            instance.Root.Parameters.Action = "show";
            instance.Root.Parameters.TargetScreenName = targetScreenName;
            return new TwitterFriendshipsShow(instance.Root);
        }

        /// <summary>
        /// Gets detailed information about the relationship between two users.
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="sourceScreenName">screen name of the subject user</param>
        /// <param name="targetScreenName">screen name of the user that the subject user is following</param>
        /// <returns></returns>
        public static ITwitterFriendshipsShow Show(this ITwitterFriendships instance, string sourceScreenName,
                                                   string targetScreenName)
        {
            instance.Root.Parameters.Action = "show";
            instance.Root.Parameters.SourceScreenName = sourceScreenName;
            instance.Root.Parameters.TargetScreenName = targetScreenName;
            return new TwitterFriendshipsShow(instance.Root);
        }

        /// <summary>
        /// Gets detailed information about the relationship between two users.
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="sourceId">ID of the subject user</param>
        /// <param name="targetScreenName">screen name of the user that the subject user is following</param>
        /// <returns></returns>
        public static ITwitterFriendshipsShow Show(this ITwitterFriendships instance, int sourceId,
                                                   string targetScreenName)
        {
            instance.Root.Parameters.Action = "show";
            instance.Root.Parameters.SourceId = sourceId;
            instance.Root.Parameters.TargetScreenName = targetScreenName;
            return new TwitterFriendshipsShow(instance.Root);
        }

        /// <summary>
        /// Gets detailed information about the relationship between two users.
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="sourceId">ID of the subject user</param>
        /// <param name="targetScreenName">screen name of the user that the subject user is following</param>
        /// <returns></returns>
        public static ITwitterFriendshipsShow Show(this ITwitterFriendships instance, long sourceId,
                                                   string targetScreenName)
        {
            instance.Root.Parameters.Action = "show";
            instance.Root.Parameters.SourceId = sourceId;
            instance.Root.Parameters.TargetScreenName = targetScreenName;
            return new TwitterFriendshipsShow(instance.Root);
        }

        /// <summary>
        /// Gets detailed information about the relationship between two users.
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="sourceId">ID of the subject user</param>
        /// <param name="targetId">ID of the user that the subject user is following</param>
        /// <returns></returns>
        public static ITwitterFriendshipsShow Show(this ITwitterFriendships instance, long sourceId, long targetId)
        {
            instance.Root.Parameters.Action = "show";
            instance.Root.Parameters.SourceId = sourceId;
            instance.Root.Parameters.TargetId = targetId;
            return new TwitterFriendshipsShow(instance.Root);
        }

        /// <summary>
        /// Gets detailed information about the relationship between two users.
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="sourceId">ID of the subject user</param>
        /// <param name="targetId">ID of the user that the subject user is following</param>
        /// <returns></returns>
        public static ITwitterFriendshipsShow Show(this ITwitterFriendships instance, int sourceId, int targetId)
        {
            instance.Root.Parameters.Action = "show";
            instance.Root.Parameters.SourceId = sourceId;
            instance.Root.Parameters.TargetId = targetId;
            return new TwitterFriendshipsShow(instance.Root);
        }

        /// <summary>
        /// Gets detailed information about the relationship between two users.
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="sourceScreenName">name of the subject user</param>
        /// <param name="targetId">ID of the user that the subject user is following</param>
        /// <returns></returns>
        public static ITwitterFriendshipsShow Show(this ITwitterFriendships instance, string sourceScreenName,
                                                   int targetId)
        {
            instance.Root.Parameters.Action = "show";
            instance.Root.Parameters.SourceScreenName = sourceScreenName;
            instance.Root.Parameters.TargetId = targetId;
            return new TwitterFriendshipsShow(instance.Root);
        }

        /// <summary>
        /// Gets detailed information about the relationship between two users.
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="sourceScreenName">name of the subject user</param>
        /// <param name="targetId">ID of the user that the subject user is following</param>
        /// <returns></returns>
        public static ITwitterFriendshipsShow Show(this ITwitterFriendships instance, string sourceScreenName,
                                                   long targetId)
        {
            instance.Root.Parameters.Action = "show";
            instance.Root.Parameters.SourceScreenName = sourceScreenName;
            instance.Root.Parameters.TargetId = targetId;
            return new TwitterFriendshipsShow(instance.Root);
        }

        /// <summary>
        /// Gets a list of user IDs who have submitted requests to follow the protected account of the authenticating user.
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public static ITwitterFriendshipsIncoming Incoming( this ITwitterFriendships instance )
        {
            instance.Root.Parameters.Action = "incoming";
            return new TwitterFriendshipsIncoming(instance.Root);
        }

        /// <summary>
        /// Gets a list of user IDs to whom the authenticated user has submitted requests to follow and which are still pending.
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public static ITwitterFriendshipsOutgoing Outgoing(this ITwitterFriendships instance)
        {
            instance.Root.Parameters.Action = "outgoing";
            return new TwitterFriendshipsOutgoing(instance.Root);
        }
    }
}