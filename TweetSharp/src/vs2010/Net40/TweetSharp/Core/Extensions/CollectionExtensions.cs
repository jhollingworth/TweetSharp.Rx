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
using System.Text;
using Hammock.Web;

#if !SILVERLIGHT
using System.Web;
#endif

namespace TweetSharp.Core.Extensions
{
    internal static class CollectionExtensions
    {
        public static IEnumerable<T> ToEnumerable<T>(this object[] items) where T : class
        {
            foreach (var item in items)
            {
                var record = item as T;
                yield return record;
            }
        }

        public static void ForEach<T>(this IEnumerable<T> items, Action<T> action)
        {
            foreach (var item in items)
            {
                action(item);
            }
        }

        public static string ConcatenateWith<T>(this IEnumerable<T> collection, string separator)
        {
            return ConcatenateWith(collection, separator, false);
        }

        public static string ConcatenateWith<T>(this IEnumerable<T> collection, string separator, bool encodeItems)
        {
            var sb = new StringBuilder();
            var total = collection.Count();
            var count = 0;

            foreach (var item in collection)
            {
                var value = item.ToString();
                var result = encodeItems ? value.UrlEncodeRelaxed() : value;

                sb.Append(result);
                count++;
                
                if (count < total)
                {
                    sb.Append(separator);
                }
            }

            return sb.ToString();
        }

        public static string ConcatenateWith(this WebParameterCollection collection, string separator, string spacer)
        {
            var sb = new StringBuilder();

            var total = collection.Count;
            var count = 0;

            foreach (var item in collection)
            {
                sb.Append(item.Name);
                sb.Append(separator);
                sb.Append(item.Value);

                count++;
                if (count < total)
                {
                    sb.Append(spacer);
                }
            }

            return sb.ToString();
        }
    }
}