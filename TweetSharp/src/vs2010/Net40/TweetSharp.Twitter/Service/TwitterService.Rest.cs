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

using System.Collections.Generic;
using System.Linq;
using TweetSharp.Model;
using TweetSharp.Twitter.Core;
using TweetSharp.Twitter.Fluent;
using TweetSharp.Twitter.Model;

namespace TweetSharp.Twitter.Service
{
    partial class TwitterService : ITwitterService
    {
#if !SILVERLIGHT
        #region Timeline Methods

        #region Public Timeline

        /// <summary>
        /// Returns the latest 20 tweets from Twitter's public timeline.
        /// This method is cached by Twitter for 60 seconds.
        /// </summary>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-statuses-public_timeline" />
        /// <returns></returns>
        public IEnumerable<TwitterStatus> ListTweetsOnPublicTimeline()
        {
            return WithTweetSharp<IEnumerable<TwitterStatus>>(q => q.Statuses().OnPublicTimeline());
        }

        #endregion

        #region Home Timeline

        /// <summary>
        /// Retrieves a list of all tweets on the authenticating user's home timeline.
        /// </summary>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-statuses-home_timeline" />
        /// <returns></returns>
        public IEnumerable<TwitterStatus> ListTweetsOnHomeTimeline()
        {
            return WithTweetSharp<IEnumerable<TwitterStatus>>(q => q.Statuses().OnHomeTimeline());
        }

        /// <summary>
        /// Retrieves a list of all tweets on the authenticating user's home timeline.
        /// </summary>
        /// <param name="count">The number of results expected.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-statuses-home_timeline"/>
        public IEnumerable<TwitterStatus> ListTweetsOnHomeTimeline(int count)
        {
            return ListTweetsOnHomeTimeline(1, count);
        }

        /// <summary>
        /// Retrieves a list of all tweets on the authenticating user's home timeline.
        /// </summary>
        /// <param name="page">The page expected.</param>
        /// <param name="count">The number of results expected.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-statuses-home_timeline"/>
        public IEnumerable<TwitterStatus> ListTweetsOnHomeTimeline(int page, int count)
        {
            return WithTweetSharp<IEnumerable<TwitterStatus>>(q => q.Statuses().OnHomeTimeline().Skip(page).Take(count));
        }

        /// <summary>
        /// Retrieves a list of all tweets on the authenticating user's home timeline.
        /// List is limited to results since a given ID.
        /// </summary>
        /// <param name="sinceId">The since ID.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-statuses-home_timeline"/>
        public IEnumerable<TwitterStatus> ListTweetsOnHomeTimelineSince(long sinceId)
        {
            return WithTweetSharp<IEnumerable<TwitterStatus>>(q => q.Statuses().OnHomeTimeline().Since(sinceId));
        }

        /// <summary>
        /// Retrieves a list of all tweets on the authenticating user's home timeline.
        /// List is limited to results since a given ID.
        /// </summary>
        /// <param name="sinceId">The since ID.</param>
        /// <param name="count">The number of results expected.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-statuses-home_timeline"/>
        public IEnumerable<TwitterStatus> ListTweetsOnHomeTimelineSince(long sinceId, int count)
        {
            return
                WithTweetSharp<IEnumerable<TwitterStatus>>(q => q.Statuses().OnHomeTimeline().Since(sinceId).Take(count));
        }

        /// <summary>
        /// Retrieves a list of all tweets on the authenticating user's home timeline.
        /// List is limited to results since a given ID.
        /// </summary>
        /// <param name="sinceId">The since ID.</param>
        /// <param name="page">The page expected.</param>
        /// <param name="count">The number of results expected.</param>
        /// <returns></returns>
        public IEnumerable<TwitterStatus> ListTweetsOnHomeTimelineSince(long sinceId, int page, int count)
        {
            return
                WithTweetSharp<IEnumerable<TwitterStatus>>(
                                                              q =>
                                                              q.Statuses().OnHomeTimeline().Since(sinceId).Skip(page).
                                                                  Take(count));
        }

        /// <summary>
        /// Retrieves a list of all tweets on the authenticating user's home timeline.
        /// List is limited to results before a given ID.
        /// </summary>
        /// <param name="maxId">The max ID.</param>
        /// <returns></returns>
        public IEnumerable<TwitterStatus> ListTweetsOnHomeTimelineBefore(long maxId)
        {
            return WithTweetSharp<IEnumerable<TwitterStatus>>(q => q.Statuses().OnHomeTimeline().Before(maxId));
        }

        /// <summary>
        /// Retrieves a list of all tweets on the authenticating user's home timeline.
        /// List is limited to results before a given ID.
        /// </summary>
        /// <param name="maxId">The max ID.</param>
        /// <param name="count">The number of results expected.</param>
        /// <returns></returns>
        public IEnumerable<TwitterStatus> ListTweetsOnHomeTimelineBefore(long maxId, int count)
        {
            return
                WithTweetSharp<IEnumerable<TwitterStatus>>(q => q.Statuses().OnHomeTimeline().Before(maxId).Take(count));
        }

        /// <summary>
        /// Retrieves a list of all tweets on the authenticating user's home timeline.
        /// List is limited to results before a given ID.
        /// </summary>
        /// <param name="maxId">The max ID.</param>
        /// <param name="page">The page expected.</param>
        /// <param name="count">The number of results expected.</param>
        /// <returns></returns>
        public IEnumerable<TwitterStatus> ListTweetsOnHomeTimelineBefore(long maxId, int page, int count)
        {
            return
                WithTweetSharp<IEnumerable<TwitterStatus>>(
                                                              q =>
                                                              q.Statuses().OnHomeTimeline().Before(maxId).Skip(page).
                                                                  Take(count));
        }

        #endregion

        #region Friends Timeline

        /// <summary>
        /// Retrieves the tweets on the authenticating user's friends timeline.
        /// </summary>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-statuses-friends_timeline"/>
        public IEnumerable<TwitterStatus> ListTweetsOnFriendsTimeline()
        {
            return WithTweetSharp<IEnumerable<TwitterStatus>>(q => q.Statuses().OnFriendsTimeline());
        }

        /// <summary>
        /// Retrieves the tweets on the authenticating user's friends timeline.
        /// </summary>
        /// <param name="count">The number of results expected.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-statuses-friends_timeline"/>
        public IEnumerable<TwitterStatus> ListTweetsOnFriendsTimeline(int count)
        {
            return ListTweetsOnFriendsTimeline(1, count);
        }

        /// <summary>
        /// Retrieves the tweets on the authenticating user's friends timeline.
        /// </summary>
        /// <param name="page">The page expected.</param>
        /// <param name="count">The number of results expected.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-statuses-friends_timeline"/>
        public IEnumerable<TwitterStatus> ListTweetsOnFriendsTimeline(int page, int count)
        {
            return
                WithTweetSharp<IEnumerable<TwitterStatus>>(q => q.Statuses().OnFriendsTimeline().Skip(page).Take(count));
        }

        /// <summary>
        /// Retrieves the tweets on the authenticating user's friends timeline.
        /// List is limited to tweets after a given ID.
        /// </summary>
        /// <param name="sinceId">The since ID.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-statuses-friends_timeline"/>
        public IEnumerable<TwitterStatus> ListTweetsOnFriendsTimelineSince(long sinceId)
        {
            return WithTweetSharp<IEnumerable<TwitterStatus>>(
                q => q.Statuses().OnFriendsTimeline().Since(sinceId)
                );
        }

        /// <summary>
        /// Retrieves the tweets on the authenticating user's friends timeline.
        /// List is limited to tweets after a given ID.
        /// </summary>
        /// <param name="sinceId">The since ID.</param>
        /// <param name="count">The number of results expected.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-statuses-friends_timeline"/>
        public IEnumerable<TwitterStatus> ListTweetsOnFriendsTimelineSince(long sinceId, int count)
        {
            return
                WithTweetSharp<IEnumerable<TwitterStatus>>(
                    q => q.Statuses().OnFriendsTimeline().Since(sinceId).Take(count)
                    );
        }

        /// <summary>
        /// Retrieves the tweets on the authenticating user's friends timeline.
        /// List is limited to tweets after a given ID.
        /// </summary>
        /// <param name="sinceId">The since ID.</param>
        /// <param name="page">The page expected.</param>
        /// <param name="count">The number of results expected.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-statuses-friends_timeline"/>
        public IEnumerable<TwitterStatus> ListTweetsOnFriendsTimelineSince(long sinceId, int page, int count)
        {
            return
                WithTweetSharp<IEnumerable<TwitterStatus>>(
                                                              q =>
                                                              q.Statuses().OnFriendsTimeline().Since(sinceId).Skip(page)
                                                                  .Take(count));
        }

        /// <summary>
        /// Retrieves the tweets on the authenticating user's friends timeline.
        /// List is limited to tweets before a given ID.
        /// </summary>
        /// <param name="maxId">The max ID.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-statuses-friends_timeline"/>
        public IEnumerable<TwitterStatus> ListTweetsOnFriendsTimelineBefore(long maxId)
        {
            return WithTweetSharp<IEnumerable<TwitterStatus>>(q => q.Statuses().OnFriendsTimeline().Before(maxId));
        }

        /// <summary>
        /// Retrieves the tweets on the authenticating user's friends timeline.
        /// List is limited to tweets before a given ID.
        /// </summary>
        /// <param name="maxId">The max ID.</param>
        /// <param name="count">The number of results expected.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-statuses-friends_timeline"/>
        public IEnumerable<TwitterStatus> ListTweetsOnFriendsTimelineBefore(long maxId, int count)
        {
            return
                WithTweetSharp<IEnumerable<TwitterStatus>>(
                                                              q =>
                                                              q.Statuses().OnFriendsTimeline().Before(maxId).Take(count));
        }

        /// <summary>
        /// Retrieves the tweets on the authenticating user's friends timeline.
        /// List is limited to tweets before a given ID.
        /// </summary>
        /// <param name="maxId">The max ID.</param>
        /// <param name="page">The page expected.</param>
        /// <param name="count">The number of results expected.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-statuses-friends_timeline"/>
        public IEnumerable<TwitterStatus> ListTweetsOnFriendsTimelineBefore(long maxId, int page, int count)
        {
            return
                WithTweetSharp<IEnumerable<TwitterStatus>>(
                    q => q.Statuses().OnFriendsTimeline().Before(maxId).Skip(page).Take(count)
                    );
        }

        #endregion

        #region User Timeline

        /// <summary>
        /// Retrieves tweets on the authenticated user's timeline.
        /// </summary>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-statuses-user_timeline"/>
        public IEnumerable<TwitterStatus> ListTweetsOnUserTimeline()
        {
            return WithTweetSharp<IEnumerable<TwitterStatus>>(q => q.Statuses().OnUserTimeline());
        }

        /// <summary>
        /// Retrieves tweets on the authenticated user's timeline.
        /// </summary>
        /// <param name="count">The number of results expected.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-statuses-user_timeline"/>
        public IEnumerable<TwitterStatus> ListTweetsOnUserTimeline(int count)
        {
            return ListTweetsOnUserTimeline(1, count);
        }

        /// <summary>
        /// Retrieves tweets on the authenticated user's timeline.
        /// </summary>
        /// <param name="page">The page expected.</param>
        /// <param name="count">The number of results expected.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-statuses-user_timeline"/>
        public IEnumerable<TwitterStatus> ListTweetsOnUserTimeline(int page, int count)
        {
            return WithTweetSharp<IEnumerable<TwitterStatus>>(q => q.Statuses().OnUserTimeline().Skip(page).Take(count));
        }

        /// <summary>
        /// Retrieves tweets on the authenticated user's timeline.
        /// List is limited to tweets after a given ID.
        /// </summary>
        /// <param name="sinceId">The since ID.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-statuses-user_timeline"/>
        public IEnumerable<TwitterStatus> ListTweetsOnUserTimelineSince(long sinceId)
        {
            return WithTweetSharp<IEnumerable<TwitterStatus>>(q => q.Statuses().OnUserTimeline().Since(sinceId));
        }

        /// <summary>
        /// Retrieves tweets on the authenticated user's timeline.
        /// List is limited to tweets after a given ID.
        /// </summary>
        /// <param name="sinceId">The since ID.</param>
        /// <param name="count">The number of results expected.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-statuses-user_timeline"/>
        public IEnumerable<TwitterStatus> ListTweetsOnUserTimelineSince(long sinceId, int count)
        {
            return WithTweetSharp<IEnumerable<TwitterStatus>>(
                q => q.Statuses().OnUserTimeline().Since(sinceId).Take(count)
                );
        }

        /// <summary>
        /// Retrieves tweets on the authenticated user's timeline.
        /// List is limited to tweets after a given ID.
        /// </summary>
        /// <param name="sinceId">The since ID.</param>
        /// <param name="page">The page expected.</param>
        /// <param name="count">The number of results expected.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-statuses-user_timeline"/>
        public IEnumerable<TwitterStatus> ListTweetsOnUserTimelineSince(long sinceId, int page, int count)
        {
            return WithTweetSharp<IEnumerable<TwitterStatus>>(
                q =>q.Statuses().OnUserTimeline().Since(sinceId).Skip(page).Take(count)
                );
        }

        /// <summary>
        /// Retrieves tweets on the authenticated user's timeline.
        /// List is limited to tweets before a given ID.
        /// </summary>
        /// <param name="maxId">The max ID.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-statuses-user_timeline"/>
        public IEnumerable<TwitterStatus> ListTweetsOnUserTimelineBefore(long maxId)
        {
            return WithTweetSharp<IEnumerable<TwitterStatus>>(q => q.Statuses().OnUserTimeline().Before(maxId));
        }

        /// <summary>
        /// Retrieves tweets on the authenticated user's timeline.
        /// List is limited to tweets before a given ID.
        /// </summary>
        /// <param name="maxId">The max ID.</param>
        /// <param name="count">The number of results expected.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-statuses-user_timeline"/>
        public IEnumerable<TwitterStatus> ListTweetsOnUserTimelineBefore(long maxId, int count)
        {
            return WithTweetSharp<IEnumerable<TwitterStatus>>(
                q => q.Statuses().OnUserTimeline().Before(maxId).Take(count)
                );
        }

        /// <summary>
        /// Retrieves tweets on the authenticated user's timeline.
        /// List is limited to tweets before a given ID.
        /// </summary>
        /// <param name="maxId">The max ID.</param>
        /// <param name="page">The page expected.</param>
        /// <param name="count">The number of results expected.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-statuses-user_timeline"/>
        public IEnumerable<TwitterStatus> ListTweetsOnUserTimelineBefore(long maxId, int page, int count)
        {
            return WithTweetSharp<IEnumerable<TwitterStatus>>(
                q => q.Statuses().OnUserTimeline().Before(maxId).Skip(page).Take(count)
                );
        }

        /// <summary>
        /// Retrieves tweets on the specified user's timeline.
        /// </summary>
        /// <param name="userId">The user ID.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-statuses-user_timeline"/>
        public IEnumerable<TwitterStatus> ListTweetsOnSpecifiedUserTimeline(int userId)
        {
            return WithTweetSharp<IEnumerable<TwitterStatus>>(q => q.Statuses().OnUserTimeline().For(userId));
        }

        /// <summary>
        /// Retrieves tweets on the specified user's timeline.
        /// </summary>
        /// <param name="userId">The user ID.</param>
        /// <param name="count">The number of results expected.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-statuses-user_timeline"/>
        public IEnumerable<TwitterStatus> ListTweetsOnSpecifiedUserTimeline(int userId, int count)
        {
            return ListTweetsOnSpecifiedUserTimeline(1, count);
        }

        /// <summary>
        /// Retrieves tweets on the specified user's timeline.
        /// </summary>
        /// <param name="userId">The user ID.</param>
        /// <param name="page">The page expected.</param>
        /// <param name="count">The number of results expected.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-statuses-user_timeline"/>
        public IEnumerable<TwitterStatus> ListTweetsOnSpecifiedUserTimeline(int userId, int page, int count)
        {
            return WithTweetSharp<IEnumerable<TwitterStatus>>(
                q => q.Statuses().OnUserTimeline().For(userId).Skip(page).Take(count)
                );
        }

        /// <summary>
        /// Retrieves tweets on the specified user's timeline.
        /// List is limited to tweets after a given ID.
        /// </summary>
        /// <param name="userId">The user ID.</param>
        /// <param name="sinceId">The since ID.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-statuses-user_timeline"/>
        public IEnumerable<TwitterStatus> ListTweetsOnSpecifiedUserTimelineSince(int userId, long sinceId)
        {
            return WithTweetSharp<IEnumerable<TwitterStatus>>(
                q => q.Statuses().OnUserTimeline().For(userId).Since(sinceId)
                );
        }

        /// <summary>
        /// Retrieves tweets on the specified user's timeline.
        /// List is limited to tweets after a given ID.
        /// </summary>
        /// <param name="userId">The user ID.</param>
        /// <param name="sinceId">The since ID.</param>
        /// <param name="count">The number of results expected.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-statuses-user_timeline"/>
        public IEnumerable<TwitterStatus> ListTweetsOnSpecifiedUserTimelineSince(int userId, long sinceId, int count)
        {
            return WithTweetSharp<IEnumerable<TwitterStatus>>(
                q => q.Statuses().OnUserTimeline().For(userId).Since(sinceId).Take(count)
                );
        }

        /// <summary>
        /// Retrieves tweets on the specified user's timeline.
        /// List is limited to tweets after a given ID.
        /// </summary>
        /// <param name="userId">The user ID.</param>
        /// <param name="sinceId">The since ID.</param>
        /// <param name="page">The page expected.</param>
        /// <param name="count">The number of results expected.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-statuses-user_timeline"/>
        public IEnumerable<TwitterStatus> ListTweetsOnSpecifiedUserTimelineSince(int userId, long sinceId, int page,
                                                                                 int count)
        {
            return WithTweetSharp<IEnumerable<TwitterStatus>>(
                q =>q.Statuses().OnUserTimeline().For(userId).Since(sinceId).Skip(page).Take(count)
                );
        }

