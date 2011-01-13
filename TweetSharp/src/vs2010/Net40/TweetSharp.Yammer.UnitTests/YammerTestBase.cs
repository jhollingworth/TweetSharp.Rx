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
using TweetSharp.UnitTests.Base;

namespace TweetSharp.Yammer.UnitTests
{
    /// <summary>
    /// To run these unit tests you must include, in your /bin/#CONFIG, an xml file called "YammerSetup.xml" 
    /// (or override SetupFileName to return a name of your choosing)
    /// containing your Yammer authorization, and additional information used in the tests. An example
    /// is provided below. Yammer supports only OAuth authentication, so you will need an
    /// a consumerkey and consumer secret from Yammer to test successfully. 
    /// 
    ///  
    ///<example>
    ///<?xml version="1.0" encoding="utf-8" ?>
    ///<setup> 
    ///    <username>username</username> 
    ///    <oauth>
    ///        <consumerKey>KEY</consumerKey>
    ///        <consumerSecret>SECRET</consumerSecret>
    ///    </oauth>
    ///</setup> 
    ///</example>
    ///</summary>
    public abstract class YammerTestBase : OAuthTestBase
    {
        protected string YAMMER_DOMAIN;
        protected string YAMMER_EMAIL_TO_INVITE;
        protected long YAMMER_GROUP_ID;
        protected long YAMMER_PREVIOUS_MESSAGE_ID;
        protected long YAMMER_TAG_ID;
        protected string YAMMER_USERNAME;
        protected long YAMMER_USER_ID;

        protected static string SetupFileName
        {
            get { return "setup.xml"; }
        }

        protected override string OAuthElement
        {
            get { return "yammeroauth"; }
        }

        protected override void LoadSetupFile()
        {
            var document = XDocument.Load(SetupFileName);
            var setup = document.Element("setup");
            var valid = ParseSetupNode(setup);

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
                var username = setup.Element("yammerusername");
                var oauth = setup.Element("yammeroauth");

                if (username == null)
                {
                    valid = false;
                }
                else
                {
                    YAMMER_USERNAME = username.Value;
                }
                valid &= ParseOAuthNode(oauth);

                var email = setup.Element("yammerusertoinvite");
                if (email != null)
                {
                    YAMMER_EMAIL_TO_INVITE = email.Value;
                }

                var domain = setup.Element("yammernetworkdomain");
                if (domain != null)
                {
                    YAMMER_DOMAIN = domain.Value;
                }

                var userid = setup.Element("yammeruserid");
                var tagid = setup.Element("yammertagid");
                var previousMessageId = setup.Element("yammerpreviousmessageid");
                var groupId = setup.Element("yammergroupid");

                valid &= ParseInt64Node(userid, id => YAMMER_USER_ID = id);
                valid &= ParseInt64Node(tagid, id => YAMMER_TAG_ID = id);
                valid &= ParseInt64Node(previousMessageId, id => YAMMER_PREVIOUS_MESSAGE_ID = id);
                valid &= ParseInt64Node(groupId, id => YAMMER_GROUP_ID = id);
            }

            return valid;
        }


        private static bool ParseInt64Node(XElement id, Action<long> assign)
        {
            // This is optional
            if (id == null)
            {
                return false;
            }
            if (String.IsNullOrEmpty(id.Value))
            {
                return false;
            }
#if !Smartphone
            long val;
            if (long.TryParse(id.Value, out val))
            {
                assign(val);
                return true;
            }
#else
            try
            {
                var val = long.Parse(id.Value);
                assign(val);
                return true; 
            }
            catch (FormatException)
            {
            }
#endif
            return false;
        }

        [TearDown]
        public void TearDown()
        {
        }
    }
}