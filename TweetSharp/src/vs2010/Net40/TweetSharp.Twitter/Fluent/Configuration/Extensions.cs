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
using Hammock.Caching;
using Hammock.Retries;
using Hammock.Tasks;
using TweetSharp.Core.Extensions;
using TweetSharp.Twitter.Core;

namespace TweetSharp.Twitter.Fluent
{
    public static partial class Extensions
    {
        /// <summary>
        /// When this configuration option is called, any status updates that are longer than the
        /// 140 character option are truncated prior to sending. By default, updates longer than
        /// 140 characters will throw a <see cref="TweetSharpException" />
        /// </summary>
        /// <param name="instance">The current position in the fluent expression</param>
        /// <returns>The current position in the fluent expression</returns>
        public static IFluentTwitter UseUpdateTruncation(this IFluentTwitterConfiguration instance)
        {
            instance.TruncateUpdates = true;
            return instance.Root;
        }

        /// <summary>
        /// When this configuration option is called, the specified cache provider is used for any subsequent
        /// caching on the request. The default caching strategy is this method is not used, is <see cref="Hammock.Caching.AspNetCache" />.
        /// </summary>
        /// <param name="cache">The caching strategy to use</param>
        /// <param name="instance">The current position in the fluent expression</param>
        /// <returns>The current position in the fluent expression</returns>
        public static IFluentTwitter CacheWith(this IFluentTwitterConfiguration instance, ICache cache)
        {
            instance.CacheStrategy = cache;
            return instance.Root;
        }

        /// <summary>
        /// When this configuration option is called, any request made inside the specified absolute expiration date,
        /// is served from the client cache rather than from a request made to Twitter.
        /// </summary>
        /// <param name="absoluteExpiration">The specified local time that the cache for the request as defined will expire</param>
        /// <param name="instance">The current position in the fluent expression</param>
        /// <returns>The current position in the fluent expression</returns>
        public static IFluentTwitter CacheUntil(this IFluentTwitterConfiguration instance, DateTime absoluteExpiration)
        {
            instance.CacheAbsoluteExpiration = absoluteExpiration;
            return instance.Root;
        }

        /// <summary>
        /// When this configuration option is called, any request made inside the specified sliding expiratino date,
        /// is served from the client cache rather than from a request made to Twitter. Sliding expiration countdown begins
        /// from the last time a request for the same URL was executed.
        /// </summary>
        /// <param name="slidingExpiration">The specified amount of inactivity that may elapse before expiring the cache</param>
        /// <param name="instance">The current position in the fluent expression</param>
        /// <returns>The current position in the fluent expression</returns>
        public static IFluentTwitter CacheForInactivityOf(this IFluentTwitterConfiguration instance,
                                                          TimeSpan slidingExpiration)
        {
            instance.CacheSlidingExpiration = slidingExpiration;
            return instance.Root;
        }

        /// <summary>
        /// Throttles recurring task using a calculation, using the return value from the predicate to determine if the task should run
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="predicate">Predicate - will not run query if false is returned.</param>
        /// <param name="getRateLimitFunction">User provided function to get the RateLimit status</param>
        /// <returns></returns>
        public static IFluentTwitter UseRateLimiting(this IFluentTwitterConfiguration instance,
                                                     Predicate<IRateLimitStatus> predicate,
                                                     Func<IRateLimitStatus> getRateLimitFunction)
        {
            instance.LimitRate = true;
            instance.Root.RateLimitingRule = new RateLimitingRule<IRateLimitStatus>(predicate)
                                                 {
                                                     GetRateLimitStatus = getRateLimitFunction
                                                 };
            return instance.Root;
        }

        /// <summary>
        /// Throttles recurring task using a calculation, limiting it to a percentage of the periodic total rate limit
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="percentOfTotalLimit">Percentage of the user's total rate limit to allocate for this recurring request</param>
        /// <returns></returns>
        public static IFluentTwitter UseRateLimiting(this IFluentTwitterConfiguration instance,
                                                     double percentOfTotalLimit)
        {
            instance.LimitRate = true;
            instance.Root.RateLimitingRule = new RateLimitingRule<IRateLimitStatus>(percentOfTotalLimit);
            return instance.Root;
        }

        /// <summary>
        /// Throttles recurring task using a calculation, limiting it to a percentage of the periodic total rate limit
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="percentOfTotalLimit">Percentage of the user's total rate limit to allocate for this recurring request</param>
        /// <param name="getRateLimitFunction">User provided function to get the rate limit status</param>
        /// <returns></returns>
        public static IFluentTwitter UseRateLimiting(this IFluentTwitterConfiguration instance,
                                                     double percentOfTotalLimit,
                                                     Func<IRateLimitStatus> getRateLimitFunction)
        {
            instance.LimitRate = true;
            instance.Root.RateLimitingRule = new  RateLimitingRule<IRateLimitStatus>(percentOfTotalLimit)
                                                 {GetRateLimitStatus = getRateLimitFunction};
            return instance.Root;
        }