        /// <summary>
        /// Retrieves tweets on the specified user's timeline.
        /// List is limited to tweets before a given ID.
        /// </summary>
        /// <param name="userId">The user ID.</param>
        /// <param name="maxId">The max ID.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-statuses-user_timeline"/>
        public IEnumerable<TwitterStatus> ListTweetsOnSpecifiedUserTimelineBefore(int userId, long maxId)
        {
            return WithTweetSharp<IEnumerable<TwitterStatus>>(
                q => q.Statuses().OnUserTimeline().For(userId).Before(maxId)
                );
        }

        /// <summary>
        /// Retrieves tweets on the specified user's timeline.
        /// List is limited to tweets before a given ID.
        /// </summary>
        /// <param name="userId">The user ID.</param>
        /// <param name="maxId">The max ID.</param>
        /// <param name="count">The number of results expected.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-statuses-user_timeline"/>
        public IEnumerable<TwitterStatus> ListTweetsOnSpecifiedUserTimelineBefore(int userId, long maxId, int count)
        {
            return WithTweetSharp<IEnumerable<TwitterStatus>>(
                q => q.Statuses().OnUserTimeline().For(userId).Before(maxId).Take(count)
                );
        }

        /// <summary>
        /// Retrieves tweets on the specified user's timeline.
        /// List is limited to tweets before a given ID.
        /// </summary>
        /// <param name="userId">The user ID.</param>
        /// <param name="maxId">The max ID.</param>
        /// <param name="page">The page expected.</param>
        /// <param name="count">The number of results expected.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-statuses-user_timeline"/>
        public IEnumerable<TwitterStatus> ListTweetsOnSpecifiedUserTimelineBefore(int userId, long maxId, int page, int count)
        {
            return WithTweetSharp<IEnumerable<TwitterStatus>>(
                q => q.Statuses().OnUserTimeline().For(userId).Before(maxId).Skip(page).Take(count)
                );
        }

        /// <summary>
        /// Retrieves tweets on the specified user's timeline.
        /// </summary>
        /// <param name="userScreenName">The user's screen name.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-statuses-user_timeline"/>
        public IEnumerable<TwitterStatus> ListTweetsOnSpecifiedUserTimeline(string userScreenName)
        {
            return WithTweetSharp<IEnumerable<TwitterStatus>>(q => q.Statuses().OnUserTimeline().For(userScreenName));
        }

        /// <summary>
        /// Retrieves tweets on the specified user's timeline.
        /// </summary>
        /// <param name="userScreenName">The user's screen name.</param>
        /// <param name="count">The number of results expected.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-statuses-user_timeline"/>
        public IEnumerable<TwitterStatus> ListTweetsOnSpecifiedUserTimeline(string userScreenName, int count)
        {
            return ListTweetsOnSpecifiedUserTimeline(userScreenName, 1, count);
        }

        /// <summary>
        /// Retrieves tweets on the specified user's timeline.
        /// </summary>
        /// <param name="userScreenName">The user's screen name.</param>
        /// <param name="page">The page expected.</param>
        /// <param name="count">The number of results expected.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-statuses-user_timeline"/>
        public IEnumerable<TwitterStatus> ListTweetsOnSpecifiedUserTimeline(string userScreenName, int page, int count)
        {
            return WithTweetSharp<IEnumerable<TwitterStatus>>(
                q => q.Statuses().OnUserTimeline().For(userScreenName).Skip(page).Take(count)
                );
        }

        /// <summary>
        /// Retrieves tweets on the specified user's timeline.
        /// List is limited to tweets after a given ID
        /// </summary>
        /// <param name="userScreenName">The user's screen name.</param>
        /// <param name="sinceId">The since ID.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-statuses-user_timeline"/>
        public IEnumerable<TwitterStatus> ListTweetsOnSpecifiedUserTimelineSince(string userScreenName, long sinceId)
        {
            return WithTweetSharp<IEnumerable<TwitterStatus>>(
                q => q.Statuses().OnUserTimeline().For(userScreenName).Since(sinceId)
                );
        }

        /// <summary>
        /// Retrieves tweets on the specified user's timeline.
        /// List is limited to tweets after a given ID
        /// </summary>
        /// <param name="userScreenName">The user's screen name.</param>
        /// <param name="sinceId">The since ID.</param>
        /// <param name="count">The number of results expected.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-statuses-user_timeline"/>
        public IEnumerable<TwitterStatus> ListTweetsOnSpecifiedUserTimelineSince(string userScreenName, long sinceId, int count)
        {
            return WithTweetSharp<IEnumerable<TwitterStatus>>(
                q => q.Statuses().OnUserTimeline().For(userScreenName).Since(sinceId).Take(count)
                );
        }

        /// <summary>
        /// Retrieves tweets on the specified user's timeline.
        /// List is limited to tweets after a given ID
        /// </summary>
        /// <param name="userScreenName">The user's screen name.</param>
        /// <param name="sinceId">The since ID.</param>
        /// <param name="page">The page expected.</param>
        /// <param name="count">The number of results expected.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-statuses-user_timeline"/>
        public IEnumerable<TwitterStatus> ListTweetsOnSpecifiedUserTimelineSince(string userScreenName, long sinceId, int page, int count)
        {
            return WithTweetSharp<IEnumerable<TwitterStatus>>(
                q => q.Statuses().OnUserTimeline().For(userScreenName).Since(sinceId).Skip(page).Take(count)
                );
        }

        /// <summary>
        /// Retrieves tweets on the specified user's timeline.
        /// List is limited to tweets before a given ID
        /// </summary>
        /// <param name="userScreenName">The user's screen name.</param>
        /// <param name="maxId">The max ID.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-statuses-user_timeline"/>
        public IEnumerable<TwitterStatus> ListTweetsOnSpecifiedUserTimelineBefore(string userScreenName, long maxId)
        {
            return WithTweetSharp<IEnumerable<TwitterStatus>>(
                q => q.Statuses().OnUserTimeline().For(userScreenName).Before(maxId)
                );
        }

        /// <summary>
        /// Retrieves tweets on the specified user's timeline.
        /// List is limited to tweets before a given ID
        /// </summary>
        /// <param name="userScreenName">The user's screen name.</param>
        /// <param name="maxId">The max ID.</param>
        /// <param name="count">The number of results expected.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-statuses-user_timeline"/>
        public IEnumerable<TwitterStatus> ListTweetsOnSpecifiedUserTimelineBefore(string userScreenName, long maxId,
                                                                                  int count)
        {
            return WithTweetSharp<IEnumerable<TwitterStatus>>(
                q => q.Statuses().OnUserTimeline().For(userScreenName).Before(maxId).Take(count)
                );
        }

        /// <summary>
        /// Retrieves tweets on the specified user's timeline.
        /// List is limited to tweets before a given ID
        /// </summary>
        /// <param name="userScreenName">The user's screen name.</param>
        /// <param name="maxId">The max ID.</param>
        /// <param name="page">The page expected.</param>
        /// <param name="count">The number of results expected.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-statuses-user_timeline"/>
        public IEnumerable<TwitterStatus> ListTweetsOnSpecifiedUserTimelineBefore(string userScreenName, long maxId,
                                                                                  int page, int count)
        {
            return WithTweetSharp<IEnumerable<TwitterStatus>>(
                q =>q.Statuses().OnUserTimeline().For(userScreenName).Before(maxId).Skip(page).Take(count)
                );
        }

        #endregion

        #region Mentions

        /// <summary>
        /// Lists the first 20 tweets mentioning the authenticated user.
        /// </summary>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-statuses-mentions"/>
        public IEnumerable<TwitterStatus> ListTweetsMentioningMe()
        {
            return WithTweetSharp<IEnumerable<TwitterStatus>>(q => q.Statuses().Mentions());
        }

        /// <summary>
        /// Lists the first page of tweets mentioning the authenticated user.
        /// Each page has 20 tweets.
        /// </summary>
        /// <param name="count">The number of tweets to return, up to 200.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-statuses-mentions"/>
        public IEnumerable<TwitterStatus> ListTweetsMentioningMe(int count)
        {
            return ListTweetsMentioningMe(1, count);
        }

        /// <summary>
        /// Lists the specified page of tweets mentioning the authenticated user.
        /// Each page has the specified number of tweets, up to 200.
        /// </summary>
        /// <param name="page">The page of tweets to return, relative to the tweet count.</param>
        /// <param name="count">The number of tweets to return, up to 200.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-statuses-mentions"/>
        public IEnumerable<TwitterStatus> ListTweetsMentioningMe(int page, int count)
        {
            return WithTweetSharp<IEnumerable<TwitterStatus>>(q => q.Statuses().Mentions().Skip(page).Take(count));
        }

        /// <summary>
        /// Retrieves the tweets mentioning the authenticating user.
        /// List is limited to mentions after a given ID.
        /// </summary>
        /// <param name="sinceId">The since ID.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-statuses-mentions"/>
        public IEnumerable<TwitterStatus> ListTweetsMentioningMeSince(long sinceId)
        {
            return WithTweetSharp<IEnumerable<TwitterStatus>>(q => q.Statuses().Mentions().Since(sinceId));
        }

        /// <summary>
        /// Retrieves the tweets mentioning the authenticating user.
        /// List is limited to mentions after a given ID.
        /// </summary>
        /// <param name="sinceId">The since ID.</param>
        /// <param name="count">The number of results expected.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-statuses-mentions"/>
        public IEnumerable<TwitterStatus> ListTweetsMentioningMeSince(long sinceId, int count)
        {
            return WithTweetSharp<IEnumerable<TwitterStatus>>(q => q.Statuses().Mentions().Since(sinceId).Take(count));
        }

        /// <summary>
        /// Retrieves the tweets mentioning the authenticating user.
        /// List is limited to mentions after a given ID.
        /// </summary>
        /// <param name="sinceId">The since ID.</param>
        /// <param name="page">The page expected.</param>
        /// <param name="count">The number of results expected.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-statuses-mentions"/>
        public IEnumerable<TwitterStatus> ListTweetsMentioningMeSince(long sinceId, int page, int count)
        {
            return
                WithTweetSharp<IEnumerable<TwitterStatus>>(
                                                              q =>
                                                              q.Statuses().Mentions().Since(sinceId).Skip(page).Take(
                                                                                                                        count));
        }

        /// <summary>
        /// Retrieves the tweets mentioning the authenticating user.
        /// List is limited to mentions before a given ID.
        /// </summary>
        /// <param name="maxId">The max ID.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-statuses-mentions"/>
        public IEnumerable<TwitterStatus> ListTweetsMentioningMeBefore(long maxId)
        {
            return WithTweetSharp<IEnumerable<TwitterStatus>>(q => q.Statuses().Mentions().Before(maxId));
        }

        /// <summary>
        /// Retrieves the tweets mentioning the authenticating user.
        /// List is limited to mentions before a given ID.
        /// </summary>
        /// <param name="maxId">The max ID.</param>
        /// <param name="count">The number of results expected.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-statuses-mentions"/>
        public IEnumerable<TwitterStatus> ListTweetsMentioningMeBefore(long maxId, int count)
        {
            return WithTweetSharp<IEnumerable<TwitterStatus>>(q => q.Statuses().Mentions().Before(maxId).Take(count));
        }

        /// <summary>
        /// Retrieves the tweets mentioning the authenticating user.
        /// List is limited to mentions before a given ID.
        /// </summary>
        /// <param name="maxId">The max ID.</param>
        /// <param name="page">The page expected.</param>
        /// <param name="count">The number of results expected.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-statuses-mentions"/>
        public IEnumerable<TwitterStatus> ListTweetsMentioningMeBefore(long maxId, int page, int count)
        {
            return
                WithTweetSharp<IEnumerable<TwitterStatus>>(
                    q => q.Statuses().Mentions().Before(maxId).Skip(page).Take(count)
                    );
        }

        #endregion

        #region Retweeted by Me

        /// <summary>
        /// Retrieves all retweeted tweets by the authenticating user.
        /// </summary>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-statuses-retweeted_by_me" />
        /// <returns></returns>
        public IEnumerable<TwitterStatus> ListRetweetsByMe()
        {
            return WithTweetSharp<IEnumerable<TwitterStatus>>(q => q.Statuses().RetweetedByMe());
        }

        /// <summary>
        /// Retrieves all retweeted tweets by the authenticating user.
        /// </summary>
        /// <param name="count">The number of results expected.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-statuses-retweeted_by_me"/>
        public IEnumerable<TwitterStatus> ListRetweetsByMe(int count)
        {
            return ListRetweetsByMe(1, count);
        }

        /// <summary>
        /// Retrieves all retweeted tweets by the authenticating user.
        /// </summary>
        /// <param name="page">The page expected.</param>
        /// <param name="count">The number of results expected.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-statuses-retweeted_by_me"/>
        public IEnumerable<TwitterStatus> ListRetweetsByMe(int page, int count)
        {
            return WithTweetSharp<IEnumerable<TwitterStatus>>(q => q.Statuses().RetweetedByMe().Skip(page).Take(count));
        }

        /// <summary>
        /// Retrieves all retweeted tweets by the authenticating user.
        /// List is limited to results after a given ID.
        /// </summary>
        /// <param name="sinceId">The since ID.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-statuses-retweeted_by_me"/>
        public IEnumerable<TwitterStatus> ListRetweetsByMe(long sinceId)
        {
            return WithTweetSharp<IEnumerable<TwitterStatus>>(q => q.Statuses().RetweetedByMe().Since(sinceId));
        }

        /// <summary>
        /// Retrieves all retweeted tweets by the authenticating user.
        /// List is limited to results after a given ID.
        /// </summary>
        /// <param name="sinceId">The since ID.</param>
        /// <param name="count">The number of results expected.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-statuses-retweeted_by_me"/>
        public IEnumerable<TwitterStatus> ListRetweetsByMeSince(long sinceId, int count)
        {
            return WithTweetSharp<IEnumerable<TwitterStatus>>(q => q.Statuses().RetweetedByMe().Since(sinceId).Take(count));
        }

        /// <summary>
        /// Retrieves all retweeted tweets by the authenticating user.
        /// List is limited to results after a given ID.
        /// </summary>
        /// <param name="sinceId">The since ID.</param>
        /// <param name="page">The page expected.</param>
        /// <param name="count">The number of results expected.</param>
        /// <returns></returns>
        public IEnumerable<TwitterStatus> ListRetweetsByMeSince(long sinceId, int page, int count)
        {
            return WithTweetSharp<IEnumerable<TwitterStatus>>(q =>
                                                              q.Statuses().RetweetedByMe().Since(sinceId).Skip(page).
                                                                  Take(count));
        }

        /// <summary>
        /// Retrieves all retweeted tweets by the authenticating user.
        /// List is limited to results before a given ID.
        /// </summary>
        /// <param name="maxId">The max ID.</param>
        /// <returns></returns>
        public IEnumerable<TwitterStatus> ListRetweetsByMeBefore(long maxId)
        {
            return WithTweetSharp<IEnumerable<TwitterStatus>>(q => q.Statuses().RetweetedByMe().Before(maxId));
        }

        /// <summary>
        /// Retrieves all retweeted tweets by the authenticating user.
        /// List is limited to results before a given ID.
        /// </summary>
        /// <param name="maxId">The max ID.</param>
        /// <param name="count">The number of results expected.</param>
        /// <returns></returns>
        public IEnumerable<TwitterStatus> ListRetweetsByMeBefore(long maxId, int count)
        {
            return WithTweetSharp<IEnumerable<TwitterStatus>>(
                q => q.Statuses().RetweetedByMe().Before(maxId).Take(count)
                );
        }

        /// <summary>
        /// Retrieves all retweeted tweets by the authenticating user.
        /// List is limited to results before a given ID.
        /// </summary>
        /// <param name="maxId">The max ID.</param>
        /// <param name="page">The page expected.</param>
        /// <param name="count">The number of results expected.</param>
        /// <returns></returns>
        public IEnumerable<TwitterStatus> ListRetweetsByMeBefore(long maxId, int page, int count)
        {
            return
                WithTweetSharp<IEnumerable<TwitterStatus>>(
                                                              q =>
                                                              q.Statuses().RetweetedByMe().Before(maxId).Skip(page).Take
                                                                  (count));
        }

        #endregion

        #region Retweeted To Me

        /// <summary>
        /// Retrieves all retweets visible to the authenticating user.
        /// </summary>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-statuses-retweeted_to_me"/>
        public IEnumerable<TwitterStatus> ListRetweetsToMe()
        {
            return WithTweetSharp<IEnumerable<TwitterStatus>>(q => q.Statuses().RetweetedToMe());
        }


        /// <summary>
        /// Retrieves all retweets visible to the authenticating user.
        /// </summary>
        /// <param name="count">The number of results expected.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-statuses-retweeted_to_me"/>
        public IEnumerable<TwitterStatus> ListRetweetsToMe(int count)
        {
            return ListRetweetsToMe(1, count);
        }

        /// <summary>
        /// Retrieves all retweets visible to the authenticating user.
        /// </summary>
        /// <param name="page">The page expected.</param>
        /// <param name="count">The number of results expected.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-statuses-retweeted_to_me"/>
        public IEnumerable<TwitterStatus> ListRetweetsToMe(int page, int count)
        {
            return WithTweetSharp<IEnumerable<TwitterStatus>>(
                q => q.Statuses().RetweetedToMe().Skip(page).Take(count)
                );
        }
        
        /// <summary>
        /// Retrieves all retweets visible to the authenticating user.
        /// List is limited to tweets after a given ID.
        /// </summary>
        /// <param name="sinceId">The since ID.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-statuses-retweeted_to_me"/>
        public IEnumerable<TwitterStatus> ListRetweetsToMeSince(long sinceId)
        {
            return WithTweetSharp<IEnumerable<TwitterStatus>>(
                q => q.Statuses().RetweetedToMe().Since(sinceId)
                );
        }

