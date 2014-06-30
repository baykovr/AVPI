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
    public partial class frmProfile : Form
    {
        VI_Profile profile;
        public frmProfile(VI_Profile profile)
        {
            InitializeComponent();
            this.profile = profile;

            refresh_dgTriggers();
            refresh_dgActionSequences();
        }
        private void refresh_dgTriggers()
        {
            dgTriggers.DataSource = null;
            dgTriggers.DataSource = profile.Profile_Triggers.ToArray();

        }
        private void refresh_dgActionSequences()
        {
            dgActionSequences.DataSource = null;
            dgActionSequences.DataSource = profile.Profile_ActionSequences.ToArray();
        }
        private void refresh_dgTriggerEvents()
        {
            List<VI_TriggerEvent> test = new List<VI_TriggerEvent>();
            foreach (DataGridViewRow row in this.dgTriggers.SelectedRows)
            {
                VI_Trigger selected_trigger = row.DataBoundItem as VI_Trigger;
                if (selected_trigger != null)
                {
                    foreach (VI_Trigger meta_trigger in selected_trigger.Triggers)
                    {
                        test.Add(meta_trigger);
                    }
                    foreach (VI_Action_Sequence meta_sequence in selected_trigger.Action_Sequences)
                    {
                        test.Add(meta_sequence);
                    }
                }
            }
            dgTriggerEvents.DataSource = null;
            dgTriggerEvents.DataSource = test.ToArray();
        }
        private void dgTriggers_SelectionChanged(object sender, EventArgs e)
        {
            refresh_dgTriggerEvents();
        }
        private void addNewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmTrigger newTrigger = new frmTrigger(profile);
            newTrigger.ShowDialog();
            refresh_dgTriggers();
        }

        private void addNewToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmActionSequence newActionSequence = new frmActionSequence(profile);
            newActionSequence.ShowDialog();

            refresh_dgActionSequences();
        }

        #region ActionSequences Context
        private void taddtoeventToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //--wow there: multi select is off, it better stay that way
            if (dgActionSequences.MultiSelect == true)
            {
                throw new NotImplementedException("Adding multiple sequences at once is unsupported.");
            }
            foreach (DataGridViewRow row in this.dgActionSequences.SelectedRows)
            {
                VI_Action_Sequence sequence_to_add = row.DataBoundItem as VI_Action_Sequence;
                frmAddtoTriggerEvent newAddtoTriggerEvent = new frmAddtoTriggerEvent(profile, sequence_to_add);
                newAddtoTriggerEvent.ShowDialog();
            }
            refresh_dgTriggerEvents();
        }
        private void editToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (dgActionSequences.MultiSelect == true)
            {
                throw new NotImplementedException("Multiple sequences at once is unsupported.");
            }
            foreach (DataGridViewRow row in this.dgActionSequences.SelectedRows)
            {
                VI_Action_Sequence sequence_to_edit = row.DataBoundItem as VI_Action_Sequence;

                frmActionSequence newActionSequence = new frmActionSequence(profile, sequence_to_edit);
                newActionSequence.ShowDialog();

                
            }
            refresh_dgActionSequences();
            refresh_dgTriggerEvents();
        }
        private void deleteToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }
        #endregion
        

        #region Triggers Context
        #endregion

    }
}
