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
		private bool selected = false;
        static private string[] illegaCharacters = { "*", "|", "\\", ":", "\"", "<", ">", "?", "/" };


        public Form1()
		{
			InitializeComponent();
			magicNumTextBox.Focus();
            pathTextBox.Text = @"C:\Users\eric2\Desktop\動漫\backup";
        }

		private async void magicNumber_KeyPress(object sender, KeyPressEventArgs e)
		{

        }

		private void magicNumber_MouseDown(object sender, MouseEventArgs e)
		{
			if (!selected) magicNumTextBox.SelectAll();
			else magicNumTextBox.DeselectAll();
			selected = !selected;
		}

        private async void downloadButton_Click(object sender, EventArgs e)
		{
            downloadButton.Enabled = false;

            Task task = new Task(() =>
            {
                downloadDoujin();
            });
            task.Start();
            await task;
        }

		private async void downloadDoujin()
		{
			Random ran = new Random(int.Parse(magicNumTextBox.Text));

            WebClient mywebclient = new WebClient();
            for (int i = int.Parse(magicNumTextBox.Text); ; i++)
            {
                var loliPage = WebRequest.Create(@"https://nhentai.net/tag/lolicon/?page=" + i.ToString());
                loliPage.Timeout = 10000;
                loliPage.Method = "GET";
                var loliHentai = new StreamReader(loliPage.GetResponse().GetResponseStream()).ReadToEnd();
                string magicNum = "";

                // find loli hentais in this page
                for (int j = 0; j < 25; i++)
                {
                    loliHentai = loliHentai.Substring(loliHentai.IndexOf("<a href=\"/g/") + "<a href=\"/g/".Length);
                    magicNum = loliHentai.Substring(0, loliHentai.IndexOf("/"));

                    // goto the loli hentai
                    WebRequest doujinPageRequest;
                    string hentaiPage = "";
                    try
                    {
                        doujinPageRequest = WebRequest.Create(@"https://nhentai.net/g/" + magicNumTextBox.Text + '/');
                        doujinPageRequest.Timeout = 10000;
                        doujinPageRequest.Method = "GET";
                        hentaiPage = new StreamReader(doujinPageRequest.GetResponse().GetResponseStream()).ReadToEnd();
                    }
                    catch
                    {
                        MessageBox.Show(
                            "Cannot find page" + magicNum,
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    magicNumTextBox.Invoke((Action)(() =>
                    {
                        magicNumTextBox.Text = magicNum;
                    }));

                    #region Hentai Infomation
                    //
                    // hentai length
                    //
                    int lengthStart = hentaiPage.IndexOf("<div>") + "<div>".Length;
					int lengthLength = hentaiPage.IndexOf(" pages</div>") - lengthStart;
					int len = int.Parse(hentaiPage.Substring(lengthStart, lengthLength));
                    //
                    // hentai title
                    //
                    int titleStart = hentaiPage.IndexOf("<h2>") + "<h2>".Length;
                    int titleLength = hentaiPage.IndexOf("</h2>") - titleStart;
                    string hentaiTitle = hentaiPage.Substring(titleStart, titleLength);
                    //
                    // hentai gallary number
                    //
                    string gNum = "";
                    try
                    {
                        WebRequest doujinPage;
                        doujinPage = WebRequest.Create(@"https://nhentai.net/g/" + magicNum + "/1/");
                        doujinPage.Timeout = 10000;
                        doujinPage.Method = "GET";
                        gNum = new StreamReader(doujinPage.GetResponse().GetResponseStream()).ReadToEnd();
                    }
                    catch (WebException)
                    {
                        MessageBox.Show("We have problem downloading " + magicNum,
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    gNum = gNum.Substring(gNum.IndexOf("<img src=\"https://i.nhentai.net/galleries/") + "<img src=\"https://i.nhentai.net/galleries/".Length);
                    gNum = gNum.Substring(0, gNum.IndexOf("1.jpg"));

                    #endregion

                    // create download folder
                    try
                    {
                        Directory.CreateDirectory(pathTextBox.Text + "\\" + hentaiTitle);
                    }
                    catch (System.ArgumentException)
                    {
                        foreach (string illegal in illegaCharacters)
                        {
                            hentaiTitle = hentaiTitle.Replace(illegal, string.Empty);
                        };
                        Directory.CreateDirectory(pathTextBox.Text + "\\" + hentaiTitle);
                    }

                    // download
                    magicNumTextBox.Invoke((Action)(() =>
                    {
                        magicNumTextBox.Text = len.ToString();
                    }));
                    for (int k = 1; k <= len; k++)
                    {
                        Thread.Sleep(ran.Next(500, 700));

                        Task task = new Task(() =>
                        {
                            try
                            {
                                mywebclient.DownloadFile(@"https://i.nhentai.net/galleries/" + gNum + "/" + k.ToString() + ".jpg",
                                    pathTextBox.Text + "\\" + hentaiTitle + "\\" + k.ToString() + ".jpg");
                            }
                            catch (WebException)
                            {
                                k = len + 1;
                                /*
                                var result = MessageBox.Show("We have problem downloading " + magicNum + ", page" + k.ToString(),
                                    "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);

                                // retry?
                                if (result == DialogResult.Retry)
                                {
                                    --k;
                                }
                                else return;
                                */
                            }
                        });
                        task.Start();
                        await task;
                        
                    }
                }
            
            } 

		}

    }
}
