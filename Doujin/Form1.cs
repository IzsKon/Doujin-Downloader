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
    public partial class Form1 : Form
    {
        private bool selected = false;

        public Form1()
        {
            InitializeComponent();
            magicNumber.Focus();
        }

        private void magicNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;
            if (e.KeyChar == (char)13)
            {
                selected = true;
                magicNumber.SelectAll();
                Form2 downloadPage = new Form2(magicNumber.Text);
                downloadPage.Show();
            }
        }

        private void magicNumber_MouseDown(object sender, MouseEventArgs e)
        {
            if (!selected) magicNumber.SelectAll();
            else magicNumber.DeselectAll();
            selected = !selected;
        }
    }
}
