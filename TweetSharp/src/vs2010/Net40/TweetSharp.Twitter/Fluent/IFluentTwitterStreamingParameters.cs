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
using TweetSharp.Twitter.Model;

namespace TweetSharp.Twitter.Fluent
{
    /// <summary>
    /// Parameters for a streaming query request.
    /// </summary>
    public interface IFluentTwitterStreamingParameters
    {
        /// <summary>
        /// Gets or sets the number of results to wait for before ending the stream.
        /// </summary>
        int? Count { get; set; }

        /// <summary>
        /// Gets or sets the delimiter length.
        /// </summary>
        int? Length { get; set; }

        /// <summary>
        /// Gets or sets user IDs used to filter the stream.
        /// </summary>
        IEnumerable<int> UserIds { get; set; }

        /// <summary>
        /// Gets or sets the keywords used to filter the stream.
        /// </summary>
        IEnumerable<string> Keywords { get; set; }

        /// <summary>
        /// Gets or sets the geo locations used to filter the stream.
        /// </summary>
        IEnumerable<TwitterGeoLocation> Locations { get; set; }

        /// <summary>
        /// Gets or sets the amount of time to stream results before closing the stream.
        /// </summary>
        TimeSpan? Duration { get; set; }

        /// <summary>
        /// Gets or sets the callback to invoke when the stream reaches the configured batch size.
        /// </summary>
        TwitterWebCallback Callback { get; set; }

        /// <summary>
        /// Gets or sets the number of results to receive between callback invocations.
        /// </summary>
        int? ResultsPerCallback { get; set; }
    }
}