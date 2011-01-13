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
    /// Extension methods for the IFluentYammer Interface
    /// </summary>
    public static partial class IFluentYammerExtensions
    {
        /// <summary>
        /// Accesses the Messages subset of the Yammer REST API
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns>An <see cref="IYammerMessages"/> instance with methods for dealing with messages</returns>
        public static IYammerMessages Messages(this IFluentYammer instance)
        {
            instance.Parameters.Activity = "messages";
            return new YammerMessages(instance);
        }
        
        /// <summary>
        /// Accesses the Groups subset of the Yammer REST API
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns>An <see cref="IYammerMessages"/> instance with methods for dealing with groups</returns>
        public static IYammerGroups Groups(this IFluentYammer instance)
        {
            instance.Parameters.Activity = "groups";
            return new YammerGroups(instance);
        }

        /// <summary>
        /// Accesses the Tags subset of the Yammer REST API
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns>An <see cref="IYammerMessages"/> instance with methods for dealing with tags</returns>
        public static IYammerTags Tags(this IFluentYammer instance)
        {
            instance.Parameters.Activity = "tags";
            return new YammerTags(instance);
        }
        
        /// <summary>
        /// Accesses the Users subset of the Yammer REST API
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns>An <see cref="IYammerMessages"/> instance with methods for dealing with users</returns>
        public static IYammerUsers Users(this IFluentYammer instance)
        {
            instance.Parameters.Activity = "users";
            return new YammerUsers(instance);
        }

        /// <summary>
        /// Accesses the Group Memberships subset of the Yammer REST API
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns>An <see cref="IYammerMessages"/> instance with methods for dealing with group memberships</returns>
        public static IYammerGroupMemberships GroupMemberships(this IFluentYammer instance)
        {
            instance.Parameters.Activity = "group_memberships";
            return new YammerGroupMemberships(instance);
        }

        /// <summary>
        /// Accesses the Relationships subset of the Yammer REST API
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns>An <see cref="IYammerMessages"/> instance with methods for dealing with relationships</returns>
        public static IYammerRelationships Relationships(this IFluentYammer instance)
        {
            instance.Parameters.Activity = "relationships";
            return new YammerRelationships(instance);
        }

        /// <summary>
        /// Accesses the Suggestions subset of the Yammer REST API
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns>An <see cref="IYammerMessages"/> instance with methods for dealing with suggestions</returns>
        public static IYammerSuggestions Suggestions(this IFluentYammer instance)
        {
            instance.Parameters.Activity = "suggestions";
            return new YammerSuggestions(instance);
        }

        /// <summary>
        /// Accesses the Subscriptions subset of the Yammer REST API
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns>An <see cref="IYammerMessages"/> instance with methods for dealing with subscriptions</returns>
        public static IYammerSubscriptions Subscriptions(this IFluentYammer instance)
        {
            instance.Parameters.Activity = "subscriptions";
            return new YammerSubscriptions(instance);
        }

        /// <summary>
        /// Accesses the AutoComplete subset of the Yammer REST API
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns>An <see cref="IYammerMessages"/> instance with methods for dealing with auto-complete</returns>
        public static IYammerAutoComplete AutoComplete(this IFluentYammer instance)
        {
            instance.Parameters.Activity = "autocomplete";
            return new YammerAutoComplete(instance);
        }

        /// <summary>
        /// Accesses the Invitations subset of the Yammer REST API
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns>An <see cref="IYammerMessages"/> instance with methods for dealing with invitations</returns>
        public static IYammerInvitations Invitations(this IFluentYammer instance)
        {
            instance.Parameters.Activity = "invitations";
            return new YammerInvitations(instance);
        }

        /// <summary>
        /// Accesses the Search subset of the Yammer REST API
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns>An <see cref="IYammerMessages"/> instance with methods for dealing with search</returns>
        public static IYammerSearch Search(this IFluentYammer instance)
        {
            instance.Parameters.Activity = "search";
            return new YammerSearch(instance);
        }

        /// <summary>
        /// Accesses the Networks subset of the Yammer REST API
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns>An <see cref="IYammerMessages"/> instance with methods for dealing with networks</returns>
        public static IYammerNetworks Networks(this IFluentYammer instance)
        {
            instance.Parameters.Activity = "networks";
            return new YammerNetworks(instance);
        }
    }
}