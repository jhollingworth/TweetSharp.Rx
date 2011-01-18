using System;
using TweetSharp.Model;
using TweetSharp.Twitter.Model;

namespace TweetSharp.Rx.Entities
{
    public class DeleteTweet : ITwitterModel
    {
        public string RawSource { get; set; }
    }
}