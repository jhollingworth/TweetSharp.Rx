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

using TweetSharp.Fluent;

namespace TweetSharp.Twitter.Fluent
{
    /// <summary>
    /// A contract defining elements for Twitter API configuration.
    /// </summary>
    public interface IFluentTwitterConfiguration : IFluentConfiguration
    {
        /// <summary>
        /// Gets the query root.
        /// </summary>
        /// <value>The query root.</value>
        IFluentTwitter Root { get; }

        /// <summary>
        /// Gets or sets a value determining if tweets longer than 140 characters
        /// are truncated before being sent.
        /// </summary>
        bool TruncateUpdates { get; set; }

        /// <summary>
        /// Gets or sets a value determing if client-side rate limiting rules are enforced.
        /// </summary>
        bool LimitRate { get; set; }
        
        /// <summary>
        /// Gets or sets a value determing if HTTPS communication is enforced.
        /// </summary>
        bool UseHttps { get; set; }
    }
}