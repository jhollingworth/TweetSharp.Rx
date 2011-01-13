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
using TweetSharp.Core.Extensions;
using TweetSharp.Model;
using Newtonsoft.Json;

namespace TweetSharp.Twitter.Model
{
#if !SILVERLIGHT
    /// <summary>
    /// This data class represents a <see cref="TwitterStatus" /> originating from a Search API result.
    /// It is largely similar, but does not derive from the REST API's <see cref="TwitterStatus" />.
    /// </summary>
    [Serializable]
#endif
#if !Smartphone
    [DataContract]
    [DebuggerDisplay("{FromUserScreenName}: {Text}")]
#endif
    [JsonObject(MemberSerialization.OptIn)]
    public class TwitterSearchStatus : PropertyChangedBase,
                                       IComparable<TwitterSearchStatus>,
                                       IEquatable<TwitterSearchStatus>,
                                       ITwitterModel,
                                       ITweetable
    {
        private DateTime _createdAt;
        private int _fromUserId;
        private string _fromUserScreenName;
        private long _id;
        private string _isoLanguageCode;
        private string _profileImageUrl;
        private long _sinceId;
        private string _source;
        private string _text;
        private int? _toUserId;
        private string _toUserScreenName;
        private string _location;
        private TwitterGeoLocation _geoLocation;

        /// <summary>
        /// Gets or sets the ID of this tweet. 
        /// As seen through the Search API.
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

        /// <summary>
        /// Gets the text of the update.
        /// </summary>
        /// <value></value>
        [JsonProperty("text")]
#if !Smartphone
        [DataMember]
#endif
        public virtual string Text
        {
            get { return _text; }
            set
            {
                if (_text == value)
                {
                    return;
                }

                _text = value;
                OnPropertyChanged("Text");
            }
        }

        /// <summary>
        /// Calculates the HTML value of the <see cref="Text" />
        /// by parsing URLs, mentions, and hashtags into anchors.
        /// </summary>
        /// <value>The HTML text.</value>
        public virtual string TextHtml
        {
            get { return Text.ParseTwitterageToHtml(); }
        }

        /// <summary>
        /// Returns the URLs embedded in the <see cref="Text" /> value.
        /// </summary>
        /// <value>The <see cref="Uri" /> values embedded in <see cref="Text" />.</value>
        public virtual IEnumerable<Uri> TextLinks
        {
            get { return Text.ParseTwitterageToUris(); }
        }

        /// <summary>
        /// Returns the <see cref="TwitterUser.ScreenName" /> values mentioned in the <see cref="Text" /> value.
        /// </summary>
        /// <value>The <see cref="TwitterUser.ScreenName" /> values mentioned in <see cref="Text" />.</value>
        public virtual IEnumerable<string> TextMentions
        {
            get { return Text.ParseTwitterageToScreenNames(); }
        }

        /// <summary>
        /// Returns the hashtag values used in the <see cref="Text" /> value.
        /// </summary>
        /// <value>The hashtag values used in <see cref="Text" />.</value>
        public virtual IEnumerable<string> TextHashtags
        {
            get { return Text.ParseTwitterageToHashtags(); }
        }

        /// <summary>
        /// Gets or sets the source of the user's tweet.
        /// This is the URL and name of the Twitter client 
        /// the user used to publish this <seealso cref="TwitterStatus" />.
        /// </summary>
        /// <value>The source.</value>
        [JsonProperty("source")]
#if !Smartphone
        [DataMember]
#endif
        public virtual string Source
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

        /// <summary>
        /// Gets the <see cref="DateTime"/> that the status was posted to the web
        /// </summary>
        /// <value></value>
        [JsonProperty("created_at")]
#if !Smartphone
        [DataMember]
#endif
        public virtual DateTime CreatedDate
        {
            get { return _createdAt; }
            set
            {
                if (_createdAt == value)
                {
                    return;
                }

                _createdAt = value;
                OnPropertyChanged("CreatedDate");
            }
        }

        /// <summary>
        /// Gets or sets the internal ID for the user who received a status in a search
        /// result. Keep in mind that this ID is currently meaningless to the Twitter API
        /// as it is not the same ID as the user's ID. It will eventually provide the
        /// correct ID for the target user.
        /// </summary>
        [JsonProperty("to_user_id")]
#if !Smartphone
        [DataMember]
#endif
        [Obsolete("This property is currently erroneous as it contains an internal ID")]
        public virtual int? ToUserId
        {
            get { return _toUserId; }
            set
            {
                if (_toUserId == value)
                {
                    return;
                }

                _toUserId = value;
                OnPropertyChanged("ToUserId");
            }
        }

        /// <summary>
        /// Gets or sets the internal ID for the user who wrote a status in a search
        /// result. Keep in mind that this ID is currently meaningless to the Twitter API
        /// as it is not the same ID as the user's ID. It will eventually provide the
        /// correct ID for the originating user.
        /// </summary>
        [JsonProperty("from_user_id")]
#if !Smartphone
        [DataMember]
#endif
        [Obsolete("This property is currently erroneous as it contains an internal ID")]
        public int? FromUserId
        {
            get { return _fromUserId; }
            set
            {
                if (_fromUserId == value)
                {
                    return;
                }

                if (value == null)
                {
                    return;
                }
                
                _fromUserId = (int) value;
                OnPropertyChanged("FromUserId");
            }
        }

        /// <summary>
        /// Gets or sets the screen name of the authoring user.
        /// </summary>
        /// <value>The the screen name of the authoring user.</value>
        [JsonProperty("from_user")]
#if !Smartphone
        [DataMember]
#endif
            public string FromUserScreenName
        {
            get { return _fromUserScreenName; }
            set
            {
                if (_fromUserScreenName == value)
                {
                    return;
                }

                _fromUserScreenName = value;
                OnPropertyChanged("FromUserScreenName");
            }
        }

        /// <summary>
        /// Gets or sets the screen name of the recipient user.
        /// </summary>
        /// <value>The the screen name of the recipient user.</value>
        [JsonProperty("to_user")]
#if !Smartphone
        [DataMember]
#endif
            public string ToUserScreenName
        {
            get { return _toUserScreenName; }
            set
            {
                if (_toUserScreenName == value)
                {
                    return;
                }

                _toUserScreenName = value;
                OnPropertyChanged("ToUserScreenName");
            }
        }

        /// <summary>
        /// Gets or sets the known ISO language code for this tweet, if any.
        /// </summary>
        /// <value>The known ISO language code.</value>
        [JsonProperty("iso_language_code")]
#if !Smartphone
        [DataMember]
#endif
            public string IsoLanguageCode
        {
            get { return _isoLanguageCode; }
            set
            {
                if (_isoLanguageCode == value)
                {
                    return;
                }

                _isoLanguageCode = value;
                OnPropertyChanged("IsoLanguageCode");
            }
        }

        /// <summary>
        /// Gets or sets the authoring user's profile image URL.
        /// </summary>
        /// <value>The authoring user's profile image URL.</value>
        [JsonProperty("profile_image_url")]
#if !Smartphone
        [DataMember]
#endif
            public string ProfileImageUrl
        {
            get { return _profileImageUrl; }
            set
            {
                if (_profileImageUrl == value)
                {
                    return;
                }

                _profileImageUrl = value;
                OnPropertyChanged("ProfileImageUrl");
            }
        }

        /// <summary>
        /// Gets or sets the ID of the tweet this tweet came after.
        /// </summary>
        /// <value>The since ID.</value>
        [JsonProperty("since_id")]
#if !Smartphone
        [DataMember]
#endif
            public long SinceId
        {
            get { return _sinceId; }
            set
            {
                if (_sinceId == value)
                {
                    return;
                }

                _sinceId = value;
                OnPropertyChanged("SinceId");
            }
        }

        /// <summary>
        /// Gets or sets the user's self-described location.
        /// </summary>
        /// <value>The user's self-described location.</value>
        [JsonProperty("location")]
#if !Smartphone
        [DataMember]
#endif
            public string Location
        {
            get { return _location; }
            set
            {
                if (_location == value)
                {
                    return;
                }

                _location = value;
                OnPropertyChanged("Location");
            }
        }

        /// <summary>
        /// Gets or sets the <seealso cref="TwitterGeoLocation" />
        /// that this tweet came from, if any.
        /// </summary>
        /// <value>The location.</value>
        [JsonProperty("geo")]
#if !Smartphone
        [DataMember]
#endif
        public TwitterGeoLocation GeoLocation
        {
            get { return _geoLocation; }
            set
            {
                if (_geoLocation == value)
                {
                    return;
                }

                _geoLocation = value;
                OnPropertyChanged("TwitterGeoLocation");
            }
        }

        #region IComparable<TwitterSearchStatus> Members

        /// <summary>
        /// Compares the current object with another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// A 32-bit signed integer that indicates the relative order of the objects being compared. The return value has the following meanings: Value Meaning Less than zero This object is less than the <paramref name="other"/> parameter.Zero This object is equal to <paramref name="other"/>. Greater than zero This object is greater than <paramref name="other"/>.
        /// </returns>
        public int CompareTo(TwitterSearchStatus other)
        {
            return other.Id == Id ? 0 : other.Id > Id ? -1 : 1;
        }

        #endregion

        #region IEquatable<TwitterSearchStatus> Members

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="status">The status.</param>
        /// <returns>
        /// true if the current object is equal to the <paramref name="status"/> parameter; otherwise, false.
        /// </returns>
        public bool Equals(TwitterSearchStatus status)
        {
            if (ReferenceEquals(null, status))
            {
                return false;
            }
            if (ReferenceEquals(this, status))
            {
                return true;
            }
            return status.Id == Id;
        }

        #endregion

        /// <summary>
        /// Determines whether the specified <see cref="System.Object"/> is equal to this instance.
        /// </summary>
        /// <param name="status">The <see cref="System.Object"/> to compare with this instance.</param>
        /// <returns>
        /// 	<c>true</c> if the specified <see cref="System.Object"/> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object status)
        {
            if (ReferenceEquals(null, status))
            {
                return false;
            }
            if (ReferenceEquals(this, status))
            {
                return true;
            }
            return status.GetType() == typeof (TwitterSearchStatus) && Equals((TwitterSearchStatus) status);
        }

        /// <summary>
        /// Serves as a hash function for a particular type.
        /// </summary>
        /// <returns>
        /// A hash code for the current <see cref="T:System.Object"/>.
        /// </returns>
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator ==(TwitterSearchStatus left, TwitterSearchStatus right)
        {
            return Equals(left, right);
        }

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator !=(TwitterSearchStatus left, TwitterSearchStatus right)
        {
            return !Equals(left, right);
        }

        ///<summary>
        /// This implicit conversion supports treating a search status as if it were a 
        /// regular <see cref="TwitterStatus" />. This is useful if you need to combine search
        /// results and regular results in the same UI context.
        ///</summary>
        ///<param name="searchStatus"></param>
        ///<returns></returns>
        public static implicit operator TwitterStatus(TwitterSearchStatus searchStatus)
        {
            var user = new TwitterUser
                           {
                               ProfileImageUrl = searchStatus.ProfileImageUrl,
                               ScreenName = searchStatus.FromUserScreenName
                           };

            var status = new TwitterStatus
                             {
                                 CreatedDate = searchStatus.CreatedDate,
                                 Id = searchStatus.Id,
                                 Source = searchStatus.Source,
                                 Text = searchStatus.Text,
                                 User = user
                             };

            return status;
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