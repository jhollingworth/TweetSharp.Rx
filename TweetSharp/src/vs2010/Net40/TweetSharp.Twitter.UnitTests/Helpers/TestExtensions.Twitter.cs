using TweetSharp.Twitter.Model;

namespace TweetSharp.Twitter.UnitTests.Helpers
{
    internal static class TestExtensions
    {
        public static TwitterResult ToResult(this string response)
        {
            return new TwitterResult { Response = response };
        }
    }
}
