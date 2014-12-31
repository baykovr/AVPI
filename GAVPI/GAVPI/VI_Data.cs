using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAVPI
{
    /*
     * This approach is not very scalable, however it sufficies give the
     * time constraint. 
     * Alternative approach is to use a tempalte class, this causes some arithmatic 
     * abstraction problems, however..
     */
    public abstract class VI_Data
    {
        /*
         * VI_Data represents a simple data element which we will allow the user to 
         * manipulate with some primitive functions: set, inc, dec etc.
         * Primary use is for logical triggers which have rely on comparator components.
         */
        public string name { get; set; } // enforce UNIQ in DB
        public string type { get; set; }
        // public: one of {INT,FLOAT,STRING} value
        public string comment { get; set; }

        // Operations (all optional)
        // dec by val
        // inc by val
        // set by val

        public VI_Data(string name,string comment)
        {
            this.name = name;
            this.comment = comment;
        }
    }
    public partial class VI_INT : VI_Data
    {
        int value { get; set; }
        public VI_INT(string name, int value, string comment)
            : base(name,comment)
        {
            this.value = value;
            this.type = "VI_INT";
        }
    }
    public partial class VI_FLOAT : VI_Data
    {
        float value { get; set; }
        public VI_FLOAT(string name, float value, string comment)
            : base(name, comment)
        {
            this.value = value;
            this.type = "VI_FLOAT";
        }

    }
    public partial class VI_STRING : VI_Data
    {
        string value { get; set; }
        public VI_STRING(string name, string value, string comment)
            : base(name, comment)
        {
            this.value = value;
            this.type = "VI_STRING";
        }
 
    }
}