        /// <summary>
        /// Retrieves all retweets visible to the authenticating user.
        /// List is limited to tweets after a given ID.
        /// </summary>
        /// <param name="sinceId">The since ID.</param>
        /// <param name="count">The number of results expected.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-statuses-retweeted_to_me"/>
        public IEnumerable<TwitterStatus> ListRetweetsToMeSince(long sinceId, int count)
        {
            return WithTweetSharp<IEnumerable<TwitterStatus>>(
                q => q.Statuses().RetweetedToMe().Since(sinceId).Take(count)
                );
        }

        /// <summary>
        /// Retrieves all retweets visible to the authenticating user.
        /// List is limited to tweets after a given ID.
        /// </summary>
        /// <param name="sinceId">The since ID.</param>
        /// <param name="page">The page expected.</param>
        /// <param name="count">The number of results expected.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-statuses-retweeted_to_me"/>
        public IEnumerable<TwitterStatus> ListRetweetsToMeSince(long sinceId, int page, int count)
        {
            return
                WithTweetSharp<IEnumerable<TwitterStatus>>(
                    q => q.Statuses().RetweetedToMe().Since(sinceId).Skip(page).Take(count)
                    );
        }

        /// <summary>
        /// Retrieves all retweets visible to the authenticating user.
        /// List is limited to tweets before a given ID.
        /// </summary>
        /// <param name="maxId">The max ID.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-statuses-retweeted_to_me"/>
        public IEnumerable<TwitterStatus> ListRetweetsToMeBefore(long maxId)
        {
            return WithTweetSharp<IEnumerable<TwitterStatus>>(q => q.Statuses().RetweetedToMe().Before(maxId));
        }

        /// <summary>
        /// Retrieves all retweets visible to the authenticating user.
        /// List is limited to tweets before a given ID.
        /// </summary>
        /// <param name="maxId">The max ID.</param>
        /// <param name="count">The number of results expected.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-statuses-retweeted_to_me"/>
        public IEnumerable<TwitterStatus> ListRetweetsToMeBefore(long maxId, int count)
        {
            return WithTweetSharp<IEnumerable<TwitterStatus>>(
                q => q.Statuses().RetweetedToMe().Before(maxId).Take(count)
                );
        }

        /// <summary>
        /// Retrieves all retweets visible to the authenticating user.
        /// List is limited to tweets before a given ID.
        /// </summary>
        /// <param name="maxId">The max ID.</param>
        /// <param name="page">The page expected.</param>
        /// <param name="count">The number of results expected.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-statuses-retweeted_to_me"/>
        public IEnumerable<TwitterStatus> ListRetweetsToMeBefore(long maxId, int page, int count)
        {
            return WithTweetSharp<IEnumerable<TwitterStatus>>(
                q => q.Statuses().RetweetedToMe().Before(maxId).Skip(page).Take(count)
                );
        }

        #endregion

        #region Retweets Of Me

        /// <summary>
        /// Retrieves tweets retweeted by the authenticating user.
        /// </summary>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-statuses-retweets_of_me"/>
        public IEnumerable<TwitterStatus> ListRetweetsOfMyTweets()
        {
            return WithTweetSharp<IEnumerable<TwitterStatus>>(q => q.Statuses().RetweetsOfMe());
        }

        /// <summary>
        /// Retrieves tweets retweeted by the authenticating user.
        /// </summary>
        /// <param name="count">The number of results expected.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-statuses-retweets_of_me"/>
        public IEnumerable<TwitterStatus> ListRetweetsOfMyTweets(int count)
        {
            return ListRetweetsOfMyTweets(1, count);
        }

        /// <summary>
        /// Retrieves tweets retweeted by the authenticating user.
        /// </summary>
        /// <param name="page">The page expected.</param>
        /// <param name="count">The number of results expected.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-statuses-retweets_of_me"/>
        public IEnumerable<TwitterStatus> ListRetweetsOfMyTweets(int page, int count)
        {
            return WithTweetSharp<IEnumerable<TwitterStatus>>(q => q.Statuses().RetweetsOfMe().Skip(page).Take(count));
        }

        /// <summary>
        /// Retrieves tweets retweeted by the authenticating user.
        /// List is limited to tweets after a given ID.
        /// </summary>
        /// <param name="sinceId">The since ID.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-statuses-retweets_of_me"/>
        public IEnumerable<TwitterStatus> ListRetweetsOfMyTweets(long sinceId)
        {
            return WithTweetSharp<IEnumerable<TwitterStatus>>(q => q.Statuses().RetweetsOfMe().Since(sinceId));
        }

        /// <summary>
        /// Retrieves tweets retweeted by the authenticating user.
        /// List is limited to tweets after a given ID.
        /// </summary>
        /// <param name="sinceId">The since ID.</param>
        /// <param name="count">The number of results expected.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-statuses-retweets_of_me"/>
        public IEnumerable<TwitterStatus> ListRetweetsOfMyTweetsSince(long sinceId, int count)
        {
            return WithTweetSharp<IEnumerable<TwitterStatus>>(
                q => q.Statuses().RetweetsOfMe().Since(sinceId).Take(count)
                );
        }

        /// <summary>
        /// Retrieves tweets retweeted by the authenticating user.
        /// List is limited to tweets before a given ID.
        /// </summary>
        /// <param name="sinceId">The since ID.</param>
        /// <param name="page">The page expected.</param>
        /// <param name="count">The number of results expected.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-statuses-retweets_of_me"/>
        public IEnumerable<TwitterStatus> ListRetweetsOfMyTweetsSince(long sinceId, int page, int count)
        {
            return
                WithTweetSharp<IEnumerable<TwitterStatus>>(
                    q => q.Statuses().RetweetsOfMe().Since(sinceId).Skip(page).Take(count)
                    );
        }

        /// <summary>
        /// Retrieves tweets retweeted by the authenticating user.
        /// List is limited to tweets before a given ID.
        /// </summary>
        /// <param name="maxId">The max ID.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-statuses-retweets_of_me"/>
        public IEnumerable<TwitterStatus> ListRetweetsOfMyTweetsSince(long maxId)
        {
            return WithTweetSharp<IEnumerable<TwitterStatus>>(q => q.Statuses().RetweetsOfMe().Before(maxId));
        }

        /// <summary>
        /// Retrieves tweets retweeted by the authenticating user.
        /// List is limited to tweets before a given ID.
        /// </summary>
        /// <param name="maxId">The max ID.</param>
        /// <param name="count">The number of results expected.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-statuses-retweets_of_me"/>
        public IEnumerable<TwitterStatus> ListRetweetsOfMyTweetsBefore(long maxId, int count)
        {
            return WithTweetSharp<IEnumerable<TwitterStatus>>(q => q.Statuses().RetweetsOfMe().Before(maxId).Take(count));
        }

        /// <summary>
        /// Retrieves tweets retweeted by the authenticating user.
        /// List is limited to tweets before a given ID.
        /// </summary>
        /// <param name="maxId">The max ID.</param>
        /// <param name="page">The page expected.</param>
        /// <param name="count">The number of results expected.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-statuses-retweets_of_me"/>
        public IEnumerable<TwitterStatus> ListRetweetsOfMyTweetsBefore(long maxId, int page, int count)
        {
            return WithTweetSharp<IEnumerable<TwitterStatus>>(
                q => q.Statuses().RetweetsOfMe().Before(maxId).Skip(page).Take(count)
                );
        }

        #endregion

        #endregion

        #region Status Methods

        #region Show

        /// <summary>
        /// Gets the tweet with the specified ID.
        /// </summary>
        /// <param name="id">The tweet ID.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-statuses%C2%A0show"></seealso>
        public TwitterStatus GetTweet(long id)
        {
            return WithTweetSharp<TwitterStatus>(q => q.Statuses().Show(id));
        }

        /// <summary>
        /// Gets the tweet with the specified ID.
        /// </summary>
        /// <param name="id">The tweet ID.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-statuses%C2%A0show"></seealso>
        public TwitterStatus GetTweet(int id)
        {
            return WithTweetSharp<TwitterStatus>(q => q.Statuses().Show(id));
        }

        #endregion

        #region Update

        /// <summary>
        /// Tweets the specified text from the authenticated user.
        /// A tweet with text identical to the authenticating user's 
        /// current status will be ignored.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns></returns>
        public TwitterStatus SendTweet(string text)
        {
            return WithTweetSharp<TwitterStatus>(q => q.Statuses().Update(text));
        }

        /// <summary>
        /// Tweets the specified text from the authenticated user.
        /// Includes provided geo-tagging data.
        /// A tweet with text identical to the authenticating user's 
        /// current status will be ignored.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="latitude">The latitude.</param>
        /// <param name="longitude">The longitude.</param>
        /// <returns></returns>
        public TwitterStatus SendTweet(string text, double latitude, double longitude)
        {
            return WithTweetSharp<TwitterStatus>(q => q.Statuses().Update(text, latitude, longitude));
        }

        /// <summary>
        /// Tweets the specified text from the authenticated user.
        /// Includes the provided <see cref="TwitterGeoLocation" /> data.
        /// A tweet with text identical to the authenticating user's 
        /// current status will be ignored.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="location">The location.</param>
        /// <returns></returns>
        public TwitterStatus SendTweet(string text, TwitterGeoLocation location)
        {
            return WithTweetSharp<TwitterStatus>(q => q.Statuses().Update(text, location));
        }

        /// <summary>
        /// Tweets the specified text from the authenticated user.
        /// You must mention a user using @username in your message
        /// if you intend your tweet to include a reference to <see cref="TwitterStatus.InReplyToStatusId" />.
        /// A tweet with text identical to the authenticating user's 
        /// current status will be ignored.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="inReplyToStatusId"></param>
        /// <returns></returns>
        public TwitterStatus SendTweet(string text, long inReplyToStatusId)
        {
            return WithTweetSharp<TwitterStatus>(q => q.Statuses().Update(text).InReplyToStatus(inReplyToStatusId));
        }

        /// <summary>
        /// Tweets the specified text from the authenticated user.
        /// Includes the provided <see cref="TwitterGeoLocation"/> data.
        /// You must mention a user using @username in your message
        /// if you intend your tweet to include a reference to <see cref="TwitterStatus.InReplyToStatusId"/>.
        /// A tweet with text identical to the authenticating user's
        /// current status will be ignored.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="inReplyToStatusId">The ID of the tweet this tweet is replying to.</param>
        /// <param name="location">The location.</param>
        /// <returns></returns>
        public TwitterStatus SendTweet(string text, long inReplyToStatusId, TwitterGeoLocation location)
        {
            return
                WithTweetSharp<TwitterStatus>(
                                                 q =>
                                                 q.Statuses().Update(text, location).InReplyToStatus(inReplyToStatusId));
        }

        #endregion

        #region Destroy

        /// <summary>
        /// Deletes a tweet. The tweet must be authored by the authenticated user.
        /// </summary>
        /// <param name="status">The tweet to delete.</param>
        /// <returns></returns>
        public TwitterStatus DeleteTweet(TwitterStatus status)
        {
            return WithTweetSharp<TwitterStatus>(q => q.Statuses().Destroy(status.Id));
        }

        /// <summary>
        /// Deletes a tweet. The tweet must be authored by the authenticated user.
        /// </summary>
        /// <param name="id">The tweet ID.</param>
        /// <returns></returns>
        public TwitterStatus DeleteTweet(long id)
        {
            return WithTweetSharp<TwitterStatus>(q => q.Statuses().Destroy(id));
        }

        /// <summary>
        /// Deletes a tweet. The tweet must be authored by the authenticated user.
        /// </summary>
        /// <param name="id">The tweet ID.</param>
        /// <returns></returns>
        public TwitterStatus DeleteTweet(int id)
        {
            return WithTweetSharp<TwitterStatus>(q => q.Statuses().Destroy(id));
        }

        #endregion

        #region Retweet

        /// <summary>
        /// Posts a retweet of a given tweet.
        /// </summary>
        /// <param name="statusId">The status ID.</param>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-statuses-retweet" />
        /// <returns></returns>
        public TwitterStatus SendRetweet(long statusId)
        {
            return WithTweetSharp<TwitterStatus>(q => q.Statuses().Retweet(statusId));
        }

        /// <summary>
        /// Posts a retweet of a given tweet.
        /// </summary>
        /// <param name="status">The status.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-statuses-retweet"/>
        public TwitterStatus SendRetweet(TwitterStatus status)
        {
            return WithTweetSharp<TwitterStatus>(q => q.Statuses().Retweet(status));
        }

        /// <summary>
        /// Posts a retweet of a given tweet.
        /// </summary>
        /// <param name="statusId">The status ID.</param>
        /// <param name="mode">The <seealso cref="RetweetMode" /> mode.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-statuses-retweet"/>
        public TwitterStatus SendRetweet(long statusId, RetweetMode mode)
        {
            return WithTweetSharp<TwitterStatus>(q => q.Statuses().Retweet(statusId, mode));
        }

        /// <summary>
        /// Sends the retweet.
        /// </summary>
        /// <param name="status">The status.</param>
        /// <param name="mode">The <seealso cref="RetweetMode" /> mode.</param>
        /// <returns></returns>
        public TwitterStatus SendRetweet(TwitterStatus status, RetweetMode mode)
        {
            return WithTweetSharp<TwitterStatus>(q => q.Statuses().Retweet(status, mode));
        }

        #endregion

        #region Retweets

        /// <summary>
        /// Retrieves retweets of a given tweet.
        /// Requires authentication.
        /// </summary>
        /// <param name="statusId">The status ID.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method:-GET-statuses-id-retweeted_by"/>
        public IEnumerable<TwitterStatus> ListRetweets(long statusId)
        {
            return WithTweetSharp<IEnumerable<TwitterStatus>>(q => q.Statuses().RetweetsOf(statusId));
        }

        /// <summary>
        /// Retrieves retweets of a given tweet.
        /// Requires authentication.
        /// </summary>
        /// <param name="statusId">The status ID.</param>
        /// <param name="count">The number of results expected.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method:-GET-statuses-id-retweeted_by"/>
        public IEnumerable<TwitterStatus> ListRetweets(long statusId, int count)
        {
            return WithTweetSharp<IEnumerable<TwitterStatus>>(q => q.Statuses().RetweetsOf(statusId).Take(count));
        }

        /// <summary>
        /// Retrieves retweets of a given tweet.
        /// Requires authentication.
        /// </summary>
        /// <param name="statusId">The status ID.</param>
        /// <param name="page">The page expected.</param>
        /// <param name="count">The number of results expected.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method:-GET-statuses-id-retweeted_by"/>
        public IEnumerable<TwitterStatus> ListRetweets(long statusId, int page, int count)
        {
            return
                WithTweetSharp<IEnumerable<TwitterStatus>>(q => q.Statuses().RetweetsOf(statusId).Skip(page).Take(count));
        }

        /// <summary>
        /// Retrieves retweets of a given tweet.
        /// Requires authentication.
        /// List is limited to tweets after a given ID.
        /// </summary>
        /// <param name="statusId">The status ID.</param>
        /// <param name="sinceId">The since ID.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method:-GET-statuses-id-retweeted_by"/>
        public IEnumerable<TwitterStatus> ListRetweetsSince(long statusId, long sinceId)
        {
            return WithTweetSharp<IEnumerable<TwitterStatus>>(q => q.Statuses().RetweetsOf(statusId).Since(sinceId));
        }

        /// <summary>
        /// Retrieves retweets of a given tweet.
        /// Requires authentication.
        /// List is limited to tweets after a given ID.
        /// </summary>
        /// <param name="statusId">The status ID.</param>
        /// <param name="sinceId">The since ID.</param>
        /// <param name="count">The number of results expected.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method:-GET-statuses-id-retweeted_by"/>
        public IEnumerable<TwitterStatus> ListRetweetsSince(long statusId, long sinceId, int count)
        {
            return WithTweetSharp<IEnumerable<TwitterStatus>>(
                q =>q.Statuses().RetweetsOf(statusId).Since(sinceId).Take(count)
                );
        }

        /// <summary>
        /// Retrieves retweets of a given tweet.
        /// Requires authentication.
        /// List is limited to tweets after a given ID.
        /// </summary>
        /// <param name="statusId">The status ID.</param>
        /// <param name="sinceId">The since ID.</param>
        /// <param name="page">The page expected.</param>
        /// <param name="count">The number of results expected.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method:-GET-statuses-id-retweeted_by"/>
        public IEnumerable<TwitterStatus> ListRetweetsSince(long statusId, long sinceId, int page, int count)
        {
            return WithTweetSharp<IEnumerable<TwitterStatus>>(
                q => q.Statuses().RetweetsOf(statusId).Since(sinceId).Skip(page).Take(count)
                );
        }

        /// <summary>
        /// Retrieves retweets of a given tweet.
        /// Requires authentication.
        /// List is limited to tweets before a given ID.
        /// </summary>
        /// <param name="statusId">The status ID.</param>
        /// <param name="maxId">The max ID.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method:-GET-statuses-id-retweeted_by"/>
        public IEnumerable<TwitterStatus> ListRetweetsBefore(long statusId, long maxId)
        {
            return WithTweetSharp<IEnumerable<TwitterStatus>>(q => q.Statuses().RetweetsOf(statusId).Before(maxId));
        }

        /// <summary>
        /// Retrieves retweets of a given tweet.
        /// Requires authentication.
        /// List is limited to tweets before a given ID.
        /// </summary>
        /// <param name="statusId">The status ID.</param>
        /// <param name="maxId">The max ID.</param>
        /// <param name="count">The number of results expected.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method:-GET-statuses-id-retweeted_by"/>
        public IEnumerable<TwitterStatus> ListRetweetsBefore(long statusId, long maxId, int count)
        {
            return
                WithTweetSharp<IEnumerable<TwitterStatus>>(
                                                              q =>
                                                              q.Statuses().RetweetsOf(statusId).Before(maxId).Take(count));
        }

