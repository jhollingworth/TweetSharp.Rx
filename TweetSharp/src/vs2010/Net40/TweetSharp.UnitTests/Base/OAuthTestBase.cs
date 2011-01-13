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
using System.Net;
using System.Xml.Linq;
using TweetSharp.Model;

namespace TweetSharp.UnitTests.Base
{
    public abstract class OAuthTestBase : TestBase
    {
        protected string OAUTH_CONSUMER_KEY;
        protected string OAUTH_CONSUMER_SECRET;

        protected virtual string OAuthElement
        {
            get { return "oauth"; }
        }

        protected override void LoadSetupFile()
        {
            // todo this is called in FluentTwitter, but not outside of that scope
            ServicePointManager.Expect100Continue = false;

            Document = XDocument.Load("setup.xml");
            var setup = Document.Element("setup");
            bool valid;

            if (setup != null)
            {
                var oauth = setup.Element("oauth");
                valid = ParseOAuthNode(oauth);
            }
            else
            {
                valid = false;
            }

            if (!valid)
            {
                throw new Exception("Authorization could not be parsed from setup.xml");
            }
        }

        protected bool ParseOAuthNode(XContainer oauth)
        {
            var valid = true;

            if (oauth == null)
            {
                valid = false;
            }
            else
            {
                var consumerKey = oauth.Element("consumerKey");
                var consumerSecret = oauth.Element("consumerSecret");

                if (consumerKey == null)
                {
                    valid = false;
                }
                else
                {
                    OAUTH_CONSUMER_KEY = consumerKey.Value;
                }

                if (consumerSecret == null)
                {
                    valid = false;
                }
                else
                {
                    OAUTH_CONSUMER_SECRET = consumerSecret.Value;
                }
            }

            return valid;
        }

        private XElement LoadOAuthInfo()
        {
            if (Document == null)
            {
                Document = XDocument.Load("setup.xml");
            }

            var setup = Document.Element("setup");
            if (setup == null)
            {
                throw new Exception("Setup info could not be parsed from setup.xml");
            }

            var oauth = setup.Element(OAuthElement);
            if (oauth == null)
            {
                throw new Exception("OAuth info could not be parsed from setup.xml");
            }

            return oauth;
        }

        protected void SaveToken(OAuthToken token, string root)
        {
            var oauth = LoadOAuthInfo();

            var tokenElement = oauth.Element(string.Format("{0}Token", root));
            var tokenSecretElement = oauth.Element(string.Format("{0}TokenSecret", root));

            if (tokenElement == null || tokenSecretElement == null)
            {
                return;
            }

            tokenElement.Value = token.Token;
            tokenSecretElement.Value = token.TokenSecret;
            Document.Save("setup.xml", SaveOptions.None);
        }

        protected void SavePin(string pin)
        {
            var oauth = LoadOAuthInfo();

            var pinElement = oauth.Element("pin");
            if (pinElement == null)
            {
                return;
            }

            pinElement.Value = pin;

            Document.Save("setup.xml", SaveOptions.None);
        }

        protected OAuthToken LoadToken(string root)
        {
            var oauth = LoadOAuthInfo();

            var tokenElement = oauth.Element(string.Format("{0}Token", root));
            var tokenSecretElement = oauth.Element(string.Format("{0}TokenSecret", root));

            if (tokenElement == null || tokenSecretElement == null)
            {
                return null;
            }

            return new OAuthToken { Token = tokenElement.Value, TokenSecret = tokenSecretElement.Value};
        }

        protected string LoadPin()
        {
            var oauth = LoadOAuthInfo();

            var pinElement = oauth.Element("pin");


            if (pinElement == null)
            {
                return null;
            }

            return pinElement.Value;
        }
    }
}