using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GAVPI
{
    static class GAVPI
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            VI_Action test = new VI_Action("test");
            test.add_event(new KeyDownEvent(Keys.A));
            while (true)
            {
                test.run_sequence();
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmGAVPI());
        }
    }
}
