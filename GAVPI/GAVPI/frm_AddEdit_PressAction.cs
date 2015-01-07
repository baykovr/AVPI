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
    public partial class frm_AddEdit_PressAction : Form
    {
        public Action action_to_add;

        private Action action_to_edit;

        public frm_AddEdit_PressAction()
        {
            InitializeComponent();

            populate_fields();
        }
        public frm_AddEdit_PressAction(Action action)
        {
            InitializeComponent();
            
            this.action_to_edit = action;

            populate_fields();
        }

        public void populate_fields()
        {
            // Populate initial values for drop down boxes.
            cbPressType.DataSource  = VI_Action_Sequence.Press_Types;
            
            // default to keys.
            cbPressValue.DataSource = Enum.GetValues(typeof(Keys)).Cast<Keys>();

            // if editing, select current values of existing action.
            if (action_to_edit != null)
            {
                
            }
 
        }

        // UI Event : Type Drop Down Selection has changed
        private void cbPressType_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Set drop down choices appropriately
            if (cbPressType.SelectedItem.ToString() == "KeyDown" ||
                cbPressType.SelectedItem.ToString() == "KeyUp"   ||
                cbPressType.SelectedItem.ToString() == "KeyPress")
            {
                // if the cb selected is one of Key then cbvalues values must be keys
                cbPressValue.DataSource = Enum.GetValues(typeof(Keys)).Cast<Keys>();
            }
            else if (
                cbPressType.SelectedItem.ToString() == "MouseKeyDown" ||
                cbPressType.SelectedItem.ToString() == "MouseKeyUp"   ||
                cbPressType.SelectedItem.ToString() == "MouseKeyPress")
            {
                // if the cb selected is one of Mouse then cbvalues must be mouse buttons.
                cbPressValue.DataSource = Enum.GetValues(
                    typeof(InputManager.Mouse.MouseKeys)).Cast<InputManager.Mouse.MouseKeys>();
            }
            else
            {
                MessageBox.Show("WARNING: This action type is not implemented!");
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            action_to_add = null;

            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
