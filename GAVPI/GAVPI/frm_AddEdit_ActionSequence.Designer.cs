namespace GAVPI
{
    partial class frm_AddEdit_ActionSequence
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_AddEdit_ActionSequence));
            this.txtActionSequenceName = new System.Windows.Forms.TextBox();
            this.lblActionSequenceName = new System.Windows.Forms.Label();
            this.btnActSeqSave = new System.Windows.Forms.Button();
            this.btnActSeqCancel = new System.Windows.Forms.Button();
            this.stripActionSequence = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.moveUpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.moveDownToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dgActionSequence = new System.Windows.Forms.DataGridView();
            this.lblActionSequenceComment = new System.Windows.Forms.Label();
            this.txtActionSequenceComment = new System.Windows.Forms.TextBox();
            this.btnMoveUp = new System.Windows.Forms.Button();
            this.btnMoveDown = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnAddAction = new System.Windows.Forms.Button();
            this.lblActionType = new System.Windows.Forms.Label();
            this.cbActionType = new System.Windows.Forms.ComboBox();
            this.stripActionSequence.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgActionSequence)).BeginInit();
            this.SuspendLayout();
            // 
            // txtActionSequenceName
            // 
            this.txtActionSequenceName.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtActionSequenceName.Location = new System.Drawing.Point(111, 15);
            this.txtActionSequenceName.Name = "txtActionSequenceName";
            this.txtActionSequenceName.Size = new System.Drawing.Size(329, 20);
            this.txtActionSequenceName.TabIndex = 1;
            // 
            // lblActionSequenceName
            // 
            this.lblActionSequenceName.AutoSize = true;
            this.lblActionSequenceName.Location = new System.Drawing.Point(18, 18);
            this.lblActionSequenceName.Name = "lblActionSequenceName";
            this.lblActionSequenceName.Size = new System.Drawing.Size(87, 13);
            this.lblActionSequenceName.TabIndex = 2;
            this.lblActionSequenceName.Text = "Sequence Name";
            // 
            // btnActSeqSave
            // 
            this.btnActSeqSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnActSeqSave.Location = new System.Drawing.Point(365, 551);
            this.btnActSeqSave.Name = "btnActSeqSave";
            this.btnActSeqSave.Size = new System.Drawing.Size(75, 23);
            this.btnActSeqSave.TabIndex = 6;
            this.btnActSeqSave.Text = "Save";
            this.btnActSeqSave.UseVisualStyleBackColor = true;
            this.btnActSeqSave.Click += new System.EventHandler(this.btnActSeqSave_Click);
            // 
            // btnActSeqCancel
            // 
            this.btnActSeqCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnActSeqCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnActSeqCancel.Location = new System.Drawing.Point(446, 551);
            this.btnActSeqCancel.Name = "btnActSeqCancel";
            this.btnActSeqCancel.Size = new System.Drawing.Size(83, 23);
            this.btnActSeqCancel.TabIndex = 7;
            this.btnActSeqCancel.Text = "Cancel";
            this.btnActSeqCancel.UseVisualStyleBackColor = true;
            this.btnActSeqCancel.Click += new System.EventHandler(this.btnActSeqCancel_Click);
            // 
            // stripActionSequence
            // 
            this.stripActionSequence.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.moveUpToolStripMenuItem,
            this.moveDownToolStripMenuItem,
            this.editToolStripMenuItem,
            this.removeToolStripMenuItem});
            this.stripActionSequence.Name = "contextMenuStrip1";
            this.stripActionSequence.Size = new System.Drawing.Size(130, 92);
            // 
            // moveUpToolStripMenuItem
            // 
            this.moveUpToolStripMenuItem.Name = "moveUpToolStripMenuItem";
            this.moveUpToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.moveUpToolStripMenuItem.Text = "Move Up";
            this.moveUpToolStripMenuItem.Click += new System.EventHandler(this.moveUpToolStripMenuItem_Click);
            // 
            // moveDownToolStripMenuItem
            // 
            this.moveDownToolStripMenuItem.Name = "moveDownToolStripMenuItem";
            this.moveDownToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.moveDownToolStripMenuItem.Text = "Move Down";
            this.moveDownToolStripMenuItem.Click += new System.EventHandler(this.moveDownToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.editToolStripMenuItem.Text = "Edit";
            this.editToolStripMenuItem.Click += new System.EventHandler(this.editToolStripMenuItem_Click);
            // 
            // removeToolStripMenuItem
            // 
            this.removeToolStripMenuItem.Name = "removeToolStripMenuItem";
            this.removeToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.removeToolStripMenuItem.Text = "Remove";
            this.removeToolStripMenuItem.Click += new System.EventHandler(this.removeToolStripMenuItem_Click);
            // 
            // dgActionSequence
            // 
            this.dgActionSequence.AllowUserToAddRows = false;
            this.dgActionSequence.AllowUserToDeleteRows = false;
            this.dgActionSequence.AllowUserToResizeColumns = false;
            this.dgActionSequence.AllowUserToResizeRows = false;
            this.dgActionSequence.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgActionSequence.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgActionSequence.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgActionSequence.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgActionSequence.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgActionSequence.ContextMenuStrip = this.stripActionSequence;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgActionSequence.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgActionSequence.GridColor = System.Drawing.Color.White;
            this.dgActionSequence.Location = new System.Drawing.Point(12, 114);
            this.dgActionSequence.Name = "dgActionSequence";
            this.dgActionSequence.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgActionSequence.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgActionSequence.RowHeadersVisible = false;
            this.dgActionSequence.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgActionSequence.Size = new System.Drawing.Size(428, 430);
            this.dgActionSequence.TabIndex = 11;
            this.dgActionSequence.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgActionSequence_CellContentClick);
            this.dgActionSequence.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.ActionSequenceList_CellMouseDown);
            // 
            // lblActionSequenceComment
            // 
            this.lblActionSequenceComment.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblActionSequenceComment.AutoSize = true;
            this.lblActionSequenceComment.Location = new System.Drawing.Point(54, 44);
            this.lblActionSequenceComment.Name = "lblActionSequenceComment";
            this.lblActionSequenceComment.Size = new System.Drawing.Size(51, 13);
            this.lblActionSequenceComment.TabIndex = 14;
            this.lblActionSequenceComment.Text = "Comment";
            // 
            // txtActionSequenceComment
            // 
            this.txtActionSequenceComment.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtActionSequenceComment.Location = new System.Drawing.Point(111, 41);
            this.txtActionSequenceComment.Name = "txtActionSequenceComment";
            this.txtActionSequenceComment.Size = new System.Drawing.Size(329, 20);
            this.txtActionSequenceComment.TabIndex = 15;
            // 
            // btnMoveUp
            // 
            this.btnMoveUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMoveUp.Location = new System.Drawing.Point(446, 114);
            this.btnMoveUp.Name = "btnMoveUp";
            this.btnMoveUp.Size = new System.Drawing.Size(83, 22);
            this.btnMoveUp.TabIndex = 17;
            this.btnMoveUp.Text = "Move Up";
            this.btnMoveUp.UseVisualStyleBackColor = true;
            this.btnMoveUp.Click += new System.EventHandler(this.btnMoveUp_Click);
            // 
            // btnMoveDown
            // 
            this.btnMoveDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMoveDown.Location = new System.Drawing.Point(446, 142);
            this.btnMoveDown.Name = "btnMoveDown";
            this.btnMoveDown.Size = new System.Drawing.Size(83, 22);
            this.btnMoveDown.TabIndex = 18;
            this.btnMoveDown.Text = "Move Down";
            this.btnMoveDown.UseVisualStyleBackColor = true;
            this.btnMoveDown.Click += new System.EventHandler(this.btnMoveDown_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEdit.Location = new System.Drawing.Point(446, 170);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(83, 22);
            this.btnEdit.TabIndex = 19;
            this.btnEdit.Text = "Edit";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemove.Location = new System.Drawing.Point(446, 198);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(83, 22);
            this.btnRemove.TabIndex = 20;
            this.btnRemove.Text = "Remove";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnAddAction
            // 
            this.btnAddAction.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddAction.Location = new System.Drawing.Point(306, 86);
            this.btnAddAction.Margin = new System.Windows.Forms.Padding(5, 5, 5, 25);
            this.btnAddAction.Name = "btnAddAction";
            this.btnAddAction.Size = new System.Drawing.Size(134, 22);
            this.btnAddAction.TabIndex = 24;
            this.btnAddAction.Text = "Add New Action";
            this.btnAddAction.UseVisualStyleBackColor = true;
            this.btnAddAction.Click += new System.EventHandler(this.btnAddAction_Click);
            // 
            // lblActionType
            // 
            this.lblActionType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblActionType.AutoSize = true;
            this.lblActionType.Location = new System.Drawing.Point(16, 91);
            this.lblActionType.Name = "lblActionType";
            this.lblActionType.Size = new System.Drawing.Size(89, 13);
            this.lblActionType.TabIndex = 23;
            this.lblActionType.Text = "New Action Type";
            // 
            // cbActionType
            // 
            this.cbActionType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbActionType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbActionType.FormattingEnabled = true;
            this.cbActionType.Location = new System.Drawing.Point(111, 86);
            this.cbActionType.Name = "cbActionType";
            this.cbActionType.Size = new System.Drawing.Size(187, 21);
            this.cbActionType.TabIndex = 22;
            // 
            // frm_AddEdit_ActionSequence
            // 
            this.AcceptButton = this.btnActSeqSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnActSeqCancel;
            this.ClientSize = new System.Drawing.Size(541, 586);
            this.Controls.Add(this.btnAddAction);
            this.Controls.Add(this.lblActionType);
            this.Controls.Add(this.cbActionType);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnMoveDown);
            this.Controls.Add(this.btnMoveUp);
            this.Controls.Add(this.txtActionSequenceComment);
            this.Controls.Add(this.lblActionSequenceComment);
            this.Controls.Add(this.dgActionSequence);
            this.Controls.Add(this.btnActSeqCancel);
            this.Controls.Add(this.btnActSeqSave);
            this.Controls.Add(this.lblActionSequenceName);
            this.Controls.Add(this.txtActionSequenceName);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(551, 616);
            this.Name = "frm_AddEdit_ActionSequence";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Action Sequence";
            this.stripActionSequence.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgActionSequence)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtActionSequenceName;
        private System.Windows.Forms.Label lblActionSequenceName;
        private System.Windows.Forms.Button btnActSeqSave;
        private System.Windows.Forms.Button btnActSeqCancel;
        private System.Windows.Forms.ContextMenuStrip stripActionSequence;
        private System.Windows.Forms.DataGridView dgActionSequence;
        private System.Windows.Forms.ToolStripMenuItem removeToolStripMenuItem;
        private System.Windows.Forms.Label lblActionSequenceComment;
        private System.Windows.Forms.TextBox txtActionSequenceComment;
        private System.Windows.Forms.Button btnMoveUp;
        private System.Windows.Forms.Button btnMoveDown;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.ToolStripMenuItem moveUpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem moveDownToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.Button btnAddAction;
        private System.Windows.Forms.Label lblActionType;
        private System.Windows.Forms.ComboBox cbActionType;
    }
}