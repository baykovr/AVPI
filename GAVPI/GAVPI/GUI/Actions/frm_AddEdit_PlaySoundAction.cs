using NAudio.Wave;
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

namespace GAVPI
{
    public partial class frm_AddEdit_PlaySoundAction : Form
    {
       
        #region Globals
        
        private Action form_action;

        private int times_to_add;

        #endregion

        #region Globals : Fetchers
        
        public Action get_action()
        {
            return form_action;
        }
        
        public int get_times_to_add()
        {
            return times_to_add;
        }
        #endregion

        #region Constructors
        public frm_AddEdit_PlaySoundAction()
        {
            InitializeComponent();

            populate_fields();
        }
        public frm_AddEdit_PlaySoundAction(Action action)
        {
            InitializeComponent();

            this.form_action = action;

            populate_fields();
        }
        #endregion

        #region UI : Populate 
        private void populate_fields()
        {
            //default times to add is one time
            times_to_add = 1;
            txtTimesToAdd.Text = times_to_add.ToString();

            //cbSpeechType.DataSource = Action_Sequence.SpeechAction_Types;
            
            // Fill out the output devices cb.
            // Note, if a device goes missing (unplugged) after population we will just fall back to default.
            cbOutputDevices.Items.Add("Default");
            for (int deviceId = 0; deviceId < WaveOut.DeviceCount; deviceId++)
            {
                var capabilities = WaveOut.GetCapabilities(deviceId);
                cbOutputDevices.Items.Add(capabilities.ProductName);
            }
            cbOutputDevices.SelectedIndex = 0;

            // Editing an existing action
            if (form_action != null)
            {
                chckMultiAdd.Visible = false;
                txtTimesToAdd.Visible = false;
                btnAdd.Text = "Edit";

                int deviceID = ((Play_Sound)form_action).getDeviceID();

                if (cbOutputDevices.Items.Count > deviceID)
                {
                    cbOutputDevices.SelectedIndex = deviceID;
                }
                txtFilePath.Text = form_action.value;
            }
            // New Action
            else
            {
                
                
            }
        }
        #endregion

        #region UI : Press Events : Add/Edit : Cancel : Drop Down
        private void btnAdd_Click(object sender, EventArgs e)
        {
            // Validate times to add
            // Number of Times to Add particular action
            if (Int32.TryParse(txtTimesToAdd.Text, out times_to_add))
            {
                if (times_to_add <= 0)
                {
                    MessageBox.Show("Times to add value cannot be less than or equal to 0.");
                    return;
                }
            }
            else
            {
                MessageBox.Show("Times to add value must be a valid integer greater than one.");
                return;
            }

            // Validate Text
            if (String.IsNullOrWhiteSpace(txtFilePath.Text))
            {
                MessageBox.Show("File path value cannot be blank or null");
                return;
            }
            else if (!File.Exists(txtFilePath.Text))
            {
                MessageBox.Show("File path verification failed, are you sure the path is correct?");
                return;
            }

            Type new_action_type = Type.GetType("GAVPI.Play_Sound");

            // Since we added a "default" entry we need to offset the id's by minus one.
            int deviceID = cbOutputDevices.SelectedIndex - 1;

            form_action = (Action)Activator.CreateInstance(
                new_action_type, txtFilePath.Text, deviceID);

            this.DialogResult = DialogResult.OK;

            this.Close();
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnChooseFile_Click(object sender, EventArgs e)
        {
            string path = null;
            OpenFileDialog sound_file_dialog = new OpenFileDialog();
            sound_file_dialog.Title  = "Choose a sound file to play";
            sound_file_dialog.Filter = "MP3 (*.mp3)|*.mp3|WAV (*.wav)|*.wav|MP4 (*.mp4)|*.mp4|WMV (*.wvm)|*.wvm|All Files (*.*)|*.*";
            if (sound_file_dialog.ShowDialog() == DialogResult.OK)
            {
                path = sound_file_dialog.FileName;
            }
            txtFilePath.Text = path;
        }

        #endregion

        #region UI : Refresh

        #endregion

        #region UI : CheckBoxes : Multiple Add
        private void chckMultiAdd_CheckedChanged(object sender, EventArgs e)
        {
            if (chckMultiAdd.Checked == true)
            {
                txtTimesToAdd.Enabled = true;
            }
            else
            {
                txtTimesToAdd.Enabled = false;
            }
        }
        #endregion
    }
}
