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
        VI_Action_Sequence sequence_to_add;

        public frmAddtoTriggerEvent(VI_Profile profile,VI_Action_Sequence sequence_to_add)
        {
            InitializeComponent();
            this.profile = profile;
            this.sequence_to_add = sequence_to_add;

            cbAddtoTriggerEvent.DataSource = profile.Profile_Triggers.ToArray();
            cbAddtoTriggerEvent.DisplayMember = "name";
            
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            VI_Trigger selected_trigger = cbAddtoTriggerEvent.SelectedItem as VI_Trigger;

            if (selected_trigger.Action_Sequences.Contains(sequence_to_add))
            {
                MessageBox.Show("Trigger " + selected_trigger.name + " already contains action sequence " + sequence_to_add.name);
                return;
            }
            else
            {
                selected_trigger.Action_Sequences.Add(sequence_to_add);
                this.Close();
            }

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
