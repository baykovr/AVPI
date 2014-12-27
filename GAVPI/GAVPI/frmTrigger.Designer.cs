namespace GAVPI
{
    partial class frmTrigger
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTrigger));
            this.btnTriggerOk = new System.Windows.Forms.Button();
            this.btnTriggerCancel = new System.Windows.Forms.Button();
            this.lblTriggerName = new System.Windows.Forms.Label();
            this.txtTriggerName = new System.Windows.Forms.TextBox();
            this.tbTriggerTypes = new System.Windows.Forms.TabControl();
            this.tabPagePhrase = new System.Windows.Forms.TabPage();
            this.tabPageLogical = new System.Windows.Forms.TabPage();
            this.txtTriggerValue = new System.Windows.Forms.TextBox();
            this.lblTriggerValue = new System.Windows.Forms.Label();
            this.lblif = new System.Windows.Forms.Label();
            this.cbActSeqActionType = new System.Windows.Forms.ComboBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.tbTriggerTypes.SuspendLayout();
            this.tabPagePhrase.SuspendLayout();
            this.tabPageLogical.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnTriggerOk
            // 
            this.btnTriggerOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTriggerOk.Location = new System.Drawing.Point(380, 172);
            this.btnTriggerOk.Name = "btnTriggerOk";
            this.btnTriggerOk.Size = new System.Drawing.Size(75, 23);
            this.btnTriggerOk.TabIndex = 3;
            this.btnTriggerOk.Text = "Ok";
            this.btnTriggerOk.UseVisualStyleBackColor = true;
            this.btnTriggerOk.Click += new System.EventHandler(this.btnTriggerOk_Click);
            // 
            // btnTriggerCancel
            // 
            this.btnTriggerCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTriggerCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnTriggerCancel.Location = new System.Drawing.Point(461, 172);
            this.btnTriggerCancel.Name = "btnTriggerCancel";
            this.btnTriggerCancel.Size = new System.Drawing.Size(75, 23);
            this.btnTriggerCancel.TabIndex = 4;
            this.btnTriggerCancel.Text = "Cancel";
            this.btnTriggerCancel.UseVisualStyleBackColor = true;
            this.btnTriggerCancel.Click += new System.EventHandler(this.btnTriggerCancel_Click);
            // 
            // lblTriggerName
            // 
            this.lblTriggerName.AutoSize = true;
            this.lblTriggerName.Location = new System.Drawing.Point(12, 12);
            this.lblTriggerName.Name = "lblTriggerName";
            this.lblTriggerName.Size = new System.Drawing.Size(35, 13);
            this.lblTriggerName.TabIndex = 6;
            this.lblTriggerName.Text = "Name";
            // 
            // txtTriggerName
            // 
            this.txtTriggerName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTriggerName.CharacterCasing = System.Windows.Forms.CharacterCasing.Lower;
            this.txtTriggerName.Location = new System.Drawing.Point(54, 9);
            this.txtTriggerName.Name = "txtTriggerName";
            this.txtTriggerName.Size = new System.Drawing.Size(477, 20);
            this.txtTriggerName.TabIndex = 1;
            // 
            // tbTriggerTypes
            // 
            this.tbTriggerTypes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbTriggerTypes.Controls.Add(this.tabPagePhrase);
            this.tbTriggerTypes.Controls.Add(this.tabPageLogical);
            this.tbTriggerTypes.Location = new System.Drawing.Point(16, 35);
            this.tbTriggerTypes.Name = "tbTriggerTypes";
            this.tbTriggerTypes.SelectedIndex = 0;
            this.tbTriggerTypes.Size = new System.Drawing.Size(519, 132);
            this.tbTriggerTypes.TabIndex = 7;
            // 
            // tabPagePhrase
            // 
            this.tabPagePhrase.Controls.Add(this.txtTriggerValue);
            this.tabPagePhrase.Controls.Add(this.lblTriggerValue);
            this.tabPagePhrase.Location = new System.Drawing.Point(4, 22);
            this.tabPagePhrase.Margin = new System.Windows.Forms.Padding(5);
            this.tabPagePhrase.Name = "tabPagePhrase";
            this.tabPagePhrase.Padding = new System.Windows.Forms.Padding(8);
            this.tabPagePhrase.Size = new System.Drawing.Size(511, 106);
            this.tabPagePhrase.TabIndex = 0;
            this.tabPagePhrase.Text = "VI_Phrase";
            this.tabPagePhrase.UseVisualStyleBackColor = true;
            // 
            // tabPageLogical
            // 
            this.tabPageLogical.Controls.Add(this.textBox1);
            this.tabPageLogical.Controls.Add(this.comboBox1);
            this.tabPageLogical.Controls.Add(this.cbActSeqActionType);
            this.tabPageLogical.Controls.Add(this.lblif);
            this.tabPageLogical.Location = new System.Drawing.Point(4, 22);
            this.tabPageLogical.Margin = new System.Windows.Forms.Padding(5);
            this.tabPageLogical.Name = "tabPageLogical";
            this.tabPageLogical.Padding = new System.Windows.Forms.Padding(15);
            this.tabPageLogical.Size = new System.Drawing.Size(511, 106);
            this.tabPageLogical.TabIndex = 1;
            this.tabPageLogical.Text = "VI_Logical";
            this.tabPageLogical.UseVisualStyleBackColor = true;
            // 
            // txtTriggerValue
            // 
            this.txtTriggerValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTriggerValue.CharacterCasing = System.Windows.Forms.CharacterCasing.Lower;
            this.txtTriggerValue.Location = new System.Drawing.Point(6, 24);
            this.txtTriggerValue.Name = "txtTriggerValue";
            this.txtTriggerValue.Size = new System.Drawing.Size(494, 20);
            this.txtTriggerValue.TabIndex = 3;
            // 
            // lblTriggerValue
            // 
            this.lblTriggerValue.AutoSize = true;
            this.lblTriggerValue.Location = new System.Drawing.Point(3, 8);
            this.lblTriggerValue.Name = "lblTriggerValue";
            this.lblTriggerValue.Size = new System.Drawing.Size(184, 13);
            this.lblTriggerValue.TabIndex = 4;
            this.lblTriggerValue.Text = "Speach Recognition Word or Phrase ";
            // 
            // lblif
            // 
            this.lblif.AutoSize = true;
            this.lblif.Location = new System.Drawing.Point(10, 15);
            this.lblif.Name = "lblif";
            this.lblif.Size = new System.Drawing.Size(12, 13);
            this.lblif.TabIndex = 0;
            this.lblif.Text = "if";
            // 
            // cbActSeqActionType
            // 
            this.cbActSeqActionType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbActSeqActionType.FormattingEnabled = true;
            this.cbActSeqActionType.Location = new System.Drawing.Point(21, 12);
            this.cbActSeqActionType.Name = "cbActSeqActionType";
            this.cbActSeqActionType.Size = new System.Drawing.Size(122, 21);
            this.cbActSeqActionType.TabIndex = 4;
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(149, 12);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(122, 21);
            this.comboBox1.TabIndex = 5;
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Location = new System.Drawing.Point(277, 12);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(231, 20);
            this.textBox1.TabIndex = 6;
            // 
            // frmTrigger
            // 
            this.AcceptButton = this.btnTriggerOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnTriggerCancel;
            this.ClientSize = new System.Drawing.Size(550, 211);
            this.Controls.Add(this.tbTriggerTypes);
            this.Controls.Add(this.txtTriggerName);
            this.Controls.Add(this.lblTriggerName);
            this.Controls.Add(this.btnTriggerCancel);
            this.Controls.Add(this.btnTriggerOk);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(560, 241);
            this.Name = "frmTrigger";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Trigger";
            this.tbTriggerTypes.ResumeLayout(false);
            this.tabPagePhrase.ResumeLayout(false);
            this.tabPagePhrase.PerformLayout();
            this.tabPageLogical.ResumeLayout(false);
            this.tabPageLogical.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnTriggerOk;
        private System.Windows.Forms.Button btnTriggerCancel;
        private System.Windows.Forms.Label lblTriggerName;
        private System.Windows.Forms.TextBox txtTriggerName;
        private System.Windows.Forms.TabControl tbTriggerTypes;
        private System.Windows.Forms.TabPage tabPagePhrase;
        private System.Windows.Forms.TabPage tabPageLogical;
        private System.Windows.Forms.TextBox txtTriggerValue;
        private System.Windows.Forms.Label lblTriggerValue;
        private System.Windows.Forms.Label lblif;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ComboBox cbActSeqActionType;
    }
}