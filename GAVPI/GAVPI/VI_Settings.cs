using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAVPI
{
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
