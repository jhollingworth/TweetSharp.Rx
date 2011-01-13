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
using TweetSharp.Yammer.Model;


namespace TweetSharp.Yammer.Fluent
{
    /// <summary>
    /// Extension methods for accessing the Suggestions subset of the Yammer REST API. 
    /// </summary>
    public static class YammerSuggestionsExtensions
    {
        /// <summary>
        /// Gets all pending suggestions.
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public static IYammerSuggestionsAll ShowAll(this IYammerSuggestions instance)
        {
            return new YammerSuggestionsAll(instance.Root);
        }

        /// <summary>
        /// Gets suggested <see cref="YammerUser">users</see>.
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public static IYammerSuggestionsUsers ShowUsers(this IYammerSuggestions instance)
        {
            instance.Root.Parameters.Action = "users";
            return new YammerSuggestionsUsers(instance.Root);
        }

        /// <summary>
        /// Gets suggested <see cref="YammerGroup">groups</see>.
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public static IYammerSuggestionsGroups ShowGroups(this IYammerSuggestions instance)
        {
            instance.Root.Parameters.Action = "groups";
            return new YammerSuggestionsGroups(instance.Root);
        }

        /// <summary>
        /// Declines a pending suggestion.
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="suggestionId">Id of the suggestion to decline.</param>
        /// <returns></returns>
        public static IYammerSuggestionsDecline Decline(this IYammerSuggestions instance, int suggestionId)
        {
            instance.Root.Method = WebMethod.Delete;
            instance.Root.Parameters.Action = "groups";
            instance.Root.Parameters.Id = suggestionId;
            return new YammerSuggestionsDecline(instance.Root);
        }
    }
}