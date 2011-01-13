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
    /// The focal point of Twitter, this is the "tweet", or status update that
    /// a user posts to the service.
    /// </summary>
    [Serializable]
#endif
#if !Smartphone
    [DataContract]
    [DebuggerDisplay("{User.ScreenName}: {Text}")]
#endif
    [JsonObject(MemberSerialization.OptIn)]
    public class TwitterStatus : PropertyChangedBase,
                                 IComparable<TwitterStatus>,
                                 IEquatable<TwitterStatus>,
                                 ITwitterEntity,
                                 ITweetable
    {
        private DateTime _createdDate;
        private long _id;
        private string _inReplyToScreenName;
        private long? _inReplyToStatusId;
        private int? _inReplyToUserId;
        private bool _isFavorited;
        private bool _isTruncated;
        private string _source;
        private string _text;
        private TwitterUser _user;
        private TwitterStatus _retweetedStatus;
        private TwitterGeoLocation _location;

        /// <summary>
        /// Gets the ID of this tweet.
        /// </summary>
        /// <value>The ID.</value>
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
        /// Gets or sets the user ID to whom this
        /// tweet was a reply, if any.
        /// </summary>
        /// <value>The user ID of the target tweet.</value>
        [JsonProperty("in_reply_to_user_id")]
#if !Smartphone
        [DataMember]
#endif
        public virtual int? InReplyToUserId
        {
            get { return _inReplyToUserId; }
            set
            {
                if (_inReplyToUserId == value)
                {
                    return;
                }

                _inReplyToUserId = value;
                OnPropertyChanged("InReplyToUserId");
            }
        }

        /// <summary>
        /// Gets or sets the ID of the tweet to which 
        /// this tweet is a reply, if any.
        /// </summary>
        /// <value>The ID of the target tweet.</value>
        [JsonProperty("in_reply_to_status_id")]
#if !Smartphone
        [DataMember]
#endif
        public virtual long? InReplyToStatusId
        {
            get { return _inReplyToStatusId; }
            set
            {
                if (_inReplyToStatusId == value)
                {
                    return;
                }

                _inReplyToStatusId = value;
                OnPropertyChanged("InReplyToStatusId");
            }
        }

        /// <summary>
        /// Gets or sets the name of the screen name to whom
        /// this tweet is a reply, if any.
        /// </summary>
        /// <value>The name of the in reply to screen.</value>
        [JsonProperty("in_reply_to_screen_name")]
#if !Smartphone
        [DataMember]
#endif
        public virtual string InReplyToScreenName
        {
            get { return _inReplyToScreenName; }
            set
            {
                if (_inReplyToScreenName == value)
                {
                    return;
                }

                _inReplyToScreenName = value;
                OnPropertyChanged("InReplyToScreenName");
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this tweet was truncated to fit
        /// within 140 characters.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this tweet was truncated; otherwise, <c>false</c>.
        /// </value>
        [JsonProperty("truncated")]
#if !Smartphone
        [DataMember]
#endif
        public virtual bool IsTruncated
        {
            get { return _isTruncated; }
            set
            {
                if (_isTruncated == value)
                {
                    return;
                }

                _isTruncated = value;
                OnPropertyChanged("IsTruncated");
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this tweet is favorited.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this tweet is favorited; otherwise, <c>false</c>.
        /// </value>
        [JsonProperty("favorited")]
#if !Smartphone
        [DataMember]
#endif
        public virtual bool IsFavorited
        {
            get { return _isFavorited; }
            set
            {
                if (_isFavorited == value)
                {
                    return;
                }

                _isFavorited = value;
                OnPropertyChanged("IsFavorited");
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
        /// Gets or sets the user that authored this <seealso cref="TwitterStatus" />.
        /// </summary>
        /// <value>The user that authored this status.</value>
        [JsonProperty("user")]
#if !Smartphone
        [DataMember]
#endif
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
                OnPropertyChanged("TwitterUser");
            }
        }

        /// <summary>
        /// Gets or sets the retweeted <seealso cref="TwitterStatus" /> that
        /// this status is derived from, if any.
        /// </summary>
        /// <value>The retweeted status.</value>
        [JsonProperty("retweeted_status")]
#if !Smartphone
        [DataMember]
#endif
        public virtual TwitterStatus RetweetedStatus
        {
            get { return _retweetedStatus; }
            set
            {
                if (_retweetedStatus == value)
                {
                    return;
                }

                _retweetedStatus = value;
                OnPropertyChanged("RetweetedStatus");
            }
        }

        /// <summary>
        /// Gets the created date of this tweet.
        /// </summary>
        /// <value>The created date.</value>
        [JsonProperty("created_at")]
#if !Smartphone
        [DataMember]
#endif
        public virtual DateTime CreatedDate
        {
            get { return _createdDate; }
            set
            {
                if (_createdDate == value)
                {
                    return;
                }

                _createdDate = value;
                OnPropertyChanged("CreatedDate");
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
        public virtual TwitterGeoLocation Location
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

#if !Smartphone
        /// <summary>
        /// The source content used to deserialize the model entity instance.
        /// Can be XML or JSON, depending on the endpoint used.
        /// </summary>
        [DataMember]
#endif
        public virtual string RawSource { get; set; }

        #region IComparable<TwitterStatus> Members

        /// <summary>
        /// Compares the current object with another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// A value that indicates the relative order of the objects being compared. The return value has the following meanings: Value Meaning Less than zero This object is less than the <paramref name="other"/> parameter.Zero This object is equal to <paramref name="other"/>. Greater than zero This object is greater than <paramref name="other"/>.
        /// </returns>
        public int CompareTo(TwitterStatus other)
        {
            return other.Id == Id ? 0 : other.Id > Id ? -1 : 1;
        }

        #endregion

        #region IEquatable<TwitterStatus> Members

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="status">The status.</param>
        /// <returns>
        /// true if the current object is equal to the <paramref name="status"/> parameter; otherwise, false.
        /// </returns>
        public bool Equals(TwitterStatus status)
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
        /// Determines whether the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <param name="status">The <see cref="System.Object"/> to compare with this instance.</param>
        /// <returns>
        /// true if the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>; otherwise, false.
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
            return status.GetType() == typeof (TwitterStatus) && Equals((TwitterStatus) status);
        }

        /// <summary>
        /// Serves as a hash function for a particular type. 
        /// </summary>
        /// <returns>
        /// A hash code for the current <see cref="T:System.Object"/>.
        /// </returns>
        /// <filterpriority>2</filterpriority>
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
        public static bool operator ==(TwitterStatus left, TwitterStatus right)
        {
            return Equals(left, right);
        }

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator !=(TwitterStatus left, TwitterStatus right)
        {
            return !Equals(left, right);
        }
    }
}