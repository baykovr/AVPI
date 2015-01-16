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
		
        public frmProfile()
        {
            InitializeComponent();

            refresh_dgTriggers();
            refresh_dgActionSequences();
            refresh_dgDatabase();
            refresh_dgTriggerEvents();
        }

        //
        //  private void frmProfile_Load( object, EventArgs )
        //
        //  Prior to frmProfile being rendered, we may dynamically change the availability of, or modify the
        //  content of, UI elements that offer indicators of the user's workflow.
        //

        private void frmProfile_Load( object sender, EventArgs e )
        {

            //  If we are re-modifying an existing Profile that has pending changes, ensure we re-enable
            //  File->Save and (if the Profile has already been saved - and named) the File->File As menu items.

            if( GAVPI.vi_profile.IsEdited() ) ProfileEdited();

            //  Reflect the Profile's current state in the Status Bar...

            btmStatusProfile.Text = GAVPI.GetStatusString();          

            //  ...And display the name of an associated executable in the relevant text box.

            AssociatedProcessTextBox.Text = GAVPI.vi_profile.GetAssociatedProcess();

        }  //  private void frmProfile_Load()

        private void refresh_dgTriggers()
        {
            dgTriggers.DataSource = null;
            dgTriggers.DataSource = GAVPI.vi_profile.Profile_Triggers.ToList();

        }
        private void refresh_dgActionSequences()
        {
            dgActionSequences.DataSource = null;
            dgActionSequences.DataSource = GAVPI.vi_profile.Profile_ActionSequences.ToList();
        }
        private void refresh_dgDatabase()
        {
            dgDatabase.DataSource = null;
            dgDatabase.DataSource = GAVPI.vi_profile.ProfileDB.DB.Values.ToList();
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
            dgTriggerEvents.DataSource = dg_data_trigger_events.ToList();
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
                frm_Add_to_TriggerEvent newAddtoTriggerEvent = new frm_Add_to_TriggerEvent( event_to_add );
             	
				if( newAddtoTriggerEvent.ShowDialog() == DialogResult.OK ) ProfileEdited();
            }
			
            refresh_dgTriggerEvents();
        }
        // Add New Action Sequence (top menu)
        private void addNewToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frm_AddEdit_ActionSequence newActionSequence = new frm_AddEdit_ActionSequence();
			
            if( newActionSequence.ShowDialog() == DialogResult.OK ) ProfileEdited();
            
			refresh_dgActionSequences();
			
        }
       
        // Add New Action Sequence from Context
        private void newToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frm_AddEdit_ActionSequence newActionSequence = new frm_AddEdit_ActionSequence();
			
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
                frm_AddEdit_ActionSequence newActionSequence = new frm_AddEdit_ActionSequence( sequence_to_edit );
				
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
                GAVPI.vi_profile.Profile_ActionSequences.Remove(sequence_to_remove);
                
                // Remove this event from existing triggers.
                foreach (VI_Trigger existing_trigger in GAVPI.vi_profile.Profile_Triggers)
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
                frm_Add_to_TriggerEvent newAddtoTriggerEvent = new frm_Add_to_TriggerEvent( event_to_add );
                
				if( newAddtoTriggerEvent.ShowDialog() == DialogResult.OK ) ProfileEdited();
				
            }
            refresh_dgTriggerEvents();
        }
        private void phraseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frm_AddEdit_PhraseTrigger newTrigger = new frm_AddEdit_PhraseTrigger();
            
			if( newTrigger.ShowDialog() == DialogResult.OK ) {
			
				ProfileEdited();
			
				refresh_dgTriggers();
				
			}  //  if()
				
        }
        
        /*Right Click Context Menu*/
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
                    frm_AddEdit_PhraseTrigger newTrigger = new frm_AddEdit_PhraseTrigger( selected_trigger );
					
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
                    GAVPI.vi_profile.Profile_Triggers.Remove(selected_trigger);

                    // Remove refernces of this trigger from existing triggers
                    foreach (VI_Trigger existing_trigger in GAVPI.vi_profile.Profile_Triggers)
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

            if( !GAVPI.SaveProfile() ) return;

            RefreshUI( Path.GetFileNameWithoutExtension( GAVPI.vi_profile.GetProfileFilename() ) );   

        }
		
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if( !GAVPI.SaveAsProfile() ) return;
					
			RefreshUI( Path.GetFileNameWithoutExtension( GAVPI.vi_profile.GetProfileFilename() ) );          	

        }

        private void loadToolStripMenuItem_Click( object sender, EventArgs e )
        {

            //  Attempt to load a Profile and then update the Form's UI elements and status.  (The Form's
            //  status bar should display the name of the opened Profile.)

            if( !GAVPI.LoadProfile() ) return;

        }



        //
        //  private void newToolStripMenuItem_Click( object, EventArgs )
        //
        //  Selecting the File->New menu item destroys any current Profile and establishes an empty Profile.
        //

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {

            //  Request a new profile...

            if( !GAVPI.NewProfile() ) return;

            RefreshUI( GAVPI.GetStatusString() );

            //  The Profile hasn't been edited, so there are no changes to save, therefore let's disable the
            //  File menu's Save As and Save buttons for now.

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
		//  To maintain a consistent workflow, let's recognise when the current Profile has been edited by the
        //  user.  We'll update the state within each method that adds or removes aspects of the Profile that
        //  require persistence.
		//
	
		private void ProfileEdited()
		{
		
            //
			//  Changes have been made to the Profile.  If the Profile is named, ensure File->Save menu item is
			//  enabled.  Also ensure that File->Save As menu item is enabled.  We also need to indicate to the
            //  user, in the Form's status bar, that the Profile is presently unsaved.
			//

            if( GAVPI.vi_profile.IsEmpty() ) {

                btmStatusProfile.Text = null;

                return;

            }

			if( GAVPI.vi_profile.GetProfileFilename() != null ) this.saveToolStripMenuItem.Enabled = true;

            GAVPI.vi_profile.Edited();

			this.saveAsToolStripMenuItem.Enabled = true;

            btmStatusProfile.Text = GAVPI.GetStatusString();

		}  //  private void ProfileEdited()

        private void ActionSequences_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if(e.Button == MouseButtons.Right)
            {
                this.dgActionSequences.CurrentCell = this.dgActionSequences.Rows[e.RowIndex].Cells[e.ColumnIndex];
            }
        }

        private void Triggers_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                this.dgTriggers.CurrentCell = this.dgTriggers.Rows[e.RowIndex].Cells[e.ColumnIndex];
            }
        }
        


        //
        //  public void RefreshUI( string )
        //
        //  Request that the User Interface updates any elements that are dependant on states that may change
        //  beyond the scope of the current thread of execution.  This method is typically called by way of the
        //  GAVPI class.
        //

        public void RefreshUI( string Status )
        {

            //  Refresh the UI...

            refresh_dgActionSequences();
            refresh_dgTriggers();
            refresh_dgTriggerEvents();
            refresh_dgDatabase();

            btmStatusProfile.Text = Status;

        }  //  public void RefreshUI()

        private void TriggerEvents_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                this.dgTriggerEvents.CurrentCell = this.dgTriggerEvents.Rows[e.RowIndex].Cells[e.ColumnIndex];
            }
        }

        private void phraseToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frm_AddEdit_PhraseTrigger newTrigger = new frm_AddEdit_PhraseTrigger();

            if (newTrigger.ShowDialog() == DialogResult.OK)
            {

                ProfileEdited();

                refresh_dgTriggers();

            }  //  if()
        }

        #region Database Context Strip
        
        // Add DataItem
        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frm_AddEdit_Data newData = new frm_AddEdit_Data();

            if (newData.ShowDialog() == DialogResult.OK)
            {
                ProfileEdited();
                refresh_dgDatabase();
            }  //  if()

            refresh_dgDatabase();
        }

        // Edit DataItem
        private void editToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (dgDatabase.MultiSelect == true)
            {
                throw new NotImplementedException("Editing mutliple data items at once is unsupported.");
            }
            foreach (DataGridViewRow row in this.dgDatabase.SelectedRows)
            {
                VI_Data selected_data = row.DataBoundItem as VI_Data;
                if (selected_data != null)
                {
                    frm_AddEdit_Data newData = new frm_AddEdit_Data(selected_data);
                    if (newData.ShowDialog() == DialogResult.OK)
                    {
                        ProfileEdited();
                        refresh_dgDatabase();
                    }  //  if()
                }
            }
        }

        // Delete DataItem
        private void deleteToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            if (dgDatabase.MultiSelect == true)
            {
                throw new NotImplementedException("Editing mutliple data items at once is unsupported.");
            }
            foreach (DataGridViewRow row in this.dgDatabase.SelectedRows)
            {
                VI_Data selected_data = row.DataBoundItem as VI_Data;
                if (selected_data != null)
                {
                    GAVPI.vi_profile.ProfileDB.Remove(selected_data);
                    ProfileEdited();
                    refresh_dgDatabase();
                }
            }
        }
        #endregion



        //
        //  private void AssociatedProcessButton_Click( object, EventArgs )
        //
        //
        //

        private void AssociatedProcessButton_Click( object sender, EventArgs e )
        {

            //  Select the executable whose process we may listen out for.

            using( OpenFileDialog dialog = new OpenFileDialog() ) {

                //  Give the Dialog a title then establish a default filter to hide anything that isn't
                //  an executable.

                dialog.Title = "Select an Application...";
                dialog.Filter = "Executables (*.EXE)|*.EXE|All Files (*.*)|*.*";

                dialog.RestoreDirectory = true;

                if( dialog.ShowDialog() == DialogResult.Cancel ) return;
               
                //  Add it to the text box.
            
                AssociatedProcessTextBox.Text = dialog.FileName;

            }  //  using()

        }  //  private void AssociatedProcessButton_Click( object, EventArgs )



        //
        //
        //
        //
        //

        private void AssociatedProcessTextBox_TextChanged( object sender, EventArgs e )
        {
           
            //  Assign the content of the TextBox to the Profile.

            GAVPI.vi_profile.SetAssociatedProcess( AssociatedProcessTextBox.Text );

            //  Consider this Profile edited.

           // ProfileEdited();

        }
       
    }

}
