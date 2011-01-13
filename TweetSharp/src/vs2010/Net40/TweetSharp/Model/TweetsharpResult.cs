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
using System.Net;
using TweetSharp.Core.Extensions;

namespace TweetSharp.Model
{

    /// <summary>
    /// Base class for objects describing the results of a query to a supported service
    /// </summary>
    public abstract class TweetSharpResult
    {
#if !SILVERLIGHT
        bool _timedOut; 
#endif
        /// <summary>
        /// Gets or sets the date and time that the request was made
        /// </summary>
        public virtual DateTime? RequestDate { get; set; }
        /// <summary>
        /// Gets or sets the Uri used in the query request
        /// </summary>
        public virtual Uri RequestUri { get; set; }
        /// <summary>
        /// Gets or sets the Http method used for the request
        /// </summary>
        public virtual string RequestHttpMethod { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual bool RequestKeptAlive { get; set; }
        /// <summary>
        /// Gets or sets the date and time the query response was received
        /// </summary>
        public virtual DateTime? ResponseDate { get; set; }
        /// <summary>
        /// Gets or sets the underlying <see cref="System.Net.WebResponse">WebResponse</see> object
        /// </summary>
        public virtual WebResponse WebResponse { get; set; }
        /// <summary>
        /// Gets or sets the response body 
        /// </summary>
        public virtual string Response { get; set; }
        /// <summary>
        /// Gets or sets the content type of the response
        /// </summary>
        public virtual string ResponseType { get; set; }
        /// <summary>
        /// Gets or sets the Http Status code returned from the server
        /// </summary>
        public virtual int ResponseHttpStatusCode { get; set; }
        /// <summary>
        /// Gets or sets the description associated with <see cref="ResponseHttpStatusCode"/>
        /// </summary>
        public virtual string ResponseHttpStatusDescription { get; set; }
        /// <summary>
        /// Gets or sets the length of the response in bytes
        /// </summary>
        public virtual long ResponseLength { get; set; }
        /// <summary>
        /// Gets or sets the Uri of the response
        /// </summary>
        public virtual Uri ResponseUri { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether or not the query was fulfilled with mock data
        /// </summary>
        public virtual bool IsMock { get; set; }
        /// <summary>
        /// Gets or sets any <see cref="System.Net.WebException">exception</see> that was caught during the query 
        /// </summary>
        public virtual WebException Exception { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether or not the recurring query was skipped due to rate limiting rules
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this query was skipped for rate limiting purposes; otherwise, <c>false</c>.
        /// </value>
        public virtual bool WasRateLimited { get; set; }
        ///// <summary>
        ///// Gets or sets the result of a previous failed query that occured during a retry-enabled query
        ///// </summary>
        //public virtual TweetSharpResult PreviousResult { get; set; }
        /// <summary>
        /// Gets or sets a value indicating that the query failed due to an error in the target REST service
        /// </summary>
        public abstract bool IsServiceError { get; }

        /// <summary>
        /// Gets a value indicating whether this result was served from a cache.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this result is from a cache; otherwise, <c>false</c>.
        /// </value>
        public virtual bool IsFromCache
        {
            get
            {
                return !Response.IsNullOrBlank() &&
                       ResponseUri == null &&
                       !IsServiceError;
            }
        }

#if !SILVERLIGHT
        /// <summary>
        /// Gets a value indicating whether the query failed due to a network error
        /// </summary>
        /// <value>
        /// 	<c>true</c> if the query failed because of a network problem; otherwise, <c>false</c>.
        /// </value>
        public virtual bool IsNetworkError
        {
            get
            {
                return ExceptionStatus != WebExceptionStatus.Success
                       && ExceptionStatus != WebExceptionStatus.ProtocolError
                       && ExceptionStatus != WebExceptionStatus.Pending;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the query timed out waiting for a response
        /// </summary>
        /// <value>
        /// 	<c>true</c> if the query failed due to timeout; otherwise, <c>false</c>.
        /// </value>
        public virtual bool TimedOut
        {
            set
            {
                _timedOut = value; 
            }
            get
            {
                return _timedOut || ExceptionStatus == WebExceptionStatus.Timeout
                       || ExceptionStatus == WebExceptionStatus.RequestCanceled;
            }
        }

#else
        public virtual bool IsNetworkError
        {
            get
            {
                return ExceptionStatus != WebExceptionStatus.Success
                       && ExceptionStatus != WebExceptionStatus.Pending;
            }
        }

        public virtual bool IsTimeout
        {
            get { return ExceptionStatus == WebExceptionStatus.RequestCanceled; }
        }
#endif
        /// <summary>
        /// Gets a value indicating the nature of the <see cref="Exception">WebException</see> that occured
        /// </summary>
        /// <value>
        ///     <c>WebExceptionStatus.Success</c> if the query succeeded; otherwise a <see cref="System.Net.WebExceptionStatus">WebExceptionStatus</see> enumeration value
        /// </value>
        public virtual WebExceptionStatus ExceptionStatus
        {
            get { return Exception == null ? WebExceptionStatus.Success : Exception.Status; }
        }

        /// <summary>
        /// Gets or sets a value indicating how many retries occured to get the current result
        /// </summary>
        public virtual int Retries
        {
            get; set;
        }
    }
}