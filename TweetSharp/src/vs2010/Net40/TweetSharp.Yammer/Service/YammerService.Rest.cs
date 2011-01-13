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

using System.Collections.Generic;
using TweetSharp.Yammer.Fluent;
using TweetSharp.Yammer.Model;

namespace TweetSharp.Yammer.Service
{
    partial class YammerService
    {
        #region Messages (Viewing)

        #region All

        /// <summary>
        /// Gets a list of all messages. 
        /// </summary>
        /// <returns>An enumeration of messages</returns>
        public IEnumerable<YammerMessage> ListMessages()
        {
            return WithTweetSharp<IEnumerable<YammerMessage>>(q =>
                                                              q.Messages().All()
                );
        }

        /// <summary>
        /// Gets threaded list of messages posted after a specific message. 
        /// </summary>
        /// <param name="sinceId">The id of the low-water mark message.</param>
        /// <returns>An enumeration of messages</returns>
        public IEnumerable<YammerMessage> ListMessagesSince(long sinceId)
        {
            return WithTweetSharp<IEnumerable<YammerMessage>>(q =>
                                                              q.Messages().All().NewerThan(sinceId)
                );
        }
        
        /// <summary>
        /// Gets a list of messages posted before a specific message. 
        /// </summary>
        /// <param name="beforeId">The id of the low-water mark message.</param>
        /// <returns>An enumeration of messages</returns>
        public IEnumerable<YammerMessage> ListMessagesBefore(long beforeId)
        {
            return WithTweetSharp<IEnumerable<YammerMessage>>(q =>
                                                              q.Messages().All().OlderThan(beforeId)
                );
        }

        /// <summary>
        /// Gets a threaded list of messages.
        /// </summary>
        /// <returns>An enumeration of messages</returns>
        public IEnumerable<YammerMessage> ListThreadedMessages()
        {
            return WithTweetSharp<IEnumerable<YammerMessage>>(q =>
                                                              q.Messages().All().Threaded()
                );
        }

        /// <summary>
        /// Gets a threaded list of messages posted after a specific message. 
        /// </summary>
        /// <param name="sinceId">The id of the low-water mark message.</param>
        /// <returns>An enumeration of messages</returns>
        public IEnumerable<YammerMessage> ListThreadedMessagesSince(long sinceId)
        {
            return WithTweetSharp<IEnumerable<YammerMessage>>(q =>
                                                              q.Messages().All().NewerThan(sinceId).Threaded()
                );
        }

        /// <summary>
        /// Gets a threaded list of messages posted before a specific message. 
        /// </summary>
        /// <param name="beforeId">The id of the low-water mark message.</param>
        /// <returns>An enumeration of messages</returns>
        public IEnumerable<YammerMessage> ListThreadedMessagesBefore(long beforeId)
        {
            return WithTweetSharp<IEnumerable<YammerMessage>>(q =>
                                                              q.Messages().All().OlderThan(beforeId).Threaded()
                );
        }

        #endregion

        #region Sent
        /// <summary>
        /// Gets received messages.
        /// </summary>
        /// <returns>An enumeration of sent messages.</returns>
        public IEnumerable<YammerMessage> ListMessagesSent()
        {
            return WithTweetSharp<IEnumerable<YammerMessage>>(q =>
                                                              q.Messages().Sent()
                );
        }

        /// <summary>
        /// Gets sent messages posted after a specific message. 
        /// </summary>
        /// <param name="sinceId">The id of the low-water mark message.</param>
        /// <returns>An enumeration of sent messages</returns>
        public IEnumerable<YammerMessage> ListMessagesSentSince(int sinceId)
        {
            return WithTweetSharp<IEnumerable<YammerMessage>>(q =>
                                                              q.Messages().Sent().NewerThan(sinceId)
                );
        }

        /// <summary>
        /// Gets sent messages posted after a specific message. 
        /// </summary>
        /// <param name="beforeId">The id of the high-water mark message.</param>
        /// <returns>An enumeration of sent messages</returns>
        public IEnumerable<YammerMessage> ListMessagesSentBefore(int beforeId)
        {
            return WithTweetSharp<IEnumerable<YammerMessage>>(q =>
                                                              q.Messages().Sent().OlderThan(beforeId)
                );
        }

