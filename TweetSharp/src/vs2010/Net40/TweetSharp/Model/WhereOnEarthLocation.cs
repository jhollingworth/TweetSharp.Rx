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
using Newtonsoft.Json;

namespace TweetSharp.Model
{
    // {
    //     "url":"http://where.yahooapis.com/v1/place/23424900",
    //     "woeid":23424900,
    //     "placeType":{"code":12,"name":"Country"},
    //     "countryCode":"MX",
    //     "name":"Mexico",
    //     "country":"Mexico" }

#if !SILVERLIGHT
    /// <summary>
    /// Represents a location in the Yahoo! WOE specification.
    /// </summary>
    /// <seealso>"http://developer.yahoo.com/geo/geoplanet/"</seealso>
    [Serializable]
#endif
#if !Smartphone
    [DebuggerDisplay("{WoeId}: {Name}")]
#endif
    [JsonObject(MemberSerialization.OptIn)]
    public class WhereOnEarthLocation : PropertyChangedBase, IModel 
    {
        private long _woeId;
        private string _url;
        private string _name;
        private string _countryCode;
        private string _country;
        private WhereOnEarthPlaceType _placeType;

#if !Smartphone
        ///<summary>
        /// The ID of this WhereOnEarth location
        ///</summary>
        [DataMember]
#endif
        [JsonProperty("woeid")]
        public virtual long WoeId
        {
            get { return _woeId; }
            set
            {
                if (_woeId == value)
                {
                    return;
                }

                _woeId = value;
                OnPropertyChanged("WoeId");
            }
        }

#if !Smartphone
        ///<summary>
        ///Gets or sets the lookup URL for this WhereOnEarth location
        ///</summary>
        [DataMember]
#endif
        [JsonProperty("url")]
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

#if !Smartphone
        ///<summary>
        /// Gets or sets the <see cref="WhereOnEarthPlaceType"/> for this location
        ///</summary>
        [DataMember]
#endif
        [JsonProperty("placeType")]
        public virtual WhereOnEarthPlaceType PlaceType
        {
            get { return _placeType; }
            set
            {
                if (_placeType == value)
                {
                    return;
                }

                _placeType = value;
                OnPropertyChanged("PlaceType");
            }
        }

#if !Smartphone
        ///<summary>
        /// Gets or sets the name of this location
        ///</summary>
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
        ///<summary>
        /// Gets or sets the ISO Country code associated with this location
        ///</summary>
        [DataMember]
#endif
        [JsonProperty("countryCode")]
        public virtual string CountryCode
        {
            get { return _countryCode; }
            set
            {
                if (_countryCode == value)
                {
                    return;
                }

                _countryCode = value;
                OnPropertyChanged("CountryCode");
            }
        }

#if !Smartphone
        ///<summary>
        /// Gets or sets the country name for this location
        ///</summary>
        [DataMember]
#endif
        [JsonProperty("country")]
        public virtual string Country
        {
            get { return _country; }
            set
            {
                if (_country == value)
                {
                    return;
                }

                _country = value;
                OnPropertyChanged("Country");
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