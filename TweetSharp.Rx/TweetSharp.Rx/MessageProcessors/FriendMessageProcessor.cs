using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;
using TweetSharp.Rx.Entities;
using TweetSharp.Twitter.Model;

namespace TweetSharp.Rx.MessageProcessors
{
    internal class FriendMessageProcessor : IMessageProcessor<Friends>
    {
        private readonly Regex _regex = new Regex("^{\\\"friends\\\":");

        public Regex MessageRegex
        {
            get { return _regex; }
        }

        public Friends Process(TwitterResult result)
        {
            var jsonObject = JObject.Parse(result.Response);

            var friends = jsonObject["friends"];

            return new Friends(string.Join(",", friends.Values<long>()), result.Response);
        }
    }
}