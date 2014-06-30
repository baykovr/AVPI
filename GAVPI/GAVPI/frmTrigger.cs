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
        public frmTrigger(VI_Profile profile)
        {
            InitializeComponent();
            this.profile = profile;
            cbTriggerType.DataSource = this.profile.Trigger_Types;

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
            foreach(VI_Trigger trigger in profile.Profile_Triggers)
            {
                if (trigger.name == new_trigger_name)
                {
                    MessageBox.Show("A trigger with name " + new_trigger_name + " already exists.");
                    return;
                }
            }

            // Additional validation by type, ie cant have duplicate phrases
            // todo: abstract to VI_Profile.validate(vi_type,name,value)
            if ("VI_Phrase" == cbTriggerType.SelectedItem.ToString())
            {
                foreach (VI_Trigger trigger in profile.Profile_Triggers)
                {
                    if (trigger.value == new_trigger_value)
                    {
                        MessageBox.Show("Trigger "+ trigger.name+" already has value "+ new_trigger_value);
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
