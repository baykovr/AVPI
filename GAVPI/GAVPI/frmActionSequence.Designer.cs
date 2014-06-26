namespace GAVPI
{
    partial class frmActionSequence
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lstActSeqActions = new System.Windows.Forms.ListView();
            this.txtActionSequenceName = new System.Windows.Forms.TextBox();
            this.lblActionSequenceName = new System.Windows.Forms.Label();
            this.cboxActSeqAction = new System.Windows.Forms.ComboBox();
            this.lblAddAction = new System.Windows.Forms.Label();
            this.btnActSeqSave = new System.Windows.Forms.Button();
            this.btnActSeqCancel = new System.Windows.Forms.Button();
            this.btnActSeqAdd = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lstActSeqActions
            // 
            this.lstActSeqActions.GridLines = true;
            this.lstActSeqActions.Location = new System.Drawing.Point(12, 98);
            this.lstActSeqActions.Name = "lstActSeqActions";
            this.lstActSeqActions.Size = new System.Drawing.Size(434, 365);
            this.lstActSeqActions.TabIndex = 0;
            this.lstActSeqActions.UseCompatibleStateImageBehavior = false;
            // 
            // txtActionSequenceName
            // 
            this.txtActionSequenceName.Location = new System.Drawing.Point(138, 9);
            this.txtActionSequenceName.Name = "txtActionSequenceName";
            this.txtActionSequenceName.Size = new System.Drawing.Size(265, 20);
            this.txtActionSequenceName.TabIndex = 1;
            // 
            // lblActionSequenceName
            // 
            this.lblActionSequenceName.AutoSize = true;
            this.lblActionSequenceName.Location = new System.Drawing.Point(12, 9);
            this.lblActionSequenceName.Name = "lblActionSequenceName";
            this.lblActionSequenceName.Size = new System.Drawing.Size(120, 13);
            this.lblActionSequenceName.TabIndex = 2;
            this.lblActionSequenceName.Text = "Action Sequence Name";
            // 
            // cboxActSeqAction
            // 
            this.cboxActSeqAction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboxActSeqAction.FormattingEnabled = true;
            this.cboxActSeqAction.Location = new System.Drawing.Point(55, 62);
            this.cboxActSeqAction.Name = "cboxActSeqAction";
            this.cboxActSeqAction.Size = new System.Drawing.Size(227, 21);
            this.cboxActSeqAction.TabIndex = 3;
            // 
            // lblAddAction
            // 
            this.lblAddAction.AutoSize = true;
            this.lblAddAction.Location = new System.Drawing.Point(12, 65);
            this.lblAddAction.Name = "lblAddAction";
            this.lblAddAction.Size = new System.Drawing.Size(37, 13);
            this.lblAddAction.TabIndex = 5;
            this.lblAddAction.Text = "Action";
            // 
            // btnActSeqSave
            // 
            this.btnActSeqSave.Location = new System.Drawing.Point(290, 469);
            this.btnActSeqSave.Name = "btnActSeqSave";
            this.btnActSeqSave.Size = new System.Drawing.Size(75, 23);
            this.btnActSeqSave.TabIndex = 6;
            this.btnActSeqSave.Text = "Save";
            this.btnActSeqSave.UseVisualStyleBackColor = true;
            // 
            // btnActSeqCancel
            // 
            this.btnActSeqCancel.Location = new System.Drawing.Point(371, 469);
            this.btnActSeqCancel.Name = "btnActSeqCancel";
            this.btnActSeqCancel.Size = new System.Drawing.Size(75, 23);
            this.btnActSeqCancel.TabIndex = 7;
            this.btnActSeqCancel.Text = "Cancel";
            this.btnActSeqCancel.UseVisualStyleBackColor = true;
            // 
            // btnActSeqAdd
            // 
            this.btnActSeqAdd.Location = new System.Drawing.Point(290, 62);
            this.btnActSeqAdd.Name = "btnActSeqAdd";
            this.btnActSeqAdd.Size = new System.Drawing.Size(75, 23);
            this.btnActSeqAdd.TabIndex = 8;
            this.btnActSeqAdd.Text = "Add";
            this.btnActSeqAdd.UseVisualStyleBackColor = true;
            // 
            // frmActionSequence
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(463, 504);
            this.Controls.Add(this.btnActSeqAdd);
            this.Controls.Add(this.btnActSeqCancel);
            this.Controls.Add(this.btnActSeqSave);
            this.Controls.Add(this.lblAddAction);
            this.Controls.Add(this.cboxActSeqAction);
            this.Controls.Add(this.lblActionSequenceName);
            this.Controls.Add(this.txtActionSequenceName);
            this.Controls.Add(this.lstActSeqActions);
            this.Name = "frmActionSequence";
            this.Text = "Action Sequence";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lstActSeqActions;
        private System.Windows.Forms.TextBox txtActionSequenceName;
        private System.Windows.Forms.Label lblActionSequenceName;
        private System.Windows.Forms.ComboBox cboxActSeqAction;
        private System.Windows.Forms.Label lblAddAction;
        private System.Windows.Forms.Button btnActSeqSave;
        private System.Windows.Forms.Button btnActSeqCancel;
        private System.Windows.Forms.Button btnActSeqAdd;
    }
}