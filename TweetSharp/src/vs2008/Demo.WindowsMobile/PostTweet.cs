using System;
using System.Drawing;
using System.Windows.Forms;
using TweetSharp.Extensions;
using TweetSharp.Fluent;
using TweetSharp.Model;
using TweetSharp.Twitter.Extensions;
using TweetSharp.Twitter.Fluent;
using TweetSharp.Twitter.Model;

namespace Demo.WindowsMobile
{
    public partial class PostTweet : Form
    {
        private readonly string _user;
        private readonly string _password; 

        public PostTweet(string user, string password)
        {
            _user = user;
            _password = password; 
            InitializeComponent();
            this.textBox1.TextChanged += (s, e)
                                         =>
                                             {
                                                 var len = this.textBox1.Text.Length;
                                                 var remain = 140 - len;
                                                 RemainingCharactersLabel.Text = remain.ToString(); 
                                                 RemainingCharactersLabel.ForeColor = remain < 0 ? Color.Red : Color.Black;
                                                 menuItem1.Enabled = remain >= 0; 
                                             };

        }

        private void menuItem1_Click(object sender, EventArgs e)
        {
            

        }

        private void PostTweetCallback(object s, TwitterResult r, object u)
        {
            Action act = () =>
                             {
                                 if (r.IsNetworkError || r.IsTwitterError || r.IsFailWhale)
                                 {
                                     MessageBox.Show(string.Format("Error - {0}", r.AsError()));
                                     return;
                                 }
                                 DialogResult = DialogResult.OK; 
                                 Close();
                             };
            
            Invoke(act);
        }

        private void menuItem2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel; 
            this.Close();
        }

        private void menuItem4_Click(object sender, EventArgs e)
        {
            var text = textBox1.Text;
            var twitter = FluentTwitter.CreateRequest()
                .AuthenticateAs(_user, _password)
                .Configuration.UseAutomaticRetries(RetryOn.FailWhaleOrNetwork, 5)
                .Statuses().Update(text);

            twitter.CallbackTo(PostTweetCallback);
            twitter.BeginRequest(); 
        }

        private void PostTweet_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.F1))
            {
                // Soft Key 1
                // Not handled when menu is present.
            }
            if ((e.KeyCode == Keys.F2))
            {
                // Soft Key 2
                // Not handled when menu is present.
            }
            if ((e.KeyCode == Keys.Up))
            {
                // Up
            }
            if ((e.KeyCode == Keys.Down))
            {
                // Down
            }
            if ((e.KeyCode == Keys.Left))
            {
                // Left
            }
            if ((e.KeyCode == Keys.Right))
            {
                // Right
            }
            if ((e.KeyCode == Keys.Enter))
            {
                // Enter
            }
            if ((e.KeyCode == Keys.D1))
            {
                // 1
            }
            if ((e.KeyCode == Keys.D2))
            {
                // 2
            }
            if ((e.KeyCode == Keys.D3))
            {
                // 3
            }
            if ((e.KeyCode == Keys.D4))
            {
                // 4
            }
            if ((e.KeyCode == Keys.D5))
            {
                // 5
            }
            if ((e.KeyCode == Keys.D6))
            {
                // 6
            }
            if ((e.KeyCode == Keys.D7))
            {
                // 7
            }
            if ((e.KeyCode == Keys.D8))
            {
                // 8
            }
            if ((e.KeyCode == Keys.D9))
            {
                // 9
            }
            if ((e.KeyCode == Keys.F8))
            {
                // *
            }
            if ((e.KeyCode == Keys.D0))
            {
                // 0
            }
            if ((e.KeyCode == Keys.F9))
            {
                // #
            }

        }
    }
}
