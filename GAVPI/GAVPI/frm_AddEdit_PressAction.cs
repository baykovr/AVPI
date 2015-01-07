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

            // default times to add
            txtTimesToAdd.Text = "1";

            // if editing, select current values of existing action.
            if (action_to_edit != null)
            {
                // Hide multiple add (since this is an edit on single item)
                chckMultiAdd.Visible  = false;
                txtTimesToAdd.Visible = false;
                
            }
 
        }

        // UI Event : Type Drop Down Selection has changed
        private void cbPressType_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Set drop down choices appropriately
            if (cbPressType.SelectedItem.ToString() == "KeyDown" ||
                cbPressType.SelectedItem.ToString() == "KeyUp" ||
                cbPressType.SelectedItem.ToString() == "KeyPress")
            {
                // if the cb selected is one of Key then cbvalues values must be keys
                cbPressValue.DataSource = Enum.GetValues(typeof(Keys)).Cast<Keys>();
            }
            else if (
                cbPressType.SelectedItem.ToString() == "MouseKeyDown" ||
                cbPressType.SelectedItem.ToString() == "MouseKeyUp" ||
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
            // Validate times to add
            //// Number of Times to Add particular action
            int times_to_add = 1;
            if (Int32.TryParse(txtTimesToAdd.Text, out times_to_add))
            {
                if (times_to_add <= 0)
                {
                    MessageBox.Show("Times to add value cannot be less than or equal to 0.");
                    return;
                }
            }
            else
            {
                MessageBox.Show("Times to add value must be a valid integer greater than one. (Max size 32bits)");
                return;
            }

            // -- 

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            action_to_add = null;

            this.DialogResult = DialogResult.Cancel;

            this.Close();
        }

        private void chckMultiAdd_CheckedChanged(object sender, EventArgs e)
        {
            if (chckMultiAdd.Checked)
            {
                txtTimesToAdd.Enabled = true;
            }
            else
            {
                txtTimesToAdd.Enabled = false;
            }
        }
    }
}
