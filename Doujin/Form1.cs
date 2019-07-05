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
        private string doujinTitle = "";
        private int doujinLen = 0;
        private int buttonNum = 0;
        private CommonOpenFileDialog folderSelectDialog;
        private bool selected = false;

        public class DownloadInfo
        {
            public CancellationTokenSource cts;
            public int doujinLen;
            public string magicNum;
            public string path;
            public Label doujinTitle;
            public Label pageProgress;
            public Button cancelButton;
        }

        private LinkedList<Task> downloadTaskManager = new LinkedList<Task>();

        public Form1()
        {
            InitializeComponent();
            magicNumTextBox.Focus();
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;

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
                    "Cannot find page.\nPlease check your Internet and make sure the number you enter is valid.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error );
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
                    doujinCoverPic.Image = Image.FromStream(doujinPageRequest.GetResponse().GetResponseStream());
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
            // create folder
            string path = "";

            if (folderSelectDialog.ShowDialog() != CommonFileDialogResult.Ok) return;
            path = folderSelectDialog.FileName + "\\" + doujinTitle;
            if (Directory.Exists(path))
            {
                var result = MessageBox.Show("Folder already exist!\nReplace Folder?", "Folder already exist!",
                                             MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (result == DialogResult.No) return;
            }
            else Directory.CreateDirectory(path);

            // disable download button
            //downloadButton.Enabled = false;

            buttonNum++;

            DownloadInfo di = new DownloadInfo();
            di.magicNum = magicNumTextBox.Text;
            di.doujinLen = doujinLen;
            di.path = path;

            // set this task cancellable
            CancellationTokenSource tokenSource = new CancellationTokenSource();
            di.cts = tokenSource;

            Label title = new Label();
            this.Controls.Add(title);
            // title.Location = new Point(426, 16 + 25 * downloadTaskManager.Count);
            // title.Size = new Size(416, 23);
            title.Location = new Point(270, 25 * buttonNum);
            title.Size = new Size(350, 23);
            title.ForeColor = Color.White;
            title.BackColor = Color.Gray;
            title.Text = doujinTitle;
            title.TextAlign = ContentAlignment.MiddleLeft;
            di.doujinTitle = title;

            Label progress = new Label();
            this.Controls.Add(progress);
            // page.Location = new Point(848, 16 + 25 * downloadTaskManager.Count);
            // page.Size = new Size(75, 23);
            progress.Location = new Point(620, 25 * buttonNum);
            progress.Size = new Size(55, 23);
            progress.Text = "waiting...";
            progress.TextAlign = ContentAlignment.MiddleCenter;
            progress.ForeColor = Color.White;
            di.pageProgress = progress;

            Button cancel = new Button();
            this.Controls.Add(cancel);
            // cancel.Location = new Point(919, 16 + 25 * downloadTaskManager.Count);
            // cancel.Size = new Size(23, 23);
            cancel.Location = new Point(675, 25 * buttonNum);
            cancel.Size = new Size(23, 23);
            cancel.TabStop = false;
            cancel.FlatStyle = FlatStyle.Flat;
            cancel.FlatAppearance.BorderSize = 0;
            cancel.Text = "X";
            cancel.ForeColor = Color.White;
            cancel.BackColor = Color.DarkRed;
            cancel.Click += (sen, eve) =>
            {
                tokenSource.Cancel();
                progress.Text = "cancled";
                progress.ForeColor = Color.Gray;
            };
            di.cancelButton = cancel;

            Task downloadTask = new Task(() =>
            {
                downloadDoujin(di);
            });

            downloadTaskManager.AddLast(downloadTask);
            if (downloadTaskManager.Count == 1)
            {
                downloadTask.Start();
                await downloadTask;
            }

        }

        private async void downloadDoujin(DownloadInfo di)
        {
            WebRequest doujinPage;
            Random ran = new Random(int.Parse(magicNumTextBox.Text));
            string imageLink = "";
            for (int i = 1; i <= di.doujinLen; i++)
            {
                // if task is ordered to cancel
                if (di.cts.IsCancellationRequested) break;

                //download delay. DO NOT REMOVE!!
                Thread.Sleep(ran.Next(500, 1000));

                // go to the hentai page
                try
                {
                    doujinPage = WebRequest.Create(@"https://nhentai.net/g/" + di.magicNum + "/" + i.ToString());
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
                    mywebclient.DownloadFile(imageLink, di.path + "\\" + i.ToString() + ".jpg");
                    if (di.cts.IsCancellationRequested) break;
                    di.pageProgress.Invoke((Action)(() =>
                    {
                        di.pageProgress.Text = i.ToString() + "/" + di.doujinLen.ToString();
                    }));
                    
                }
                catch (WebException)
                {
                    var result = MessageBox.Show("We have problem downloading " + di.magicNum + ", page" + i.ToString(),
                        "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);

                    // retry?
                    if (result == DialogResult.Retry)
                    {
                        --i;
                        continue;
                    }
                    else return;
                }
            }

            downloadTaskManager.RemoveFirst();

            if (!di.cts.IsCancellationRequested)
            {
                this.Invoke((Action)(() =>
                {
                    this.Controls.Remove(di.doujinTitle);
                    this.Controls.Remove(di.pageProgress);
                    this.Controls.Remove(di.cancelButton);
                }
                ));
            }

            // download next doujin
            if(downloadTaskManager.Count > 0)
            {
                downloadTaskManager.ElementAt(0).Start();
                await downloadTaskManager.ElementAt(0);
            }
        }

    }
}
