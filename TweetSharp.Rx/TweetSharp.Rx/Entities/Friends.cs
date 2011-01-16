using System;
using TweetSharp.Twitter.Model;

namespace TweetSharp.Rx.Entities
{
    internal class Friends : ITwitterModel
    {
        public string FriendIds { get; set; }

        public Friends(string friendIds, string source)
        {
            FriendIds = friendIds;
        }

        public string RawSource { get; set; }
    }
}