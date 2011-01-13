using Ninject;
using Twintimidator.Threads;

namespace Twintimidator
{
    partial class MainForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.StartButton = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.ResultsUI = new Twintimidator.Results.ResultsUI();
            this.ThreadsUI = new Twintimidator.Threads.ThreadsUI();
            this.ActionsUI = new Twintimidator.ActionsUI();
            this.AccountsUI = new Twintimidator.AccountsUI();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // StartButton
            // 
            this.StartButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.StartButton.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StartButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.StartButton.Location = new System.Drawing.Point(582, 182);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(77, 38);
            this.StartButton.TabIndex = 4;
            this.StartButton.Text = "Go";
            this.StartButton.UseVisualStyleBackColor = true;
            this.StartButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Image = global::Twintimidator.Properties.Resources.twintimidator;
            this.pictureBox1.InitialImage = global::Twintimidator.Properties.Resources.twintimidator;
            this.pictureBox1.Location = new System.Drawing.Point(290, 182);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(210, 38);
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // ResultsUI
            // 
            this.ResultsUI.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ResultsUI.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ResultsUI.Location = new System.Drawing.Point(2, 276);
            this.ResultsUI.MinimumSize = new System.Drawing.Size(667, 152);
            this.ResultsUI.Name = "ResultsUI";
            this.ResultsUI.Size = new System.Drawing.Size(667, 195);
            this.ResultsUI.TabIndex = 6;
            // 
            // ThreadsUI
            // 
            this.ThreadsUI.Location = new System.Drawing.Point(2, 182);
            this.ThreadsUI.Name = "ThreadsUI";
            this.ThreadsUI.Size = new System.Drawing.Size(283, 88);
            this.ThreadsUI.TabIndex = 3;
            // 
            // ActionsUI
            // 
            this.ActionsUI.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ActionsUI.Location = new System.Drawing.Point(2, 4);
            this.ActionsUI.Name = "ActionsUI";
            this.ActionsUI.Size = new System.Drawing.Size(283, 172);
            this.ActionsUI.TabIndex = 2;
            // 
            // AccountsUI
            // 
            this.AccountsUI.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.AccountsUI.Location = new System.Drawing.Point(291, 5);
            this.AccountsUI.Name = "AccountsUI";
            this.AccountsUI.Size = new System.Drawing.Size(378, 171);
            this.AccountsUI.TabIndex = 1;
            this.AccountsUI.Load += new System.EventHandler(this.AccountsUI_Load);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(671, 449);
            this.Controls.Add(this.ResultsUI);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.StartButton);
            this.Controls.Add(this.ThreadsUI);
            this.Controls.Add(this.ActionsUI);
            this.Controls.Add(this.AccountsUI);
            this.MinimumSize = new System.Drawing.Size(687, 444);
            this.Name = "MainForm";
            this.Text = "Twintimidator";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private AccountsUI AccountsUI;
        private ActionsUI ActionsUI;
        private Twintimidator.Threads.ThreadsUI ThreadsUI;
        private System.Windows.Forms.Button StartButton;
        private System.Windows.Forms.PictureBox pictureBox1;
        private Twintimidator.Results.ResultsUI ResultsUI;

    }
}

