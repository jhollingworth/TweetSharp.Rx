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
using System.Security.Cryptography;
using System.Text;
using Hammock.Authentication.OAuth;

namespace TweetSharp.Core.Extensions
{
    /// <summary>
    /// Extension methods for working with OAuth data
    /// </summary>
    public static class OAuthExtensions
    {
        /// <summary>
        /// Converts an <see cref="OAuthSignatureMethod"/> into a value 
        /// to be passed to the server as part of the request to indicate the 
        /// signature method used
        /// </summary>
        /// <param name="signatureMethod">the <see cref="OAuthSignatureMethod"/> to convert</param>
        /// <returns>A <see cref="string"/> value in the expected format</returns>
        public static string ToRequestValue(this OAuthSignatureMethod signatureMethod)
        {
            var value = signatureMethod.ToString().ToUpper();
            var shaIndex = value.IndexOf("SHA1");
            return shaIndex > -1 ? value.Insert(shaIndex, "-") : value;
        }

        /// <summary>
        /// Hashes a string using the provided <see cref="HashAlgorithm"/>
        /// </summary>
        /// <param name="input">the string to hash</param>
        /// <param name="algorithm">the algorithm with which to perform the hash</param>
        /// <returns>A Base-64 encoded representation of the hash</returns>
        public static string HashWith(this string input, HashAlgorithm algorithm)
        {
            var data = Encoding
#if !SILVERLIGHT
                .ASCII
#else
                .UTF8
#endif
                .GetBytes(input);
            var hash = algorithm.ComputeHash(data);
            return Convert.ToBase64String(hash);
        }
    }
}