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

namespace TweetSharp.Yammer.Model
{
    /// <summary>
    /// Representation of a Yammer search result. 
    /// </summary>
    [Serializable]
    [JsonObject(MemberSerialization.OptIn)]
    public class YammerSearchResult : YammerObjectBase, IYammerModel
    {
        private IEnumerable<YammerGroup> _groups = new List<YammerGroup>();
        private IEnumerable<YammerMessage> _messages = new List<YammerMessage>();
        private IEnumerable<YammerTag> _tags = new List<YammerTag>();
        private IEnumerable<YammerUser> _users = new List<YammerUser>();

        /// <summary>
        /// Gets or sets an enumeration of Users found by the search.
        /// </summary>
        [JsonProperty("users")]
#if !Smartphone
        [DataMember]
#endif
        public virtual IEnumerable<YammerUser> Users
        {
            get { return _users; }
            set
            {
                if (_users == value)
                {
                    return;
                }
                _users = value;
                OnPropertyChanged("Users");
            }
        }

        /// <summary>
        /// Gets or sets an enumeration of Tags found by the search.
        /// </summary>
        [JsonProperty("Tags")]
#if !Smartphone
        [DataMember]
#endif
        public virtual IEnumerable<YammerTag> Tags
        {
            get { return _tags; }
            set
            {
                if (_tags == value)
                {
                    return;
                }
                _tags = value;
                OnPropertyChanged("Tags");
            }
        }

        /// <summary>
        /// Gets or sets an enumeration of Groups returned by the search.
        /// </summary>
        [JsonProperty("Groups")]
#if !Smartphone
        [DataMember]
#endif
        public virtual IEnumerable<YammerGroup> Groups
        {
            get { return _groups; }
            set
            {
                if (_groups == value)
                {
                    return;
                }
                _groups = value;
                OnPropertyChanged("Groups");
            }
        }

        /// <summary>
        /// Gets or sets an enumeration of messages found by the search.
        /// </summary>
        [JsonProperty("messages")]
#if !Smartphone
        [DataMember]
#endif
        public virtual IEnumerable<YammerMessage> Messages
        {
            get { return _messages; }
            set
            {
                if (_messages == value)
                {
                    return;
                }
                _messages = value;
                OnPropertyChanged("Messages");
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