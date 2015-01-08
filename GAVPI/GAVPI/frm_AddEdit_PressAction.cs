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
    /*
     * Form must garantee form_action is non-null,
     * validated and correct on form OK exit.
     * Otherwise form_action is null
     */
    public partial class frm_AddEdit_PressAction : Form
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
        public frm_AddEdit_PressAction()
        {
            InitializeComponent();

            populate_fields();
        }
        public frm_AddEdit_PressAction(Action action)
        {
            InitializeComponent();
            
            this.form_action = action;

            populate_fields();
        }

        public void populate_fields()
        {
            // Populate initial values for drop down boxes.
            cbPressType.DataSource  = VI_Action_Sequence.Press_Types;
            
            // default to keys.
            cbPressValue.DataSource = Enum.GetValues(typeof(Keys)).Cast<Keys>();

            // default times to add is one time.
            times_to_add = 1;

            txtTimesToAdd.Text = times_to_add.ToString();


            if (form_action != null)
            {
                // if editing, select current values of existing action
                cbPressType.SelectedItem  = form_action.type.ToString();

                if (form_action.type.ToString() == "KeyDown" ||
                    form_action.type.ToString() == "KeyUp" ||
                    form_action.type.ToString() == "KeyPress")
                {
                    cbPressValue.SelectedItem = (Keys)Enum.Parse(typeof(Keys), form_action.value.ToString());
                }
                else if (form_action.type.ToString() == "MouseKeyDown" ||
                    form_action.type.ToString() == "MouseKeyUp" ||
                    form_action.type.ToString() == "MouseKeyPress")
                {
                    cbPressValue.SelectedItem =
                        (InputManager.Mouse.MouseKeys)Enum.Parse(typeof(InputManager.Mouse.MouseKeys),
                        form_action.value.ToString());
                }
                else
                {
                    // Something went wrong, just set default values and throw a warning.
                    MessageBox.Show("WARNING: Editing could set current values of this action.");
                }
                // Hide multiple add (since this is an edit on single item)
                chckMultiAdd.Visible = false;
                txtTimesToAdd.Visible = false;
                
                btnAdd.Text = "Edit";
            }
            else
            {
 
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
            // Number of Times to Add particular action
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
                MessageBox.Show("Times to add value must be a valid integer greater than one.");
                return;
            }

            // Build the action from current form values
            Type new_action_type = Type.GetType(
                "GAVPI." + cbPressType.SelectedItem.ToString());
            
            // Set the form action, overriting it with newly created action object.
            form_action = (Action)Activator.CreateInstance(
                new_action_type, cbPressValue.SelectedItem.ToString());

            this.DialogResult = DialogResult.OK;

            this.Close();

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            form_action = null;

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
