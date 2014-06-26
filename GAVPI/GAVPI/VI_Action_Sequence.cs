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
    public class VI_Action_Sequence
    {
        public string name;
        private List<Action> action_sequence;

        public VI_Action_Sequence(string name)
        {
            action_sequence = new List<Action>();
            this.name = name;
        }
        public List<Action> get_Action_sequence()
        {
            return this.action_sequence;
        }
        public void set_Action_sequence(List<Action> new_Action_sequence)
        {
            this.action_sequence = new_Action_sequence;
        }

        public void add_Action(Action new_Action)
        {
            action_sequence.Add(new_Action);
        }
        public void remove_Action(Action rm_Action)
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
        public abstract void run();
    }
    public partial class KeyDownAction : Action
    {
        Keys kd_Key;
        public KeyDownAction(Keys keydown)
        {
            this.kd_Key = keydown;
        }
        public override void run()
        {
            Keyboard.KeyDown(kd_Key);
        }
    }
    public partial class KeyUpAction : Action
    {
        Keys ku_Key;
        public KeyUpAction(Keys keyup)
        {
            this.ku_Key = keyup;
        }
        public override void run()
        {
            Keyboard.KeyDown(ku_Key);
        }
    }
    public partial class KeyPressAction : Action
    {
        Keys kp_Key;

        public KeyPressAction(Keys keypress)
        {
            this.kp_Key = keypress;
        }
        public override void run()
        {
            Keyboard.KeyPress(kp_Key);
        }
    }

    public partial class MouseKeyDownAction : Action
    {
        Mouse.MouseKeys md_Key;
        public MouseKeyDownAction(Mouse.MouseKeys mousedown)
        {
            this.md_Key = mousedown;
        }
        public override void run()
        {
            Mouse.ButtonDown(md_Key);
        }
    }
    public partial class MouseKeyUpAction : Action
    {
        Mouse.MouseKeys mu_Key;
        public MouseKeyUpAction(Mouse.MouseKeys mouseup)
        {
            this.mu_Key = mouseup;
        }
        public override void run()
        {
            Mouse.ButtonUp(mu_Key);
        }
    }
    public partial class MouseKeyPressAction : Action
    {
        Mouse.MouseKeys mp_Key;
        public MouseKeyPressAction(Mouse.MouseKeys mousepress)
        {
            this.mp_Key = mousepress;
        }
        public override void run()
        {
            Mouse.PressButton(mp_Key);
        }
    }
    public partial class WaitAction : Action
    {
        Int32 ms_wait;
        public WaitAction(Int32 ms_to_wait)
        {
            this.ms_wait = ms_to_wait;
        }
        public override void run()
        {
            Thread.Sleep(ms_wait);
        }
    }
    public partial class SpeakAction : Action
    {
        string speech;
        SpeechSynthesizer synth;
        public SpeakAction (SpeechSynthesizer synth, string speech)
        {
            this.synth = synth;
            this.speech = speech;
        }
        public override void run()
        {
            try
            {
                synth.SpeakAsync(speech);
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
