using System;
using System.Collections.Generic;
using TweetSharp.Twitter.Fluent;
using TweetSharp.Twitter.Model;

namespace TweetSharp.Rx
{
    internal class UserMessageStreamObservable : IObservable<TwitterResult>
    {
        private readonly List<IObserver<TwitterResult>> _observers = new List<IObserver<TwitterResult>>();

        public UserMessageStreamObservable(ITwitterStreamingUser userStream)
        {
            userStream.Take(1).CallbackTo(MessageRecieved).BeginRequest();
        }

        private void MessageRecieved(object sender, TwitterResult result, object userstate)
        {
            result.Response = result.Response.Trim();

            lock(_observers)
            {
                foreach (var observer in _observers)
                {
                    observer.OnNext(result);
                }
            }
        }

        public IDisposable Subscribe(IObserver<TwitterResult> observer)
        {
            _observers.Add(observer);

            return new ActionDisposable(() => Remove(observer));
        }

        private void Remove(IObserver<TwitterResult> observer)
        {
            lock(_observers)
            {
                if(_observers.Contains(observer))
                {
                    _observers.Remove(observer);    
                }
            }
        }
    }
}