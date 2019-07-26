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
        private Image doujinCover;
        private string doujinTitle = "";
        private string magicNumber = "";
        private int doujinLen = 0;
        private bool selected = false;
        static private string[] illegaCharacters = { "*", "|", "\\", ":", "\"", "<", ">", "?", "/" };
        static private CommonOpenFileDialog folderSelectDialog = new CommonOpenFileDialog();


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
                    //
                    // doujin info
                    //
                    int infoStart = doujinPage.IndexOf("<div id=\"info-block\">") + "<div id=\"info-block\">".Length;
                    int infoLength = doujinPage.IndexOf("<div class=\"buttons\">") - infoStart;
                    string info = doujinPage.Substring(infoStart, infoLength);
                    using (StreamWriter sw = File.CreateText(pathButton.Text + "\\" + magicNumTextBox.Text + "\\info"))
                    {
                        sw.WriteLine(info);
                    }
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
                    doujinTitleLabel.Text = doujinTitle;
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
                    doujinCover = Image.FromStream(doujinPageRequest.GetResponse().GetResponseStream());
                    doujinCoverPic.Image = doujinCover;
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
            downloadButton.Enabled = false;

            for(int i = int.Parse(startTextBox.Text); i <= int.Parse(endTextBox.Text); i++)
            {
                var mainTask = new Task(() =>
                {
                    addTask(i.ToString());
                });
                mainTask.Start();
                await mainTask; // asynchronouly wait page to load
            }
            

            //downloadTaskManager.ElementAt(0).downloadTask.Start();
            //await downloadTaskManager.ElementAt(0).downloadTask;

        }

        private async void downloadDoujin(DownloadTask dt)
        {
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

            // find the image galleries number
            imageNum = imageNum.Substring(imageNum.IndexOf("<img src=\"https://i.nhentai.net/galleries/") + "<img src=\"https://i.nhentai.net/galleries/".Length);
            imageNum = imageNum.Substring(0, imageNum.IndexOf("/"));

            WebClient mywebclient = new WebClient();
            for (int page = 1; page <= 3 /* dt.doujinLen */; page++)
            {
                // if task is ordered to cancel
                if (dt.cts.IsCancellationRequested) break;

                //download delay. DO NOT REMOVE!!
                Thread.Sleep(ran.Next(500, 700));

                // download image
                try
                {
                    mywebclient.DownloadFile(@"https://i.nhentai.net/galleries/" + imageNum + "/" + page.ToString() + ".jpg",
                        dt.path + "\\" + page.ToString() + ".jpg");

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

            // remove completed task
            this.Invoke((Action)(() =>
            {
                this.Controls.Remove(dt.taskUI);
            }));
            downloadTaskManager.RemoveFirst();

            // move all download tasks above
            for (int i = 0; i < downloadTaskManager.Count; ++i)
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
                downloadDoujin(dt);
            });

            dt.magicNum = magicNumTextBox.Text;
            dt.doujinLen = doujinLen;
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

            try
            {
                this.Invoke((Action)(() =>
                {
                    this.Controls.Add(taskui);
                }));
            }
            catch { };

            return dt;
        }

        private void addTask(string page)
        {
            WebRequest loliPage = WebRequest.Create(@"https://nhentai.net/tag/lolicon/?page=" + page);
            loliPage.Timeout = 10000;
            loliPage.Method = "GET";
            string loliHentai = new StreamReader(loliPage.GetResponse().GetResponseStream()).ReadToEnd();

            for (int i = 0; i < 25; i++)
            {
                loliHentai = loliHentai.Substring(loliHentai.IndexOf("<a href=\"/g/") + "<a href=\"/g/".Length);
                try
                {
                    magicNumTextBox.Invoke((Action)(() =>
                    {
                        magicNumTextBox.Text = loliHentai.Substring(0, loliHentai.IndexOf("/"));
                    }));
                }
                catch
                {
                    // TODO
                }

                // create folder
                string path = pathButton.Text + "\\" + magicNumTextBox.Text;
                if (!Directory.Exists(path))
                {
                    try
                    {
                        Directory.CreateDirectory(path);
                    }
                    catch (System.ArgumentException)
                    {
                        foreach (string illegal in illegaCharacters)
                        {
                            doujinTitle = doujinTitle.Replace(illegal, string.Empty);
                        }
                        path = pathButton.Text + "\\" + magicNumTextBox.Text;
                        Directory.CreateDirectory(path);
                    }
                }

                // load page
                load();

                // new download task
                DownloadTask dt = newDownloadTask(path);
                downloadTaskManager.AddLast(dt);
                downloadDoujin(dt);


                // if (downloadTaskManager.Count == 1)
                // {
                //     downloadTaskManager.ElementAt(0).downloadTask.Start();
                //     await downloadTaskManager.ElementAt(0).downloadTask;
                // }
            }

        }

        private void pathButton_Click(object sender, EventArgs e)
        {
            if (folderSelectDialog.ShowDialog() != CommonFileDialogResult.Ok) return;
            pathButton.Text = folderSelectDialog.FileName;
        }
    }
}