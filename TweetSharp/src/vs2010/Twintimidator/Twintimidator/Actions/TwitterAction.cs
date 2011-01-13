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
using TweetSharp.Twitter.Extensions;
using TweetSharp.Twitter.Fluent;
using TweetSharp.Twitter.Model;
using Twintimidator.Accounts;
using Twintimidator.Properties;

namespace Twintimidator.Actions
{
    /// <summary>
    /// Describes an action to perform using TweetSharp 
    /// </summary>
    /// <typeparam name="T">The expected type from TweetSharp.Model that the action will produce</typeparam>
    public class TwitterAction<T> : ITwitterAction<T>, ITwitterAction where T : class
    {
        #region ITwitterAction Members

        /// <summary>
        /// An identifier for this action
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// The twitter account with which to perform this action
        /// </summary>
        public virtual IUserAccount User { get; set; }

        /// <summary>
        /// Returns a new FluentTwitter query with the appropriate configuation and authentication set to build upon
        /// </summary>
        /// <returns>A partially configured FluentTwitter query instance</returns>
        public virtual IFluentTwitter GetBaseQuery()
        {
            if (User != null)
            {
                if (User.AccountType == AccountType.BasicAuth)
                {
                    return FluentTwitter.CreateRequest().AuthenticateAs(User.UserName, User.Password);
                }
                var consumerKey = Resources.ConsumerKey;
                var consumerSecret = Resources.ConsumerSecret;
                return FluentTwitter.CreateRequest().AuthenticateWith(consumerKey,
                                                                      consumerSecret,
                                                                      User.OAuthToken,
                                                                      User.OAuthTokenSecret);
            }
            return FluentTwitter.CreateRequest();
        }

        /// <summary>
        /// Gets or sets the FluentTwitter query used to perform the action
        /// </summary>
        public Func<ITwitterLeafNode> QueryMethod { get; set; }


        ITwitterActionResult ITwitterAction.Execute()
        {
            return Execute();
        }

        ITwitterActionResult ITwitterAction.ExecuteAs(IUserAccount user)
        {
            return ExecuteAs(user);
        }

        /// <summary>
        /// executes the action asynchronously, calls back to the provided callback
        /// </summary>
        public void ExecuteAsync(Action<ITwitterActionResult> callback)
        {
            CheckStateAndThrowIfInvalidForExecution();
            QueryAsync((s, r, e) =>
                           {
                               var twitterActionResult = CheckAndConvertResult(r);
                               if (twitterActionResult.Success)
                               {
                                   twitterActionResult.Success = EvaluateConvertedReturnValueMethod(twitterActionResult);
                               }
                               callback(twitterActionResult);
                           });
        }

        /// <summary>
        /// executes the action asynchronously as the provided user, calls back to the provided callback
        /// </summary>
        public void ExecuteAsyncAs(IUserAccount user, Action<ITwitterActionResult> callback)
        {
            User = user;
            ExecuteAsync(callback);
        }

        #endregion

        #region ITwitterAction<T> Members

        void ITwitterAction<T>.ExecuteAsync(Action<ITwitterActionResult<T>> callback)
        {
            ExecuteAsync(c => callback(c as ITwitterActionResult<T>));
        }

        void ITwitterAction<T>.ExecuteAsyncAs(IUserAccount user, Action<ITwitterActionResult<T>> callback)
        {
            ExecuteAsyncAs(user, c => callback(c as ITwitterActionResult<T>));
        }

        /// <summary>
        /// gets or sets the function used to convert the result from twitter in to a type of T
        /// </summary>
        public Func<TwitterResult, T> ConvertReturnValueMethod { get; set; }

        /// <summary>
        /// gets or sets the function used to determine whether or not the result was correct
        /// </summary>
        public Func<ITwitterActionResult<T>, bool> EvaluateConvertedReturnValueMethod { get; set; }

        /// <summary>
        /// executes the action as the provided user, checks the results, and returns a result object
        /// </summary>
        /// <returns>a result object with the results of the action</returns>
        public ITwitterActionResult<T> ExecuteAs(IUserAccount user)
        {
            User = user;
            return Execute();
        }

        /// <summary>
        /// executes the action, checks the results, and returns a result object
        /// </summary>
        /// <returns>a result object with the results of the action</returns>
        public ITwitterActionResult<T> Execute()
        {
            CheckStateAndThrowIfInvalidForExecution();

            var queryResponse = Query();
            var twitterActionResult = CheckAndConvertResult(queryResponse);
            if (twitterActionResult.Success)
            {
                twitterActionResult.Success = EvaluateConvertedReturnValueMethod(twitterActionResult);
            }
            return twitterActionResult;
        }

        #endregion

        private TwitterResult Query()
        {
            if (User == null)
            {
                throw new UserNotSpecifiedException();
            }
            return QueryMethod().Request();
        }

        private void QueryAsync(TwitterWebCallback callback)
        {
            if (User == null)
            {
                throw new UserNotSpecifiedException();
            }
            QueryMethod().CallbackTo(callback).BeginRequest();
        }

        public override string ToString()
        {
            return Name;
        }

        private ITwitterActionResult<T> CheckAndConvertResult(TwitterResult twitterResult)
        {
            var actionResult = new TwitterActionResult();
            if (twitterResult.IsTwitterError)
            {
                actionResult.Success = false;
                actionResult.ErrorMessage = twitterResult.AsError().ErrorMessage;
            }
            else
            {
                actionResult.Success = true;
                actionResult.ReturnValue = ConvertReturnValueMethod(twitterResult);
            }
            return actionResult;
        }

        private void CheckStateAndThrowIfInvalidForExecution()
        {
            if (QueryMethod == null)
            {
                throw new InvalidOperationException("'Query' not set");
            }
            if (ConvertReturnValueMethod == null)
            {
                throw new InvalidOperationException("'ConvertReturnValueMethod' not set");
            }
            if (EvaluateConvertedReturnValueMethod == null)
            {
                throw new InvalidOperationException("'EvaluateConvertedReturnValueMethod' not set");
            }
        }

        #region Nested type: TwitterActionResult

        /// <summary>
        /// Object encapsulating the results of the action
        /// </summary>
        public class TwitterActionResult : ITwitterActionResult<T>
        {
            #region ITwitterActionResult<T> Members

            public bool Success { get; set; }
            public object ReturnValue { get; internal set; }

            T ITwitterActionResult<T>.ReturnValue
            {
                get { return ReturnValue as T; }
            }

            public string ErrorMessage { get; internal set; }

            #endregion
        }

        #endregion
    }
}