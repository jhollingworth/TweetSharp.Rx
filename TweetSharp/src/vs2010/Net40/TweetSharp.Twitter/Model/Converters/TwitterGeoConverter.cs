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
using TweetSharp.Core.Extensions;
using Newtonsoft.Json;
using TweetSharp.Model.Converters;

namespace TweetSharp.Twitter.Model.Converters
{
    // [DC]: All converters must be public for Silverlight to construct them correctly.

    /// <summary>
    /// This converter exists to convert geo-spatial coordinates.
    /// </summary>
    public class TwitterGeoConverter : TweetSharpConverterBase
    {
        private const string GeoTemplate =
            "\"geo\":{{\"type\":\"Point\",\"coordinates\":[{0}, {1}]}}";

        /// <summary>
        /// Writes the JSON.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="value">The value.</param>
        /// <param name="serializer">The serializer.</param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is TwitterGeoLocation))
            {
                return;
            }

            var location = (TwitterGeoLocation)value;
            var json = GeoTemplate.FormatWith(location.Coordinates.Latitude, location.Coordinates.Longitude);
            writer.WriteValue(json);
        }

        /// <summary>
        /// Reads the JSON.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="objectType">Type of the object.</param>
        /// <param name="existingValue">The existing value.</param>
        /// <param name="serializer">The serializer.</param>
        /// <returns></returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
            {
                return null;
            }


            if (reader.TokenType != JsonToken.StartArray)
            {
                return null;
            }
            reader.Read();
            var coords = new double[2];
            if (reader.TokenType == JsonToken.Float)
            {
                coords[0] = (double)reader.Value;
                reader.Read();
            }
            if (reader.TokenType == JsonToken.Float)
            {
                coords[1] = (double)reader.Value;
                reader.Read();
            }

            var latitude = coords[0];
            var longitude = coords[1];

            return new TwitterGeoLocation.GeoCoordinates { Latitude = latitude, Longitude = longitude };
        }

        /// <summary>
        /// Determines whether this instance can convert the specified object type.
        /// </summary>
        /// <param name="objectType">Type of the object.</param>
        /// <returns>
        /// 	<c>true</c> if this instance can convert the specified object type; otherwise, <c>false</c>.
        /// </returns>
        public override bool CanConvert(Type objectType)
        {
            var t = (IsNullableType(objectType))
                        ? Nullable.GetUnderlyingType(objectType)
                        : objectType;

            return typeof(TwitterGeoLocation.GeoCoordinates).IsAssignableFrom(t);
        }
    }
}
