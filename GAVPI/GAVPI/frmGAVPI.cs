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
    public partial class frmGAVPI : Form
    {
        VI_Settings vi_settings;
        VI_Profile vi_profile;
 
        public frmGAVPI()
        {
            InitializeComponent();
            
            vi_settings = new VI_Settings("settings.gavpi");
            vi_profile  = new VI_Profile("default.gavpi");

            // Here we go.
            VI virtualinterface = new VI(vi_profile, vi_settings);
        }

        #region Profile
        private void modifyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmProfile modProfileFrm = new frmProfile(vi_profile);
            modProfileFrm.ShowDialog();
        }
        #endregion
        #region Settings
        private void mainStripSettings_Click(object sender, EventArgs e)
        {
            frmSettings modSettingsFrm = new frmSettings(vi_settings);
            modSettingsFrm.ShowDialog();
        }
        #endregion

       
    }
}
