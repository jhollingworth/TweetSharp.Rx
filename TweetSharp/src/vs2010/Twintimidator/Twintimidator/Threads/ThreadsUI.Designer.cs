namespace Twintimidator.Threads
{
    partial class ThreadsUI
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
            System.Windows.Forms.GroupBox groupBox1;
            System.Windows.Forms.Label label2;
            System.Windows.Forms.Label label1;
            this.RepeatCountUpDown = new System.Windows.Forms.NumericUpDown();
            this.RequestAsyncRadioBtn = new System.Windows.Forms.RadioButton();
            this.ThreadPoolRadioBtn = new System.Windows.Forms.RadioButton();
            this.StandardThreadsRadioBtn = new System.Windows.Forms.RadioButton();
            this.NumberOfThreadsUpDown = new System.Windows.Forms.NumericUpDown();
            groupBox1 = new System.Windows.Forms.GroupBox();
            label2 = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RepeatCountUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumberOfThreadsUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(this.RepeatCountUpDown);
            groupBox1.Controls.Add(this.RequestAsyncRadioBtn);
            groupBox1.Controls.Add(this.ThreadPoolRadioBtn);
            groupBox1.Controls.Add(this.StandardThreadsRadioBtn);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(this.NumberOfThreadsUpDown);
            groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            groupBox1.Location = new System.Drawing.Point(0, 0);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new System.Drawing.Size(290, 81);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Threads";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(3, 38);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(100, 13);
            label2.TabIndex = 6;
            label2.Text = "Times to repeat set:";
            // 
            // RepeatCountUpDown
            // 
            this.RepeatCountUpDown.Enabled = false;
            this.RepeatCountUpDown.Location = new System.Drawing.Point(161, 34);
            this.RepeatCountUpDown.Name = "RepeatCountUpDown";
            this.RepeatCountUpDown.Size = new System.Drawing.Size(120, 20);
            this.RepeatCountUpDown.TabIndex = 5;
            // 
            // RequestAsyncRadioBtn
            // 
            this.RequestAsyncRadioBtn.AutoSize = true;
            this.RequestAsyncRadioBtn.Location = new System.Drawing.Point(187, 58);
            this.RequestAsyncRadioBtn.Name = "RequestAsyncRadioBtn";
            this.RequestAsyncRadioBtn.Size = new System.Drawing.Size(94, 17);
            this.RequestAsyncRadioBtn.TabIndex = 4;
            this.RequestAsyncRadioBtn.TabStop = true;
            this.RequestAsyncRadioBtn.Text = "RequestAsync";
            this.RequestAsyncRadioBtn.UseVisualStyleBackColor = true;
            // 
            // ThreadPoolRadioBtn
            // 
            this.ThreadPoolRadioBtn.AutoSize = true;
            this.ThreadPoolRadioBtn.Location = new System.Drawing.Point(97, 58);
            this.ThreadPoolRadioBtn.Name = "ThreadPoolRadioBtn";
            this.ThreadPoolRadioBtn.Size = new System.Drawing.Size(83, 17);
            this.ThreadPoolRadioBtn.TabIndex = 3;
            this.ThreadPoolRadioBtn.TabStop = true;
            this.ThreadPoolRadioBtn.Text = "Thread Pool";
            this.ThreadPoolRadioBtn.UseVisualStyleBackColor = true;
            // 
            // StandardThreadsRadioBtn
            // 
            this.StandardThreadsRadioBtn.AutoSize = true;
            this.StandardThreadsRadioBtn.Location = new System.Drawing.Point(7, 58);
            this.StandardThreadsRadioBtn.Name = "StandardThreadsRadioBtn";
            this.StandardThreadsRadioBtn.Size = new System.Drawing.Size(83, 17);
            this.StandardThreadsRadioBtn.TabIndex = 2;
            this.StandardThreadsRadioBtn.TabStop = true;
            this.StandardThreadsRadioBtn.Text = "Std Threads";
            this.StandardThreadsRadioBtn.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(3, 17);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(97, 13);
            label1.TabIndex = 1;
            label1.Text = "Number of threads:";
            // 
            // NumberOfThreadsUpDown
            // 
            this.NumberOfThreadsUpDown.Location = new System.Drawing.Point(161, 13);
            this.NumberOfThreadsUpDown.Name = "NumberOfThreadsUpDown";
            this.NumberOfThreadsUpDown.Size = new System.Drawing.Size(120, 20);
            this.NumberOfThreadsUpDown.TabIndex = 0;
            // 
            // ThreadsUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(groupBox1);
            this.Name = "ThreadsUI";
            this.Size = new System.Drawing.Size(290, 81);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RepeatCountUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumberOfThreadsUpDown)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NumericUpDown NumberOfThreadsUpDown;
        private System.Windows.Forms.RadioButton ThreadPoolRadioBtn;
        private System.Windows.Forms.RadioButton StandardThreadsRadioBtn;
        private System.Windows.Forms.RadioButton RequestAsyncRadioBtn;
        private System.Windows.Forms.NumericUpDown RepeatCountUpDown;

    }
}
