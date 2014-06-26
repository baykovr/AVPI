using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/*
 * Anything else file load/unload related.
 */

namespace GAVPI
{
    /*Stage 1: One profile at a time.*/
    public class VI_Profile
    {
        //Set of
        List<VI_Trigger> Profile_Triggers;
        List<VI_Action_Sequence> Profile_ActionSequences;
        
        public VI_Profile(string filename)
        {
            //todo: load from file.
            try
            {
                //load_profile(filename)
                throw new NotImplementedException();
            }
            catch (Exception profile_err)
            {
                MessageBox.Show("Problem loading profile.\n" + profile_err.Message, "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation,
                    MessageBoxDefaultButton.Button1);
                
                //remove after load_profile
                //ask if load defaults, then call that.
                Profile_Triggers = new List<VI_Trigger>();
                Profile_ActionSequences = new List<VI_Action_Sequence>();

                
            }
        }
        public void Add_Trigger(VI_Trigger trigger_toAdd)
        {
            Profile_Triggers.Add(trigger_toAdd);
        }
        public void Add_Action_Sequence(VI_Action_Sequence action_sequence_toAdd)
        {
            Profile_ActionSequences.Add(action_sequence_toAdd);
        }

        public void load_profile(string filename)
        {
            //parse from file
 
        }
        public void save_profile(string filename)
        {
            //parse to file
 
        }
    }
}
