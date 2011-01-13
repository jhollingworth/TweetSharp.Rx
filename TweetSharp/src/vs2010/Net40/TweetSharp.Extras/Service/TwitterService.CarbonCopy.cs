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

using TweetSharp.Twitter.Fluent;
using TweetSharp.Twitter.Model;

namespace TweetSharp.Twitter.Service
{
    public static class TwitterServiceExtensions
    {
        /// <summary>
        /// Posts a tweet while simultaneously posting the same text to MySpace as a status update
        /// </summary>
        /// <param name="service">the service</param>
        /// <param name="text">Status text to post</param>
        /// <param name="mySpaceUserId">Id of the MySpace user whose status should be updated</param>
        /// <param name="consumerKey">MySpace OAuth consumer key for the application</param>
        /// <param name="consumerSecret">MySpace OAuth consumer secret for the application</param>
        /// <param name="accessKey">MySpace OAuth access token for the user specified by mySpaceUserId</param>
        /// <param name="accessSecret">MySpace OAuth access token secret for the user specified by mySpaceUserId</param>
        /// <returns>TwitterStatus object as posted</returns>
        public static TwitterStatus SendTweetWithCopyToMySpace(
            this TwitterService service, string text,
            int mySpaceUserId,
            string consumerKey, string consumerSecret,
            string accessKey, string accessSecret)
        {
            return service.WithTweetSharp<TwitterStatus>(q =>
                                                  q.Statuses().Update(text).CopyToMySpace(mySpaceUserId, consumerKey,
                                                                                          consumerSecret, accessKey,
                                                                                          accessSecret));
        }

        /// <summary>
        /// Posts a tweet while simultaneously posting the same text to MySpace as a status update
        /// </summary>
        /// <param name="service">the service</param>
        /// <param name="text">Status text to post</param>
        /// <param name="latitude">Geo-location latitude to associate with the tweet</param>
        /// <param name="longitude">Geo-location longitude to associate with the tweet</param>
        /// <param name="mySpaceUserId">Id of the MySpace user whose status should be updated</param>
        /// <param name="consumerKey">MySpace OAuth consumer key for the application</param>
        /// <param name="consumerSecret">MySpace OAuth consumer secret for the application</param>
        /// <param name="accessKey">MySpace OAuth access token for the user specified by mySpaceUserId</param>
        /// <param name="accessSecret">MySpace OAuth access token secret for the user specified by mySpaceUserId</param>
        /// <returns>TwitterStatus object as posted</returns>
        public static TwitterStatus SendTweetWithCopyToMySpace(
            this TwitterService service, string text, double latitude, double longitude,
            int mySpaceUserId,
            string consumerKey, string consumerSecret,
            string accessKey, string accessSecret)
        {
            return service.WithTweetSharp<TwitterStatus>(q =>
                                                  q.Statuses().Update(text, latitude, longitude).CopyToMySpace(mySpaceUserId, consumerKey,
                                                                                          consumerSecret, accessKey,
                                                                                          accessSecret));
        }

        /// <summary>
        /// Posts a tweet while simultaneously posting the same text to MySpace as a status update
        /// </summary>
        /// <param name="service">the service</param>
        /// <param name="text">Status text to post</param>
        /// <param name="location">Geo-location data to associate with the tweet</param>
        /// <param name="mySpaceUserId">Id of the MySpace user whose status should be updated</param>
        /// <param name="consumerKey">MySpace OAuth consumer key for the application</param>
        /// <param name="consumerSecret">MySpace OAuth consumer secret for the application</param>
        /// <param name="accessKey">MySpace OAuth access token for the user specified by mySpaceUserId</param>
        /// <param name="accessSecret">MySpace OAuth access token secret for the user specified by mySpaceUserId</param>
        /// <returns>TwitterStatus object as posted</returns>
        public static TwitterStatus SendTweetWithCopyToMySpace(
           this TwitterService service, string text, TwitterGeoLocation location,
           int mySpaceUserId,
           string consumerKey, string consumerSecret,
           string accessKey, string accessSecret)
        {
            return service.WithTweetSharp<TwitterStatus>(q =>
                                                  q.Statuses().Update(text, location).CopyToMySpace(mySpaceUserId, consumerKey,
                                                                                          consumerSecret, accessKey,
                                                                                          accessSecret));
        }

        /// <summary>
        /// Posts a tweet while simultaneously posting the same text to Facebook as a status update
        /// </summary>
        /// <param name="service">the service instance</param>
        /// <param name="text">Status text to post</param>
        /// <param name="facebookApiKey">Your application's facebook api key</param>
        /// <param name="facebookSessionKey">Session key associated with your application and the user whose status you want to update</param>
        /// <param name="facebookSessionSecret">Session secret associated with your application and the user whose status you want to update</param>
        /// <returns>TwitterStatus object as posted</returns>
        public static TwitterStatus SendTweetWithCopyToFacebook(this TwitterService service, string text, string facebookApiKey, string facebookSessionKey, string facebookSessionSecret)
        {
            return service.WithTweetSharp<TwitterStatus>(q =>
                                                  q.Statuses().Update(text).CopyToFacebook(facebookApiKey, facebookSessionKey, facebookSessionSecret));
        }

        /// <summary>
        /// Posts a tweet while simultaneously posting the same text to Facebook as a status update
        /// </summary>
        /// <param name="service">the service instance</param>
        /// <param name="text">Status text to post</param>
        /// <param name="latitude">Geo-location latitude to associate with the tweet</param>
        /// <param name="longitude">Geo-location longitude to associate with the tweet</param>
        /// <param name="facebookApiKey">Your application's facebook api key</param>
        /// <param name="facebookSessionKey">Session key associated with your application and the user whose status you want to update</param>
        /// <param name="facebookSessionSecret">Session secret associated with your application and the user whose status you want to update</param>
        /// <returns>TwitterStatus object as posted</returns>
        public static TwitterStatus SendTweetWithCopyToFacebook(this TwitterService service, string text, double latitude, double longitude, string facebookApiKey, string facebookSessionKey, string facebookSessionSecret)
        {
            return service.WithTweetSharp<TwitterStatus>(q =>
                                                  q.Statuses().Update(text, latitude, longitude)
                                                  .CopyToFacebook(facebookApiKey, facebookSessionKey, facebookSessionSecret));
        }

        /// <summary>
        /// Posts a tweet while simultaneously posting the same text to Facebook as a status update
        /// </summary>
        /// <param name="service">the service instance</param>
        /// <param name="text">Status text to post</param>
        /// <param name="location">Geo-location data to associate with the tweet</param>
        /// <param name="facebookApiKey">Your application's facebook api key</param>
        /// <param name="facebookSessionKey">Session key associated with your application and the user whose status you want to update</param>
        /// <param name="facebookSessionSecret">Session secret associated with your application and the user whose status you want to update</param>
        /// <returns>TwitterStatus object as posted</returns>
        public static TwitterStatus SendTweetWithCopyToFacebook(this TwitterService service, string text, TwitterGeoLocation location, string facebookApiKey, string facebookSessionKey, string facebookSessionSecret)
        {
            return service.WithTweetSharp<TwitterStatus>(q =>
                                                  q.Statuses().Update(text, location)
                                                  .CopyToFacebook(facebookApiKey, facebookSessionKey, facebookSessionSecret));
        }
        
    }
}