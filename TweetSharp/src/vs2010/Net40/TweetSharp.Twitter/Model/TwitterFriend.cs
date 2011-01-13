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
    /// Represents a relative relationship between two users on Twitter,
    /// including basic information from which to locate more info for each user.
    /// </summary>
    [Serializable]
#endif
#if !Smartphone
    [DataContract]
#endif
    [JsonObject(MemberSerialization.OptIn)]
    public class TwitterFriend : PropertyChangedBase,
                                 ITwitterModel
    {
        private long _id;
        private string _screenName;
        private bool _following;
        private bool _followedBy;
        private bool? _notificationsEnabled;

#if !Smartphone
        /// <summary>
        /// Gets or sets the ID of the friendship recipient.
        /// </summary>
        /// <value>The friendship recipient's ID.</value>
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
        /// Gets or sets the screen name of the friendship recipient.
        /// </summary>
        /// <value>The friendship recipient's screen name.</value>
        [DataMember]
#endif
        [JsonProperty("screen_name")]
        public virtual string ScreenName
        {
            get { return _screenName; }
            set
            {
                if (_screenName == value)
                {
                    return;
                }

                _screenName = value;
                OnPropertyChanged("ScreenName");
            }
        }

#if !Smartphone
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="TwitterFriend"/> is following
        /// the other <see cref="TwitterFriend"/> defined in a <see cref="TwitterRelationship" />.
        /// </summary>
        /// <value><c>true</c> if following; otherwise, <c>false</c>.</value>
        [DataMember]
#endif
        [JsonProperty("following")]
        public virtual bool Following
        {
            get { return _following; }
            set
            {
                if (_following == value)
                {
                    return;
                }

                _following = value;
                OnPropertyChanged("Following");
            }
        }

#if !Smartphone
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="TwitterFriend"/> is followed by
        /// the other <see cref="TwitterFriend"/> defined in a <see cref="TwitterRelationship" />.
        /// </summary>
        /// <value><c>true</c> if followed by; otherwise, <c>false</c>.</value>
        [DataMember]
#endif
        [JsonProperty("followed_by")]
        public virtual bool FollowedBy
        {
            get { return _followedBy; }
            set
            {
                if (_followedBy == value)
                {
                    return;
                }

                _followedBy = value;
                OnPropertyChanged("FollowedBy");
            }
        }

#if !Smartphone
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="TwitterFriend"/> is receiving
        /// notifications for the other <see cref="TwitterFriend"/> defined in a <see cref="TwitterRelationship" />.
        /// </summary>
        /// <value><c>true</c> if receiving notifications; otherwise, <c>false</c>.</value>
        [DataMember]
#endif
        [JsonProperty("notifications_enabled")]
        public virtual bool? NotificationsEnabled
        {
            get { return _notificationsEnabled; }
            set
            {
                if (_notificationsEnabled == value)
                {
                    return;
                }

                _notificationsEnabled = value;
                OnPropertyChanged("NotificationsEnabled");
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