        /// <summary>
        /// Retrieves retweets of a given tweet.
        /// Requires authentication.
        /// List is limited to tweets before a given ID.
        /// </summary>
        /// <param name="statusId">The status ID.</param>
        /// <param name="maxId">The max ID.</param>
        /// <param name="page">The page expected.</param>
        /// <param name="count">The number of results expected.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method:-GET-statuses-id-retweeted_by"/>
        public IEnumerable<TwitterStatus> ListRetweetsBefore(long statusId, long maxId, int page, int count)
        {
            return WithTweetSharp<IEnumerable<TwitterStatus>>(
                q => q.Statuses().RetweetsOf(statusId).Before(maxId).Skip(page).Take(count)
                );
        }

        #endregion

        #endregion

        #region User Methods

        /// <summary>
        /// Gets up to the first 100 friends for the authenticating user.
        /// </summary>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-statuses%C2%A0friends" />
        /// <returns></returns>
        public TwitterCursorList<TwitterUser> ListFriends()
        {
            return WithTweetSharpAndCursors<TwitterUser>(q => q.Users().GetFriends().CreateCursor());
        }

        /// <summary>
        /// Lists the friends of the authenticated user by cursor value.
        /// This is useful for paging through large numbers of friends.
        /// </summary>
        /// <param name="cursor">The cursor.</param>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-statuses%C2%A0friends"/>
        /// <returns></returns>
        public TwitterCursorList<TwitterUser> ListFriends(long cursor)
        {
            return WithTweetSharpAndCursors<TwitterUser>(q => q.Users().GetFriends().GetCursor(cursor));
        }

        /// <summary>
        /// Gets up to the first 100 friends for the authenticating user.
        /// </summary>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-statuses%C2%A0followers"/>
        /// <returns></returns>
        public TwitterCursorList<TwitterUser> ListFollowers()
        {
            return WithTweetSharpAndCursors<TwitterUser>(q => q.Users().GetFollowers().CreateCursor());
        }

        /// <summary>
        /// Lists the friends of the authenticated user by cursor value.
        /// This is useful for paging through large numbers of friends.
        /// </summary>
        /// <param name="cursor">The cursor value for paging.</param>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-statuses%C2%A0followers"/>
        /// <returns></returns>
        public TwitterCursorList<TwitterUser> ListFollowers(long cursor)
        {
            return WithTweetSharpAndCursors<TwitterUser>(q => q.Users().GetFollowers().GetCursor(cursor));
        }

        /// <summary>
        /// Gets the authenticated user's profile.
        /// This is achieved with a call to verify credentials.
        /// </summary>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-account%C2%A0verify_credentials"/>
        /// <returns></returns>
        public TwitterUser GetUserProfile()
        {
            return WithTweetSharp<TwitterUser>(q => q.Account().VerifyCredentials());
        }

        /// <summary>
        /// Gets the specified user screen name's profile.
        /// </summary>
        /// <param name="screenName">The user's screen name.</param>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-users%C2%A0show" />
        /// <returns></returns>
        public TwitterUser GetUserProfileFor(string screenName)
        {
            return WithTweetSharp<TwitterUser>(q => q.Users().ShowProfileFor(screenName));
        }

        /// <summary>
        /// Gets the specified user ID's profile.
        /// </summary>
        /// <param name="id">The user ID.</param>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-users%C2%A0show" />
        /// <returns></returns>
        public TwitterUser GetUserProfileFor(int id)
        {
            return WithTweetSharp<TwitterUser>(q => q.Users().ShowProfileFor(id));
        }

        /// <summary>
        /// Searches for a Twitter user given a query.
        /// This search is the same as Twitter's web-based People Search.
        /// This search yields a maximum of 1000 results in total.
        /// </summary>
        /// <param name="query">The search query.</param>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-users-search" />
        /// <returns></returns>
        public IEnumerable<TwitterUser> SearchForUser(string query)
        {
            return WithTweetSharp<IEnumerable<TwitterUser>>(q => q.Users().SearchFor(query));
        }

        /// <summary>
        /// Searches for a Twitter user given a query.
        /// This search is the same as Twitter's web-based People Search.
        /// This search yields a maximum of 1000 results in total.
        /// </summary>
        /// <param name="query">The search query.</param>
        /// <param name="page">The page of user results to get for this query.</param>
        /// <returns></returns>
        public IEnumerable<TwitterUser> SearchForUser(string query, int page)
        {
            return WithTweetSharp<IEnumerable<TwitterUser>>(q => q.Users().SearchFor(query).Page(page));
        }

        /// <summary>
        /// Searches for a Twitter user given a query.
        /// This search is the same as Twitter's web-based People Search.
        /// This search yields a maximum of 1000 results in total.
        /// </summary>
        /// <param name="query">The search query.</param>
        /// <param name="page">The page of user results to get for this query.</param>
        /// <param name="count">The number of results to return on this page.</param>
        /// <returns></returns>
        public IEnumerable<TwitterUser> SearchForUser(string query, int page, int count)
        {
            return WithTweetSharp<IEnumerable<TwitterUser>>(q => q.Users().SearchFor(query).Page(page).Count(count));
        }

        

        /// <summary>
        /// Gets the user profiles of up to 20 of the specified user screen names.
        /// </summary>
        /// <param name="screenNames">The screen names to retrieve.</param>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method:-users-lookup" />
        /// <returns></returns>
        public IEnumerable<TwitterUser> ListUserProfilesFor(IEnumerable<string> screenNames)
        {
            screenNames = screenNames.Count() >= 20 ? screenNames.Take(20) : screenNames;
            return WithTweetSharp<IEnumerable<TwitterUser>>(q => q.Users().Lookup(screenNames));
        }

        /// <summary>
        /// Gets the user profiles of up to 20 of the specified user IDs.
        /// </summary>
        /// <param name="userIds">The user IDs to retrieve.</param>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method:-users-lookup" />
        /// <returns></returns>
        public IEnumerable<TwitterUser> ListUserProfilesFor(IEnumerable<int> userIds)
        {
            userIds = userIds.Count() >= 20 ? userIds.Take(20) : userIds;
            return WithTweetSharp<IEnumerable<TwitterUser>>(q => q.Users().Lookup(userIds));
        }

        /// <summary>
        /// Gets the user profiles of up to 20 of the specified user screen names and IDs.
        /// </summary>
        /// <param name="screenNames">The screen names to retrieve.</param>
        /// <param name="userIds">The user IDs to retrieve.</param>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method:-users-lookup" />
        /// <returns></returns>
        public IEnumerable<TwitterUser> ListUserProfilesFor(IEnumerable<string> screenNames, IEnumerable<int> userIds)
        {
            screenNames = screenNames.Count() >= 20 ? screenNames.Take(20) : screenNames;
            var leftover = 20 - screenNames.Count();
            userIds = userIds.Count() >= leftover ? userIds.Take(leftover) : userIds;

            return WithTweetSharp<IEnumerable<TwitterUser>>(q => q.Users().Lookup(screenNames, userIds));
        }

        #endregion

        #region List Methods

        #region POST lists

        /// <summary>
        /// Creates a new public list for the authenticated user. Accounts are limited to 20 lists.
        /// </summary>
        /// <param name="listOwnerScreenName">The list owner's screen name.</param>
        /// <param name="listName">The list name.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-POST-lists"/>
        public TwitterList CreatePublicList(string listOwnerScreenName, string listName)
        {
            return CreatePublicList(listOwnerScreenName, listName, null);
        }

        /// <summary>
        /// Creates a new public list for the authenticated user. Accounts are limited to 20 lists.
        /// </summary>
        /// <param name="listOwnerScreenName">The list owner's screen name.</param>
        /// <param name="listName">The list name.</param>
        /// <param name="listDescription">The list description.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-POST-lists"/>
        public TwitterList CreatePublicList(string listOwnerScreenName, string listName, string listDescription)
        {
            return WithTweetSharp<TwitterList>(
                q => q.Lists().CreatePublicList(listOwnerScreenName, listName, listDescription)
                );
        }

        /// <summary>
        /// Creates a new list for the authenticated user. Accounts are limited to 20 lists.
        /// </summary>
        /// <param name="listOwnerScreenName">The list owner's screen name.</param>
        /// <param name="listName">The list name.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-POST-lists"/>
        public TwitterList CreatePrivateList(string listOwnerScreenName, string listName)
        {
            return WithTweetSharp<TwitterList>(
                q => q.Lists().CreatePrivateList(listOwnerScreenName, listName, null)
                );
        }

        /// <summary>
        /// Creates a new list for the authenticated user. Accounts are limited to 20 lists.
        /// </summary>
        /// <param name="listOwnerScreenName">The list owner's screen name.</param>
        /// <param name="listName">The list name.</param>
        /// <param name="listDescription">The list description.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-POST-lists"/>
        public TwitterList CreatePrivateList(string listOwnerScreenName, string listName, string listDescription)
        {
            return WithTweetSharp<TwitterList>(
                q => q.Lists().CreatePrivateList(listOwnerScreenName, listName, listDescription)
                );
        }

        #endregion

        #region POST lists id

        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-POST-lists-id" />
        /// <returns></returns>
        public TwitterList UpdateList(TwitterList list)
        {
            return WithTweetSharp<TwitterList>(q => q.Lists().UpdateList(list));
        }

        /// <summary>
        /// List the lists of the specified user. 
        /// Private lists are included if the authenticated users is the same as the 
        /// user whose lists are being returned.
        /// </summary>
        /// <param name="listOwnerScreenName">The screen name of the list owner</param>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-GET-lists"/>
        /// <returns></returns>
        public TwitterCursorList<TwitterList> ListListsFor(string listOwnerScreenName)
        {
            return WithTweetSharpAndCursorsForLists(
                q => q.Lists().GetListsBy(listOwnerScreenName).CreateCursor()
                );
        }
        /// <summary>
        /// List the lists of the specified user. 
        /// Private lists are included if the authenticated users is the same as the 
        /// user whose lists are being returned.
        /// </summary>
        /// <param name="listOwnerScreenName">The screen name of the list owner</param>
        /// <param name="cursor">The cursor location in the master list</param>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-GET-lists"/>
        /// <returns></returns>
        public TwitterCursorList<TwitterList> ListListsFor(string listOwnerScreenName, long cursor)
        {
            return WithTweetSharpAndCursorsForLists(
                q => q.Lists().GetListsBy(listOwnerScreenName).GetCursor(cursor)
                );
        }

        #endregion

        #region GET list id

        /// <summary>
        /// Gets the list identified by the given info.
        /// </summary>
        /// <param name="listOwnerScreenName">The list owner's screen name.</param>
        /// <param name="listId">The list ID.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-GET-list-id"/>
        public TwitterList GetList(string listOwnerScreenName, long listId)
        {
            return WithTweetSharp<TwitterList>(q => q.Lists().GetListBy(listOwnerScreenName, listId));
        }

        /// <summary>
        /// Gets the list identified by the given info.
        /// </summary>
        /// <param name="listOwnerScreenName">The list owner's screen name.</param>
        /// <param name="listId">The list ID.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-GET-list-id"/>
        public TwitterList GetList(string listOwnerScreenName, int listId)
        {
            return WithTweetSharp<TwitterList>(q => q.Lists().GetListBy(listOwnerScreenName, listId));
        }

        /// <summary>
        /// Gets the list identified by the given info.
        /// </summary>
        /// <param name="listOwnerScreenName">The list owner's screen name.</param>
        /// <param name="listSlug">The list slug.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-GET-list-id"/>
        public TwitterList GetList(string listOwnerScreenName, string listSlug)
        {
            return WithTweetSharp<TwitterList>(q => q.Lists().GetListBy(listOwnerScreenName, listSlug));
        }

        #endregion

        #region DELETE list id

        /// <summary>
        /// Deletes a list identified by the given info.
        /// </summary>
        /// <param name="listOwnerScreenName">The list owner's screen name.</param>
        /// <param name="listId">The list ID</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-DELETE-list-id"/>
        public TwitterList DeleteList(string listOwnerScreenName, long listId)
        {
            return WithTweetSharp<TwitterList>(q => q.Lists().DeleteList(listOwnerScreenName, listId));
        }

        /// <summary>
        /// Deletes a list identified by the given info.
        /// </summary>
        /// <param name="listOwnerScreenName">The list owner's screen name.</param>
        /// <param name="listId">The list ID.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-DELETE-list-id"/>
        public TwitterList DeleteList(string listOwnerScreenName, int listId)
        {
            return WithTweetSharp<TwitterList>(q => q.Lists().DeleteList(listOwnerScreenName, listId));
        }

        /// <summary>
        /// Deletes a list identified by the given info.
        /// </summary>
        /// <param name="listOwnerScreenName">The list owner's screen name.</param>
        /// <param name="listSlug">The list slug.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-DELETE-list-id"/>
        public TwitterList DeleteList(string listOwnerScreenName, string listSlug)
        {
            return WithTweetSharp<TwitterList>(q => q.Lists().DeleteList(listOwnerScreenName, listSlug));
        }

        #endregion

        #region GET list statuses

        /// <summary>
        /// Retrieves tweets on the list owner's list timeline.
        /// Requires authentication.
        /// </summary>
        /// <param name="listOwnerScreenName">The list owner's screen name.</param>
        /// <param name="listId">The list ID.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-GET-list-statuses"/>
        public IEnumerable<TwitterStatus> ListTweetsOnListTimeline(string listOwnerScreenName, int listId)
        {
            return WithTweetSharp<IEnumerable<TwitterStatus>>(
                q => q.Statuses().OnListTimeline(listOwnerScreenName, listId)
                );
        }

        /// <summary>
        /// Retrieves tweets on the list owner's list timeline.
        /// Requires authentication.
        /// </summary>
        /// <param name="listOwnerScreenName">The list owner's screen name.</param>
        /// <param name="listId">The list ID.</param>
        /// <param name="count">The number of results expected.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-GET-list-statuses"/>
        public IEnumerable<TwitterStatus> ListTweetsOnListTimeline(string listOwnerScreenName, int listId, int count)
        {
            return ListTweetsOnListTimeline(listOwnerScreenName, listId, 1, count);
        }

        /// <summary>
        /// Retrieves tweets on the list owner's list timeline.
        /// Requires authentication.
        /// </summary>
        /// <param name="listOwnerScreenName">The list owner's screen name.</param>
        /// <param name="listId">The list ID.</param>
        /// <param name="page">The page expected.</param>
        /// <param name="count">The number of results expected.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-GET-list-statuses"/>
        public IEnumerable<TwitterStatus> ListTweetsOnListTimeline(string listOwnerScreenName, int listId, int page, int count)
        {
            return WithTweetSharp<IEnumerable<TwitterStatus>>(
            q => q.Statuses().OnListTimeline(listOwnerScreenName, listId).Skip(page).Take(count)
            );
        }

        /// <summary>
        /// Retrieves tweets on the list owner's list timeline.
        /// Requires authentication.
        /// List is limited to tweets after a given ID.
        /// </summary>
        /// <param name="listOwnerScreenName">The list owner's screen name.</param>
        /// <param name="listId">The list ID.</param>
        /// <param name="sinceId">The since ID.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-GET-list-statuses"/>
        public IEnumerable<TwitterStatus> ListTweetsOnListTimelineSince(string listOwnerScreenName, int listId, long sinceId)
        {
            return WithTweetSharp<IEnumerable<TwitterStatus>>(
            q => q.Statuses().OnListTimeline(listOwnerScreenName, listId).Since(sinceId)
            );
        }

        /// <summary>
        /// Retrieves tweets on the list owner's list timeline.
        /// Requires authentication.
        /// List is limited to tweets after a given ID.
        /// </summary>
        /// <param name="listOwnerScreenName">The list owner's screen name.</param>
        /// <param name="listId">The list ID.</param>
        /// <param name="sinceId">The since ID.</param>
        /// <param name="count">The number of results expected.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-GET-list-statuses"/>
        public IEnumerable<TwitterStatus> ListTweetsOnListTimelineSince(string listOwnerScreenName, int listId, long sinceId, int count)
        {
            return WithTweetSharp<IEnumerable<TwitterStatus>>(
                q => q.Statuses().OnListTimeline(listOwnerScreenName, listId).Since(sinceId).Take(count)
                    );
        }

        /// <summary>
        /// Retrieves tweets on the list owner's list timeline.
        /// Requires authentication.
        /// List is limited to tweets after a given ID.
        /// </summary>
        /// <param name="listOwnerScreenName">The list owner's screen name.</param>
        /// <param name="listId">The list ID.</param>
        /// <param name="sinceId">The since ID.</param>
        /// <param name="page">The page expected.</param>
        /// <param name="count">The number of results expected.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-GET-list-statuses"/>
        public IEnumerable<TwitterStatus> ListTweetsOnListTimelineSince(string listOwnerScreenName, int listId, long sinceId, int page, int count)
        {
            return WithTweetSharp<IEnumerable<TwitterStatus>>(
                q => q.Statuses().OnListTimeline(listOwnerScreenName, listId).Since(sinceId).Skip(page).Take(count)
                );
        }

        /// <summary>
        /// Retrieves tweets on the list owner's list timeline.
        /// Requires authentication.
        /// List is limited to tweets before a given ID.
        /// </summary>
        /// <param name="listOwnerScreenName">The list owner's screen name.</param>
        /// <param name="listId">The list ID.</param>
        /// <param name="maxId">The max ID.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-GET-list-statuses"/>
        public IEnumerable<TwitterStatus> ListTweetsOnListTimelineBefore(string listOwnerScreenName, int listId, long maxId)
        {
            return WithTweetSharp<IEnumerable<TwitterStatus>>(
                q => q.Statuses().OnListTimeline(listOwnerScreenName, listId).Before(maxId)
                );
        }

        /// <summary>
        /// Retrieves tweets on the list owner's list timeline.
        /// Requires authentication.
        /// List is limited to tweets before a given ID.
        /// </summary>
        /// <param name="listOwnerScreenName">The list owner's screen name.</param>
        /// <param name="listId">The list ID.</param>
        /// <param name="maxId">The max ID.</param>
        /// <param name="count">The number of results expected.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-GET-list-statuses"/>
        public IEnumerable<TwitterStatus> ListTweetsOnListTimelineBefore(string listOwnerScreenName, int listId,
                                                                         long maxId, int count)
        {
            return WithTweetSharp<IEnumerable<TwitterStatus>>(
                q => q.Statuses().OnListTimeline(listOwnerScreenName, listId).Before(maxId).Take(count)
                );
        }

