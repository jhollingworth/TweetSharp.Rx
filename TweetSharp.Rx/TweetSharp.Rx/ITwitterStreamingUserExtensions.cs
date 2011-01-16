using System;
using TweetSharp.Twitter.Fluent;

namespace TweetSharp.Rx
{
    public static class TwitterStreamingUserExtensions
    {
        public static IUserStreamObservable ToObservable(this ITwitterStreamingUser userStream)
        {
            return new UserStreamObservable(userStream);
        }
    }
}