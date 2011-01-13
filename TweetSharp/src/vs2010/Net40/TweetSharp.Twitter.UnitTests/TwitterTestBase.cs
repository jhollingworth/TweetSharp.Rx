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
using System.Xml.Linq;
using NUnit.Framework;
using TweetSharp.Model;
using TweetSharp.Twitter.Extensions;
using TweetSharp.Twitter.Fluent;
using TweetSharp.Twitter.Model;

namespace TweetSharp.UnitTests.Base
{
    /// <summary>
    /// To run these unit tests you must include, in your /bin/#CONFIG, an xml file called "setup.xml",
    /// containing your Twitter authorization, and additional information used in the tests. An example
    /// is provided as Setup.xml.example in this project.
    /// 
    /// Even if you are not testing OAuth, you must provide at least the dummy values
    /// below in order for the unit tests to load correctly.
    /// </summary>
    public abstract class TwitterTestBase : OAuthTestBase
    {
        protected bool ALLOW_POSTS;
        protected string TWITTER_CELEBRITY_SCREEN_NAME;
        protected string TWITTER_FAILWHALE_PROXY;
        protected string TWITTER_FOLLOW_LIST_OWNER;
        protected string TWITTER_FOLLOW_LIST_SLUG;
        protected string TWITTER_MEMBER_OF_PERM_LIST;
        protected string TWITTER_NONFRIEND_SCREEN_NAME;
        protected string TWITTER_NON_MEMBER_OF_PERM_LIST;
        protected string TWITTER_NON_SUBSCRIBER_OF_PERM_LIST;
        protected string TWITTER_PROTECTEDUSER;
        
        // Lists API
        protected long TWITTER_PERMANENT_LIST_ID;
        protected string TWITTER_PERMANENT_LIST_SLUG;
        protected int TWITTER_RECIPIENT_ID;
        protected string TWITTER_RECIPIENT_SCREEN_NAME;
        protected string TWITTER_SUBSCRIBER_OF_PERM_LIST;
        protected string TWITTER_TEMPORARY_LIST_SLUG;
        protected string TWITTER_TRANSPARENT_PROXY;
        protected string TWITTER_USERNAME;
        protected OAuthToken TWITTER_OAUTH;

        // Carbon copy services
        protected string FACEBOOK_API_KEY;
        protected string FACEBOOK_SESSION_KEY;
        protected string FACEBOOK_SESSION_SECRET;

        protected string MYSPACE_CONSUMER_KEY;
        protected string MYSPACE_CONSUMER_SECRET;
        protected string MYSPACE_ACCESS_TOKEN;
        protected string MYSPACE_TOKEN_SECRET;
        protected int MYSPACE_USER_ID;

        protected override void LoadSetupFile()
        {
            var document = XDocument.Load("setup.xml");
            var setup = document.Element("setup");
            var valid = ParseSetupNode(setup);

            TWITTER_OAUTH = LoadToken("access");

            if (!valid)
            {
                throw new TweetSharpException("Authorization could not be parsed from setup.xml");
            }
        }

        private bool ParseSetupNode(XContainer setup)
        {
            var valid = true;

            if (setup == null)
            {
                valid = false;
            }
            else
            {
                var username = setup.Element("username");
                var proxy = setup.Element("proxy");
                var celebrity = setup.Element("celebrity");
                var recipient = setup.Element("recipient");
                var nonFriend = setup.Element("nonfriend");
                var protecteduser = setup.Element("protecteduser");
                var protectedpassword = setup.Element("protecteduserpassword");
                var oauth = setup.Element("oauth");
                var lists = setup.Element("lists");
                var allowPosts = setup.Element("allowposts");
                var failwhaleproxy = setup.Element("failwhaleproxy");
                var facebookapikey = setup.Element("facebookapikey");
                var facebooksessionkey = setup.Element("facebooksessionkey");
                var facebooksessionsecret = setup.Element("facebooksessionsecret");
                if (username == null)
                {
                    valid = false;
                }
                else
                {
                    TWITTER_USERNAME = username.Value;
                }

                valid &= ParseAllowPostsNode(allowPosts);
                valid &= ParseRecipientNode(recipient);
                valid &= ParseCelebrityNode(celebrity);
                valid &= ParseOAuthNode(oauth);
                valid &= ParseNonFriendNode(nonFriend);
                valid &= ParseProxyNode(proxy);
                valid &= ParseListsNode(lists);
                valid &= ParseFailWhaleProxyNode(failwhaleproxy);
                valid &= ParseFacebookSettings(facebookapikey, facebooksessionkey, facebooksessionsecret);
                valid &= ParseMySpaceSettings(setup);
                valid &= ParseProtectedUser(protecteduser, protectedpassword);
            }

            return valid;
        }

