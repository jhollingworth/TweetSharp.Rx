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

using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using TweetSharp.Twitter.Core;
using TweetSharp.Twitter.Extensions;
using TweetSharp.Twitter.Model;
using TweetSharp.Twitter.Fluent;

namespace TweetSharp.Twitter.Service
{
    partial class TwitterService
    {
        #region Filter
#if !WindowsPhone

        /// <summary>
        /// Asynchronously streams from the Twitter 'Filter' stream.
        /// </summary>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Streaming-API-Documentation"/>
        public IAsyncResult BeginStreamFilter()
        {
            return WithTweetSharpAsync(
                q => q.Stream().FromFilter()
                         .CallbackTo((sender, result, state) => RaiseStreamResults(result))
                );
        }

        /// <summary>
        /// Asynchronously streams from the Twitter 'Filter' stream.
        /// Uses filter options to refine results.
        /// </summary>
        /// <param name="options">The options.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Streaming-API-Documentation"/>
        public IAsyncResult BeginStreamFilter(TwitterFilterStreamOptions options)
        {
            return WithTweetSharpAsync(
                q => q.Stream().FromFilter()
                         .Following(options.FollowingUserIds)
                         .WithBacklog(options.Backlog)
                         .Tracking(options.TrackingKeywords)
                         .Within(options.BoundingGeoLocations)
                         .CallbackTo((sender, result, state) => RaiseStreamResults(result))
                );
        }

        /// <summary>
        /// Asynchronously streams from the Twitter 'Filter' stream.
        /// Uses filter options to refine results.
        /// </summary>
        /// <param name="options">The options.</param>
        /// <param name="duration">The duration.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Streaming-API-Documentation"/>
        public IAsyncResult BeginStreamFilter(TwitterFilterStreamOptions options, TimeSpan duration)
        {
            return WithTweetSharpAsync(
                q => q.Stream().FromFilter()
                         .Following(options.FollowingUserIds)
                         .For(duration)
                         .WithBacklog(options.Backlog)
                         .Tracking(options.TrackingKeywords)
                         .Within(options.BoundingGeoLocations)
                         .CallbackTo((sender, result, state) => RaiseStreamResults(result))
                );
        }

        /// <summary>
        /// Asynchronously streams from the Twitter 'Filter' stream.
        /// Uses filter options to refine results.
        /// </summary>
        /// <param name="options">The options.</param>
        /// <param name="resultsPerCallback">The results per callback.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Streaming-API-Documentation"/>
        public IAsyncResult BeginStreamFilter(TwitterFilterStreamOptions options, int resultsPerCallback)
        {
            return WithTweetSharpAsync(q => q.Stream().FromFilter()
                                         .Following(options.FollowingUserIds)
                                         .Take(resultsPerCallback)
                                         .WithBacklog(options.Backlog)
                                         .Tracking(options.TrackingKeywords)
                                         .Within(options.BoundingGeoLocations)
                                         .CallbackTo((sender, result, state) => RaiseStreamResults(result))
                );
        }

        /// <summary>
        /// Asynchronously streams from the Twitter 'Filter' stream.
        /// Uses filter options to refine results.
        /// </summary>
        /// <param name="options">The options.</param>
        /// <param name="duration">The duration.</param>
        /// <param name="resultsPerCallback">The results per callback.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Streaming-API-Documentation"/>
        public IAsyncResult BeginStreamFilter(TwitterFilterStreamOptions options, TimeSpan duration, int resultsPerCallback)
        {
            return WithTweetSharpAsync(
                q => q.Stream().FromFilter()
                         .Following(options.FollowingUserIds)
                         .For(duration).Take(resultsPerCallback)
                         .WithBacklog(options.Backlog)
                         .Tracking(options.TrackingKeywords)
                         .Within(options.BoundingGeoLocations)
                         .CallbackTo((sender, result, state) => RaiseStreamResults(result))
                );
        }

        /// <summary>
        /// Asynchronously streams from the Twitter 'Filter' stream.
        /// </summary>
        /// <param name="duration">The duration.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Streaming-API-Documentation"/>
        public IAsyncResult BeginStreamFilter(TimeSpan duration)
        {
            return WithTweetSharpAsync(
                q => q.Stream().FromFilter().For(duration)
                         .CallbackTo((sender, result, state) => RaiseStreamResults(result))
                );
        }

        /// <summary>
        /// Asynchronously streams from the Twitter 'Filter' stream.
        /// </summary>
        /// <param name="duration">The duration.</param>
        /// <param name="resultsPerCallback">The results per callback.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Streaming-API-Documentation"/>
        public IAsyncResult BeginStreamFilter(TimeSpan duration, int resultsPerCallback)
        {
            return WithTweetSharpAsync(
                q => q.Stream().FromFilter().For(duration).Take(resultsPerCallback)
                         .CallbackTo((sender, result, state) => RaiseStreamResults(result))
                );
        }

