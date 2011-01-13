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
using TweetSharp.Model;

namespace TweetSharp.Fluent
{

    /// <summary>
    /// Generic interface for the fluent interface root nodes. 
    /// </summary>
    /// <typeparam name="T">The expected result type.  Must be derived from <see cref="TweetSharpResult"/></typeparam>
    public interface IFluentBase<T> : IRequestPattern<T> where T : TweetSharpResult
    {
        /// <summary>
        /// Result object describing the results of the query after it has been executed
        /// </summary>
        T Result { get; set; }
    }

    /// <summary>
    /// Interface describing the request pattern
    /// </summary>
    /// <typeparam name="T">The expected return type of the request</typeparam>
    public interface IRequestPattern<T>
    {
#if !SILVERLIGHT
        /// <summary>
        /// Begins a synchronous request to the service API
        /// </summary>
        /// <returns>The result of the request</returns>
        T Request();
#endif

#if !WindowsPhone
        /// <summary>
        /// Begins an asynchronous (background) request to the service API
        /// </summary>
        /// <returns>the <see cref="IAsyncResult"/> handle for the background operation</returns>
        IAsyncResult BeginRequest();

        /// <summary>
        /// Begins an asynchronous (background) request to the service API
        /// </summary>
        /// <returns>the <see cref="IAsyncResult"/> handle for the background operation</returns>
        IAsyncResult BeginRequest(object state);

        /// <summary>
        /// Completes an asynchronous operation, returning the result of the query
        /// </summary>
        /// <param name="asyncResult">The <see cref="IAsyncResult"/> handle returned from the 
        /// <see cref="BeginRequest()"/> method that started the background operation</param>
        /// <returns>The result of the query</returns>
        T EndRequest(IAsyncResult asyncResult);
#else
        /// <summary>
        /// Begins an asynchronous (background) request to the service API
        /// </summary>
        void BeginRequest();

        /// <summary>
        /// Begins an asynchronous (background) request to the service API
        /// </summary>
        void BeginRequest(object state);
#endif
    }
}