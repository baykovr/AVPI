using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * Anything else file load/unload related.
 */

namespace GAVPI
{
    /*Stage 1: One profile at a time.*/
    public class VI_Profile
    {
        //Set of
        HashSet<VI_Phrase> Profile_Phrases;
        HashSet<VI_Action_Sequence> Profile_ActionSequences;
        
        // Mapping
        Dictionary<VI_Trigger, VI_Action_Sequence> Profile_TriggerMap;

        public VI_Profile(string filename)
        {
            //todo: file io
            Profile_Phrases = new HashSet<VI_Phrase>();
            Profile_ActionSequences = new HashSet<VI_Action_Sequence>();
 
        }
        public bool Add_ActionSequence(VI_Action_Sequence action_sequence_toAdd)
        {
            return Profile_ActionSequences.Add(action_sequence_toAdd);
        }
        public bool Add_Phrase(VI_Phrase phrase_toAdd)
        {
            return Profile_Phrases.Add(phrase_toAdd);
        }

        public void load_profile(string filename)
        {
 
        }
        public void save_profile(string filename)
        {
 
        }
    }
}
