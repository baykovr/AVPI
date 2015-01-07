namespace GAVPI
{
    partial class frm_AddEdit_PhraseTrigger
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_AddEdit_PhraseTrigger));
            this.btnTriggerOk = new System.Windows.Forms.Button();
            this.btnTriggerCancel = new System.Windows.Forms.Button();
            this.lblTriggerName = new System.Windows.Forms.Label();
            this.txtTriggerName = new System.Windows.Forms.TextBox();
            this.txtTriggerValue = new System.Windows.Forms.TextBox();
            this.lblTriggerValue = new System.Windows.Forms.Label();
            this.lblTriggerComment = new System.Windows.Forms.Label();
            this.txtTriggerComment = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnTriggerOk
            // 
            this.btnTriggerOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTriggerOk.Location = new System.Drawing.Point(270, 181);
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
            this.btnTriggerCancel.Location = new System.Drawing.Point(351, 181);
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
            this.lblTriggerName.Location = new System.Drawing.Point(13, 12);
            this.lblTriggerName.Name = "lblTriggerName";
            this.lblTriggerName.Size = new System.Drawing.Size(71, 13);
            this.lblTriggerName.TabIndex = 6;
            this.lblTriggerName.Text = "Trigger Name";
            // 
            // txtTriggerName
            // 
            this.txtTriggerName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTriggerName.CharacterCasing = System.Windows.Forms.CharacterCasing.Lower;
            this.txtTriggerName.Location = new System.Drawing.Point(15, 30);
            this.txtTriggerName.Name = "txtTriggerName";
            this.txtTriggerName.Size = new System.Drawing.Size(410, 20);
            this.txtTriggerName.TabIndex = 1;
            // 
            // txtTriggerValue
            // 
            this.txtTriggerValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTriggerValue.CharacterCasing = System.Windows.Forms.CharacterCasing.Lower;
            this.txtTriggerValue.Location = new System.Drawing.Point(15, 85);
            this.txtTriggerValue.Name = "txtTriggerValue";
            this.txtTriggerValue.Size = new System.Drawing.Size(411, 20);
            this.txtTriggerValue.TabIndex = 2;
            // 
            // lblTriggerValue
            // 
            this.lblTriggerValue.AutoSize = true;
            this.lblTriggerValue.Location = new System.Drawing.Point(13, 69);
            this.lblTriggerValue.Name = "lblTriggerValue";
            this.lblTriggerValue.Size = new System.Drawing.Size(184, 13);
            this.lblTriggerValue.TabIndex = 4;
            this.lblTriggerValue.Text = "Speach Recognition Word or Phrase ";
            // 
            // lblTriggerComment
            // 
            this.lblTriggerComment.AutoSize = true;
            this.lblTriggerComment.Location = new System.Drawing.Point(13, 121);
            this.lblTriggerComment.Name = "lblTriggerComment";
            this.lblTriggerComment.Size = new System.Drawing.Size(51, 13);
            this.lblTriggerComment.TabIndex = 7;
            this.lblTriggerComment.Text = "Comment";
            // 
            // txtTriggerComment
            // 
            this.txtTriggerComment.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTriggerComment.CharacterCasing = System.Windows.Forms.CharacterCasing.Lower;
            this.txtTriggerComment.Location = new System.Drawing.Point(15, 137);
            this.txtTriggerComment.Name = "txtTriggerComment";
            this.txtTriggerComment.Size = new System.Drawing.Size(411, 20);
            this.txtTriggerComment.TabIndex = 3;
            // 
            // frm_AddEdit_PhraseTrigger
            // 
            this.AcceptButton = this.btnTriggerOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnTriggerCancel;
            this.ClientSize = new System.Drawing.Size(440, 220);
            this.Controls.Add(this.txtTriggerComment);
            this.Controls.Add(this.lblTriggerComment);
            this.Controls.Add(this.lblTriggerValue);
            this.Controls.Add(this.txtTriggerValue);
            this.Controls.Add(this.txtTriggerName);
            this.Controls.Add(this.lblTriggerName);
            this.Controls.Add(this.btnTriggerCancel);
            this.Controls.Add(this.btnTriggerOk);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(450, 250);
            this.Name = "frm_AddEdit_PhraseTrigger";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Trigger";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnTriggerOk;
        private System.Windows.Forms.Button btnTriggerCancel;
        private System.Windows.Forms.Label lblTriggerName;
        private System.Windows.Forms.TextBox txtTriggerName;
        private System.Windows.Forms.TextBox txtTriggerValue;
        private System.Windows.Forms.Label lblTriggerValue;
        private System.Windows.Forms.Label lblTriggerComment;
        private System.Windows.Forms.TextBox txtTriggerComment;
    }
}