        /// <summary>
        /// Retrieves tweets on the list owner's list timeline.
        /// Requires authentication.
        /// List is limited to tweets before a given ID.
        /// </summary>
        /// <param name="listOwnerScreenName">The list owner's screen name.</param>
        /// <param name="listId">The list ID.</param>
        /// <param name="maxId">The max ID.</param>
        /// <param name="page">The page expected.</param>
        /// <param name="count">The number of results expected.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-GET-list-statuses"/>
        public IEnumerable<TwitterStatus> ListTweetsOnListTimelineBefore(string listOwnerScreenName, int listId, long maxId, int page, int count)
        {
            return WithTweetSharp<IEnumerable<TwitterStatus>>(
                q => q.Statuses().OnListTimeline(listOwnerScreenName, listId).Before(maxId).Skip(page).Take(count)
                );
        }

        /// <summary>
        /// Retrieves tweets on the list owner's list timeline.
        /// Requires authentication.
        /// </summary>
        /// <param name="listOwnerScreenName">The list owner's screen name.</param>
        /// <param name="listSlug">The list slug.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-GET-list-statuses"/>
        public IEnumerable<TwitterStatus> ListTweetsOnListTimeline(string listOwnerScreenName, string listSlug)
        {
            return WithTweetSharp<IEnumerable<TwitterStatus>>(
                    q => q.Statuses().OnListTimeline(listOwnerScreenName, listSlug)
                    );
        }

        /// <summary>
        /// Retrieves tweets on the list owner's list timeline.
        /// Requires authentication.
        /// </summary>
        /// <param name="listOwnerScreenName">The list owner's screen name.</param>
        /// <param name="listSlug">The list slug.</param>
        /// <param name="count">The number of results expected.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-GET-list-statuses"/>
        public IEnumerable<TwitterStatus> ListTweetsOnListTimeline(string listOwnerScreenName, string listSlug, int count)
        {
            return ListTweetsOnListTimeline(listOwnerScreenName, listSlug, 1, count);
        }

        /// <summary>
        /// Retrieves tweets on the list owner's list timeline.
        /// Requires authentication.
        /// </summary>
        /// <param name="listOwnerScreenName">The list owner's screen name.</param>
        /// <param name="listSlug">The list slug.</param>
        /// <param name="page">The page expected.</param>
        /// <param name="count">The number of results expected.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-GET-list-statuses"/>
        public IEnumerable<TwitterStatus> ListTweetsOnListTimeline(string listOwnerScreenName, string listSlug, int page, int count)
        {
            return WithTweetSharp<IEnumerable<TwitterStatus>>(
                q => q.Statuses().OnListTimeline(listOwnerScreenName, listSlug).Skip(page).Take(count)
                );
        }

        /// <summary>
        /// Retrieves tweets on the list owner's list timeline.
        /// Requires authentication.
        /// List is limited to tweets after a given ID.
        /// </summary>
        /// <param name="listOwnerScreenName">The list owner's screen name.</param>
        /// <param name="listSlug">The list slug.</param>
        /// <param name="sinceId">The since ID.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-GET-list-statuses"/>
        public IEnumerable<TwitterStatus> ListTweetsOnListTimelineSince(string listOwnerScreenName, string listSlug, long sinceId)
        {
            return
                WithTweetSharp<IEnumerable<TwitterStatus>>(
                    q => q.Statuses().OnListTimeline(listOwnerScreenName, listSlug).Since(sinceId)
                    );
        }

        /// <summary>
        /// Retrieves tweets on the list owner's list timeline.
        /// Requires authentication.
        /// List is limited to tweets after a given ID.
        /// </summary>
        /// <param name="listOwnerScreenName">The list owner's screen name.</param>
        /// <param name="listSlug">The list slug.</param>
        /// <param name="sinceId">The since ID.</param>
        /// <param name="count">The number of results expected.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-GET-list-statuses"/>
        public IEnumerable<TwitterStatus> ListTweetsOnListTimelineSince(string listOwnerScreenName, string listSlug,
                                                                        long sinceId, int count)
        {
            return
                WithTweetSharp<IEnumerable<TwitterStatus>>(
                    q =>
                    q.Statuses().OnListTimeline(listOwnerScreenName, listSlug)
                        .Since(sinceId).Take(count));
        }

        /// <summary>
        /// Retrieves tweets on the list owner's list timeline.
        /// Requires authentication.
        /// List is limited to tweets after a given ID.
        /// </summary>
        /// <param name="listOwnerScreenName">The list owner's screen name.</param>
        /// <param name="listSlug">The list slug.</param>
        /// <param name="sinceId">The since ID.</param>
        /// <param name="page">The page expected.</param>
        /// <param name="count">The number of results expected.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-GET-list-statuses"/>
        public IEnumerable<TwitterStatus> ListTweetsOnListTimelineSince(string listOwnerScreenName, string listSlug,
                                                                        long sinceId, int page, int count)
        {
            return WithTweetSharp<IEnumerable<TwitterStatus>>(
                    q => q.Statuses().OnListTimeline(listOwnerScreenName, listSlug).Since(sinceId).Skip(page).Take(count)
                    );
        }

        /// <summary>
        /// Retrieves tweets on the list owner's list timeline.
        /// Requires authentication.
        /// List is limited to tweets before a given ID.
        /// </summary>
        /// <param name="listOwnerScreenName">The list owner's screen name.</param>
        /// <param name="listSlug">The list slug.</param>
        /// <param name="maxId">The max ID.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-GET-list-statuses"/>
        public IEnumerable<TwitterStatus> ListTweetsOnListTimelineBefore(string listOwnerScreenName, string listSlug,
                                                                         long maxId)
        {
            return
                WithTweetSharp<IEnumerable<TwitterStatus>>(
                    q => q.Statuses().OnListTimeline(listOwnerScreenName, listSlug).Before(maxId)
                    );
        }

        /// <summary>
        /// Retrieves tweets on the list owner's list timeline.
        /// Requires authentication.
        /// List is limited to tweets before a given ID.
        /// </summary>
        /// <param name="listOwnerScreenName">The list owner's screen name.</param>
        /// <param name="listSlug">The list slug.</param>
        /// <param name="maxId">The max ID.</param>
        /// <param name="count">The number of results expected.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-GET-list-statuses"/>
        public IEnumerable<TwitterStatus> ListTweetsOnListTimelineBefore(string listOwnerScreenName, string listSlug,
                                                                         long maxId, int count)
        {
            return WithTweetSharp<IEnumerable<TwitterStatus>>(
                    q => q.Statuses().OnListTimeline(listOwnerScreenName, listSlug).Before(maxId).Take(count)
                    );
        }

        /// <summary>
        /// Retrieves tweets on the list owner's list timeline.
        /// Requires authentication.
        /// List is limited to tweets before a given ID.
        /// </summary>
        /// <param name="listOwnerScreenName">The list owner's screen name.</param>
        /// <param name="listSlug">The list slug.</param>
        /// <param name="maxId">The max ID.</param>
        /// <param name="page">The page expected.</param>
        /// <param name="count">The number of results expected.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-GET-list-statuses"/>
        public IEnumerable<TwitterStatus> ListTweetsOnListTimelineBefore(string listOwnerScreenName, string listSlug,
                                                                         long maxId, int page, int count)
        {
            return WithTweetSharp<IEnumerable<TwitterStatus>>(
                q =>q.Statuses().OnListTimeline(listOwnerScreenName, listSlug).Before(maxId).Skip(page).Take(count)
                );
        }

        #endregion

        #region GET list memberships

        /// <summary>
        /// List the lists the specified user has been added to.
        /// </summary>
        /// <param name="listOwnerScreenName">The list owner's screen name.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-GET-list-memberships"/>
        public TwitterCursorList<TwitterList> ListListMembershipsFor(string listOwnerScreenName)
        {
            return WithTweetSharpAndCursors<TwitterList>(
                q => q.Lists().GetMemberships(listOwnerScreenName).CreateCursor()
                );
        }

        /// <summary>
        /// List the lists the specified user follows.
        /// </summary>
        /// <param name="listSubscriberScreenName">The list subscriber's screen name.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-GET-list-subscriptions"/>
        public TwitterCursorList<TwitterList> ListListSubscriptionsFor(string listSubscriberScreenName)
        {
            return WithTweetSharpAndCursors<TwitterList>(
                q => q.Lists().GetSubscriptions(listSubscriberScreenName).CreateCursor()
                );
        }

        /// <summary>
        /// List the lists the specified user follows.
        /// </summary>
        /// <param name="listSubscriberScreenName">The list subscriber's screen name.</param>
        /// <param name="cursor">The cursor.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-GET-list-subscriptions"/>
        public TwitterCursorList<TwitterList> ListListSubscriptionsFor(string listSubscriberScreenName, long cursor)
        {
            return WithTweetSharpAndCursors<TwitterList>(
                q =>q.Lists().GetSubscriptions(listSubscriberScreenName).GetCursor(cursor)
                );
        }

        #endregion

        #endregion

        #region List Members Methods

        #region GET list members

        /// <summary>
        /// Retrieves the members of the specified list.
        /// </summary>
        /// <param name="listOwnerScreenName">The list owner's screen name.</param>
        /// <param name="listId">The list ID.</param>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-GET-list-members"/>
        /// <returns></returns>
        public TwitterCursorList<TwitterUser> ListListMembers(string listOwnerScreenName, int listId)
        {
            return WithTweetSharpAndCursors<TwitterUser>(
                q => q.Lists().GetMembersOf(listOwnerScreenName, listId).CreateCursor()
                );
        }

        /// <summary>
        /// Retrieves the members of the specified list.
        /// </summary>
        /// <param name="listOwnerScreenName">The list owner's screen name.</param>
        /// <param name="listSlug">The list slug.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-GET-list-members"/>
        public TwitterCursorList<TwitterUser> ListListMembers(string listOwnerScreenName, string listSlug)
        {
            return WithTweetSharpAndCursors<TwitterUser>(
                q => q.Lists().GetMembersOf(listOwnerScreenName, listSlug).CreateCursor()
                );
        }

        /// <summary>
        /// Retrieves the members of the specified list.
        /// </summary>
        /// <param name="listOwnerScreenName">The list owner's screen name.</param>
        /// <param name="listId">The list ID.</param>
        /// <param name="cursor">The cursor.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-GET-list-members"/>
        public TwitterCursorList<TwitterUser> ListListMembers(string listOwnerScreenName, int listId, long cursor)
        {
            return WithTweetSharpAndCursors<TwitterUser>(
                q => q.Lists().GetMembersOf(listOwnerScreenName, listId).GetCursor(cursor)
                );
        }

        /// <summary>
        /// Retrieves the members of the specified list.
        /// </summary>
        /// <param name="listOwnerScreenName">The list owner's screen name.</param>
        /// <param name="listSlug">The list slug.</param>
        /// <param name="cursor">The cursor.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-GET-list-members"/>
        public TwitterCursorList<TwitterUser> ListListMembers(string listOwnerScreenName, string listSlug, long cursor)
        {
            return WithTweetSharpAndCursors<TwitterUser>(
                q => q.Lists().GetMembersOf(listOwnerScreenName, listSlug).GetCursor(cursor)
                );
        }

        #endregion

        #region POST list members

        /// <summary>
        /// Add a member to a list. The authenticated user must own the list to add members to it.
        /// Lists are limited to 500 members.
        /// </summary>
        /// <param name="listOwnerScreenName">The list owner's screen name.</param>
        /// <param name="listId">The list ID.</param>
        /// <param name="userId">The user ID.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-POST-list-members"/>
        public TwitterList AddMemberToList(string listOwnerScreenName, long listId, int userId)
        {
            return WithTweetSharp<TwitterList>(
                q => q.Lists().AddMemberTo(listOwnerScreenName, listId, userId)
                );
        }

        /// <summary>
        /// Add a member to a list. The authenticated user must own the list to add members to it.
        /// Lists are limited to 500 members.
        /// </summary>
        /// <param name="listOwnerScreenName">The list owner's screen name.</param>
        /// <param name="listId">The list ID.</param>
        /// <param name="userId">The user ID.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-POST-list-members"/>
        public TwitterList AddMemberToList(string listOwnerScreenName, int listId, int userId)
        {
            return WithTweetSharp<TwitterList>(
                q => q.Lists().AddMemberTo(listOwnerScreenName, listId, userId)
                );
        }

        /// <summary>
        /// Add a member to a list. The authenticated user must own the list to add members to it.
        /// Lists are limited to 500 members.
        /// </summary>
        /// <param name="listOwnerScreenName">The list owner's screen name.</param>
        /// <param name="listSlug">The list slug.</param>
        /// <param name="userId">The user ID.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-POST-list-members"/>
        public TwitterList AddMemberToList(string listOwnerScreenName, string listSlug, int userId)
        {
            return WithTweetSharp<TwitterList>(
                q => q.Lists().AddMemberTo(listOwnerScreenName, listSlug, userId)
                );
        }

        #endregion

        #region DELETE list members

        /// <summary>
        /// Removes the specified member from the list.
        /// The authenticated user must be the list's owner to remove members from the list.
        /// </summary>
        /// <param name="listOwnerScreenName">The list owner's screen name.</param>
        /// <param name="listId">The list ID.</param>
        /// <param name="userId">The user ID to remove.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-DELETE-list-members"/>
        public TwitterList RemoveMemberFromList(string listOwnerScreenName, int listId, int userId)
        {
            return WithTweetSharp<TwitterList>(q => q.Lists().RemoveMemberFrom(listOwnerScreenName, listId, userId));
        }

        /// <summary>
        /// Removes the specified member from the list.
        /// The authenticated user must be the list's owner to remove members from the list.
        /// </summary>
        /// <param name="listOwnerScreenName">The list owner's screen name.</param>
        /// <param name="listSlug">The list slug.</param>
        /// <param name="userId">The user ID to remove.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-DELETE-list-members"/>
        public TwitterList RemoveMemberFromList(string listOwnerScreenName, string listSlug, int userId)
        {
            return WithTweetSharp<TwitterList>(q => q.Lists().RemoveMemberFrom(listOwnerScreenName, listSlug, userId));
        }

        #endregion

        #region GET list members id

        /// <summary>
        /// Verifies the given user is a member of the specified list.
        /// </summary>
        /// <param name="listOwnerScreenName">The list owner's screen name.</param>
        /// <param name="listId">The list ID.</param>
        /// <param name="userId">The user ID.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-GET-list-subscribers-id"/>
        public TwitterUser VerifyListMember(string listOwnerScreenName, int listId, int userId)
        {
            return WithTweetSharp<TwitterUser>(q => q.Lists().IsUserMemberOf(listOwnerScreenName, listId, userId));
        }

        /// <summary>
        /// Verifies the given user is a member of the specified list.
        /// </summary>
        /// <param name="listOwnerScreenName">The list owner's screen name.</param>
        /// <param name="listSlug">The list slug.</param>
        /// <param name="userId">The user ID.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-GET-list-subscribers-id"/>
        public TwitterUser VerifyListMember(string listOwnerScreenName, string listSlug, int userId)
        {
            return WithTweetSharp<TwitterUser>(q => q.Lists().IsUserMemberOf(listOwnerScreenName, listSlug, userId));
        }

        #endregion 

        #endregion

        #region List Subscribers Methods

        #region GET list susbcribers

        /// <summary>
        /// Returns the subscribers of the specified list.
        /// </summary>
        /// <param name="listOwnerScreenName">The list owner screen name.</param>
        /// <param name="listId">The list ID.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-GET-list-subscribers"/>
        public TwitterCursorList<TwitterUser> ListListFollowers(string listOwnerScreenName, int listId)
        {
            return WithTweetSharpAndCursors<TwitterUser>(
                q => q.Lists().GetSubscribersOf(listOwnerScreenName, listId).CreateCursor()
                );
        }

        /// <summary>
        /// Returns the subscribers of the specified list.
        /// </summary>
        /// <param name="listOwnerScreenName">The list owner screen name.</param>
        /// <param name="listSlug">The list slug.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-GET-list-subscribers"/>
        public TwitterCursorList<TwitterUser> ListListFollowers(string listOwnerScreenName, string listSlug)
        {
            return WithTweetSharpAndCursors<TwitterUser>(
                q => q.Lists().GetSubscribersOf(listOwnerScreenName, listSlug).CreateCursor()
                );
        }

        /// <summary>
        /// Returns the subscribers of the specified list.
        /// </summary>
        /// <param name="listOwnerScreenName">The list owner screen name.</param>
        /// <param name="listId">The list ID.</param>
        /// <param name="cursor">The cursor.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-GET-list-subscribers"/>
        public TwitterCursorList<TwitterUser> ListListFollowers(string listOwnerScreenName, int listId, long cursor)
        {
            return WithTweetSharpAndCursors<TwitterUser>(
                q => q.Lists().GetSubscribersOf(listOwnerScreenName, listId).GetCursor(cursor)
                );
        }

        /// <summary>
        /// Returns the subscribers of the specified list.
        /// </summary>
        /// <param name="listOwnerScreenName">The list owner screen name.</param>
        /// <param name="listSlug">The list slug.</param>
        /// <param name="cursor">The cursor.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-GET-list-subscribers"/>
        public TwitterCursorList<TwitterUser> ListListFollowers(string listOwnerScreenName, string listSlug, long cursor)
        {
            return WithTweetSharpAndCursors<TwitterUser>(
                q => q.Lists().GetSubscribersOf(listOwnerScreenName, listSlug).GetCursor(cursor)
                );
        }

        #endregion

        #region POST list subscribers

        /// <summary>
        /// Subscribes the authenticated user to the specified list.
        /// </summary>
        /// <param name="listOwnerScreenName">The list owner's screen name.</param>
        /// <param name="listId">The list ID.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-POST-list-subscribers"/>
        public TwitterList FollowList(string listOwnerScreenName, long listId)
        {
            return WithTweetSharp<TwitterList>(q => q.Lists().Follow(listOwnerScreenName, listId));
        }

