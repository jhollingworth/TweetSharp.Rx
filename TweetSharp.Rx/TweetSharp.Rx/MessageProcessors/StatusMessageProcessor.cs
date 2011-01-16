using System.Text.RegularExpressions;
using TweetSharp.Twitter.Extensions;
using TweetSharp.Twitter.Model;

namespace TweetSharp.Rx.MessageProcessors
{
    internal class StatusMessageProcessor : IMessageProcessor<TwitterStatus>
    {
        private readonly Regex _regex = new Regex("^{\\\"user\\\":");

        public Regex MessageRegex
        {
            get { return _regex; }
        }

        public TwitterStatus Process(TwitterResult result)
        {
            return result.AsStatus();
        }
    }
}