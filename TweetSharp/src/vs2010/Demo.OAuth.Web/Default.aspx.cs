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
using System.Configuration;
using System.Web.UI;
using TweetSharp.Extensions;
using TweetSharp.Fluent;
using TweetSharp.Model;
using TweetSharp.Twitter.Extensions;
using TweetSharp.Twitter.Fluent;
using TweetSharp.Twitter.Model;

namespace Demo.OAuth.Web
{
    public partial class _Default : Page
    {
        private string _consumerKey;
        private string _consumerSecret;

        protected void Page_Load(object sender, EventArgs e)
        {
            // add these to web.config
            _consumerKey = ConfigurationManager.AppSettings["consumerKey"];
            _consumerSecret = ConfigurationManager.AppSettings["consumerSecret"];

            var requestToken = Request["oauth_token"];
            var requestVerifier = Request["oauth_verifier"];

            if (requestToken == null)
            {
                // This callback URL overrides the one set up via Twitter application settings
                var request = GetRequestToken("http://localhost:8080");

                var authorizeUrl = FluentTwitter.CreateRequest()
                    .Authentication.GetAuthorizationUrl(request.Token);

                Response.Redirect(authorizeUrl);
            }
            else
            {
                // exchange returned request token for access token
                var access = GetAccessToken(requestToken, requestVerifier);

                // make an oauth-authenticated call with the access token,
                // and remember you need to persist this token for this user's auth
                var query = FluentTwitter.CreateRequest()
                    .AuthenticateWith(_consumerKey,
                                      _consumerSecret,
                                      access.Token,
                                      access.TokenSecret)
                    .Account()
                    .VerifyCredentials()
                    .AsXml();

                // OAuth is still in Beta, YMMV
                var response = query.Request();
                GetResponse(response);
            }
        }

        private void GetResponse(TwitterResult response)
        {
            var identity = response.AsUser();
            if (identity != null)
            {
                trace.InnerHtml = String.Format("{0} authenticated successfully.", identity.ScreenName);
            }
            else
            {
                var error = response.AsError();
                if (error != null)
                {
                    trace.InnerHtml = error.ErrorMessage;
                }
            }
        }

        private OAuthToken GetAccessToken(string requestToken, string verifier)
        {
            var accessToken = FluentTwitter.CreateRequest()
                .Authentication.GetAccessToken(_consumerKey, _consumerSecret, requestToken, verifier);

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

        private OAuthToken GetRequestToken(string callbackUrl)
        {
            var requestToken = FluentTwitter.CreateRequest()
                .Authentication.GetRequestToken(_consumerKey, _consumerSecret, callbackUrl);

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
    }
}