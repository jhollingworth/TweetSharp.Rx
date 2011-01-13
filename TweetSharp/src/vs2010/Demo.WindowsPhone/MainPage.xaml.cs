using System.Windows;
using Microsoft.Phone.Controls;
using TweetSharp.Twitter.Extensions;
using TweetSharp.Twitter.Fluent;

namespace Demo.WindowsPhone
{
    public partial class MainPage
    {
        public MainPage()
        {
            InitializeComponent();

            SupportedOrientations = 
                SupportedPageOrientation.Portrait | 
                SupportedPageOrientation.Landscape;

            Loaded += MainPage_Loaded;
        }

        void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            var dispatcher = Deployment.Current.Dispatcher;

            var query = FluentTwitter.CreateRequest()
                .Statuses().OnPublicTimeline()
                .CallbackTo((s, r, u) =>
                                {
                                    var statuses = r.AsStatuses();
                                    foreach (var status in statuses)
                                    {
                                        var inline = status;
                                        dispatcher.BeginInvoke(
                                            () => listBox1.Items.Add(inline));
                                    }
                                }
                );

            query.BeginRequest();
        }
    }
}