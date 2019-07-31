using System;

namespace Doujin
{
	partial class TaskUI
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.Timer timer;

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

		public void shift(System.Drawing.PointF newPos)
		{
			timer?.Dispose();
			timer = new System.Windows.Forms.Timer();
			timer.Interval = 20;

			System.Drawing.PointF position = this.Location;
            System.Drawing.PointF delta = new System.Drawing.PointF(newPos.X - this.Location.X, newPos.Y - this.Location.Y);
            timer.Tick += (sen, eve) =>
			{
                // (int)position.X is to cancel out floating point error
                position.X += this.Location.X - (int)position.X + delta.X * 0.1f;
                position.Y += this.Location.Y - (int)position.Y + delta.Y * 0.1f;

                delta.X -= delta.X * 0.1f;
                delta.Y -= delta.Y * 0.1f;

                Location = new System.Drawing.Point((int)position.X, (int)position.Y);
				if (Math.Abs(delta.X) < 0.1 && Math.Abs(delta.Y) < 0.1)
				{
					this.Location = new System.Drawing.Point((int)(position.X + delta.X), (int)(position.Y + delta.Y));
					timer.Stop();
				}
			};
			timer.Start();                    
        }

		public void setTitle(string title)
		{
			this.label_title.Text = title;
		}

		public void setCancelEvent(System.EventHandler even)
		{
			this.button_cancel.Click += even;
		}

		public void setProgress(string text)
		{
			this.label_progress.Text = text;
		}

		public void setProgressColor(System.Drawing.Color color)
		{
			this.label_progress.ForeColor = color;
		}

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.label_title = new System.Windows.Forms.Label();
            this.label_progress = new System.Windows.Forms.Label();
            this.button_cancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label_title
            // 
            this.label_title.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label_title.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.label_title.ForeColor = System.Drawing.Color.White;
            this.label_title.Location = new System.Drawing.Point(1, 1);
            this.label_title.Margin = new System.Windows.Forms.Padding(1);
            this.label_title.Name = "label_title";
            this.label_title.Size = new System.Drawing.Size(467, 36);
            this.label_title.TabIndex = 0;
            this.label_title.Text = "title";
            this.label_title.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_progress
            // 
            this.label_progress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label_progress.BackColor = System.Drawing.Color.Transparent;
            this.label_progress.ForeColor = System.Drawing.Color.White;
            this.label_progress.Location = new System.Drawing.Point(469, 1);
            this.label_progress.Margin = new System.Windows.Forms.Padding(1);
            this.label_progress.Name = "label_progress";
            this.label_progress.Size = new System.Drawing.Size(110, 36);
            this.label_progress.TabIndex = 1;
            this.label_progress.Text = "waiting...";
            this.label_progress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // button_cancel
            // 
            this.button_cancel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button_cancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(37)))), ((int)(((byte)(83)))));
            this.button_cancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_cancel.FlatAppearance.BorderSize = 0;
            this.button_cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_cancel.Font = new System.Drawing.Font("UD Digi Kyokasho NK-B", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button_cancel.ForeColor = System.Drawing.Color.White;
            this.button_cancel.Location = new System.Drawing.Point(581, 1);
            this.button_cancel.Margin = new System.Windows.Forms.Padding(1);
            this.button_cancel.Name = "button_cancel";
            this.button_cancel.Size = new System.Drawing.Size(31, 38);
            this.button_cancel.TabIndex = 2;
            this.button_cancel.TabStop = false;
            this.button_cancel.Text = "X";
            this.button_cancel.UseVisualStyleBackColor = false;
            // 
            // TaskUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.button_cancel);
            this.Controls.Add(this.label_progress);
            this.Controls.Add(this.label_title);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "TaskUI";
            this.Size = new System.Drawing.Size(613, 38);
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label label_title;
		private System.Windows.Forms.Label label_progress;
		private System.Windows.Forms.Button button_cancel;
    }
}
