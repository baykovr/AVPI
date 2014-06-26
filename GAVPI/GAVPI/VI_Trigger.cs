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
    public abstract class VI_Trigger
    {
        // Name must be unique
        public string name;
        // Hashset contains trigger/actseq names
        
        public VI_Profile profile;
        public List<VI_Trigger> Triggers;
        public List<VI_Action_Sequence> Action_Sequences;
        
        public VI_Trigger(string name)
        {
            this.name = name;
            Triggers = new List<VI_Trigger>();
            Action_Sequences = new List<VI_Action_Sequence>();
        }

        public void Add(VI_Trigger trigger)
        {
            Triggers.Add(trigger);
        }
        public bool Remove(VI_Trigger trigger)
        {
            return Triggers.Remove(trigger);
        }
        public void Add(VI_Action_Sequence action_sequence)
        {
            Action_Sequences.Add(action_sequence);
        }
        public bool Remove(VI_Action_Sequence action_sequence)
        {
            return Action_Sequences.Remove(action_sequence);
        }

        public abstract void run();

    }
    public partial class VI_Phrase : VI_Trigger
    {
        public string phrase;

        public VI_Phrase(string name ,string phrase) : base(name)
        {
            this.phrase = phrase;
        }
        public override void run()
        {
            foreach (VI_Trigger trigger in Triggers)
            {
                trigger.run();
            }
            foreach (VI_Action_Sequence action_sequence in Action_Sequences)
            {
                action_sequence.run();
            }
            throw new NotImplementedException();
        }
    }
    public partial class VI_Condition : VI_Trigger
    {

        public VI_Condition(string name) : base(name)
        {
 
        }
        public override void run()
        {
            throw new NotImplementedException();
        }
    }
}