        /// <summary>
        /// Gets a threaded list of sent messages.
        /// </summary>
        /// <returns>An enumeration of sent messages</returns>
        public IEnumerable<YammerMessage> ListThreadedMessagesSent()
        {
            return WithTweetSharp<IEnumerable<YammerMessage>>(
                                                                 q => q.Messages().Sent().Threaded()
                );
        }

        /// <summary>
        /// Gets threaded list of sent messages posted after a specific message. 
        /// </summary>
        /// <param name="sinceId">The id of the low-water mark message.</param>
        /// <returns>An enumeration of sent messages</returns>
        public IEnumerable<YammerMessage> ListThreadedMessagesSentSince(int sinceId)
        {
            return WithTweetSharp<IEnumerable<YammerMessage>>(
                                                                 q => q.Messages().Sent().NewerThan(sinceId).Threaded()
                );
        }

        /// <summary>
        /// Gets threaded list of sent messages posted before a specific message. 
        /// </summary>
        /// <param name="beforeId">The id of the high-water mark message.</param>
        /// <returns>An enumeration of sent messages</returns>
        public IEnumerable<YammerMessage> ListThreadedMessagesSentBefore(int beforeId)
        {
            return WithTweetSharp<IEnumerable<YammerMessage>>(
                                                                 q => q.Messages().Sent().OlderThan(beforeId).Threaded()
                );
        }

        #endregion

        #region Received

        /// <summary>
        /// Gets received messages.
        /// </summary>
        /// <returns>An enumeration of received messages.</returns>
        public IEnumerable<YammerMessage> ListMessagesReceived()
        {
            return WithTweetSharp<IEnumerable<YammerMessage>>(q =>
                                                              q.Messages().Received()
                );
        }

        /// <summary>
        /// Gets received messages posted after a specific message. 
        /// </summary>
        /// <param name="sinceId">The id of the high-water mark message.</param>
        /// <returns>An enumeration of received messages</returns>
        public IEnumerable<YammerMessage> ListMessagesReceivedSince(int sinceId)
        {
            return WithTweetSharp<IEnumerable<YammerMessage>>(q =>
                                                              q.Messages().Received().NewerThan(sinceId)
                );
        }

        /// <summary>
        /// Gets received messages posted before a specific message. 
        /// </summary>
        /// <param name="beforeId">The id of the high-water mark message.</param>
        /// <returns>An enumeration of received messages</returns>
        public IEnumerable<YammerMessage> ListMessagesReceivedBefore(int beforeId)
        {
            return WithTweetSharp<IEnumerable<YammerMessage>>(q =>
                                                              q.Messages().Received().OlderThan(beforeId)
                );
        }

        /// <summary>
        /// Gets a threaded list of received messages.
        /// </summary>
        /// <returns>An enumeration of received messages.</returns>
        public IEnumerable<YammerMessage> ListThreadedMessagesReceived()
        {
            return WithTweetSharp<IEnumerable<YammerMessage>>(q =>
                                                              q.Messages().Received().Threaded()
                );
        }

        /// <summary>
        /// Gets a threaded list of messages posted after a specific message. 
        /// </summary>
        /// <param name="sinceId">The id of the low-water mark message.</param>
        /// <returns>An enumeration of messages</returns>
        public IEnumerable<YammerMessage> ListThreadedMessagesReceivedSince(int sinceId)
        {
            return WithTweetSharp<IEnumerable<YammerMessage>>(
                                                                 q =>
                                                                 q.Messages().Received().NewerThan(sinceId).Threaded()
                );
        }

        /// <summary>
        /// Gets a threaded list of messages posted before a specific message. 
        /// </summary>
        /// <param name="beforeId">The id of the high-water mark message.</param>
        /// <returns>An enumeration of received messages</returns>
        public IEnumerable<YammerMessage> ListThreadedMessagesReceivedBefore(int beforeId)
        {
            return WithTweetSharp<IEnumerable<YammerMessage>>(
                                                                 q =>
                                                                 q.Messages().Received().OlderThan(beforeId).Threaded()
                );
        }

        #endregion

        #endregion
    }
}