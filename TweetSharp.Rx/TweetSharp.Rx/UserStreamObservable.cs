using System;
using System.Linq;
using TweetSharp.Rx.Entities;
using TweetSharp.Rx.MessageProcessors;
using TweetSharp.Twitter.Fluent;
using TweetSharp.Twitter.Model;

namespace TweetSharp.Rx
{
    public class UserStreamObservable : IUserStreamObservable
    {
        private readonly IObservable<TwitterResult> _messageStream;
        private readonly IObservable<TwitterStatus> _statusObservable;
        private readonly IObservable<DeleteTweet> _deleteObservable;
        private readonly IObservable<Friends> _friendsObservable;


        public UserStreamObservable(ITwitterStreamingUser userStream)
        {
            _messageStream = new UserMessageStreamObservable(userStream);

            var statusProcessor = new StatusMessageProcessor();
            var deleteProcessor = new DeleteMessageProcessor();
            var friendProcessor = new FriendMessageProcessor();

            _statusObservable = _messageStream
                .Where(s => statusProcessor.MessageRegex.IsMatch(s.Response))
                .Select(statusProcessor.Process);

            _deleteObservable = _messageStream
                .Where(s => deleteProcessor.MessageRegex.IsMatch(s.Response))
                .Select(deleteProcessor.Process);

            _friendsObservable = _messageStream
                .Where(s => friendProcessor.MessageRegex.IsMatch(s.Response))
                .Select(friendProcessor.Process);
        }

        public IDisposable Subscribe(IObserver<TwitterStatus> observer)
        {
            return _statusObservable.Subscribe(observer);
        }

        public IDisposable Subscribe(IObserver<DeleteTweet> observer)
        {
            return _deleteObservable.Subscribe(observer);
        }

        public IDisposable Subscribe(IObserver<Friends> observer)
        {
            return _friendsObservable.Subscribe(observer);
        }
    }
}