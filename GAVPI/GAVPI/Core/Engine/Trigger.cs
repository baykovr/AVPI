using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAVPI
{
    /* Triggers 
     * A trigger is an event which can:
     * trigger an action_sequence
     * trigger a trigger
     * can have many action_sequences
     * can have many triggers
     */
    public abstract class Trigger : Trigger_Event
    {
        // Name must be unique
        public string type { get; set; }
        public string name {get;set;}
        public string value { get; set; }
        public string comment { get; set; }
        // Hashset contains trigger/actseq names
        
        public Profile profile;

        public List<Trigger_Event>TriggerEvents;

        public Trigger(string name)
        {
            this.name = name;
            this.type = this.GetType().Name;

            TriggerEvents = new List<Trigger_Event>();
        }

        public void Add(Trigger_Event tevent)
        {
            TriggerEvents.Add(tevent);
        }
        public bool Remove(Trigger_Event tevent)
        {
            return TriggerEvents.Remove(tevent);
        }

        public abstract void run();

    }
    // Tigger Types
    // 
    public partial class VI_Phrase : Trigger
    {
        public VI_Phrase(string name ,string value) : base(name)
        {
            this.value = value;
        }
        public VI_Phrase(string name, string value, string comment)
            : base(name)
        {
            this.value = value;
            this.comment = comment;
        }
        public override void run()
        {
            foreach (Trigger_Event tevent in TriggerEvents)
            {
                tevent.run();
            }
        }
    }

    public partial class VI_Logical : Trigger
    {
        // Logical trigger compare Data obj
        // to some value
        // has CONDITION ( < , > , === etc)
        // runs if CONDITION
        public VI_Logical(string name, string value)
            : base(name)
        {
 
        }
        public override void run()
        {
            throw new NotImplementedException();
        }
    }
}
