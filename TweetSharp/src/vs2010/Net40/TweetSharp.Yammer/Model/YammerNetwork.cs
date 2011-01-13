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
    /// Representation of a Yammer Network.
    /// </summary>
    [Serializable]
    [JsonObject(MemberSerialization.OptIn)]
    public class YammerNetwork : PropertyChangedBase, IYammerModel
    {
        private long _id;
        private string _name;
        private string _permaLink;
        private int _unseenMessageCount;

        /// <summary>
        /// Gets or sets the full web address to this network on Yammer.com.
        /// </summary>
        [JsonProperty("permalink")]
#if !Smartphone
        [DataMember]
#endif
        public virtual string PermaLink
        {
            get { return _permaLink; }
            set
            {
                if (_permaLink == value)
                {
                    return;
                }
                _permaLink = value;
                OnPropertyChanged("PermaLink");
            }
        }

        /// <summary>
        /// Gets or sets the number of messages not yet seen by the authenticating user. 
        /// </summary>
        [JsonProperty("unseen_message_count")]
#if !Smartphone
        [DataMember]
#endif
        public virtual int UnseenMessageCount
        {
            get { return _unseenMessageCount; }
            set
            {
                if (_unseenMessageCount == value)
                {
                    return;
                }
                _unseenMessageCount = value;
                OnPropertyChanged("UnseenMessageCount");
            }
        }

        /// <summary>
        /// Gets or sets the network name. 
        /// </summary>
        [JsonProperty("name")]
#if !Smartphone
        [DataMember]
#endif
        public virtual string Name
        {
            get { return _name; }
            set
            {
                if (_name == value)
                {
                    return;
                }
                _name = value;
                OnPropertyChanged("Name");
            }
        }

        /// <summary>
        /// Gets or sets the id of the network. 
        /// </summary>
        [JsonProperty("id")]
#if !Smartphone
        [DataMember]
#endif
        public virtual long Id
        {
            get { return _id; }
            set
            {
                if (_id == value)
                {
                    return;
                }
                _id = value;
                OnPropertyChanged("Id");
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