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
using TweetSharp.Fluent;
using TweetSharp.Fluent.Base;
using TweetSharp.Model;

namespace TweetSharp.Service
{
    /// <summary>
    /// Abstract base implementation for a flattened, non-fluent representation of a TweetSharp supported service
    /// </summary>
    /// <typeparam name="TFluent">Type of fluent interface this is the flattened representation of. Must be derived from type <see cref="IFluentBase{T}"/></typeparam>
    /// <typeparam name="TResult">Type of result object returned from the fluent interface methods. Must be derived from type <see cref="TweetSharpResult"/></typeparam>
    /// <typeparam name="TNode">The base type of fluent nodes for this service type.  Must be derived from type <see cref="IFluentNode"/></typeparam>
    public abstract class ServiceBase<TFluent, TResult, TNode>
        where TFluent : IFluentBase<TResult>
        where TResult : TweetSharpResult
        where TNode : IFluentNode
    {
        /// <summary>
        /// Gets the fluent query 
        /// </summary>
        /// <returns>The <see cref="IFluentBase{TResult}"/> query</returns>
        protected abstract TFluent GetAuthenticatedQuery();

#if !SILVERLIGHT
        /// <summary>
        /// Executes the fluent query and returns the result
        /// </summary>
        /// <typeparam name="T">The return type of the query</typeparam>
        /// <param name="query">The <see cref="IFluentBase{TResult}"/> to execute</param>
        /// <param name="post">A callback method to invoke upon query completion</param>
        /// <returns>A <see cref="TweetSharpResult"/> with the result of the query</returns>
        protected internal virtual T Execute<T>(TFluent query, Action<TResult> post) where T : class
        {
            var result = query.Request();

            if (result != null && post != null)
            {
                post.Invoke(result);
            }

            return HandleResponse<T>(query, result);
        }

        /// <summary>
        /// Executes the query
        /// </summary>
        /// <param name="query">The query to execute</param>
        /// <param name="post">Callback method to invoke after the query</param>
        protected virtual void Execute(TFluent query, Action<TResult> post)
        {
            var result = query.Request();

            if (result != null && post != null)
            {
                post.Invoke(result);
            }

            HandleResponse(query, result);
        }

        /// <summary>
        /// Executes a query with TweetSharp
        /// </summary>
        /// <typeparam name="T">the response type</typeparam>
        /// <param name="executor">The fluent tree describing the query</param>
        /// <returns>The query response</returns>
        protected internal virtual T WithTweetSharp<T>(Func<TFluent, TNode> executor) where T : class
        {
            var query = GetAuthenticatedQuery();

            PrepareQuery<T>(query, executor);

            return Execute<T>(query, null);
        }

        /// <summary>
        /// Executes a query with TweetSharp
        /// </summary>
        /// <typeparam name="T">the response type</typeparam>
        /// <param name="executor">The fluent tree describing the query</param>
        /// <returns>The query response</returns>
        protected internal virtual T WithTweetSharp<T>(Func<TFluent, TFluent> executor) where T : class
        {
            var query = GetAuthenticatedQuery();

            executor.Invoke(query);

            return Execute<T>(query, null);
        }

        /// <summary>
        /// Executes a query with TweetSharp
        /// </summary>
        /// <param name="executor">The fluent tree describing the query</param>
        protected internal virtual void WithTweetSharp(Func<TFluent, TFluent> executor)
        {
            var query = GetAuthenticatedQuery();

            executor.Invoke(query);

            Execute(query, null);
        }

        /// <summary>
        /// Executes a query with TweetSharp
        /// </summary>
        /// <param name="executor">The fluent tree describing the query</param>
        /// <param name="post">A callback method to be invoked after the query completes</param>
        protected internal virtual void WithTweetSharp(Func<TFluent, TNode> executor, Action<TResult> post)
        {
            var query = GetAuthenticatedQuery();

            executor.Invoke(query);

            Execute(query, post);
        }

        /// <summary>
        /// Executes a query with TweetSharp
        /// </summary>
        /// <param name="executor">The fluent tree describing the query</param>
        /// <param name="post">A callback method to be invoked after the query completes</param>
        protected internal virtual void WithTweetSharp(Func<TFluent, TFluent> executor, Action<TResult> post)
        {
            var query = GetAuthenticatedQuery();

            executor.Invoke(query);

            Execute(query, post);
        }

        /// <summary>
        /// Executes a query with TweetSharp
        /// </summary>
        /// <param name="executor">The fluent tree describing the query</param>
        /// <param name="post">A callback method to be invoked after the query completes</param>
        protected internal virtual T WithTweetSharp<T>(Func<TFluent, TNode> executor, Action<TResult> post) where T : class
        {
            var query = GetAuthenticatedQuery();

            PrepareQuery<T>(query, executor);

            return Execute<T>(query, post);
        }
#endif

#if !WindowsPhone
        /// <summary>
        /// Asynchronously executes the query
        /// </summary>
        /// <param name="query">The query to execute</param>
        /// <param name="post">Callback method to invoke when execution is complete</param>
        /// <returns>An <see cref="IAsyncResult"/> handle for this method</returns>
        protected virtual IAsyncResult ExecuteAsync(TFluent query, Action<TResult> post)
        {
            return query.BeginRequest();
        }
#else
        /// <summary>
        /// Asynchronously executes the query
        /// </summary>
        /// <param name="query">The query to execute</param>
        /// <param name="post">Callback method to invoke when execution is complete</param>
        protected virtual void ExecuteAsync(TFluent query, Action<TResult> post)
        {
            query.BeginRequest();
        }
#endif

#if !WindowsPhone
        /// <summary>
        /// Executes a query with TweetSharp asynchronously
        /// </summary>
        /// <param name="executor">The fluent tree describing the query</param>
        protected internal virtual IAsyncResult WithTweetSharpAsync(Func<TFluent, TNode> executor)
        {
            var query = GetAuthenticatedQuery();

            executor.Invoke(query);

            var result = ExecuteAsync(query, null);

            return result;
        }
#else
        /// <summary>
        /// Executes a query with TweetSharp asynchronously
        /// </summary>
        /// <param name="executor">The fluent tree describing the query</param>
        protected internal virtual void WithTweetSharpAsync(Func<TFluent, TNode> executor)
        {
            var query = GetAuthenticatedQuery();

            executor.Invoke(query);

            ExecuteAsync(query, null);
        }
#endif

        /// <summary>
        /// Prepares the query for submission
        /// </summary>
        protected abstract void PrepareQuery<T>(TFluent query, Func<TFluent, TNode> executor);
        /// <summary>
        /// Handles the response
        /// </summary>
        protected abstract T HandleResponse<T>(TFluent query, TResult response) where T : class;
        /// <summary>
        /// Handles the response
        /// </summary>
        protected abstract void HandleResponse(TFluent query, TResult response);
    }
}