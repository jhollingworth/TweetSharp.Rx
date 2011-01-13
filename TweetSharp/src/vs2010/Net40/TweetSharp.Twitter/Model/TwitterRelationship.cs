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

namespace TweetSharp.Twitter.Model
{
#if !SILVERLIGHT
    /// <summary>
    /// Represents the relationship between two users on Twitter
    /// as they related to each <see cref="TwitterFriend" /> representation.
    /// </summary>
    [Serializable]
#endif
    [JsonObject(MemberSerialization.OptIn)]
    public class TwitterRelationship : PropertyChangedBase, ITwitterModel
    {
        private TwitterFriend _source;
        private TwitterFriend _target;

#if !Smartphone
        /// <summary>
        /// Gets or sets the relative source of the relationship.
        /// </summary>
        /// <value>The source.</value>
        [DataMember]
#endif
        [JsonProperty("source")]
        public virtual TwitterFriend Source
        {
            get { return _source; }
            set
            {
                if (_source == value)
                {
                    return;
                }

                _source = value;
                OnPropertyChanged("Source");
            }
        }

#if !Smartphone
        /// <summary>
        /// Gets or sets the relative target of the relationship.
        /// </summary>
        /// <value>The target.</value>
        [DataMember]
#endif
        [JsonProperty("target")]
        public virtual TwitterFriend Target
        {
            get { return _target; }
            set
            {
                if (_target == value)
                {
                    return;
                }

                _target = value;
                OnPropertyChanged("Target");
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