        /// <summary>
        /// Asynchronously streams from the Twitter 'Filter' stream.
        /// </summary>
        /// <param name="resultsPerCallback">The results per callback.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Streaming-API-Documentation"/>
        public IAsyncResult BeginStreamFilter(int resultsPerCallback)
        {
            return WithTweetSharpAsync(
                q => q.Stream().FromFilter().Take(resultsPerCallback)
                         .CallbackTo((sender, result, state) => RaiseStreamResults(result))
                );
        } 
#else
        public void BeginStreamFilter()
        {
            WithTweetSharpAsync(
                q => q.Stream().FromFilter()
                         .CallbackTo((sender, result, state) => RaiseStreamResults(result))
                );
        }

        public void BeginStreamFilter(TwitterFilterStreamOptions options)
        {
            WithTweetSharpAsync(
                q => q.Stream().FromFilter()
                         .Following(options.FollowingUserIds)
                         .WithBacklog(options.Backlog)
                         .Tracking(options.TrackingKeywords)
                         .Within(options.BoundingGeoLocations)
                         .CallbackTo((sender, result, state) => RaiseStreamResults(result))
                );
        }

        public void BeginStreamFilter(TwitterFilterStreamOptions options, TimeSpan duration)
        {
            WithTweetSharpAsync(
                q => q.Stream().FromFilter()
                         .Following(options.FollowingUserIds)
                         .For(duration)
                         .WithBacklog(options.Backlog)
                         .Tracking(options.TrackingKeywords)
                         .Within(options.BoundingGeoLocations)
                         .CallbackTo((sender, result, state) => RaiseStreamResults(result))
                );
        }

        public void BeginStreamFilter(TwitterFilterStreamOptions options, int resultsPerCallback)
        {
            WithTweetSharpAsync(q => q.Stream().FromFilter()
                                         .Following(options.FollowingUserIds)
                                         .Take(resultsPerCallback)
                                         .WithBacklog(options.Backlog)
                                         .Tracking(options.TrackingKeywords)
                                         .Within(options.BoundingGeoLocations)
                                         .CallbackTo((sender, result, state) => RaiseStreamResults(result))
                );
        }

        public void BeginStreamFilter(TwitterFilterStreamOptions options, TimeSpan duration, int resultsPerCallback)
        {
            WithTweetSharpAsync(
                q => q.Stream().FromFilter()
                         .Following(options.FollowingUserIds)
                         .For(duration).Take(resultsPerCallback)
                         .WithBacklog(options.Backlog)
                         .Tracking(options.TrackingKeywords)
                         .Within(options.BoundingGeoLocations)
                         .CallbackTo((sender, result, state) => RaiseStreamResults(result))
                );
        }

        public void BeginStreamFilter(TimeSpan duration)
        {
            WithTweetSharpAsync(
                q => q.Stream().FromFilter().For(duration)
                         .CallbackTo((sender, result, state) => RaiseStreamResults(result))
                );
        }

        public void BeginStreamFilter(TimeSpan duration, int resultsPerCallback)
        {
            WithTweetSharpAsync(
                q => q.Stream().FromFilter().For(duration).Take(resultsPerCallback)
                         .CallbackTo((sender, result, state) => RaiseStreamResults(result))
                );
        }

        public void BeginStreamFilter(int resultsPerCallback)
        {
            WithTweetSharpAsync(
                q => q.Stream().FromFilter().Take(resultsPerCallback)
                         .CallbackTo((sender, result, state) => RaiseStreamResults(result))
                );
        } 
#endif
        #endregion

        #region Firehose

#if !WindowsPhone
        /// <summary>
        /// Asynchronously streams from the Twitter 'Firehose' stream.
        /// </summary>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Streaming-API-Documentation"/>
        public IAsyncResult BeginStreamFirehose()
        {
            return WithTweetSharpAsync(
                                   q => q.Stream().FromSample()
                                            .CallbackTo((sender, result, state) => RaiseStreamResults(result))
                );
        }

        /// <summary>
        /// Asynchronously streams from the Twitter 'Firehose' stream.
        /// </summary>
        /// <param name="duration">The duration.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Streaming-API-Documentation"/>
        public IAsyncResult BeginStreamFirehose(TimeSpan duration)
        {
            return WithTweetSharpAsync(
                                   q => q.Stream().FromSample().For(duration)
                                            .CallbackTo((sender, result, state) => RaiseStreamResults(result))
                );
        }