        /// <summary>
        /// When this configuration option is called, the query request is sent via the specified proxy URL,
        /// rather than directly to Twitter.
        /// <remarks>
        /// Currently, the .NET default of using the Internet Explorer defined proxy as a default for all
        /// outgoing requests is in place, but may change before the version 1.0 release.
        /// </remarks>
        /// </summary>
        /// <param name="url">The URL of a proxy to use</param>
        /// <param name="instance">The current position in the fluent expression</param>
        /// <returns>The current position in the fluent expression</returns>
        public static IFluentTwitter UseProxy(this IFluentTwitterConfiguration instance, string url)
        {
            instance.Proxy = url;
            return instance.Root;
        }

        /// <summary>
        /// Uses the transparent proxy instead of calling twitter directly
        /// </summary>
        /// <param name="instance">The FluentTwitter instance.</param>
        /// <param name="url">The transparent proxy URL.</param>
        /// <returns>The FluentTwitter instance</returns>
        public static IFluentTwitter UseTransparentProxy(this IFluentTwitterConfiguration instance, string url)
        {
            if (!url.EndsWith("/"))
            {
                url = url.Then("/");
            }
            instance.TransparentProxy = url;
            return instance.Root;
        }

        /// <summary>
        /// When this configuration option is called, the query request is sent as GZIP encoded content,
        /// and automatically decompressed when received. This is useful for requests that retrieve a large
        /// number of results, but will increase bandwidth on smaller requests.
        /// </summary>
        /// <param name="instance">The current position in the fluent expression</param>
        /// <returns>The current position in the fluent expression</returns>
        public static IFluentTwitter UseGzipCompression(this IFluentTwitterConfiguration instance)
        {
            instance.CompressHttpRequests = true;
            return instance.Root;
        }

        /// <summary>
        /// Sets up automatic retries for the error conditions indicated in 'retryPolicies'
        /// </summary>
        /// <param name="instance">The intance</param>
        /// <param name="retryOn">The error condition(s) that trigger a retry</param>
        /// <param name="maximumRetries">Max number of times to retry.  If exhausted, the last error will be returned</param>
        /// <returns></returns>
        public static IFluentTwitter UseAutomaticRetries(this IFluentTwitterConfiguration instance, 
                                                         RetryOn retryOn,
                                                         int maximumRetries)
        {
            var conditions = new List<IRetryCondition>();
            var policy = new RetryPolicy { RetryCount = maximumRetries };
            var failWhale = new FailWhale();
            var serviceError = new ServiceError();
           
            switch(retryOn)
            {
                case RetryOn.Never:
                    break;
                case RetryOn.FailWhale:
                    conditions.Add(failWhale);
                    break;
                case RetryOn.ServiceError:
                    conditions.Add(serviceError);
                    break;
                case RetryOn.Network:
                    conditions.Add(new NetworkError());
                    break;
                case RetryOn.FailWhaleOrNetwork:
                    conditions.Add(failWhale);
                    conditions.Add(new NetworkError());
                    break;
#if !SILVERLIGHT
                case RetryOn.Timeout:
                    conditions.Add(new Timeout());
                    break;
                case RetryOn.ConnectionClosed:
                    conditions.Add(new ConnectionClosed());
                    break;
#endif
                default:
                    throw new ArgumentOutOfRangeException("retryOn");
            }

            policy.RetryOn(conditions);
            instance.RetryPolicy = policy;
            instance.MaxRetries = maximumRetries;
            return instance.Root;
        }

        /// <summary>
        /// Configures the query to timeout at a specified time.
        /// This timeout applies to the length of time needed to process an HTTP request.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="timeout">The timeout.</param>
        /// <returns></returns>
        public static IFluentTwitter TimeoutAfter(this IFluentTwitterConfiguration instance, TimeSpan timeout)
        {
            instance.RequestTimeout = timeout;
            return instance.Root;
        }

        /// <summary>
        /// Configures TweetSharp to use https endpoints when connecting to twitter. Has no effect 
        /// if the <see cref="UseTransparentProxy">UseTransparentProxy</see> sets a proxy server to
        /// use instead of calling the twitter endpoints directly. 
        /// </summary>
        /// <param name="instance">The intance</param>
        /// <returns></returns>
        public static IFluentTwitter UseHttps(this IFluentTwitterConfiguration instance )
        {
            instance.UseHttps = true;
            return instance.Root;
        }
    }
}