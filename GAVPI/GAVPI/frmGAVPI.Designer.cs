namespace GAVPI
{
    partial class frmGAVPI
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
            this.mainStatStrip = new System.Windows.Forms.StatusStrip();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.mainStripFile = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainStripSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.mainStripAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.mainStripProfile = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainStatStrip
            // 
            this.mainStatStrip.Location = new System.Drawing.Point(0, 422);
            this.mainStatStrip.Name = "mainStatStrip";
            this.mainStatStrip.Size = new System.Drawing.Size(905, 22);
            this.mainStatStrip.TabIndex = 0;
            this.mainStatStrip.Text = "statusStrip1";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mainStripFile,
            this.mainStripProfile,
            this.mainStripSettings,
            this.mainStripAbout});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(905, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "mainMenuStrip";
            // 
            // mainStripFile
            // 
            this.mainStripFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.mainStripFile.Name = "mainStripFile";
            this.mainStripFile.Size = new System.Drawing.Size(37, 20);
            this.mainStripFile.Text = "File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            // 
            // mainStripSettings
            // 
            this.mainStripSettings.Name = "mainStripSettings";
            this.mainStripSettings.Size = new System.Drawing.Size(62, 20);
            this.mainStripSettings.Text = "Settings";
            this.mainStripSettings.Click += new System.EventHandler(this.mainStripSettings_Click);
            // 
            // mainStripAbout
            // 
            this.mainStripAbout.Name = "mainStripAbout";
            this.mainStripAbout.Size = new System.Drawing.Size(52, 20);
            this.mainStripAbout.Text = "About";
            // 
            // mainStripProfile
            // 
            this.mainStripProfile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editToolStripMenuItem});
            this.mainStripProfile.Name = "mainStripProfile";
            this.mainStripProfile.Size = new System.Drawing.Size(53, 20);
            this.mainStripProfile.Text = "Profile";
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.editToolStripMenuItem.Text = "Modify";
            this.editToolStripMenuItem.Click += new System.EventHandler(this.modifyToolStripMenuItem_Click);
            // 
            // frmGAVPI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(905, 444);
            this.Controls.Add(this.mainStatStrip);
            this.Controls.Add(this.menuStrip1);
            this.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmGAVPI";
            this.Text = "GAVPI : Graphical Artifical Virtual Pilot Interface";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip mainStatStrip;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mainStripFile;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mainStripSettings;
        private System.Windows.Forms.ToolStripMenuItem mainStripAbout;
        private System.Windows.Forms.ToolStripMenuItem mainStripProfile;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
    }
}

