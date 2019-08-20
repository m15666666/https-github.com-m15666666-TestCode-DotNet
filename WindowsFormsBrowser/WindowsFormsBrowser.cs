using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class WindowsFormsBrowser : Form
    {
        public WindowsFormsBrowser()
        {
            InitializeComponent();
            textBox1.KeyUp += TextBox1_KeyUp;
        }

        protected override void OnLoad(EventArgs e)
        {
            WindowState = FormWindowState.Maximized;
            base.OnLoad(e);
        }

        private void TextBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                Go(textBox1.Text);
            }
        }

        private void switchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //webBrowser1.Url = new Uri("http://www.163.com");
            Go(textBox1.Text);
            //webBrowser1.Document.
        }

        private void Go(string url)
        {
            if (string.IsNullOrEmpty(url)) return;
            try
            {
                webBrowser1.Navigate(url);
            }
            catch (Exception)
            {

            }
        }
    }
}
