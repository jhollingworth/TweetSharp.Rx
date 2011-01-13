namespace Demo.WindowsMobile
{
    partial class PostTweet
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu postMenu;

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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.postMenu = new System.Windows.Forms.MainMenu();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.menuItem4 = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.RemainingCharactersLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // postMenu
            // 
            this.postMenu.MenuItems.Add(this.menuItem1);
            this.postMenu.MenuItems.Add(this.menuItem2);
            // 
            // menuItem1
            // 
            this.menuItem1.MenuItems.Add(this.menuItem3);
            this.menuItem1.MenuItems.Add(this.menuItem4);
            this.menuItem1.Text = "Post";
            this.menuItem1.Click += new System.EventHandler(this.menuItem1_Click);
            // 
            // menuItem4
            // 
            this.menuItem4.Text = "Post Now";
            this.menuItem4.Click += new System.EventHandler(this.menuItem4_Click);
            // 
            // menuItem2
            // 
            this.menuItem2.Text = "Cancel";
            this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
            // 
            // textBox1
            // 
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox1.Location = new System.Drawing.Point(0, 0);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(176, 158);
            this.textBox1.TabIndex = 0;
            // 
            // RemainingCharactersLabel
            // 
            this.RemainingCharactersLabel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.RemainingCharactersLabel.Location = new System.Drawing.Point(0, 158);
            this.RemainingCharactersLabel.Name = "RemainingCharactersLabel";
            this.RemainingCharactersLabel.Size = new System.Drawing.Size(176, 22);
            this.RemainingCharactersLabel.Text = "140";
            // 
            // PostTweet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(176, 180);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.RemainingCharactersLabel);
            this.KeyPreview = true;
            this.Menu = this.postMenu;
            this.Name = "PostTweet";
            this.Text = "PostTweet";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PostTweet_KeyDown);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label RemainingCharactersLabel;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.MenuItem menuItem3;
        private System.Windows.Forms.MenuItem menuItem4;
    }
}