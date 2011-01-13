using System;
using System.Windows;
using TweetSharp.Twitter.Fluent;
using TweetSharp.Twitter.Extensions;

namespace Demo.Silverlight
{
    public partial class MainPage
    {
        private string TwitterUserName = "username";
        private string TwitterPassword = "password";

        public MainPage()
        {
            InitializeComponent();
            Application.Current.InstallStateChanged += Current_InstallStateChanged;
        }

        static void Current_InstallStateChanged(object sender, EventArgs e)
        {

        }

        private void GetTweetsClick(object sender, RoutedEventArgs e)
        {
            // You have to be running out of browser to have 
            // elevated permissions to call cross-domain
            if (Application.Current.IsRunningOutOfBrowser)
            {
                GetTweetsImpl();
            }
        }

        private void GetTweetsImpl()
        {
            var twitter = FluentTwitter.CreateRequest()
                .AuthenticateAs(TwitterUserName, TwitterPassword)
                .Statuses().OnHomeTimeline()
                .CallbackTo((s, r, u) =>
                {
                    var statuses = r.AsStatuses();
                    foreach(var status in statuses)
                    {
                        var tweet = status; // closure
                        Dispatcher.BeginInvoke(() => TweetList.Items.Add(tweet.Text));                    
                    }
                });

            twitter.BeginRequest();
        }        
    }
}
