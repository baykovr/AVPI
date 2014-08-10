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
    public partial class frmActionSequence : Form
    {
        VI_Profile profile;
        VI_Action_Sequence action_sequence;
        bool edit_mode;

        public frmActionSequence(VI_Profile profile)
        {
            InitializeComponent();
            this.profile = profile;
            this.action_sequence = new VI_Action_Sequence("new sequence");
            populate_sequence();
            edit_mode = false;
        }
        public frmActionSequence(VI_Profile profile, VI_Action_Sequence action_sequence)
        {
            InitializeComponent();
            this.profile = profile;
            this.action_sequence = action_sequence;
            populate_sequence();
            edit_mode = true;
        }
        private void populate_sequence()
        {
            cbActSeqActionType.DataSource = profile.Action_Types;
            refresh_action_value_choices();
            txtActionSequenceName.Text = action_sequence.name;
            dgEditActionSequence.DataSource = null;
            dgEditActionSequence.DataSource = action_sequence.action_sequence.ToArray();
            txtActionXTimes.Text = "1";
        }
        private void refresh_editactionsequence()
        {
            
            dgEditActionSequence.DataSource = null;
            dgEditActionSequence.DataSource = action_sequence.action_sequence.ToArray();
        }
        private void refresh_action_value_choices()
        {
            if (cbActSeqActionType.SelectedItem.ToString() == "KeyDown" ||
                cbActSeqActionType.SelectedItem.ToString() == "KeyUp" ||
                cbActSeqActionType.SelectedItem.ToString() == "KeyPress")
            {
                cbActSeqActionValue.DropDownStyle = ComboBoxStyle.DropDownList;
                cbActSeqActionValue.DataSource = Enum.GetValues(typeof(Keys)).Cast<Keys>();
            }
            else if (cbActSeqActionType.SelectedItem.ToString() == "MouseKeyDown" ||
                cbActSeqActionType.SelectedItem.ToString() == "MouseKeyUp" ||
                cbActSeqActionType.SelectedItem.ToString() == "MouseKeyPress")
            {
                cbActSeqActionValue.DropDownStyle = ComboBoxStyle.DropDownList;
                cbActSeqActionValue.DataSource = Enum.GetValues(typeof(Mouse.MouseKeys)).Cast<Mouse.MouseKeys>();
            }
            else if (cbActSeqActionType.SelectedItem.ToString() == "Wait")
            {
                cbActSeqActionValue.DropDownStyle = ComboBoxStyle.Simple;

            }
            else if (cbActSeqActionType.SelectedItem.ToString() == "Speak")
            {
                cbActSeqActionValue.DropDownStyle = ComboBoxStyle.Simple;
            }
            else
            {
                throw new NotImplementedException();
            }
        }
        private void btnActSeqCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnActSeqSave_Click(object sender, EventArgs e)
        {
            string new_action_sequence_name = txtActionSequenceName.Text.Trim();
            if (new_action_sequence_name == "")
            {
                MessageBox.Show("Blank value in name not allowed");
                return;
            }
            foreach (VI_Action_Sequence existing_sequence in profile.Profile_ActionSequences)
            {
                if (new_action_sequence_name == existing_sequence.name)
                {
                    profile.Profile_ActionSequences.Remove(existing_sequence);
                    profile.Profile_ActionSequences.Add(action_sequence);
                    // Replace and break
                    break;
                }
            }
            action_sequence.name = new_action_sequence_name;

            if (edit_mode)
            {
                //todo: suboptimal? But is find and replace any better?
                profile.Profile_ActionSequences.Remove(action_sequence);
                profile.Profile_ActionSequences.Add(action_sequence);
            }
            else
            {
                profile.Profile_ActionSequences.Add(action_sequence);
            }
            this.Close();
        }

        private void cbActSeqActionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            refresh_action_value_choices();
        }
        

        private void btnActSeqAdd_Click(object sender, EventArgs e)
        {
            Type new_action_type = Type.GetType("GAVPI." + cbActSeqActionType.SelectedItem.ToString());

            // Number of Times to Add particular action
            int times_to_add = 1; //by default it is one unless the check fails
            if (Int32.TryParse(txtActionXTimes.Text, out times_to_add))
            {
                if (times_to_add <= 0)
                {
                    MessageBox.Show("Times to add value cannot be less than or equal to 0.");
                    return;
                }
            }
            else 
            {
                MessageBox.Show("Times to add value must be a valid integer greater than one. (Max size 32bits)");
                return;
            }


            object action_instance;
            if (new_action_type.ToString() == "GAVPI.Speak")
            {
                action_instance = Activator.CreateInstance(new_action_type, profile.synth ,cbActSeqActionValue.Text);
                
                for (uint i = 0; i < times_to_add; i++)
                {
                    action_sequence.Add((Action)action_instance);
                }
                
                refresh_editactionsequence();
            }
            else if (new_action_type.ToString() == "GAVPI.Wait")
            {
                int test;
                if (Int32.TryParse(cbActSeqActionValue.Text, out test) && test > 0)
                {
                    action_instance = Activator.CreateInstance(new_action_type, cbActSeqActionValue.Text);
                    for (uint i = 0; i < times_to_add; i++)
                    {
                        action_sequence.Add((Action)action_instance);
                    }
                    refresh_editactionsequence();
                }
                else
                {
                    MessageBox.Show("Wait must be positive integer in milliseconds.");
                    return;
                }
            }
            else
            {
                action_instance = Activator.CreateInstance(new_action_type, cbActSeqActionValue.SelectedItem.ToString());
                for (uint i = 0; i < times_to_add; i++)
                {
                    action_sequence.Add((Action)action_instance);
                }
                refresh_editactionsequence();
            }
            
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgEditActionSequence.SelectedRows)
            {
                Action singleaction = row.DataBoundItem as Action;
                action_sequence.Remove(singleaction);
            }
            refresh_editactionsequence();
        }
    }
}
