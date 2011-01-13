#region License

// TweetSharp
// Copyright (c) 2010 Daniel Crenna and Jason Diller
// 
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

#endregion

using System;
using System.Linq;
using System.Windows.Forms;
using Twintimidator.Dispatcher;
using Twintimidator.Properties;
using Twintimidator.Threads;

namespace Twintimidator
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            if (string.IsNullOrEmpty(Resources.ConsumerKey) || string.IsNullOrEmpty(Resources.ConsumerSecret))
            {
                MessageBox.Show(
                                   "To run tests with OAuth users, specify the ConsumerKey and ConsumerSecret in the resource file."
                                   , "No OAuth Tokens Found"
                                   , MessageBoxButtons.OK,
                                   MessageBoxIcon.Warning);
            }
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            var dispatcher = new TaskDispatcher(AccountsUI.Controller,
                                                ActionsUI.Controller,
                                                ThreadsUI.Controller,
                                                ResultsUI.Controller);

            if (ThreadsUI.Controller.ThreadType == ThreadType.RequestAsync)
            {
                var expectedResults = ThreadsUI.Controller.RepeatCount*ActionsUI.Controller.SelectedActions.Count();
                ResultsUI.Controller.ExpectedNumberOfResults = expectedResults;
            }
            else
            {
                ResultsUI.Controller.ExpectedNumberOfResults = (int) ThreadsUI.Controller.NumberOfThreadsToUse;
            }

            dispatcher.Start();
            StartButton.Enabled = false;
            //ResultsUI.Done += (s, e2) => StartButton.Enabled = true; 
        }

        private void AccountsUI_Load(object sender, EventArgs e)
        {

        }
    }
}