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
using TweetSharp.Core.Extensions;
using TweetSharp.Fluent;
using TweetSharp.Model;

namespace TweetSharp.Yammer.Fluent
{
    public static partial class IFluentYammerExtensions
    {
        /// <summary>
        /// Specifies the authentication details to use for the query \
        /// </summary>
        /// <param name="instance">the instance</param>
        /// <param name="consumerKey">Your application's OAuth Consumer Key</param>
        /// <param name="consumerSecret">Your application's OAuth Consumer secret</param>
        /// <param name="token">The user's Access token</param>
        /// <param name="tokenSecret">The user's Access token secret</param>
        /// <returns></returns>
        public static IFluentYammer AuthenticateWith(this IFluentYammer instance, string consumerKey,
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
        /// Specifies the authentication details to use for the query with consumer key set in the <see cref="YammerClientInfo" /> object.
        /// </summary>
        /// <param name="instance">the instance</param>
        /// <param name="token">The user's Access token</param>
        /// <param name="tokenSecret">The user's Access token secret</param>
        /// <returns></returns>
        public static IFluentYammer AuthenticateWith(this IFluentYammer instance, string token, string tokenSecret)
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

        internal static void ValidateConsumerCredentials(this IFluentYammer instance)
        {
            if (instance.ClientInfo == null ||
                instance.ClientInfo.ConsumerKey.IsNullOrBlank() ||
                instance.ClientInfo.ConsumerSecret.IsNullOrBlank())
            {
                throw new TweetSharpException(
                    "You need to provide a consumer key and secret, either in YammerClientInfo, or to the overload of this method.");
            }
        }
        /// <summary>
        /// Method to callback to when an asynchronous request completes
        /// </summary>
        /// <param name="instance">The instance</param>
        /// <param name="callback">The callback method</param>
        /// <returns></returns>
        public static IFluentYammer CallbackTo(this IFluentYammer instance, YammerWebCallback callback)
        {
            instance.Callback = callback;
            return instance;
        }

        //[jd]todo: mock support for yammer
        //public static IFluentYammer Expect(this IFluentYammer instance, IEnumerable<IModel> graph)
        //{
        //    //instance.Configuration.MockWebRequests = true;
        //    //instance.Configuration.MockGraph = graph;
        //    return instance;
        //}
    }
}