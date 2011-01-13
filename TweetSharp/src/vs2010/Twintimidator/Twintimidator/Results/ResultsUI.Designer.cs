namespace Twintimidator.Results
{
    partial class ResultsUI
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
            System.Windows.Forms.Label label1;
            System.Windows.Forms.Label label2;
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.PassedCountLabel = new System.Windows.Forms.Label();
            this.FailCountLabel = new System.Windows.Forms.Label();
            this.ErrorsText = new System.Windows.Forms.TextBox();
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // progressBar
            // 
            this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar.Location = new System.Drawing.Point(3, 3);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(335, 23);
            this.progressBar.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label1.Location = new System.Drawing.Point(4, 33);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(66, 21);
            label1.TabIndex = 1;
            label1.Text = "Passed:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label2.Location = new System.Drawing.Point(178, 33);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(73, 21);
            label2.TabIndex = 2;
            label2.Text = "Failures:";
            // 
            // PassedCountLabel
            // 
            this.PassedCountLabel.AutoSize = true;
            this.PassedCountLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PassedCountLabel.ForeColor = System.Drawing.Color.Green;
            this.PassedCountLabel.Location = new System.Drawing.Point(76, 33);
            this.PassedCountLabel.Name = "PassedCountLabel";
            this.PassedCountLabel.Size = new System.Drawing.Size(52, 21);
            this.PassedCountLabel.TabIndex = 3;
            this.PassedCountLabel.Text = "label3";
            // 
            // FailCountLabel
            // 
            this.FailCountLabel.AutoSize = true;
            this.FailCountLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FailCountLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.FailCountLabel.Location = new System.Drawing.Point(257, 33);
            this.FailCountLabel.Name = "FailCountLabel";
            this.FailCountLabel.Size = new System.Drawing.Size(52, 21);
            this.FailCountLabel.TabIndex = 4;
            this.FailCountLabel.Text = "label4";
            // 
            // ErrorsText
            // 
            this.ErrorsText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ErrorsText.Location = new System.Drawing.Point(7, 57);
            this.ErrorsText.Multiline = true;
            this.ErrorsText.Name = "ErrorsText";
            this.ErrorsText.Size = new System.Drawing.Size(328, 67);
            this.ErrorsText.TabIndex = 5;
            // 
            // ResultsUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ErrorsText);
            this.Controls.Add(this.FailCountLabel);
            this.Controls.Add(this.PassedCountLabel);
            this.Controls.Add(label2);
            this.Controls.Add(label1);
            this.Controls.Add(this.progressBar);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "ResultsUI";
            this.Size = new System.Drawing.Size(338, 124);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label PassedCountLabel;
        private System.Windows.Forms.Label FailCountLabel;
        private System.Windows.Forms.TextBox ErrorsText;
    }
}
