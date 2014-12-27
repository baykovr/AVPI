using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAVPI
{
    public class VI_Data<T>
    {

        public string name { get; set; }
        T value            { get; set; }

        public VI_Data(string name, T value)
        {
            this.name  = name;
            this.value = value;
        }
    }
}
