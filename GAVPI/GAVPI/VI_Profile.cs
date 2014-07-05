using System;
using System.Collections.Generic;
using System.Linq;
using System.Speech.Synthesis;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

/*
 * Anything else file load/unload related.
 */

namespace GAVPI
{
    /*Stage 1: One profile at a time (for now).*/
    public class VI_Profile
    {
        public string name;

        public SpeechSynthesizer synth;

        public List<VI_Trigger> Profile_Triggers;
        public List<string> Trigger_Types;

        public List<VI_Action_Sequence> Profile_ActionSequences;
        public List<string> Action_Types;

        public VI_Profile(string filename)
        {
            try
            {
                Profile_Triggers = new List<VI_Trigger>();
                Profile_ActionSequences = new List<VI_Action_Sequence>();

                synth = new SpeechSynthesizer(); //used by action Speak

                if (filename != null)
                { 
                    load_profile(filename); 
                }
            }
            catch (Exception profile_err)
            {
                MessageBox.Show("Problem loading profile.\n" + profile_err.Message, "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation,
                    MessageBoxDefaultButton.Button1);
            }
            finally
            {
                Trigger_Types = new List<string>();
                Action_Types = new List<string>();
                Trigger_Types.Add("VI_Phrase");
                Action_Types.Add("KeyDown");
                Action_Types.Add("KeyUp");
                Action_Types.Add("KeyPress");
                Action_Types.Add("MouseKeyDown");
                Action_Types.Add("MouseKeyUp");
                Action_Types.Add("MouseKeyPress");
                Action_Types.Add("Wait");
                Action_Types.Add("Speak");
            }
        }
        public void Add_Trigger(VI_Trigger trigger_toAdd)
        {
            Profile_Triggers.Add(trigger_toAdd);
        }
        public void Add_Action_Sequence(VI_Action_Sequence action_sequence_toAdd)
        {
            Profile_ActionSequences.Add(action_sequence_toAdd);
        }

