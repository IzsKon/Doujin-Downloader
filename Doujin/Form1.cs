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
        private CommonOpenFileDialog folderSelectDialog;
        private CancellationTokenSource tokenSource;

        private bool selected = false;
        private LinkedList<Task> downloadTasks = new LinkedList<Task>();

        public Form1()
        {
            InitializeComponent();
            magicNumTextBox.Focus();

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
                // doujin title
                doujinTitleLabel.Invoke((Action)(() =>
                {
                    int titleStart = doujinPage.IndexOf("<h2>") + "<h2>".Length;
                    int titleLength = doujinPage.IndexOf("</h2>") - titleStart;
                    doujinTitle = doujinPage.Substring(titleStart, titleLength);
                    doujinTitleLabel.Text = doujinTitle;
                }));

                // doujin length
                doujinLenLabel.Invoke((Action)(() =>
                {
                    int lengthStart = doujinPage.IndexOf("<div>") + "<div>".Length;
                    int lengthLength = doujinPage.IndexOf(" pages</div>") - lengthStart;
                    doujinLen = int.Parse(doujinPage.Substring(lengthStart, lengthLength));
                    doujinLenLabel.Text = doujinLen.ToString() + " pages";
                }));

                // doujin cover
                doujinCoverPic.Invoke((Action)(() =>
                {
                    doujinPage = doujinPage.Substring(doujinPage.IndexOf("<meta itemprop=\"image\" content=\"") + "<meta itemprop=\"image\" content=\"".Length);
                    doujinPage = doujinPage.Substring(0, doujinPage.IndexOf("\""));
                    doujinPageRequest = WebRequest.Create(doujinPage);
                    doujinPageRequest.Timeout = 10000;
                    doujinPageRequest.Method = "GET";
                    doujinCoverPic.Image = Image.FromStream(doujinPageRequest.GetResponse().GetResponseStream());
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

        private async void downloadButton_Click(object sender, EventArgs e)
        {
            // create folder
            string path = "";
            if (folderSelectDialog.ShowDialog() != CommonFileDialogResult.Ok) return;
            path = folderSelectDialog.FileName + "\\" + doujinTitle;
            System.Diagnostics.Debug.WriteLine(path);
            if (Directory.Exists(path))
            {
                var result = MessageBox.Show("Folder already exist!\nReplace Folder?", "Folder already exist!",
                                             MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (result == DialogResult.No) return;
            }
            else Directory.CreateDirectory(path);

            // disable download button
            //downloadButton.Enabled = false;

            // emit start event
            //startDownloadEvent?.Invoke(null, null);

            // set this task cancellable
            tokenSource = new CancellationTokenSource();
            Task downloadTask = new Task(() =>
            {
                downloadDoujin(path, tokenSource.Token, magicNumTextBox.Text);
            });
            downloadTasks.AddLast(downloadTask);
            if(downloadTasks.Count == 1)
            {
                downloadTask.Start();
                await downloadTask;
            }

            // canceled?
            if (tokenSource.IsCancellationRequested)
                MessageBox.Show("Download canceled", "Notation", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        private async void downloadDoujin(string path, CancellationToken cts, string magicNum)
        {
            WebRequest hentaiPage;
            Random ran = new Random(int.Parse(magicNumTextBox.Text));
            string imageLink = "";
            for (int i = 1; i <= doujinLen; i++)
            {
                // if task is ordered to cancel
                if (cts.IsCancellationRequested) return;

                //download delay. DO NOT REMOVE!!
                Thread.Sleep(ran.Next(500, 1000));

                // go to the hentai page
                try
                {
                    hentaiPage = WebRequest.Create(@"https://nhentai.net/g/" + magicNum + "/" + i.ToString());
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
                    var result = MessageBox.Show("We have problem downloading " + magicNum + ", page" + i.ToString(),
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
            downloadTasks.RemoveFirst();

            // download next doujin
            if(downloadTasks.Count >= 0)
            {
                downloadTasks.ElementAt(0).Start();
                await downloadTasks.ElementAt(0);
            }
        }

    }
}
