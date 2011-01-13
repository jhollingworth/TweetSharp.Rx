using System;
using System.Collections.Generic;
using Hammock.Caching;
using Hammock.Retries;
using TweetSharp.Model;

namespace TweetSharp.Fluent
{
    /// <summary>
    /// Common configuration options for a fluent interface to a web service.
    /// </summary>
    public interface IFluentConfiguration
    {
        /// <summary>
        /// Gets or sets a graph of mock objects to use in a mock query
        /// </summary>
        IEnumerable<IModel> MockGraph { get; set; }

        /// <summary>
        /// Gets or sets the HTTP status code to return in a mock query.
        /// </summary>
        int? MockStatusCode { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="Hammock.Caching.ICache">cache strategy</see> for the query
        /// </summary>
        ICache CacheStrategy { get; set; }

        /// <summary>
        /// Gets or sets the date and time the cache results should expire
        /// </summary>
        DateTime? CacheAbsoluteExpiration { get; set; }

        /// <summary>
        /// Gets or sets the amount of time to cache the query results for
        /// </summary>
        TimeSpan? CacheSlidingExpiration { get; set; }

        /// <summary>
        /// Gets or sets the address of the proxy server to use for the query
        /// </summary>
        string Proxy { get; set; }

        /// <summary>
        /// Gets or sets the address of a transparent proxy to use for the query
        /// The transparent proxy must implement the same endpoints as the service.
        /// Example: 'http://api.twitter.com/v1/statuses/hometimeline.json' becomes
        /// 'http://transparentproxy.example.com/statuses/hometimeline.json' when
        /// the query is submitted.
        /// </summary>
        string TransparentProxy { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="Hammock.Retries.RetryPolicy">Retry Policy</see> detailing 
        /// when failed queries should be retried
        /// </summary>
        RetryPolicy RetryPolicy { get; set; }

        /// <summary>
        /// Gets or sets the number of times a failing query should be retried. Use
        /// in conjunction with <see cref="RetryPolicy"/>
        /// </summary>
        int MaxRetries { get; set; }

        /// <summary>
        /// Gets or sets the amount of time to wait before aborting a query.
        /// If unspecified, the system default is used. 
        /// </summary>
        TimeSpan? RequestTimeout { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the client should request 
        /// compressed data from the server. 
        /// </summary>
        bool CompressHttpRequests { get; set; }
    }
}