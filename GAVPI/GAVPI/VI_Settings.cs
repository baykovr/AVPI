using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Speech.Synthesis;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace GAVPI
{
    public class VI_Settings
    {
        /*
         * @default_profile_name
         * @default_profile_filepath
         *      profile name and filepath, allow the user to specify a profile to load on startup
         *      this is possibly null if no default is specified. (TODO : always create a default blank profile?)
         * @voice_info
         *      Pulls from MS system installed voices
         * @pushtotalk_mode
         *      Panz: Press/Hold/Toggle
         * @pushtotalk_key
         *      TODO: Key conflicts?
         * @recognizer_info
         *      Culture of speech recog, this is crucial in VI.cs grammar builder must match this value. ie EN-US
         */
        public const string DEFUALT_SETTINGS_FILENAME = "gavpi-settings.xml";

        public string default_profile_name;
        public string default_profile_filepath;

        public string voice_info;
        public string pushtotalk_mode;
        public string pushtotalk_key;
        public System.Globalization.CultureInfo recognizer_info;

        public VI_Settings()
        {
            recognizer_info = CultureInfo.CurrentCulture;
            SpeechSynthesizer synthesizer = new SpeechSynthesizer();
            voice_info = new SpeechSynthesizer().GetInstalledVoices()[0].VoiceInfo.Name;
            pushtotalk_mode = "Off" ;
            pushtotalk_key  = "Scroll" ;


            // Load from settings file, if possible.
            this.load_settings();
        }
        public void load_settings()
        {
            // TODO
            XmlDocument gavpi_settings = new XmlDocument();
            try
            {
                gavpi_settings.Load(DEFUALT_SETTINGS_FILENAME);

                // Attempt to parse and load
                if (gavpi_settings.DocumentElement.Name != "gavpi")
                {
                    throw new Exception("Malformed settings file expected first tag gavpi got,"
                    + gavpi_settings.DocumentElement.Name);
                }
                XmlNodeList gavpi_settings_elements = gavpi_settings.DocumentElement.ChildNodes;
                foreach (XmlNode element in gavpi_settings_elements)
                {
                    if (element.Name == "VI_Settings")
                    {
                        // Warning : can be null
                        string xml_default_profile_name = element.Attributes.GetNamedItem("default_profile_name").Value;
                        // Warning : can be null
                        string xml_default_profile_filepath = element.Attributes.GetNamedItem("default_profile_filepath").Value;
                        
                        string xml_voice_info = element.Attributes.GetNamedItem("voice_info").Value;
                        string xml_pushtotalk_mode = element.Attributes.GetNamedItem("pushtotalk_mode").Value;
                        string xml_pushtotalk_key = element.Attributes.GetNamedItem("pushtotalk_key").Value;
                        string xml_recognizer_info = element.Attributes.GetNamedItem("recognizer_info").Value;

                     if (String.IsNullOrEmpty(xml_voice_info) &&
                             String.IsNullOrEmpty(xml_pushtotalk_mode) &&
                             String.IsNullOrEmpty(xml_pushtotalk_key) &&
                             String.IsNullOrEmpty(xml_recognizer_info))
                        {
                           default_profile_name = xml_default_profile_name;
                           default_profile_filepath = xml_default_profile_filepath;
                           voice_info = xml_voice_info;
                           pushtotalk_mode = xml_pushtotalk_mode;
                           pushtotalk_key = xml_pushtotalk_key;
                           recognizer_info = new System.Globalization.CultureInfo(xml_recognizer_info);
                        }
                        else
                        {
                            throw new Exception("Malformed settings file, some values are null.");
                        }
                    }
                    else
                    {
                        throw new Exception("Malformed settings file, unexpected element: " + element.Name);
                    }
                    
                }

            }
            catch (Exception loading_err){
                // Likely the user moved the exe or deleted the settings,
                // Give the option to write what we have
                DialogResult RestoreDefaults = MessageBox.Show(loading_err.Message+ "\n\n" +
                                                            "Would you like to revert to default values?",
                                                            "Error Loading GAVPI Settings",
                                                            MessageBoxButtons.YesNo);
                if (RestoreDefaults == DialogResult.Yes){
                    this.save_settings();
                }
            }
        }
        public void save_settings()
        {
            // Check for null values. (except profile)
            if (!validate_profile()){
                MessageBox.Show("Cannot save profile, one or more values current uset.");
                return;
            }

            try{
                XmlWriterSettings settings = new XmlWriterSettings();
                settings.Indent = true;
                XmlWriter writer = XmlWriter.Create(DEFUALT_SETTINGS_FILENAME, settings);
                writer.WriteStartDocument();
                writer.WriteStartElement("gavpi");

                writer.WriteStartElement("VI_Settings");

                // Warning : can be null
                writer.WriteAttributeString("default_profile_name", default_profile_name);
                // Warning : can be null
                writer.WriteAttributeString("default_profile_filepath", default_profile_filepath); 

                writer.WriteAttributeString("voice_info", voice_info);
                writer.WriteAttributeString("pushtotalk_mode", pushtotalk_mode);
                writer.WriteAttributeString("pushtotalk_key", pushtotalk_key);
                writer.WriteAttributeString("recognizer_info", recognizer_info.ToString());

                writer.WriteEndElement();
                writer.WriteEndDocument();
                writer.Flush();
                writer.Close();
            }
            catch (Exception err_saving)
            {
                MessageBox.Show("Error saving settings to file : " + err_saving.Message);
            }
        } // public void save_settings()
        public bool validate_profile()
        {
            // Basic check before writing to file/loading.
            
            // There may not be a profile present, this is ok.
            // current_profile_path
            if (String.IsNullOrEmpty(voice_info) &&
                             String.IsNullOrEmpty(pushtotalk_mode) &&
                             String.IsNullOrEmpty(pushtotalk_key) &&
                             String.IsNullOrEmpty(recognizer_info.ToString()))
            {
                return true; 
            }
            else { 
                return false; 
            }
        } //public bool validate_profile()
    }
}
