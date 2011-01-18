using System;
using TweetSharp.Rx.Entities;
using TweetSharp.Twitter.Fluent;
using TweetSharp.Twitter.Model;

namespace TweetSharp.Rx.Console
{
    class Program
    {
        static void Main()
        {
            var twitter = FluentTwitter.CreateRequest()
                .AuthenticateWith(Settings.ConsumerKey, Settings.ConsumerSecret, Settings.Token, Settings.TokenSecret)
                .Stream()
                .FromUser()
                .ToObservable();

            twitter.Subscribe<TwitterStatus>(s => System.Console.WriteLine("Status: " + s.Text));
            
            twitter.Subscribe<Friends>(s => System.Console.WriteLine("Friend Ids: " + s.FriendIds));

            twitter.Subscribe<DeleteTweet>(s => System.Console.WriteLine("Tweet Deleted: " + s.RawSource));
            
            System.Console.WriteLine("Press enter to quit");
            System.Console.ReadKey();
            System.Console.WriteLine("Goodbye!");
        }
    }
}
