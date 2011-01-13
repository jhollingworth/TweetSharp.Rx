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
using System.Linq;
using TweetSharp.Fluent;
using NUnit.Framework;
using TweetSharp.Yammer.Extensions;
using TweetSharp.Yammer.Fluent;

namespace TweetSharp.Yammer.UnitTests.Fluent
{
    partial class FluentYammerTests
    {
        private long PostMessageAndFavoriteIt()
        {
            var token = LoadToken("access");
            var yammer = FluentYammer.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, token.Token, token.TokenSecret)
                .Messages()
                .Post(string.Format("This message will be my favourite soon. at {0} on {1}",
                                    DateTime.Now.ToShortTimeString(), DateTime.Now.ToLongDateString()))
                .AsJson();

            Console.WriteLine(yammer);
            var response = yammer.Request();
            Assert.IsNotNull(response);
            Assert.IsNull(response.Exception);
            Assert.IsFalse(response.IsYammerError);
            var message = response.AsMessage();
            Assert.IsNotNull(message);

            var y2 = FluentYammer.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, token.Token, token.TokenSecret)
                .Messages()
                .Favorites().Create(message.Id)
                .AsJson();


            response = y2.Request();
            Assert.IsNotNull(response);
            Assert.IsNull(response.Exception);
            Assert.IsFalse(response.IsYammerError);
            return message.Id;
        }

        [Test]
        [Category("Messages")]
        public void Can_delete_message()
        {
            var token = LoadToken("access");
            var yammer = FluentYammer.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, token.Token, token.TokenSecret)
                .Messages()
                .Post(string.Format("Posting a message that will be deleted.  At {0} on {1}",
                                    DateTime.Now.ToShortTimeString(), DateTime.Now.ToLongDateString()))
                .AsJson();


            var response = yammer.Request();
            var message = response.AsMessage();
            Assert.IsNotNull(message);

            var yammerDelete = FluentYammer.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, token.Token, token.TokenSecret)
                .Messages()
                .Delete(message.Id)
                .AsJson();

            var replyResponse = yammerDelete.Request();
            Assert.IsNotNull(replyResponse);
            Assert.IsFalse(replyResponse.IsYammerError);
            Assert.IsNull(replyResponse.Exception);
        }

        [Test]
        [Category("YammerMessages")]
        public void Can_get_all_messages_as_json()
        {
            var token = LoadToken("access");

            var yammer = FluentYammer.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, token.Token, token.TokenSecret)
                .Messages()
                .All()
                .AsJson();

            var response = yammer.Request();
            var messages = response.AsMessages();
            Assert.IsNotNull(messages);
            Assert.IsNotNull(response);
            Assert.IsNull(response.Exception);
            Assert.IsFalse(response.IsYammerError);
            //test that the message was parsed correctly
            messages.ToList().ForEach(VerifyMessage);
        }

        [Test]
        [Category("YammerMessages")]
        public void Can_get_all_messages_as_xml()
        {
            var token = LoadToken("access");

            var yammer = FluentYammer.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, token.Token, token.TokenSecret)
                .Messages()
                .All()
                .AsXml();

            var response = yammer.Request();
            var messages = response.AsMessages();
            Assert.IsNotNull(response);
            Assert.IsNull(response.Exception);
            Assert.IsFalse(response.IsYammerError);
            Assert.IsNotNull(messages);
            Assert.IsTrue(messages.Any());

            //test that the message was parsed correctly
            messages.ToList().ForEach(VerifyMessage);
        }

        [Test]
        [Category("YammerMessages")]
        public void Can_get_all_messages_newer_than_some_id_as_json()
        {
            var token = LoadToken("access");

            var yammer = FluentYammer.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, token.Token, token.TokenSecret)
                .Messages().All().OlderThan(YAMMER_PREVIOUS_MESSAGE_ID)
                .AsJson();

            var response = yammer.Request();
            var messages = response.AsMessages();
            Assert.IsNotNull(response);
            Assert.IsNotNull(messages);
            Assert.IsNull(response.Exception);
            Assert.IsFalse(response.IsYammerError);
        }

        [Test]
        [Category("YammerMessages")]
        public void Can_get_all_messages_newer_than_some_id_as_xml()
        {
            var token = LoadToken("access");

            var yammer = FluentYammer.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, token.Token, token.TokenSecret)
                .Messages().All().OlderThan(YAMMER_PREVIOUS_MESSAGE_ID)
                .AsXml();

            var response = yammer.Request();
            var messages = response.AsMessages();
            Assert.IsNotNull(response);
            Assert.IsNull(response.Exception);
            Assert.IsFalse(response.IsYammerError);
            Assert.IsNotNull(messages);
            Assert.IsTrue(messages.Any());
        }

        [Test]
        [Category("YammerMessages")]
        public void Can_get_all_messages_older_than_some_id_as_json()
        {
            var token = LoadToken("access");

            var yammer = FluentYammer.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, token.Token, token.TokenSecret)
                .Messages().All().OlderThan(YAMMER_PREVIOUS_MESSAGE_ID)
                .AsJson();

            var response = yammer.Request();
            var messages = response.AsMessages();
            Assert.IsNotNull(response);
            Assert.IsNotNull(messages);
            Assert.IsNull(response.Exception);
            Assert.IsFalse(response.IsYammerError);
        }

        [Test]
        [Category("YammerMessages")]
        public void Can_get_all_messages_older_than_some_id_as_xml()
        {
            var token = LoadToken("access");

            var yammer = FluentYammer.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, token.Token, token.TokenSecret)
                .Messages().All().OlderThan(YAMMER_PREVIOUS_MESSAGE_ID)
                .AsXml();

            var response = yammer.Request();
            var messages = response.AsMessages();
            Assert.IsNotNull(response);
            Assert.IsNotNull(messages);
            Assert.IsNull(response.Exception);
            Assert.IsFalse(response.IsYammerError);
            Assert.IsNotNull(messages);
            Assert.IsTrue(messages.Any());
        }

        [Test]
        [Category("YammerMessages")]
        public void Can_get_all_messages_threaded_as_json()
        {
            var token = LoadToken("access");

            var yammer = FluentYammer.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, token.Token, token.TokenSecret)
                .Messages().All().Threaded()
                .AsJson();

            var response = yammer.Request();
            Console.WriteLine(yammer);
            var messages = response.AsMessages();
            Assert.IsNotNull(response);
            Assert.IsNotNull(messages);
            Assert.IsNull(response.Exception);
            Assert.IsFalse(response.IsYammerError);
        }

        [Test]
        [Category("YammerMessages")]
        public void Can_get_all_messages_threaded_as_xml()
        {
            var token = LoadToken("access");

            var yammer = FluentYammer.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, token.Token, token.TokenSecret)
                .Messages().All().Threaded()
                .AsXml();

            var response = yammer.Request();
            Console.WriteLine(yammer);
            var messages = response.AsMessages();
            Assert.IsNotNull(response);
            Assert.IsNotNull(messages);
            Assert.IsNull(response.Exception);
            Assert.IsFalse(response.IsYammerError);
        }

        [Test]
        [Category("YammerMessages")]
        public void Can_get_current_users_favorites()
        {
            var token = LoadToken("access");
            var yammer = FluentYammer.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, token.Token, token.TokenSecret)
                .Messages()
                .Favorites()
                .AsJson();

            Console.WriteLine(yammer);
            var response = yammer.Request();
            Assert.IsNotNull(response);
            Assert.IsNull(response.Exception);
            Assert.IsFalse(response.IsYammerError);
            var messages = response.AsMessages();
            Assert.IsNotNull(messages);
            messages.ToList().ForEach(VerifyMessage);
        }

        [Test]
        [Category("YammerMessages")]
        public void Can_get_messages_from_user_as_json()
        {
            var token = LoadToken("access");

            var yammer = FluentYammer.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, token.Token, token.TokenSecret)
                .Messages().FromUser(YAMMER_USER_ID)
                .AsJson();

            var response = yammer.Request();
            Console.WriteLine(yammer);
            var messages = response.AsMessages();
            Assert.IsNotNull(response);
            Assert.IsNotNull(messages);
            Assert.IsNull(response.Exception);
            Assert.IsFalse(response.IsYammerError);
        }

        [Test]
        [Category("YammerMessages")]
        public void Can_get_messages_from_user_as_xml()
        {
            var token = LoadToken("access");

            var yammer = FluentYammer.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, token.Token, token.TokenSecret)
                .Messages().FromUser(YAMMER_USER_ID)
                .AsXml();

            var response = yammer.Request();
            Console.WriteLine(yammer);
            var messages = response.AsMessages();
            Assert.IsNotNull(response);
            Assert.IsNotNull(messages);
            Assert.IsNull(response.Exception);
            Assert.IsFalse(response.IsYammerError);
        }

        [Test]
        [Category("YammerMessages")]
        public void Can_get_messages_with_tag_as_json()
        {
            var token = LoadToken("access");

            var yammer = FluentYammer.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, token.Token, token.TokenSecret)
                .Messages().WithTag(YAMMER_TAG_ID)
                .AsJson();

            var response = yammer.Request();
            Console.WriteLine(yammer);
            var messages = response.AsMessages();
            Assert.IsNotNull(response);
            Assert.IsNotNull(messages);
            Assert.IsNull(response.Exception);
            Assert.IsFalse(response.IsYammerError);
        }

        [Test]
        [Category("YammerMessages")]
        public void Can_get_messages_with_tag_as_xml()
        {
            var token = LoadToken("access");

            var yammer = FluentYammer.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, token.Token, token.TokenSecret)
                .Messages().WithTag(YAMMER_TAG_ID)
                .AsXml();

            var response = yammer.Request();
            Console.WriteLine(yammer);
            var messages = response.AsMessages();
            Assert.IsNotNull(response);
            Assert.IsNotNull(messages);
            Assert.IsNull(response.Exception);
            Assert.IsFalse(response.IsYammerError);
        }

        [Test]
        [Category("YammerMessages")]
        public void Can_get_metadata_from_messages_request_as_json()
        {
            var token = LoadToken("access");

            var yammer = FluentYammer.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, token.Token, token.TokenSecret)
                .Messages()
                .All()
                .AsJson();

            var response = yammer.Request();
            Assert.IsNotNull(response);
            Assert.IsNull(response.Exception);
            Assert.IsFalse(response.IsYammerError);

            var meta = response.AsResponseMetadata();
            Assert.IsNotNull(meta);
            Assert.Greater(meta.RequestedPollInterval, 0);
        }

        [Test]
        [Category("YammerMessages")]
        public void Can_get_metadata_from_messages_request_as_xml()
        {
            var token = LoadToken("access");

            var yammer = FluentYammer.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, token.Token, token.TokenSecret)
                .Messages()
                .All()
                .AsXml();

            var response = yammer.Request();
            Assert.IsNotNull(response);
            Assert.IsNull(response.Exception);
            Assert.IsFalse(response.IsYammerError);

            VerifyMetadata(response);
        }

        [Test]
        [Category("YammerMessages")]
        [Ignore("Always returns http 500")]
        public void Can_get_other_users_favorites()
        {
#pragma warning disable 0618 //test case is still valid, problem exists on yammer's end
            var token = LoadToken("access");
            var yammer = FluentYammer.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, token.Token, token.TokenSecret)
                .Messages()
                .FavoritesOf(YAMMER_USER_ID)
                .AsJson();
