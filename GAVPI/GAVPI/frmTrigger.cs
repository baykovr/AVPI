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
        private VI_Trigger existing_trigger;
        private bool edit_mode; 

        public frmTrigger(VI_Profile profile)
        {
            InitializeComponent();
            this.profile = profile;
            edit_mode = false;
            populate_fields();
        }
        public frmTrigger(VI_Profile profile, VI_Trigger existing_trigger)
        {
            InitializeComponent();
            this.profile = profile;
            this.existing_trigger = existing_trigger;
            edit_mode = true;
            populate_fields();
        }
        private void populate_fields()
        {
            cbTriggerType.DataSource = this.profile.Trigger_Types;
            if (edit_mode)
            {
                txtTriggerName.Text = existing_trigger.name;
                txtTriggerValue.Text = existing_trigger.value;
            }
        }

        private void btnTriggerCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnTriggerOk_Click(object sender, EventArgs e)
        {
            // Check if exists with name
            string new_trigger_name = txtTriggerName.Text.Trim();
            string new_trigger_value = txtTriggerValue.Text.Trim();

            if ( (new_trigger_name.Length == 0) ||  (new_trigger_value.Length == 0))
            {
                MessageBox.Show("Blank values not allowed");
                return;
            }

            // Additional validation by type, ie cant have duplicate phrases
            // todo: abstract to VI_Profile.validate(vi_type,name,value)

            // Check existings, remove if exisits (in which case the user is making an edit)
            foreach(VI_Trigger current_trigger in profile.Profile_Triggers)
            {
                if (new_trigger_name == current_trigger.name)
                {
                    if ("VI_Phrase" == cbTriggerType.SelectedItem.ToString())
                    {
                        foreach (VI_Trigger trigger in profile.Profile_Triggers)
                        {
                            if ( (trigger.value == new_trigger_value) && (trigger.name != current_trigger.name) )
                            {
                                MessageBox.Show("Trigger " + trigger.name + " currently uses value " + new_trigger_value);
                                return;
                            }
                        }
                    }
                    else
                    {
                        profile.Profile_Triggers.Remove(current_trigger);
                        break;
                    }
                }
            }
            if ("VI_Phrase" == cbTriggerType.SelectedItem.ToString())
            {
                foreach (VI_Trigger trigger in profile.Profile_Triggers)
                {
                    if (trigger.value == new_trigger_value)
                    {
                        MessageBox.Show("Trigger " + trigger.name + " currently uses value " + new_trigger_value);
                        return;
                    }
                }
            }

            Type new_trigger_type = Type.GetType("GAVPI." + cbTriggerType.SelectedItem.ToString());
            object trigger_instance = Activator.CreateInstance(new_trigger_type, new_trigger_name, new_trigger_value);

            profile.Profile_Triggers.Add((VI_Trigger)trigger_instance);
            this.Close();
        }
    }
}
