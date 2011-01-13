using System.Net;

#if SL3 || SL4
using System.Net.Browser;
#endif

namespace TweetSharp.Core.Configuration
{
    internal static class Bootstrapper
    {
        public static void Run()
        {
#if !SILVERLIGHT
            // http://groups.google.com/group/twitter-development-talk/browse_thread/thread/7c67ff1a2407dee7
            ServicePointManager.Expect100Continue = false;
#endif
#if !SILVERLIGHT && !Smartphone
            ServicePointManager.UseNagleAlgorithm = false;
#endif

#if SL3 || SL4
            // [DC]: Opt-in to the networking stack so we can get headers for proxies
            WebRequest.RegisterPrefix("http://", WebRequestCreator.ClientHttp);
            WebRequest.RegisterPrefix("https://", WebRequestCreator.ClientHttp);
#endif
        }
    }
}