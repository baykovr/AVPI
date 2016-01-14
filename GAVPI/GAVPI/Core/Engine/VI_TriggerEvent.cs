using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAVPI
{
    public interface VI_TriggerEvent
    {
       string name { get; set; }
       string type { get; set; }
       string comment { get; set; }
       string value { get; set; }
       void run();
    }
}
