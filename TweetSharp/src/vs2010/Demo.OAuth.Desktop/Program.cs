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
using TweetSharp.Model;
using TweetSharp.Twitter.Extensions;
using TweetSharp.Twitter.Fluent;
using TweetSharp.Twitter.Model;

namespace Demo.OAuth.Desktop
{
    internal class Program
    {
        private static void Main()
        {
            // get these from app.config
            var consumerKey = Settings.Default.ConsumerKey;
            var consumerSecret = Settings.Default.ConsumerSecret;

            // get an authenticated request token from twitter
            var requestToken = GetRequestToken(consumerKey, consumerSecret);

            // automatically starts the default web browser, sending the 
            // user to the authorization URL.
            FluentTwitter.CreateRequest()
                .Authentication
                .AuthorizeDesktop(consumerKey,
                                  consumerSecret,
                                  requestToken.Token);

            // user authorization occurs out of band, so wait here
            Console.Write("Press any key when authorization is granted...");
            Console.ReadKey();

            Console.WriteLine("Enter the PIN provided by twitter.com:");
            var pin = Console.ReadLine();

            // exchange the unauthenticated request token with an authenticated access token,
            // and remember to persist this authentication pair for future use
            var accessToken = GetAccessToken(consumerKey, consumerSecret, requestToken.Token, pin);

            // make an authenticated call to Twitter with the token and secret
            var verify = FluentTwitter.CreateRequest()
                .AuthenticateWith(consumerKey, consumerSecret, accessToken.Token, accessToken.TokenSecret)
                .Statuses().Update("Running the #tweetsharp desktop OAuth sample.");

            var response = verify.Request();
            GetResponse(response);

            var mentions = FluentTwitter.CreateRequest()
                .AuthenticateWith(consumerKey, consumerSecret, accessToken.Token, accessToken.TokenSecret)
                .Statuses().Mentions();

            mentions.Request();

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        private static void GetResponse(TwitterResult response)
        {
            var identity = response.AsUser();
            if (identity != null)
            {
                Console.WriteLine("{0} authenticated successfully.", identity.ScreenName);
            }
            else
            {
                var error = response.AsError();
                if (error != null)
                {
                    Console.WriteLine(error.ErrorMessage);
                }
            }
        }

        private static OAuthToken GetRequestToken(string consumerKey, string consumerSecret)
        {
            var requestToken = FluentTwitter.CreateRequest()
                .Authentication.GetRequestToken(consumerKey, consumerSecret);

            var response = requestToken.Request();
            var result = response.AsToken();

            if (result == null)
            {
                var error = response.AsError();
                if (error != null)
                {
                    throw new Exception(error.ErrorMessage);
                }
            }

            return result;
        }

        private static OAuthToken GetAccessToken(string consumerKey, string consumerSecret, string token, string pin)
        {
            var accessToken = FluentTwitter.CreateRequest()
                .Authentication.GetAccessToken(consumerKey, consumerSecret, token, pin);

            var response = accessToken.Request();
            var result = response.AsToken();

            if (result == null)
            {
                var error = response.AsError();
                if (error != null)
                {
                    throw new Exception(error.ErrorMessage);
                }
            }

            return result;
        }
    }
}