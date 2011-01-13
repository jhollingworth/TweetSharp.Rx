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

using TweetSharp.Fluent;
using TweetSharp.Model;

namespace TweetSharp.Twitter.Fluent
{
    public static class TwitterCarbonCopyServicesExtensions
    {
        public static ITwitterStatusUpdate CopyToFacebook(
            this ITwitterStatusUpdate instance, string facebookApiKey, string facebookSessionKey, string facebookSessionSecret)
        {
            var auth = new FacebookAuthentication
                           {
                               ApplicationApiKey = facebookApiKey,
                               SessionKey = facebookSessionKey,
                               SessionSecret = facebookSessionSecret
                           };

            instance.Root.ExternalAuthentication.Add(AuthenticationMode.Facebook, auth);
            instance.Root.Parameters.CopyTo |= ExternalService.Facebook;
            return instance;
        }

        public static ITwitterStatusUpdate CopyToMySpace(
            this ITwitterStatusUpdate instance,
            int mySpaceUserId,
            string consumerKey,
            string consumerSecret,
            string accessToken,
            string accessTokenSecret)
        {
            var auth = new MySpaceAuthentication
                           {
                               ConsumerKey = consumerKey,
                               ConsumerSecret = consumerSecret,
                               AccessToken = accessToken,
                               TokenSecret = accessTokenSecret,
                               UserId = mySpaceUserId,
                           };
            instance.Root.ExternalAuthentication.Add(AuthenticationMode.MySpace, auth);
            instance.Root.Parameters.CopyTo |= ExternalService.MySpace;
            return instance; 
        }
    }
}