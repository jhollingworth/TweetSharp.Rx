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

using TweetSharp.Model;
using TweetSharp.Yammer.Model;

namespace TweetSharp.Yammer.Fluent
{
    public static partial class Extensions
    {
        /// <summary>
        /// Post a reply to the specified message
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="message">The message to reply to</param>
        /// <returns></returns>
        public static IYammerMessagePost InReplyTo(this IYammerMessagePost instance, YammerMessage message)
        {
            return InReplyTo(instance, message.Id);
        }

        /// <summary>
        /// Post the message to a specific group
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="group">The group</param>
        /// <returns></returns>
        public static IYammerMessagePost ToGroup(this IYammerMessagePost instance, YammerGroup group)
        {
            return InReplyTo(instance, group.Id);
        }

        /// <summary>
        /// Sends a message directly to a specific user
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="user">The user to whom to send the message</param>
        /// <returns></returns>
        public static IYammerMessagePost DirectToUser(this IYammerMessagePost instance, YammerUser user)
        {
            return InReplyTo(instance, user.Id);
        }

        /// <summary>
        /// Post a reply to the specified message
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="messageId">Id of the message to reply to</param>
        /// <returns></returns>
        public static IYammerMessagePost InReplyTo(this IYammerMessagePost instance, long messageId)
        {
            instance.Root.Parameters.InReplyTo = messageId;
            return instance;
        }

        /// <summary>
        /// Posts a message to a specifc group
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="groupId">Id of the group</param>
        /// <returns></returns>
        public static IYammerMessagePost ToGroup(this IYammerMessagePost instance, long groupId)
        {
            instance.Root.Parameters.ToGroupID = groupId;
            return instance;
        }

        /// <summary>
        /// Sends a message directly to a specific user
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="userId">The id of the user to whom to send the message</param>
        /// <returns></returns>
        public static IYammerMessagePost DirectToUser(this IYammerMessagePost instance, long userId)
        {
            instance.Root.Parameters.DirectToUser = userId;
            return instance;
        }

        /// <summary>
        /// Post a message with an attachment
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="attachmentPath">Path to the file to atach</param>
        /// <returns></returns>
        public static IYammerMessagePost WithAttachment(this IYammerMessagePost instance, string attachmentPath)
        {
            instance.Root.Parameters.Attachments.Add(attachmentPath);
            return instance;
        }
    }
}