        public IAsyncResult BeginStreamUser(TimeSpan duration)
        {
            return WithTweetSharpAsync(
                q => q.Stream()
                      .FromUser()
                      .For(duration)
                      .CallbackTo((sender, result, state) => RaiseStreamResults(result))
            );
        }

        /// <summary>
        /// Asynchronously streams from the Twitter 'Firehose' stream.
        /// </summary>
        /// <param name="duration">The duration.</param>
        /// <param name="resultsPerCallback">The results per callback.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Streaming-API-Documentation"/>
        public IAsyncResult BeginStreamFirehose(TimeSpan duration, int resultsPerCallback)
        {
            return WithTweetSharpAsync(
                                   q => q.Stream().FromSample().For(duration).Take(resultsPerCallback)
                                            .CallbackTo((sender, result, state) => RaiseStreamResults(result))
                );
        }

        /// <summary>
        /// Asynchronously streams from the Twitter 'Firehose' stream.
        /// </summary>
        /// <param name="resultsPerCallback">The results per callback.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Streaming-API-Documentation"/>
        public IAsyncResult BeginStreamFirehose(int resultsPerCallback)
        {
            return WithTweetSharpAsync(
                                   q => q.Stream().FromSample().Take(resultsPerCallback)
                                            .CallbackTo((sender, result, state) => RaiseStreamResults(result))
                );
        } 
#else
        public void BeginStreamFirehose()
        {
            WithTweetSharpAsync(
                                   q => q.Stream().FromSample()
                                            .CallbackTo((sender, result, state) => RaiseStreamResults(result))
                );
        }

        public void BeginStreamFirehose(TimeSpan duration)
        {
            WithTweetSharpAsync(
                                   q => q.Stream().FromSample().For(duration)
                                            .CallbackTo((sender, result, state) => RaiseStreamResults(result))
                );
        }

        public void BeginStreamFirehose(TimeSpan duration, int resultsPerCallback)
        {
            WithTweetSharpAsync(
                                   q => q.Stream().FromSample().For(duration).Take(resultsPerCallback)
                                            .CallbackTo((sender, result, state) => RaiseStreamResults(result))
                );
        }

        public void BeginStreamFirehose(int resultsPerCallback)
        {
            WithTweetSharpAsync(
                                   q => q.Stream().FromSample().Take(resultsPerCallback)
                                            .CallbackTo((sender, result, state) => RaiseStreamResults(result))
                );
        }
#endif

        #endregion

        #region Retweets

#if !WindowsPhone
        /// <summary>
        /// Asynchronously streams from the Twitter 'Retweets' stream.
        /// </summary>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Streaming-API-Documentation"/>
        public IAsyncResult BeginStreamRetweets()
        {
            return WithTweetSharpAsync(
                q => q.Stream().FromSample()
                         .CallbackTo((sender, result, state) => RaiseStreamResults(result))
                );
        }

        /// <summary>
        /// Asynchronously streams from the Twitter 'Retweets' stream.
        /// </summary>
        /// <param name="duration">The duration.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Streaming-API-Documentation"/>
        public IAsyncResult BeginStreamRetweets(TimeSpan duration)
        {
            return WithTweetSharpAsync(
                q => q.Stream().FromSample().For(duration)
                         .CallbackTo((sender, result, state) => RaiseStreamResults(result))
                );
        }

        /// <summary>
        /// Asynchronously streams from the Twitter 'Retweets' stream.
        /// </summary>
        /// <param name="duration">The duration.</param>
        /// <param name="resultsPerCallback">The results per callback.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Streaming-API-Documentation"/>
        public IAsyncResult BeginStreamRetweets(TimeSpan duration, int resultsPerCallback)
        {
            return WithTweetSharpAsync(
                q => q.Stream().FromSample().For(duration).Take(resultsPerCallback)
                         .CallbackTo((sender, result, state) => RaiseStreamResults(result))
                );
        }

        /// <summary>
        /// Asynchronously streams from the Twitter 'Retweets' stream.
        /// </summary>
        /// <param name="resultsPerCallback">The results per callback.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Streaming-API-Documentation"/>
        public IAsyncResult BeginStreamRetweets(int resultsPerCallback)
        {
            return WithTweetSharpAsync(
                q => q.Stream().FromSample().Take(resultsPerCallback)
                         .CallbackTo((sender, result, state) => RaiseStreamResults(result))
                );
        }
#else
        public void BeginStreamRetweets()
        {
            WithTweetSharpAsync(
                q => q.Stream().FromSample()
                         .CallbackTo((sender, result, state) => RaiseStreamResults(result))
                );
        }

