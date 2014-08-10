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
    public partial class frmAddtoTriggerEvent : Form
    {
        VI_Profile profile;
        VI_TriggerEvent event_to_add;

        public frmAddtoTriggerEvent(VI_Profile profile, VI_TriggerEvent event_to_add)
        {
            InitializeComponent();
            MaximizeBox = false;
            this.profile = profile;
            this.event_to_add = event_to_add;

            cbAddtoTriggerEvent.DataSource = profile.Profile_Triggers.ToArray();
            cbAddtoTriggerEvent.DisplayMember = "name";
            
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            VI_Trigger selected_trigger = cbAddtoTriggerEvent.SelectedItem as VI_Trigger;

            //if (selected_trigger.TriggerEvents.Contains(event_to_add))
            //{
            //    // Ignore Doubles
            //    //MessageBox.Show("Trigger " + selected_trigger.name + " already contains " + event_to_add.name);
            //    //return;
            //}
            if ( (event_to_add.GetType().Name != "VI_Action_Sequence") && event_to_add.name == selected_trigger.name)
            {
                MessageBox.Show("You cannot add a trigger to itself.");
                return;
             }
            else
            {
                selected_trigger.TriggerEvents.Add(event_to_add);
                this.Close();
            }

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
