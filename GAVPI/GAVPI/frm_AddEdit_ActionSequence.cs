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

    //cbActionSequenceData.DataSource = GAVPI.vi_profile.ProfileDB.DB.Keys.ToList();
    //cbActionSequenceData.Enabled = true;
    
    public partial class frm_AddEdit_ActionSequence : Form
    {
        public bool ActionSequenceEdited;

        private VI_Action_Sequence sequence_to_edit;

        public static List<string> Action_Groups = new List<string>(
            new string[] { 
                "Mouse/Key Press",
                "Timing (Waiting)",
                "Speak Text",
                "Data Interaction"
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
            cbActionType.DataSource = Action_Groups;

            // and refresh it immediatly
            refresh_action_value_choices();
            
            // Null the data grid, later we will bind actions to it (as list)
            dgActionSequence.DataSource = null;

            // set default # times to insert to 1
            // txtActionXTimes.Text = "1";

            

            // if we have a sequence to edit, populate the fields with existing values
            if (sequence_to_edit != null)
            {
                txtActionSequenceName.Text = sequence_to_edit.name;

                dgActionSequence.DataSource = sequence_to_edit.action_sequence.ToList();

                txtActionSequenceComment.Text = sequence_to_edit.comment;
            }
            else
            {
                /*Leave the present values blank.*/
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

        private void refresh_action_value_choices()
        {
            

            //else if (
            //    cbActionType.SelectedItem.ToString() == "MouseKeyDown" ||
            //    cbActionType.SelectedItem.ToString() == "MouseKeyUp" ||
            //    cbActionType.SelectedItem.ToString() == "MouseKeyPress")
            //{
            //    cbActSeqActionValue.DropDownStyle = ComboBoxStyle.DropDownList;
            //    cbActSeqActionValue.DataSource = Enum.GetValues(typeof(Mouse.MouseKeys)).Cast<Mouse.MouseKeys>();
            //}

            //else if (cbActionType.SelectedItem.ToString() == "Wait")
            //{
            //    cbActSeqActionValue.DropDownStyle = ComboBoxStyle.Simple;

            //}

            //else if (cbActionType.SelectedItem.ToString() == "Speak")
            //{
            //    cbActSeqActionValue.DropDownStyle = ComboBoxStyle.Simple;
            //}
            //else if (
            //    cbActionType.SelectedItem.ToString() == "Data Set" ||
            //    cbActionType.SelectedItem.ToString() == "Data Increment" ||
            //    cbActionType.SelectedItem.ToString() == "Data Decrement"
            //    )
            //{
 
            //}
            //else if (cbActionType.SelectedItem.ToString() == "Data Speak")
            //{
 
            //}
            //else
            //{
            //    MessageBox.Show("WARNING: This action type is not implemented!");
            //    //throw new NotImplementedException();
            //}
        }
        private void btnActSeqCancel_Click(object sender, EventArgs e)
        {
			this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnActSeqSave_Click(object sender, EventArgs e)
        {
            //string new_action_sequence_name = txtActionSequenceName.Text.Trim();
            //string new_action_sequence_comment = txtActionSequenceComment.Text.Trim();

            //if (String.IsNullOrEmpty(new_action_sequence_name))
            //{
            //    MessageBox.Show("Blank value in name not allowed");

            //    this.DialogResult = DialogResult.Cancel;				
				
            //    return;
            //}

            //foreach (VI_Action_Sequence existing_sequence in GAVPI.vi_profile.Profile_ActionSequences)
            //{
            //    if (new_action_sequence_name == existing_sequence.name)
            //    {
            //        GAVPI.vi_profile.Profile_ActionSequences.Remove(existing_sequence);
            //        GAVPI.vi_profile.Profile_ActionSequences.Add(action_sequence);
            //        // Replace and break
            //        break;
            //    }
            //}
            //action_sequence.name = new_action_sequence_name;
            //action_sequence.comment = new_action_sequence_comment;

            //if (edit_mode)
            //{
            //    //todo: suboptimal? But is find and replace any better?
            //    GAVPI.vi_profile.Profile_ActionSequences.Remove(action_sequence);
            //    GAVPI.vi_profile.Profile_ActionSequences.Add(action_sequence);
            //}
            //else
            //{
            //    GAVPI.vi_profile.Profile_ActionSequences.Add(action_sequence);
            //}
			

            //this.DialogResult = DialogResult.OK;
			
            //this.Close();
        }

        private void cbActSeqActionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            refresh_action_value_choices();
        }
        

        private void btnActSeqAdd_Click(object sender, EventArgs e)
        {
            // TODO
            // switch on type
            frm_AddEdit_PressAction newKeyAction = new frm_AddEdit_PressAction();
            if (newKeyAction.ShowDialog() == DialogResult.OK)
            {
                ActionSequenceEdited = true;
                refresh_dgActionSequence();
            } // if()

            //--
            //Type new_action_type = Type.GetType("GAVPI." + cbActSeqActionType.SelectedItem.ToString());

            //// Number of Times to Add particular action
            //int times_to_add = 1; //by default it is one unless the check fails
            //if (Int32.TryParse(txtActionXTimes.Text, out times_to_add))
            //{
            //    if (times_to_add <= 0)
            //    {
            //        MessageBox.Show("Times to add value cannot be less than or equal to 0.");
            //        return;
            //    }
            //}
            //else 
            //{
            //    MessageBox.Show("Times to add value must be a valid integer greater than one. (Max size 32bits)");
            //    return;
            //}


            //object action_instance;
            //if (new_action_type.ToString() == "GAVPI.Speak")
            //{
            //    action_instance = Activator.CreateInstance(new_action_type, GAVPI.vi_profile.synth ,cbActSeqActionValue.Text);
                
            //    for (uint i = 0; i < times_to_add; i++)
            //    {
            //        action_sequence.Add((Action)action_instance);
            //    }
                
            //    refresh_editactionsequence();
            //}
            //else if (new_action_type.ToString() == "GAVPI.Wait")
            //{
            //    int test;
            //    if (Int32.TryParse(cbActSeqActionValue.Text, out test) && test > 0)
            //    {
            //        action_instance = Activator.CreateInstance(new_action_type, cbActSeqActionValue.Text);
            //        for (uint i = 0; i < times_to_add; i++)
            //        {
            //            action_sequence.Add((Action)action_instance);
            //        }
            //        refresh_editactionsequence();
            //    }
            //    else
            //    {
            //        MessageBox.Show("Wait must be positive integer in milliseconds.");
            //        return;
            //    }
            //}
            //else
            //{
            //    action_instance = Activator.CreateInstance(new_action_type, cbActSeqActionValue.SelectedItem.ToString());
            //    for (uint i = 0; i < times_to_add; i++)
            //    {
            //        action_sequence.Add((Action)action_instance);
            //    }
            //    refresh_editactionsequence();
            //}
			
			//  We've updated the Action Sequence, so allow the user to save their changes...
			
            // TODO
			//this.btnActSeqSave.Enabled = true;
            
        }


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

        }

        private void moveDownToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ////foreach (DataGridViewRow row in dgActionSequence.SelectedRows)
            ////{
            ////    Action singleaction = row.DataBoundItem as Action;
            ////    action_sequence.Remove(singleaction);
            ////}
            ////refresh_editactionsequence();
        }
        #endregion
        #region Action Add : Edit : Remove
        private void btnAddAction_Click(object sender, EventArgs e)
        {
            //TODO on type
            //switch
            frm_AddEdit_PressAction newPressAction = new frm_AddEdit_PressAction();
            if (newPressAction.ShowDialog() == DialogResult.OK)
            {
                ActionSequenceEdited = true;
                refresh_dgActionSequence();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {

        }

        private void btnRemove_Click(object sender, EventArgs e)
        {

        }
        #endregion
        #region Action Ordering
        private void btnMoveUp_Click(object sender, EventArgs e)
        {

        }

        private void btnMoveDown_Click(object sender, EventArgs e)
        {

        }
        #endregion

        private void dgActionSequence_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
