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
    public partial class frm_AddEdit_NestAction : Form
    {

        #region Globals

        private Action form_action;

        private int times_to_add;

        #endregion

        #region Globals : Fetchers

        public Action get_action()
        {
            return form_action;
        }

        public int get_times_to_add()
        {
            return times_to_add;
        }
        #endregion

        #region Constructors
        public frm_AddEdit_NestAction()
        {
            InitializeComponent();

            populate_fields();
        }
        public frm_AddEdit_NestAction(Action action)
        {
            InitializeComponent();

            this.form_action = action;

            populate_fields();
        }
        #endregion

        #region UI : Populate 
        private void populate_fields()
        {
            //default times to add is one time
            times_to_add = 1;

            txtTimesToAdd.Text = times_to_add.ToString();

            cbNestAction.DataSource = GAVPI.Profile.Profile_ActionSequences;
            cbNestAction.DisplayMember = "name";
            cbNestAction.ValueMember = "name";

            // Editing an existing action
            if (form_action != null)
            {
                chckMultiAdd.Visible = false;
                txtTimesToAdd.Visible = false;
                btnAdd.Text = "Edit";

                refresh_comboboxes();

                cbNestAction.SelectedText = form_action.value;


            }
            // New Action
            else
            {


            }
        }
        #endregion

        #region UI : Press Events : Add/Edit : Cancel : Drop Down
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

            // Validate Combobox value
            if (String.IsNullOrWhiteSpace(cbNestAction.Text))
            {
                MessageBox.Show("Value cannot be blank.");
                return;
            }


            // Build the action from current form values
            Type new_action_type = Type.GetType(
                "GAVPI.Nest");


            form_action = (Action)Activator.CreateInstance(new_action_type, cbNestAction.Text);

            this.DialogResult = DialogResult.OK;

            this.Close();

        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void cbNestAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            refresh_comboboxes();
        }
        #endregion

        #region UI : Refresh
        private void refresh_comboboxes()
        {
            cbNestAction.DropDownStyle = ComboBoxStyle.DropDownList;
            cbNestAction.DataSource = GAVPI.Profile.Profile_ActionSequences;
            cbNestAction.DisplayMember = "name";
            cbNestAction.ValueMember = "name";

        }
        #endregion

        #region UI : CheckBoxes : Multiple Add
        private void chckMultiAdd_CheckedChanged(object sender, EventArgs e)
        {
            if (chckMultiAdd.Checked == true)
            {
                txtTimesToAdd.Enabled = true;
            }
            else
            {
                txtTimesToAdd.Enabled = false;
            }
        }
        #endregion
    }
}
