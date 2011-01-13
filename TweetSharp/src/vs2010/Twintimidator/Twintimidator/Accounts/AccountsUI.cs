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
using Ninject;
using Twintimidator.Accounts;

namespace Twintimidator
{
    public partial class AccountsUI : UserControl
    {
        private IUserAccountController _accountController;
        private IAccountListSerializer _serializer;

        public AccountsUI()
        {
            InitializeComponent();
        }

        public IUserAccountController Controller
        {
            get { return _accountController; }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!DesignMode)
            {
                _accountController = Program.Kernel.Get<IUserAccountController>();
                _serializer = Program.Kernel.Get<IAccountListSerializer>();
                _accountController.UserAdded += account => AccountsList.Items.Add(account);
                _accountController.UserRemoved += account => AccountsList.Items.Remove(account);
                _accountController.UserAdded += account => _serializer.SerializeToDisk(_accountController);
                _accountController.UserRemoved += account => _serializer.SerializeToDisk(_accountController);
                _accountController.AddRange(_serializer.DeserializeFromDisk());
                AccountsList.Items.AddRange(_accountController.ToArray());
            }
        }


        private void basicAuthRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            PasswordOrTokenLabel.Text = "Password:";
            PasswordOrTokenTextBox.UseSystemPasswordChar = true;
            TokenSecretLabel.Visible = false;
            TokenSecretTextBox.Visible = false;
        }

        private void OAuthRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            PasswordOrTokenLabel.Text = "Token:";
            PasswordOrTokenTextBox.UseSystemPasswordChar = false;
            TokenSecretTextBox.Visible = true;
            TokenSecretLabel.Visible = true;
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            if (UserNameTextBox.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Username required", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (basicAuthRadioButton.Checked)
            {
                if (PasswordOrTokenTextBox.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("Password required", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                _accountController.CreateBasicAuthUser(UserNameTextBox.Text.Trim(),
                                                       PasswordOrTokenTextBox.Text.Trim());
            }
            else
            {
                if (PasswordOrTokenTextBox.Text.Trim() == string.Empty
                    || TokenSecretTextBox.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("Both the OAuth Token and Token Secret are required", "Error", MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                    return;
                }
                _accountController.CreateOAuthUser(UserNameTextBox.Text.Trim(),
                                                   PasswordOrTokenTextBox.Text.Trim(),
                                                   TokenSecretTextBox.Text.Trim());
            }
            UserNameTextBox.Text = string.Empty;
            PasswordOrTokenTextBox.Text = string.Empty;
            TokenSecretTextBox.Text = string.Empty;
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            AccountsList.SelectedItems
                .OfType<UserAccount>()
                .ToList()
                .ForEach(account => _accountController.RemoveUser(account));
        }
    }
}