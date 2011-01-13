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
    /*     
     <list>
        <id>2029636</id>
        <name>firemen</name>
        <full_name>@twitterapidocs/firemen</full_name>
        <slug>firemen</slug>
        <subscriber_count>0</subscriber_count>
        <member_count>0</member_count>
        <uri>/twitterapidocs/firemen</uri>
        <mode>public</mode>    
        <user/>
     </list>     
     */

#if !SILVERLIGHT
    /// <summary>
    /// Represents a user-curated list of Twitter members, 
    /// that other users can subscribe to and see the aggregated 
    /// list of member tweets in a dedicated timeline.
    /// </summary>
    [Serializable]
#endif
#if !Smartphone
    [DataContract]
#endif
    [JsonObject(MemberSerialization.OptIn)]
    public class TwitterList : PropertyChangedBase, ITwitterModel
    {
        private long _id;
        private string _name;
        private string _fullName;
        private string _slug;
        private string _description;
        private int _subscriberCount;
        private int _memberCount;
        private string _uri;
        private string _mode;
        private TwitterUser _user;

#if !Smartphone
        /// <summary>
        /// Gets or sets the ID of the list.
        /// </summary>
        /// <value>The list ID.</value>
        [DataMember]
#endif
        [JsonProperty("id")]
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
        /// Gets or sets the descriptive name of the list.
        /// </summary>
        /// <value>The name.</value>
        [DataMember]
#endif
        [JsonProperty("name")]
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

#if !Smartphone
        /// <summary>
        /// Gets or sets the full name of the list including the list owner.
        /// </summary>
        /// <value>The full name of the list.</value>
        [DataMember]
#endif
        [JsonProperty("full_name")]
        public virtual string FullName
        {
            get { return _fullName; }
            set
            {
                if (_fullName == value)
                {
                    return;
                }

                _fullName = value;
                OnPropertyChanged("FullName");
            }
        }

#if !Smartphone
        /// <summary>
        /// Gets or sets the user-supplied list description.
        /// </summary>
        /// <value>The list description.</value>
        [DataMember]
#endif
        [JsonProperty("description")]
        public virtual string Description
        {
            get { return _description; }
            set
            {
                if (_description == value)
                {
                    return;
                }

                _description = value;
                OnPropertyChanged("Description");
            }
        }
        
#if !Smartphone
        /// <summary>
        /// Gets or sets the list URL slug.
        /// </summary>
        /// <value>The list slug.</value>
        [DataMember]
#endif
        [JsonProperty("slug")]
        public virtual string Slug
        {
            get { return _slug; }
            set
            {
                if (_slug == value)
                {
                    return;
                }

                _slug = value;
                OnPropertyChanged("Slug");
            }
        }

#if !Smartphone
        /// <summary>
        /// Gets or sets the subscriber count.
        /// A subscriber follows the list's updates.
        /// </summary>
        /// <value>The subscriber count.</value>
        [DataMember]
#endif
        [JsonProperty("subscriber_count")]
        public virtual int SubscriberCount
        {
            get { return _subscriberCount; }
            set
            {
                if (_subscriberCount == value)
                {
                    return;
                }

                _subscriberCount = value;
                OnPropertyChanged("SubscriberCount");
            }
        }

#if !Smartphone
        /// <summary>
        /// Gets or sets the member count.
        /// A member's updates appear in the list.
        /// </summary>
        /// <value>The member count.</value>
        [DataMember]
#endif
        [JsonProperty("member_count")]
        public virtual int MemberCount
        {
            get { return _memberCount; }
            set
            {
                if (_memberCount == value)
                {
                    return;
                }

                _memberCount = value;
                OnPropertyChanged("MemberCount");
            }
        }

#if !Smartphone
        /// <summary>
        /// Gets or sets the URI of the list.
        /// </summary>
        /// <value>The URI of the list.</value>
        [DataMember]
#endif
        [JsonProperty("uri")]
        public virtual string Uri
        {
            get { return _uri; }
            set
            {
                if (_uri == value)
                {
                    return;
                }

                _uri = value;
                OnPropertyChanged("Uri");
            }
        }

#if !Smartphone
        /// <summary>
        /// Gets or sets the mode.
        /// The list can be "public", visible to everyone,
        /// or "private", visible only to the authenticating user.
        /// </summary>
        /// <value>The mode.</value>
        [DataMember]
#endif
        [JsonProperty("mode")]
        public virtual string Mode
        {
            get { return _mode; }
            set
            {
                if (_mode == value)
                {
                    return;
                }

                _mode = value;
                OnPropertyChanged("Mode");
            }
        }

#if !Smartphone
        /// <summary>
        /// Gets or sets the user who created the list.
        /// </summary>
        /// <value>The user.</value>
        [DataMember]
#endif
        [JsonProperty("user")]
        public virtual TwitterUser User
        {
            get { return _user; }
            set
            {
                if (_user == value)
                {
                    return;
                }

                _user = value;
                OnPropertyChanged("User");
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