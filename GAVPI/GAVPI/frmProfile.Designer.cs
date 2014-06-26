namespace GAVPI
{
    partial class frmProfile
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
            this.menuProfile = new System.Windows.Forms.MenuStrip();
            this.splitProfile = new System.Windows.Forms.SplitContainer();
            this.dgTriggers = new System.Windows.Forms.DataGridView();
            this.dgActionSeqeunces = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.splitProfile)).BeginInit();
            this.splitProfile.Panel1.SuspendLayout();
            this.splitProfile.Panel2.SuspendLayout();
            this.splitProfile.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgTriggers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgActionSeqeunces)).BeginInit();
            this.SuspendLayout();
            // 
            // menuProfile
            // 
            this.menuProfile.Location = new System.Drawing.Point(0, 0);
            this.menuProfile.Name = "menuProfile";
            this.menuProfile.Size = new System.Drawing.Size(950, 24);
            this.menuProfile.TabIndex = 0;
            this.menuProfile.Text = "menuStrip1";
            // 
            // splitProfile
            // 
            this.splitProfile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitProfile.Location = new System.Drawing.Point(0, 24);
            this.splitProfile.Name = "splitProfile";
            // 
            // splitProfile.Panel1
            // 
            this.splitProfile.Panel1.Controls.Add(this.dgTriggers);
            // 
            // splitProfile.Panel2
            // 
            this.splitProfile.Panel2.Controls.Add(this.dgActionSeqeunces);
            this.splitProfile.Size = new System.Drawing.Size(950, 525);
            this.splitProfile.SplitterDistance = 350;
            this.splitProfile.TabIndex = 1;
            // 
            // dgTriggers
            // 
            this.dgTriggers.AllowUserToAddRows = false;
            this.dgTriggers.AllowUserToDeleteRows = false;
            this.dgTriggers.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgTriggers.BackgroundColor = System.Drawing.Color.White;
            this.dgTriggers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgTriggers.Location = new System.Drawing.Point(3, 23);
            this.dgTriggers.Name = "dgTriggers";
            this.dgTriggers.ReadOnly = true;
            this.dgTriggers.Size = new System.Drawing.Size(344, 499);
            this.dgTriggers.TabIndex = 0;
            // 
            // dgActionSeqeunces
            // 
            this.dgActionSeqeunces.AllowUserToAddRows = false;
            this.dgActionSeqeunces.AllowUserToDeleteRows = false;
            this.dgActionSeqeunces.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgActionSeqeunces.BackgroundColor = System.Drawing.Color.White;
            this.dgActionSeqeunces.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgActionSeqeunces.GridColor = System.Drawing.Color.White;
            this.dgActionSeqeunces.Location = new System.Drawing.Point(3, 23);
            this.dgActionSeqeunces.Name = "dgActionSeqeunces";
            this.dgActionSeqeunces.ReadOnly = true;
            this.dgActionSeqeunces.Size = new System.Drawing.Size(590, 499);
            this.dgActionSeqeunces.TabIndex = 0;
            // 
            // frmProfile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(950, 549);
            this.Controls.Add(this.splitProfile);
            this.Controls.Add(this.menuProfile);
            this.MainMenuStrip = this.menuProfile;
            this.Name = "frmProfile";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Profile";
            this.splitProfile.Panel1.ResumeLayout(false);
            this.splitProfile.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitProfile)).EndInit();
            this.splitProfile.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgTriggers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgActionSeqeunces)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuProfile;
        private System.Windows.Forms.SplitContainer splitProfile;
        private System.Windows.Forms.DataGridView dgTriggers;
        private System.Windows.Forms.DataGridView dgActionSeqeunces;
    }
}