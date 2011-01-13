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
    /// Extension methods for Yammer
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// Requests messages in the thread older than the message with the specified Id
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="id">the Id of the message</param>
        /// <returns></returns>
        public static IYammerMessagesInThread OlderThan(this IYammerMessagesInThread instance, int id)
        {
            instance.Root.Parameters.OlderThan = id;
            return instance;
        }

        /// <summary>
        /// Requests messages in the thread newer than the message with the specified Id
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="id">the Id of the message</param>
        /// <returns></returns>
        public static IYammerMessagesInThread NewerThan(this IYammerMessagesInThread instance, int id)
        {
            instance.Root.Parameters.NewerThan = id;
            return instance;
        }
    }
}