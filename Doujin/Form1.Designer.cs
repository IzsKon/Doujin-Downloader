namespace Doujin
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.magicNumTextBox = new System.Windows.Forms.TextBox();
            this.doujinCoverPic = new System.Windows.Forms.PictureBox();
            this.doujinTitleLabel = new System.Windows.Forms.Label();
            this.downloadButton = new System.Windows.Forms.Button();
            this.doujinLenLabel = new System.Windows.Forms.Label();
            this.doujinInfoPanel = new System.Windows.Forms.Panel();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.startTextBox = new System.Windows.Forms.TextBox();
            this.endTextBox = new System.Windows.Forms.TextBox();
            this.pathButton = new System.Windows.Forms.Button();
            this.startLabel = new System.Windows.Forms.Label();
            this.endLabel = new System.Windows.Forms.Label();
            this.pathLabel = new System.Windows.Forms.Label();
            this.pageLabel = new System.Windows.Forms.Label();
            this.delayTrackBar = new System.Windows.Forms.TrackBar();
            this.delayLabel = new System.Windows.Forms.Label();
            this.minDelayLabel = new System.Windows.Forms.Label();
            this.maxDelayLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.doujinCoverPic)).BeginInit();
            this.doujinInfoPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.delayTrackBar)).BeginInit();
            this.SuspendLayout();
            // 
            // magicNumTextBox
            // 
            this.magicNumTextBox.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.magicNumTextBox.Font = new System.Drawing.Font("PMingLiU", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.magicNumTextBox.ImeMode = System.Windows.Forms.ImeMode.Alpha;
            this.magicNumTextBox.Location = new System.Drawing.Point(13, 13);
            this.magicNumTextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.magicNumTextBox.Name = "magicNumTextBox";
            this.magicNumTextBox.ReadOnly = true;
            this.magicNumTextBox.Size = new System.Drawing.Size(315, 40);
            this.magicNumTextBox.TabIndex = 0;
            this.magicNumTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.magicNumber_KeyPress);
            this.magicNumTextBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.magicNumber_MouseDown);
            // 
            // doujinCoverPic
            // 
            this.doujinCoverPic.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.doujinCoverPic.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.doujinCoverPic.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.doujinCoverPic.Location = new System.Drawing.Point(0, 0);
            this.doujinCoverPic.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.doujinCoverPic.Name = "doujinCoverPic";
            this.doujinCoverPic.Size = new System.Drawing.Size(313, 343);
            this.doujinCoverPic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.doujinCoverPic.TabIndex = 3;
            this.doujinCoverPic.TabStop = false;
            this.doujinCoverPic.Tag = "";
            // 
            // doujinTitleLabel
            // 
            this.doujinTitleLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.doujinTitleLabel.BackColor = System.Drawing.Color.Transparent;
            this.doujinTitleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.doujinTitleLabel.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.doujinTitleLabel.Location = new System.Drawing.Point(2, 342);
            this.doujinTitleLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.doujinTitleLabel.Name = "doujinTitleLabel";
            this.doujinTitleLabel.Size = new System.Drawing.Size(313, 123);
            this.doujinTitleLabel.TabIndex = 4;
            // 
            // downloadButton
            // 
            this.downloadButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.downloadButton.BackColor = System.Drawing.Color.DodgerBlue;
            this.downloadButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.downloadButton.FlatAppearance.BorderSize = 0;
            this.downloadButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.downloadButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.downloadButton.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.downloadButton.Location = new System.Drawing.Point(164, 469);
            this.downloadButton.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.downloadButton.Name = "downloadButton";
            this.downloadButton.Size = new System.Drawing.Size(151, 40);
            this.downloadButton.TabIndex = 5;
            this.downloadButton.Text = "Download";
            this.downloadButton.UseVisualStyleBackColor = false;
            this.downloadButton.Click += new System.EventHandler(this.downloadButton_Click);
            // 
            // doujinLenLabel
            // 
            this.doujinLenLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.doujinLenLabel.BackColor = System.Drawing.Color.Transparent;
            this.doujinLenLabel.Font = new System.Drawing.Font("PMingLiU", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.doujinLenLabel.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.doujinLenLabel.Location = new System.Drawing.Point(4, 481);
            this.doujinLenLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.doujinLenLabel.Name = "doujinLenLabel";
            this.doujinLenLabel.Size = new System.Drawing.Size(156, 27);
            this.doujinLenLabel.TabIndex = 6;
            // 
            // doujinInfoPanel
            // 
            this.doujinInfoPanel.Controls.Add(this.progressBar);
            this.doujinInfoPanel.Controls.Add(this.doujinCoverPic);
            this.doujinInfoPanel.Controls.Add(this.doujinLenLabel);
            this.doujinInfoPanel.Controls.Add(this.downloadButton);
            this.doujinInfoPanel.Controls.Add(this.doujinTitleLabel);
            this.doujinInfoPanel.Location = new System.Drawing.Point(13, 60);
            this.doujinInfoPanel.Name = "doujinInfoPanel";
            this.doujinInfoPanel.Size = new System.Drawing.Size(315, 532);
            this.doujinInfoPanel.TabIndex = 7;
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(2, 516);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(313, 10);
            this.progressBar.TabIndex = 7;
            // 
            // startTextBox
            // 
            this.startTextBox.ImeMode = System.Windows.Forms.ImeMode.Alpha;
            this.startTextBox.Location = new System.Drawing.Point(358, 150);
            this.startTextBox.Name = "startTextBox";
            this.startTextBox.Size = new System.Drawing.Size(100, 25);
            this.startTextBox.TabIndex = 9;
            this.startTextBox.Text = "1";
            this.startTextBox.TextChanged += new System.EventHandler(this.startTextBox_TextChanged);
            this.startTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.numOnly_KeyPress);
            // 
            // endTextBox
            // 
            this.endTextBox.ImeMode = System.Windows.Forms.ImeMode.Alpha;
            this.endTextBox.Location = new System.Drawing.Point(502, 150);
            this.endTextBox.Name = "endTextBox";
            this.endTextBox.Size = new System.Drawing.Size(118, 25);
            this.endTextBox.TabIndex = 10;
            this.endTextBox.Text = "2558";
            this.endTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.numOnly_KeyPress);
            // 
            // pathButton
            // 
            this.pathButton.Location = new System.Drawing.Point(358, 206);
            this.pathButton.Name = "pathButton";
            this.pathButton.Size = new System.Drawing.Size(588, 40);
            this.pathButton.TabIndex = 11;
            this.pathButton.Text = "C:\\\\";
            this.pathButton.UseVisualStyleBackColor = true;
            this.pathButton.Click += new System.EventHandler(this.pathButton_Click);
            // 
            // startLabel
            // 
            this.startLabel.AutoSize = true;
            this.startLabel.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.startLabel.Location = new System.Drawing.Point(358, 132);
            this.startLabel.Name = "startLabel";
            this.startLabel.Size = new System.Drawing.Size(65, 15);
            this.startLabel.TabIndex = 12;
            this.startLabel.Text = "Start Page";
            // 
            // endLabel
            // 
            this.endLabel.AutoSize = true;
            this.endLabel.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.endLabel.Location = new System.Drawing.Point(499, 132);
            this.endLabel.Name = "endLabel";
            this.endLabel.Size = new System.Drawing.Size(61, 15);
            this.endLabel.TabIndex = 13;
            this.endLabel.Text = "End Page";
            // 
            // pathLabel
            // 
            this.pathLabel.AutoSize = true;
            this.pathLabel.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.pathLabel.Location = new System.Drawing.Point(358, 188);
            this.pathLabel.Name = "pathLabel";
            this.pathLabel.Size = new System.Drawing.Size(119, 15);
            this.pathLabel.TabIndex = 14;
            this.pathLabel.Text = "Download Location";
            // 
            // pageLabel
            // 
            this.pageLabel.AutoSize = true;
            this.pageLabel.Font = new System.Drawing.Font("PMingLiU", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.pageLabel.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.pageLabel.Location = new System.Drawing.Point(640, 150);
            this.pageLabel.Name = "pageLabel";
            this.pageLabel.Size = new System.Drawing.Size(141, 24);
            this.pageLabel.TabIndex = 15;
            this.pageLabel.Text = "now at page 1 ";
            // 
            // delayTrackBar
            // 
            this.delayTrackBar.LargeChange = 50;
            this.delayTrackBar.Location = new System.Drawing.Point(361, 347);
            this.delayTrackBar.Maximum = 500;
            this.delayTrackBar.Minimum = 50;
            this.delayTrackBar.Name = "delayTrackBar";
            this.delayTrackBar.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.delayTrackBar.Size = new System.Drawing.Size(299, 56);
            this.delayTrackBar.TabIndex = 16;
            this.delayTrackBar.Tag = "";
            this.delayTrackBar.Value = 300;
            this.delayTrackBar.Scroll += new System.EventHandler(this.delayTrackBar_Scroll);
            // 
            // delayLabel
            // 
            this.delayLabel.AutoSize = true;
            this.delayLabel.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.delayLabel.Location = new System.Drawing.Point(374, 318);
            this.delayLabel.Name = "delayLabel";
            this.delayLabel.Size = new System.Drawing.Size(161, 15);
            this.delayLabel.TabIndex = 17;
            this.delayLabel.Text = "Download Delay: 300 (ms)";
            // 
            // minDelayLabel
            // 
            this.minDelayLabel.AutoSize = true;
            this.minDelayLabel.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.minDelayLabel.Location = new System.Drawing.Point(358, 388);
            this.minDelayLabel.Name = "minDelayLabel";
            this.minDelayLabel.Size = new System.Drawing.Size(21, 15);
            this.minDelayLabel.TabIndex = 18;
            this.minDelayLabel.Text = "50";
            // 
            // maxDelayLabel
            // 
            this.maxDelayLabel.AutoSize = true;
            this.maxDelayLabel.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.maxDelayLabel.Location = new System.Drawing.Point(614, 388);
            this.maxDelayLabel.Name = "maxDelayLabel";
            this.maxDelayLabel.Size = new System.Drawing.Size(28, 15);
            this.maxDelayLabel.TabIndex = 19;
            this.maxDelayLabel.Text = "500";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(982, 598);
            this.Controls.Add(this.maxDelayLabel);
            this.Controls.Add(this.minDelayLabel);
            this.Controls.Add(this.delayLabel);
            this.Controls.Add(this.delayTrackBar);
            this.Controls.Add(this.pageLabel);
            this.Controls.Add(this.pathLabel);
            this.Controls.Add(this.endLabel);
            this.Controls.Add(this.startLabel);
            this.Controls.Add(this.pathButton);
            this.Controls.Add(this.endTextBox);
            this.Controls.Add(this.startTextBox);
            this.Controls.Add(this.doujinInfoPanel);
            this.Controls.Add(this.magicNumTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Doujin Downloader";
            ((System.ComponentModel.ISupportInitialize)(this.doujinCoverPic)).EndInit();
            this.doujinInfoPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.delayTrackBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox magicNumTextBox;
        private System.Windows.Forms.PictureBox doujinCoverPic;
        private System.Windows.Forms.Label doujinTitleLabel;
        private System.Windows.Forms.Button downloadButton;
        private System.Windows.Forms.Label doujinLenLabel;
        private System.Windows.Forms.Panel doujinInfoPanel;
        private System.Windows.Forms.TextBox startTextBox;
        private System.Windows.Forms.TextBox endTextBox;
        private System.Windows.Forms.Button pathButton;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label pageLabel;
        private System.Windows.Forms.Label pathLabel;
        private System.Windows.Forms.Label endLabel;
        private System.Windows.Forms.Label startLabel;
        private System.Windows.Forms.TrackBar delayTrackBar;
        private System.Windows.Forms.Label delayLabel;
        private System.Windows.Forms.Label minDelayLabel;
        private System.Windows.Forms.Label maxDelayLabel;
    }
}

