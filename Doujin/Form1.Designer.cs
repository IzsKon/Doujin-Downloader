﻿namespace Doujin
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.magicNumTextBox = new System.Windows.Forms.TextBox();
            this.doujinTitleLabel = new System.Windows.Forms.Label();
            this.downloadButton = new System.Windows.Forms.Button();
            this.doujinLenLabel = new System.Windows.Forms.Label();
            this.doujinInfoPanel = new System.Windows.Forms.Panel();
            this.doujinCoverPic = new System.Windows.Forms.PictureBox();
            this.magicNumToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.titleToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.searchButton = new System.Windows.Forms.Button();
            this.dtPanel = new System.Windows.Forms.Panel();
            this.doujinInfoPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.doujinCoverPic)).BeginInit();
            this.SuspendLayout();
            // 
            // magicNumTextBox
            // 
            this.magicNumTextBox.Font = new System.Drawing.Font("PMingLiU", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.magicNumTextBox.ImeMode = System.Windows.Forms.ImeMode.Alpha;
            this.magicNumTextBox.Location = new System.Drawing.Point(19, 13);
            this.magicNumTextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.magicNumTextBox.Name = "magicNumTextBox";
            this.magicNumTextBox.Size = new System.Drawing.Size(256, 40);
            this.magicNumTextBox.TabIndex = 0;
            this.magicNumToolTip.SetToolTip(this.magicNumTextBox, " ");
            this.magicNumTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.magicNumber_KeyPress);
            // 
            // doujinTitleLabel
            // 
            this.doujinTitleLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.doujinTitleLabel.BackColor = System.Drawing.Color.Transparent;
            this.doujinTitleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.doujinTitleLabel.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.doujinTitleLabel.Location = new System.Drawing.Point(2, 340);
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
            this.downloadButton.Enabled = false;
            this.downloadButton.FlatAppearance.BorderSize = 0;
            this.downloadButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.downloadButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.downloadButton.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.downloadButton.Location = new System.Drawing.Point(162, 467);
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
            this.doujinLenLabel.Location = new System.Drawing.Point(2, 479);
            this.doujinLenLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.doujinLenLabel.Name = "doujinLenLabel";
            this.doujinLenLabel.Size = new System.Drawing.Size(156, 27);
            this.doujinLenLabel.TabIndex = 6;
            // 
            // doujinInfoPanel
            // 
            this.doujinInfoPanel.Controls.Add(this.doujinCoverPic);
            this.doujinInfoPanel.Controls.Add(this.doujinLenLabel);
            this.doujinInfoPanel.Controls.Add(this.downloadButton);
            this.doujinInfoPanel.Controls.Add(this.doujinTitleLabel);
            this.doujinInfoPanel.Location = new System.Drawing.Point(13, 60);
            this.doujinInfoPanel.Name = "doujinInfoPanel";
            this.doujinInfoPanel.Size = new System.Drawing.Size(315, 514);
            this.doujinInfoPanel.TabIndex = 7;
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
            this.doujinCoverPic.Size = new System.Drawing.Size(313, 336);
            this.doujinCoverPic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.doujinCoverPic.TabIndex = 3;
            this.doujinCoverPic.TabStop = false;
            this.doujinCoverPic.Tag = "";
            // 
            // magicNumToolTip
            // 
            this.magicNumToolTip.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.magicNumToolTip.ToolTipTitle = "magic number";
            // 
            // searchButton
            // 
            this.searchButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(37)))), ((int)(((byte)(83)))));
            this.searchButton.BackgroundImage = global::Doujin.Properties.Resources.search_icon_2;
            this.searchButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.searchButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.searchButton.FlatAppearance.BorderSize = 0;
            this.searchButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.searchButton.Location = new System.Drawing.Point(281, 10);
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(45, 45);
            this.searchButton.TabIndex = 8;
            this.searchButton.UseVisualStyleBackColor = false;
            this.searchButton.Click += new System.EventHandler(this.searchButton_Click);
            // 
            // dtPanel
            // 
            this.dtPanel.AutoScroll = true;
            this.dtPanel.Location = new System.Drawing.Point(347, 13);
            this.dtPanel.Name = "dtPanel";
            this.dtPanel.Size = new System.Drawing.Size(674, 551);
            this.dtPanel.TabIndex = 9;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(982, 576);
            this.Controls.Add(this.dtPanel);
            this.Controls.Add(this.searchButton);
            this.Controls.Add(this.doujinInfoPanel);
            this.Controls.Add(this.magicNumTextBox);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Doujin Downloader";
            this.doujinInfoPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.doujinCoverPic)).EndInit();
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
        private System.Windows.Forms.ToolTip magicNumToolTip;
        private System.Windows.Forms.ToolTip titleToolTip;
        private System.Windows.Forms.Button searchButton;
        private System.Windows.Forms.Panel dtPanel;
    }
}