        private bool ParseProtectedUser(XElement protecteduser, XElement protectedpassword)
        {
            TWITTER_PROTECTEDUSER = protecteduser != null ? protecteduser.Value : string.Empty;
            return true;
        }

        private bool ParseMySpaceSettings(XContainer setup)
        {
            var myspaceNode = setup.Element("myspaceoauth");
            if (myspaceNode != null)
            {
                var consumerKeyElement = myspaceNode.Element("consumerKey");
                var consumerSecretElement = myspaceNode.Element("consumerSecret");
                var accessTokenElement = myspaceNode.Element("accessToken");
                var tokenSecretElement = myspaceNode.Element("accessTokenSecret");
                var userIdElement = myspaceNode.Element("userid");
                MYSPACE_CONSUMER_KEY = consumerKeyElement != null ? consumerKeyElement.Value : "";
                MYSPACE_CONSUMER_SECRET = consumerSecretElement != null ? consumerSecretElement.Value : "";
                MYSPACE_ACCESS_TOKEN = accessTokenElement != null ? accessTokenElement.Value : "";
                MYSPACE_TOKEN_SECRET = tokenSecretElement != null ? tokenSecretElement.Value : "";
                if (userIdElement != null)
                {
                    try
                    {
                        MYSPACE_USER_ID = int.Parse(userIdElement.Value);
                    }
                    catch (FormatException)
                    {
                    }
                }
            }
            return true;
        }

        private bool ParseAllowPostsNode(XElement allowPosts)
        {
            var ret = true;
#if !Smartphone

            bool val;
            if (bool.TryParse(allowPosts.Value, out val))
            {
                ALLOW_POSTS = val;
            }
            else
            {
                ret = false;
            }
#else
            try
            {
                ALLOW_POSTS = bool.Parse(allowPosts.Value);
            }
            catch (FormatException)
            {
                ret = false;
            }
#endif
            return ret;
        }

        protected bool ParseListsNode(XContainer lists)
        {
            // This is optional
            if (lists != null)
            {
                var listSlug = lists.Element("listSlug");
                var listId = lists.Element("listId");
                var tempListSlug = lists.Element("tempListSlug");
                var listMember = lists.Element("listMember");
                var nonListMember = lists.Element("nonListMember");
                var listSubscriber = lists.Element("listSubscriber");
                var nonListSubscriber = lists.Element("nonListSubscriber");
                var followListSlug = lists.Element("followListSlug");
                var followListOwner = lists.Element("followListOwner");

                TWITTER_PERMANENT_LIST_SLUG = listSlug != null ? listSlug.Value : string.Empty;
                TWITTER_TEMPORARY_LIST_SLUG = tempListSlug != null ? tempListSlug.Value : string.Empty;
                TWITTER_MEMBER_OF_PERM_LIST = listMember != null ? listMember.Value : string.Empty;
                TWITTER_NON_MEMBER_OF_PERM_LIST = nonListMember != null ? nonListMember.Value : string.Empty;
                TWITTER_SUBSCRIBER_OF_PERM_LIST = listSubscriber != null ? listSubscriber.Value : string.Empty;
                TWITTER_NON_SUBSCRIBER_OF_PERM_LIST = nonListSubscriber != null ? nonListSubscriber.Value : string.Empty;

                TWITTER_FOLLOW_LIST_SLUG = followListSlug != null ? followListSlug.Value : string.Empty;
                TWITTER_FOLLOW_LIST_OWNER = followListOwner != null ? followListOwner.Value : string.Empty;

                ParsePermanentListId(listId);
            }
            return true;
        }

