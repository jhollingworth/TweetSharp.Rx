using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TweetSharp.Extensions;
using TweetSharp.Fluent;
using TweetSharp.Model;
using Demo.WindowsMobile.Extensions;
using TweetSharp.Twitter.Extensions;
using TweetSharp.Twitter.Fluent;
using TweetSharp.Twitter.Model;

namespace Demo.WindowsMobile
{
    public partial class TimeLine : Form
    {
        private string _user;
        private string _password;
        private long? _lastStatus;
        private Timer _timer;

        public TimeLine(string user, string password)
        {
            InitializeComponent();
            _user = user;
            _password = password;

            
            _timer = new Timer
                         {
                             Interval = ((int) 15.Seconds().TotalMilliseconds)
                         };
            _timer.Tick += (s,e) => GetHomeTimeline();
            _timer.Enabled = false;

            GetHomeTimeline();
        }

        private void GetHomeTimeline()
        {
            _timer.Enabled = false; 
            Cursor.Current = Cursors.WaitCursor; 
            var twitter = FluentTwitter.CreateRequest()
                .AuthenticateAs(_user, _password)
                .Configuration.UseAutomaticRetries(RetryOn.FailWhaleOrNetwork, 5)
                .Statuses().OnHomeTimeline();

            if ( _lastStatus.HasValue)
            {
                twitter.Since(_lastStatus.Value); 
            }

            twitter.CallbackTo(GetHomeTimelineCallback); 

            twitter.BeginRequest(); 
        }

        private void GetHomeTimelineCallback(object s, TwitterResult r, object u)
        {
            Action act = () =>
                             {

                                 if (r.IsNetworkError || r.IsTwitterError || r.IsFailWhale)
                                 {
                                     MessageBox.Show(string.Format("Error - {0}", r.AsError()));
                                     return;
                                 }
                                 var statusesEnumerable = r.AsStatuses();
                                 
                                 if ( statusesEnumerable == null )
                                 {
                                     return; 
                                 }
                                 var statuses = statusesEnumerable.ToList(); 
                                 Comparison<TwitterStatus> comparison = (one,two) =>( int )Math.Round(( one.CreatedDate - two.CreatedDate ).TotalSeconds, 0 );
                                 statuses.Sort(comparison);
                                     
                                 _lastStatus = _lastStatus ?? 0;

                                 foreach (var status in statuses)
                                 {
                                     var box = new TweetBox(status, this.TimeLinePanel.ClientSize.Width) {Location = new Point(0, 0 + this.TimeLinePanel.AutoScrollPosition.Y)};
                                     box.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
                                     this.TimeLinePanel.Controls.ForEach(c => c.Top += box.Height );
                                     this.TimeLinePanel.Controls.Add(box);
                                     box.Visible = true; 
                                     _lastStatus = Math.Max(_lastStatus.Value, status.Id);
                                 }

                                 this.TimeLinePanel.Controls.OfType<TweetBox>().ForEach(tbox => tbox.UpdateSourceText());
                                 
                                 Cursor.Current = Cursors.Default;
                                 _timer.Enabled = true; 

                             };
            this.Invoke(act);
            
        }

        private void menuItem1_Click(object sender, EventArgs e)
        {
            var tweet = new PostTweet(_user,_password);
            tweet.ShowDialog(); 
        }
    }
}