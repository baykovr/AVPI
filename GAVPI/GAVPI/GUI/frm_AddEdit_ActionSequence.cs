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

    public partial class frm_AddEdit_ActionSequence : Form
    {
        #region Form Globals

        public bool ActionSequenceEdited;

        private Action_Sequence sequence_to_edit;

        public static Dictionary<string, List<string>> ActionGroups
            = new Dictionary<string, List<string>>() 
            {
                { "Key/Mouse Press",  new List<string> { "KeyDown", "KeyUp", "KeyPress","MouseKeyDown","MouseKeyUp","MouseKeyPress"} },
                { "Timing",           new List<string> { "Wait" } },
                { "Speak Action",     new List<string> { "Speak","Data_Speak" } },
                { "PlaySound Action", new List<string> { "Play_Sound" } },
                { "StopSound Action", new List<string> { "Stop_Sound" } },
                { "Paste Action",     new List<string> { "ClipboardPaste" } },
                { "Data Action",      new List<string> { "Data_Paste" } }
            };

        // These type lists are used to populate ui elements,
        // their (array string) value must match the class name specified bellow, ex : Action : Data_Set
        // this is particularly important since the string will be cast to a class instance later.

        //public static List<string> PressAction_Types = new List<string>(
        //    new string[] { 
        //        "KeyDown", "KeyUp", "KeyPress",
        //        "MouseKeyDown","MouseKeyUp","MouseKeyPress"
        //    });
        //public static List<string> SpeechAction_Types = new List<string>(
        //    new string[] { 
        //       "Speak",
        //       "Data_Speak"
        //    });
        //public static List<string> PlaySoundAction_Types = new List<string>(
        //    new string[] {
        //        "Play_Sound"
        //    });
        //public static List<string> TimingAction_Types = new List<string>(
        //    new string[] { 
        //       "Wait"
        //    });
        //public static List<string> DataAction_Types = new List<string>(
        //    new string[] { 
        //       "Data_Paste"
        //       //"Data_Set","Data_Decrement","Data_Increment"
        //    });

        #endregion

        #region Constructors
        public frm_AddEdit_ActionSequence()
        {
            // if no sequence is passed, a new sequence is created.
            InitializeComponent();

            populate_fields();
        }
        public frm_AddEdit_ActionSequence( Action_Sequence action_sequence )
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
        private void ui_moveup()
        {
            int index;

            foreach (DataGridViewRow row in dgActionSequence.SelectedRows)
            {
                Action action_to_moveup = row.DataBoundItem as Action;
                
                index = sequence_to_edit.action_sequence.IndexOf(action_to_moveup);
                
                // Check if the current location is at the front
                if (index == 0)
                    { break;}

                sequence_to_edit.action_sequence.RemoveAt(index);
                
                // Check if the new location is at the front
                if (index - 1 <= 0)
                {
                    // insert it at the front of the list
                    
                    sequence_to_edit.action_sequence.Insert(0, action_to_moveup);

                    refresh_dgActionSequence();
                }
                else
                {
                    sequence_to_edit.action_sequence.Insert(index - 1, action_to_moveup);
                    
                    refresh_dgActionSequence();
                    
                    // Select the item just moved.
                    dgActionSequence.CurrentCell = dgActionSequence.Rows[index - 1].Cells[0];
                }
            }
            ActionSequenceEdited = true;
        }
        private void ui_movedown()
        {
            int index;
            foreach (DataGridViewRow row in dgActionSequence.SelectedRows)
            {
                Action action_to_movedown = row.DataBoundItem as Action;

                index = sequence_to_edit.action_sequence.IndexOf(action_to_movedown);
                
                // Check if the current location is at the end
                if (index == sequence_to_edit.action_sequence.Count)
                    { break; }

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
        private void ui_add()
        {
            CreateNewAction( cbActionType.SelectedItem.ToString() );
        }

        private void ui_edit()
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

                string group = "";

                // Choose Form Method to Invoke based on type
                foreach (KeyValuePair<string, List<string>> action_group in ActionGroups)
                {
                    // which group does this action type come from.
                    if(action_group.Value.Contains(action_type)){
                        group = action_group.Key;
                    }
                }

                switch (group)
                {
                    case "Key/Mouse Press":
                    {
                        ProcessForm_AddEditPressAction(action_to_edit, index);
                        break;
                    }
                    case "Timing":
                    {
                        ProcessForm_AddEditTimingAction(action_to_edit, index);
                        break;
                    }
                    case "Speak Action":
                    {
                        ProcessForm_AddEditSpeechAction(action_to_edit, index);
                        break;
                    }
                    case "PlaySound Action":
                    {
                        ProcessForm_AddEditPlaySoundAction(action_to_edit, index);
                        break;
                    }
                    case "StopSound Action":
                    {
                        // StopSound has no form, albiet it does have a placeholder value.
                        break;
                    }
                    case "Paste Action":
                    {
                        ProcessForm_AddEditPasteAction(action_to_edit, index);
                        break;
                    }
                    case "Data Action":
                    {
                        // not implemented.
                        break;
                    }
                    default:
                    GAVPI.Log.Entry("[ ! ] ActionSequence editor error bad group hit default case: "+group);
                        break;
                }
            }
        }
        #endregion
        
        #region Remove
        private void ui_remove()
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

        private void CreateNewAction(string action_group)
        {
            switch (cbActionType.SelectedItem.ToString())
            {
                case "Key/Mouse Press":
                    {
                        ProcessForm_AddEditPressAction(null, 0);
                        break;
                    }
                case "Timing":
                    {
                        ProcessForm_AddEditTimingAction(null, 0);
                        break;
                    }
                case "Speak Action":
                    {
                        ProcessForm_AddEditSpeechAction(null, 0);
                        break;
                    }
                case "PlaySound Action":
                    {
                        ProcessForm_AddEditPlaySoundAction(null, 0);
                        break;
                    }
                case "Paste Action":
                    {
                        ProcessForm_AddEditPasteAction(null, 0);
                        break;
                    }
                case "StopSound Action":
                    {

                        ProcessForm_AddEditStopSoundAction(null, 0);
                        break;
                    }
                case "Data Action":
                    {
                        // TODO
                        MessageBox.Show("This feature is not ready yet.");
                        //frm_AddEdit_DataAction newDataAction = new frm_AddEdit_DataAction();
                        //if (newDataAction.ShowDialog() == DialogResult.OK)
                        //{

                        //}
                        break;
                    }
                default:
                    {
                        MessageBox.Show("WARNING: This action type is not implemented!");
                        break;
                    }
            }
        }

        // Form processing invokes handles form creation
        // Once the form exits these functions also handle refreshing/updating UI elements
        // and setting form global variables (such as if we need to save)
        // @edit_action : either be an existing action (for editing) or null (create new)
        // @index       : index of existing action in data grid view, unused if new action.
        private void EditAction(Action action_to_edit, int index)
        {
            // HERE
            // We need to infer the form type automatically based on type of action_to_edit
            // or have a big switch...
        }

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
                    return;
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
                    return;
                }
                ActionSequenceEdited = true;
                refresh_dgActionSequence();
                // Bring Selection back to edited element
                dgActionSequence.CurrentCell = dgActionSequence.Rows[index].Cells[0];
            }
        }

        private void ProcessForm_AddEditPlaySoundAction(Action edit_action, int index)
        {
            frm_AddEdit_PlaySoundAction newPlaySoundAction;
            if (edit_action == null)
            {
                newPlaySoundAction = new frm_AddEdit_PlaySoundAction();
            }
            else
            {
                newPlaySoundAction = new frm_AddEdit_PlaySoundAction(edit_action);
            }

            // On form OK we have changes (either new or edited action)
            if (newPlaySoundAction.ShowDialog() == DialogResult.OK)
            {
                // Make sure the returned action is sane
                if (newPlaySoundAction.get_action() != null)
                {
                    // Called by Add
                    if (edit_action == null)
                    {
                        // Insert number of times specified by the form
                        for (int i = 0; i < newPlaySoundAction.get_times_to_add(); i++)
                        {
                            sequence_to_edit.Add(newPlaySoundAction.get_action());
                        }
                    }
                    // Called by Edit
                    else
                    {
                        // Replace the current action with the new from the form
                        sequence_to_edit.action_sequence[index] = newPlaySoundAction.get_action();
                    }
                }
                // form returned null action
                else
                {
                    MessageBox.Show("WARNING: Press form returned an invalid action.");
                    return;
                }
                ActionSequenceEdited = true;
                refresh_dgActionSequence();
                // Bring Selection back to edited element
                dgActionSequence.CurrentCell = dgActionSequence.Rows[index].Cells[0];
            }
        }

        private void ProcessForm_AddEditPasteAction(Action edit_action, int index)
        {
            frm_AddEdit_PasteAction newPasteAction;
            if (edit_action == null)
            {
                newPasteAction = new frm_AddEdit_PasteAction();
            }
            else
            {
                newPasteAction = new frm_AddEdit_PasteAction(edit_action);
            }

            // On form OK we have changes (either new or edited action)
            if (newPasteAction.ShowDialog() == DialogResult.OK)
            {
                // Make sure the returned action is sane
                if (newPasteAction.get_action() != null)
                {
                    // Called by Add
                    if (edit_action == null)
                    {
                        // Insert number of times specified by the form
                        for (int i = 0; i < newPasteAction.get_times_to_add(); i++)
                        {
                            sequence_to_edit.Add(newPasteAction.get_action());
                        }
                    }
                    // Called by Edit
                    else
                    {
                        // Replace the current action with the new from the form
                        sequence_to_edit.action_sequence[index] = newPasteAction.get_action();
                    }
                }
                else
                {
                    MessageBox.Show("WARNING: Press form returned an invalid action.");
                    return;
                }
                ActionSequenceEdited = true;
                refresh_dgActionSequence();
                // Bring Selection back to edited element
                dgActionSequence.CurrentCell = dgActionSequence.Rows[index].Cells[0];
            }
        }

        private void ProcessForm_AddEditStopSoundAction(Action edit_action, int index)
        {
            Action stopAction = new Stop_Sound();
            // Add new action.
            if (edit_action == null)
            {
                sequence_to_edit.Add( stopAction );
            }
            // Edit an existing action, in this case nothing since stop_sound has no arguments or values to edit.
            else
            { }

            ActionSequenceEdited = true;
            refresh_dgActionSequence();
            // Bring Selection back to edited element
            dgActionSequence.CurrentCell = dgActionSequence.Rows[index].Cells[0];
        }

        private void ProcessForm_AddEditTimingAction(Action edit_action,int index)
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
                else
                {
                    MessageBox.Show("WARNING: Press form returned an invalid action.");
                    return;
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
            cbActionType.DataSource = ActionGroups.Keys.ToList();

            // Null the data grid, later we will bind actions to it (as list)
            dgActionSequence.DataSource = null;

            // if we have a sequence to edit, populate the fields with existing values
            if (sequence_to_edit != null)
            {
                txtActionSequenceName.Text = sequence_to_edit.name;

                // Bind the actions of this sequence as the datasource of the data grid.
                dgActionSequence.DataSource = sequence_to_edit.action_sequence;

                txtActionSequenceComment.Text = sequence_to_edit.comment;

                chkRandomExecution.Checked = sequence_to_edit.random_exec;
            }
            // Otherwise just init new attributes.
            else
            {
                this.sequence_to_edit = new Action_Sequence();
                dgActionSequence.DataSource = new List<Action>();
            }
        }
        #endregion

        #region UI : Context Menu : Move Up/Down : Edit : Remove
        private void moveUpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ui_moveup();
        }

        private void moveDownToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ui_movedown();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ui_edit();
        }
        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ui_remove();
        }
        #endregion

        #region UI : Buttons Presses : Move Up/Down : Add : Edit : Remove
        private void btnMoveUp_Click(object sender, EventArgs e)
        {
            ui_moveup();
        }

        private void btnMoveDown_Click(object sender, EventArgs e)
        {
            ui_movedown();
        }
        private void btnAddAction_Click(object sender, EventArgs e)
        {
            ui_add();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            ui_edit();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            ui_remove();
        }
        private void chkRandomExecution_CheckedChanged(object sender, EventArgs e)
        {
            sequence_to_edit.random_exec = chkRandomExecution.Checked;
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
                if (dgActionSequence.RowCount > 0 && e.RowIndex > 0)
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
            GAVPI.Profile.Profile_ActionSequences.Remove(sequence_to_edit);

            if (GAVPI.Profile.isActionSequenceNameTaken(aseq_name))
            {
                // Reinsert without modifications.
                GAVPI.Profile.Profile_ActionSequences.Add(sequence_to_edit);
                MessageBox.Show("An action sequence with this name already exists.");
                return;
            }
            else
            {
                //Name is not taken, add it in.
                sequence_to_edit.name = aseq_name;
                sequence_to_edit.comment = aseq_comment;

                GAVPI.Profile.Profile_ActionSequences.Add(sequence_to_edit);

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
