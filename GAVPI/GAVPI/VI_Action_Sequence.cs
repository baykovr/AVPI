using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Speech.Synthesis;
using InputManager;

/*
 * A single action is composed of 
 * name - the name identifying the action
 * sequence of Actions
 * Actions are carried out sequentially one at time
 */

namespace GAVPI
{
    public class VI_Action_Sequence : VI_TriggerEvent
    {
        public string name {get; set;}
        public string type { get; set; }
        public string comment { get; set; }
        public List<Action> action_sequence;
        

        public VI_Action_Sequence(string name)
        {
            action_sequence = new List<Action>();
            this.name = name;
            this.type = "Action Sequence";
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
            foreach (Action action in action_sequence)
            {
                action.run();
            }
        }
    }

     /*
      *   Arbitrary abstract Action
      *  Currently one of 
      *  Keyboard up/down/press
      *  Mouse up/down/press
      *  Wait 
      *  ...add your own
      *  ie: public partial class PlaySound() ...
     */
    public abstract class Action
    {
        public string type { get; set; }
        public string value { get; set; }

        public abstract void run();

        public Action(string value)
        {
            this.value = value;
            this.type = this.GetType().Name;
        }
    }
    public partial class KeyDown : Action
    {
        public KeyDown(string value) : base(value)
        {

        }
        public override void run()
        {
            Keyboard.KeyDown((Keys)Enum.Parse(typeof(Keys), value));
        }
    }
    public partial class KeyUp : Action
    {
        public KeyUp(string value): base(value)
        {
        }
        public override void run()
        {
            Keyboard.KeyDown((Keys)Enum.Parse(typeof(Keys), value));
        }
    }
    public partial class KeyPress : Action
    {
        public KeyPress(string value): base(value)
        {
           
        }
        public override void run()
        {
            Keyboard.KeyDown((Keys)Enum.Parse(typeof(Keys), value));
        }
    }

    public partial class MouseKeyDown : Action
    {
        public MouseKeyDown(string value): base(value)
        {
            
        }
        public override void run()
        {
            Mouse.ButtonDown((Mouse.MouseKeys)Enum.Parse(typeof(Mouse.MouseKeys), value));
        }
    }
    public partial class MouseKeyUp : Action
    {
        public MouseKeyUp(string value) : base(value)
        {
           
        }
        public override void run()
        {
            Mouse.ButtonDown((Mouse.MouseKeys)Enum.Parse(typeof(Mouse.MouseKeys), value));
        }
    }
    public partial class MouseKeyPress : Action
    {
        public MouseKeyPress(string value) : base(value)
        {
        }
        public override void run()
        {
            Mouse.ButtonDown((Mouse.MouseKeys)Enum.Parse(typeof(Mouse.MouseKeys), value));
        }
    }
    public partial class Wait : Action
    {
        public Wait(string value) : base(value)
        {
        }
        public override void run()
        {
            Thread.Sleep( Int32.Parse(value));
        }
    }
    public partial class Speak : Action
    {
        SpeechSynthesizer synth;
        public Speak(SpeechSynthesizer synth, string value) : base(value)
        {
            this.synth = synth;
        }
        public override void run()
        {
            try
            {
                synth.SpeakAsync(value);
            }
            catch(Exception synth_err)
            {
                MessageBox.Show("Speech Synth Err : "+synth_err.Message,"Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation,
                    MessageBoxDefaultButton.Button1);
            }
        }
    }
}
