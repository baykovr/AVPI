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
    public partial class frmProfile : Form
    {
        VI_Profile profile;
        public frmProfile(VI_Profile profile)
        {
            InitializeComponent();
            this.profile = profile;

        }
    }
}
