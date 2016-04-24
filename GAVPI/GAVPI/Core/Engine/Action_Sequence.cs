using System;
using System.Collections.Generic;
using System.Speech.Synthesis;
using System.Threading;
using System.Xml;

/*
 * A single action is composed of 
 * name - the name identifying the action
 * sequence of Actions
 * Actions are carried out sequentially one at time
 */

namespace GAVPI
{
    public class Action_Sequence : Trigger_Event
    {
        // These type lists are used to populate ui elements,
        // their (array string) value must match the class name specified bellow, ex : Action : Data_Set
        // this is particularly important since the string will be cast to a class instance later.

        public static List<string> PressAction_Types = new List<string>(
            new string[] { 
                "KeyDown", "KeyUp", "KeyPress",
                "MouseKeyDown","MouseKeyUp","MouseKeyPress"
            });
        public static List<string> SpeechAction_Types = new List<string>(
            new string[] { 
               "Speak",
               "Data_Speak"
            });
        public static List<string> PlaySoundAction_Types = new List<string>(
            new string[] {
                "Play_Sound"
            });
        public static List<string> TimingAction_Types = new List<string>(
            new string[] { 
               "Wait"
            });
        public static List<string> DataAction_Types = new List<string>(
            new string[] { 
               "Data_Paste"
               //"Data_Set","Data_Decrement","Data_Increment"
            });


        private System.Random rand = new System.Random();
        public bool random_exec;

        public string name {get; set;}
        public string type { get; set; }
        public string comment { get; set; }
        public string value { get; set; }

        public List<Action> action_sequence;
        
        public Action_Sequence()
        {
            action_sequence = new List<Action>();
            this.name  = "new sequence";
            this.type  = this.GetType().Name;
            this.value = null;
            this.random_exec = false;
            this.comment = "";
        }

        public Action_Sequence(string name)
        {
            action_sequence = new List<Action>();
            this.type = this.GetType().Name;
            this.value = null;
            this.comment = "";
            this.random_exec = false;
            this.name = name;
        }

        public Action_Sequence(XmlNode element, Database ProfileDB, SpeechSynthesizer synth)
        {
           /* An action sequence is composed of an <Action_Sequence> node with several
            * requred and several optional attributes. 
            * Nested child nodes are Actions.
            */
            // Init defaults.
            action_sequence = new List<Action>();

            // TODO : Remove value and type deps.
            this.type        = this.GetType().Name;
            this.value       = null;
            this.comment     = "";
            this.name        = "";
            this.random_exec = false;
            

            // Process Action Sequence attributes

            // Load Required Attributes
            if (element.Attributes["name"] != null)
            {
                this.name = element.Attributes["name"].Value;
            }
            else
            {
                throw new ArgumentNullException("Action_Sequence missing required attribute name.");
            }


            // Load Optional Attributes
            // random: is this sequence randomly executed.
            if (element.Attributes["random"] != null)
            {
                this.random_exec = Convert.ToBoolean(element.Attributes["random"].Value);
            }

            // comment: a comment string associated with this sequence.
            if (element.Attributes["comment"] != null)
            {
                this.comment = element.Attributes["comment"].Value;
            }

            #region Process Children Nodes (Actions)
            foreach (XmlNode action in element.ChildNodes)
            {
                string action_type = action.Attributes.GetNamedItem("type").Value;
                string action_value = action.Attributes.GetNamedItem("value").Value;

                Type new_action_type = Type.GetType("GAVPI." + action_type);
                object action_instance;

                switch (action_type)
                {
                    case "Speak":
                    {
                        action_instance = Activator.CreateInstance(new_action_type, synth, action_value);
                        break;
                    }
                    case "Play_Sound":
                    {
                        int deviceID;
                        if (Int32.TryParse(action.Attributes.GetNamedItem("deviceID").Value, out deviceID))
                        {
                            action_instance = Activator.CreateInstance(new_action_type, action_value, deviceID);
                        }
                        else
                        {
                            action_instance = Activator.CreateInstance(new_action_type, action_value, Play_Sound.defaultDeviceID);
                        }
                        break;
                    }
                    case "Data_Speak":
                    {
                        // action_value is the db key, the data element name
                        if (ProfileDB.DB.ContainsKey(action_value))
                        {
                            action_instance = Activator.CreateInstance(new_action_type, synth,
                                (Data)ProfileDB.DB[action_value]);
                        }
                        else
                        {
                            action_instance = null;
                        }
                        break;
                    }
                    default:
                    {
                        action_instance = Activator.CreateInstance(new_action_type, action_value);
                        break;
                    }
                    
                }
                if (action_instance != null)
                {
                    action_sequence.Add((Action)action_instance);
                }
                else
                {
                    // log a malformed action (load from profile).
                }
            }
            #endregion

        }
        
        public void writeXML(XmlWriter writer)
        {
            writer.WriteStartElement("Action_Sequence");
            writer.WriteAttributeString("name", this.name);
            writer.WriteAttributeString("type", this.type);
            writer.WriteAttributeString("comment", this.comment);
            writer.WriteAttributeString("random", this.random_exec.ToString());

            foreach (Action action in this.action_sequence)
            {
                writer.WriteStartElement("Action");
                switch (action.type)
                {
                    case "Play_Sound":
                        {

                            writer.WriteAttributeString("type", action.type);
                            writer.WriteAttributeString("value", action.value);
                            writer.WriteAttributeString("deviceID", ((Play_Sound)action).getDeviceID().ToString());
                            break;
                        }
                    default:
                        {
                            writer.WriteAttributeString("type", action.type);
                            writer.WriteAttributeString("value", action.value);
                            break;
                        }

                }
                writer.WriteEndElement();
            }
            writer.WriteEndElement();
        }

        public List<Action> get_Action_sequence()
        {
            return this.action_sequence;
        }
        public void set_Action_sequence(List<Action> new_Action_sequence)
        {
            this.action_sequence = new_Action_sequence;
        }

        public void Add(Action new_Action)
        {
            action_sequence.Add(new_Action);
        }
        public void Remove(Action rm_Action)
        {
            action_sequence.Remove(rm_Action);
        }

        

        public void run()
        {
            // If set, randomly choose *one* action to execute.
            if (random_exec)
            {
                int random_index = rand.Next(action_sequence.Count);
                action_sequence[random_index].run();
            }
            // Otherwise, standard sequential execution.
            else
            {
                foreach (Action action in action_sequence)
                {
                    action.run();
                    // A little hotfix for starcitizen, it cant handle going fast.
                    Thread.Sleep(20);
                }
            }
        }
    }
}
