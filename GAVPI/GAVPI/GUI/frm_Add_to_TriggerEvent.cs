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
    public partial class frm_Add_to_TriggerEvent : Form
    {
 
        Trigger_Event event_to_add;

        public frm_Add_to_TriggerEvent( Trigger_Event event_to_add )
        {
            InitializeComponent();
            MaximizeBox = false;

            this.event_to_add = event_to_add;

            cbAddtoTriggerEvent.DataSource = GAVPI.Profile.Profile_Triggers.ToArray();
            cbAddtoTriggerEvent.DisplayMember = "name";
            
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Trigger selected_trigger = cbAddtoTriggerEvent.SelectedItem as Trigger;

            //if (selected_trigger.TriggerEvents.Contains(event_to_add))
            //{
            //    // Ignore Doubles
            //    //MessageBox.Show("Trigger " + selected_trigger.name + " already contains " + event_to_add.name);
            //    //return;
            //}
            if ( (event_to_add.GetType().Name != "Action_Sequence") && event_to_add.name == selected_trigger.name)
            {
                MessageBox.Show("You cannot add a trigger to itself.");
				
				this.DialogResult = DialogResult.Cancel;
				
                return;
             }
            else
            {
                selected_trigger.TriggerEvents.Add(event_to_add);
				
				this.DialogResult = DialogResult.OK;
				
                this.Close();
            }

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
		
			this.DialogResult = DialogResult.Cancel;
		
            this.Close();
        }
    }
}
