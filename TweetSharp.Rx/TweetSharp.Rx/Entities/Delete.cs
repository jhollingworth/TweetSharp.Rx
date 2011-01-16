using System;
using TweetSharp.Model;
using TweetSharp.Twitter.Model;

namespace TweetSharp.Rx.Entities
{
    public class Delete : ITwitterModel
    {
        public string RawSource { get; set; }
    }
}