namespace Doujin
{
    partial class Form2
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form2));
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.download = new System.Windows.Forms.Button();
			this.infoTitle = new System.Windows.Forms.Label();
			this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
			this.progressBar1 = new System.Windows.Forms.ProgressBar();
			this.infoPage = new System.Windows.Forms.Label();
			this.panel1 = new System.Windows.Forms.Panel();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// pictureBox1
			// 
			this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pictureBox1.Location = new System.Drawing.Point(2, 3);
			this.pictureBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(470, 382);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.pictureBox1.TabIndex = 0;
			this.pictureBox1.TabStop = false;
			// 
			// download
			// 
			this.download.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.download.BackColor = System.Drawing.Color.DodgerBlue;
			this.download.FlatAppearance.BorderSize = 0;
			this.download.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.download.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.download.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
			this.download.Location = new System.Drawing.Point(286, 147);
			this.download.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.download.Name = "download";
			this.download.Size = new System.Drawing.Size(181, 53);
			this.download.TabIndex = 1;
			this.download.Text = "Download";
			this.download.UseVisualStyleBackColor = false;
			this.download.Click += new System.EventHandler(this.download_Click);
			// 
			// infoTitle
			// 
			this.infoTitle.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.infoTitle.BackColor = System.Drawing.Color.Transparent;
			this.infoTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.infoTitle.ForeColor = System.Drawing.SystemColors.ControlLightLight;
			this.infoTitle.Location = new System.Drawing.Point(3, 0);
			this.infoTitle.Name = "infoTitle";
			this.infoTitle.Size = new System.Drawing.Size(464, 143);
			this.infoTitle.TabIndex = 2;
			this.infoTitle.Text = "Titile";
			// 
			// progressBar1
			// 
			this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.progressBar1.Location = new System.Drawing.Point(3, 208);
			this.progressBar1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.progressBar1.Name = "progressBar1";
			this.progressBar1.Size = new System.Drawing.Size(464, 15);
			this.progressBar1.TabIndex = 3;
			// 
			// infoPage
			// 
			this.infoPage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.infoPage.AutoSize = true;
			this.infoPage.BackColor = System.Drawing.Color.Transparent;
			this.infoPage.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
			this.infoPage.ForeColor = System.Drawing.SystemColors.ButtonFace;
			this.infoPage.Location = new System.Drawing.Point(10, 176);
			this.infoPage.Name = "infoPage";
			this.infoPage.Size = new System.Drawing.Size(61, 24);
			this.infoPage.TabIndex = 4;
			this.infoPage.Text = "pages";
			// 
			// panel1
			// 
			this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panel1.Controls.Add(this.infoTitle);
			this.panel1.Controls.Add(this.download);
			this.panel1.Controls.Add(this.infoPage);
			this.panel1.Controls.Add(this.progressBar1);
			this.panel1.Location = new System.Drawing.Point(2, 392);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(470, 227);
			this.panel1.TabIndex = 5;
			// 
			// Form2
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(144F, 144F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.ClientSize = new System.Drawing.Size(473, 622);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.pictureBox1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.MinimumSize = new System.Drawing.Size(300, 300);
			this.Name = "Form2";
			this.Text = "Downloader";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form2_FormClosing);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button download;
        private System.Windows.Forms.Label infoTitle;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label infoPage;
		private System.Windows.Forms.Panel panel1;
	}
}