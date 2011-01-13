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
    /// Authentication details for the Facebook API
    /// </summary>
    public class FacebookAuthentication : IExternalAuthenticationDetails
    {
        /// <summary>
        /// Gets or sets the application's API key.  This is provided to 
        /// you by Facebook when you register your app
        /// </summary>
        public string ApplicationApiKey { get; set; }
        /// <summary>
        /// Gets or sets the user's Session key.  This must be a persistent 
        /// session id with permission to post status updates
        /// </summary>
        public string SessionKey { get; set; }
        /// <summary>
        /// Gets or sets the secret assoociated with the <see cref="SessionKey"/>
        /// </summary>
        public string SessionSecret { get; set; }
    }
}