        private void ParsePermanentListId(XElement id)
        {
            // This is optional
            if (id == null)
            {
                return;
            }
            if (String.IsNullOrEmpty(id.Value))
            {
                return;
            }
#if !Smartphone
            long val;
            if (long.TryParse(id.Value, out val))
            {
                TWITTER_PERMANENT_LIST_ID = val;
            }
#else
            try
            {
                TWITTER_PERMANENT_LIST_ID = long.Parse(id.Value);
            }
            catch (FormatException)
            {
            }
#endif
        }

        private bool ParseProxyNode(XElement proxy)
        {
            // This is optional
            if (proxy != null)
            {
                if (!String.IsNullOrEmpty(proxy.Value))
                {
                    TWITTER_TRANSPARENT_PROXY = proxy.Value;
                }
            }

            return true;
        }

        private bool ParseFailWhaleProxyNode(XElement proxy)
        {
            // This is optional
            if (proxy != null)
            {
                if (!String.IsNullOrEmpty(proxy.Value))
                {
                    TWITTER_FAILWHALE_PROXY = proxy.Value;
                }
            }
            return true;
        }

        private bool ParseFacebookSettings(XElement apiKey, XElement sessionKey, XElement sessionSecret)
        {
            //optional settings
            FACEBOOK_API_KEY = apiKey != null ? apiKey.Value : string.Empty;
            FACEBOOK_SESSION_KEY = sessionKey != null ? sessionKey.Value : string.Empty;
            FACEBOOK_SESSION_SECRET = sessionSecret != null ? sessionSecret.Value : string.Empty;
            return true;
        }

        private bool ParseCelebrityNode(XContainer celebrity)
        {
            var valid = true;

            if (celebrity == null)
            {
                valid = false;
            }
            else
            {
                var celebrityScreenName = celebrity.Element("screenName");

                if (celebrityScreenName == null)
                {
                    valid = false;
                }
                else
                {
                    TWITTER_CELEBRITY_SCREEN_NAME = celebrityScreenName.Value;
                }
            }
            return valid;
        }

        private bool ParseNonFriendNode(XContainer nonfriend)
        {
            var valid = true;

            if (nonfriend == null)
            {
                valid = false;
            }
            else
            {
                var nonFriendScreenName = nonfriend.Element("screenName");

                if (nonFriendScreenName == null)
                {
                    valid = false;
                }
                else
                {
                    TWITTER_NONFRIEND_SCREEN_NAME = nonFriendScreenName.Value;
                }
            }
            return valid;
        }

        private bool ParseRecipientNode(XContainer recipient)
        {
            var valid = true;

            if (recipient == null)
            {
                valid = false;
            }
            else
            {
                var recipientId = recipient.Element("id");
                var recipientScreenName = recipient.Element("screenName");

                if (recipientId == null)
                {
                    valid = false;
                }
                else
                {
#if !Smartphone
                    int id;
                    if (Int32.TryParse(recipientId.Value, out id))
                    {
                        TWITTER_RECIPIENT_ID = id;
                    }
                    else
                    {
                        valid = false;
                    }
#else
                    try
                    {
                        TWITTER_RECIPIENT_ID = Int32.Parse(recipientId.Value);
                    }
                    catch (FormatException)
                    {
                        valid = false;
                    }
#endif
                }

                if (recipientScreenName == null)
                {
                    valid = false;
                }
                else
                {
                    TWITTER_RECIPIENT_SCREEN_NAME = recipientScreenName.Value;
                }
            }
            return valid;
        }

        [TearDown]
        public void TearDown()
        {
            var twitter = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .Account().GetRateLimitStatus()
                .AsJson();

            var response = twitter.Request();
            //don't call IgnoreFailWhales here as it will actually result in a 
            //test failure in the event of a whale, just ignore the result
            if (!response.IsFailWhale)
            {
                var status = response.AsRateLimitStatus();
                Assert.IsNotNull(status);

                Console.WriteLine("--------------------");
                Console.WriteLine("Remaining requests for @{0}: {1} / {2}", TWITTER_USERNAME, status.RemainingHits,
                                  status.HourlyLimit);
            }
            else
            {
                Console.WriteLine("Getting rate limit status in test teardown resulted in a FailWhale.");
            }
        }

        protected static void IgnoreFailWhales(TwitterResult result)
        {
            Assert.IsNotNull(result);
            if (result.IsFailWhale)
            {
                Assert.Ignore("Failwhale ahoy");
            }
        }
    }
}