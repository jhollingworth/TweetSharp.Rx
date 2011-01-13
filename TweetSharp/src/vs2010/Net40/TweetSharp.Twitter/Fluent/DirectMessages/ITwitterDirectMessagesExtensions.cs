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
    /// Fluent interface methods for accessing direct message endpoints
    /// </summary>
    public static class ITwitterDirectMessagesExtensions
    {
        /// <summary>
        /// Gets direct messages receieved by the authenticated user
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public static ITwitterDirectMessagesReceived Received(this ITwitterDirectMessages instance)
        {
            return new TwitterDirectMessagesReceived(instance.Root);
        }

        /// <summary>
        /// Gets direct messages sent by the authenticated user
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public static ITwitterDirectMessagesSent Sent(this ITwitterDirectMessages instance)
        {
            instance.Root.Parameters.Action = "sent";
            return new TwitterDirectMessagesSent(instance.Root);
        }

        /// <summary>
        /// Sends a direct message from the authenticated user to another user
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="id">ID of the user to whom the direct message will be sent</param>
        /// <param name="text">the text of the message</param>
        /// <returns></returns>
        public static ITwitterDirectMessagesNew Send(this ITwitterDirectMessages instance, int id, string text)
        {
            instance.Root.Parameters.UserId = id;
            instance.Root.Parameters.Text = text;
            return new TwitterDirectMessagesNew(instance.Root);
        }

        /// <summary>
        /// Sends a direct message from the authenticated user to another user
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="id">ID of the user to whom the direct message will be sent</param>
        /// <param name="text">the text of the message</param>
        /// <returns></returns>
        
        public static ITwitterDirectMessagesNew Send(this ITwitterDirectMessages instance, long id, string text)
        {
            instance.Root.Parameters.Action = "new";
            instance.Root.Parameters.UserId = id;
            instance.Root.Parameters.Text = text;
            return new TwitterDirectMessagesNew(instance.Root);
        }

        /// <summary>
        /// Sends a direct message from the authenticated user to another user
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="screenName">screen name of the user to whom the direct message will be sent</param>
        /// <param name="text">the text of the message</param>
        /// <returns></returns>
        
        public static ITwitterDirectMessagesNew Send(this ITwitterDirectMessages instance, string screenName,
                                                     string text)
        {
            instance.Root.Parameters.Action = "new";
            instance.Root.Parameters.UserScreenName = screenName;
            instance.Root.Parameters.Text = text;
            return new TwitterDirectMessagesNew(instance.Root);
        }

        /// <summary>
        /// Deletes a direct message
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="id">Id of the message to delete</param>
        /// <returns></returns>
        
        public static ITwitterDirectMessagesDestroy Destroy(this ITwitterDirectMessages instance, int id)
        {
            instance.Root.Parameters.Action = "destroy";
            instance.Root.Parameters.Id = id;
            return new TwitterDirectMessagesDestroy(instance.Root);
        }

        /// <summary>
        /// Deletes a direct message
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="id">Id of the message to delete</param>
        /// <returns></returns>
        
        public static ITwitterDirectMessagesDestroy Destroy(this ITwitterDirectMessages instance, long id)
        {
            instance.Root.Parameters.Action = "destroy";
            instance.Root.Parameters.Id = id;
            return new TwitterDirectMessagesDestroy(instance.Root);
        }
    }
}