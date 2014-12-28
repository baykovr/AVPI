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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmActionSequence));
            this.txtActionSequenceName = new System.Windows.Forms.TextBox();
            this.lblActionSequenceName = new System.Windows.Forms.Label();
            this.cbActSeqActionType = new System.Windows.Forms.ComboBox();
            this.lblActionType = new System.Windows.Forms.Label();
            this.btnActSeqSave = new System.Windows.Forms.Button();
            this.btnActSeqCancel = new System.Windows.Forms.Button();
            this.btnActSeqAdd = new System.Windows.Forms.Button();
            this.lblActionValue = new System.Windows.Forms.Label();
            this.cbActSeqActionValue = new System.Windows.Forms.ComboBox();
            this.stripActionSequence = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.removeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dgEditActionSequence = new System.Windows.Forms.DataGridView();
            this.lblActionTimes = new System.Windows.Forms.Label();
            this.txtActionXTimes = new System.Windows.Forms.TextBox();
            this.stripActionSequence.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgEditActionSequence)).BeginInit();
            this.SuspendLayout();
            // 
            // txtActionSequenceName
            // 
            this.txtActionSequenceName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtActionSequenceName.Location = new System.Drawing.Point(105, 464);
            this.txtActionSequenceName.Name = "txtActionSequenceName";
            this.txtActionSequenceName.Size = new System.Drawing.Size(115, 20);
            this.txtActionSequenceName.TabIndex = 1;
            // 
            // lblActionSequenceName
            // 
            this.lblActionSequenceName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblActionSequenceName.AutoSize = true;
            this.lblActionSequenceName.Location = new System.Drawing.Point(12, 467);
            this.lblActionSequenceName.Name = "lblActionSequenceName";
            this.lblActionSequenceName.Size = new System.Drawing.Size(87, 13);
            this.lblActionSequenceName.TabIndex = 2;
            this.lblActionSequenceName.Text = "Sequence Name";
            // 
            // cbActSeqActionType
            // 
            this.cbActSeqActionType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbActSeqActionType.FormattingEnabled = true;
            this.cbActSeqActionType.Location = new System.Drawing.Point(12, 28);
            this.cbActSeqActionType.Name = "cbActSeqActionType";
            this.cbActSeqActionType.Size = new System.Drawing.Size(122, 21);
            this.cbActSeqActionType.TabIndex = 3;
            this.cbActSeqActionType.SelectedIndexChanged += new System.EventHandler(this.cbActSeqActionType_SelectedIndexChanged);
            // 
            // lblActionType
            // 
            this.lblActionType.AutoSize = true;
            this.lblActionType.Location = new System.Drawing.Point(12, 9);
            this.lblActionType.Name = "lblActionType";
            this.lblActionType.Size = new System.Drawing.Size(64, 13);
            this.lblActionType.TabIndex = 5;
            this.lblActionType.Text = "Action Type";
            // 
            // btnActSeqSave
            // 
            this.btnActSeqSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnActSeqSave.Enabled = false;
            this.btnActSeqSave.Location = new System.Drawing.Point(330, 462);
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
            this.btnActSeqCancel.Location = new System.Drawing.Point(411, 462);
            this.btnActSeqCancel.Name = "btnActSeqCancel";
            this.btnActSeqCancel.Size = new System.Drawing.Size(75, 23);
            this.btnActSeqCancel.TabIndex = 7;
            this.btnActSeqCancel.Text = "Cancel";
            this.btnActSeqCancel.UseVisualStyleBackColor = true;
            this.btnActSeqCancel.Click += new System.EventHandler(this.btnActSeqCancel_Click);
            // 
            // btnActSeqAdd
            // 
            this.btnActSeqAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnActSeqAdd.Location = new System.Drawing.Point(411, 26);
            this.btnActSeqAdd.Name = "btnActSeqAdd";
            this.btnActSeqAdd.Size = new System.Drawing.Size(75, 23);
            this.btnActSeqAdd.TabIndex = 8;
            this.btnActSeqAdd.Text = "Add";
            this.btnActSeqAdd.UseVisualStyleBackColor = true;
            this.btnActSeqAdd.Click += new System.EventHandler(this.btnActSeqAdd_Click);
            // 
            // lblActionValue
            // 
            this.lblActionValue.AutoSize = true;
            this.lblActionValue.Location = new System.Drawing.Point(140, 9);
            this.lblActionValue.Name = "lblActionValue";
            this.lblActionValue.Size = new System.Drawing.Size(67, 13);
            this.lblActionValue.TabIndex = 9;
            this.lblActionValue.Text = "Action Value";
            // 
            // cbActSeqActionValue
            // 
            this.cbActSeqActionValue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbActSeqActionValue.FormattingEnabled = true;
            this.cbActSeqActionValue.Location = new System.Drawing.Point(140, 28);
            this.cbActSeqActionValue.Name = "cbActSeqActionValue";
            this.cbActSeqActionValue.Size = new System.Drawing.Size(142, 21);
            this.cbActSeqActionValue.TabIndex = 10;
            // 
            // stripActionSequence
            // 
            this.stripActionSequence.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.removeToolStripMenuItem});
            this.stripActionSequence.Name = "contextMenuStrip1";
            this.stripActionSequence.Size = new System.Drawing.Size(118, 26);
            // 
            // removeToolStripMenuItem
            // 
            this.removeToolStripMenuItem.Name = "removeToolStripMenuItem";
            this.removeToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.removeToolStripMenuItem.Text = "Remove";
            this.removeToolStripMenuItem.Click += new System.EventHandler(this.removeToolStripMenuItem_Click);
            // 
            // dgEditActionSequence
            // 
            this.dgEditActionSequence.AllowUserToAddRows = false;
            this.dgEditActionSequence.AllowUserToDeleteRows = false;
            this.dgEditActionSequence.AllowUserToResizeColumns = false;
            this.dgEditActionSequence.AllowUserToResizeRows = false;
            this.dgEditActionSequence.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgEditActionSequence.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgEditActionSequence.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgEditActionSequence.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgEditActionSequence.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgEditActionSequence.ContextMenuStrip = this.stripActionSequence;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgEditActionSequence.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgEditActionSequence.GridColor = System.Drawing.Color.White;
            this.dgEditActionSequence.Location = new System.Drawing.Point(15, 55);
            this.dgEditActionSequence.Name = "dgEditActionSequence";
            this.dgEditActionSequence.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgEditActionSequence.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgEditActionSequence.RowHeadersVisible = false;
            this.dgEditActionSequence.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgEditActionSequence.Size = new System.Drawing.Size(467, 401);
            this.dgEditActionSequence.TabIndex = 11;
            this.dgEditActionSequence.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.ActionSequenceList_CellMouseDown);
            // 
            // lblActionTimes
            // 
            this.lblActionTimes.AutoSize = true;
            this.lblActionTimes.Location = new System.Drawing.Point(285, 9);
            this.lblActionTimes.Name = "lblActionTimes";
            this.lblActionTimes.Size = new System.Drawing.Size(69, 13);
            this.lblActionTimes.TabIndex = 12;
            this.lblActionTimes.Text = "Times to Add";
            // 
            // txtActionXTimes
            // 
            this.txtActionXTimes.Location = new System.Drawing.Point(288, 29);
            this.txtActionXTimes.Name = "txtActionXTimes";
            this.txtActionXTimes.Size = new System.Drawing.Size(110, 20);
            this.txtActionXTimes.TabIndex = 13;
            // 
            // frmActionSequence
            // 
            this.AcceptButton = this.btnActSeqSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnActSeqCancel;
            this.ClientSize = new System.Drawing.Size(494, 493);
            this.Controls.Add(this.txtActionXTimes);
            this.Controls.Add(this.lblActionTimes);
            this.Controls.Add(this.dgEditActionSequence);
            this.Controls.Add(this.cbActSeqActionValue);
            this.Controls.Add(this.lblActionValue);
            this.Controls.Add(this.btnActSeqAdd);
            this.Controls.Add(this.btnActSeqCancel);
            this.Controls.Add(this.btnActSeqSave);
            this.Controls.Add(this.lblActionType);
            this.Controls.Add(this.cbActSeqActionType);
            this.Controls.Add(this.lblActionSequenceName);
            this.Controls.Add(this.txtActionSequenceName);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(408, 523);
            this.Name = "frmActionSequence";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Action Sequence";
            this.stripActionSequence.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgEditActionSequence)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtActionSequenceName;
        private System.Windows.Forms.Label lblActionSequenceName;
        private System.Windows.Forms.ComboBox cbActSeqActionType;
        private System.Windows.Forms.Label lblActionType;
        private System.Windows.Forms.Button btnActSeqSave;
        private System.Windows.Forms.Button btnActSeqCancel;
        private System.Windows.Forms.Button btnActSeqAdd;
        private System.Windows.Forms.Label lblActionValue;
        private System.Windows.Forms.ComboBox cbActSeqActionValue;
        private System.Windows.Forms.ContextMenuStrip stripActionSequence;
        private System.Windows.Forms.DataGridView dgEditActionSequence;
        private System.Windows.Forms.ToolStripMenuItem removeToolStripMenuItem;
        private System.Windows.Forms.Label lblActionTimes;
        private System.Windows.Forms.TextBox txtActionXTimes;
    }
}