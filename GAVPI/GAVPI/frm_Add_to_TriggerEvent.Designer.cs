namespace GAVPI
{
    partial class frm_Add_to_TriggerEvent
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_Add_to_TriggerEvent));
            this.lblAddtoTriggerName = new System.Windows.Forms.Label();
            this.cbAddtoTriggerEvent = new System.Windows.Forms.ComboBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblAddtoTriggerName
            // 
            this.lblAddtoTriggerName.AutoSize = true;
            this.lblAddtoTriggerName.Location = new System.Drawing.Point(12, 30);
            this.lblAddtoTriggerName.Name = "lblAddtoTriggerName";
            this.lblAddtoTriggerName.Size = new System.Drawing.Size(40, 13);
            this.lblAddtoTriggerName.TabIndex = 0;
            this.lblAddtoTriggerName.Text = "Trigger";
            // 
            // cbAddtoTriggerEvent
            // 
            this.cbAddtoTriggerEvent.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbAddtoTriggerEvent.FormattingEnabled = true;
            this.cbAddtoTriggerEvent.Location = new System.Drawing.Point(58, 27);
            this.cbAddtoTriggerEvent.Name = "cbAddtoTriggerEvent";
            this.cbAddtoTriggerEvent.Size = new System.Drawing.Size(279, 21);
            this.cbAddtoTriggerEvent.TabIndex = 1;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(181, 76);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 2;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(262, 76);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // frmAddtoTriggerEvent
            // 
            this.AcceptButton = this.btnAdd;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(351, 115);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.cbAddtoTriggerEvent);
            this.Controls.Add(this.lblAddtoTriggerName);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(361, 145);
            this.MinimumSize = new System.Drawing.Size(361, 145);
            this.Name = "frmAddtoTriggerEvent";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add to Trigger";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblAddtoTriggerName;
        private System.Windows.Forms.ComboBox cbAddtoTriggerEvent;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnCancel;
    }
}