        /// <summary>
        /// Subscribes the authenticated user to the specified list.
        /// </summary>
        /// <param name="listOwnerScreenName">The list owner's screen name.</param>
        /// <param name="listSlug">The list slug.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-POST-list-subscribers"/>
        public TwitterList FollowList(string listOwnerScreenName, string listSlug)
        {
            return WithTweetSharp<TwitterList>(q => q.Lists().Follow(listOwnerScreenName, listSlug));
        }

        #endregion

        #region DELETE list subscribers

        /// <summary>
        /// Unsubscribes the authenticated user from the specified list.
        /// </summary>
        /// <param name="listOwnerScreenName">The list owner's screen name.</param>
        /// <param name="listId">The list ID.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-DELETE-list-subscribers"/>
        public TwitterList UnfollowList(string listOwnerScreenName, long listId)
        {
            return WithTweetSharp<TwitterList>(q => q.Lists().Unfollow(listOwnerScreenName, listId));
        }

        /// <summary>
        /// Unsubscribes the authenticated user from the specified list.
        /// </summary>
        /// <param name="listOwnerScreenName">The list owner's screen name.</param>
        /// <param name="listSlug">The list slug.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-DELETE-list-subscribers"/>
        public TwitterList UnfollowList(string listOwnerScreenName, string listSlug)
        {
            return WithTweetSharp<TwitterList>(q => q.Lists().Unfollow(listOwnerScreenName, listSlug));
        }

        #endregion

        #region GET list subscribers id

        /// <summary>
        /// Verifies the given user is a follower (subscriber) of the specified list.
        /// </summary>
        /// <param name="listOwnerScreenName">The list owner's screen name.</param>
        /// <param name="listId">The list ID.</param>
        /// <param name="userId">The user ID.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-GET-list-subscribers-id"/>
        public TwitterUser VerifyListFollower(string listOwnerScreenName, int listId, int userId)
        {
            return WithTweetSharp<TwitterUser>(
                q => q.Lists().IsUserFollowerOf(listOwnerScreenName, listId, userId)
                );
        }

        /// <summary>
        /// Verifies the given user is a follower (subscriber) of the specified list.
        /// </summary>
        /// <param name="listOwnerScreenName">The list owner's screen name.</param>
        /// <param name="listSlug">The list slug.</param>
        /// <param name="userId">The user ID.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-GET-list-subscribers-id"/>
        public TwitterUser VerifyListFollower(string listOwnerScreenName, string listSlug, int userId)
        {
            return WithTweetSharp<TwitterUser>(
                q => q.Lists().IsUserFollowerOf(listOwnerScreenName, listSlug, userId)
                );
        }

        #endregion

        #endregion

        #region Direct Message Methods

        #region / (Received)

        /// <summary>
        /// Retrieves direct messages received by the authenticating user.
        /// </summary>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-direct_messages"/>
        public IEnumerable<TwitterDirectMessage> ListDirectMessagesReceived()
        {
            return WithTweetSharp<IEnumerable<TwitterDirectMessage>>(q => q.DirectMessages().Received());
        }

        /// <summary>
        /// Retrieves direct messages received by the authenticating user.
        /// </summary>
        /// <param name="count">The number of results expected.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-direct_messages"/>
        public IEnumerable<TwitterDirectMessage> ListDirectMessagesReceived(int count)
        {
            return WithTweetSharp<IEnumerable<TwitterDirectMessage>>(q => q.DirectMessages().Received().Take(10));
        }

        /// <summary>
        /// Retrieves direct messages received by the authenticating user.
        /// </summary>
        /// <param name="page">The page expected.</param>
        /// <param name="count">The number of results expected.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-direct_messages"/>
        public IEnumerable<TwitterDirectMessage> ListDirectMessagesReceived(int page, int count)
        {
            return
                WithTweetSharp<IEnumerable<TwitterDirectMessage>>(q => q.DirectMessages().Received().Skip(page).Take(10));
        }

        /// <summary>
        /// Retrieves direct messages received by the authenticating user.
        /// List is limited to messages after a given ID.
        /// </summary>
        /// <param name="sinceId">The since ID.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-direct_messages"/>
        public IEnumerable<TwitterDirectMessage> ListDirectMessagesReceivedSince(long sinceId)
        {
            return WithTweetSharp<IEnumerable<TwitterDirectMessage>>(
                q => q.DirectMessages().Received().Since(sinceId)
                );
        }

        /// <summary>
        /// Retrieves direct messages received by the authenticating user.
        /// List is limited to messages after a given ID.
        /// </summary>
        /// <param name="sinceId">The since ID.</param>
        /// <param name="count">The number of results expected.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-direct_messages"/>
        public IEnumerable<TwitterDirectMessage> ListDirectMessagesReceivedSince(long sinceId, int count)
        {
            return WithTweetSharp<IEnumerable<TwitterDirectMessage>>(
                q => q.DirectMessages().Received().Since(sinceId).Take(count)
                );
        }

        /// <summary>
        /// Retrieves direct messages received by the authenticating user.
        /// List is limited to messages after a given ID.
        /// </summary>
        /// <param name="sinceId">The since ID.</param>
        /// <param name="page">The page expected.</param>
        /// <param name="count">The number of results expected.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-direct_messages"/>
        public IEnumerable<TwitterDirectMessage> ListDirectMessagesReceivedSince(long sinceId, int page, int count)
        {
            return WithTweetSharp<IEnumerable<TwitterDirectMessage>>(
                q => q.DirectMessages().Received().Since(sinceId).Skip(page).Take(count)
                );
        }

        /// <summary>
        /// Retrieves direct messages received by the authenticating user.
        /// List is limited to messages before a given ID.
        /// </summary>
        /// <param name="maxId">The max ID.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-direct_messages"/>
        public IEnumerable<TwitterDirectMessage> ListDirectMessagesReceivedBefore(long maxId)
        {
            return WithTweetSharp<IEnumerable<TwitterDirectMessage>>(q => q.DirectMessages().Received().Before(maxId));
        }

        /// <summary>
        /// Retrieves direct messages received by the authenticating user.
        /// List is limited to messages before a given ID.
        /// </summary>
        /// <param name="maxId">The max ID.</param>
        /// <param name="count">The number of results expected.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-direct_messages"/>
        public IEnumerable<TwitterDirectMessage> ListDirectMessagesReceivedBefore(long maxId, int count)
        {
            return
                WithTweetSharp<IEnumerable<TwitterDirectMessage>>(
                                                                     q =>
                                                                     q.DirectMessages().Received().Before(maxId).Take(
                                                                                                                         count));
        }

        /// <summary>
        /// Retrieves direct messages received by the authenticating user.
        /// List is limited to messages before a given ID.
        /// </summary>
        /// <param name="maxId">The max ID.</param>
        /// <param name="page">The page expected.</param>
        /// <param name="count">The number of results expected.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-direct_messages"/>
        public IEnumerable<TwitterDirectMessage> ListDirectMessagesReceivedBefore(long maxId, int page, int count)
        {
            return
                WithTweetSharp<IEnumerable<TwitterDirectMessage>>(
                                                                     q =>
                                                                     q.DirectMessages().Received().Before(maxId).Skip(
                                                                                                                         page)
                                                                         .Take(count));
        }

        #endregion

        #region Sent

        /// <summary>
        /// Retrieves direct messages sent by the authenticating user.
        /// </summary>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method:-direct_messages%C2%A0sent"/>
        public IEnumerable<TwitterDirectMessage> ListDirectMessagesSent()
        {
            return WithTweetSharp<IEnumerable<TwitterDirectMessage>>(q => q.DirectMessages().Sent());
        }

        /// <summary>
        /// Retrieves direct messages sent by the authenticating user.
        /// </summary>
        /// <param name="count">The number of results expected.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method:-direct_messages%C2%A0sent"/>
        public IEnumerable<TwitterDirectMessage> ListDirectMessagesSent(int count)
        {
            return WithTweetSharp<IEnumerable<TwitterDirectMessage>>(q => q.DirectMessages().Sent().Take(10));
        }

        /// <summary>
        /// Retrieves direct messages sent by the authenticating user.
        /// </summary>
        /// <param name="page">The page expected.</param>
        /// <param name="count">The number of results expected.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method:-direct_messages%C2%A0sent"/>
        public IEnumerable<TwitterDirectMessage> ListDirectMessagesSent(int page, int count)
        {
            return WithTweetSharp<IEnumerable<TwitterDirectMessage>>(q => q.DirectMessages().Sent().Skip(page).Take(10));
        }

        /// <summary>
        /// Retrieves direct messages sent by the authenticating user.
        /// List is limited to messages after a given ID.
        /// </summary>
        /// <param name="sinceId">The since ID.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method:-direct_messages%C2%A0sent"/>
        public IEnumerable<TwitterDirectMessage> ListDirectMessagesSentSince(long sinceId)
        {
            return WithTweetSharp<IEnumerable<TwitterDirectMessage>>(
                q => q.DirectMessages().Sent().Since(sinceId)
                );
        }

        /// <summary>
        /// Retrieves direct messages sent by the authenticating user.
        /// List is limited to messages after a given ID.
        /// </summary>
        /// <param name="sinceId">The since ID.</param>
        /// <param name="count">The number of results expected.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method:-direct_messages%C2%A0sent"/>
        public IEnumerable<TwitterDirectMessage> ListDirectMessagesSentSince(long sinceId, int count)
        {
            return WithTweetSharp<IEnumerable<TwitterDirectMessage>>(
                q => q.DirectMessages().Sent().Since(sinceId).Take(count)
                );
        }

        /// <summary>
        /// Retrieves direct messages sent by the authenticating user.
        /// List is limited to messages after a given ID.
        /// </summary>
        /// <param name="sinceId">The since ID.</param>
        /// <param name="page">The page expected.</param>
        /// <param name="count">The number of results expected.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method:-direct_messages%C2%A0sent"/>
        public IEnumerable<TwitterDirectMessage> ListDirectMessagesSentSince(long sinceId, int page, int count)
        {
            return WithTweetSharp<IEnumerable<TwitterDirectMessage>>(
                q => q.DirectMessages().Sent().Since(sinceId).Skip(page).Take(count)
                );
        }

        /// <summary>
        /// Retrieves direct messages sent by the authenticating user.
        /// List is limited to messages before a given ID.
        /// </summary>
        /// <param name="maxId">The max ID.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method:-direct_messages%C2%A0sent"/>
        public IEnumerable<TwitterDirectMessage> ListDirectMessagesSentBefore(long maxId)
        {
            return WithTweetSharp<IEnumerable<TwitterDirectMessage>>(q => q.DirectMessages().Sent().Before(maxId));
        }

        /// <summary>
        /// Retrieves direct messages sent by the authenticating user.
        /// List is limited to messages before a given ID.
        /// </summary>
        /// <param name="maxId">The max ID.</param>
        /// <param name="count">The number of results expected.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method:-direct_messages%C2%A0sent"/>
        public IEnumerable<TwitterDirectMessage> ListDirectMessagesSentBefore(long maxId, int count)
        {
            return WithTweetSharp<IEnumerable<TwitterDirectMessage>>(
                q => q.DirectMessages().Sent().Before(maxId).Take(count)
                );
        }

        /// <summary>
        /// Retrieves direct messages sent by the authenticating user.
        /// List is limited to messages before a given ID.
        /// </summary>
        /// <param name="maxId">The max ID.</param>
        /// <param name="page">The page expected.</param>
        /// <param name="count">The number of results expected.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method:-direct_messages%C2%A0sent"/>
        public IEnumerable<TwitterDirectMessage> ListDirectMessagesSentBefore(long maxId, int page, int count)
        {
            return WithTweetSharp<IEnumerable<TwitterDirectMessage>>(
                q => q.DirectMessages().Sent().Before(maxId).Skip(page).Take(count)
                );
        }

        #endregion

        #region Destroy

        /// <summary>
        /// Deletes the direct message. The direct message must be authored by the authenticated user.
        /// </summary>
        /// <param name="messageId">The message ID.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method:-direct_messages%C2%A0destroy"/>
        public TwitterDirectMessage DeleteDirectMessage(long messageId)
        {
            return WithTweetSharp<TwitterDirectMessage>(q => q.DirectMessages().Destroy(messageId));
        }

        /// <summary>
        /// Deletes the direct message. The direct message must be authored by the authenticated user.
        /// </summary>
        /// <param name="messageId">The message ID.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method:-direct_messages%C2%A0destroy"/>
        public TwitterDirectMessage DeleteDirectMessage(int messageId)
        {
            return WithTweetSharp<TwitterDirectMessage>(q => q.DirectMessages().Destroy(messageId));
        }

        #endregion

        #region Create

        /// <summary>
        /// Sends a direct message from the authenticated user.
        /// </summary>
        /// <param name="recipientUserId">The recipient user ID.</param>
        /// <param name="text">The text.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method:-direct_messages%C2%A0new"/>
        public TwitterDirectMessage SendDirectMessage(int recipientUserId, string text)
        {
            return WithTweetSharp<TwitterDirectMessage>(q => q.DirectMessages().Send(recipientUserId, text));
        }

        /// <summary>
        /// Sends a direct message from the authenticated user.
        /// </summary>
        /// <param name="recipientScreenName">The recipient's screen name.</param>
        /// <param name="text">The text.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method:-direct_messages%C2%A0new"/>
        public TwitterDirectMessage SendDirectMessage(string recipientScreenName, string text)
        {
            return WithTweetSharp<TwitterDirectMessage>(q => q.DirectMessages().Send(recipientScreenName, text));
        }

        #endregion

        #endregion

        #region Friendship Methods

        #region Create

        /// <summary>
        /// Follows the specified user by ID.
        /// </summary>
        /// <param name="userId">The user's ID.</param>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-friendships%C2%A0create"/>
        /// <returns></returns>
        public TwitterUser FollowUser(int userId)
        {
            return WithTweetSharp<TwitterUser>(q => q.Friendships().Befriend(userId));
        }

        /// <summary>
        /// Follows the specified user by screen name.
        /// </summary>
        /// <param name="screenName">The user's screen name.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-friendships%C2%A0create"/>
        public TwitterUser FollowUser(string screenName)
        {
            return WithTweetSharp<TwitterUser>(q => q.Friendships().Befriend(screenName));
        }

        /// <summary>
        /// Gets the list of user IDs for users who have submitted follow requests which have not been acted on.
        /// </summary>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method:-friendships-incoming" />
        public TwitterCursorList<int> GetIncomingFriendRequests()
        {
            return WithTweetSharpAndCursors<int>(q => q.Friendships().Incoming().CreateCursor());
        }

        /// <summary>
        /// Gets a page of userids for users who have submitted follow requests which have not been acted on.
        /// </summary>
        /// <param name="cursor">The cursor.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method:-friendships-incoming" />
        public TwitterCursorList<int> GetIncomingFriendRequests(long cursor)
        {
            return  WithTweetSharpAndCursors<int>(q => q.Friendships().Incoming().GetCursor(cursor));
        }

        /// <summary>
        /// Gets the list of userids to whom the authenticating user has submitted follow requests which have not been acted on.
        /// </summary>
        /// <returns></returns>
        public TwitterCursorList<int> GetOutgoingFriendRequests()
        {
            return WithTweetSharpAndCursors<int>(q => q.Friendships().Outgoing().CreateCursor());
        }

        /// <summary>
        /// Gets a page of userids to whom the authenticating user has submitted follow requests which have not been acted on.
        /// </summary>
        /// <returns></returns>
        public TwitterCursorList<int> GetOutgoingFriendRequests(long cursor)
        {
            return WithTweetSharp<TwitterCursorList<int>>(q => q.Friendships().Outgoing().GetCursor(cursor));
        }

        #endregion

        #region Destroy

        /// <summary>
        /// Unfollows the specified user from the authenticated user's friends.
        /// </summary>
        /// <param name="screenName">The user screen name.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-friendships%C2%A0destroy"/>
        public TwitterUser UnfollowUser(string screenName)
        {
            return WithTweetSharp<TwitterUser>(q => q.Friendships().Destroy(screenName));
        }

        /// <summary>
        /// Unfollows the specified user from the authenticated user's friends.
        /// </summary>
        /// <param name="userId">The user ID.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-friendships%C2%A0destroy"/>
        public TwitterUser UnfollowUser(int userId)
        {
            return WithTweetSharp<TwitterUser>(q => q.Friendships().Destroy(userId));
        }

        #endregion

        #region Exists

        /// <summary>
        /// Returns an indication whether the authenticating user follows the specified user.
        /// </summary>
        /// <param name="followedUserId">The user ID.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-friendships-exists"/>
        public bool VerifyFriendship(int followedUserId)
        {
            var exists = false;

            WithTweetSharp(
                q => q.Friendships().Verify(followedUserId),
                r => exists = r.Response.Equals("true")
                );

            return exists;
        }

        /// <summary>
        /// Returns an indication whether the first specified user follows the other specified user.
        /// </summary>
        /// <param name="followingUserId">The following user ID.</param>
        /// <param name="followedUserId">The followed user ID.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-friendships-exists"/>
        public bool VerifyFriendship(int followingUserId, int followedUserId)
        {
            var exists = false;

            WithTweetSharp(
                q => q.Friendships().Verify(followingUserId).IsFriendsWith(followedUserId),
                r => exists = r.Response.Equals("true")
                );

            return exists;
        }

        #endregion

        #region Show

        /// <summary>
        /// Returns detailed information about the relationship between two users.
        /// </summary>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-friendships-show"/>
        /// <returns></returns>
        public TwitterFriendship GetFriendshipInfo(string sourceUserScreenName, string targetUserScreenName)
        {
            return
                WithTweetSharp<TwitterFriendship>(q => q.Friendships().Show(sourceUserScreenName, targetUserScreenName));
        }