        public void load_profile(string filename)
        {
            //Clean Current
            Profile_Triggers = new List<VI_Trigger>();
            Profile_ActionSequences = new List<VI_Action_Sequence>();
            //Constructor will catch.
            XmlDocument profile_xml = new XmlDocument();
            profile_xml.Load(filename);
            //Check first element tag
            if (profile_xml.DocumentElement.Name != "gavpi") {
                throw new Exception("Malformed profile expected first tag gavpi got,"
                + profile_xml.DocumentElement.Name);
            }
            XmlNodeList profile_xml_elements = profile_xml.DocumentElement.ChildNodes;
            foreach (XmlNode element in profile_xml_elements)
            {
                if (element.Name == "VI_Action_Sequence")
                {
                    VI_Action_Sequence ack_frm_file;
                    ack_frm_file = new VI_Action_Sequence(element.Attributes.GetNamedItem("name").Value);
                    ack_frm_file.type = element.Attributes.GetNamedItem("type").Value;
                    ack_frm_file.comment = element.Attributes.GetNamedItem("comment").Value;
                    
                    foreach (XmlNode action in element.ChildNodes)
                    {
                        string action_type = action.Attributes.GetNamedItem("type").Value;
                        string action_value = action.Attributes.GetNamedItem("value").Value;
                        Type new_action_type = Type.GetType("GAVPI." + action_type );
                        object action_instance;
                        if (action_type == "Speak")
                        {
                            action_instance = Activator.CreateInstance(new_action_type,this.synth, action_value);
                        }
                        else
                        {
                            action_instance = Activator.CreateInstance(new_action_type, action_value);
                        }
                        ack_frm_file.Add((Action)action_instance);
                    }
                    if (!Profile_ActionSequences.Any(ack => ack.name == ack_frm_file.name))
                    {
                        Profile_ActionSequences.Add(ack_frm_file);
                    }
                }
                else if (element.Name == "VI_Trigger")
                {
                    VI_Trigger trig_frm_file;
                    string trigger_name = element.Attributes.GetNamedItem("name").Value;
                    string trigger_type = element.Attributes.GetNamedItem("type").Value;
                    string trigger_value = element.Attributes.GetNamedItem("value").Value;
                    string trigger_comment= element.Attributes.GetNamedItem("comment").Value;

                    Type new_trigger_type = Type.GetType("GAVPI." + trigger_type);
                    object trigger_isntance = trigger_isntance = Activator.CreateInstance(new_trigger_type, trigger_name, trigger_value);
                    trig_frm_file = (VI_Trigger)trigger_isntance;
                    trig_frm_file.comment = trigger_comment;

                    // Trigger Events
                    foreach (XmlElement trigger_event in element.ChildNodes)
                    {
                        string event_type = trigger_event.Attributes.GetNamedItem("type").Value;
                        string event_name = trigger_event.Attributes.GetNamedItem("name").Value;
                        string event_value = trigger_event.Attributes.GetNamedItem("value").Value;
                        if (event_type == "VI_Action_Sequence")
                        {
                            trig_frm_file.Add(Profile_ActionSequences.Find( ackseq => ackseq.name == event_name));
                        }
                        else if (event_type == "VI_Phrase")
                        {
                            VI_Trigger newMetaTrigger;
                            Type meta_trigger_type = Type.GetType("GAVPI." + event_type);
                            object meta_trigger_isntance = Activator.CreateInstance(meta_trigger_type,event_name, event_value);
                            newMetaTrigger = (VI_Trigger)meta_trigger_isntance;

                            trig_frm_file.Add(newMetaTrigger);
                        }
                    }
                    // Malformed xml or double load, need to switch to dictionaries tbh
                    if (!Profile_Triggers.Any(trig=>trig.name == trig_frm_file.name))
                    { 
                        Profile_Triggers.Add(trig_frm_file); 
                    }
                }
            }
        }
        public void save_profile(string filename)
        {
            try 
            {
                XmlWriterSettings settings = new XmlWriterSettings();
                settings.Indent = true;
                XmlWriter writer = XmlWriter.Create(filename, settings);
                writer.WriteStartDocument();
                writer.WriteStartElement("gavpi");

                foreach (VI_Action_Sequence ack_seq in Profile_ActionSequences)
                {
                    writer.WriteStartElement("VI_Action_Sequence");
                    writer.WriteAttributeString("name", ack_seq.name);
                    writer.WriteAttributeString("type", ack_seq.type);
                    writer.WriteAttributeString("comment", ack_seq.comment);

                    foreach (Action act in ack_seq.action_sequence)
                    {
                        writer.WriteStartElement("Action");
                        writer.WriteAttributeString("type", act.type);
                        writer.WriteAttributeString("value", act.value);
                        writer.WriteEndElement();
                    }
                    writer.WriteEndElement();
                }
                foreach (VI_Trigger trig in Profile_Triggers){
                    writer.WriteStartElement("VI_Trigger");
                        writer.WriteAttributeString("name", trig.name);
                        writer.WriteAttributeString("value", trig.value);
                        writer.WriteAttributeString("type", trig.type);
                        writer.WriteAttributeString("comment", trig.comment);
                        //TriggerEvents (Events which this trigger will cause to happen)
                   foreach(VI_TriggerEvent trigger_event in trig.TriggerEvents)
                   {
                       writer.WriteStartElement("VI_TriggerEvent");
                       writer.WriteAttributeString("name", trigger_event.name);
                       writer.WriteAttributeString("type", trigger_event.type);
                       writer.WriteAttributeString("value", trigger_event.value);
                       writer.WriteAttributeString("comment", trig.comment);
                       writer.WriteEndElement();
                   }
                    writer.WriteEndElement();
                }

                writer.WriteEndElement();
                writer.WriteEndDocument();

                writer.Flush();
                writer.Close();
            }
            catch (Exception err_saving)
            {
                MessageBox.Show("Error saving to file" + err_saving.Message);
            }
        }
    }
}
