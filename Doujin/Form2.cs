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
    public partial class Form2 : Form
    {
        private string magicNumber = "";
        private int length = 0;

        public Form2(string num)
        {
            InitializeComponent();
            magicNumber = num;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            //doujin page//
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

            //doujin title//
            int titleStart = page.IndexOf("<h2>") + "<h2>".Length;
            int titleLength = page.IndexOf("</h2>") - titleStart;
            string title = page.Substring(titleStart, titleLength);
            saveFileDialog1.FileName = title;
            infoTitle.Text = title;

            //doujin length//
            int lengthStart = page.IndexOf("<div>") + "<div>".Length;
            int lengthLength = page.IndexOf(" pages</div>") - lengthStart;
            length = int.Parse(page.Substring(lengthStart, lengthLength));
            infoPage.Text = length.ToString() + " pages";

            //doujin image//
            page = page.Substring(page.IndexOf("<meta itemprop=\"image\" content=\"") + "<meta itemprop=\"image\" content=\"".Length);
            page = page.Substring(0, page.IndexOf("\""));
            myRequest = WebRequest.Create(@page);
            myRequest.Timeout = 10000;
            myRequest.Method = "GET";
            pictureBox1.Image = Image.FromStream(myRequest.GetResponse().GetResponseStream());
        }

        private void download_Click(object sender, EventArgs e)
        {
            //create folder//
            string path = "";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                path = saveFileDialog1.FileName;
                Directory.CreateDirectory(path);
            }
            else return;

            //download//
            progressBar1.Maximum = length;
            WebRequest myRequest;
            Random ran = new Random(int.Parse(magicNumber));
            string page = "";
            for (int i = 1; i <= length; i++)
            {
                Thread.Sleep(ran.Next(500, 1000));// !! DOWNLOAD DELAY !! //
                try
                {
                    myRequest = WebRequest.Create(@"https://nhentai.net/g/" + magicNumber + "/" + i.ToString() + "/");
                    myRequest.Timeout = 10000;
                    myRequest.Method = "GET";
                    page = new StreamReader(myRequest.GetResponse().GetResponseStream()).ReadToEnd();
                }
                catch (WebException) { break; }

                page = page.Substring(page.LastIndexOf("<img src=\"") + "<img src = ".Length - 1);
                page = page.Substring(0, page.IndexOf("\""));

                WebClient mywebclient = new WebClient();
                mywebclient.DownloadFile(page, path + "\\" + i.ToString() + ".jpg");
                progressBar1.Value = i;
            }
            MessageBox.Show("Download finished", "Notaion", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            Close();
        }
    }
}
