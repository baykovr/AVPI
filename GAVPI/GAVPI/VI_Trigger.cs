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
    public abstract class VI_Trigger : VI_TriggerEvent
    {
        // Name must be unique
        public string type { get; set; }
        public string name {get;set;}
        public string value { get; set; }
        public string comment { get; set; }
        // Hashset contains trigger/actseq names
        
        public VI_Profile profile;

        public List<VI_TriggerEvent>TriggerEvents;

        public VI_Trigger(string name)
        {
            this.name = name;
            this.type = this.GetType().Name;

            TriggerEvents = new List<VI_TriggerEvent>();
        }

        public void Add(VI_TriggerEvent tevent)
        {
            TriggerEvents.Add(tevent);
        }
        public bool Remove(VI_TriggerEvent tevent)
        {
            return TriggerEvents.Remove(tevent);
        }

        public abstract void run();

    }
    // Tigger Types
    // 
    public partial class VI_Phrase : VI_Trigger
    {
        public VI_Phrase(string name ,string value) : base(name)
        {
            this.value = value;
        }
        public override void run()
        {
            foreach (VI_TriggerEvent tevent in TriggerEvents)
            {
                tevent.run();
            }
        }
    }

    public partial class VI_Logical : VI_Trigger
    {
        // Logical trigger compare VI_Data obj
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
