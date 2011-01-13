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
using System.Runtime.Serialization;
using Newtonsoft.Json;
using TweetSharp.Model;

namespace TweetSharp.Yammer.Model
{
    /// <summary>
    /// Reprentation of <see cref="YammerMessage"/> thread statistics. 
    /// </summary>
    [Serializable]
    [JsonObject(MemberSerialization.OptIn)]
    public class YammerThreadStats : PropertyChangedBase, IYammerModel
    {
        private DateTime _lastReplyAt;
        private int _updates;

        /// <summary>
        /// Gets or sets the number of updates to the thread. 
        /// </summary>
        [JsonProperty("updates")]
#if !Smartphone
        [DataMember]
#endif
        public virtual int Updates
        {
            get { return _updates; }
            set
            {
                if (_updates == value)
                {
                    return;
                }
                _updates = value;
                OnPropertyChanged("Updates");
            }
        }

        /// <summary>
        /// Gets or sets the date and time of the last reply in the thread. 
        /// </summary>
        [JsonProperty("last_reply_at")]
#if !Smartphone
        [DataMember]
#endif
            public DateTime LastReplyAt
        {
            get { return _lastReplyAt; }
            set
            {
                if (_lastReplyAt == value)
                {
                    return;
                }
                _lastReplyAt = value;
                OnPropertyChanged("LastReplyAt");
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
    }
}