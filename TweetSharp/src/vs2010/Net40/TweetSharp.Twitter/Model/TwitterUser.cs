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
using System.Diagnostics;
using System.Runtime.Serialization;
using TweetSharp.Model;
using Newtonsoft.Json;
using TweetSharp.Twitter.Model.Converters;

#if !Smartphone
#endif

namespace TweetSharp.Twitter.Model
{
#if !SILVERLIGHT
    /// <summary>
    /// This data class provides more information than the basic data provided by
    /// <see cref="TwitterUser" /> returned in other calls for friends and followers.
    /// </summary>
    [Serializable]
#endif
#if !Smartphone
    [DataContract]
    [DebuggerDisplay("{ScreenName}")]
#endif
    [JsonObject(MemberSerialization.OptIn)]
    public class TwitterUser : PropertyChangedBase,
                               IComparable<TwitterUser>,
                               IEquatable<TwitterUser>,
                               ITwitterModel
    {
        private string _description;
        private int _followersCount;
        private int _id;
        private bool? _isProtected;
        private string _location;
        private string _name;
        private string _profileImageUrl;
        private string _screenName;
        private TwitterStatus _status;
        private string _url;
        private DateTime _createdDate;
        private int _favouritesCount;
        private int _friendsCount;
        private bool? _hasNotifications;
        private bool? _isFollowing;
        private bool? _isVerified;
        private bool? _isGeoEnabled;
        private bool _isProfileBackgroundTiled;
        private string _profileBackgroundColor;
        private string _profileBackgroundImageUrl;
        private string _profileLinkColor;
        private string _profileSidebarBorderColor;
        private string _profileSidebarFillColor;
        private string _profileTextColor;
        private int _statusesCount;
        private string _timeZone;
        private string _utcOffset;
        private string _language;

#if !Smartphone
        /// <summary>
        /// Gets or sets the ID.
        /// </summary>
        /// <value>The ID.</value>
        [DataMember]
#endif
        [JsonProperty("id")]
        public virtual int Id
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
        /// Gets or sets the followers count.
        /// </summary>
        /// <value>The followers count.</value>
        [DataMember]
#endif
        [JsonProperty("followers_count")]
        public virtual int FollowersCount
        {
            get { return _followersCount; }
            set
            {
                if (_followersCount == value)
                {
                    return;
                }

                _followersCount = value;
                OnPropertyChanged("FollowersCount");
            }
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
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
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        [JsonProperty("description")]
#if !Smartphone
        [DataMember]
#endif
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

        /// <summary>
        /// Gets or sets the profile image URL.
        /// </summary>
        /// <value>The profile image URL.</value>
        [JsonProperty("profile_image_url")]
#if !Smartphone
        [DataMember]
#endif
        public virtual string ProfileImageUrl
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
        /// Gets or sets the URL.
        /// </summary>
        /// <value>The URL.</value>
        [JsonProperty("url")]
#if !Smartphone
        [DataMember]
#endif
        public virtual string Url
        {
            get { return _url; }
            set
            {
                if (_url == value)
                {
                    return;
                }

                _url = value;
                OnPropertyChanged("Url");
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the user's timeline is protected.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if the  user's timeline is protected; otherwise, <c>false</c>.
        /// </value>
        [JsonProperty("protected")]
#if !Smartphone
        [DataMember]
#endif
        public virtual bool? IsProtected
        {
            get { return _isProtected; }
            set
            {
                if (_isProtected == value)
                {
                    return;
                }

                _isProtected = value;
                OnPropertyChanged("IsProtected");
            }
        }

        /// <summary>
        /// Gets or sets the user's screen name.
        /// </summary>
        /// <value>The user's screen name.</value>
        [JsonProperty("screen_name")]
#if !Smartphone
        [DataMember]
#endif
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

        /// <summary>
        /// Gets or sets the user's self-described location. 
        /// </summary>
        /// <value>The user's self-described location.</value>
        [JsonProperty("location")]
#if !Smartphone
        [DataMember]
#endif
        public virtual string Location
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
        /// Gets or sets the user's last <seealso cref="TwitterStatus" />.
        /// </summary>
        /// <value>The user's last status.</value>
        [JsonProperty("status")]
#if !Smartphone
        [DataMember]
#endif
        public virtual TwitterStatus Status
        {
            get { return _status; }
            set
            {
                if (_status == value)
                {
                    return;
                }

                _status = value;
                OnPropertyChanged("Status");
            }
        }


        /// <summary>
        /// Gets or sets the friends count.
        /// </summary>
        /// <value>The friends count.</value>
        [JsonProperty("friends_count")]
#if !Smartphone
        [DataMember]
#endif
        public virtual int FriendsCount
        {
            get { return _friendsCount; }
            set
            {
                if (_friendsCount == value)
                {
                    return;
                }

                _friendsCount = value;
                OnPropertyChanged("FriendsCount");
            }
        }

        /// <summary>
        /// Gets or sets the color of the user's profile background.
        /// </summary>
        /// <value>The color of the user's profile background.</value>
        [JsonProperty("profile_background_color")]
#if !Smartphone
        [DataMember]
#endif
        public virtual string ProfileBackgroundColor
        {
            get { return _profileBackgroundColor; }
            set
            {
                if (_profileBackgroundColor == value)
                {
                    return;
                }

                _profileBackgroundColor = value;
                OnPropertyChanged("ProfileBackgroundColor");
            }
        }

        /// <summary>
        /// Gets or sets the UTC offset.
        /// </summary>
        /// <value>The UTC offset.</value>
        [JsonProperty("utc_offset")]
#if !Smartphone
        [DataMember]
#endif
        public virtual string UtcOffset
        {
            get { return _utcOffset; }
            set
            {
                if (_utcOffset == value)
                {
                    return;
                }

                _utcOffset = value;
                OnPropertyChanged("UtcOffset");
            }
        }

        /// <summary>
        /// Gets or sets the color of the profile text.
        /// </summary>
        /// <value>The color of the profile text.</value>
        [JsonProperty("profile_text_color")]
#if !Smartphone
        [DataMember]
#endif
        public virtual string ProfileTextColor
        {
            get { return _profileTextColor; }
            set
            {
                if (_profileTextColor == value)
                {
                    return;
                }

                _profileTextColor = value;
                OnPropertyChanged("ProfileTextColor");
            }
        }

        /// <summary>
        /// Gets or sets the user's profile background image URL.
        /// </summary>
        /// <value>The user's profile background image URL.</value>
        [JsonProperty("profile_background_image_url")]
#if !Smartphone
        [DataMember]
#endif
        public virtual string ProfileBackgroundImageUrl
        {
            get { return _profileBackgroundImageUrl; }
            set
            {
                if (_profileBackgroundImageUrl == value)
                {
                    return;
                }

                _profileBackgroundImageUrl = value;
                OnPropertyChanged("ProfileBackgroundImageUrl");
            }
        }

        /// <summary>
        /// Gets or sets the time zone.
        /// </summary>
        /// <value>The time zone.</value>
        [JsonProperty("time_zone")]
#if !Smartphone
        [DataMember]
#endif
        public virtual string TimeZone
        {
            get { return _timeZone; }
            set
            {
                if (_timeZone == value)
                {
                    return;
                }

                _timeZone = value;
                OnPropertyChanged("TimeZone");
            }
        }
        
#if !Smartphone
        /// <summary>
        /// Gets or sets the favorites count.
        /// </summary>
        /// <remarks>
        /// Spelling is UK English here but not elsewhere.
        /// </remarks>
        /// <value>The favorites count.</value>
        [DataMember]
#endif
        [JsonProperty("favourites_count")]
        public virtual int FavouritesCount
        {
            get { return _favouritesCount; }
            set
            {
                if (_favouritesCount == value)
                {
                    return;
                }

                _favouritesCount = value;
                OnPropertyChanged("FavouritesCount");
            }
        }

        /// <summary>
        /// Gets or sets the color of profile links.
        /// </summary>
        /// <value>The color of profile links.</value>
        [JsonProperty("profile_link_color")]
#if !Smartphone
        [DataMember]
#endif
        public virtual string ProfileLinkColor
        {
            get { return _profileLinkColor; }
            set
            {
                if (_profileLinkColor == value)
                {
                    return;
                }

                _profileLinkColor = value;
                OnPropertyChanged("ProfileLinkColor");
            }
        }

        /// <summary>
        /// Gets or sets the statuses count.
        /// </summary>
        /// <value>The statuses count.</value>
        [JsonProperty("statuses_count")]
#if !Smartphone
        [DataMember]
#endif
        public virtual int StatusesCount
        {
            get { return _statusesCount; }
            set
            {
                if (_statusesCount == value)
                {
                    return;
                }

                _statusesCount = value;
                OnPropertyChanged("StatusesCount");
            }
        }

        /// <summary>
        /// Gets or sets the color of the profile sidebar fill.
        /// </summary>
        /// <value>The color of the profile sidebar fill.</value>
        [JsonProperty("profile_sidebar_fill_color")]
#if !Smartphone
        [DataMember]
#endif
        public virtual string ProfileSidebarFillColor
        {
            get { return _profileSidebarFillColor; }
            set
            {
                if (_profileSidebarFillColor == value)
                {
                    return;
                }

                _profileSidebarFillColor = value;
                OnPropertyChanged("ProfileSidebarFillColor");
            }
        }

        /// <summary>
        /// Gets or sets the color of the profile sidebar border.
        /// </summary>
        /// <value>The color of the profile sidebar border.</value>
        [JsonProperty("profile_sidebar_border_color")]
#if !Smartphone
        [DataMember]
#endif
        public virtual string ProfileSidebarBorderColor
        {
            get { return _profileSidebarBorderColor; }
            set
            {
                if (_profileSidebarBorderColor == value)
                {
                    return;
                }

                _profileSidebarBorderColor = value;
                OnPropertyChanged("ProfileSidebarBorderColor");
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the user's profile is background tiled.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if the user's profile is profile background tiled; otherwise, <c>false</c>.
        /// </value>
        [JsonProperty("profile_background_tile")]
#if !Smartphone
        [DataMember]
#endif
        public virtual bool IsProfileBackgroundTiled
        {
            get { return _isProfileBackgroundTiled; }
            set
            {
                if (_isProfileBackgroundTiled == value)
                {
                    return;
                }

                _isProfileBackgroundTiled = value;
                OnPropertyChanged("IsProfileBackgroundTiled");
            }
        }

        // http://twitter.com/account/verify_credentials
        /// <summary>
        /// Gets or sets a value indicating whether the user's profile has notifications enabled.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if the user's profile has notifications enabled; otherwise, <c>false</c>.
        /// </value>
        [JsonProperty("notifications")]
#if !Smartphone
        [DataMember]
#endif
        [Obsolete("Twitter will obsolete this element and it is unreliable- http://is.gd/7LRZs")]
        public virtual bool? HasNotifications
        {
            get { return _hasNotifications; }
            set
            {
                if (_hasNotifications == value)
                {
                    return;
                }

                _hasNotifications = value;
                OnPropertyChanged("HasNotifications");
            }
        }

        // http://twitter.com/account/verify_credentials
        /// <summary>
        /// Gets or sets a value indicating whether the user is following the authenticated user.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if the user is following the authenticated user; otherwise, <c>false</c>.
        /// </value>
        [JsonProperty("following")]
#if !Smartphone
        [DataMember]
#endif
        [Obsolete("Twitter will obsolete this element and it is unreliable- http://is.gd/7LRZs")]
        public virtual bool? IsFollowing
        {
            get { return _isFollowing; }
            set
            {
                if (_isFollowing == value)
                {
                    return;
                }

                _isFollowing = value;
                OnPropertyChanged("IsFollowing");
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the user is verified by Twitter as authentic.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if the user is verified by Twitter as authentic; otherwise, <c>false</c>.
        /// </value>
        [JsonProperty("verified")]
#if !Smartphone
        [DataMember]
#endif
        public virtual bool? IsVerified
        {
            get { return _isVerified; }
            set
            {
                if (_isVerified == value)
                {
                    return;
                }

                _isVerified = value;
                OnPropertyChanged("IsVerified");
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the user has enabled geo-tagging.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if the user has enabled geo-tagging.; otherwise, <c>false</c>.
        /// </value>
        [JsonProperty("geo_enabled")]
#if !Smartphone
        [DataMember]
#endif
        public virtual bool? IsGeoEnabled
        {
            get { return _isGeoEnabled; }
            set
            {
                if (_isGeoEnabled == value)
                {
                    return;
                }

                _isGeoEnabled = value;
                OnPropertyChanged("IsGeoEnabled");
            }
        }

        /// <summary>
        /// Gets or sets the user's known language.
        /// </summary>
        /// <value>The user's known language.</value>
        [JsonProperty("lang")]
#if !Smartphone
        [DataMember]
#endif
        public virtual string Language
        {
            get { return _language; }
            set
            {
                if (_language == value)
                {
                    return;
                }
                _language = value;
                OnPropertyChanged("Language");
            }
        }

        /// <summary>
        /// Gets or sets the created date.
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

#if !Smartphone
        /// <summary>
        /// The source content used to deserialize the model entity instance.
        /// Can be XML or JSON, depending on the endpoint used.
        /// </summary>
        [DataMember]
#endif
        public virtual string RawSource { get; set; }

        #region IComparable<TwitterUser> Members

        /// <summary>
        /// Compares the current object with another object of the same type.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>
        /// A value that indicates the relative order of the objects being compared. 
        /// The return value has the following meanings: 
        /// Less than zero: This object is less than the <paramref name="user"/> parameter.
        /// Zero: This object is equal to <paramref name="user"/>. 
        /// Greater than zero: This object is greater than <paramref name="user"/>.
        /// </returns>
        public int CompareTo(TwitterUser user)
        {
            return user.Id == Id ? 0 : user.Id > Id ? -1 : 1;
        }

        #endregion

        #region IEquatable<TwitterUser> Members

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>
        /// true if the current object is equal to the <paramref name="user"/> parameter; otherwise, false.
        /// </returns>
        public bool Equals(TwitterUser user)
        {
            if (ReferenceEquals(null, user))
            {
                return false;
            }
            if (ReferenceEquals(this, user))
            {
                return true;
            }
            return user.Id == Id;
        }

        #endregion

        /// <summary>
        /// Determines whether the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <param name="user">The <see cref="System.Object"/> to compare with this instance.</param>
        /// <returns>
        /// true if the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>; otherwise, false.
        /// </returns>
        public override bool Equals(object user)
        {
            if (ReferenceEquals(null, user))
            {
                return false;
            }
            if (ReferenceEquals(this, user))
            {
                return true;
            }
            return user.GetType() == typeof (TwitterUser) && Equals((TwitterUser) user);
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
        public static bool operator ==(TwitterUser left, TwitterUser right)
        {
            return Equals(left, right);
        }

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator !=(TwitterUser left, TwitterUser right)
        {
            return !Equals(left, right);
        }
    }
}