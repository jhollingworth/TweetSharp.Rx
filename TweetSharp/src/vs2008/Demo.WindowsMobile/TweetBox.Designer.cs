namespace Demo.WindowsMobile
{
    partial class TweetBox
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
            this.AvatarBox = new System.Windows.Forms.PictureBox();
            this.SourceLabel = new System.Windows.Forms.LinkLabel();
            this.UserLabel = new System.Windows.Forms.Label();
            this.TweetText = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // AvatarBox
            // 
            this.AvatarBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.AvatarBox.Location = new System.Drawing.Point(0, 0);
            this.AvatarBox.Name = "AvatarBox";
            this.AvatarBox.Size = new System.Drawing.Size(47, 82);
            // 
            // SourceLabel
            // 
            this.SourceLabel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.SourceLabel.Font = new System.Drawing.Font("Segoe Condensed", 8F, System.Drawing.FontStyle.Regular);
            this.SourceLabel.ForeColor = System.Drawing.Color.LightSlateGray;
            this.SourceLabel.Location = new System.Drawing.Point(47, 66);
            this.SourceLabel.Name = "SourceLabel";
            this.SourceLabel.Size = new System.Drawing.Size(196, 16);
            this.SourceLabel.TabIndex = 1;
            this.SourceLabel.Text = "linkLabel1";
            // 
            // UserLabel
            // 
            this.UserLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.UserLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.UserLabel.Location = new System.Drawing.Point(47, 0);
            this.UserLabel.Name = "UserLabel";
            this.UserLabel.Size = new System.Drawing.Size(196, 17);
            this.UserLabel.Text = "label1";
            // 
            // TweetText
            // 
            this.TweetText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TweetText.Font = new System.Drawing.Font("Segoe Condensed", 8F, System.Drawing.FontStyle.Regular);
            this.TweetText.Location = new System.Drawing.Point(47, 17);
            this.TweetText.Name = "TweetText";
            this.TweetText.Size = new System.Drawing.Size(196, 49);
            this.TweetText.Text = "label1";
            // 
            // TweetBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.TweetText);
            this.Controls.Add(this.UserLabel);
            this.Controls.Add(this.SourceLabel);
            this.Controls.Add(this.AvatarBox);
            this.Name = "TweetBox";
            this.Size = new System.Drawing.Size(243, 82);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox AvatarBox;
        private System.Windows.Forms.LinkLabel SourceLabel;
        private System.Windows.Forms.Label UserLabel;
        private System.Windows.Forms.Label TweetText;
    }
}
