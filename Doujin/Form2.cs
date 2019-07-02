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
using Microsoft.WindowsAPICodePack.Dialogs;

namespace Doujin
{
	public partial class Form2 : Form
	{
		public event EventHandler finishEvent;
		public event EventHandler startDownloadEvent;
		public Func<bool> checkMaxReached;
		private string magicNumber = "";
		private int length = 0;
		private CancellationTokenSource tokenSource;
		private CommonOpenFileDialog dialog;
		private string title = "";

		public Form2(string num)
		{
			InitializeComponent();
			magicNumber = num;

			dialog = new CommonOpenFileDialog();
			dialog.IsFolderPicker = true;
		}

		public void load()
		{
			// doujin page
			WebRequest myRequest;
			string page = "";
			try
			{
				myRequest = WebRequest.Create(@"https://nhentai.net/g/" + magicNumber + '/');
				myRequest.Timeout = 10000;
				myRequest.Method = "GET";
				page = new StreamReader(myRequest.GetResponse().GetResponseStream()).ReadToEnd();
			}
			catch
			{
				MessageBox.Show("Cannot find page.\nPlease make sure the number you enter is valid.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				Close();
				return;
			}

			// doujin title
			int titleStart = page.IndexOf("<h2>") + "<h2>".Length;
			int titleLength = page.IndexOf("</h2>") - titleStart;
			title = page.Substring(titleStart, titleLength);
			infoTitle.Text = title;

			// doujin length
			int lengthStart = page.IndexOf("<div>") + "<div>".Length;
			int lengthLength = page.IndexOf(" pages</div>") - lengthStart;
			length = int.Parse(page.Substring(lengthStart, lengthLength));
			infoPage.Text = length.ToString() + " pages";

			// doujin image
			page = page.Substring(page.IndexOf("<meta itemprop=\"image\" content=\"") + "<meta itemprop=\"image\" content=\"".Length);
			page = page.Substring(0, page.IndexOf("\""));
			myRequest = WebRequest.Create(@page);
			myRequest.Timeout = 10000;
			myRequest.Method = "GET";
			pictureBox1.Image = Image.FromStream(myRequest.GetResponse().GetResponseStream());
		}

		private void downloadDoujin(string path, CancellationToken cts)
		{
			WebRequest hentaiPage;
			Random ran = new Random(int.Parse(magicNumber));
			string imageLink = "";
			for (int i = 1; i <= length; i++)
			{
                // if task is ordered to cancel
                if (cts.IsCancellationRequested) return;

                //download delay. DO NOT REMOVE!!
                Thread.Sleep(ran.Next(500, 1000));

                // go to the hentai page
                try
				{
					hentaiPage = WebRequest.Create(@"https://nhentai.net/g/" + magicNumber + "/" + i.ToString());
					hentaiPage.Timeout = 10000;
					hentaiPage.Method = "GET";
					imageLink = new StreamReader(hentaiPage.GetResponse().GetResponseStream()).ReadToEnd();
				}
				catch (WebException) { break; }

                // find the image link
				imageLink = imageLink.Substring(imageLink.LastIndexOf("<img src=\"") + "<img src = ".Length - 1);
				imageLink = imageLink.Substring(0, imageLink.IndexOf("\""));

                // download image
                try
                {
                    WebClient mywebclient = new WebClient();
                    mywebclient.DownloadFile(imageLink, path + "\\" + i.ToString() + ".jpg");
                }
                catch (WebException)
                {
                    var result = MessageBox.Show("We have problem downloading " + magicNumber + ", page" + i.ToString(), 
                        "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);

                    // retry?
                    if (result == DialogResult.Retry)
                    {
                        --i;
                        continue;
                    }
                    else return;
                }

				try
				{
					progressBar1.Invoke((Action)(() =>
					{
						progressBar1.Value = i;
					}));
					infoPage.Invoke((Action)(() =>
					{
						infoPage.Text = i + "/" + length + " pages";
					}));
				}
				catch (InvalidOperationException)
				{
					// window closed before task cancel,
					// causing progressBar1 to be a invalid object to operate.
					// This exception is inevitable but not a critical issue,
					// just let the exception go, task will be canceled soon.
				}
			}
		}

		private async void download_Click(object sender, EventArgs e)
		{
			//create folder//
			string path = "";
			if (checkMaxReached())
			{
				MessageBox.Show("You horny swine, stop lewding pictures.\r\nCurrent download count exceeds maximum.",
					"You Sick Pervert!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			if (dialog.ShowDialog() != CommonFileDialogResult.Ok)
				return;

			path = dialog.FileName + "\\" + title;
			System.Diagnostics.Debug.WriteLine(path);
			if (Directory.Exists(path))
			{
				var result = MessageBox.Show("Folder already exist!\nReplace Folder?", "Folder already exist!",
											 MessageBoxButtons.YesNo, MessageBoxIcon.Information);
				if (result == DialogResult.No)
					return;
			}
			else
				Directory.CreateDirectory(path);

			// disable download button
			download.Enabled = false;
			// set up for progressbar
			progressBar1.Maximum = length;

			// emit start event
			startDownloadEvent?.Invoke(null, null);

			// set this task cancellable
			tokenSource = new CancellationTokenSource();
			Task downloadTask = new Task(() =>
			{
				downloadDoujin(path, tokenSource.Token);
			});
			downloadTask.Start();
			await downloadTask;

			// canceled?
			if (tokenSource.IsCancellationRequested)
				MessageBox.Show("Download canceled", "Notation", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

			// finish and close
			Close();
		}

		private void Form2_FormClosing(object sender, FormClosingEventArgs e)
		{
			tokenSource?.Cancel();
			finishEvent?.Invoke(null, null);
		}
	}
}
