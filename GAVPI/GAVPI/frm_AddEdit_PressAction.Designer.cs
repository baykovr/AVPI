namespace GAVPI
{
    partial class frm_AddEdit_PressAction
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_AddEdit_PressAction));
            this.lblPressType = new System.Windows.Forms.Label();
            this.cbPressType = new System.Windows.Forms.ComboBox();
            this.lblPressValue = new System.Windows.Forms.Label();
            this.cbPressValue = new System.Windows.Forms.ComboBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.txtTimesToAdd = new System.Windows.Forms.TextBox();
            this.chckMultiAdd = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // lblPressType
            // 
            this.lblPressType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPressType.AutoSize = true;
            this.lblPressType.Location = new System.Drawing.Point(9, 23);
            this.lblPressType.Name = "lblPressType";
            this.lblPressType.Size = new System.Drawing.Size(60, 13);
            this.lblPressType.TabIndex = 7;
            this.lblPressType.Text = "Press Type";
            // 
            // cbPressType
            // 
            this.cbPressType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbPressType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPressType.FormattingEnabled = true;
            this.cbPressType.Location = new System.Drawing.Point(76, 20);
            this.cbPressType.Name = "cbPressType";
            this.cbPressType.Size = new System.Drawing.Size(283, 21);
            this.cbPressType.TabIndex = 1;
            this.cbPressType.SelectedIndexChanged += new System.EventHandler(this.cbPressType_SelectedIndexChanged);
            // 
            // lblPressValue
            // 
            this.lblPressValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPressValue.AutoSize = true;
            this.lblPressValue.Location = new System.Drawing.Point(35, 54);
            this.lblPressValue.Name = "lblPressValue";
            this.lblPressValue.Size = new System.Drawing.Size(34, 13);
            this.lblPressValue.TabIndex = 8;
            this.lblPressValue.Text = "Value";
            // 
            // cbPressValue
            // 
            this.cbPressValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbPressValue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPressValue.FormattingEnabled = true;
            this.cbPressValue.Location = new System.Drawing.Point(76, 51);
            this.cbPressValue.Name = "cbPressValue";
            this.cbPressValue.Size = new System.Drawing.Size(283, 21);
            this.cbPressValue.TabIndex = 2;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(284, 121);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.Location = new System.Drawing.Point(203, 121);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 3;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // txtTimesToAdd
            // 
            this.txtTimesToAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtTimesToAdd.Enabled = false;
            this.txtTimesToAdd.Location = new System.Drawing.Point(12, 123);
            this.txtTimesToAdd.Name = "txtTimesToAdd";
            this.txtTimesToAdd.Size = new System.Drawing.Size(115, 20);
            this.txtTimesToAdd.TabIndex = 12;
            // 
            // chckMultiAdd
            // 
            this.chckMultiAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chckMultiAdd.AutoSize = true;
            this.chckMultiAdd.Location = new System.Drawing.Point(12, 101);
            this.chckMultiAdd.Name = "chckMultiAdd";
            this.chckMultiAdd.Size = new System.Drawing.Size(115, 17);
            this.chckMultiAdd.TabIndex = 13;
            this.chckMultiAdd.Text = "Add Multiple Times";
            this.chckMultiAdd.UseVisualStyleBackColor = true;
            this.chckMultiAdd.CheckedChanged += new System.EventHandler(this.chckMultiAdd_CheckedChanged);
            // 
            // frm_AddEdit_PressAction
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(371, 156);
            this.Controls.Add(this.chckMultiAdd);
            this.Controls.Add(this.txtTimesToAdd);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.cbPressValue);
            this.Controls.Add(this.lblPressValue);
            this.Controls.Add(this.lblPressType);
            this.Controls.Add(this.cbPressType);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(381, 186);
            this.MinimumSize = new System.Drawing.Size(381, 186);
            this.Name = "frm_AddEdit_PressAction";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Key/Mouse Press Action";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblPressType;
        private System.Windows.Forms.ComboBox cbPressType;
        private System.Windows.Forms.Label lblPressValue;
        private System.Windows.Forms.ComboBox cbPressValue;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.TextBox txtTimesToAdd;
        private System.Windows.Forms.CheckBox chckMultiAdd;
    }
}