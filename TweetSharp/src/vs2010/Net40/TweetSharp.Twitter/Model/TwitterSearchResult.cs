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
using System.Diagnostics;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace TweetSharp.Twitter.Model
{
#if !SILVERLIGHT
    /// <summary>
    /// The results of a request to the Search API.
    /// </summary>
    [Serializable]
#endif
#if !Smartphone
    [DataContract]
    [DebuggerDisplay("{ResultsPerPage} results on page {Page} from {Source}")]
#endif
    [JsonObject(MemberSerialization.OptIn)]
    public class TwitterSearchResult : ITwitterModel
    {
        /// <summary>
        /// Gets or sets the search statuses that comprise the actual query results.
        /// </summary>
        /// <value>The statuses.</value>
        [JsonProperty("results")]
#if !Smartphone
        [DataMember]
#endif
        public virtual List<TwitterSearchStatus> Statuses { get; set; }

        /// <summary>
        /// Gets or sets the last ID found in the result, to be used
        /// with subsequent queries that want to find tweets after these results.
        /// </summary>
        /// <value>The since ID.</value>
        [JsonProperty("since_id")]
#if !Smartphone
        [DataMember]
#endif
        public virtual long SinceId { get; set; }

        /// <summary>
        /// Gets or sets the first ID found in the result, to be used
        /// with subsequent queries that want to find tweets before these results.
        /// </summary>
        /// <value>The max ID.</value>
        [JsonProperty("max_id")]
#if !Smartphone
        [DataMember]
#endif
        public virtual long MaxId { get; set; }

        /// <summary>
        /// Gets or sets the refresh URL to use for web applications and widgets
        /// to obtain more results from the same query.
        /// </summary>
        /// <value>The refresh URL.</value>
        [JsonProperty("refresh_url")]
#if !Smartphone
        [DataMember]
#endif
        public virtual string RefreshUrl { get; set; }

        /// <summary>
        /// Gets or sets the number of results per page issued
        /// when this query was executed.
        /// </summary>
        /// <value>The results per page.</value>
        [JsonProperty("results_per_page")]
#if !Smartphone
        [DataMember]
#endif
        public virtual int ResultsPerPage { get; set; }

        /// <summary>
        /// Gets or sets the URL path slug to use to build a subsequent query
        /// that returns the next page of this query's results.
        /// </summary>
        /// <value>The next page.</value>
        [JsonProperty("next_page")]
#if !Smartphone
        [DataMember]
#endif
        public virtual string NextPage { get; set; }

        /// <summary>
        /// Gets or sets the URL path slug to use to build a subsequent query
        /// that returns the previous page of this query's results.
        /// </summary>
        /// <value>The previous page.</value>
        [JsonProperty("previous_page")]
#if !Smartphone
        [DataMember]
#endif
        public virtual string PreviousPage { get; set; }

        /// <summary>
        /// Gets or sets the server-side execution time of the query.
        /// </summary>
        /// <value>The completed in.</value>
        [JsonProperty("completed_in")]
#if !Smartphone
        [DataMember]
#endif
        public virtual double CompletedIn { get; set; }

        /// <summary>
        /// Gets or sets the page of results this
        /// query represents, from other pages with the same
        /// query parameters.
        /// </summary>
        /// <value>The page.</value>
        [JsonProperty("page")]
#if !Smartphone
        [DataMember]
#endif
        public virtual int Page { get; set; }

        /// <summary>
        /// Gets or sets the query used to generate this result.
        /// </summary>
        /// <value>The query.</value>
        [JsonProperty("query")]
#if !Smartphone
        [DataMember]
#endif
        public virtual string Query { get; set; }

        /// <summary>
        /// Gets or sets the warning message issued by Twitter for these results, if any.
        /// </summary>
        /// <value>The warning.</value>
        [JsonProperty("warning")]
#if !Smartphone
        [DataMember]
#endif
        public virtual string Warning { get; set; }

        /// <summary>
        /// Gets or sets the consumer source of the issued search.
        /// This is typically the name and URL of the application the search originated from.
        /// </summary>
        /// <value>The source.</value>
        [JsonProperty("source")]
#if !Smartphone
        [DataMember]
#endif
        public virtual string Source { get; set; }

        /// <summary>
        /// Gets or sets the total number of search results returned from this query.
        /// </summary>
        /// <value>The total number of results.</value>
        [JsonProperty("total")]
#if !Smartphone
        [DataMember]
#endif
        public virtual int Total { get; set; }

#if !Smartphone
        /// <summary>
        /// The source content used to deserialize the model entity instance.
        /// Can be XML or JSON, depending on the endpoint used.
        /// </summary>
        [DataMember]
#endif
        public virtual string RawSource { get; set; }
    }
}