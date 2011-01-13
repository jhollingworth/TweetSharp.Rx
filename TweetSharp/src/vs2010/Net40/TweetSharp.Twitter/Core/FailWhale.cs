using System;
using System.Net;
using Hammock.Retries;
using Hammock.Web;

namespace TweetSharp.Twitter.Core
{
#if !SILVERLIGHT
    [Serializable]
#endif
    internal class FailWhale : RetryResultCondition
    {
        public override Predicate<WebQueryResult> RetryIf
        {
            get
            {
                return r => // text/html; charset=UTF-8
                    (r.Exception != null && r.Exception.Response is HttpWebResponse 
                    &&(((HttpWebResponse)r.Exception.Response).StatusCode == (HttpStatusCode)502 || ((HttpWebResponse)r.Exception.Response).StatusCode == (HttpStatusCode)503))
                    ||
                    (r.ResponseHttpStatusCode == 502 || r.ResponseHttpStatusCode == 503) &&
                    (!String.IsNullOrEmpty(r.ResponseType) && r.ResponseType.ToLower().Contains("text/html"));
            }
        }
    }
}
