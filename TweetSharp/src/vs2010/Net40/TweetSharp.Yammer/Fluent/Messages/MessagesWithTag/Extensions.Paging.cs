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
    public static partial class Extensions
    {
        /// <summary>
        /// Requests messages posted with a specific tag and that are older than a specific message
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="id">The id of the message</param>
        /// <returns></returns>
        public static IYammerMessagesWithTag OlderThan(this IYammerMessagesWithTag instance, int id)
        {
            instance.Root.Parameters.OlderThan = id;
            return instance;
        }

        /// <summary>
        /// Requests messages posted with a specific tag and that are newer than a specific message
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="id">The id of the message</param>
        /// <returns></returns>
        public static IYammerMessagesWithTag NewerThan(this IYammerMessagesWithTag instance, int id)
        {
            instance.Root.Parameters.NewerThan = id;
            return instance;
        }

        /// <summary>
        /// Requests that the results be returned in threaded format
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public static IYammerMessagesWithTag Threaded(this IYammerMessagesWithTag instance)
        {
            instance.Root.Parameters.Threaded = true;
            return instance;
        }
    }
}