        /// <summary>
        /// Returns detailed information about the relationship between two users.
        /// </summary>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-friendships-show"/>
        /// <returns></returns>
        public TwitterFriendship GetFriendshipInfo(int sourceUserId, int targetUserId)
        {
            return WithTweetSharp<TwitterFriendship>(q => q.Friendships().Show(sourceUserId, targetUserId));
        }

        #endregion

        #endregion

        #region Social Graph Methods

        #region Friends

        /// <summary>
        /// Lists the authenticating user's friends' IDs.
        /// </summary>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-friends%C2%A0ids"/>
        /// <returns></returns>
        public TwitterCursorList<int> ListFriendIds()
        {
            return WithTweetSharpAndCursors<int>(q => q.SocialGraph().Ids().ForFriends().CreateCursor());
        }

        /// <summary>
        /// Lists the authenticating user's friends' IDs.
        /// </summary>
        /// <param name="cursor">The cursor.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-friends%C2%A0ids"/>
        public TwitterCursorList<int> ListFriendIds(long cursor)
        {
            return WithTweetSharpAndCursors<int>(q => q.SocialGraph().Ids().ForFriends().GetCursor(cursor));
        }

        /// <summary>
        /// Lists the specified user's friends' IDs.
        /// </summary>
        /// <param name="userId">The user ID.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-friends%C2%A0ids"/>
        public TwitterCursorList<int> ListFriendIdsFor(int userId)
        {
            return WithTweetSharpAndCursors<int>(
                q => q.SocialGraph().Ids().ForFriendsOf(userId).CreateCursor()
                );
        }

        /// <summary>
        /// Lists the specified user's friends' IDs.
        /// </summary>
        /// <param name="userId">The user ID.</param>
        /// <param name="cursor">The cursor.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-friends%C2%A0ids"/>
        public TwitterCursorList<int> ListFriendIdsFor(int userId, long cursor)
        {
            return WithTweetSharpAndCursors<int>(
                q => q.SocialGraph().Ids().ForFriendsOf(userId).GetCursor(cursor)
                );
        }

        /// <summary>
        /// Lists the specified user's friends' IDs.
        /// </summary>
        /// <param name="userScreenName">The user screen name.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-friends%C2%A0ids"/>
        public TwitterCursorList<int> ListFriendIdsFor(string userScreenName)
        {
            return WithTweetSharpAndCursors<int>(
                q => q.SocialGraph().Ids().ForFriendsOf(userScreenName).CreateCursor()
                );
        }

        /// <summary>
        /// Lists the specified user's friends' IDs.
        /// </summary>
        /// <param name="userScreenName">The user screen name.</param>
        /// <param name="cursor">The cursor.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-friends%C2%A0ids"/>
        public TwitterCursorList<int> ListFriendIdsFor(string userScreenName, long cursor)
        {
            return WithTweetSharpAndCursors<int>(
                q => q.SocialGraph().Ids().ForFriendsOf(userScreenName).GetCursor(cursor)
                );
        }

        #endregion

        #region Followers

        /// <summary>
        /// Lists the authenticating user's followers' IDs.
        /// </summary>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-followers%C2%A0ids"/>
        /// <returns></returns>
        public TwitterCursorList<int> ListFollowerIds()
        {
            return WithTweetSharpAndCursors<int>(q => q.SocialGraph().Ids().ForFollowers().CreateCursor());
        }

        /// <summary>
        /// Lists the authenticating user's followers' IDs.
        /// </summary>
        /// <param name="cursor">The cursor.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-followers%C2%A0ids"/>
        public TwitterCursorList<int> ListFollowerIds(long cursor)
        {
            return WithTweetSharpAndCursors<int>(q => q.SocialGraph().Ids().ForFollowers().GetCursor(cursor));
        }

        /// <summary>
        /// Lists the specified user's followers' IDs.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-followers%C2%A0ids"/>
        public TwitterCursorList<int> ListFollowerIdsFor(int userId)
        {
            return WithTweetSharpAndCursors<int>(q => q.SocialGraph().Ids().ForFollowersOf(userId).CreateCursor());
        }

        /// <summary>
        /// Lists the specified user's followers' IDs.
        /// </summary>
        /// <param name="userScreenName">The user screen name.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-followers%C2%A0ids"/>
        public TwitterCursorList<int> ListFollowerIdsFor(string userScreenName)
        {
            return WithTweetSharpAndCursors<int>(
                q => q.SocialGraph().Ids().ForFollowersOf(userScreenName).CreateCursor()
                );
        }

        /// <summary>
        /// Lists the specified user's followers' IDs.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <param name="cursor">The cursor.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-followers%C2%A0ids"/>
        public TwitterCursorList<int> ListFollowerIdsFor(int userId, long cursor)
        {
            return WithTweetSharpAndCursors<int>(q => q.SocialGraph().Ids().ForFollowersOf(userId).GetCursor(cursor));
        }

        /// <summary>
        /// Lists the specified user's followers' IDs.
        /// </summary>
        /// <param name="userScreenName">The user screen name.</param>
        /// <param name="cursor">The cursor.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-followers%C2%A0ids"/>
        public TwitterCursorList<int> ListFollowerIdsFor(string userScreenName, long cursor)
        {
            return WithTweetSharpAndCursors<int>(
                q => q.SocialGraph().Ids().ForFollowersOf(userScreenName).GetCursor(cursor)
                );
        }

        #endregion

        #endregion

        #region Account Methods

        /// <summary>
        /// Verifies the credentials provided with the service call to ensure a user 
        /// will authenticate against the API.
        /// </summary>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-account%C2%A0verify_credentials"></seealso>
        /// <returns></returns>
        public TwitterUser VerifyCredentials()
        {
            return WithTweetSharp<TwitterUser>(q => q.Account().VerifyCredentials());
        }

        /// <summary>
        /// Gets the <see cref="TwitterRateLimitStatus" /> for the authenticated user.
        /// </summary>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-account%C2%A0rate_limit_status"/>
        /// <returns></returns>
        public TwitterRateLimitStatus GetRateLimitStatus()
        {
            return WithTweetSharp<TwitterRateLimitStatus>(q => q.Account().GetRateLimitStatus());
        }

        /// <summary>
        /// Ends the session of the authenticating user, returning a null cookie.  
        /// Use this method to sign users out of client-facing applications like widgets.
        /// If this method returns a null <see cref="TwitterError"/> instance, 
        /// the session was located and ended successfully.
        /// </summary>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-account%C2%A0end_session"/>
        /// <returns></returns>
        public TwitterError EndSession()
        {
            return WithTweetSharp<TwitterError>(q => q.Account().EndSession());
        }

        /// <summary>
        /// Updates the authenticated user's tweet delivery device.
        /// </summary>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-account%C2%A0update_delivery_device"/>
        /// <returns></returns>
        public TwitterUser UpdateDeliveryDevice(TwitterDeliveryDevice device)
        {
            return WithTweetSharp<TwitterUser>(q => q.Account().UpdateDeliveryDeviceTo(device));
        }

        /// <summary>
        /// Updates the authenticated user's profile colors.
        /// The colors are inputted as hexademical colors with or without a leading '#'.
        /// </summary>
        /// <param name="backgroundColor">Color of the background.</param>
        /// <param name="textColor">Color of the text.</param>
        /// <param name="linkColor">Color of the link.</param>
        /// <param name="sidebarFillColor">Color of the sidebar fill.</param>
        /// <param name="sidebarBorderColor">Color of the sidebar border.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-account%C2%A0update_profile_colors"/>
        public TwitterUser UpdateProfileColors(string backgroundColor, string textColor, string linkColor,
                                               string sidebarFillColor, string sidebarBorderColor)
        {
            return WithTweetSharp<TwitterUser>(q => q.Account().UpdateProfileColors()
                                                        .UpdateProfileBackgroundColor(backgroundColor)
                                                        .UpdateProfileTextColor(textColor)
                                                        .UpdateProfileLinkColor(linkColor)
                                                        .UpdateProfileSidebarFillColor(sidebarFillColor)
                                                        .UpdateProfileSidebarBorderColor(sidebarBorderColor)
                );
        }

        /// <summary>
        /// Updates the authenticated user's profile colors.
        /// The colors are inputted as hexademical colors with or without a leading '#'.
        /// </summary>
        /// <param name="backgroundColor">Color of the background.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-account%C2%A0update_profile_colors"/>
        public TwitterUser UpdateProfileColors(string backgroundColor)
        {
            return UpdateProfileColors(backgroundColor, null, null, null, null);
        }

        /// <summary>
        /// Updates the authenticated user's profile colors.
        /// The colors are inputted as hexademical colors with or without a leading '#'.
        /// </summary>
        /// <param name="backgroundColor">Color of the background.</param>
        /// <param name="textColor">Color of the text.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-account%C2%A0update_profile_colors"/>
        public TwitterUser UpdateProfileColors(string backgroundColor, string textColor)
        {
            return UpdateProfileColors(backgroundColor, textColor, null, null, null);
        }

        /// <summary>
        /// Updates the authenticated user's profile colors.
        /// The colors are inputted as hexademical colors with or without a leading '#'.
        /// </summary>
        /// <param name="backgroundColor">Color of the background.</param>
        /// <param name="textColor">Color of the text.</param>
        /// <param name="linkColor">Color of the link.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-account%C2%A0update_profile_colors"/>
        public TwitterUser UpdateProfileColors(string backgroundColor, string textColor, string linkColor)
        {
            return UpdateProfileColors(backgroundColor, textColor, linkColor, null, null);
        }

        /// <summary>
        /// Updates the authenticated user's profile colors.
        /// The colors are inputted as hexademical colors with or without a leading '#'.
        /// </summary>
        /// <param name="backgroundColor">Color of the background.</param>
        /// <param name="textColor">Color of the text.</param>
        /// <param name="linkColor">Color of the link.</param>
        /// <param name="sidebarFillColor">Color of the sidebar fill.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-account%C2%A0update_profile_colors"/>
        public TwitterUser UpdateProfileColors(string backgroundColor, string textColor, string linkColor,
                                               string sidebarFillColor)
        {
            return UpdateProfileColors(backgroundColor, textColor, linkColor, sidebarFillColor, null);
        }

        /// <summary>
        /// Updates the authenticating user's profile image.
        /// This method expects raw multipart data, not a URL to an image.
        /// Must be a valid GIF, JPG, or PNG image of less than 700 kilobytes in size.
        /// Images with width larger than 500 pixels will be scaled down.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-account%C2%A0update_profile_image"/>
        public TwitterUser UpdateProfileImage(string path)
        {
            return WithTweetSharp<TwitterUser>(q => q.Account().UpdateProfileImage(path));
        }

        /// <summary>
        /// Updates the authenticated user's profile background image.
        /// This method expects raw multipart data, not a URL to an image.
        /// Must be a valid GIF, JPG, or PNG image of less than 800 kilobytes in size.
        /// Images with width larger than 2048 pixels will be scaled down.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-account%C2%A0update_profile_background_image"/>
        public TwitterUser UpdateProfileBackgroundImage(string path)
        {
            return WithTweetSharp<TwitterUser>(q => q.Account().UpdateProfileBackgroundImage(path));
        }

        /// <summary>
        /// Updates the authenticated user's profile info.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="description">The description.</param>
        /// <param name="email">The email.</param>
        /// <param name="url">The URL.</param>
        /// <param name="location">The location.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-account%C2%A0update_profile"/>
        public TwitterUser UpdateProfile(string name, string description, string email, string url, string location)
        {
            return WithTweetSharp<TwitterUser>(q => q.Account().UpdateProfile()
                                                        .UpdateName(name)
                                                        .UpdateDescription(name)
                                                        .UpdateEmail(email)
                                                        .UpdateUrl(url)
                                                        .UpdateLocation(location));
        }

        #endregion

        #region Favorite Methods

        /// <summary>
        /// Retrieves the authenticated user's favorited tweets.
        /// </summary>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-favorites"/>
        public IEnumerable<TwitterStatus> ListFavoriteTweets()
        {
            return WithTweetSharp<IEnumerable<TwitterStatus>>(q => q.Favorites().GetFavorites());
        }

        /// <summary>
        /// Retrieves the authenticated user's favorited tweets.
        /// </summary>
        /// <param name="page">The page expected.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-favorites"/>
        public IEnumerable<TwitterStatus> ListFavoriteTweets(int page)
        {
            return WithTweetSharp<IEnumerable<TwitterStatus>>(q => q.Favorites().GetFavorites().Skip(page));
        }

        /// <summary>
        /// Retrieves the authenticated user's favorited tweets.
        /// </summary>
        /// <param name="page">The page expected.</param>
        /// <param name="count">The number of results expected.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-favorites"/>
        public IEnumerable<TwitterStatus> ListFavoriteTweets(int page, int count)
        {
            return WithTweetSharp<IEnumerable<TwitterStatus>>(q => q.Favorites().GetFavorites().Skip(page).Take(count));
        }

        /// <summary>
        /// Retrieves the specified user's favorited tweets.
        /// This method requires authentication.
        /// </summary>
        /// <param name="userId">The user ID.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-favorites"/>
        public IEnumerable<TwitterStatus> ListFavoriteTweetsFor(int userId)
        {
            return WithTweetSharp<IEnumerable<TwitterStatus>>(q => q.Favorites().GetFavoritesFor(userId));
        }

        /// <summary>
        /// Retrieves the specified user's favorited tweets.
        /// This method requires authentication.
        /// </summary>
        /// <param name="userId">The user ID.</param>
        /// <param name="page">The page expected.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-favorites"/>
        public IEnumerable<TwitterStatus> ListFavoriteTweetsFor(int userId, int page)
        {
            return WithTweetSharp<IEnumerable<TwitterStatus>>(q => q.Favorites().GetFavoritesFor(userId).Skip(page));
        }

        /// <summary>
        /// Retrieves the specified user's favorited tweets.
        /// This method requires authentication.
        /// </summary>
        /// <param name="userId">The user ID.</param>
        /// <param name="page">The page expected.</param>
        /// <param name="count">The number of results expected.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-favorites"/>
        public IEnumerable<TwitterStatus> ListFavoriteTweetsFor(int userId, int page, int count)
        {
            return WithTweetSharp<IEnumerable<TwitterStatus>>(
                q => q.Favorites().GetFavoritesFor(userId).Skip(page).Take(count)
                );
        }

        /// <summary>
        /// Retrieves the specified user's favorited tweets.
        /// This method requires authentication.
        /// </summary>
        /// <param name="userScreenName">The user screen name.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-favorites"/>
        public IEnumerable<TwitterStatus> ListFavoriteTweetsFor(string userScreenName)
        {
            return WithTweetSharp<IEnumerable<TwitterStatus>>(q => q.Favorites().GetFavoritesFor(userScreenName));
        }

        /// <summary>
        /// Retrieves the specified user's favorited tweets.
        /// This method requires authentication.
        /// </summary>
        /// <param name="userScreenName">The user screen name.</param>
        /// <param name="page">The page expected.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-favorites"/>
        public IEnumerable<TwitterStatus> ListFavoriteTweetsFor(string userScreenName, int page)
        {
            return WithTweetSharp<IEnumerable<TwitterStatus>>(
                q => q.Favorites().GetFavoritesFor(userScreenName).Skip(page)
                );
        }

        /// <summary>
        /// Retrieves the specified user's favorited tweets.
        /// This method requires authentication.
        /// </summary>
        /// <param name="userScreenName">The user screen name.</param>
        /// <param name="page">The page expected.</param>
        /// <param name="count">The count.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-favorites"/>
        public IEnumerable<TwitterStatus> ListFavoriteTweetsFor(string userScreenName, int page, int count)
        {
            return WithTweetSharp<IEnumerable<TwitterStatus>>(
                q => q.Favorites().GetFavoritesFor(userScreenName).Skip(page).Take(count)
                );
        }

        /// <summary>
        /// Favorites a tweet on behalf of the authenticated user.
        /// </summary>
        /// <param name="statusId">The status ID.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-favorites%C2%A0create"/>
        public TwitterStatus FavoriteTweet(long statusId)
        {
            return WithTweetSharp<TwitterStatus>(q => q.Favorites().Favorite(statusId));
        }

        /// <summary>
        /// Favorites a tweet on behalf of the authenticated user.
        /// </summary>
        /// <param name="status">The status.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-favorites%C2%A0create"/>
        public TwitterStatus FavoriteTweet(TwitterStatus status)
        {
            return WithTweetSharp<TwitterStatus>(q => q.Favorites().Favorite(status));
        }

        /// <summary>
        /// Unfavorites a tweet on behalf of the authenticated user.
        /// </summary>
        /// <param name="statusId">The status ID.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-favorites%C2%A0destroy"/>
        public TwitterStatus UnfavoriteTweet(long statusId)
        {
            return WithTweetSharp<TwitterStatus>(q => q.Favorites().Unfavorite(statusId));
        }

        /// <summary>
        /// Unfavorites a tweet on behalf of the authenticated user.
        /// </summary>
        /// <param name="status">The status.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-favorites%C2%A0destroy"/>
        public TwitterStatus UnfavoriteTweet(TwitterStatus status)
        {
            return WithTweetSharp<TwitterStatus>(q => q.Favorites().Unfavorite(status));
        }

        #endregion

        #region Notification Methods

        #region Follow

        /// <summary>
        /// Follows the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-notifications%C2%A0follow" />
        /// <returns></returns>
        public TwitterUser FollowUserNotifications(TwitterUser user)
        {
            return WithTweetSharp<TwitterUser>(q => q.Notifications().Follow(user.Id));
        }

        /// <summary>
        /// Follows the specified user by screen name.
        /// </summary>
        /// <param name="screenName">The user's screen name.</param>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-notifications%C2%A0follow" />
        /// <returns></returns>
        public TwitterUser FollowUserNotifications(string screenName)
        {
            return WithTweetSharp<TwitterUser>(q => q.Notifications().Follow(screenName));
        }

        /// <summary>
        /// Follows the specified user by ID.
        /// </summary>
        /// <param name="userId">The user ID.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-notifications%C2%A0follow"/>
        public TwitterUser FollowUserNotifications(int userId)
        {
            return WithTweetSharp<TwitterUser>(q => q.Notifications().Follow(userId));
        }

