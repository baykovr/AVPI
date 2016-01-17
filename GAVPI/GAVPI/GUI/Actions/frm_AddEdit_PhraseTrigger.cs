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
    public partial class frm_AddEdit_PhraseTrigger : Form
    {
     
        private Trigger trigger_to_edit;

        public frm_AddEdit_PhraseTrigger()
        {
            // Constructor passes no trigger, meaning create new
            InitializeComponent();
            populate_fields();
        }
        public frm_AddEdit_PhraseTrigger( Trigger trigger_to_edit )
        {
            // Passing a trigger to the contructor edits an existing
            InitializeComponent();
            this.trigger_to_edit = trigger_to_edit;
            populate_fields();
        }
        private void populate_fields()
        {
            //cbTriggerType.DataSource = GAVPI.Profile.Trigger_Types;
            // If trigger to edit is not null, we are editing an existing trigger
            if (trigger_to_edit != null)
            {
                txtTriggerName.Text = trigger_to_edit.name;
                txtTriggerValue.Text = trigger_to_edit.value;
                txtTriggerComment.Text = trigger_to_edit.comment;
            }
        }

        private void btnTriggerCancel_Click(object sender, EventArgs e)
        {

			this.DialogResult = DialogResult.Cancel;

            this.Close();
			
        }

        private void btnTriggerOk_Click(object sender, EventArgs e)
        {
            // This causes edit to close the form if validation fails.
			//this.DialogResult = DialogResult.Cancel;  //  Unless otherwise stated, the Dialog was cancelled by the user.

            // Validate fields for name and value
            string trigger_name  = txtTriggerName.Text.Trim();
            string trigger_value = txtTriggerValue.Text.Trim();
            string trigger_comment = txtTriggerComment.Text.Trim();

            if ((trigger_name.Length == 0) || (trigger_value.Length == 0)){
                MessageBox.Show("Trigger name or value cannot be blank.");
								
                return;
            }
            
            // Handle Trigger Edit/Insertion
            // Case 1 : New Trigger
            if (trigger_to_edit == null)
            {
                // check if name OR value is taken
                // if not push new trigger into profile
                if ( GAVPI.Profile.isTriggerNameTaken(trigger_name) || GAVPI.Profile.isTriggerValueTaken(trigger_value))
                {
                    MessageBox.Show("A trigger with this name or value already exisits.");
										
                    return;
                }
                else
                {
                    // Get type from dropdown and cast to object dynamically
                    //Type new_trigger_type = Type.GetType("GAVPI." + cbTriggerType.SelectedItem.ToString());
                    // TODO logicals
                    Type new_trigger_type = Type.GetType("GAVPI.VI_Phrase");
                    object trigger_instance = Activator.CreateInstance(new_trigger_type, trigger_name, trigger_value, trigger_comment);
                    GAVPI.Profile.Profile_Triggers.Add((Trigger)trigger_instance);
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
                GAVPI.Profile.Profile_Triggers.Remove(trigger_to_edit);

                if( GAVPI.Profile.isTriggerNameTaken(trigger_name) || GAVPI.Profile.isTriggerValueTaken(trigger_value ) )
                {
                    MessageBox.Show("A trigger with this name or value already exisits.");
                    GAVPI.Profile.Profile_Triggers.Add(trigger_to_edit);
									
                    return;
                }
                else
                {
                    trigger_to_edit.name = trigger_name;
                    trigger_to_edit.value = trigger_value;
                    trigger_to_edit.comment = trigger_comment;
                    GAVPI.Profile.Profile_Triggers.Add(trigger_to_edit);
                }
            }
			
            //  The user okay'd their edit, so the Dialog successfully accomplished something.

			this.DialogResult = DialogResult.OK;
			
            this.Close();
			
        }  //  private void btnTriggerOk_Click()
    }
}
