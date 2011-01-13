using System;
using System.Windows.Forms;
using TweetSharp.Extensions;
using TweetSharp.Fluent;
using TweetSharp.Twitter.Extensions;
using TweetSharp.Twitter.Fluent;

namespace Demo.WindowsMobile
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void menuItem1_Click(object sender, EventArgs e)
        {
            
            Cursor.Current = Cursors.WaitCursor; 
            var twitter = FluentTwitter.CreateRequest()
                .AuthenticateAs(UserNameTextBox.Text, PasswordTextBox.Text)
                .Account().VerifyCredentials();
            
            
            twitter.CallbackTo((s, r, u) =>
                                   {
                                       var user = r.AsUser();
                                       if (user == null)
                                       {
                                           Action del = () => ErrorLabel.Visible = true;
                                           ErrorLabel.Invoke(del); 
                                       }
                                       else
                                       {
                                           Action del = () =>
                                                            {
                                                                Cursor.Current = Cursors.Default;
                                                                var timelineForm = new TimeLine(UserNameTextBox.Text, PasswordTextBox.Text);
                                                                timelineForm.Show();
                                                                this.Hide(); 
                                                                
                                                            };
                                           this.Invoke(del);
                                       }
                                       this.Invoke(new Action(() => Cursor.Current = Cursors.Default ) );
                                       
                                   });

            twitter.BeginRequest();
        }
    }
}