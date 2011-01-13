using System;
using Hammock.Web;
using TweetSharp.Twitter.Extensions;
using TweetSharp.Twitter.Model;

namespace TweetSharp.Twitter.Core
{
#if !SILVERLIGHT
    [Serializable]
#endif
    internal class ServiceError : FailWhale
    {
        public override Predicate<WebQueryResult> RetryIf
        {
            get
            {
                return r =>
                       r.Exception != null || BaseRetryIf(r) || r.ResponseHttpStatusCode >= 400 ||
                       (!String.IsNullOrEmpty(r.Response) &&
                        new TwitterResult(r, r.Exception).AsError() != null
                       );
            }
        }

        private bool BaseRetryIf(WebQueryResult r)
        {
            return base.RetryIf(r);
        }
    }
}