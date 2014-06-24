using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using InputManager;

/*
 * A single action is composed of 
 * name - the name identifying the action
 * sequence of events
 * events are carried out sequentially one at time
 */

namespace GAVPI
{
    public class VI_Action
    {
        public string name;
        private List<Event> event_sequence;

        public VI_Action(string name)
        {
            event_sequence = new List<Event>();
            this.name = name;
        }
        public List<Event> get_event_sequence()
        {
            return this.event_sequence;
        }
        public void set_event_sequence(List<Event> new_event_sequence)
        {
            this.event_sequence = new_event_sequence;
        }

        public void add_event(Event new_event)
        {
            event_sequence.Add(new_event);
        }
        public void remove_event(Event rm_event)
        {
            event_sequence.Remove(rm_event);
        }
        public void run_sequence()
        {
            foreach (Event e in event_sequence)
            {
               e.run();
            }
        }
    }

     /*
     *  Arbitrary abstract Event
      *  Currently one of 
      *  Keyboard up/down/press
      *  Mouse up/down/press
      *  Wait 
      *  ...add your own
      *  ie: public partial class PlaySound() ...
     */
    public abstract class Event
    {
        public abstract void run();
    }
    public partial class KeyDownEvent : Event
    {
        Keys kd_Key;
        public KeyDownEvent(Keys keydown)
        {
            this.kd_Key = keydown;
        }
        public override void run()
        {
            Keyboard.KeyDown(kd_Key);
        }
    }
    public partial class KeyUpEvent : Event
    {
        Keys ku_Key;
        public KeyUpEvent(Keys keyup)
        {
            this.ku_Key = keyup;
        }
        public override void run()
        {
            Keyboard.KeyDown(ku_Key);
        }
    }
    public partial class KeyPressEvent : Event
    {
        Keys kp_Key;

        public KeyPressEvent(Keys keypress)
        {
            this.kp_Key = keypress;
        }
        public override void run()
        {
            Keyboard.KeyPress(kp_Key);
        }
    }

    public partial class MouseKeyDownEvent : Event
    {
        Mouse.MouseKeys md_Key;
        public MouseKeyDownEvent(Mouse.MouseKeys mousedown)
        {
            this.md_Key = mousedown;
        }
        public override void run()
        {
            Mouse.ButtonDown(md_Key);
        }
    }
    public partial class MouseKeyUpEvent : Event
    {
        Mouse.MouseKeys mu_Key;
        public MouseKeyUpEvent(Mouse.MouseKeys mouseup)
        {
            this.mu_Key = mouseup;
        }
        public override void run()
        {
            Mouse.ButtonUp(mu_Key);
        }
    }
    public partial class MouseKeyPressEvent : Event
    {
        Mouse.MouseKeys mp_Key;
        public MouseKeyPressEvent(Mouse.MouseKeys mousepress)
        {
            this.mp_Key = mousepress;
        }
        public override void run()
        {
            Mouse.PressButton(mp_Key);
        }
    }
    public partial class WaitEvent : Event
    {
        Int32 ms_wait;
        public WaitEvent(Int32 ms_to_wait)
        {
            this.ms_wait = ms_to_wait;
        }
        public override void run()
        {
            Thread.Sleep(ms_wait);
        }
    }
}
