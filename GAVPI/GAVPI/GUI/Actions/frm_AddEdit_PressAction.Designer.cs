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
            this.btnKeyFrmPress = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblPressType
            // 
            this.lblPressType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPressType.AutoSize = true;
            this.lblPressType.Location = new System.Drawing.Point(14, 35);
            this.lblPressType.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPressType.Name = "lblPressType";
            this.lblPressType.Size = new System.Drawing.Size(87, 20);
            this.lblPressType.TabIndex = 7;
            this.lblPressType.Text = "Press Type";
            // 
            // cbPressType
            // 
            this.cbPressType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbPressType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPressType.FormattingEnabled = true;
            this.cbPressType.Location = new System.Drawing.Point(114, 31);
            this.cbPressType.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbPressType.Name = "cbPressType";
            this.cbPressType.Size = new System.Drawing.Size(480, 28);
            this.cbPressType.TabIndex = 1;
            this.cbPressType.SelectedIndexChanged += new System.EventHandler(this.cbPressType_SelectedIndexChanged);
            // 
            // lblPressValue
            // 
            this.lblPressValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPressValue.AutoSize = true;
            this.lblPressValue.Location = new System.Drawing.Point(52, 83);
            this.lblPressValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPressValue.Name = "lblPressValue";
            this.lblPressValue.Size = new System.Drawing.Size(50, 20);
            this.lblPressValue.TabIndex = 8;
            this.lblPressValue.Text = "Value";
            // 
            // cbPressValue
            // 
            this.cbPressValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbPressValue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPressValue.FormattingEnabled = true;
            this.cbPressValue.Location = new System.Drawing.Point(114, 78);
            this.cbPressValue.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbPressValue.Name = "cbPressValue";
            this.cbPressValue.Size = new System.Drawing.Size(334, 28);
            this.cbPressValue.TabIndex = 2;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(483, 148);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(112, 35);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.Location = new System.Drawing.Point(363, 148);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(112, 35);
            this.btnAdd.TabIndex = 3;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // txtTimesToAdd
            // 
            this.txtTimesToAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtTimesToAdd.Enabled = false;
            this.txtTimesToAdd.Location = new System.Drawing.Point(200, 186);
            this.txtTimesToAdd.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtTimesToAdd.Name = "txtTimesToAdd";
            this.txtTimesToAdd.Size = new System.Drawing.Size(94, 26);
            this.txtTimesToAdd.TabIndex = 12;
            // 
            // chckMultiAdd
            // 
            this.chckMultiAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chckMultiAdd.AutoSize = true;
            this.chckMultiAdd.Location = new System.Drawing.Point(18, 191);
            this.chckMultiAdd.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chckMultiAdd.Name = "chckMultiAdd";
            this.chckMultiAdd.Size = new System.Drawing.Size(168, 24);
            this.chckMultiAdd.TabIndex = 13;
            this.chckMultiAdd.Text = "Add Multiple Times";
            this.chckMultiAdd.UseVisualStyleBackColor = true;
            this.chckMultiAdd.CheckedChanged += new System.EventHandler(this.chckMultiAdd_CheckedChanged);
            // 
            // btnKeyFrmPress
            // 
            this.btnKeyFrmPress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnKeyFrmPress.Location = new System.Drawing.Point(459, 78);
            this.btnKeyFrmPress.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnKeyFrmPress.Name = "btnKeyFrmPress";
            this.btnKeyFrmPress.Size = new System.Drawing.Size(136, 35);
            this.btnKeyFrmPress.TabIndex = 14;
            this.btnKeyFrmPress.Text = "From Press";
            this.btnKeyFrmPress.UseVisualStyleBackColor = true;
            this.btnKeyFrmPress.Click += new System.EventHandler(this.btnKeyFrmPress_Click);
            // 
            // frm_AddEdit_PressAction
            // 
            this.AcceptButton = this.btnAdd;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(596, 197);
            this.Controls.Add(this.btnKeyFrmPress);
            this.Controls.Add(this.chckMultiAdd);
            this.Controls.Add(this.txtTimesToAdd);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.cbPressValue);
            this.Controls.Add(this.lblPressValue);
            this.Controls.Add(this.lblPressType);
            this.Controls.Add(this.cbPressType);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximumSize = new System.Drawing.Size(618, 253);
            this.MinimumSize = new System.Drawing.Size(618, 253);
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
        private System.Windows.Forms.Button btnKeyFrmPress;
    }
}