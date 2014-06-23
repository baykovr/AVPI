namespace GAVPI
{
    partial class GAVPI
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
            this.mainToolStrip = new System.Windows.Forms.ToolStrip();
            this.SuspendLayout();
            // 
            // mainStatStrip
            // 
            this.mainStatStrip.Location = new System.Drawing.Point(0, 331);
            this.mainStatStrip.Name = "mainStatStrip";
            this.mainStatStrip.Size = new System.Drawing.Size(732, 22);
            this.mainStatStrip.TabIndex = 0;
            this.mainStatStrip.Text = "statusStrip1";
            // 
            // mainToolStrip
            // 
            this.mainToolStrip.Location = new System.Drawing.Point(0, 0);
            this.mainToolStrip.Name = "mainToolStrip";
            this.mainToolStrip.Size = new System.Drawing.Size(732, 25);
            this.mainToolStrip.TabIndex = 1;
            this.mainToolStrip.Text = "toolStrip1";
            // 
            // GAVPI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(732, 353);
            this.Controls.Add(this.mainToolStrip);
            this.Controls.Add(this.mainStatStrip);
            this.Name = "GAVPI";
            this.Text = "GAVPI : Graphical Artifical Virtual Pilot Interface";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip mainStatStrip;
        private System.Windows.Forms.ToolStrip mainToolStrip;
    }
}

