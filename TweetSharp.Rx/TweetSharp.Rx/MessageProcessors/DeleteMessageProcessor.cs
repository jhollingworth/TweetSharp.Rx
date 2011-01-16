using System.Text.RegularExpressions;
using TweetSharp.Rx.Entities;
using TweetSharp.Twitter.Model;

namespace TweetSharp.Rx.MessageProcessors
{
    internal class DeleteMessageProcessor : IMessageProcessor<Delete>
    {
        private readonly Regex _regex = new Regex("^{\\\"delete\\\":");

        public Regex MessageRegex
        {
            get { return _regex; }
        }

        public Delete Process(TwitterResult result)
        {
            return new Delete {RawSource = result.Response};
        }
    }
}