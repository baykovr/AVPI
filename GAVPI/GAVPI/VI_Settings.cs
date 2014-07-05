using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Speech.Synthesis;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace GAVPI
{
    public class VI_Settings
    {
        public string current_profile_path;

        public System.Globalization.CultureInfo recognizer_info;
        public string voice_info;

        public VI_Settings()
        {
            recognizer_info = CultureInfo.CurrentCulture;
            SpeechSynthesizer synthesizer = new SpeechSynthesizer();
            voice_info = new SpeechSynthesizer().GetInstalledVoices()[0].VoiceInfo.Name;
        }
        public void load_settings(string filename)
        {

        }
        public void save_settings()
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            XmlWriter writer = XmlWriter.Create("settings.xml", settings);
            writer.WriteStartDocument();
            writer.WriteStartElement("gavpi");


            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Flush();
            writer.Close();
        }
    }
}
