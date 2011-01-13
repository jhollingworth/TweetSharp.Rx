namespace Twintimidator
{
    partial class AccountsUI
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.GroupBox Accounts;
            this.TokenSecretLabel = new System.Windows.Forms.Label();
            this.TokenSecretTextBox = new System.Windows.Forms.TextBox();
            this.PasswordOrTokenLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.OAuthRadioButton = new System.Windows.Forms.RadioButton();
            this.basicAuthRadioButton = new System.Windows.Forms.RadioButton();
            this.UserNameTextBox = new System.Windows.Forms.TextBox();
            this.PasswordOrTokenTextBox = new System.Windows.Forms.TextBox();
            this.AddButton = new System.Windows.Forms.Button();
            this.DeleteButton = new System.Windows.Forms.Button();
            this.AccountsList = new System.Windows.Forms.ListBox();
            Accounts = new System.Windows.Forms.GroupBox();
            Accounts.SuspendLayout();
            this.SuspendLayout();
            // 
            // Accounts
            // 
            Accounts.Controls.Add(this.TokenSecretLabel);
            Accounts.Controls.Add(this.TokenSecretTextBox);
            Accounts.Controls.Add(this.PasswordOrTokenLabel);
            Accounts.Controls.Add(this.label1);
            Accounts.Controls.Add(this.OAuthRadioButton);
            Accounts.Controls.Add(this.basicAuthRadioButton);
            Accounts.Controls.Add(this.UserNameTextBox);
            Accounts.Controls.Add(this.PasswordOrTokenTextBox);
            Accounts.Controls.Add(this.AddButton);
            Accounts.Controls.Add(this.DeleteButton);
            Accounts.Controls.Add(this.AccountsList);
            Accounts.Dock = System.Windows.Forms.DockStyle.Fill;
            Accounts.Location = new System.Drawing.Point(0, 0);
            Accounts.MinimumSize = new System.Drawing.Size(378, 171);
            Accounts.Name = "Accounts";
            Accounts.Size = new System.Drawing.Size(378, 171);
            Accounts.TabIndex = 0;
            Accounts.TabStop = false;
            Accounts.Text = "Accounts";
            // 
            // TokenSecretLabel
            // 
            this.TokenSecretLabel.AutoSize = true;
            this.TokenSecretLabel.Location = new System.Drawing.Point(6, 128);
            this.TokenSecretLabel.Name = "TokenSecretLabel";
            this.TokenSecretLabel.Size = new System.Drawing.Size(41, 13);
            this.TokenSecretLabel.TabIndex = 8;
            this.TokenSecretLabel.Text = "Secret:";
            this.TokenSecretLabel.Visible = false;
            // 
            // TokenSecretTextBox
            // 
            this.TokenSecretTextBox.Location = new System.Drawing.Point(3, 145);
            this.TokenSecretTextBox.Name = "TokenSecretTextBox";
            this.TokenSecretTextBox.PasswordChar = '*';
            this.TokenSecretTextBox.Size = new System.Drawing.Size(100, 20);
            this.TokenSecretTextBox.TabIndex = 2;
            this.TokenSecretTextBox.Visible = false;
            // 
            // PasswordOrTokenLabel
            // 
            this.PasswordOrTokenLabel.AutoSize = true;
            this.PasswordOrTokenLabel.Location = new System.Drawing.Point(6, 87);
            this.PasswordOrTokenLabel.Name = "PasswordOrTokenLabel";
            this.PasswordOrTokenLabel.Size = new System.Drawing.Size(56, 13);
            this.PasswordOrTokenLabel.TabIndex = 4;
            this.PasswordOrTokenLabel.Text = "Password:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Username:";
            // 
            // OAuthRadioButton
            // 
            this.OAuthRadioButton.AutoSize = true;
            this.OAuthRadioButton.Location = new System.Drawing.Point(64, 19);
            this.OAuthRadioButton.Name = "OAuthRadioButton";
            this.OAuthRadioButton.Size = new System.Drawing.Size(55, 17);
            this.OAuthRadioButton.TabIndex = 1;
            this.OAuthRadioButton.Text = "OAuth";
            this.OAuthRadioButton.UseVisualStyleBackColor = true;
            this.OAuthRadioButton.CheckedChanged += new System.EventHandler(this.OAuthRadioButton_CheckedChanged);
            // 
            // basicAuthRadioButton
            // 
            this.basicAuthRadioButton.AutoSize = true;
            this.basicAuthRadioButton.Checked = true;
            this.basicAuthRadioButton.Location = new System.Drawing.Point(7, 19);
            this.basicAuthRadioButton.Name = "basicAuthRadioButton";
            this.basicAuthRadioButton.Size = new System.Drawing.Size(51, 17);
            this.basicAuthRadioButton.TabIndex = 0;
            this.basicAuthRadioButton.TabStop = true;
            this.basicAuthRadioButton.Text = "Basic";
            this.basicAuthRadioButton.UseVisualStyleBackColor = true;
            this.basicAuthRadioButton.CheckedChanged += new System.EventHandler(this.basicAuthRadioButton_CheckedChanged);
            // 
            // UserNameTextBox
            // 
            this.UserNameTextBox.Location = new System.Drawing.Point(3, 63);
            this.UserNameTextBox.Name = "UserNameTextBox";
            this.UserNameTextBox.Size = new System.Drawing.Size(100, 20);
            this.UserNameTextBox.TabIndex = 0;
            // 
            // PasswordOrTokenTextBox
            // 
            this.PasswordOrTokenTextBox.Location = new System.Drawing.Point(3, 104);
            this.PasswordOrTokenTextBox.Name = "PasswordOrTokenTextBox";
            this.PasswordOrTokenTextBox.PasswordChar = '*';
            this.PasswordOrTokenTextBox.Size = new System.Drawing.Size(100, 20);
            this.PasswordOrTokenTextBox.TabIndex = 1;
            // 
            // AddButton
            // 
            this.AddButton.Location = new System.Drawing.Point(112, 60);
            this.AddButton.Name = "AddButton";
            this.AddButton.Size = new System.Drawing.Size(55, 23);
            this.AddButton.TabIndex = 3;
            this.AddButton.Text = "Add";
            this.AddButton.UseVisualStyleBackColor = true;
            this.AddButton.Click += new System.EventHandler(this.AddButton_Click);
            // 
            // DeleteButton
            // 
            this.DeleteButton.Location = new System.Drawing.Point(112, 89);
            this.DeleteButton.Name = "DeleteButton";
            this.DeleteButton.Size = new System.Drawing.Size(55, 23);
            this.DeleteButton.TabIndex = 11;
            this.DeleteButton.Text = "Delete";
            this.DeleteButton.UseVisualStyleBackColor = true;
            this.DeleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
            // 
            // AccountsList
            // 
            this.AccountsList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.AccountsList.FormattingEnabled = true;
            this.AccountsList.Location = new System.Drawing.Point(173, 19);
            this.AccountsList.Name = "AccountsList";
            this.AccountsList.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.AccountsList.Size = new System.Drawing.Size(199, 147);
            this.AccountsList.TabIndex = 90;
            // 
            // AccountsUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(Accounts);
            this.Name = "AccountsUI";
            this.Size = new System.Drawing.Size(378, 171);
            Accounts.ResumeLayout(false);
            Accounts.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label TokenSecretLabel;
        private System.Windows.Forms.TextBox TokenSecretTextBox;
        private System.Windows.Forms.Label PasswordOrTokenLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton OAuthRadioButton;
        private System.Windows.Forms.RadioButton basicAuthRadioButton;
        private System.Windows.Forms.TextBox UserNameTextBox;
        private System.Windows.Forms.TextBox PasswordOrTokenTextBox;
        private System.Windows.Forms.Button AddButton;
        private System.Windows.Forms.Button DeleteButton;
        private System.Windows.Forms.ListBox AccountsList;
    }
}
