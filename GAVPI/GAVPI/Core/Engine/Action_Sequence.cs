using System.Collections.Generic;
using System.Threading;

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
               "Data_Set","Data_Decrement","Data_Increment"
            });


        private System.Random rand = new System.Random();
        public bool random_exec = false;

        public string name {get; set;}
        public string type { get; set; }
        public string comment { get; set; }

        //sequences do not use value, but must have it as part of TriggerEvent interface
        public string value { get; set; }

        public List<Action> action_sequence;
        
        public Action_Sequence()
        {
            action_sequence = new List<Action>();
            this.name  = "new sequence";
            this.type  = this.GetType().Name;
            this.value = null;
            this.comment = "";
        }

        public Action_Sequence(string name)
        {
            action_sequence = new List<Action>();
            this.name = name;
            this.type = this.GetType().Name;
            this.value = null;
            this.comment = "";
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
