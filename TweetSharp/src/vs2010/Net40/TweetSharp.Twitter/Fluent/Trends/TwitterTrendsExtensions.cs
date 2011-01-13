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

namespace TweetSharp.Twitter.Fluent
{
    /// <summary>
    /// Fluent interface methods for the trends endpoints
    /// </summary>
    public static class TwitterTrendsExtensions
    {
        /// <summary>
        /// Request the availabe trending topics
        /// </summary>
        /// <param name="instance">the instance</param>
        /// <returns></returns>
        public static ITwitterTrendsAvailable GetAvailable(this ITwitterTrends instance)
        {
            instance.Root.Parameters.Action = "available";
            return new TwitterTrendsAvailable(instance.Root);
        }

        /// <summary>
        /// Request the trending topics for a specific location
        /// </summary>
        /// <param name="instance">the instance</param>
        /// <param name="woeId">The Yahoo WhereOnEarth id corresponding to the location to request trends for</param>
        /// <seealso>http://developer.yahoo.com/geo/geoplanet/"</seealso>
        /// <returns></returns>
        public static ITwitterTrendsLocation ByLocation(this ITwitterTrends instance, long woeId)
        {
            instance.Root.Parameters.Action = "location";
            instance.Root.TrendsParameters.WoeId = woeId;
            return new TwitterTrendsLocation(instance.Root);
        }
    }
}