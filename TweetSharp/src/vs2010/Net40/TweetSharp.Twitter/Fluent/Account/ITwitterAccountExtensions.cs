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

using TweetSharp.Twitter.Model;

namespace TweetSharp.Twitter.Fluent
{
    /// <summary>
    /// Fluent interface methods for the Account endpoints
    /// </summary>
    public static class ITwitterAccountExtensions
    {
        /// <summary>
        /// Verifies that the provided credentials are valid
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public static ITwitterAccountVerifyCredentials VerifyCredentials(this ITwitterAccount instance)
        {
            instance.Root.Parameters.Action = "verify_credentials";
            return new TwitterAccountVerifyCredentials(instance.Root);
        }

        /// <summary>
        /// Causes Twitter to return a null cookie. Generally not needed for clients. 
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public static ITwitterAccountEndSession EndSession(this ITwitterAccount instance)
        {
            instance.Root.Parameters.Action = "end_session";
            return new TwitterAccountEndSession(instance.Root);
        }

        /// <summary>
        /// Changes the notification delivery device settings for the authenticating user
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="device">A <see cref="TwitterDeliveryDevice"/> describing the device</param>
        /// <returns></returns>
        public static ITwitterAccountEndSession UpdateDeliveryDeviceTo(this ITwitterAccount instance,
                                                                       TwitterDeliveryDevice device)
        {
            instance.Root.Parameters.Action = "update_delivery_device";
            instance.Root.Profile.ProfileDeliveryDevice = device;
            return new TwitterAccountEndSession(instance.Root);
        }

        /// <summary>
        /// Updates the website profile colors for the authenticating user
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public static ITwitterAccountUpdateProfileColors UpdateProfileColors(this ITwitterAccount instance)
        {
            instance.Root.Parameters.Action = "update_profile_colors";
            return new TwitterAccountUpdateProfileColors(instance.Root);
        }

        /// <summary>
        /// Changes the profile image (avatar) for the authenticating uer
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="path">Path to the new avatar image</param>
        /// <returns></returns>
        public static ITwitterAccountUpdateProfileImage UpdateProfileImage(this ITwitterAccount instance, string path)
        {
            instance.Root.Parameters.Action = "update_profile_image";
            instance.Root.Profile.ProfileImagePath = path;
            return new TwitterAccountUpdateProfileImage(instance.Root);
        }

        /// <summary>
        /// Changes the website background image for the authenticated user
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="path">Path to the new background image file</param>
        /// <returns></returns>
        public static ITwitterAccountUpdateProfileBackgroundImage UpdateProfileBackgroundImage(
            this ITwitterAccount instance, string path)
        {
            instance.Root.Parameters.Action = "update_profile_background_image";
            instance.Root.Profile.ProfileBackgroundImagePath = path;
            return new TwitterAccountUpdateProfileBackgroundImage(instance.Root);
        }

        /// <summary>
        /// Gets the current rate limiting details for the authenticating user
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public static ITwitterAccountRateLimitStatus GetRateLimitStatus(this ITwitterAccount instance)
        {
            instance.Root.Parameters.Action = "rate_limit_status";
            return new TwitterAccountRateLimitStatus(instance.Root);
        }

        /// <summary>
        /// Update profile data for the authenticating user. 
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public static ITwitterAccountUpdateProfile UpdateProfile(this ITwitterAccount instance)
        {
            instance.Root.Parameters.Action = "update_profile";
            return new TwitterAccountUpdateProfile(instance.Root);
        }
    }
}