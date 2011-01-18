using System;
using TweetSharp.Rx.Entities;
using TweetSharp.Twitter.Model;

namespace TweetSharp.Rx
{
    public interface IUserStreamObservable : IObservable<TwitterStatus>, IObservable<DeleteTweet>, IObservable<Friends>
    {   
    }
}