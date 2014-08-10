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
    public partial class frmTrigger : Form
    {
        private VI_Profile profile;
        private VI_Trigger trigger_to_edit;

        public frmTrigger(VI_Profile profile)
        {
            // Constructor passes no trigger, meaning create new
            InitializeComponent();
            this.profile = profile;
            populate_fields();
        }
        public frmTrigger(VI_Profile profile, VI_Trigger trigger_to_edit)
        {
            // Passing a trigger to the contructor edits an existing
            InitializeComponent();
            this.profile = profile;
            this.trigger_to_edit = trigger_to_edit;
            populate_fields();
        }
        private void populate_fields()
        {
            cbTriggerType.DataSource = this.profile.Trigger_Types;
            // If trigger to edit is not null, we are editing an existing trigger
            if (trigger_to_edit != null)
            {
                txtTriggerName.Text = trigger_to_edit.name;
                txtTriggerValue.Text = trigger_to_edit.value;
            }
        }

        private void btnTriggerCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnTriggerOk_Click(object sender, EventArgs e)
        {
            // Validate fields for name and value
            string trigger_name  = txtTriggerName.Text.Trim();
            string trigger_value = txtTriggerValue.Text.Trim();

            if ((trigger_name.Length == 0) || (trigger_value.Length == 0)){
                MessageBox.Show("Blank name and/or value cannot be blank");
                return;
            }
            
            // Handle Trigger Edit/Insertion
            // Case 1 : New Trigger
            if (trigger_to_edit == null)
            {
                // check if name OR value is taken
                // if not push new trigger into profile
                if (profile.isTriggerNameTaken(trigger_name) || profile.isTriggerValueTaken(trigger_value))
                {
                    MessageBox.Show("A trigger with this name or value already exisits.");
                    return;
                }
                else
                {
                    // Get type from dropdown and cast to object dynamically
                    Type new_trigger_type = Type.GetType("GAVPI." + cbTriggerType.SelectedItem.ToString());
                    object trigger_instance = Activator.CreateInstance(new_trigger_type, trigger_name, trigger_value);
                    profile.Profile_Triggers.Add((VI_Trigger)trigger_instance);
                }
            }
            // Case 2 : Edit existing non-null initialized trigger
            else
            {
                // A bit more complex
                // rm current from profile
                // if name OR value is taken
                //  insert current back in unchanged
                // else
                //  edit current trigger to have new name and value
                //  push it into profile
                profile.Profile_Triggers.Remove(trigger_to_edit);

                if (profile.isTriggerNameTaken(trigger_name) || profile.isTriggerValueTaken(trigger_value))
                {
                    MessageBox.Show("A trigger with this name or value already exisits.");
                    profile.Profile_Triggers.Add(trigger_to_edit);
                    return;
                }
                else
                {
                    trigger_to_edit.name = trigger_name;
                    trigger_to_edit.value = trigger_value;
                    profile.Profile_Triggers.Add(trigger_to_edit);
                }
            }
            this.Close();
        }
    }
}
