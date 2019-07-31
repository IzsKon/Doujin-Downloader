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
using System.Reflection;

namespace Doujin
{
	public partial class Form1 : Form
	{
		private string doujinTitle = "";
		private int doujinLen = 0;

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

		public Form1()
		{
			InitializeComponent();
			magicNumTextBox.Focus();
        }

        private void magicNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;
            if (e.KeyChar == (char)13)
            {
                searchButton_Click(sender, e);
            }
        }

        private async void searchButton_Click(object sender, EventArgs e)
        {
            searchButton.Enabled = false;

            // set up task
            var mainTask = new Task(() =>
            {
                load();
            });
            mainTask.Start();
            await mainTask; // asynchronouly wait page to load

            searchButton.Enabled = true;
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
                // check if Internet connection is noraml
                if (!hasInternet())
                {
                    MessageBox.Show(
                    "Cannot find page.\nPlease check your Internet connection",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show(
                    "Cannot find page.\nPlease make sure the number you enter is valid.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
				return;
			}

			try
			{
				doujinInfoPanel.Invoke((Action)(() =>
				{
                    //
					// doujin title
                    //
					int titleStart = doujinPage.IndexOf("<h2>") + "<h2>".Length;
					int titleLength = doujinPage.IndexOf("</h2>") - titleStart;
					doujinTitle = doujinPage.Substring(titleStart, titleLength);
                    // if doujin only has one title
                    if (doujinTitle.Equals("More Like This"))
                    {
                        titleStart = doujinPage.IndexOf("<h1>") + "<h1>".Length;
                        titleLength = doujinPage.IndexOf("</h1>") - titleStart;
                        doujinTitle = doujinPage.Substring(titleStart, titleLength);
                    }
                    doujinTitle = doujinTitle.Replace("&#39;", "'");  
					doujinTitleLabel.Text = doujinTitle;
                    titleToolTip.SetToolTip(doujinTitleLabel, doujinTitle);
                    //
                    // doujin length
                    //
                    int lengthStart = doujinPage.IndexOf("<div>") + "<div>".Length;
					int lengthLength = doujinPage.IndexOf(" pages</div>") - lengthStart;
					doujinLen = int.Parse(doujinPage.Substring(lengthStart, lengthLength));
					doujinLenLabel.Text = doujinLen.ToString() + " pages";
                    //
					// doujin cover
                    //
					doujinPage = doujinPage.Substring(doujinPage.IndexOf("<meta itemprop=\"image\" content=\"") + "<meta itemprop=\"image\" content=\"".Length);
					doujinPage = doujinPage.Substring(0, doujinPage.IndexOf("\""));
					doujinPageRequest = WebRequest.Create(doujinPage);
					doujinPageRequest.Timeout = 10000;
					doujinPageRequest.Method = "GET";
                    doujinCoverPic.Image = Image.FromStream(doujinPageRequest.GetResponse().GetResponseStream());

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
            catch
            {
                MessageBox.Show("We have problem loading " + magicNumTextBox.Text,
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
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

            // download dialog
            string path = "";
            using (DownloadDialog dd = new DownloadDialog(doujinTitle))
            { 
                if (dd.ShowDialog() != DialogResult.OK) return;
                path = dd.path;
                doujinTitle = dd.title;
            }

            // new download task
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

            // go to the hentai page
            string imageNum = "";
            try
            {
                doujinPage = WebRequest.Create(@"https://nhentai.net/g/" + dt.magicNum + "/1/");
                doujinPage.Timeout = 10000;
                doujinPage.Method = "GET";
                imageNum = new StreamReader(doujinPage.GetResponse().GetResponseStream()).ReadToEnd();
            }
            catch (WebException)
            {
                MessageBox.Show("We have problem downloading " + dt.magicNum,
						"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                // find the image galleries number
                imageNum = imageNum.Substring(imageNum.IndexOf("<img src=\"https://i.nhentai.net/galleries/") + "<img src=\"https://i.nhentai.net/galleries/".Length);
                imageNum = imageNum.Substring(0, imageNum.IndexOf("/"));
            }
            catch
            {
                MessageBox.Show("We have problem downloading " + dt.magicNum, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dt.cts.Cancel();
            }

            WebClient mywebclient = new WebClient();
            for (int page = 1; page <= dt.doujinLen; page++)
			{
				// if task is ordered to cancel
				if (dt.cts.IsCancellationRequested) break;

				//download delay. DO NOT REMOVE!!
				Thread.Sleep(ran.Next(300, 400));

                #region download image
                try
				{
                    mywebclient.DownloadFile(@"https://i.nhentai.net/galleries/" + imageNum + "/" + page.ToString() + ".jpg", 
                        dt.path + "\\" + page.ToString() + ".jpg");    
                    dt.taskUI.Invoke((Action)(() =>
                    {
                        dt.taskUI.setProgress(page.ToString() + "/" + dt.doujinLen.ToString());
                    }));
                }
				catch
				{
                    try // the image may be png
                    {
                        mywebclient.DownloadFile(@"https://i.nhentai.net/galleries/" + imageNum + "/" + page.ToString() + ".png",
                            dt.path + "\\" + page.ToString() + ".png");
                        dt.taskUI.Invoke((Action)(() =>
                        {
                            dt.taskUI.setProgress(page.ToString() + "/" + dt.doujinLen.ToString());
                        }));
                    }
                    catch
                    {
                        // if connection is not working
                        if (!hasInternet())
                        {
                            dt.taskUI.Invoke((Action)(() =>
                            {
                                dt.taskUI.setProgress("waiting");
                            }));
                            --page;
                            continue;
                        }
                        else
                        {
                            var result = MessageBox.Show(
                                "We have problem downloading " + dt.magicNum + ", page" + page.ToString(),
                                "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);

                            // retry?
                            if (result == DialogResult.Retry) --page;
                            else dt.cts.Cancel();
                        }
                    }
				}
                #endregion
            }

            // remove completed task
            try
            {
                dtPanel.Invoke((Action)(() =>
                {
                    dtPanel.Controls.Remove(dt.taskUI);
                }));
            }
            catch (InvalidOperationException) { }
			downloadTaskManager.RemoveFirst();

			// move all download tasks above
			for(int i = 0; i < downloadTaskManager.Count; ++i)
			{
				var temp = downloadTaskManager.ElementAt(i).taskUI;
				try
				{
					temp.Invoke((Action)(() =>
					{
                        temp.shift(new Point(0, 35 * i - dtPanel.VerticalScroll.Value));
                    }));
				}
                catch (InvalidOperationException) { };
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

			dt.magicNum = magicNumTextBox.Text;
			dt.doujinLen = doujinLen;
			dt.path = path;

			// set this task cancellable
			CancellationTokenSource tokenSource = new CancellationTokenSource();
			dt.cts = tokenSource;

			// task ui
			TaskUI taskui = new TaskUI();
            Point bar = new Point(0, 35 * downloadTaskManager.Count - dtPanel.VerticalScroll.Value);
            taskui.Location = new Point(bar.X, bar.Y + 35);
            taskui.shift(bar);
            taskui.setTitle(doujinTitle);
			taskui.setCancelEvent((sen, eve) =>
			{
				tokenSource.Cancel();
				taskui.setProgress("canceled");
				taskui.setProgressColor(Color.Gray);
			});
			dt.taskUI = taskui;
            this.dtPanel.Controls.Add(taskui);

            return dt;
		}

        /// <summary>
        /// return whether the Internet connection is normal or not
        /// </summary>
        /// <returns></returns>
        private bool hasInternet()
        {
            try
            {
                using (var client = new WebClient())
                using (client.OpenRead("http://clients3.google.com/generate_204"))
                { return true; }
            }
            catch { return false; }
        }

    }
}