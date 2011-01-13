namespace Demo.WindowsMobile
{
    partial class TimeLine
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu mainMenu1;

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
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.TimeLinePanel = new System.Windows.Forms.Panel();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.menuItem1);
            // 
            // TimeLinePanel
            // 
            this.TimeLinePanel.AutoScroll = true;
            this.TimeLinePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TimeLinePanel.Location = new System.Drawing.Point(0, 0);
            this.TimeLinePanel.Name = "TimeLinePanel";
            this.TimeLinePanel.Size = new System.Drawing.Size(176, 180);
            // 
            // menuItem1
            // 
            this.menuItem1.Text = "New Tweet";
            this.menuItem1.Click += new System.EventHandler(this.menuItem1_Click);
            // 
            // TimeLine
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(176, 180);
            this.Controls.Add(this.TimeLinePanel);
            this.Menu = this.mainMenu1;
            this.Name = "TimeLine";
            this.Text = "Home Timeline";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel TimeLinePanel;
        private System.Windows.Forms.MenuItem menuItem1;

    }
}