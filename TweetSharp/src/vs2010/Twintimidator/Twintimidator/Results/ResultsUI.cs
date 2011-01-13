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
using System.Text;
using System.Windows.Forms;
using Ninject;

namespace Twintimidator.Results
{
    public partial class ResultsUI : UserControl
    {
        private IResultsController _resultsController;

        public ResultsUI()
        {
            InitializeComponent();
        }

        public IResultsController Controller
        {
            get { return _resultsController; }
        }

        public event EventHandler Done;

        protected override void OnLoad(EventArgs e1)
        {
            base.OnLoad(e1);
            if (!DesignMode)
            {
                _resultsController = Program.Kernel.Get<IResultsController>();
                if (_resultsController != null)
                {
                    progressBar.Visible = _resultsController.ExpectedNumberOfResults > 0;
                    progressBar.Style = ProgressBarStyle.Blocks;
                    progressBar.Maximum = _resultsController.ExpectedNumberOfResults > 0
                                              ? _resultsController.ExpectedNumberOfResults
                                              : int.MaxValue;
                    _resultsController.ResultsUpdated += (s, e) => Invoke(new Action(UpdateUI));
                    UpdateUI();
                }
            }
        }

        private void UpdateUI()
        {
            if (_resultsController != null)
            {
                progressBar.Visible = _resultsController.ExpectedNumberOfResults > 0;
                progressBar.Maximum = _resultsController.ExpectedNumberOfResults > 0
                                          ? _resultsController.ExpectedNumberOfResults
                                          : int.MaxValue;
                progressBar.Value = _resultsController.NumberOfResultsRecieved;
                PassedCountLabel.Text = _resultsController.Successes.ToString();
                FailCountLabel.Text = _resultsController.Failures.ToString();
                var sb = new StringBuilder();
                _resultsController.Errors.ForEach(err => sb.AppendLine(err));
                ErrorsText.Text = sb.ToString();
                ErrorsText.ScrollToCaret();
                if (_resultsController.NumberOfResultsRecieved >= _resultsController.ExpectedNumberOfResults)
                {
                    OnDone(EventArgs.Empty);
                }
            }
        }

        protected virtual void OnDone(EventArgs e)
        {
            if (Done != null)
            {
                Done(this, e);
            }
        }
    }
}