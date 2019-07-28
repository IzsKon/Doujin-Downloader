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
        private string path = "";
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

            if (folderSelectDialog.ShowDialog() != CommonFileDialogResult.Ok) return;
            pathButton.Text = folderSelectDialog.FileName;
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
                    // progress bar
                    //
                    progressBar.Maximum = doujinLen;
                    progressBar.Value = 0;
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
            pathButton.Enabled = false;

            var mainTask = new Task(() =>
            {
                for (int i = int.Parse(startTextBox.Text); i <= int.Parse(endTextBox.Text); i++)
                {
                    try
                    {
                        pageLabel.Invoke((Action)(() =>
                        {
                            pageLabel.Text = "now at page " + i.ToString();
                        }));
                    }
                    catch { }
             
                    addTask(i.ToString());
                }
            });
            mainTask.Start();
            await mainTask; // asynchronouly wait page to load

            MessageBox.Show("Donwload Complete", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        private void downloadDoujin()
        {
            WebRequest doujinPage;
            Random ran = new Random(int.Parse(magicNumTextBox.Text));

            // go to the hentai page
            string imageNum = "";
            try
            {
                doujinPage = WebRequest.Create(@"https://nhentai.net/g/" + magicNumber + "/1/");
                doujinPage.Timeout = 10000;
                doujinPage.Method = "GET";
                imageNum = new StreamReader(doujinPage.GetResponse().GetResponseStream()).ReadToEnd();
            }
            catch (WebException)
            {
                MessageBox.Show("We have problem downloading " + magicNumber,
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // find the image galleries number
            imageNum = imageNum.Substring(imageNum.IndexOf("<img src=\"https://i.nhentai.net/galleries/") + "<img src=\"https://i.nhentai.net/galleries/".Length);
            imageNum = imageNum.Substring(0, imageNum.IndexOf("/"));

            WebClient mywebclient = new WebClient();
            for (int page = 1; page <= doujinLen; page++)
            {
                //download delay. DO NOT REMOVE!!
                Thread.Sleep(ran.Next(500, 700));

                // download image
                try
                {
                    mywebclient.DownloadFile(@"https://i.nhentai.net/galleries/" + imageNum + "/" + page.ToString() + ".jpg",
                        path + "\\" + page.ToString() + ".jpg");
                }
                catch (WebException)
                {
                    try // the image may be png
                    {
                        mywebclient.DownloadFile(@"https://i.nhentai.net/galleries/" + imageNum + "/" + page.ToString() + ".png",
                             path + "\\" + page.ToString() + ".png");
                    }
                    catch (WebException)
                    {
                        using (StreamWriter sw = File.AppendText(pathButton.Text + "\\error"))
                        {
                            sw.WriteLine(magicNumber + ", " + page.ToString());
                        }
                        break;
                    }

                }

                try
                {
                    progressBar.Invoke((Action)(() =>
                    {
                        progressBar.Value = page;
                    }));
                }
                catch { }
                
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
            //taskui.Location = new Point(270, 25 + 25 * downloadTaskManager.Count);
            Point bar = new Point(270, 25 + 25 * downloadTaskManager.Count);

            try
            {
                taskui.Invoke((Action)(() =>
                {
                    taskui.shift(new Point(bar.X, bar.Y + 25), bar);
                    taskui.setTitle(doujinTitle);
                    taskui.setCancelEvent((sen, eve) =>
                    {
                        tokenSource.Cancel();
                        taskui.setProgress("canceled");
                        taskui.setProgressColor(Color.Gray);
                    });
                }));
            }
            catch { };
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
                magicNumber = loliHentai.Substring(0, loliHentai.IndexOf("/"));
                try
                {
                    magicNumTextBox.Invoke((Action)(() =>
                    {
                        magicNumTextBox.Text = magicNumber;
                    }));
                }
                catch
                {
                    // TODO
                }

                // create folder
                path = pathButton.Text + "\\" + magicNumber;
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                // load page
                load();
                downloadDoujin();

            }

        }

        private void pathButton_Click(object sender, EventArgs e)
        {
            if (folderSelectDialog.ShowDialog() != CommonFileDialogResult.Ok) return;
            pathButton.Text = folderSelectDialog.FileName;
        }

        private void startTextBox_TextChanged(object sender, EventArgs e)
        {
            pageLabel.Text = "now at page " + startTextBox.Text;
        }

        private void numOnly_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;
        }
    }
}