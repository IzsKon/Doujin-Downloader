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
			this.magicNumber = new System.Windows.Forms.TextBox();
			this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
			this.label1 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// magicNumber
			// 
			this.magicNumber.Font = new System.Drawing.Font("新細明體", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
			this.magicNumber.ImeMode = System.Windows.Forms.ImeMode.Alpha;
			this.magicNumber.Location = new System.Drawing.Point(14, 42);
			this.magicNumber.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.magicNumber.Name = "magicNumber";
			this.magicNumber.Size = new System.Drawing.Size(154, 46);
			this.magicNumber.TabIndex = 0;
			this.magicNumber.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.magicNumber_KeyPress);
			this.magicNumber.MouseDown += new System.Windows.Forms.MouseEventHandler(this.magicNumber_MouseDown);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.BackColor = System.Drawing.Color.Transparent;
			this.label1.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
			this.label1.ForeColor = System.Drawing.SystemColors.ButtonFace;
			this.label1.Location = new System.Drawing.Point(10, 13);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(140, 24);
			this.label1.TabIndex = 2;
			this.label1.Text = "magic number";
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
			this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.ClientSize = new System.Drawing.Size(180, 104);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.magicNumber);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "Form1";
			this.Text = "Doujin";
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox magicNumber;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Label label1;
    }
}

