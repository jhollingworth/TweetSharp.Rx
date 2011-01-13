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

#if !Smartphone
#endif

namespace TweetSharp.Twitter.Model
{
#if !SILVERLIGHT
    /// <summary>
    /// Represents a private <see cref="TwitterStatus" /> between two users.
    /// </summary>
    [Serializable]
#endif
#if !Smartphone
    [DataContract]
    [DebuggerDisplay("{SenderScreenName} to {RecipientScreenName}:{Text}")]
#endif
    [JsonObject(MemberSerialization.OptIn)]
    public class TwitterDirectMessage : PropertyChangedBase,
                                        IComparable<TwitterDirectMessage>,
                                        IEquatable<TwitterDirectMessage>,
                                        ITwitterEntity,
                                        ITweetable
    {
        private long _id;
        private long _recipientId;
        private string _recipientScreenName;
        private TwitterUser _recipient;
        private long _senderId;
        private TwitterUser _sender;
        private string _senderScreenName;
        private string _text;
        private DateTime _createdDate;

        /// <summary>
        /// Gets or sets the direct message's ID.
        /// </summary>
        /// <value>The direct message ID.</value>
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
        /// Gets or sets the ID of the receiving user.
        /// </summary>
        /// <value>The receiving user's ID.</value>
        [JsonProperty("recipient_id")]
#if !Smartphone
        [DataMember]
#endif
        public virtual long RecipientId
        {
            get { return _recipientId; }
            set
            {
                if (_recipientId == value)
                {
                    return;
                }

                _recipientId = value;
                OnPropertyChanged("RecipientId");
            }
        }

        /// <summary>
        /// Gets or sets the screen name of the receiving user.
        /// </summary>
        /// <value>The receiving user's screen name.</value>
        [JsonProperty("recipient_screen_name")]
#if !Smartphone
        [DataMember]
#endif
        public virtual string RecipientScreenName
        {
            get { return _recipientScreenName; }
            set
            {
                if (_recipientScreenName == value)
                {
                    return;
                }

                _recipientScreenName = value;
                OnPropertyChanged("RecipientScreenName");
            }
        }

        /// <summary>
        /// Gets or sets the recipient.
        /// </summary>
        /// <value>The recipient.</value>
        [JsonProperty("recipient")]
#if !Smartphone
        [DataMember]
#endif
        public virtual TwitterUser Recipient
        {
            get { return _recipient; }
            set
            {
                if (_recipient == value)
                {
                    return;
                }

                _recipient = value;
                OnPropertyChanged("Recipient");
            }
        }

        /// <summary>
        /// Gets or sets the ID of the sending user.
        /// </summary>
        /// <value>The sender ID.</value>
        [JsonProperty("sender_id")]
#if !Smartphone
        [DataMember]
#endif
        public virtual long SenderId
        {
            get { return _senderId; }
            set
            {
                if (_senderId == value)
                {
                    return;
                }

                _senderId = value;
                OnPropertyChanged("SenderId");
            }
        }

        /// <summary>
        /// Gets or sets the message sender.
        /// </summary>
        /// <value>The sender.</value>
        [JsonProperty("sender")]
#if !Smartphone
        [DataMember]
#endif
        public virtual TwitterUser Sender
        {
            get { return _sender; }
            set
            {
                if (_sender == value)
                {
                    return;
                }

                _sender = value;
                OnPropertyChanged("Sender");
            }
        }

        /// <summary>
        /// Gets or sets the screen name of the sending user.
        /// </summary>
        /// <value>The sending user's screen name.</value>
        [JsonProperty("sender_screen_name")]
#if !Smartphone
        [DataMember]
#endif
        public virtual string SenderScreenName
        {
            get { return _senderScreenName; }
            set
            {
                if (_senderScreenName == value)
                {
                    return;
                }

                _senderScreenName = value;
                OnPropertyChanged("SenderScreenName");
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
        /// Gets the created date.
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

        #region IComparable<TwitterDirectMessage> Members

        /// <summary>
        /// Compares the current object with another object of the same type.
        /// </summary>
        /// <returns>
        /// A value that indicates the relative order of the objects being compared. The return value has the following meanings: Value Meaning Less than zero This object is less than the <paramref name="other"/> parameter.Zero This object is equal to <paramref name="other"/>. Greater than zero This object is greater than <paramref name="other"/>. 
        /// </returns>
        /// <param name="other">An object to compare with this object.</param>
        public int CompareTo(TwitterDirectMessage other)
        {
            return other.Id == Id ? 0 : other.Id > Id ? -1 : 1;
        }

        #endregion

        #region IEquatable<TwitterDirectMessage> Members

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="obj">An object to compare with this object.</param>
        /// <returns>
        /// true if the current object is equal to the <paramref name="obj"/> parameter; otherwise, false.
        /// </returns>
        public bool Equals(TwitterDirectMessage obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            return obj.Id == Id;
        }

        #endregion

        /// <summary>
        /// Determines whether the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <param name="obj">The <see cref="T:System.Object"/> to compare with the current <see cref="T:System.Object"/>.</param>
        /// <returns>
        /// true if the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>; otherwise, false.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            return obj.GetType() == typeof (TwitterDirectMessage) && Equals((TwitterDirectMessage) obj);
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
        public static bool operator ==(TwitterDirectMessage left, TwitterDirectMessage right)
        {
            return Equals(left, right);
        }

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator !=(TwitterDirectMessage left, TwitterDirectMessage right)
        {
            return !Equals(left, right);
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