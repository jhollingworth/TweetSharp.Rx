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
    /// Fluent interface methods for the spam reporting endpoints
    /// </summary>
    public static class ITwitterReportSpamExtensions
    {
        /// <summary>
        /// Reports a user as a spammer and blocks them from following the authenticating user.
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="screenName">Screen name of the spammer</param>
        /// <returns></returns>
        public static ITwitterReportSpamReportSpammer ReportSpammer(this ITwitterReportSpam instance, string screenName)
        {
            instance.Root.Parameters.UserScreenName = screenName;
            return new TwitterReportSpamReportSpammer(instance.Root);
        }

        /// <summary>
        /// Reports a user as a spammer and blocks them from following the authenticating user.
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="id">User ID of the spammer</param>
        /// <returns></returns>
        public static ITwitterReportSpamReportSpammer ReportSpammer(this ITwitterReportSpam instance, int id)
        {
            instance.Root.Parameters.UserId = id;
            return new TwitterReportSpamReportSpammer(instance.Root);
        }

        /// <summary>
        /// Reports a user as a spammer and blocks them from following the authenticating user.
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="id">User ID name of the spammer</param>
        /// <returns></returns>
        public static ITwitterReportSpamReportSpammer ReportSpammer(this ITwitterReportSpam instance, long id)
        {
            instance.Root.Parameters.UserId = id;
            return new TwitterReportSpamReportSpammer(instance.Root);
        }

        /// <summary>
        /// Reports a user as a spammer and blocks them from following the authenticating user.
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="user"><see cref="TwitterUser"/> object representing the spammer</param>
        /// <returns></returns>
        public static ITwitterReportSpamReportSpammer ReportSpammer(this ITwitterReportSpam instance, TwitterUser user)
        {
            instance.Root.Parameters.UserId = user.Id;
            return new TwitterReportSpamReportSpammer(instance.Root);
        }
    }
}