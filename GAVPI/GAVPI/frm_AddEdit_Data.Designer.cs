namespace GAVPI
{
    partial class frm_AddEdit_Data
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_AddEdit_Data));
            this.txtDataComment = new System.Windows.Forms.TextBox();
            this.lblTriggerComment = new System.Windows.Forms.Label();
            this.lblDataType = new System.Windows.Forms.Label();
            this.txtDataValue = new System.Windows.Forms.TextBox();
            this.txtDataName = new System.Windows.Forms.TextBox();
            this.lblDataName = new System.Windows.Forms.Label();
            this.btnTriggerCancel = new System.Windows.Forms.Button();
            this.btnTriggerOk = new System.Windows.Forms.Button();
            this.lblDataValue = new System.Windows.Forms.Label();
            this.cbDataType = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // txtDataComment
            // 
            this.txtDataComment.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDataComment.CharacterCasing = System.Windows.Forms.CharacterCasing.Lower;
            this.txtDataComment.Location = new System.Drawing.Point(14, 134);
            this.txtDataComment.Name = "txtDataComment";
            this.txtDataComment.Size = new System.Drawing.Size(495, 20);
            this.txtDataComment.TabIndex = 16;
            // 
            // lblTriggerComment
            // 
            this.lblTriggerComment.AutoSize = true;
            this.lblTriggerComment.Location = new System.Drawing.Point(12, 118);
            this.lblTriggerComment.Name = "lblTriggerComment";
            this.lblTriggerComment.Size = new System.Drawing.Size(51, 13);
            this.lblTriggerComment.TabIndex = 15;
            this.lblTriggerComment.Text = "Comment";
            // 
            // lblDataType
            // 
            this.lblDataType.AutoSize = true;
            this.lblDataType.Location = new System.Drawing.Point(12, 66);
            this.lblDataType.Name = "lblDataType";
            this.lblDataType.Size = new System.Drawing.Size(60, 13);
            this.lblDataType.TabIndex = 12;
            this.lblDataType.Text = "Data Value";
            // 
            // txtDataValue
            // 
            this.txtDataValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDataValue.CharacterCasing = System.Windows.Forms.CharacterCasing.Lower;
            this.txtDataValue.Location = new System.Drawing.Point(177, 82);
            this.txtDataValue.Name = "txtDataValue";
            this.txtDataValue.Size = new System.Drawing.Size(332, 20);
            this.txtDataValue.TabIndex = 10;
            // 
            // txtDataName
            // 
            this.txtDataName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDataName.CharacterCasing = System.Windows.Forms.CharacterCasing.Lower;
            this.txtDataName.Location = new System.Drawing.Point(14, 27);
            this.txtDataName.Name = "txtDataName";
            this.txtDataName.Size = new System.Drawing.Size(495, 20);
            this.txtDataName.TabIndex = 9;
            // 
            // lblDataName
            // 
            this.lblDataName.AutoSize = true;
            this.lblDataName.Location = new System.Drawing.Point(12, 9);
            this.lblDataName.Name = "lblDataName";
            this.lblDataName.Size = new System.Drawing.Size(61, 13);
            this.lblDataName.TabIndex = 14;
            this.lblDataName.Text = "Data Name";
            // 
            // btnTriggerCancel
            // 
            this.btnTriggerCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTriggerCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnTriggerCancel.Location = new System.Drawing.Point(434, 160);
            this.btnTriggerCancel.Name = "btnTriggerCancel";
            this.btnTriggerCancel.Size = new System.Drawing.Size(75, 23);
            this.btnTriggerCancel.TabIndex = 13;
            this.btnTriggerCancel.Text = "Cancel";
            this.btnTriggerCancel.UseVisualStyleBackColor = true;
            // 
            // btnTriggerOk
            // 
            this.btnTriggerOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTriggerOk.Location = new System.Drawing.Point(353, 160);
            this.btnTriggerOk.Name = "btnTriggerOk";
            this.btnTriggerOk.Size = new System.Drawing.Size(75, 23);
            this.btnTriggerOk.TabIndex = 11;
            this.btnTriggerOk.Text = "Ok";
            this.btnTriggerOk.UseVisualStyleBackColor = true;
            // 
            // lblDataValue
            // 
            this.lblDataValue.AutoSize = true;
            this.lblDataValue.Location = new System.Drawing.Point(174, 66);
            this.lblDataValue.Name = "lblDataValue";
            this.lblDataValue.Size = new System.Drawing.Size(60, 13);
            this.lblDataValue.TabIndex = 17;
            this.lblDataValue.Text = "Data Value";
            // 
            // cbDataType
            // 
            this.cbDataType.FormattingEnabled = true;
            this.cbDataType.Location = new System.Drawing.Point(15, 82);
            this.cbDataType.Name = "cbDataType";
            this.cbDataType.Size = new System.Drawing.Size(156, 21);
            this.cbDataType.TabIndex = 18;
            // 
            // frm_AddEdit_Data
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(521, 192);
            this.Controls.Add(this.cbDataType);
            this.Controls.Add(this.lblDataValue);
            this.Controls.Add(this.txtDataComment);
            this.Controls.Add(this.lblTriggerComment);
            this.Controls.Add(this.lblDataType);
            this.Controls.Add(this.txtDataValue);
            this.Controls.Add(this.txtDataName);
            this.Controls.Add(this.lblDataName);
            this.Controls.Add(this.btnTriggerCancel);
            this.Controls.Add(this.btnTriggerOk);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frm_AddEdit_Data";
            this.Text = "Data";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtDataComment;
        private System.Windows.Forms.Label lblTriggerComment;
        private System.Windows.Forms.Label lblDataType;
        private System.Windows.Forms.TextBox txtDataValue;
        private System.Windows.Forms.TextBox txtDataName;
        private System.Windows.Forms.Label lblDataName;
        private System.Windows.Forms.Button btnTriggerCancel;
        private System.Windows.Forms.Button btnTriggerOk;
        private System.Windows.Forms.Label lblDataValue;
        private System.Windows.Forms.ComboBox cbDataType;
    }
}