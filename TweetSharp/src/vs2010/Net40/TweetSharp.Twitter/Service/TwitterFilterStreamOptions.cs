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

namespace TweetSharp.Twitter.Service
{
#if !SILVERLIGHT
    /// <summary>
    /// Options for filtering the Twitter API Filter stream.
    /// </summary>
    [Serializable]
#endif
    public class TwitterFilterStreamOptions
    {
        /// <summary>
        /// Gets or sets the tracking keywords.
        /// </summary>
        /// <value>The tracking keywords.</value>
        public virtual ICollection<string> TrackingKeywords { get; set; }

        /// <summary>
        /// Gets or sets the following user ids.
        /// </summary>
        /// <value>The following user ids.</value>
        public virtual ICollection<int> FollowingUserIds { get; set; }

        /// <summary>
        /// Gets or sets the bounding geo locations.
        /// </summary>
        /// <value>The bounding geo locations.</value>
        public virtual IEnumerable<TwitterGeoLocation> BoundingGeoLocations { get; set; }
        
        /// <summary>
        /// Gets or sets the backlog.
        /// </summary>
        /// <value>The backlog.</value>
        public virtual int Backlog { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TwitterFilterStreamOptions"/> class.
        /// </summary>
        public TwitterFilterStreamOptions()
        {
            Initialize();
        }

        private void Initialize()
        {
            TrackingKeywords = new List<string>(0);
            FollowingUserIds = new List<int>(0);
            BoundingGeoLocations = new List<TwitterGeoLocation>(0);
        }
    }
}