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
    /// Fluent interface methods for accessing the Twitter Favorites endpoints.
    /// </summary>
    public static class ITwitterFavoritesExtensions
    {
        /// <summary>
        /// Gets the tweets marked as favorites by the authenticating user.
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public static ITwitterFavoritesGet GetFavorites(this ITwitterFavorites instance)
        {
            return new TwitterFavoritesGet(instance.Root);
        }

        /// <summary>
        /// Gets the tweets marked as favorites for the specified user.
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="screenName">The screen name of the user for whom to get the list of favorites</param>
        /// <returns></returns>
        public static ITwitterFavoritesGet GetFavoritesFor(this ITwitterFavorites instance, string screenName)
        {
            instance.Root.Parameters.UserScreenName = screenName;
            return new TwitterFavoritesGet(instance.Root);
        }

        /// <summary>
        /// Gets the tweets marked as favorites for the specified user.
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="id">The ID of the user for whom to get the list of favorites</param>
        /// <returns></returns>
        public static ITwitterFavoritesGet GetFavoritesFor(this ITwitterFavorites instance, int id)
        {
            instance.Root.Parameters.UserId = id;
            return new TwitterFavoritesGet(instance.Root);
        }

        /// <summary>
        /// Gets the tweets marked as favorites for the specified user.
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="id">The ID of the user for whom to get the list of favorites</param>
        /// <returns></returns>
        public static ITwitterFavoritesGet GetFavoritesFor(this ITwitterFavorites instance, long id)
        {
            instance.Root.Parameters.UserId = id;
            return new TwitterFavoritesGet(instance.Root);
        }

        /// <summary>
        /// Marks a tweet as one of the authenticating user's favorites.
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="id">Id of the <see cref="TwitterStatus"/> to mark as a favorite</param>
        /// <returns></returns>
        public static ITwitterFavoritesCreate Favorite(this ITwitterFavorites instance, int id)
        {
            instance.Root.Parameters.Action = "create";
            instance.Root.Parameters.Id = id;
            return new TwitterFavoritesCreate(instance.Root);
        }

        /// <summary>
        /// Marks a tweet as one of the authenticating user's favorites.
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="id">Id of the <see cref="TwitterStatus"/> to mark as a favorite</param>
        /// <returns></returns>
        public static ITwitterFavoritesCreate Favorite(this ITwitterFavorites instance, long id)
        {
            instance.Root.Parameters.Action = "create";
            instance.Root.Parameters.Id = id;
            return new TwitterFavoritesCreate(instance.Root);
        }

        /// <summary>
        /// Marks a tweet as one of the authenticating user's favorites.
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="id">The <see cref="TwitterStatus"/> to mark as a favorite</param>
        /// <returns></returns>
        public static ITwitterFavoritesCreate Favorite(this ITwitterFavorites instance, TwitterStatus id)
        {
            instance.Root.Parameters.Action = "create";
            instance.Root.Parameters.Id = id.Id;
            return new TwitterFavoritesCreate(instance.Root);
        }

        /// <summary>
        /// Unmarks a tweet as one of the authenticating user's favorites.
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="id">The ID of the <see cref="TwitterStatus"/> to unmark as a favorite</param>
        /// <returns></returns>
        public static ITwitterFavoritesDestroy Unfavorite(this ITwitterFavorites instance, int id)
        {
            instance.Root.Parameters.Action = "destroy";
            instance.Root.Parameters.Id = id;
            return new TwitterFavoritesDestroy(instance.Root);
        }

        /// <summary>
        /// Unmarks a tweet as one of the authenticating user's favorites.
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="id">The ID of the <see cref="TwitterStatus"/> to unmark as a favorite</param>
        /// <returns></returns>
        public static ITwitterFavoritesDestroy Unfavorite(this ITwitterFavorites instance, long id)
        {
            instance.Root.Parameters.Action = "destroy";
            instance.Root.Parameters.Id = id;
            return new TwitterFavoritesDestroy(instance.Root);
        }

        /// <summary>
        /// Unmarks a tweet as one of the authenticating user's favorites.
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="id">The <see cref="TwitterStatus"/> to unmark as a favorite</param>
        /// <returns></returns>
        public static ITwitterFavoritesDestroy Unfavorite(this ITwitterFavorites instance, TwitterStatus id)
        {
            instance.Root.Parameters.Action = "destroy";
            instance.Root.Parameters.Id = id.Id;
            return new TwitterFavoritesDestroy(instance.Root);
        }
    }
}