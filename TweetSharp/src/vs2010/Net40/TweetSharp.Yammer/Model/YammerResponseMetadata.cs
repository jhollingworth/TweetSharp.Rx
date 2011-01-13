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
using System.Runtime.Serialization;
using Newtonsoft.Json;
using TweetSharp.Model;

namespace TweetSharp.Yammer.Model
{
    /// <summary>
    /// Class representing metadata associated with a Yammer query response.
    /// </summary>
    [Serializable]
    [JsonObject(MemberSerialization.OptIn)]
    public class YammerResponseMetadata : PropertyChangedBase, IYammerModel
    {
        private long _currentUserId;
        private int _requestedPollInterval;

        /// <summary>
        /// The amount of time (in seconds) that Yammer requests you wait between polls
        /// </summary>
        [JsonProperty("requested_poll_interval")]
#if !Smartphone
        [DataMember]
#endif
        public virtual int RequestedPollInterval
        {
            get { return _requestedPollInterval; }
            set
            {
                if (_requestedPollInterval == value)
                {
                    return;
                }
                _requestedPollInterval = value;
                OnPropertyChanged("int");
            }
        }

        /// <summary>
        /// the ID of the user who made the request
        /// </summary>
        [JsonProperty("current_user_id")]
#if !Smartphone
        [DataMember]
#endif
        public virtual long CurrentUserId
        {
            get { return _currentUserId; }
            set
            {
                if (_currentUserId == value)
                {
                    return;
                }
                _currentUserId = value;
                OnPropertyChanged("CurrentUserId");
            }
        }

#if !Smartphone
        /// <summary>
        /// The source content used to deserialize the model entity instance.
        /// Can be XML or JSON, depending on the endpoint used.
        /// </summary>
        [DataMember]
#endif
        public virtual string RawSource { get; set; }

        /// <summary>
        /// Gets or sets an enumeration of references to <see cref="YammerUser">users</see> made in this response.
        /// </summary>
        public virtual IEnumerable<YammerUserReference> UserReferences { get; set; }

        /// <summary>
        /// Gets or sets an enumeration of references to <see cref="YammerThread">threads</see> made in this response.
        /// </summary>
        public virtual IEnumerable<YammerThreadReference> ThreadReferences { get; set; }

        /// <summary>
        /// Gets or sets an enumeration of references to <see cref="YammerTag">tags</see> made in this response.
        /// </summary>
        public virtual IEnumerable<YammerTagReference> TagReferences { get; set; }

        /// <summary>
        /// Gets or sets an enumeration of references to <see cref="YammerUser">guides</see> made in this response.
        /// </summary>
        public virtual IEnumerable<YammerGuideReference> GuideReferences { get; set; }
    }
}