using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Doujin
{
    public partial class DownloadDialog : Form
    {
        static private CommonOpenFileDialog folderSelectDialog = new CommonOpenFileDialog();
        static private string[] illegaCharacters = { "*", "|", "\\", ":", "\"", "<", ">", "?", "/" };
        public string path { get; set; }

        public DownloadDialog(string title)
        {
            InitializeComponent();

            FormBorderStyle = FormBorderStyle.FixedDialog;

            folderSelectDialog.IsFolderPicker = true;
            foreach (string illegal in illegaCharacters)
            {
                title = title.Replace(illegal, string.Empty);
            }
            titleTextBox.Text = title;

            string filename = "";
            if (!File.Exists("path"))
            {
                filename = "C:\\";
                using (StreamWriter sw = File.CreateText("path"))
                {
                    sw.WriteLine(filename);
                }
            }
            else
            {
                using (StreamReader sr = File.OpenText("path"))
                {
                    filename = sr.ReadLine();
                }
            }
            filenameButton.Text = filename;
            folderSelectDialog.InitialDirectory = filename;
            filenameToolTip.SetToolTip(filenameButton, filename);

            okBtn.Select();
        }

        private void okBtn_Click(object sender, EventArgs e)
        {
            path = filenameButton.Text + "\\" + titleTextBox.Text;
            if (Directory.Exists(path))
            {
                var result = MessageBox.Show("File already existed!\nDo you want to replace the file?", "file Already Exist!",
                                             MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.No) return;
            }
            else
            {
                try
                {
                    Directory.CreateDirectory(path);
                }
                catch(System.ArgumentException)
                {
                    MessageBox.Show("Illegal characters in path", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void filnameButton_Click(object sender, EventArgs e)
        {
            okBtn.Select();
            if (folderSelectDialog.ShowDialog() != CommonFileDialogResult.Ok) return;
            string filename = folderSelectDialog.FileName;
            filenameButton.Text = filename;
            folderSelectDialog.InitialDirectory = filename;
            filenameToolTip.SetToolTip(filenameButton, filename);
            using (StreamWriter sw = File.CreateText("path"))
            {
                sw.WriteLine(filename);
            }
        }

        private void titleTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            foreach (string illegal in illegaCharacters)
            {
                if (e.KeyChar.ToString().Equals(illegal))
                {
                    illegalCharToolTip.Show("* | \\ : \" < > ? /", titleTextBox, 10000); // DO NOT WORK AS EXPECTED
                    e.Handled = true;
                }
            }
        }

    }
}
