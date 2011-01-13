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
using System.IO;
using System.Text;
using System.Xml.Serialization;
using NUnit.Framework;

namespace TweetSharp.UnitTests.Helpers
{
    public static class TestExtensions
    {
#if !Smartphone
        public static string GetDescription(this Enum value)
        {
            var field = value.GetType().GetField(value.ToString());
            var attributes = (DescriptionAttribute[]) field.GetCustomAttributes(typeof (DescriptionAttribute), false);

            return (attributes.Length > 0) ? attributes[0].Description : value.ToString();
        }
#endif

        public static string ToXml<T>(this T instance)
        {
            var type = typeof (T);
#if !Smartphone
            if (!type.IsSerializable)
            {
                return String.Empty;
            }
#endif

            var sb = new StringBuilder();
            var serializer = new XmlSerializer(type);

            using (var writer = new StringWriter(sb))
            {
                serializer.Serialize(writer, instance);
            }

            return sb.ToString();
        }

        public static T FromXml<T>(this string xml)
        {
            T type;

            var serializer = new XmlSerializer(typeof (T));

            using (var reader = new StringReader(xml))
            {
                type = (T) serializer.Deserialize(reader);
                reader.Close();
            }

            return type;
        }

        public static Uri AsUri(this string url)
        {
            return new Uri(url);
        }
    }
}