        public void BeginStreamRetweets(TimeSpan duration)
        {
            WithTweetSharpAsync(
                q => q.Stream().FromSample().For(duration)
                         .CallbackTo((sender, result, state) => RaiseStreamResults(result))
                );
        }

        public void BeginStreamRetweets(TimeSpan duration, int resultsPerCallback)
        {
            WithTweetSharpAsync(
                q => q.Stream().FromSample().For(duration).Take(resultsPerCallback)
                         .CallbackTo((sender, result, state) => RaiseStreamResults(result))
                );
        }

        public void BeginStreamRetweets(int resultsPerCallback)
        {
            WithTweetSharpAsync(
                q => q.Stream().FromSample().Take(resultsPerCallback)
                         .CallbackTo((sender, result, state) => RaiseStreamResults(result))
                );
        }
#endif

        #endregion

        #region Sample

#if !WindowsPhone
        /// <summary>
        /// Asynchronously streams from the Twitter 'Sample' stream.
        /// </summary>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Streaming-API-Documentation"/>
        public IAsyncResult BeginStreamSample()
        {
            return WithTweetSharpAsync(
                q => q.Stream().FromSample()
                         .CallbackTo((sender, result, state) => RaiseStreamResults(result))
                );
        }

        /// <summary>
        /// Asynchronously streams from the Twitter 'Sample' stream.
        /// </summary>
        /// <param name="duration">The duration.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Streaming-API-Documentation"/>
        public IAsyncResult BeginStreamSample(TimeSpan duration)
        {
            return WithTweetSharpAsync(
                q => q.Stream().FromSample().For(duration)
                         .CallbackTo((sender, result, state) => RaiseStreamResults(result))
                );
        }

        /// <summary>
        /// Asynchronously streams from the Twitter 'Sample' stream.
        /// </summary>
        /// <param name="duration">The duration.</param>
        /// <param name="resultsPerCallback">The results per callback.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Streaming-API-Documentation"/>
        public IAsyncResult BeginStreamSample(TimeSpan duration, int resultsPerCallback)
        {
            return WithTweetSharpAsync(
                q => q.Stream().FromSample().For(duration).Take(resultsPerCallback)
                         .CallbackTo((sender, result, state) => RaiseStreamResults(result))
                );
        }

        /// <summary>
        /// Asynchronously streams from the Twitter 'Sample' stream.
        /// </summary>
        /// <param name="resultsPerCallback">The results per callback.</param>
        /// <returns></returns>
        /// <seealso href="http://apiwiki.twitter.com/Streaming-API-Documentation"/>
        public IAsyncResult BeginStreamSample(int resultsPerCallback)
        {
            return WithTweetSharpAsync(
                q => q.Stream().FromSample().Take(resultsPerCallback)
                         .CallbackTo((sender, result, state) => RaiseStreamResults(result))
                );
        }
#else
        public void BeginStreamSample()
        {
            WithTweetSharpAsync(
                q => q.Stream().FromSample()
                         .CallbackTo((sender, result, state) => RaiseStreamResults(result))
                );
        }

        public void BeginStreamSample(TimeSpan duration)
        {
            WithTweetSharpAsync(
                q => q.Stream().FromSample().For(duration)
                         .CallbackTo((sender, result, state) => RaiseStreamResults(result))
                );
        }

        public void BeginStreamSample(TimeSpan duration, int resultsPerCallback)
        {
            WithTweetSharpAsync(
                q => q.Stream().FromSample().For(duration).Take(resultsPerCallback)
                         .CallbackTo((sender, result, state) => RaiseStreamResults(result))
                );
        }

        public void BeginStreamSample(int resultsPerCallback)
        {
            WithTweetSharpAsync(
                q => q.Stream().FromSample().Take(resultsPerCallback)
                         .CallbackTo((sender, result, state) => RaiseStreamResults(result))
                );
        }
#endif

        #endregion

        private void RaiseStreamResults(TwitterResult result)
        {
            if(string.IsNullOrEmpty(result.Response))
            {
                return;
            }

            var lines = result.Response.Split(new[] {Environment.NewLine[0]})
                .Where(l => !string.IsNullOrEmpty(l) && !l.Equals("\r") && !l.Equals("\n"));
            List<TwitterStatus> statuses;
            if(lines.Count() == 1)
            {
                statuses = new List<TwitterStatus>();
                    
                var jObject = JObject.Parse(result.Response);
                var deleted = jObject.FindSingleChildProperty("delete");
                if (deleted == null)
                {
                    statuses.Add(result.AsStatus());
                }
            }
            else
            {
                statuses = result.AsStatuses().ToList();
            }
            var args = new TwitterStreamResultEventArgs(statuses);
            OnStreamResult(args);
        }
    }
}