using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAVPI
{
    /* Triggers 
     * A trigger is an event which can 
     * trigger an action_sequence
     * trigger a trigger
     * can have many action_sequences
     * can have many triggers
     */
    public abstract class VI_Trigger
    {

    }
    public partial class VI_Phrase : VI_Trigger
    {
        public string phrase;

        public VI_Phrase(string phrase)
        {
            this.phrase = phrase;
        }
    }
    public partial class VI_Condition : VI_Trigger
    {

        public VI_Condition()
        {
 
        }

    }
}
