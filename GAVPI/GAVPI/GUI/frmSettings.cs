using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GAVPI
{
    public partial class frmSettings : Form
    {
        Settings Settings;
        public frmSettings(Settings Settings)
        {
            InitializeComponent();
            MaximizeBox = false;
            this.Settings = Settings;

            populate_fields();

            // Update fields to show settings loaded from file
            populate_from_settings();
        }
        private void populate_fields()
        {
            List<String> localizations = new List<String>();
            foreach (RecognizerInfo ri in SpeechRecognitionEngine.InstalledRecognizers())
            {
                localizations.Add(ri.Culture.Name);
            }
            cbSettingsLanguage.DataSource = localizations;

            List<String> voices = new List<string>();
            SpeechSynthesizer synthesizer = new SpeechSynthesizer();
            foreach (InstalledVoice voice in synthesizer.GetInstalledVoices())
            {
                voices.Add(voice.VoiceInfo.Name);
            }
            cbSettingsSynthesizer.DataSource = voices;

            List<string> audio_devices = new List<string>();

            //TODO: actually enumerate audio devices.
            audio_devices.Add("Default Directsound device");

            cbSettingsRecordingDevice.DataSource = audio_devices;

            List<string> pushtotalk_mode_list = new List<string>();
            pushtotalk_mode_list.Add("Off");
            pushtotalk_mode_list.Add("Hold");
            pushtotalk_mode_list.Add("Toggle");
            pushtotalk_mode_list.Add("Single");
            cbSettingsPushToTalkMode.DataSource = pushtotalk_mode_list;
            cbSettingsPushToTalkMode.SelectedItem = Settings.pushtotalk_mode;

            cbSettingsPushToTalkKey.DataSource = Enum.GetValues(typeof(Keys)).Cast<Keys>();
            cbSettingsPushToTalkKey.SelectedItem = Enum.Parse(typeof(Keys), Settings.pushtotalk_key);

        }

        private void populate_from_settings()
        {
            cbSettingsLanguage.SelectedItem        = Settings.recognizer_info.Name;
            cbSettingsSynthesizer.SelectedItem     = Settings.voice_info;
            cbSettingsPushToTalkMode.SelectedItem  = Settings.pushtotalk_mode;
            cbSettingsPushToTalkKey.SelectedItem   = Settings.pushtotalk_key;
  
            AutomaticallyListen.Enabled = AutoLoadProfiles.Checked ? true : false;

            // TODO
            // unhandled recording devices
            //cbSettingsRecordingDevice.SelectedItem =
            
        }

        private void btnSettingsSave_Click(object sender, EventArgs e)
        {
            Settings.recognizer_info = new CultureInfo(cbSettingsLanguage.SelectedItem.ToString());
            Settings.voice_info      = cbSettingsSynthesizer.SelectedItem.ToString();
            Settings.pushtotalk_mode = cbSettingsPushToTalkMode.SelectedItem.ToString();
            Settings.pushtotalk_key  = cbSettingsPushToTalkKey.SelectedItem.ToString();

            Settings.save_settings();

            Properties.Settings.Default.Save();

            this.Close();
        }

        private void btnSettingsCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void StartUpShowMainWindow_CheckedChanged( object sender, EventArgs e ) {}

        private void AutoLoadProfiles_CheckedChanged( object sender, EventArgs e )
        {

            if( AutoLoadProfiles.Checked ) {
              
                AutomaticallyListen.Enabled = true;
                GAVPI.EnableAutoOpenProfile( sender, e );

            } else {

                AutomaticallyListen.Enabled = false;                
                GAVPI.DisableAutoOpenProfile( sender, e );

            }

        }

        private void AutomaticallyListen_CheckedChanged( object sender, EventArgs e ) {

        }
        
    }
}