        #endregion

        #region Leave

        /// <summary>
        /// Disables notifications for updates from the specified user to the authenticating user.
        /// Returns the specified user when successful.
        /// </summary>
        /// <param name="userId">The user ID.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-notifications%C2%A0leave"/>
        public TwitterUser UnfollowUserNotifications(int userId)
        {
            return WithTweetSharp<TwitterUser>(q => q.Notifications().Leave(userId));
        }

        /// <summary>
        /// Disables notifications for updates from the specified user to the authenticating user.
        /// Returns the specified user when successful.
        /// </summary>
        /// <param name="userScreenName">The user screen name.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-notifications%C2%A0leave"/>
        public TwitterUser UnfollowUserNotifications(string userScreenName)
        {
            return WithTweetSharp<TwitterUser>(q => q.Notifications().Leave(userScreenName));
        }

        #endregion

        #endregion

        #region Block Methods

        #region Create

        /// <summary>
        /// Blocks the specified user on behalf of the authenticated user.
        /// </summary>
        /// <param name="userId">The user ID.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method:-blocks%C2%A0create"/>
        public TwitterUser BlockUser(int userId)
        {
            return WithTweetSharp<TwitterUser>(q => q.Blocking().Block(userId));
        }

        /// <summary>
        /// Blocks the specified user on behalf of the authenticated user.
        /// </summary>
        /// <param name="userScreenName">The user screen name.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method:-blocks%C2%A0create"/>
        public TwitterUser BlockUser(string userScreenName)
        {
            return WithTweetSharp<TwitterUser>(q => q.Blocking().Block(userScreenName));
        }

        #endregion

        #region Destroy

        /// <summary>
        /// Unblocks the specified user on behalf of the authenticated user.
        /// </summary>
        /// <param name="userId">The user ID.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method:-blocks%C2%A0destroy"/>
        public TwitterUser UnblockUser(int userId)
        {
            return WithTweetSharp<TwitterUser>(q => q.Blocking().Unblock(userId));
        }

        /// <summary>
        /// Unblocks the specified user on behalf of the authenticated user.
        /// </summary>
        /// <param name="userScreenName">The user screen name.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method:-blocks%C2%A0destroy"/>
        public TwitterUser UnblockUser(string userScreenName)
        {
            return WithTweetSharp<TwitterUser>(q => q.Blocking().Unblock(userScreenName));
        }

        #endregion

        #region Exists

        /// <summary>
        /// Verifies whether the authenticated user is blocking the specified user.
        /// Returns the <seealso cref="TwitterUser" /> of the blocked user if blocking, 
        /// and an error if the block is not established.
        /// </summary>
        /// <param name="userId"></param>
        /// <seealso href="http://apiwiki.twitter.com/Twitter+REST+API+Method%3A-blocks-exists" /> 
        /// <returns></returns>
        public TwitterUser VerifyBlocking(int userId)
        {
            return WithTweetSharp<TwitterUser>(q => q.Blocking().Exists(userId));
        }

        /// <summary>
        /// Verifies whether the authenticated user is blocking the specified user.
        /// Returns the <seealso cref="TwitterUser" /> of the blocked user if blocking, 
        /// and an error if the block is not established.
        /// </summary>
        /// <param name="userScreenName">The user screen name.</param>
        /// <seealso href="http://apiwiki.twitter.com/Twitter+REST+API+Method%3A-blocks-exists" /> 
        /// <returns></returns>
        public TwitterUser VerifyBlocking(string userScreenName)
        {
            return WithTweetSharp<TwitterUser>(q => q.Blocking().Exists(userScreenName));
        }

        #endregion

        #region Blocking

        /// <summary>
        /// Retrieves a list of all users blocked by the authenticated user.
        /// </summary>
        /// <seealso href="http://apiwiki.twitter.com/Twitter+REST+API+Method%3A-blocks-blocking" />
        /// <returns></returns>
        public IEnumerable<TwitterUser> ListBlockedUsers()
        {
            return WithTweetSharp<IEnumerable<TwitterUser>>(q => q.Blocking().ListUsers());
        }

        /// <summary>
        /// Retrieves a list of all users blocked by the authenticated user.
        /// </summary>
        /// <param name="page">The page expected.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter+REST+API+Method%3A-blocks-blocking"/>
        public IEnumerable<TwitterUser> ListBlockedUsers(int page)
        {
            return WithTweetSharp<IEnumerable<TwitterUser>>(q => q.Blocking().ListUsers().Skip(page));
        }

        /// <summary>
        /// Retrieves a list of all user IDs blocked by the authenticated user.
        /// </summary>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-blocks-blocking-ids"/>
        public IEnumerable<int> ListBlockedUserIds()
        {
            return WithTweetSharp<IEnumerable<int>>(q => q.Blocking().ListIds());
        }

        #endregion

        #endregion

        #region Spam Reporting Methods

        #region Report Spam

        /// <summary>
        /// Reports the screen name as a spammer.
        /// </summary>
        /// <param name="userScreenName">The spammer's screen name.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-report_spam"/>
        public TwitterUser ReportSpamFrom(string userScreenName)
        {
            return WithTweetSharp<TwitterUser>(q => q.ReportSpam().ReportSpammer(userScreenName));
        }

        /// <summary>
        /// Reports the user ID as a spammer.
        /// </summary>
        /// <param name="userId">The user ID.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-report_spam"/>
        public TwitterUser ReportSpamFrom(int userId)
        {
            return WithTweetSharp<TwitterUser>(q => q.ReportSpam().ReportSpammer(userId));
        }

        #endregion

        #endregion

        #region Saved Searches Methods

        /// <summary>
        /// Lists the saved searches for the authenticating user.
        /// </summary>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-saved_searches"/>
        public IEnumerable<TwitterSavedSearch> ListSavedSearches()
        {
            return WithTweetSharp<IEnumerable<TwitterSavedSearch>>(q => q.SavedSearches().List());
        }

        #region Show

        /// <summary>
        /// Gets the specified saved search.
        /// </summary> 
        /// <param name="searchId">The saved search ID.</param>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-saved_searches-show" />
        /// <returns></returns>
        public TwitterSavedSearch GetSavedSearch(int searchId)
        {
            return WithTweetSharp<TwitterSavedSearch>(q => q.SavedSearches().Show(searchId));
        }
        
        #endregion

        #region Create

        /// <summary>
        /// Creates a new saved search on behalf of the authenticated user.
        /// </summary>
        /// <param name="query">The saved search query.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-saved_searches-create"/>
        public TwitterSavedSearch CreateSavedSearch(string query)
        {
            return WithTweetSharp<TwitterSavedSearch>(q => q.SavedSearches().Create(query));
        }

        #endregion

        #region Destroy

        /// <summary>
        /// Deletes the specified saved search on behalf of the authenticated user.
        /// </summary>
        /// <param name="searchId">The search ID.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-saved_searches-destroy"/>
        public TwitterSavedSearch DeleteSavedSearch(int searchId)
        {
            return WithTweetSharp<TwitterSavedSearch>(q => q.SavedSearches().Delete(searchId));
        }

        #endregion

        #endregion

        #region OAuth Methods

        #region Request Token

        /// <summary>
        /// Obtains an unauthorized OAuth request token for the given consumer application.
        /// This method assumes OAuth consumer info is part of <code>TwitterClientInfo</code>.
        /// </summary>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-oauth-request_token"/>
        public OAuthToken GetRequestToken()
        {
            OAuthToken token = null;
            
            WithTweetSharp(
                q => q.Authentication.GetRequestToken(),
                r => token = r.AsToken()
                );

            return token;
        }

        /// <summary>
        /// Obtains an unauthorized OAuth request token for the given consumer application.
        /// </summary>
        /// <param name="consumerKey">The consumer key.</param>
        /// <param name="consumerSecret">The consumer secret.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-oauth-request_token"/>
        public OAuthToken GetRequestToken(string consumerKey, string consumerSecret)
        {
            OAuthToken token = null;

            WithTweetSharp(
                q => q.Authentication.GetRequestToken(consumerKey, consumerSecret),
                r => token = r.AsToken()
                );

            return token;
        }

        #endregion

        #region Authorize

        /// <summary>
        /// Gets the OAuth authorization URL to redirect users to to authorize the
        /// use of their credentials for your consumer application.
        /// </summary>
        /// <param name="requestToken">The request token.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-oauth-authorize"/>
        public string GetAuthorizationUrl(OAuthToken requestToken)
        {
            return GetAuthenticatedQuery().Authentication.GetAuthorizationUrl(requestToken.Token);
        }

        /// <summary>
        /// Gets the OAuth authorization URL to redirect users to to authorize the
        /// use of their credentials for your consumer application.
        /// </summary>
        /// <param name="requestToken">The request token.</param>
        /// <param name="callback">The callback.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-oauth-authorize"/>
        public string GetAuthorizationUrl(OAuthToken requestToken, string callback)
        {
            return GetAuthenticatedQuery().Authentication.GetAuthorizationUrl(requestToken.Token, callback);
        }

        /// <summary>
        /// Gets the OAuth authorization URL to redirect users to to authorize the
        /// use of their credentials for your consumer application, and then
        /// calls the OS browser to navigate to that URL automatically.
        /// This method assumes OAuth consumer info is part of <code>TwitterClientInfo</code>.
        /// </summary>
        /// <param name="requestToken">The request token.</param>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-oauth-authorize"/>
        public void AuthorizeDesktop(OAuthToken requestToken)
        {
            WithTweetSharp(q => q.Authentication.AuthorizeDesktop(requestToken.Token));
        }

        /// <summary>
        /// Gets the OAuth authorization URL to redirect users to to authorize the
        /// use of their credentials for your consumer application, and then
        /// calls the OS browser to navigate to that URL automatically.
        /// </summary>
        /// <param name="consumerKey">The consumer key.</param>
        /// <param name="consumerSecret">The consumer secret.</param>
        /// <param name="requestToken">The request token.</param>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-oauth-authorize"/>
        public void AuthorizeDesktop(string consumerKey, string consumerSecret, OAuthToken requestToken)
        {
            WithTweetSharp(q => q.Authentication.AuthorizeDesktop(consumerKey, consumerSecret, requestToken.Token));
        }

        #endregion

        #region Authenticate

        /// <summary>
        /// Gets the OAuth authorization URL to redirect users to to "Sign In With Twitter"
        /// through your consumer application.
        /// </summary>
        /// <param name="requestToken">The request token.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-oauth-authorize"/>
        public string GetAuthenticationUrl(OAuthToken requestToken)
        {
            return GetAuthenticatedQuery().Authentication.GetAuthenticationUrl(requestToken.Token);
        }

        /// <summary>
        /// Gets the OAuth authorization URL to redirect users to to "Sign In With Twitter"
        /// through your consumer application.
        /// </summary>
        /// <param name="requestToken">The request token.</param>
        /// <param name="callback">The callback.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-oauth-authorize"/>
        public string GetAuthenticationUrl(OAuthToken requestToken, string callback)
        {
            return GetAuthenticatedQuery().Authentication.GetAuthenticationUrl(requestToken.Token, callback);
        }

        /// <summary>
        /// Gets the OAuth authorization URL to redirect users to to "Sign In With Twitter"
        /// through your consumer application, and then
        /// calls the OS browser to navigate to that URL automatically.
        /// The callback is always omitted for OOB.
        /// This method assumes OAuth consumer info is part of <code>TwitterClientInfo</code>.
        /// </summary>
        /// <param name="requestToken">The request token.</param>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-oauth-authorize"/>
        public void AuthenticateDesktop(OAuthToken requestToken)
        {
            WithTweetSharp(q => q.Authentication.AuthenticateDesktop(requestToken.Token, "OOB"));
        }

        /// <summary>
        /// Gets the OAuth authorization URL to redirect users to to "Sign In With Twitter"
        /// through your consumer application, and then
        /// calls the OS browser to navigate to that URL automatically.
        /// The callback is always omitted for OOB.
        /// </summary>
        /// <param name="consumerKey">The consumer key.</param>
        /// <param name="consumerSecret">The consumer secret.</param>
        /// <param name="requestToken">The request token.</param>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-oauth-authorize"/>
        public void AuthenticateDesktop(string consumerKey, string consumerSecret, OAuthToken requestToken)
        {
            WithTweetSharp(q => q.Authentication.AuthenticateDesktop(consumerKey, consumerSecret, requestToken.Token, "OOB"));
        }

        #endregion

        #region Access Token

        /// <summary>
        /// Exchanges an authorized OAuth request token for an authorized access token.
        /// The access token is used with the <code>AuthenticateWith</code> methods
        /// to authenticate on behalf of a user when accessing protected API methods.
        /// This method assumes OAuth consumer info is part of <code>TwitterClientInfo</code>.
        /// </summary>
        /// <param name="requestToken">The request token.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-oauth-access_token"/>
        public OAuthToken GetAccessToken(OAuthToken requestToken)
        {
            OAuthToken token = null;

            WithTweetSharp(
                q => q.Authentication.GetAccessToken(requestToken.Token),
                r => token = r.AsToken()
                );

            return token;
        }

        /// <summary>
        /// Exchanges an authorized OAuth request token for an authorized access token.
        /// The access token is used with the <code>AuthenticateWith</code> methods
        /// to authenticate on behalf of a user when accessing protected API methods.
        /// </summary>
        /// <param name="consumerKey">The consumer key.</param>
        /// <param name="consumerSecret">The consumer secret.</param>
        /// <param name="requestToken">The request token.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-oauth-access_token"/>
        public OAuthToken GetAccessToken(string consumerKey, string consumerSecret, OAuthToken requestToken)
        {
            OAuthToken token = null;

            WithTweetSharp(
                q => q.Authentication.GetAccessToken(consumerKey, consumerSecret, requestToken.Token),
                r => token = r.AsToken()
                );

            return token;
        }

        /// <summary>
        /// Exchanges an authorized OAuth request token for an authorized access token.
        /// The access token is used with the <code>AuthenticateWith</code> methods
        /// to authenticate on behalf of a user when accessing protected API methods.
        /// This method assumes OAuth consumer info is part of <code>TwitterClientInfo</code>.
        /// </summary>
        /// <param name="requestToken">The request token.</param>
        /// <param name="pin">The PIN verifier provided during desktop authorization.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-oauth-access_token"/>
        public OAuthToken GetAccessToken(OAuthToken requestToken, string pin)
        {
            OAuthToken token = null;

            WithTweetSharp(
                q => q.Authentication.GetAccessToken(requestToken.Token, pin),
                r => token = r.AsToken()
                );

            return token;
        }

        /// <summary>
        /// Exchanges an authorized OAuth request token for an authorized access token.
        /// The access token is used with the <code>AuthenticateWith</code> methods
        /// to authenticate on behalf of a user when accessing protected API methods.
        /// </summary>
        /// <param name="consumerKey">The consumer key.</param>
        /// <param name="consumerSecret">The consumer secret.</param>
        /// <param name="requestToken">The request token.</param>
        /// <param name="pin">The PIN verifier provided during desktop authorization.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-oauth-access_token"/>
        public OAuthToken GetAccessToken(string consumerKey, string consumerSecret, OAuthToken requestToken, string pin)
        {
            OAuthToken token = null;

            WithTweetSharp(
                q => q.Authentication.GetAccessToken(consumerKey, consumerSecret, requestToken.Token, pin),
                r => token = r.AsToken()
                );

            return token;
        }

        #endregion

        #endregion

        #region Local Trends Methods

        /// <summary>
        /// Retrieves a list of all Where On Earth (WOE) locations that support local trends.
        /// </summary>
        /// <returns></returns>
        /// <seealso href="http://twitterapi.pbworks.com/Twitter-REST-API-Method%3A-trends-location"/>
        public IEnumerable<WhereOnEarthLocation> ListLocalTrendLocations()
        {
            return WithTweetSharp<IEnumerable<WhereOnEarthLocation>>(q => q.Trends().GetAvailable());
        }

        /// <summary>
        /// Retrieves a list of all Where On Earth (WOE) locations
        /// that map to the given <see cref="TwitterGeoLocation"/>.
        /// </summary>
        /// <param name="orderByLocation">The order by location.</param>
        /// <returns></returns>
        /// <seealso href="http://twitterapi.pbworks.com/Twitter-REST-API-Method%3A-trends-location"/>
        public IEnumerable<WhereOnEarthLocation> ListLocalTrendLocations(TwitterGeoLocation orderByLocation)
        {
            return WithTweetSharp<IEnumerable<WhereOnEarthLocation>>(q => q.Trends().GetAvailable().OrderBy(orderByLocation)
                );
        }

        /// <summary>
        /// Searches local trends for a specific Where On Earth (WOE) ID.
        /// </summary>
        /// <param name="woeId">The WOE ID.</param>
        /// <seealso href="http://twitterapi.pbworks.com/Twitter-REST-API-Method%3A-trends-location"/>
        /// <returns></returns>
        public TwitterLocalTrends SearchLocalTrends(long woeId)
        {
            return WithTweetSharp<TwitterLocalTrends>(q => q.Trends().ByLocation(woeId));
        }

        #endregion

        #region Help Methods

        #region Test

        /// <summary>
        /// Determines whether Twitter is currently down for maintenance or heavy load.
        /// </summary>
        /// <returns>
        /// 	<c>true</c> if Twitter is down; otherwise, <c>false</c>.
        /// </returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-help%C2%A0test"/>
        public bool IsTwitterDown()
        {
            var test = WithTweetSharp<string>(q => q.Help().TestService().AsXml());
            return !test.Contains("true");
        }

        /// <summary>
        /// Determines whether Twitter is currently operational.
        /// </summary>
        /// <returns>
        /// 	<c>true</c> if Twitter is up; otherwise, <c>false</c>.
        /// </returns>
        /// <seealso href="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-help%C2%A0test"/>
        public bool IsTwitterUp()
        {
            var test = WithTweetSharp<string>(q => q.Help().TestService().AsXml());
            return test.Contains("true");
        }

        #endregion

        #endregion
#endif
    }
}