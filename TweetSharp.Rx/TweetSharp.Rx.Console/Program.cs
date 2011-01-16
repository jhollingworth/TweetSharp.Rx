using System;
using System.Linq;
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

            var statusSubscriber = twitter
                .Where<TwitterStatus>(s => s.Text != "FOo")
                .Subscribe(status => System.Console.WriteLine(status.Text));

            var deleteSubscriber = twitter.Subscribe<Delete>(System.Console.WriteLine);
            
            System.Console.WriteLine("Press enter to quit");
            System.Console.ReadKey();

            statusSubscriber.Dispose();
            deleteSubscriber.Dispose();

            System.Console.WriteLine("Goodbye!");
        }
    }
}
