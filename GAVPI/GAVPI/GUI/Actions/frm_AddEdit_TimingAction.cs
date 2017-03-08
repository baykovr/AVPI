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
    public partial class frm_AddEdit_TimingAction : Form
    {
        private Action form_action;

        private int time_value;

        private int times_to_add;

        public Action get_action()
        {
            return form_action;
        }

        public int get_times_to_add()
        {
            return times_to_add;
        }

        public frm_AddEdit_TimingAction()
        {
            InitializeComponent();

            populate_fields();
        }

        public frm_AddEdit_TimingAction(Action action)
        {
            InitializeComponent();

            this.form_action = action;

            populate_fields();
        }

        private void populate_fields()
        {
            time_value = 1;
            times_to_add = 1;

            // Editing Existing
            if (form_action != null)
            {
                txtValue.Text = form_action.value;

                btnAdd.Text = "Edit";

                chckMultiAdd.Visible = false;
                txtTimesToAdd.Visible = false;
            }
            else
            {
                txtValue.Text = time_value.ToString();
            }
            txtTimesToAdd.Text = times_to_add.ToString();
 
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

            
            if (String.IsNullOrEmpty(txtValue.Text))
            {
                MessageBox.Show("Value cannot be blank.");
                return;
            }
            
            if (Int32.TryParse(txtValue.Text, out time_value))
            {
                if (time_value < 0)
                {
                    MessageBox.Show("Value cannot be negative.");
                    return;
                }
                else
                {
                    // I am aware the casting is very redudant.
                    // TODO : Reduce casting string -> int -> string -> int
                    Type new_action_type = Type.GetType("GAVPI.Wait");
                    form_action = (Action)Activator.CreateInstance(
                        new_action_type, time_value.ToString());
                    
                    this.DialogResult = DialogResult.OK;

                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Value must be an integer.");
                return;
            }

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
