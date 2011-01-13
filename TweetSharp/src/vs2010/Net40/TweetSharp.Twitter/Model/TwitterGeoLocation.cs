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
using System.Linq;
using Newtonsoft.Json;
using TweetSharp.Model;
using TweetSharp.Twitter.Model.Converters;

namespace TweetSharp.Twitter.Model
{
#if !SILVERLIGHT
    ///<summary>
    /// Represents a geospatial location, for APIs that support it.
    ///</summary>
    [Serializable]
#endif
    [JsonObject(MemberSerialization.OptIn)]
    public class TwitterGeoLocation : PropertyChangedBase, IEquatable<TwitterGeoLocation>
    {
#if !SILVERLIGHT
        /// <summary>
        /// The inner spatial coordinates for this location.
        /// </summary>
        [Serializable]
#endif
        public class GeoCoordinates
        {
            /// <summary>
            /// Gets or sets the latitude.
            /// </summary>
            /// <value>The latitude.</value>
            public virtual double Latitude{ get; set; }

            /// <summary>
            /// Gets or sets the longitude.
            /// </summary>
            /// <value>The longitude.</value>
            public virtual double Longitude { get; set; }

            /// <summary>
            /// Performs an explicit conversion from <see cref="TwitterGeoLocation.GeoCoordinates"/> to array of <see cref="System.Double"/>.
            /// </summary>
            /// <param name="location">The location.</param>
            /// <returns>The result of the conversion.</returns>
            public static explicit operator double[](GeoCoordinates location)
            {
                return new[] { location.Latitude, location.Longitude };
            }

            /// <summary>
            /// Performs an implicit conversion from <see cref="double"/> to <see cref="TwitterGeoLocation.GeoCoordinates"/>.
            /// </summary>
            /// <param name="values">The values.</param>
            /// <returns>The result of the conversion.</returns>
            public static implicit operator GeoCoordinates(List<double> values)
            {
                return FromEnumerable(values);
            }

            /// <summary>
            /// Performs an implicit conversion from array of <see cref="System.Double"/> to <see cref="TwitterGeoLocation.GeoCoordinates"/>.
            /// </summary>
            /// <param name="values">The values.</param>
            /// <returns>The result of the conversion.</returns>
            public static implicit operator GeoCoordinates(double[] values)
            {
                return FromEnumerable(values);
            }

            /// <summary>
            /// Froms the enumerable.
            /// </summary>
            /// <param name="values">The values.</param>
            /// <returns></returns>
            private static GeoCoordinates FromEnumerable(IEnumerable<double> values)
            {
                if (values == null)
                {
                    throw new ArgumentNullException("values");
                }

                var latitude = values.First();
                var longitude = values.Skip(1).Take(1).Single();

                return new GeoCoordinates {Latitude = latitude, Longitude = longitude};
            }
        }

        private static readonly TwitterGeoLocation _none = new TwitterGeoLocation();
        private GeoCoordinates _coordinates;
        private string _type;

        /// <summary>
        /// Initializes a new instance of the <see cref="TwitterGeoLocation"/> struct.
        /// </summary>
        /// <param name="latitude">The latitude.</param>
        /// <param name="longitude">The longitude.</param>
        public TwitterGeoLocation(double latitude, double longitude)
        {
            _coordinates = new GeoCoordinates
                               {
                                   Latitude = latitude,
                                   Longitude = longitude
                               };
        }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="TwitterGeoLocation"/> struct.
        /// </summary>
        public TwitterGeoLocation()
        {

        }

        /// <summary>
        /// Gets or sets the inner spatial coordinates.
        /// </summary>
        /// <value>The coordinates.</value>
        [JsonProperty("Coordinates")]
        [JsonConverter(typeof(TwitterGeoConverter))]
        public virtual GeoCoordinates Coordinates
        {
            get { return _coordinates;  }
            set
            {
                _coordinates = value;
                OnPropertyChanged("Coordinates");
            }
        }

        /// <summary>
        /// Gets or sets the type of location.
        /// </summary>
        /// <value>The type.</value>
        [JsonProperty("Type")]
        public virtual string Type
        {
            get { return _type;  }
            set
            {
                _type = value; 
                OnPropertyChanged("Type");
            }
        }
        
        /// <summary>
        /// Gets the latitude.
        /// </summary>
        /// <value>The latitude.</value>
        [Obsolete("Use the Coordinates property.")]
        public virtual double Latitude
        {
            get { return _coordinates.Latitude; }
            set
            {
                _coordinates.Latitude = value;
                OnPropertyChanged("Latitude");
            }
        }

        /// <summary>
        /// Gets the longitude.
        /// </summary>
        /// <value>The longitude.</value>
        [Obsolete("Use the Coordinates property.")]
        public virtual double Longitude
        {
            get { return _coordinates.Longitude; }
            set
            {
                _coordinates.Longitude = value;
                OnPropertyChanged("Longitude");
            }
        }

        /// <summary>
        /// Gets an instance of <see cref="TwitterGeoLocation" />
        /// that represents nowhere.
        /// </summary>
        /// <value>The none.</value>
        public static TwitterGeoLocation None
        {
            get { return _none; }
        }

        #region IEquatable<TwitterGeoLocation> Members

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
        /// </returns>
        public virtual bool Equals(TwitterGeoLocation other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }
            return other.Coordinates.Latitude == Coordinates.Latitude
                   && other.Coordinates.Longitude == Coordinates.Longitude;
        }

        #endregion

        /// <summary>
        /// Determines whether the specified <see cref="System.Object"/> is equal to this instance.
        /// </summary>
        /// <param name="instance">The <see cref="System.Object"/> to compare with this instance.</param>
        /// <returns>
        /// 	<c>true</c> if the specified <see cref="System.Object"/> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object instance)
        {
            if (ReferenceEquals(null, instance))
            {
                return false;
            }

            return instance.GetType() == typeof (TwitterGeoLocation) && Equals((TwitterGeoLocation) instance);
        }
        
        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator ==(TwitterGeoLocation left, TwitterGeoLocation right)
        {
            if ( ReferenceEquals(left,right))
            {
                return true; 
            }
            if ( ReferenceEquals(null, left))
            {
                return false;
            }

            return left.Equals(right);
        }

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator !=(TwitterGeoLocation left, TwitterGeoLocation right)
        {
            if (ReferenceEquals(left, right))
            {
                return false;
            }
            if (ReferenceEquals(null, left))
            {
                return true;
            }
            return !left.Equals(right);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            unchecked
            {
                return (Coordinates.Latitude.GetHashCode() * 397) ^ Coordinates.Longitude.GetHashCode();
            }
        }
    }
}