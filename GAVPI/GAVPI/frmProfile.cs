using System;
using System.IO;
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

        //
        //  private void frmProfile_Load( object, EventArgs )
        //
        //  Prior to frmProfile being rendered, we may dynamically change the availability of, or modify the content of, UI
        //  elements that offer indicators of the user's workflow.
        //

        private void frmProfile_Load( object sender, EventArgs e )
        {

            //  If we are re-modifying an existing Profile that has pending changes, ensure we re-enable File->Save and (if the
            //  Profile has already been saved - and named) the File->File As menu items.

            if( profile.IsEdited() ) ProfileEdited();

            btmStatusProfile.Text = ( profile.IsEdited() ? "[UNSAVED] " : "") + Path.GetFileNameWithoutExtension( profile.ProfileFilename );           

        }  //  private void frmProfile_Load()

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
             	
				if( newAddtoTriggerEvent.ShowDialog() == DialogResult.OK ) ProfileEdited();
            }
			
            refresh_dgTriggerEvents();
        }
        // Add New Action Sequence (top menu)
        private void addNewToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmActionSequence newActionSequence = new frmActionSequence(profile);
			
            if( newActionSequence.ShowDialog() == DialogResult.OK ) ProfileEdited();
            
			refresh_dgActionSequences();
			
        }
       
        // Add New Action Sequence from Context
        private void newToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmActionSequence newActionSequence = new frmActionSequence(profile);
			
            if( newActionSequence.ShowDialog() == DialogResult.OK ) ProfileEdited();
            
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
				
				if( newActionSequence.ShowDialog() == DialogResult.OK ) ProfileEdited();				
				
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
			
			ProfileEdited();
			
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
                
				if( newAddtoTriggerEvent.ShowDialog() == DialogResult.OK ) ProfileEdited();
				
            }
            refresh_dgTriggerEvents();
        }
        private void phraseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmTrigger newTrigger = new frmTrigger(profile);
            
			if( newTrigger.ShowDialog() == DialogResult.OK ) {
			
				ProfileEdited();
			
				refresh_dgTriggers();
				
			}  //  if()
				
        }
        // Add New Trigger (Right Click Context Menu)
        private void newStripMenuItem_Click(object sender, EventArgs e)
        {
            frmTrigger newTrigger = new frmTrigger(profile);
			
            if( newTrigger.ShowDialog() == DialogResult.OK ) {
			
				ProfileEdited();
			
				refresh_dgTriggers();
				
			}  //  if()
			
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
					
                    if( newTrigger.ShowDialog() == DialogResult.OK ) {

                        ProfileEdited();
										
						refresh_dgTriggers();
						
					}  //  if()
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

                    ProfileEdited();
								
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
            VI_Trigger selected_trigger = null;
            foreach (DataGridViewRow row in this.dgTriggers.SelectedRows)
            {
                 selected_trigger = row.DataBoundItem as VI_Trigger;
            }
            if (selected_trigger != null)
            {
                foreach (DataGridViewRow row in this.dgTriggerEvents.SelectedRows)
                {
                    VI_TriggerEvent selected_event = row.DataBoundItem as VI_TriggerEvent;
                    if (selected_event != null)
                    {
                        //Remove Event
                        selected_trigger.Remove(selected_event);
                        
                    }
                }
            }
			
			ProfileEdited();
			
            refresh_dgTriggers();
            refresh_dgTriggerEvents();
        }
        #endregion
        #region File
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {

            //  Let's refer to our general file saving handler....

            saveAsToolStripMenuItem_Click(sender, e);

        }
		
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if( !profile.save_profile() ) return;
					
            refresh_dgActionSequences();
            refresh_dgTriggers();
            refresh_dgTriggerEvents();

            //  Update the Form's status to reflect the Profile's state.

            btmStatusProfile.Text = Path.GetFileNameWithoutExtension(profile.ProfileFilename);	

        }
        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {

            //  Attempt to load a Profile and then update the Form's UI elements and status.  (The Form's status bar should
            //  display the name of the opened Profile.)

            if( !profile.load_profile() ) return;

             refresh_dgActionSequences();
             refresh_dgTriggers();
             refresh_dgTriggerEvents();

             btmStatusProfile.Text = ( profile.IsEdited() ? "[UNSAVED] " : "" ) + Path.GetFileNameWithoutExtension( profile.ProfileFilename );

        }

        //
        //  private void newToolStripMenuItem_Click( object, EventArgs )
        //
        //  Selecting the File->New menu item destroys any current Profile and establishes an empty Profile.
        //

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {

            //  Request a new profile...

            if( !profile.NewProfile() ) return;

            //  Refresh the UI...

            refresh_dgActionSequences();
            refresh_dgTriggers();
            refresh_dgTriggerEvents();

            //  The Profile hasn't been edited, so there are no changes to save, therefore let's disable the File menu's
            //  Save As and Save buttons for now.

            saveAsToolStripMenuItem.Enabled = false;
            saveToolStripMenuItem.Enabled = false;
            btmStatusProfile.Text = null;


        }  //  private void newToolStripMenuItem_Click( object, EventArgs )

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {

            this.Close();

        }
        #endregion

        private void stripProfileHelp_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Nothing here yet");
        }

        //
        //  private void ProfileEdited()
		//
		//  To maintain a consistent workflow, let's recognise when the current Profile has been edited by the user.
		//  We'll update the state within each method that adds or removes aspects of the Profile that require
		//  persistence.
		//
	
		private void ProfileEdited()
		{
		
            //
			//  Changes have been made to the Profile.  If the Profile is named, ensure File->Save menu item is
			//  enabled.  Also ensure that File->Save As menu item is enabled.  We also need to indicate to the user,
            //  in the Form's status bar, that the Profile is presently unsaved.
			//

            if( profile.IsEmpty() ) {

                btmStatusProfile.Text = null;

                return;

            }

			if( profile.ProfileFilename != null ) this.saveToolStripMenuItem.Enabled = true;

            profile.Edited();

			this.saveAsToolStripMenuItem.Enabled = true;

            btmStatusProfile.Text = "[UNSAVED] " + Path.GetFileNameWithoutExtension(profile.ProfileFilename);

		}  //  private void ProfileEdited()
        
    }
}
