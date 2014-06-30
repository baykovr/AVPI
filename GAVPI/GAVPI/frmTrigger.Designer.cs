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
            this.lblTriggerType = new System.Windows.Forms.Label();
            this.cbTriggerType = new System.Windows.Forms.ComboBox();
            this.lblTriggerValue = new System.Windows.Forms.Label();
            this.txtTriggerValue = new System.Windows.Forms.TextBox();
            this.btnTriggerOk = new System.Windows.Forms.Button();
            this.btnTriggerCancel = new System.Windows.Forms.Button();
            this.lblTriggerName = new System.Windows.Forms.Label();
            this.txtTriggerName = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lblTriggerType
            // 
            this.lblTriggerType.AutoSize = true;
            this.lblTriggerType.Location = new System.Drawing.Point(256, 39);
            this.lblTriggerType.Name = "lblTriggerType";
            this.lblTriggerType.Size = new System.Drawing.Size(31, 13);
            this.lblTriggerType.TabIndex = 0;
            this.lblTriggerType.Text = "Type";
            // 
            // cbTriggerType
            // 
            this.cbTriggerType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbTriggerType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTriggerType.FormattingEnabled = true;
            this.cbTriggerType.Location = new System.Drawing.Point(293, 36);
            this.cbTriggerType.Name = "cbTriggerType";
            this.cbTriggerType.Size = new System.Drawing.Size(130, 21);
            this.cbTriggerType.TabIndex = 1;
            // 
            // lblTriggerValue
            // 
            this.lblTriggerValue.AutoSize = true;
            this.lblTriggerValue.Location = new System.Drawing.Point(13, 89);
            this.lblTriggerValue.Name = "lblTriggerValue";
            this.lblTriggerValue.Size = new System.Drawing.Size(34, 13);
            this.lblTriggerValue.TabIndex = 2;
            this.lblTriggerValue.Text = "Value";
            // 
            // txtTriggerValue
            // 
            this.txtTriggerValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTriggerValue.CharacterCasing = System.Windows.Forms.CharacterCasing.Lower;
            this.txtTriggerValue.Location = new System.Drawing.Point(52, 86);
            this.txtTriggerValue.Name = "txtTriggerValue";
            this.txtTriggerValue.Size = new System.Drawing.Size(371, 20);
            this.txtTriggerValue.TabIndex = 3;
            // 
            // btnTriggerOk
            // 
            this.btnTriggerOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTriggerOk.Location = new System.Drawing.Point(276, 144);
            this.btnTriggerOk.Name = "btnTriggerOk";
            this.btnTriggerOk.Size = new System.Drawing.Size(75, 23);
            this.btnTriggerOk.TabIndex = 4;
            this.btnTriggerOk.Text = "Ok";
            this.btnTriggerOk.UseVisualStyleBackColor = true;
            this.btnTriggerOk.Click += new System.EventHandler(this.btnTriggerOk_Click);
            // 
            // btnTriggerCancel
            // 
            this.btnTriggerCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTriggerCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnTriggerCancel.Location = new System.Drawing.Point(357, 144);
            this.btnTriggerCancel.Name = "btnTriggerCancel";
            this.btnTriggerCancel.Size = new System.Drawing.Size(75, 23);
            this.btnTriggerCancel.TabIndex = 5;
            this.btnTriggerCancel.Text = "Cancel";
            this.btnTriggerCancel.UseVisualStyleBackColor = true;
            this.btnTriggerCancel.Click += new System.EventHandler(this.btnTriggerCancel_Click);
            // 
            // lblTriggerName
            // 
            this.lblTriggerName.AutoSize = true;
            this.lblTriggerName.Location = new System.Drawing.Point(12, 39);
            this.lblTriggerName.Name = "lblTriggerName";
            this.lblTriggerName.Size = new System.Drawing.Size(35, 13);
            this.lblTriggerName.TabIndex = 6;
            this.lblTriggerName.Text = "Name";
            // 
            // txtTriggerName
            // 
            this.txtTriggerName.CharacterCasing = System.Windows.Forms.CharacterCasing.Lower;
            this.txtTriggerName.Location = new System.Drawing.Point(52, 36);
            this.txtTriggerName.Name = "txtTriggerName";
            this.txtTriggerName.Size = new System.Drawing.Size(198, 20);
            this.txtTriggerName.TabIndex = 7;
            // 
            // frmTrigger
            // 
            this.AcceptButton = this.btnTriggerOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnTriggerCancel;
            this.ClientSize = new System.Drawing.Size(444, 179);
            this.Controls.Add(this.txtTriggerName);
            this.Controls.Add(this.lblTriggerName);
            this.Controls.Add(this.btnTriggerCancel);
            this.Controls.Add(this.btnTriggerOk);
            this.Controls.Add(this.txtTriggerValue);
            this.Controls.Add(this.lblTriggerValue);
            this.Controls.Add(this.cbTriggerType);
            this.Controls.Add(this.lblTriggerType);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(456, 213);
            this.Name = "frmTrigger";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Trigger";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTriggerType;
        private System.Windows.Forms.ComboBox cbTriggerType;
        private System.Windows.Forms.Label lblTriggerValue;
        private System.Windows.Forms.TextBox txtTriggerValue;
        private System.Windows.Forms.Button btnTriggerOk;
        private System.Windows.Forms.Button btnTriggerCancel;
        private System.Windows.Forms.Label lblTriggerName;
        private System.Windows.Forms.TextBox txtTriggerName;
    }
}