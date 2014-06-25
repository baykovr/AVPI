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
        public VI_Profile(string filename)
        {
 
        }
        public void load_profile(string filename)
        {
 
        }
        public void save_profile(string filename)
        {
 
        }

    }
    public class VI_Settings
    {
        public System.Globalization.CultureInfo recognizer_info;

        public VI_Settings(string filename)
        {
            //todo file io
            recognizer_info = new System.Globalization.CultureInfo("en-US");
        }
        public void load_settings(string filename)
        { }
        public void save_settings()
        { }
 
    }
}
