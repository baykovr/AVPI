namespace GAVPI
{
    partial class frmSettings
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
            this.lblSettingsRecognizerLang = new System.Windows.Forms.Label();
            this.cbxSettingsSpeechRecognizer = new System.Windows.Forms.ComboBox();
            this.btnSettingsSave = new System.Windows.Forms.Button();
            this.btnSettingsCancel = new System.Windows.Forms.Button();
            this.btnSettingsDefaults = new System.Windows.Forms.Button();
            this.lblSettingsSpeechSynthesizer = new System.Windows.Forms.Label();
            this.cbxSettingsSynthesizer = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // lblSettingsRecognizerLang
            // 
            this.lblSettingsRecognizerLang.AutoSize = true;
            this.lblSettingsRecognizerLang.Location = new System.Drawing.Point(12, 12);
            this.lblSettingsRecognizerLang.Name = "lblSettingsRecognizerLang";
            this.lblSettingsRecognizerLang.Size = new System.Drawing.Size(146, 13);
            this.lblSettingsRecognizerLang.TabIndex = 0;
            this.lblSettingsRecognizerLang.Text = "Speech Regonizer Language";
            // 
            // cbxSettingsSpeechRecognizer
            // 
            this.cbxSettingsSpeechRecognizer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxSettingsSpeechRecognizer.FormattingEnabled = true;
            this.cbxSettingsSpeechRecognizer.Location = new System.Drawing.Point(164, 9);
            this.cbxSettingsSpeechRecognizer.Name = "cbxSettingsSpeechRecognizer";
            this.cbxSettingsSpeechRecognizer.Size = new System.Drawing.Size(121, 21);
            this.cbxSettingsSpeechRecognizer.TabIndex = 1;
            // 
            // btnSettingsSave
            // 
            this.btnSettingsSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSettingsSave.Location = new System.Drawing.Point(387, 340);
            this.btnSettingsSave.Name = "btnSettingsSave";
            this.btnSettingsSave.Size = new System.Drawing.Size(75, 23);
            this.btnSettingsSave.TabIndex = 2;
            this.btnSettingsSave.Text = "Save";
            this.btnSettingsSave.UseVisualStyleBackColor = true;
            // 
            // btnSettingsCancel
            // 
            this.btnSettingsCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSettingsCancel.Location = new System.Drawing.Point(468, 340);
            this.btnSettingsCancel.Name = "btnSettingsCancel";
            this.btnSettingsCancel.Size = new System.Drawing.Size(75, 23);
            this.btnSettingsCancel.TabIndex = 3;
            this.btnSettingsCancel.Text = "Cancel";
            this.btnSettingsCancel.UseVisualStyleBackColor = true;
            // 
            // btnSettingsDefaults
            // 
            this.btnSettingsDefaults.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSettingsDefaults.Location = new System.Drawing.Point(549, 340);
            this.btnSettingsDefaults.Name = "btnSettingsDefaults";
            this.btnSettingsDefaults.Size = new System.Drawing.Size(75, 23);
            this.btnSettingsDefaults.TabIndex = 4;
            this.btnSettingsDefaults.Text = "Defaults";
            this.btnSettingsDefaults.UseVisualStyleBackColor = true;
            // 
            // lblSettingsSpeechSynthesizer
            // 
            this.lblSettingsSpeechSynthesizer.AutoSize = true;
            this.lblSettingsSpeechSynthesizer.Location = new System.Drawing.Point(12, 52);
            this.lblSettingsSpeechSynthesizer.Name = "lblSettingsSpeechSynthesizer";
            this.lblSettingsSpeechSynthesizer.Size = new System.Drawing.Size(101, 13);
            this.lblSettingsSpeechSynthesizer.TabIndex = 5;
            this.lblSettingsSpeechSynthesizer.Text = "Speech Synthesizer";
            // 
            // cbxSettingsSynthesizer
            // 
            this.cbxSettingsSynthesizer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxSettingsSynthesizer.FormattingEnabled = true;
            this.cbxSettingsSynthesizer.Location = new System.Drawing.Point(164, 49);
            this.cbxSettingsSynthesizer.Name = "cbxSettingsSynthesizer";
            this.cbxSettingsSynthesizer.Size = new System.Drawing.Size(121, 21);
            this.cbxSettingsSynthesizer.TabIndex = 6;
            // 
            // frmSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(636, 375);
            this.Controls.Add(this.cbxSettingsSynthesizer);
            this.Controls.Add(this.lblSettingsSpeechSynthesizer);
            this.Controls.Add(this.btnSettingsDefaults);
            this.Controls.Add(this.btnSettingsCancel);
            this.Controls.Add(this.btnSettingsSave);
            this.Controls.Add(this.cbxSettingsSpeechRecognizer);
            this.Controls.Add(this.lblSettingsRecognizerLang);
            this.Name = "frmSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Settings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblSettingsRecognizerLang;
        private System.Windows.Forms.ComboBox cbxSettingsSpeechRecognizer;
        private System.Windows.Forms.Button btnSettingsSave;
        private System.Windows.Forms.Button btnSettingsCancel;
        private System.Windows.Forms.Button btnSettingsDefaults;
        private System.Windows.Forms.Label lblSettingsSpeechSynthesizer;
        private System.Windows.Forms.ComboBox cbxSettingsSynthesizer;
    }
}