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
using System.Windows.Forms;
using Ninject;
using Twintimidator.Actions;

namespace Twintimidator
{
    internal partial class ActionsUI : UserControl
    {
        private IActionsController _controller;

        public ActionsUI()
        {
            InitializeComponent();
            ActionsList.CheckOnClick = true;
            ActionsList.ItemCheck += ActionsList_ItemCheck;
        }

        public IActionsController Controller
        {
            get { return _controller; }
        }

        private void ActionsList_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            var action = (ITwitterAction) ActionsList.Items[e.Index];
            if (e.NewValue == CheckState.Checked)
            {
                _controller.AddAction(action);
            }
            else
            {
                _controller.RemoveAction(action);
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!DesignMode)
            {
                _controller = Program.Kernel.Get<IActionsController>();
                _controller.AllAvailableActions.ForEach(a => ActionsList.Items.Add(a));
            }
        }

        private void SelectAllButton_Click(object sender, EventArgs e)
        {
            for (var i = 0; i < ActionsList.Items.Count; i++)
            {
                ActionsList.SetItemChecked(i, true);
            }
        }
    }
}