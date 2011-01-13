using System.Diagnostics;
using System.Drawing;
using System.Net;
using System.Windows.Forms;
using TweetSharp.Extensions;
using Demo.WindowsMobile.Extensions;
using TweetSharp.Twitter.Model;

namespace Demo.WindowsMobile
{
    public partial class TweetBox : UserControl
    {
        private readonly TwitterStatus _status; 
        public TweetBox(TwitterStatus status, int maxWidth )
        {
            InitializeComponent();
            BorderStyle = BorderStyle.FixedSingle;
            Width = maxWidth;
            _status = status; 
            var stream = new WebClient().OpenRead(status.User.ProfileImageUrl);
            var image = new Bitmap(stream);
            stream.Dispose();
            AvatarBox.Image = image;
            TweetText.Text = status.Text;
            using (var g = CreateGraphics())
            {
                var size = g.MeasureString(status.Text, TweetText.Bounds);
                var diff = TweetText.Height - size.Height;
                TweetText.Height -= diff; 
                Height -= diff;
            }
            UpdateSourceText();
            UserLabel.Text = status.User.ScreenName;
        }

        public void UpdateSourceText()
        {
            if (!string.IsNullOrEmpty(_status.Source))
            {
                var parser = new SourceStringParser(_status.Source);
                SourceLabel.Text = _status.CreatedDate.ToLocalTime().ToRelativeTime() + " from " + parser.Text;
                if (!string.IsNullOrEmpty(parser.URL))
                {
                    SourceLabel.Click += (s, e) => Process.Start(parser.URL, "");
                }
            }
            else
            {
                SourceLabel.Text = _status.CreatedDate.ToLocalTime().ToRelativeTime() + " from ?????";
            }
        }
    }
}
