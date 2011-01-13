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

namespace TweetSharp.Yammer.Fluent
{
    /// <summary>
    /// Extension methods for accessing the Favorite Messages subset of the Yammer REST API
    /// </summary>
    public static class IYammerFavoriteMessagesExtensions
    {
        /// <summary>
        /// Create a new favorite message
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="id">The id of the message to favorite</param>
        /// <returns></returns>
        public static IYammerFavoritesCreate Create(this IYammerFavoriteMessages instance, long id)
        {
            instance.Root.Parameters.MessageId = id;
            return new YammerFavoritesCreate(instance.Root);
        }

        /// <summary>
        /// Remove an existing favorite
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="id">The id of the message to un-favorite</param>
        /// <returns></returns>
        public static IYammerFavoritesDestroy Destroy(this IYammerFavoriteMessages instance, long id)
        {
            instance.Root.Parameters.MessageId = id;
            return new YammerFavoritesDestroy(instance.Root);
        }
    }
}