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
        static private CommonOpenFileDialog folderSelectDialog;
        public string path { get; }

        public DownloadDialog(string title)
        {
            InitializeComponent();

            FormBorderStyle = FormBorderStyle.FixedDialog;

            folderSelectDialog = new CommonOpenFileDialog();
            folderSelectDialog.IsFolderPicker = true;
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
            folderSelectDialog.DefaultDirectory = filename; // NOT WORKING AS EXPECTED
            filenameToolTip.SetToolTip(filenameButton, filename);

            okBtn.Focus(); // NOT WORKING AS EXPECTED
        }

        private void okBtn_Click(object sender, EventArgs e)
        {
            string path = filenameButton.Text + "\\" + titleTextBox.Text;
            if (Directory.Exists(path))
            {
                var result = MessageBox.Show("File already existed!\nDo you want to replace the file?", "file Already Exist!",
                                             MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.No) return;
            }
            else Directory.CreateDirectory(path);

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void filnameButton_Click(object sender, EventArgs e)
        {
            if (folderSelectDialog.ShowDialog() != CommonFileDialogResult.Ok) return;
            string filename = folderSelectDialog.FileName;
            filenameButton.Text = filename;
            filenameToolTip.SetToolTip(filenameButton, filename);
            using (StreamWriter sw = File.CreateText("path"))
            {
                sw.WriteLine(filename);
            }
        }

    }
}
