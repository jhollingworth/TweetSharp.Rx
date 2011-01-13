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

#if !Smartphone && !SILVERLIGHT
using System.Net.Mail;
#endif

namespace TweetSharp.Twitter.Fluent
{
    partial class Extensions
    {
        /// <summary>
        /// Updates the user profile name.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public static ITwitterAccountUpdateProfile UpdateName(this ITwitterAccountUpdateProfile instance, string name)
        {
            instance.Root.Profile.ProfileName = name;
            return instance;
        }

#if !Smartphone && !SILVERLIGHT
        /// <summary>
        /// Updates the user profile email.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="email">The email.</param>
        /// <returns></returns>
        public static ITwitterAccountUpdateProfile UpdateEmail(this ITwitterAccountUpdateProfile instance,
                                                                MailAddress email)
        {
            instance.Root.Parameters.Email = email.Address;
            return instance;
        }
#endif

        /// <summary>
        /// Updates the user profile email.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="email">The email.</param>
        /// <returns></returns>
        public static ITwitterAccountUpdateProfile UpdateEmail(this ITwitterAccountUpdateProfile instance,
                                                               string email)
        {
            instance.Root.Parameters.Email = email;
            return instance;
        }

        /// <summary>
        /// Updates the user profile URL.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="url">The URL.</param>
        /// <returns></returns>
        public static ITwitterAccountUpdateProfile UpdateUrl(this ITwitterAccountUpdateProfile instance, string url)
        {
            instance.Root.Profile.ProfileUrl = url;
            return instance;
        }

        /// <summary>
        /// Updates the user profile URL.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="uri">The URI.</param>
        /// <returns></returns>
        public static ITwitterAccountUpdateProfile UpdateUrl(this ITwitterAccountUpdateProfile instance, Uri uri)
        {
            instance.Root.Profile.ProfileUrl = uri.ToString();
            return instance;
        }

        /// <summary>
        /// Updates the user profile location. This is self-described and does not contain geolocation data.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="location">The location.</param>
        /// <returns></returns>
        public static ITwitterAccountUpdateProfile UpdateLocation(this ITwitterAccountUpdateProfile instance,
                                                                  string location)
        {
            instance.Root.Profile.ProfileLocation = location;
            return instance;
        }
        
        /// <summary>
        /// Updates the user profile description / bio.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="description">The description.</param>
        /// <returns></returns>
        public static ITwitterAccountUpdateProfile UpdateDescription(this ITwitterAccountUpdateProfile instance,
                                                                     string description)
        {
            instance.Root.Profile.ProfileDescription = description;
            return instance;
        }
    }
}