#pragma warning restore 0618

            Console.WriteLine(yammer);
            var response = yammer.Request();
            Assert.IsNotNull(response);
            Assert.IsNull(response.Exception);
            Assert.IsFalse(response.IsYammerError);
            var messages = response.AsMessages();
            Assert.IsNotNull(messages);
            messages.ToList().ForEach(VerifyMessage);
        }

        [Test]
        [Category("YammerMessages")]
        public void Can_post_message_and_favorite_it()
        {
            PostMessageAndFavoriteIt();
        }

        [Test]
        [Category("YammerMessages")]
        public void Can_post_message_and_unfavorite_it()
        {
            var msgId = PostMessageAndFavoriteIt();
            var token = LoadToken("access");
            var yammer = FluentYammer.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, token.Token, token.TokenSecret)
                .Messages()
                .Favorites().Destroy(msgId)
                .AsJson();


            var response = yammer.Request();
            Assert.IsNotNull(response);
            Assert.IsNull(response.Exception);
            Assert.IsFalse(response.IsYammerError);
        }

        [Test]
        [Category("YammerMessages")]
        public void Can_post_message_with_attachment()
        {
            var token = LoadToken("access");
            var yammer = FluentYammer.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, token.Token, token.TokenSecret)
                .Messages()
                .Post(string.Format("Posting with attachment from unit tests at {0} on {1}",
                                    DateTime.Now.ToShortTimeString(), DateTime.Now.ToLongDateString()))
                .WithAttachment("failwhale.jpg")
                .AsJson();

            Console.WriteLine(yammer);
            var response = yammer.Request();
            Assert.IsNotNull(response);
            Assert.IsNull(response.Exception);
            Assert.IsFalse(response.IsYammerError);
            var message = response.AsMessage();
            Assert.IsNotNull(message);
            VerifyMessage(message);
        }

        [Test]
        [Category("YammerMessages")]
        public void Can_post_message_with_multiple_attachments()
        {
            var token = LoadToken("access");
            var yammer = FluentYammer.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, token.Token, token.TokenSecret)
                .Messages()
                .Post(string.Format("Posting with a few attachments from unit tests at {0} on {1}",
                                    DateTime.Now.ToShortTimeString(), DateTime.Now.ToLongDateString()))
                .WithAttachment("failwhale.jpg")
                .WithAttachment("background.png")
                .WithAttachment("setup.xml")
                .AsJson();

            Console.WriteLine(yammer);
            var response = yammer.Request();
            Assert.IsNotNull(response);
            Assert.IsNull(response.Exception);
            Assert.IsFalse(response.IsYammerError);
            var message = response.AsMessage();
            Assert.IsNotNull(message);
            VerifyMessage(message);
        }

        [Test]
        [Category("YammerMessages")]
        public void Can_post_new_message_and_return_json()
        {
            var token = LoadToken("access");
            var yammer = FluentYammer.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, token.Token, token.TokenSecret)
                .Messages()
                .Post(string.Format("Posting to Yammer from #TweetSharp unit tests at {0} on {1}",
                                    DateTime.Now.ToShortTimeString(), DateTime.Now.ToLongDateString()))
                .AsJson();

            Console.WriteLine(yammer);
            var response = yammer.Request();
            Assert.IsNotNull(response);
            Assert.IsNull(response.Exception);
            Assert.IsFalse(response.IsYammerError);
            var message = response.AsMessage();
            Assert.IsNotNull(message);
            VerifyMessage(message);
        }

        [Test]
        [Category("YammerMessages")]
        public void Can_post_new_message_and_return_xml()
        {
            var token = LoadToken("access");
            var yammer = FluentYammer.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, token.Token, token.TokenSecret)
                .Messages()
                .Post(string.Format("Posting to Yammer from #TweetSharp unit tests at {0} on {1}",
                                    DateTime.Now.ToShortTimeString(), DateTime.Now.ToLongDateString()))
                .AsXml();

            Console.WriteLine(yammer);
            var response = yammer.Request();
            Assert.IsNotNull(response);
            Assert.IsNull(response.Exception);
            Assert.IsFalse(response.IsYammerError);
            var message = response.AsMessage();
            Assert.IsNotNull(message);
            VerifyMessage(message);
        }

        [Test]
        [Category("YammerMessages")]
        public void Can_post_new_message_to_group()
        {
            var token = LoadToken("access");
            var yammer = FluentYammer.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, token.Token, token.TokenSecret)
                .Messages()
                .Post(string.Format("Posting to Group from #TweetSharp unit tests at {0} on {1}",
                                    DateTime.Now.ToShortTimeString(), DateTime.Now.ToLongDateString()))
                .ToGroup(YAMMER_GROUP_ID)
                .AsXml();

            Console.WriteLine(yammer);
            var response = yammer.Request();
            Assert.IsNotNull(response);
            Assert.IsNull(response.Exception);
            Assert.IsFalse(response.IsYammerError);
            var message = response.AsMessage();
            Assert.AreEqual(YAMMER_GROUP_ID, message.GroupId);
            Assert.IsNotNull(message);
            VerifyMessage(message);
        }

        [Test]
        [Category("YammerMessages")]
        public void Can_reply_to_message()
        {
            var token = LoadToken("access");
            var yammer = FluentYammer.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, token.Token, token.TokenSecret)
                .Messages()
                .Post(string.Format("Posting a message that will be replied to.  At {0} on {1}",
                                    DateTime.Now.ToShortTimeString(), DateTime.Now.ToLongDateString()))
                .AsJson();


            var response = yammer.Request();
            var message = response.AsMessage();
            Assert.IsNotNull(message);

            var yammerReply = FluentYammer.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, token.Token, token.TokenSecret)
                .Messages()
                .Post(string.Format("Now I'm talking to myself.  At {0} on {1}",
                                    DateTime.Now.ToShortTimeString(), DateTime.Now.ToLongDateString()))
                .InReplyTo(message.Id)
                .AsJson();

            var replyResponse = yammerReply.Request();
            var reply = replyResponse.AsMessage();
            Assert.IsNotNull(reply);
            Assert.AreEqual(message.ThreadId, reply.ThreadId);
        }

        [Test]
        [Category("YammerMessages")]
        public void Can_send_direct_message()
        {
            var token = LoadToken("access");
            var yammer = FluentYammer.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, token.Token, token.TokenSecret)
                .Messages()
                .Post(string.Format("Posting a message that is direct to someone.  At {0} on {1}",
                                    DateTime.Now.ToShortTimeString(), DateTime.Now.ToLongDateString()))
                .DirectToUser(YAMMER_USER_ID)
                .AsJson();


            var response = yammer.Request();
            var message = response.AsMessage();
            Assert.IsNotNull(message);

            VerifyMessage(message);
        }
    }
}