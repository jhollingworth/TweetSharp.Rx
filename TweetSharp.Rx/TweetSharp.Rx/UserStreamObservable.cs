using System;
using System.Collections.Generic;
using System.Linq;
using TweetSharp.Rx.Entities;
using TweetSharp.Rx.MessageProcessors;
using TweetSharp.Twitter.Fluent;
using TweetSharp.Twitter.Model;

namespace TweetSharp.Rx
{
    public class UserStreamObservable : IUserStreamObservable
    {
        private readonly List<IMessageProcessor<ITwitterModel>> _processors = new List<IMessageProcessor<ITwitterModel>>();

        private readonly Dictionary<Type, List<IObservable<ITwitterModel>>> _observers = new Dictionary<Type, List<IObservable<ITwitterModel>>>();

        public UserStreamObservable(ITwitterStreamingUser userStream)
        {
            userStream.Take(1).CallbackTo(MessageRecieved).BeginRequest();

            _processors.Add((IMessageProcessor<ITwitterModel>) new FriendMessageProcessor());
            _processors.Add((IMessageProcessor<ITwitterModel>) new StatusMessageProcessor());
            _processors.Add((IMessageProcessor<ITwitterModel>) new DeleteMessageProcessor());

            foreach (var processor in _processors)
            {
                _observers.Add(processor.GetType(), new List<IObservable<ITwitterModel>>());
            }
        }

        private void MessageRecieved(object sender, TwitterResult result, object userstate)
        {
            var response = result.Response.Trim();
            foreach (var processor in _processors)
            {
                if (processor.MessageRegex.IsMatch(response))
                {
                    processor.Process(result);
                    break;
                }
            }

            //var observers = _observers.ToArray();

            //foreach (var observer in observers)
            //{
            //    observer.OnNext(result.Response);
            //}
        }

        public IDisposable Subscribe(IObserver<TwitterStatus> observer)
        {
            //_statusObservers.Add(observer);

            //return new ActionDisposable(() => _statusObservers.Remove(observer));

            return null;
        }

        private IDisposable AddObserver<T>(IObserver<T> observer)
        {
            return null;
        }

        public IDisposable Subscribe(IObserver<Delete> observer)
        {
            //_deleteObservers.Add(observer);

            //return new ActionDisposable(() => _deleteObservers.Remove(observer));
            return null;

        }
    }
}