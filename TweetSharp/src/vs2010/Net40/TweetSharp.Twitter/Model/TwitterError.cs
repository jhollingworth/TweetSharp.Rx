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
using System.Diagnostics;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace TweetSharp.Twitter.Model
{
#if !SILVERLIGHT
    /// <summary>
    /// Represents an error received from the Twitter API.
    /// </summary>
    [Serializable]
#endif
#if !Smartphone
    [DataContract]
    [DebuggerDisplay("{ErrorMessage} ({Request})")]
#endif
    [JsonObject(MemberSerialization.OptIn)]
    public class TwitterError : ITwitterModel
    {
        /// <summary>
        /// Gets or sets the request hash.
        /// This is typically the URL slug for the API method
        /// that issued the error.
        /// </summary>
        /// <value>The request.</value>
        [JsonProperty("request")]
#if !Smartphone
        [DataMember]
#endif
        public virtual string Request { get; set; }

        /// <summary>
        /// Gets or sets the error code. 
        /// This appears when errors appear as a collection.
        /// It is part of the 'new' errors schema.
        /// </summary>
        [JsonProperty("code")]
        public virtual int Code { get; set; }

        /// <summary>
        /// Gets or sets the error message. 
        /// This appears when errors appear as a collection.
        /// It is part of the 'new' errors schema.
        /// It is functionally the same as the previous "error" element.
        /// </summary>
        [JsonProperty("message")]
        internal virtual string Message { get; set; }

        /// <summary>
        /// Gets or sets the error message returned from Twitter.
        /// It is part of the 'old' errors schema.
        /// </summary>
        /// <value>The error message.</value>
        [JsonProperty("error")]
#if !Smartphone
        [DataMember]
#endif
        internal virtual string Error { get; set; }

        /// <summary>
        /// Gets the value of either the Error element or the
        /// Message element, depending on the schema of the original 
        /// error that was received by Twitter.
        /// </summary>
        public virtual string ErrorMessage
        {
            get
            {
                return string.IsNullOrEmpty(Error) ? Message : Error;
            }
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return ErrorMessage;
        }

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