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
            this.magicNumTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.doujinCoverPic = new System.Windows.Forms.PictureBox();
            this.doujinTitleLabel = new System.Windows.Forms.Label();
            this.downloadButton = new System.Windows.Forms.Button();
            this.doujinLenLabel = new System.Windows.Forms.Label();
            this.doujinInfoPanel = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.doujinCoverPic)).BeginInit();
            this.doujinInfoPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // magicNumTextBox
            // 
            this.magicNumTextBox.Font = new System.Drawing.Font("PMingLiU", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.magicNumTextBox.ImeMode = System.Windows.Forms.ImeMode.Alpha;
            this.magicNumTextBox.Location = new System.Drawing.Point(12, 35);
            this.magicNumTextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.magicNumTextBox.Name = "magicNumTextBox";
            this.magicNumTextBox.Size = new System.Drawing.Size(137, 40);
            this.magicNumTextBox.TabIndex = 0;
            this.magicNumTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.magicNumber_KeyPress);
            this.magicNumTextBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.magicNumber_MouseDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("PMingLiU", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label1.Location = new System.Drawing.Point(9, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(115, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "magic number";
            // 
            // doujinCoverPic
            // 
            this.doujinCoverPic.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.doujinCoverPic.Location = new System.Drawing.Point(-1, 0);
            this.doujinCoverPic.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.doujinCoverPic.Name = "doujinCoverPic";
            this.doujinCoverPic.Size = new System.Drawing.Size(316, 314);
            this.doujinCoverPic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.doujinCoverPic.TabIndex = 3;
            this.doujinCoverPic.TabStop = false;
            // 
            // doujinTitleLabel
            // 
            this.doujinTitleLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.doujinTitleLabel.BackColor = System.Drawing.Color.Transparent;
            this.doujinTitleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.doujinTitleLabel.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.doujinTitleLabel.Location = new System.Drawing.Point(2, 323);
            this.doujinTitleLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.doujinTitleLabel.Name = "doujinTitleLabel";
            this.doujinTitleLabel.Size = new System.Drawing.Size(313, 136);
            this.doujinTitleLabel.TabIndex = 4;
            this.doujinTitleLabel.Text = "Title";
            // 
            // downloadButton
            // 
            this.downloadButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.downloadButton.BackColor = System.Drawing.Color.DodgerBlue;
            this.downloadButton.FlatAppearance.BorderSize = 0;
            this.downloadButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.downloadButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.downloadButton.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.downloadButton.Location = new System.Drawing.Point(154, 35);
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
            this.doujinLenLabel.AutoSize = true;
            this.doujinLenLabel.BackColor = System.Drawing.Color.Transparent;
            this.doujinLenLabel.Font = new System.Drawing.Font("PMingLiU", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.doujinLenLabel.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.doujinLenLabel.Location = new System.Drawing.Point(3, 465);
            this.doujinLenLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.doujinLenLabel.Name = "doujinLenLabel";
            this.doujinLenLabel.Size = new System.Drawing.Size(50, 20);
            this.doujinLenLabel.TabIndex = 6;
            this.doujinLenLabel.Text = "pages";
            // 
            // doujinInfoPanel
            // 
            this.doujinInfoPanel.Controls.Add(this.doujinCoverPic);
            this.doujinInfoPanel.Controls.Add(this.doujinLenLabel);
            this.doujinInfoPanel.Controls.Add(this.doujinTitleLabel);
            this.doujinInfoPanel.Location = new System.Drawing.Point(13, 82);
            this.doujinInfoPanel.Name = "doujinInfoPanel";
            this.doujinInfoPanel.Size = new System.Drawing.Size(315, 492);
            this.doujinInfoPanel.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 15);
            this.label2.TabIndex = 8;
            this.label2.Text = "label2";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(982, 576);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.doujinInfoPanel);
            this.Controls.Add(this.downloadButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.magicNumTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "Doujin";
            ((System.ComponentModel.ISupportInitialize)(this.doujinCoverPic)).EndInit();
            this.doujinInfoPanel.ResumeLayout(false);
            this.doujinInfoPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox magicNumTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox doujinCoverPic;
        private System.Windows.Forms.Label doujinTitleLabel;
        private System.Windows.Forms.Button downloadButton;
        private System.Windows.Forms.Label doujinLenLabel;
        private System.Windows.Forms.Panel doujinInfoPanel;
        private System.Windows.Forms.Label label2;
    }
}

