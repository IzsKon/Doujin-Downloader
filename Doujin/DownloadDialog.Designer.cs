namespace Doujin
{
    partial class DownloadDialog
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
            this.loactionLabel = new System.Windows.Forms.Label();
            this.filenameButton = new System.Windows.Forms.Button();
            this.titleLabel = new System.Windows.Forms.Label();
            this.titleTextBox = new System.Windows.Forms.TextBox();
            this.okBtn = new System.Windows.Forms.Button();
            this.filenameToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.cancelBtn = new System.Windows.Forms.Button();
            this.illegalCharToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // loactionLabel
            // 
            this.loactionLabel.AutoSize = true;
            this.loactionLabel.Font = new System.Drawing.Font("Microsoft JhengHei", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.loactionLabel.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.loactionLabel.Location = new System.Drawing.Point(12, 4);
            this.loactionLabel.Name = "loactionLabel";
            this.loactionLabel.Size = new System.Drawing.Size(184, 24);
            this.loactionLabel.TabIndex = 0;
            this.loactionLabel.Text = "Download Location";
            // 
            // filenameButton
            // 
            this.filenameButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.filenameButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.filenameButton.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.HotTrack;
            this.filenameButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.filenameButton.Font = new System.Drawing.Font("PMingLiU", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.filenameButton.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.filenameButton.Location = new System.Drawing.Point(12, 33);
            this.filenameButton.Name = "filenameButton";
            this.filenameButton.Size = new System.Drawing.Size(508, 37);
            this.filenameButton.TabIndex = 1;
            this.filenameButton.Text = "C://";
            this.filenameButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.filenameButton.UseVisualStyleBackColor = false;
            this.filenameButton.Click += new System.EventHandler(this.filnameButton_Click);
            // 
            // titleLabel
            // 
            this.titleLabel.AutoSize = true;
            this.titleLabel.Font = new System.Drawing.Font("Microsoft JhengHei", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.titleLabel.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.titleLabel.Location = new System.Drawing.Point(11, 81);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(49, 24);
            this.titleLabel.TabIndex = 2;
            this.titleLabel.Text = "Title";
            // 
            // titleTextBox
            // 
            this.titleTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.titleTextBox.Font = new System.Drawing.Font("PMingLiU", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.titleTextBox.ForeColor = System.Drawing.SystemColors.Window;
            this.titleTextBox.Location = new System.Drawing.Point(12, 109);
            this.titleTextBox.Name = "titleTextBox";
            this.titleTextBox.Size = new System.Drawing.Size(508, 35);
            this.titleTextBox.TabIndex = 3;
            this.titleTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.titleTextBox_KeyPress);
            // 
            // okBtn
            // 
            this.okBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.okBtn.Font = new System.Drawing.Font("Microsoft JhengHei", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.okBtn.Location = new System.Drawing.Point(274, 158);
            this.okBtn.Name = "okBtn";
            this.okBtn.Size = new System.Drawing.Size(120, 40);
            this.okBtn.TabIndex = 4;
            this.okBtn.Text = "OK";
            this.okBtn.UseVisualStyleBackColor = true;
            this.okBtn.Click += new System.EventHandler(this.okBtn_Click);
            // 
            // filenameToolTip
            // 
            this.filenameToolTip.Tag = "";
            this.filenameToolTip.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.filenameToolTip.ToolTipTitle = "Change Directory";
            // 
            // cancelBtn
            // 
            this.cancelBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelBtn.Font = new System.Drawing.Font("Microsoft JhengHei", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.cancelBtn.Location = new System.Drawing.Point(400, 158);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(120, 40);
            this.cancelBtn.TabIndex = 5;
            this.cancelBtn.Text = "Cancel";
            this.cancelBtn.UseVisualStyleBackColor = true;
            // 
            // illegalCharToolTip
            // 
            this.illegalCharToolTip.AutomaticDelay = 0;
            this.illegalCharToolTip.AutoPopDelay = 0;
            this.illegalCharToolTip.InitialDelay = 0;
            this.illegalCharToolTip.IsBalloon = true;
            this.illegalCharToolTip.ReshowDelay = 0;
            this.illegalCharToolTip.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Warning;
            this.illegalCharToolTip.ToolTipTitle = "File names cannot contain any of the following characters:";
            // 
            // DownloadDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.CancelButton = this.cancelBtn;
            this.ClientSize = new System.Drawing.Size(531, 216);
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.okBtn);
            this.Controls.Add(this.titleTextBox);
            this.Controls.Add(this.titleLabel);
            this.Controls.Add(this.filenameButton);
            this.Controls.Add(this.loactionLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DownloadDialog";
            this.ShowIcon = false;
            this.Text = "Download";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label loactionLabel;
        private System.Windows.Forms.Button filenameButton;
        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.TextBox titleTextBox;
        private System.Windows.Forms.Button okBtn;
        private System.Windows.Forms.ToolTip filenameToolTip;
        private System.Windows.Forms.Button cancelBtn;
        private System.Windows.Forms.ToolTip illegalCharToolTip;
    }
}