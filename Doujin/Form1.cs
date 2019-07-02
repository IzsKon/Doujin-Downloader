using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Threading;

namespace Doujin
{
	public partial class Form1 : Form
	{
		private bool selected = false;
		private LinkedList<Form2> downloadPages = new LinkedList<Form2>();
		private int currentDownload = 0;
		private const int maxDownload = 3;

		public Form1()
		{
			InitializeComponent();
			magicNumber.Focus();
		}

		private async void magicNumber_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
				e.Handled = true;
			if (e.KeyChar == (char)13)
			{
				selected = true;
				magicNumber.SelectAll();

				// set up download page
				var downloadPage = new Form2(magicNumber.Text);
				downloadPage.checkMaxReached = () =>
				{
					return currentDownload >= maxDownload;
				};
				downloadPage.startDownloadEvent += (sen, eve) =>
				{
					currentDownload++;
				};
				downloadPage.finishEvent += (sen, eve) =>
				{
					currentDownload--;
					downloadPages.Remove(downloadPage);
				};
				// add to list
				downloadPages.AddLast(downloadPage);

				// set up task
				var mainTask = new Task(() =>
				{
                    downloadPage.load();
				});
				mainTask.Start();
				await mainTask; // asynchronouly wait page to load

                // finish loading, show page
                try
                {
				    downloadPage.Show();
                }
                catch(System.ObjectDisposedException){}
			}
		}

		private void magicNumber_MouseDown(object sender, MouseEventArgs e)
		{
			if (!selected) magicNumber.SelectAll();
			else magicNumber.DeselectAll();
			selected = !selected;
		}
	}
}
