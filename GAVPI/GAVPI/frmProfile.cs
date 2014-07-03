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
            List<VI_TriggerEvent> dg_data_trigger_events = new List<VI_TriggerEvent>();
            foreach (DataGridViewRow row in this.dgTriggers.SelectedRows)
            {
                VI_Trigger selected_trigger = row.DataBoundItem as VI_Trigger;
                if (selected_trigger != null)
                {
                    foreach (VI_TriggerEvent trigger_event in selected_trigger.TriggerEvents)
                    {
                        dg_data_trigger_events.Add(trigger_event);
 
                    }
                }
            }
            dgTriggerEvents.DataSource = null;
            dgTriggerEvents.DataSource = dg_data_trigger_events.ToArray();
        }
        private void dgTriggers_SelectionChanged(object sender, EventArgs e)
        {
            refresh_dgTriggerEvents();
        }
        

        

        #region ActionSequences Context
        // Add Action Sequence to Existing Trigger
        private void taddtoeventToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (dgActionSequences.MultiSelect == true)
            {
                throw new NotImplementedException("Adding multiple sequences at once is unsupported.");
            }
            foreach (DataGridViewRow row in this.dgActionSequences.SelectedRows)
            {
                //In this case an Action_Sequence is added to a Trigger's TriggerEvents List
                VI_TriggerEvent event_to_add = row.DataBoundItem as VI_TriggerEvent;
                frmAddtoTriggerEvent newAddtoTriggerEvent = new frmAddtoTriggerEvent(profile, event_to_add);
                newAddtoTriggerEvent.ShowDialog();
            }
            refresh_dgTriggerEvents();
        }
        // Add New Action Sequence (top menu)
        private void addNewToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmActionSequence newActionSequence = new frmActionSequence(profile);
            newActionSequence.ShowDialog();
            refresh_dgActionSequences();
        }
       
        // Add New Action Sequence from Context
        private void newToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmActionSequence newActionSequence = new frmActionSequence(profile);
            newActionSequence.ShowDialog();
            refresh_dgActionSequences();
        }
        // Edit Action Sequence
        private void editToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (dgActionSequences.MultiSelect == true)
            {
                throw new NotImplementedException("Editing multiple sequences at once is unsupported.");
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
        // Delete Action Sequence
        private void deleteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (dgActionSequences.MultiSelect == true)
            {
                throw new NotImplementedException("Multiple sequence deletions are unsupported.");
            }
            foreach (DataGridViewRow row in this.dgActionSequences.SelectedRows)
            {
                VI_Action_Sequence sequence_to_remove = row.DataBoundItem as VI_Action_Sequence;
                profile.Profile_ActionSequences.Remove(sequence_to_remove);
                
                // Remove this event from existing triggers.
                foreach (VI_Trigger existing_trigger in profile.Profile_Triggers)
                {
                    existing_trigger.TriggerEvents.Remove(sequence_to_remove);
                }
            }
            refresh_dgActionSequences();
            refresh_dgTriggerEvents();
        }
        #endregion


        #region Triggers Context
        // Add Trigger to TriggerEvent
        private void addtoeventToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (dgTriggers.MultiSelect == true)
            {
                throw new NotImplementedException("Adding multiple triggers at once is unsupported.");
            }
            foreach (DataGridViewRow row in this.dgTriggers.SelectedRows)
            {
                // In this case it is a Trigger -> Trigger addition
                VI_TriggerEvent event_to_add = row.DataBoundItem as VI_TriggerEvent;
                frmAddtoTriggerEvent newAddtoTriggerEvent = new frmAddtoTriggerEvent(profile, event_to_add);
                newAddtoTriggerEvent.ShowDialog();
            }
            refresh_dgTriggerEvents();
        }
        // Add New Trigger (Top Menu)
        private void addNewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmTrigger newTrigger = new frmTrigger(profile);
            newTrigger.ShowDialog();
            refresh_dgTriggers();
        }
        // Add New Trigger (Right Click Context Menu)
        private void newStripMenuItem_Click(object sender, EventArgs e)
        {
            frmTrigger newTrigger = new frmTrigger(profile);
            newTrigger.ShowDialog();
            refresh_dgTriggers();
        }
        // Edit Trigger
        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgTriggers.MultiSelect == true)
            {
                throw new NotImplementedException("Editing mutliple triggers at once is unsupported.");
            }
            foreach (DataGridViewRow row in this.dgTriggers.SelectedRows)
            {
                VI_Trigger selected_trigger = row.DataBoundItem as VI_Trigger;
                if (selected_trigger != null)
                {
                    frmTrigger newTrigger = new frmTrigger(profile, selected_trigger);
                    newTrigger.ShowDialog();
                    refresh_dgTriggers();
                }
            }
        }
        // Delete Trigger
        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgTriggers.MultiSelect == true)
            {
                throw new NotImplementedException("Editing mutliple triggers at once is unsupported.");
            }
            foreach (DataGridViewRow row in this.dgTriggers.SelectedRows)
            {
                VI_Trigger selected_trigger = row.DataBoundItem as VI_Trigger;
                if (selected_trigger != null)
                {
                    profile.Profile_Triggers.Remove(selected_trigger);

                    // Remove refernces of this trigger from existing triggers
                    foreach (VI_Trigger existing_trigger in profile.Profile_Triggers)
                    {
                        existing_trigger.TriggerEvents.Remove(selected_trigger);
                    }
                    refresh_dgTriggers();
                }
            }
        }
        #endregion

        
        #region Trigger Events
        private void deleteToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (dgTriggerEvents.MultiSelect == true)
            {
                throw new NotImplementedException("Editing mutliple triggers at once is unsupported.");
            }
            foreach (DataGridViewRow row in this.dgTriggerEvents.SelectedRows)
            {
                VI_Trigger selected_trigger = row.DataBoundItem as VI_Trigger;
                if (selected_trigger != null)
                {
                    profile.Profile_Triggers.Remove(selected_trigger);

                    // Remove any references to the removed trigger from other triggers
                    foreach (VI_Trigger current_trigger in profile.Profile_Triggers)
                    {
                        current_trigger.TriggerEvents.Remove(selected_trigger);
                    }
                    refresh_dgTriggers();
                }
            }
        }
        #endregion
        #region File
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //TODO : file picker
            profile.save_profile("test.xml");
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion


        
    }
}
