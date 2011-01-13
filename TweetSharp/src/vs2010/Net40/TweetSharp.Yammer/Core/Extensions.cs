using TweetSharp.Model;
using TweetSharp.Core.Extensions;
using TweetSharp.Yammer.Model;

namespace TweetSharp.Yammer.Core
{
    internal static class Extensions
    {
#if !Smartphone && !SILVERLIGHT
        public static OAuthToken AsToken(this YammerResult result)
        {
            return result.Response.AsToken();
        }
#endif
    }
}
