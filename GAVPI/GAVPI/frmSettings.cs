using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GAVPI
{
    public partial class frmSettings : Form
    {
        VI_Settings vi_settings;
        public frmSettings(VI_Settings vi_settings)
        {
            InitializeComponent();
            MaximizeBox = false;
            this.vi_settings = vi_settings;
        }
    }
}
