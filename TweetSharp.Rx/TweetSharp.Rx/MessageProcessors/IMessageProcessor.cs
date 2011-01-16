using System;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;
using TweetSharp.Twitter.Model;

namespace TweetSharp.Rx.MessageProcessors
{
    internal interface IMessageProcessor<T>
        where T : ITwitterModel
    {
        Regex MessageRegex { get; }

        T Process(TwitterResult result);
    }
}