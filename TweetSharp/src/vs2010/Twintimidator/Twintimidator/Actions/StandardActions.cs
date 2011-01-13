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
using System.Reflection;
using TweetSharp.Extensions;
using TweetSharp.Twitter.Fluent;
using TweetSharp.Twitter.Model;
using TweetSharp.Twitter.Extensions;

namespace Twintimidator.Actions
{
    internal static class StandardActions
    {
        public static IEnumerable<ITwitterAction> Actions
        {
            get
            {
                var t = typeof (StandardActions);
                var props = t.GetProperties(BindingFlags.Public | BindingFlags.Static);
                foreach (var prop in props)
                {
                    var val = prop.GetValue(null, BindingFlags.Static, null, new object[] {}, null);
                    if (val is ITwitterAction)
                    {
                        yield return (ITwitterAction) val;
                    }
                }
            }
        }

        public static ITwitterAction<TwitterUser> VerifyCredentialsAsJson
        {
            get
            {
                var action = new TwitterAction<TwitterUser>
                                 {
                                     Name = "Verify Credentials As Json",
                                 };
                action.QueryMethod =
                    () =>
                    ITwitterAccountExtensions.VerifyCredentials(FluentTwitterExtensions.Account(action.GetBaseQuery()))
                        .AsJson();
                action.ConvertReturnValueMethod = s => s.AsUser();
                action.EvaluateConvertedReturnValueMethod = result => result.ReturnValue != null;
                return action;
            }
        }

        public static ITwitterAction<TwitterUser> VerifyCredentialsAsXml
        {
            get
            {
                var action = new TwitterAction<TwitterUser>
                                 {
                                     Name = "Verify Credentials As XML",
                                 };
                action.QueryMethod =
                    () =>
                    ITwitterAccountExtensions.VerifyCredentials(FluentTwitterExtensions.Account(action.GetBaseQuery()))
                        .AsXml();
                action.ConvertReturnValueMethod = s => s.AsUser();
                action.EvaluateConvertedReturnValueMethod = result => result.ReturnValue != null;
                return action;
            }
        }

        public static ITwitterAction<IEnumerable<TwitterStatus>> GetHomeTimelineAsJson
        {
            get
            {
                var action = new TwitterAction<IEnumerable<TwitterStatus>>
                                 {
                                     Name = "Get Home Timeline As Json",
                                 };
                action.QueryMethod =
                    () =>
                    TwitterStatusesExtensions.OnHomeTimeline(FluentTwitterExtensions.Statuses(action.GetBaseQuery())).
                        AsJson();
                action.ConvertReturnValueMethod = s => s.AsStatuses();
                action.EvaluateConvertedReturnValueMethod = result => result.ReturnValue != null;
                return action;
            }
        }

        public static ITwitterAction<IEnumerable<TwitterStatus>> GetHomeTimelineAsXml
        {
            get
            {
                var action = new TwitterAction<IEnumerable<TwitterStatus>>
                                 {
                                     Name = "Get Home Timeline as XML",
                                 };
                action.QueryMethod =
                    () =>
                    TwitterStatusesExtensions.OnHomeTimeline(FluentTwitterExtensions.Statuses(action.GetBaseQuery())).
                        AsXml();
                action.ConvertReturnValueMethod = s => s.AsStatuses();
                action.EvaluateConvertedReturnValueMethod = result => result.ReturnValue != null;
                return action;
            }
        }

        public static ITwitterAction<TwitterSearchTrends> GetTrends
        {
            get
            {
                var action = new TwitterAction<TwitterSearchTrends>
                                 {
                                     Name = "Get trends"
                                 };
                action.QueryMethod =
                    () =>
                    ITwitterSearchExtensions.Trends(FluentTwitterExtensions.Search(action.GetBaseQuery())).AsJson();
                action.ConvertReturnValueMethod = r => r.AsSearchTrends();
                action.EvaluateConvertedReturnValueMethod =
                    result => result.ReturnValue != null && result.ReturnValue.Trends != null;
                return action;
            }
        }

        public static ITwitterAction<TwitterSearchResult> SearchForText
        {
            get
            {
                var action = new TwitterAction<TwitterSearchResult>
                                 {
                                     Name = "Search for 'turkey'",
                                 };
                action.QueryMethod =
                    () =>
                    Extensions.Containing(
                                             ITwitterSearchExtensions.Query(
                                                                               FluentTwitterExtensions.Search(
                                                                                                                  action
                                                                                                                      .
                                                                                                                      GetBaseQuery
                                                                                                                      ())),
                                             "turkey");
                action.ConvertReturnValueMethod = result => result.AsSearchResult();
                action.EvaluateConvertedReturnValueMethod = result => result.ReturnValue != null;
                return action;
            }
        }

        public static ITwitterAction<IEnumerable<TwitterStatus>> GetPublicTimelineWithCaching
        {
            get
            {
                var action = new TwitterAction<IEnumerable<TwitterStatus>>
                                 {
                                     Name = "Get public timeline with caching"
                                 };
                action.QueryMethod =
                    () =>
                    TwitterStatusesExtensions.OnPublicTimeline(
                                                                   FluentTwitterExtensions.Statuses(
                                                                                                        Extensions.
                                                                                                            CacheUntil(
                                                                                                                          action
                                                                                                                              .
                                                                                                                              GetBaseQuery
                                                                                                                              ()
                                                                                                                              .
                                                                                                                              Configuration,
                                                                                                                          20
                                                                                                                              .
                                                                                                                              Minutes
                                                                                                                              ()
                                                                                                                              .
                                                                                                                              FromNow
                                                                                                                              ())))
                        .AsJson();
                action.ConvertReturnValueMethod = result =>
                                                      {
                                                          Console.WriteLine("Response {0} from cache",
                                                                            result.IsFromCache ? "is" : "isn't");
                                                          return result.AsStatuses();
                                                      };
                action.EvaluateConvertedReturnValueMethod = result => result.ReturnValue != null;
                return action;
            }
        }

        public static ITwitterAction<IEnumerable<TwitterStatus>> SharedInstanceGetHomeTimeline
        {
            get { return new SharedInstanceGetHomeTimelineAction(); }
        }
    }
}