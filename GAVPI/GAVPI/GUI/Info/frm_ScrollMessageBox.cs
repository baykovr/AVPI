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
    public partial class frm_ScrollMessageBox : Form
    {
        public frm_ScrollMessageBox( List<string> info)
        {
            InitializeComponent();
            lstboxInfo.DataSource = info;
        }
    }
}
