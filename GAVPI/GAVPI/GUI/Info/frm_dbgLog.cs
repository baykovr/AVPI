using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GAVPI.GUI.Info
{
    public partial class frm_dbgLog : Form
    {
        List<string> log;
        public frm_dbgLog( List<string> log)
        {
            InitializeComponent();
            this.log = log;
            lstboxInfo.DataSource = log;
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string clipboard_data = String.Join("\r\n", log.ToArray());
            Clipboard.SetData(System.Windows.Forms.DataFormats.Text,
                clipboard_data);
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.log.Clear();
            lstboxInfo.DataSource = null;
            lstboxInfo.DataSource = log;
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lstboxInfo.DataSource = null;
            lstboxInfo.DataSource = log;
        }
    }
}
