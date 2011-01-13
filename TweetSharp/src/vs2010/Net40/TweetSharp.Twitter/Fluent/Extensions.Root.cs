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
using TweetSharp.Core.Extensions;
using TweetSharp.Fluent;
using TweetSharp.Model;

namespace TweetSharp.Twitter.Fluent
{
    public partial class FluentTwitterExtensions
    {
        /// <summary>
        /// Authenticates as a user using real credentials.
        /// This is facilitated by HTTP Basic Authorization, that
        /// URL-encodes a username and password in the Authorization request header.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        [Obsolete]
        public static IFluentTwitter AuthenticateAs(this IFluentTwitter instance, string username, string password)
        {
            instance.Authentication.Mode = AuthenticationMode.Basic;
            instance.Authentication.Authenticator = new FluentBaseBasicAuth(username, password);

            return instance;
        }

        /// <summary>
        /// Authenticates as a user using delegated token-based credentials.
        /// This is facilitated by OAuth v1.0a, that passes encrypted
        /// variables in the Authorization request header.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="consumerKey">The consumer key.</param>
        /// <param name="consumerSecret">The consumer secret.</param>
        /// <param name="token">The token.</param>
        /// <param name="tokenSecret">The token secret.</param>
        /// <returns></returns>
        public static IFluentTwitter AuthenticateWith(this IFluentTwitter instance, string consumerKey,
                                                      string consumerSecret, string token, string tokenSecret)
        {
            instance.Authentication.Mode = AuthenticationMode.OAuth;
            instance.Authentication.Authenticator = new FluentBaseOAuth
                                                        {
                                                            Action = "resource",
                                                            ConsumerKey = consumerKey,
                                                            ConsumerSecret = consumerSecret,
                                                            Token = token,
                                                            TokenSecret = tokenSecret
                                                        };
            return instance;
        }

        /// <summary>
        /// Authenticates as a user using delegated token-based credentials.
        /// This is facilitated by OAuth v1.0a, that passes encrypted
        /// variables in the Authorization request header.
        /// This method assumes that OAuth consumer info is set in <see cref="TwitterClientInfo" />.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="token">The token.</param>
        /// <param name="tokenSecret">The token secret.</param>
        /// <returns></returns>
        public static IFluentTwitter AuthenticateWith(this IFluentTwitter instance, string token, string tokenSecret)
        {
            ValidateConsumerCredentials(instance);

            var consumerKey = instance.ClientInfo.ConsumerKey;
            var consumerSecret = instance.ClientInfo.ConsumerSecret;

            instance.Authentication.Mode = AuthenticationMode.OAuth;
            instance.Authentication.Authenticator = new FluentBaseOAuth
                                                        {
                                                            Action = "resource",
                                                            ConsumerKey = consumerKey,
                                                            ConsumerSecret = consumerSecret,
                                                            Token = token,
                                                            TokenSecret = tokenSecret
                                                        };
            return instance;
        }

        internal static void ValidateConsumerCredentials(this IFluentTwitter instance)
        {
            if (instance.ClientInfo == null ||
                instance.ClientInfo.ConsumerKey.IsNullOrBlank() ||
                instance.ClientInfo.ConsumerSecret.IsNullOrBlank())
            {
                throw new TweetSharpException(
                    "You need to provide a consumer key and secret, either in TwitterClientInfo, or to the overload of this method."
                    );
            }
        }

        /// <summary>
        /// Calling this method will establish the asynchronous callback used when the request receives a response.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="callback">The callback executed when a request completes in the background</param>
        /// <returns>The current location in the fluent expression</returns>
        public static IFluentTwitter CallbackTo(this IFluentTwitter instance, TwitterWebCallback callback)
        {
            instance.Callback = callback;
            return instance;
        }

        /// <summary>
        /// Repeats an asynchronous query for the allotted time span.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="timeSpan">The time span.</param>
        /// <returns></returns>
        public static IFluentTwitter RepeatEvery(this IFluentTwitter instance, TimeSpan timeSpan)
        {
            instance.RepeatInterval = timeSpan;
            instance.RepeatTimes = 0;
            return instance;
        }

        /// <summary>
        /// Repeats an asynchronous query for the allotted time span,
        /// Up to the number of times specified.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="timeSpan">The time span.</param>
        /// <param name="times">The times.</param>
        /// <returns></returns>
        public static IFluentTwitter RepeatAfter(this IFluentTwitter instance, TimeSpan timeSpan, int times)
        {
            instance.RepeatInterval = timeSpan;
            instance.RepeatTimes = times;
            return instance;
        }

        /// <summary>
        /// Calling this method will enable mocking support, and return the Twitter object graph
        /// specified when the request is executed. 
        /// </summary>
        /// <param name="graph">A graph of objects expected in the response</param>
        /// <returns>The current location in the fluent expression</returns>
        /// <param name="instance">The current location in the fluent expression</param>
        public static IFluentTwitter Expect(this IFluentTwitter instance, IEnumerable<IModel> graph)
        {
            instance.Configuration.MockGraph = graph;
            return instance;
        }

        /// <summary>
        /// Calling this method will enable mocking support, and return the HTTP status code
        /// specified when the request is executed.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="statusCode">The status code.</param>
        /// <returns></returns>
        public static IFluentTwitter Expect(this IFluentTwitter instance, int statusCode)
        {
            instance.Configuration.MockStatusCode = statusCode;
            return instance;
        }
    }
}