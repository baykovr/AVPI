using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GAVPI.GUI.Actions
{
    public partial class frm_AddEdit_ProcessStartAction : Form
    {
        private Action form_action; 

        public frm_AddEdit_ProcessStartAction()
        {
            InitializeComponent();
        }

        public frm_AddEdit_ProcessStartAction(Action action)
        {
            InitializeComponent();
            this.form_action = action;
            populate_fields();
        }

        public Action get_action()
        {
            return form_action;
        }

        private void populate_fields()
        {
            if (form_action != null)
            {
                txtCmd.Text = form_action.value;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Type new_action_type = Type.GetType("GAVPI.ProcessStart");

            form_action = (Action)Activator.CreateInstance(
                  new_action_type, txtCmd.Text.ToString());

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            form_action = null;
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
