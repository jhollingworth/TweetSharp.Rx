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
using System.Windows;
using TweetSharp;
using TweetSharp.Extensions;
using TweetSharp.Fluent;
using TweetSharp.Model;
using TweetSharp.Twitter.Extensions;
using TweetSharp.Twitter.Fluent;
using TweetSharp.Twitter.Model;

namespace Demo.Silverlight
{
    public partial class MainPage
    {
        // This is your server set up with a TweetSharp-compatible proxy
        private const string ProxyUrl = "http://localhost:9595/proxy";

        // This is your consumer key; you should protect yours
        private const string ConsumerKey = "FoPkSSxcwjGzRpTVe4zPVg";

        // This is your consumer secret; you should protect yours
        private const string ConsumerSecret = "hE3BPGfo6SNedRii5s7cdzEqqh3X2RQTJQoqAyhpa4";

        public MainPage()
        {
            InitializeComponent();

            Loaded += MainPage_Loaded;
        }

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            SetClientInfo();

            // Set up workflow to trigger events
            OAuthBus.RequestTokenRetrieved += OAuthBus_RequestTokenRetrieved;
            OAuthBus.AccessTokenRetrieved += OAuthBus_AccessTokenRetrieved;

            // Step 1 - Obtain a Request Token
            var requestToken = FluentTwitter.CreateRequest()
                .Configuration.UseTransparentProxy(ProxyUrl)
                .Authentication.GetRequestToken()
                .CallbackTo(RequestTokenReceived);

            requestToken.BeginRequest();
        }

        private static void SetClientInfo()
        {
            var clientInfo = new TwitterClientInfo
                                 {
                                     ConsumerKey = ConsumerKey,
                                     ConsumerSecret = ConsumerSecret
                                 };

            FluentBase<TwitterResult>.SetClientInfo(clientInfo);
        }

        private static void RequestTokenReceived(object sender, TwitterResult result, object userState)
        {
            var token = result.AsToken();

            if (token != null)
            {
                var args = new OAuthEventArgs(token);
                OAuthBus.OnRequestTokenRetrieved(args);
            }
        }

        private static void AccessTokenReceived(object sender, TwitterResult result, object userState)
        {
            var token = result.AsToken();

            if (token != null)
            {
                var args = new OAuthEventArgs(token);
                OAuthBus.OnAccessTokenRetrieved(args);
            }
        }

        private void OAuthBus_AccessTokenRetrieved(object sender, OAuthEventArgs e)
        {
            // Step 4 - Use Access Token
            var verify = FluentTwitter.CreateRequest()
                .Configuration.UseTransparentProxy(ProxyUrl)
                .AuthenticateWith(e.Token.Token, e.Token.TokenSecret)
                .CallbackTo((s, r, u) =>
                            Dispatcher.BeginInvoke(() =>
                                                       {
                                                           var message = string.Format("Welcome, {0}!",
                                                                                       r.AsUser().ScreenName);
                                                           LoginPin.Text = message;
                                                       }))
                .Account().VerifyCredentials();

            verify.BeginRequest();
        }

        private void OAuthBus_RequestTokenRetrieved(object sender, OAuthEventArgs e)
        {
            // Step 2 - Get PIN from Twitter
            var requestToken = e.Token;
            var authorizeUrl = FluentTwitter.CreateRequest()
                .Authentication.GetAuthorizationUrl(requestToken.Token);
            var uri = new Uri(authorizeUrl);

            Dispatcher.BeginInvoke(() =>
                                       {
                                           LoginButton.NavigateUri = uri;
                                           LoginButton.Tag = requestToken;
                                           LoginButton.Content = "Login with OAuth";
                                           LoginButton.Click += LoginButton_Click;
                                       });
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            Dispatcher.BeginInvoke(() => PinControl.Visibility = Visibility.Visible);
        }

        private void SubmitPin_Click(object sender, RoutedEventArgs e)
        {
            var verifier = LoginPin.Text;
            var requestToken = (OAuthToken) LoginButton.Tag;

            // Step 3 - Get Access Token
            var accessToken = FluentTwitter.CreateRequest()
                .Configuration.UseTransparentProxy(ProxyUrl)
                .Authentication.GetAccessToken(requestToken.Token, verifier)
                .CallbackTo(AccessTokenReceived);

            accessToken.BeginRequest();
        }
    }
}