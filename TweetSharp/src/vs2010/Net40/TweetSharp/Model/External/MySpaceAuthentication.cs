#region License

// Dimebrain TweetSharp
// (www.dimebrain.com)
// 
// The MIT License
// 
// Copyright (c) 2010 Daniel Crenna & Jason Diller
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
// 

#endregion

namespace TweetSharp.Model
{
    /// <summary>
    /// Authentication details for the MySpace API
    /// </summary>
    public class MySpaceAuthentication : IExternalAuthenticationDetails 
    {
        /// <summary>
        /// OAuth Consumer key provided for your application by MySpace
        /// </summary>
        public string ConsumerKey { get; set; }
        /// <summary>
        /// OAuth consumer secret provided for your application by MySpace
        /// </summary>
        public string ConsumerSecret { get; set; }
        /// <summary>
        /// The user's access token.  Must have permission to post status updates 
        /// </summary>
        public string AccessToken { get; set; }
        /// <summary>
        /// The secret associated with <see cref="AccessToken"/>
        /// </summary>
        public string TokenSecret { get; set; }
        /// <summary>
        /// The MySpace id of the user you want to post status updates for
        /// </summary>
        public int UserId { get; set; }
    }
}
