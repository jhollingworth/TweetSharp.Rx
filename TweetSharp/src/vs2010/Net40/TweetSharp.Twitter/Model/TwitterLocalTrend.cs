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

using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace TweetSharp.Twitter.Model
{
    /// <summary>
    /// Describes a local trend
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class TwitterLocalTrend
    {
        /// <summary>
        /// Gets or sets the search compatible query used to search for stauses with this trending topic
        /// </summary>
        [JsonProperty("query")]
#if !Smartphone
        [DataMember]
#endif
        public virtual string Query { get; set; }

        /// <summary>
        /// Gets or sets the display name for the trend
        /// </summary>
        [JsonProperty("name")]
#if !Smartphone
        [DataMember]
#endif
        public virtual string Name { get; set; }

        /// <summary>
        /// Gets or sets the twitter search site url for querying this trend in a browser
        /// </summary>
        [JsonProperty("url")]
#if !Smartphone
        [DataMember]
#endif
        public virtual string Url { get; set; }
    }
}