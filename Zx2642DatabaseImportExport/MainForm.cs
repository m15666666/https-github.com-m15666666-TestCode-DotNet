using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Zx2642DatabaseImportExport
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void mnu_ExportImportDBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Form1().Show();
        }

        private void mnu_ChangeMObjectTree_Click(object sender, EventArgs e)
        {
            new ChangeMObjectTreeForm().Show();
        }
    }
}
