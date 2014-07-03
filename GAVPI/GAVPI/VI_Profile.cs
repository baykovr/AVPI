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
                //load_profile(filename);

                Profile_Triggers = new List<VI_Trigger>();
                Profile_ActionSequences = new List<VI_Action_Sequence>();
            }
            catch (Exception profile_err)
            {
                MessageBox.Show("Problem loading profile.\n" + profile_err.Message, "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation,
                    MessageBoxDefaultButton.Button1);

                //remove after load_profile
                //ask if load defaults, then call that.
                Profile_Triggers = new List<VI_Trigger>();
                Profile_ActionSequences = new List<VI_Action_Sequence>();
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
            //Constructor will catch.

            XmlDocument profile = new XmlDocument();
            profile.Load(filename);
            if (profile.DocumentElement.Name != "gavpi")
            {
                throw new Exception("Malformed profile expected first tag gavpi got,"
                + profile.DocumentElement.Name);
            }
            XmlNodeList profile_elements = profile.DocumentElement.ChildNodes;
            foreach (XmlNode element in profile_elements)
            {
                // Elements are going to be Triggers and Action_Sequences
                // each element must have a unique name attribute
                if (element.Name == "Trigger")
                {
                    string trigger_name = element.Attributes[0].Value;
 
                }
                else if (element.Name == "ActionSequence")
                {
                    string action_sequence_name = element.Attributes[0].Value;
                    //Check existing
                    foreach (VI_Action_Sequence act_seq in Profile_ActionSequences){
                        if (act_seq.name == action_sequence_name){
                            throw new Exception("Duplicate Action Sequence discovered: " 
                                + act_seq.name);
                        }
                    }

                    VI_Action_Sequence newActionSequence = new VI_Action_Sequence(action_sequence_name);

                    XmlNodeList actions = element.ChildNodes;
                    foreach (XmlNode action in actions)
                    {
                        if (action.Name == "Action")
                        {
                            //ex <Action Type="MouseKeyPressAction" Value="Mouse.MouseKeys.Left">
                            switch(action.Attributes[0].Value)
                            {
                                case "KeyDown":
                                    newActionSequence.Add(new KeyDown(action.Attributes[1].Value));
                                    break;
                                //todo

                            }
                        }
                        else
                        {
                            throw new Exception("Unknown Action Sequence element: " + action.Name);
                        }
                    }

                }
                else
                {
                    throw new Exception("Unxpected element in profile: <" + element.Name+">");
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
                    writer.WriteStartElement("ActionSequence");
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
                foreach (VI_Trigger trig in Profile_Triggers)
                {
                    writer.WriteStartElement("Trigger");
                    writer.WriteAttributeString("name", trig.name);
                    writer.WriteAttributeString("value", trig.value);
                    writer.WriteAttributeString("type", trig.type);
                    writer.WriteAttributeString("comment", trig.comment);

                    //TriggerEvents (Events which this trigger will cause to happen)
                    foreach(VI_TriggerEvent trigger_event in trig.TriggerEvents)
                    {
                    writer.WriteStartElement("TriggerEvent");
                    writer.WriteAttributeString("name", trigger_event.name);
                    writer.WriteAttributeString("type", trigger_event.type);
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
