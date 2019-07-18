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
	public partial class Form1 : Form
	{
        private Image doujinCover;
		private string doujinTitle = "";
		private int doujinLen = 0;
		private CommonOpenFileDialog folderSelectDialog;
		private bool selected = false;

		public class DownloadTask
		{
			public Task downloadTask;
			public CancellationTokenSource cts;
			public int doujinLen;
			public string magicNum;
			public string path;
			public TaskUI taskUI;
		}

		private LinkedList<DownloadTask> downloadTaskManager = new LinkedList<DownloadTask>();
		//private LinkedList<Task> downloadTaskManager = new LinkedList<Task>();

		public Form1()
		{
			InitializeComponent();
			magicNumTextBox.Focus();
			FormBorderStyle = FormBorderStyle.FixedDialog;
			MaximizeBox = false;
            MinimizeBox = true;

			folderSelectDialog = new CommonOpenFileDialog();
			folderSelectDialog.IsFolderPicker = true;
		}

		private async void magicNumber_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
				e.Handled = true;
			if (e.KeyChar == (char)13)
			{
				selected = true;
				magicNumTextBox.SelectAll();

                // set up task
			    var mainTask = new Task(() =>
				{
					load();
				});
				mainTask.Start();
				await mainTask; // asynchronouly wait page to load
			}
		}

		private void magicNumber_MouseDown(object sender, MouseEventArgs e)
		{
			if (!selected) magicNumTextBox.SelectAll();
			else magicNumTextBox.DeselectAll();
			selected = !selected;
		}

		public void load()
		{
			// get doujin
			WebRequest doujinPageRequest;
			string doujinPage = "";
			try
			{
				doujinPageRequest = WebRequest.Create(@"https://nhentai.net/g/" + magicNumTextBox.Text + '/');
				doujinPageRequest.Timeout = 10000;
				doujinPageRequest.Method = "GET";
				doujinPage = new StreamReader(doujinPageRequest.GetResponse().GetResponseStream()).ReadToEnd();
			}
			catch
			{
				MessageBox.Show(
					"Cannot find page.\nPlease check your Internet connection and make sure the number you enter is valid.",
					"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			try
			{
				doujinInfoPanel.Invoke((Action)(() =>
				{
					// doujin title
					int titleStart = doujinPage.IndexOf("<h2>") + "<h2>".Length;
					int titleLength = doujinPage.IndexOf("</h2>") - titleStart;
					doujinTitle = doujinPage.Substring(titleStart, titleLength);
					doujinTitleLabel.Text = doujinTitle;

					// doujin length
					int lengthStart = doujinPage.IndexOf("<div>") + "<div>".Length;
					int lengthLength = doujinPage.IndexOf(" pages</div>") - lengthStart;
					doujinLen = int.Parse(doujinPage.Substring(lengthStart, lengthLength));
					doujinLenLabel.Text = doujinLen.ToString() + " pages";

					// doujin cover
					doujinPage = doujinPage.Substring(doujinPage.IndexOf("<meta itemprop=\"image\" content=\"") + "<meta itemprop=\"image\" content=\"".Length);
					doujinPage = doujinPage.Substring(0, doujinPage.IndexOf("\""));
					doujinPageRequest = WebRequest.Create(doujinPage);
					doujinPageRequest.Timeout = 10000;
					doujinPageRequest.Method = "GET";
                    doujinCover = Image.FromStream(doujinPageRequest.GetResponse().GetResponseStream());
                    doujinCoverPic.Image = doujinCover;

                    downloadButton.Enabled = true;
				}
				));
			}
			catch (InvalidOperationException)
			{
				// window closed before task cancel,
				// causing progressBar1 to be a invalid object to operate.
				// This exception is inevitable but not a critical issue,
				// just let the exception go, task will be canceled soon.
			}
		}

		private async void downloadButton_Click(object sender, EventArgs e)
		{
			// check if task alreasdy existed
			foreach (DownloadTask temp in downloadTaskManager)
			{
				if (temp.magicNum == magicNumTextBox.Text)
				{
					MessageBox.Show(temp.magicNum + " is already waiting to download", "Download Already Existed",
											MessageBoxButtons.OK, MessageBoxIcon.Information);
					return;
				}
			}

			// create folder
			string path = "";

			if (folderSelectDialog.ShowDialog() != CommonFileDialogResult.Ok) return;
			path = folderSelectDialog.FileName + "\\" + doujinTitle;
			if (Directory.Exists(path))
			{
				var result = MessageBox.Show("Folder already existed!\nDo you want to replace the folder?", "Folder Already Exist!",
											 MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
				if (result == DialogResult.No) return;
			}
			else Directory.CreateDirectory(path);

			// disable download button
			//downloadButton.Enabled = false;

			DownloadTask dt = newDownloadTask(path);
			downloadTaskManager.AddLast(dt);
			if (downloadTaskManager.Count == 1)
			{
				dt.downloadTask.Start();
				await dt.downloadTask;
			}

		}

		private async void downloadDoujin()
		{
			DownloadTask dt = downloadTaskManager.ElementAt(0);
			WebRequest doujinPage;
			Random ran = new Random(int.Parse(magicNumTextBox.Text));
			string imageLink = "";
			for (int page = 1; page <= dt.doujinLen; page++)
			{
				// if task is ordered to cancel
				if (dt.cts.IsCancellationRequested) break;

				//download delay. DO NOT REMOVE!!
				Thread.Sleep(ran.Next(500, 1000));

				// go to the hentai page
				try
				{
					doujinPage = WebRequest.Create(@"https://nhentai.net/g/" + dt.magicNum + "/" + page.ToString());
					doujinPage.Timeout = 10000;
					doujinPage.Method = "GET";
					imageLink = new StreamReader(doujinPage.GetResponse().GetResponseStream()).ReadToEnd();
				}
				catch (WebException) { break; }

				// find the image link
				imageLink = imageLink.Substring(imageLink.LastIndexOf("<img src=\"") + "<img src = ".Length - 1);
				imageLink = imageLink.Substring(0, imageLink.IndexOf("\""));

				// download image
				try
				{
					WebClient mywebclient = new WebClient();
					mywebclient.DownloadFile(imageLink, dt.path + "\\" + page.ToString() + ".jpg");

					// if task is ordered to cancel
					if (dt.cts.IsCancellationRequested) break;

					dt.taskUI.Invoke((Action)(() =>
					{
						dt.taskUI.setProgress(page.ToString() + "/" + dt.doujinLen.ToString());
					}));

				}
				catch (WebException)
				{
					var result = MessageBox.Show("We have problem downloading " + dt.magicNum + ", page" + page.ToString(),
						"Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);

					// retry?
					if (result == DialogResult.Retry)
					{
						--page;
						continue;
					}
					else return;
				}
			}

			this.Invoke((Action)(() =>
			{
				this.Controls.Remove(dt.taskUI);
			}));
			downloadTaskManager.RemoveFirst();

			// move all download tasks above
			for(int i = 0; i < downloadTaskManager.Count; ++i)
			{
				var temp = downloadTaskManager.ElementAt(i).taskUI;
				try
				{
					temp.Invoke((Action)(() =>
					{
						temp.shift(temp.Location, new Point(270, 25 + 25 * i));
					}));
				}
				catch { };
			}

			// download next doujin
			if (downloadTaskManager.Count > 0)
			{
				downloadTaskManager.ElementAt(0).downloadTask.Start();
				await downloadTaskManager.ElementAt(0).downloadTask;
			}
		}

		private DownloadTask newDownloadTask(string path)
		{
			DownloadTask dt = new DownloadTask();

			// download task
			dt.downloadTask = new Task(() =>
			{
				downloadDoujin();
			});

			// magic number
			dt.magicNum = magicNumTextBox.Text;

			// doujin length
			dt.doujinLen = doujinLen;

			// download path
			dt.path = path;

			// set this task cancellable
			CancellationTokenSource tokenSource = new CancellationTokenSource();
			dt.cts = tokenSource;

			// task ui
			TaskUI taskui = new TaskUI();
			//taskui.Location = new Point(270, 25 + 25 * downloadTaskManager.Count);
			Point bar = new Point(270, 25 + 25 * downloadTaskManager.Count);
			taskui.shift(new Point(bar.X, bar.Y + 25), bar);
			taskui.setTitle(doujinTitle);
			taskui.setCancelEvent((sen, eve) =>
			{
				tokenSource.Cancel();
				taskui.setProgress("canceled");
				taskui.setProgressColor(Color.Gray);
			});
			dt.taskUI = taskui;
			this.Controls.Add(taskui);

			return dt;
		}
    }
}
