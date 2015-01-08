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
        public bool ActionSequenceEdited;

        private VI_Action_Sequence sequence_to_edit;

        public static List<string> Action_Groups = new List<string>(
            new string[] { 
                "Key/Mouse Press",
                "Wait",
                "Speak Text",
                "Data Action"
            });

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
        private void refresh_dgActionSequence()
        {
            if (sequence_to_edit != null)
            {
                dgActionSequence.DataSource = null;
                dgActionSequence.DataSource = sequence_to_edit.action_sequence.ToList();
            }
        }

        private void btnActSeqCancel_Click(object sender, EventArgs e)
        {
			this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        // This is the most important function, handles validation and commiting.
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

        #region Action Add : Edit : Remove
        // These methods are so because we have right click and button UI event methods
        // no point in copy/pasting the same thing twice.
        // UI events simply call these functions.
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
        private void add()
        {
            switch (cbActionType.SelectedItem.ToString())
            {
                case "Key/Mouse Press":
                    {
                        frm_AddEdit_PressAction newPressAction = new frm_AddEdit_PressAction();
                        if (newPressAction.ShowDialog() == DialogResult.OK)
                        {
                            // if OK pull out edited or new action
                            if (newPressAction.get_action() != null)
                            {
                                // Add Multiple Times (default : 1)
                                for (int i = 0; i < newPressAction.get_times_to_add(); i++)
                                {
                                    sequence_to_edit.Add(newPressAction.get_action());
                                }
                            }
                            else
                            {
                                MessageBox.Show("WARNING: Press form returned an invalid action.");
                            }
                            ActionSequenceEdited = true;
                            refresh_dgActionSequence();
                        }

                        break;
                    }
                case "Wait":
                    {
                        break;
                    }
                case "Speak Text":
                    {
                        break;
                    }
                case "Data Action":
                    {
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
            // and show warning.

            //TODO, for multi select just remove the items on OK
            //and insert # removed copies of edited

            foreach (DataGridViewRow row in dgActionSequence.SelectedRows)
            {
                Action action_to_edit = row.DataBoundItem as Action;

                int index = sequence_to_edit.action_sequence.IndexOf(action_to_edit);

                frm_AddEdit_PressAction newPressAction = new frm_AddEdit_PressAction(action_to_edit);
                if (newPressAction.ShowDialog() == DialogResult.OK)
                {
                    // if OK pull out edited action
                    if (newPressAction.get_action() != null)
                    {
                        sequence_to_edit.action_sequence[index] = newPressAction.get_action();
                    }
                    else
                    {
                        MessageBox.Show("WARNING: Press form returned an invalid action.");
                    }

                    ActionSequenceEdited = true;
                    refresh_dgActionSequence();
                    break; //first item only
                }
            }
        }
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

        #region Action Selection
        private void ActionSequenceList_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                dgActionSequence.CurrentCell = dgActionSequence.Rows[e.RowIndex].Cells[e.ColumnIndex];
            }
        }
        #endregion
        #region Action (RightClick) Context Menu
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
        #region UI Action Add : Edit : Remove
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
        #region Action Ordering
        private void btnMoveUp_Click(object sender, EventArgs e)
        {
            moveup();

        }

        private void btnMoveDown_Click(object sender, EventArgs e)
        {
            movedown();

        }
        #endregion
    }
}
