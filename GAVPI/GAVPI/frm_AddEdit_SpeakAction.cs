using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GAVPI
{
    public partial class frm_AddEdit_SpeakAction : Form
    {
        private Action form_action;

        private int times_to_add;

        public Action get_action()
        {
            return form_action;
        }
        public int get_times_to_add()
        {
            return times_to_add;
        }

        public frm_AddEdit_SpeakAction()
        {
            InitializeComponent();
        }
        public frm_AddEdit_SpeakAction(Action action)
        {
            InitializeComponent();
        }
    }
}
