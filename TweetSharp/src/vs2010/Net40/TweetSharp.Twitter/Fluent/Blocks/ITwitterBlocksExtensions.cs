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

using TweetSharp.Twitter.Model;

namespace TweetSharp.Twitter.Fluent
{
    /// <summary>
    /// Fluent interface methods for the Block endpoints
    /// </summary>
    public static class ITwitterBlocksExtensions
    {
        /// <summary>
        /// Blocks a user from following the authenticated user
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="id">ID of the user to block</param>
        /// <returns></returns>
        public static ITwitterBlocksCreate Block(this ITwitterBlocks instance, int id)
        {
            instance.Root.Parameters.Action = "create";
            instance.Root.Parameters.Id = id;
            return new TwitterBlocksCreate(instance.Root);
        }

        /// <summary>
        /// Blocks a user from following the authenticated user
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="id">ID of the user to block</param>
        /// <returns></returns>
        public static ITwitterBlocksCreate Block(this ITwitterBlocks instance, long id)
        {
            instance.Root.Parameters.Action = "create";
            instance.Root.Parameters.Id = id;
            return new TwitterBlocksCreate(instance.Root);
        }

        /// <summary>
        /// Blocks a user from following the authenticated user
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="screenName">screen name of the user to block</param>
        /// <returns></returns>
        
        public static ITwitterBlocksCreate Block(this ITwitterBlocks instance, string screenName)
        {
            instance.Root.Parameters.Action = "create";
            instance.Root.Parameters.ScreenName = screenName;
            return new TwitterBlocksCreate(instance.Root);
        }

        /// <summary>
        /// Blocks a user from following the authenticated user
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="user"><see cref="TwitterUser"/> representing the user to block</param>
        /// <returns></returns>
        
        public static ITwitterBlocksCreate Block(this ITwitterBlocks instance, TwitterUser user)
        {
            instance.Root.Parameters.Action = "create";
            instance.Root.Parameters.ScreenName = user.ScreenName;
            return new TwitterBlocksCreate(instance.Root);
        }

        /// <summary>
        /// Unblocks a user, allowing them to follow the authenticated user
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="id">ID of the user to unblock</param>
        /// <returns></returns>
        
        public static ITwitterBlocksDestroy Unblock(this ITwitterBlocks instance, int id)
        {
            instance.Root.Parameters.Action = "destroy";
            instance.Root.Parameters.Id = id;
            return new TwitterBlocksDestroy(instance.Root);
        }

        /// <summary>
        /// Unblocks a user, allowing them to follow the authenticated user
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="id">ID of the user to unblock</param>
        /// <returns></returns>
        
        public static ITwitterBlocksDestroy Unblock(this ITwitterBlocks instance, long id)
        {
            instance.Root.Parameters.Action = "destroy";
            instance.Root.Parameters.Id = id;
            return new TwitterBlocksDestroy(instance.Root);
        }

        /// <summary>
        /// Unblocks a user, allowing them to follow the authenticated user
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="screenName">screen name of the user to unblock</param>
        /// <returns></returns>
        
        public static ITwitterBlocksDestroy Unblock(this ITwitterBlocks instance, string screenName)
        {
            instance.Root.Parameters.Action = "destroy";
            instance.Root.Parameters.ScreenName = screenName;
            return new TwitterBlocksDestroy(instance.Root);
        }

        /// <summary>
        /// Unblocks a user, allowing them to follow the authenticated user
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="user"><see cref="TwitterUser"/>representing the user to unblock</param>
        /// <returns></returns>
        
        public static ITwitterBlocksDestroy Unblock(this ITwitterBlocks instance, TwitterUser user)
        {
            instance.Root.Parameters.Action = "destroy";
            instance.Root.Parameters.ScreenName = user.ScreenName;
            return new TwitterBlocksDestroy(instance.Root);
        }

        /// <summary>
        /// Checks if the authenticating user is blocking the specified user
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="id">ID of the user whose block status to check</param>
        /// <returns></returns>
        
        public static ITwitterBlocksDestroy Exists(this ITwitterBlocks instance, long id)
        {
            instance.Root.Parameters.Action = "exists";
            instance.Root.Parameters.Id = id;
            return new TwitterBlocksDestroy(instance.Root);
        }

        /// <summary>
        /// Checks if the authenticating user is blocking the specified user
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="id">ID of the user whose block status to check</param>
        /// <returns></returns>
        
        public static ITwitterBlocksDestroy Exists(this ITwitterBlocks instance, int id)
        {
            instance.Root.Parameters.Action = "exists";
            instance.Root.Parameters.Id = id;
            return new TwitterBlocksDestroy(instance.Root);
        }

        /// <summary>
        /// Checks if the authenticating user is blocking the specified user
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="screenName">screen name of the user whose block status to check</param>
        /// <returns></returns>
        
        public static ITwitterBlocksExists Exists(this ITwitterBlocks instance, string screenName)
        {
            instance.Root.Parameters.Action = "exists";
            instance.Root.Parameters.ScreenName = screenName;
            return new TwitterBlocksExists(instance.Root);
        }

        /// <summary>
        /// Checks if the authenticating user is blocking the specified user
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="user"><see cref="TwitterUser"/> object representing the user whose block status to check</param>
        /// <returns></returns>
        
        public static ITwitterBlocksExists Exists(this ITwitterBlocks instance, TwitterUser user)
        {
            instance.Root.Parameters.Action = "exists";
            instance.Root.Parameters.ScreenName = user.ScreenName;
            return new TwitterBlocksExists(instance.Root);
        }

        /// <summary>
        /// Lists all users blocked by the authenticating user
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        
        public static ITwitterBlocksList ListUsers(this ITwitterBlocks instance)
        {
            instance.Root.Parameters.Action = "blocking";
            return new TwitterBlocksList(instance.Root);
        }

        /// <summary>
        /// Lists all ids of users blocked by the authenticating user
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        
        public static ITwitterBlocksListIds ListIds(this ITwitterBlocks instance)
        {
            instance.Root.Parameters.Action = "blocking/ids";
            return new TwitterBlocksListIds(instance.Root);
        }
    }
}