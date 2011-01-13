using System;
using Hammock.Retries;
using Hammock.Web;
using TweetSharp.Yammer.Extensions;
using TweetSharp.Yammer.Model;

namespace TweetSharp.Yammer.Core
{
#if !SILVERLIGHT
    [Serializable]
#endif
    internal class ServiceError : RetryResultCondition
    {
        public override Predicate<WebQueryResult> RetryIf
        {
            get
            {
                return r => !String.IsNullOrEmpty(r.Response) &&
                            new YammerResult(r, r.Exception).AsError() != null;
            }
        }
    }
}
