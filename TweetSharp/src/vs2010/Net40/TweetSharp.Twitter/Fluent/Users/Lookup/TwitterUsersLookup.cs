using System;
using Hammock.Web;

namespace TweetSharp.Twitter.Fluent
{
#if(!SILVERLIGHT)
    [Serializable]
#endif
    internal class TwitterUsersLookup : TwitterLeafNodeBase, ITwitterUsersLookup
    {
        public TwitterUsersLookup(IFluentTwitter root) : base(root)
        {
            Root.Method = WebMethod.Get;
        }
    }
}