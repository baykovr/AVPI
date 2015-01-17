using InputManager;
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
    // TODO :
    // When editing the name of a sequence
    // since we remove and then add()
    // the new name will be inserted at the end of the list..
    // changing the order in profile form.

    public partial class frm_AddEdit_ActionSequence : Form
    {
        #region Form Globals

        public bool ActionSequenceEdited;

        private VI_Action_Sequence sequence_to_edit;

        public static List<string> Action_Groups = new List<string>(
            new string[] { 
                "Key/Mouse Press",
                "Timing",
                "Speak Action",
                "Data Action"
            });
        #endregion

        #region Constructors
        public frm_AddEdit_ActionSequence()
        {
            // if no sequence is passed, a new sequence is created.
            InitializeComponent();

            populate_fields();
        }
        public frm_AddEdit_ActionSequence( VI_Action_Sequence action_sequence )
        {
            // Passing an action sequence edits the passed sequence.
            InitializeComponent();

            this.sequence_to_edit = action_sequence;
            
            populate_fields();
        }
        #endregion

        #region Logic : UI Processing Methods : Move Up/Down : Add : Edit : Remove
        // These methods are invoked by UI click/press events
        // They processes the current state the UI elements and 
        // invoke the appropriate ProcessForm Method
        // the ProcessForm Method which in turn spawns the correct form and handles its output.
        
        #region Move Up/Down
        private void moveup()
        {
            int index = 0;

            foreach (DataGridViewRow row in dgActionSequence.SelectedRows)
            {
                Action action_to_movedown = row.DataBoundItem as Action;
                
                index = sequence_to_edit.action_sequence.IndexOf(action_to_movedown);
                
                // Check if the current location is at the front
                if (index == 0)
                {
                    break;
                }
                sequence_to_edit.action_sequence.RemoveAt(index);
                
                // Check if the new location is at the front
                if (index - 1 <= 0)
                {
                    sequence_to_edit.action_sequence.Add(action_to_movedown);
                    
                    refresh_dgActionSequence();
                }
                else
                {
                    sequence_to_edit.action_sequence.Insert(index - 1, action_to_movedown);
                    
                    refresh_dgActionSequence();
                    
                    // Select the item just moved.
                    dgActionSequence.CurrentCell = dgActionSequence.Rows[index - 1].Cells[0];
                }
            }
            ActionSequenceEdited = true;
        }
        private void movedown()
        {
            foreach (DataGridViewRow row in dgActionSequence.SelectedRows)
            {
                Action action_to_movedown = row.DataBoundItem as Action;
                
                //sequence_to_edit.Remove(action_to_remove);

                int index = sequence_to_edit.action_sequence.IndexOf(action_to_movedown);
                
                // Check if the current location is at the end
                if (index == sequence_to_edit.action_sequence.Count)
                {
                    break;
                }
                sequence_to_edit.action_sequence.RemoveAt(index);
                
                // Check if the new location is at the end
                if (index + 1 >= sequence_to_edit.action_sequence.Count)
                {
                    sequence_to_edit.action_sequence.Add(action_to_movedown);
                    
                    refresh_dgActionSequence();

                    dgActionSequence.CurrentCell = dgActionSequence.Rows[dgActionSequence.RowCount-1].Cells[0];
                }
                else
                {
                    sequence_to_edit.action_sequence.Insert(index + 1, action_to_movedown);

                    refresh_dgActionSequence();

                    dgActionSequence.CurrentCell = dgActionSequence.Rows[index + 1].Cells[0];
                }
            }
            ActionSequenceEdited = true;
        }
        #endregion
        
        #region Add : Edit
        private void add()
        {
            // Add New Action
            // Switch on Action_Groups
            // Since some actions utilize the same form
            // this hides some information from the user
            // perhaps it is better to just populate the dropdown with all possible actions?
            // not sure.
            switch (cbActionType.SelectedItem.ToString())
            {
                case "Key/Mouse Press":
                    {
                        ProcessForm_AddEditPressAction(null,0);
                        break;
                    }
                case "Timing":
                    {
                        ProcessForm_TimingAction(null, 0);
                        break;
                    }
                case "Speak Action":
                    {
                        //TODO
                        ProcessForm_AddEditSpeechAction(null, 0);
                        break;
                    }
                case "Data Action":
                    {
                        //TODO
                        frm_AddEdit_DataAction newDataAction = new frm_AddEdit_DataAction();
                        if (newDataAction.ShowDialog() == DialogResult.OK)
                        {

                        }
                        break;
                    }
                default:
                    {
                        MessageBox.Show("WARNING: This action type is not implemented!");
                        break;
                    }
            }
 
        }
        private void edit()
        {
            // If for some reason multi select is on, just take the first item 
            // and a show warning.

            //TODO, for multi select just remove the items on OK
            //and insert # removed copies of edited

            foreach (DataGridViewRow row in dgActionSequence.SelectedRows)
            {
                // Pull Action out of selected DataGridView item
                Action action_to_edit = row.DataBoundItem as Action;

                //Action type, ex: "KeyDown , MouseUp, Wait, Data Increment"
                string action_type = Type.GetType(action_to_edit.ToString()).Name.ToString();

                //index of action in sequence
                int index = sequence_to_edit.action_sequence.IndexOf(action_to_edit);

                // Choose Form Method to Invoke based on type
                
                //Key/KeyPress
                if (VI_Action_Sequence.PressAction_Types.Contains(action_type))
                {
                   ProcessForm_AddEditPressAction(action_to_edit, index);
                   break;
                }
                //Speech 
                else if (VI_Action_Sequence.SpeechAction_Types.Contains(action_type))
                {
                    ProcessForm_AddEditSpeechAction(action_to_edit, index);
                    break;
                }
                //Timing (Waiting)
                else if (VI_Action_Sequence.TimingAction_Types.Contains(action_type))     
                {
                    ProcessForm_TimingAction(action_to_edit, index);
                    break;
                }
                //DataActions
                else if (VI_Action_Sequence.DataAction_Types.Contains(action_type))
                {
                }
                // Unknown
                else
                {

                }
            }
        }
        #endregion
        
        #region Remove
        private void remove()
        {
            foreach (DataGridViewRow row in dgActionSequence.SelectedRows)
            {
                Action action_to_remove = row.DataBoundItem as Action;
                sequence_to_edit.Remove(action_to_remove);
            }
            ActionSequenceEdited = true;
            refresh_dgActionSequence();
        }
        #endregion
        
        #endregion

        #region Logic : Forms Invocation Add/Edit
        // Form processing invokes handles form creation
        // Once the form exits these functions also handle refreshing/updating UI elements
        // and setting form global variables (such as if we need to save)
        // @edit_action : can either be an existing action (for editing) or null (create new)
        // @index       : index of existing action, unused if new action.

        private void ProcessForm_AddEditPressAction(Action edit_action, int index)
        {
            frm_AddEdit_PressAction newPressAction;
            // Create a new action
            if (edit_action == null)
            {
                newPressAction = new frm_AddEdit_PressAction();
            }
            // Edit an existing action
            else
            {
                newPressAction = new frm_AddEdit_PressAction(edit_action);
            }
            // Invoke Form
            if (newPressAction.ShowDialog() == DialogResult.OK)
            {
                // if OK pull out edited or new action
                if (newPressAction.get_action() != null)
                {
                        // Called by Add
                        if (edit_action == null)
                        {
                            // Insert number of times specified by the form
                            for (int i = 0; i < newPressAction.get_times_to_add(); i++)
                            {
                                sequence_to_edit.Add(newPressAction.get_action());
                            }
                        }
                        // Called by Edit
                        else
                        {
                            sequence_to_edit.action_sequence[index] = newPressAction.get_action();
                        }
                }
                else
                {
                    MessageBox.Show("WARNING: Press form returned an invalid action.");
                }
                ActionSequenceEdited = true;
                refresh_dgActionSequence();
                // Bring Selection back to edited element
                dgActionSequence.CurrentCell = dgActionSequence.Rows[index].Cells[0];
            }
        }

        private void ProcessForm_AddEditSpeechAction(Action edit_action, int index)
        {
            frm_AddEdit_SpeakAction newSpeakAction;
            if(edit_action == null)
            {
                newSpeakAction = new frm_AddEdit_SpeakAction();
            }
            else
            {
                newSpeakAction  = new frm_AddEdit_SpeakAction(edit_action);
            }

            // On form OK we have changes (either new or edited action)
            if (newSpeakAction.ShowDialog() == DialogResult.OK)
            {
                // Make sure the returned action is sane
                if (newSpeakAction.get_action() != null)
                {
                        // Called by Add
                        if (edit_action == null)
                        {
                            // Insert number of times specified by the form
                            for (int i = 0; i < newSpeakAction.get_times_to_add(); i++)
                            {
                                sequence_to_edit.Add(newSpeakAction.get_action());
                            }
                        }
                        // Called by Edit
                        else
                        {
                            // Replace the current action with the new from the form
                            sequence_to_edit.action_sequence[index] = newSpeakAction.get_action();
                        }
                }
                else
                {
                    MessageBox.Show("WARNING: Press form returned an invalid action.");
                }
                ActionSequenceEdited = true;
                refresh_dgActionSequence();
                // Bring Selection back to edited element
                dgActionSequence.CurrentCell = dgActionSequence.Rows[index].Cells[0];
            }
        }

        private void ProcessForm_TimingAction(Action edit_action,int index)
        {
            frm_AddEdit_TimingAction newTimingAction;
            if(edit_action == null)
            {
                newTimingAction = new frm_AddEdit_TimingAction();

            }
            else
            {
                newTimingAction = new frm_AddEdit_TimingAction(edit_action);
            }
                        
            if( newTimingAction.ShowDialog() == DialogResult.OK)
            {
                if (newTimingAction.get_action() != null)
                {
                        if (edit_action == null)
                        {
                            for (int i = 0; i < newTimingAction.get_times_to_add(); i++)
                            {
                                sequence_to_edit.Add(newTimingAction.get_action());
                            }
                        }
                        else
                        {
                            sequence_to_edit.action_sequence[index] = newTimingAction.get_action();
                        }
                }
                ActionSequenceEdited = true;
                refresh_dgActionSequence();

                // Bring Selection back to edited element
                dgActionSequence.CurrentCell = dgActionSequence.Rows[index].Cells[0];
            }
        }
        #endregion

        #region UI : Populate 
        private void populate_fields()
        {
            // Fill combo-box with possible action types.
            cbActionType.DataSource = Action_Groups; //Used to be ActionSequence.Types (now we group them into forms)

            // Null the data grid, later we will bind actions to it (as list)
            dgActionSequence.DataSource = null;

            // if we have a sequence to edit, populate the fields with existing values
            if (sequence_to_edit != null)
            {
                txtActionSequenceName.Text = sequence_to_edit.name;

                dgActionSequence.DataSource = sequence_to_edit.action_sequence.ToList();

                txtActionSequenceComment.Text = sequence_to_edit.comment;
            }
            else
            {
                this.sequence_to_edit = new VI_Action_Sequence();

                dgActionSequence.DataSource = new List<Action>();
            }
        }
        #endregion

        #region UI : Context Menu : Move Up/Down : Edit : Remove
        private void moveUpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            moveup();
        }

        private void moveDownToolStripMenuItem_Click(object sender, EventArgs e)
        {
            movedown();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            edit();
        }
        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            remove();
        }
        #endregion

        #region UI : Buttons Presses : Move Up/Down : Add : Edit : Remove
        private void btnMoveUp_Click(object sender, EventArgs e)
        {
            moveup();
        }

        private void btnMoveDown_Click(object sender, EventArgs e)
        {
            movedown();
        }
        private void btnAddAction_Click(object sender, EventArgs e)
        {
            add();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            edit();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            remove();
        }
        #endregion

        #region UI : Element Refreshing : dgActionSequence
        private void refresh_dgActionSequence()
        {
            if (sequence_to_edit != null)
            {
                dgActionSequence.DataSource = null;
                dgActionSequence.DataSource = sequence_to_edit.action_sequence.ToList();
            }
        }
        #endregion

        #region UI : Elemenet Click Events : dgActionSequence
        private void ActionSequenceList_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (dgActionSequence.RowCount > 0)
                {
                    dgActionSequence.CurrentCell = dgActionSequence.Rows[e.RowIndex].Cells[e.ColumnIndex];
                }
            }
        }
        #endregion

        #region UI : Buttons Presses : Save : Cancel
        private void btnActSeqSave_Click(object sender, EventArgs e)
        {
            string aseq_name = txtActionSequenceName.Text.Trim();
            string aseq_comment = txtActionSequenceComment.Text.Trim();

            // Validate name field
            if (String.IsNullOrEmpty(aseq_name))
            {
                MessageBox.Show("Blank value in name not allowed");

                return;
            }

            //Check if new name is taken, this requires first removing the sequence (in case no changes)
            GAVPI.vi_profile.Profile_ActionSequences.Remove(sequence_to_edit);

            if (GAVPI.vi_profile.isActionSequenceNameTaken(aseq_name))
            {
                // Reinsert without modifications.
                GAVPI.vi_profile.Profile_ActionSequences.Add(sequence_to_edit);
                MessageBox.Show("An action sequence with this name already exists.");
                return;
            }
            else
            {
                //Name is not taken, add it in.
                sequence_to_edit.name = aseq_name;
                sequence_to_edit.comment = aseq_comment;

                GAVPI.vi_profile.Profile_ActionSequences.Add(sequence_to_edit);

                ActionSequenceEdited = true;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
        private void btnActSeqCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        #endregion
    }
}
