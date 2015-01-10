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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSettings));
            this.lblSettingsRecognizerLang = new System.Windows.Forms.Label();
            this.btnSettingsSave = new System.Windows.Forms.Button();
            this.btnSettingsCancel = new System.Windows.Forms.Button();
            this.lblSettingsSpeechSynthesizer = new System.Windows.Forms.Label();
            this.cbSettingsSynthesizer = new System.Windows.Forms.ComboBox();
            this.lblSettingsSpeechRecordingDevice = new System.Windows.Forms.Label();
            this.cbSettingsRecordingDevice = new System.Windows.Forms.ComboBox();
            this.cbSettingsLanguage = new System.Windows.Forms.ComboBox();
            this.cbSettingsPushToTalkMode = new System.Windows.Forms.ComboBox();
            this.lblSettingsPushToTalkMode = new System.Windows.Forms.Label();
            this.cbSettingsPushToTalkKey = new System.Windows.Forms.ComboBox();
            this.lblSettingsPushToTalkKey = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblSettingsRecognizerLang
            // 
            this.lblSettingsRecognizerLang.AutoSize = true;
            this.lblSettingsRecognizerLang.Location = new System.Drawing.Point(12, 9);
            this.lblSettingsRecognizerLang.Name = "lblSettingsRecognizerLang";
            this.lblSettingsRecognizerLang.Size = new System.Drawing.Size(146, 13);
            this.lblSettingsRecognizerLang.TabIndex = 0;
            this.lblSettingsRecognizerLang.Text = "Speech Regonizer Language";
            // 
            // btnSettingsSave
            // 
            this.btnSettingsSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSettingsSave.Location = new System.Drawing.Point(255, 198);
            this.btnSettingsSave.Name = "btnSettingsSave";
            this.btnSettingsSave.Size = new System.Drawing.Size(75, 23);
            this.btnSettingsSave.TabIndex = 2;
            this.btnSettingsSave.Text = "Save";
            this.btnSettingsSave.UseVisualStyleBackColor = true;
            this.btnSettingsSave.Click += new System.EventHandler(this.btnSettingsSave_Click);
            // 
            // btnSettingsCancel
            // 
            this.btnSettingsCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSettingsCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnSettingsCancel.Location = new System.Drawing.Point(336, 198);
            this.btnSettingsCancel.Name = "btnSettingsCancel";
            this.btnSettingsCancel.Size = new System.Drawing.Size(75, 23);
            this.btnSettingsCancel.TabIndex = 3;
            this.btnSettingsCancel.Text = "Cancel";
            this.btnSettingsCancel.UseVisualStyleBackColor = true;
            this.btnSettingsCancel.Click += new System.EventHandler(this.btnSettingsCancel_Click);
            // 
            // lblSettingsSpeechSynthesizer
            // 
            this.lblSettingsSpeechSynthesizer.AutoSize = true;
            this.lblSettingsSpeechSynthesizer.Location = new System.Drawing.Point(12, 49);
            this.lblSettingsSpeechSynthesizer.Name = "lblSettingsSpeechSynthesizer";
            this.lblSettingsSpeechSynthesizer.Size = new System.Drawing.Size(101, 13);
            this.lblSettingsSpeechSynthesizer.TabIndex = 5;
            this.lblSettingsSpeechSynthesizer.Text = "Speech Synthesizer";
            // 
            // cbSettingsSynthesizer
            // 
            this.cbSettingsSynthesizer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbSettingsSynthesizer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSettingsSynthesizer.FormattingEnabled = true;
            this.cbSettingsSynthesizer.Location = new System.Drawing.Point(226, 46);
            this.cbSettingsSynthesizer.Name = "cbSettingsSynthesizer";
            this.cbSettingsSynthesizer.Size = new System.Drawing.Size(185, 21);
            this.cbSettingsSynthesizer.TabIndex = 6;
            // 
            // lblSettingsSpeechRecordingDevice
            // 
            this.lblSettingsSpeechRecordingDevice.AutoSize = true;
            this.lblSettingsSpeechRecordingDevice.Location = new System.Drawing.Point(12, 89);
            this.lblSettingsSpeechRecordingDevice.Name = "lblSettingsSpeechRecordingDevice";
            this.lblSettingsSpeechRecordingDevice.Size = new System.Drawing.Size(93, 13);
            this.lblSettingsSpeechRecordingDevice.TabIndex = 7;
            this.lblSettingsSpeechRecordingDevice.Text = "Recording Device";
            // 
            // cbSettingsRecordingDevice
            // 
            this.cbSettingsRecordingDevice.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbSettingsRecordingDevice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSettingsRecordingDevice.FormattingEnabled = true;
            this.cbSettingsRecordingDevice.Location = new System.Drawing.Point(226, 86);
            this.cbSettingsRecordingDevice.Name = "cbSettingsRecordingDevice";
            this.cbSettingsRecordingDevice.Size = new System.Drawing.Size(185, 21);
            this.cbSettingsRecordingDevice.TabIndex = 8;
            // 
            // cbSettingsLanguage
            // 
            this.cbSettingsLanguage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbSettingsLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSettingsLanguage.FormattingEnabled = true;
            this.cbSettingsLanguage.Location = new System.Drawing.Point(226, 6);
            this.cbSettingsLanguage.Name = "cbSettingsLanguage";
            this.cbSettingsLanguage.Size = new System.Drawing.Size(185, 21);
            this.cbSettingsLanguage.TabIndex = 1;
            // 
            // cbSettingsPushToTalkMode
            // 
            this.cbSettingsPushToTalkMode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbSettingsPushToTalkMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSettingsPushToTalkMode.FormattingEnabled = true;
            this.cbSettingsPushToTalkMode.Location = new System.Drawing.Point(226, 126);
            this.cbSettingsPushToTalkMode.Name = "cbSettingsPushToTalkMode";
            this.cbSettingsPushToTalkMode.Size = new System.Drawing.Size(185, 21);
            this.cbSettingsPushToTalkMode.TabIndex = 10;
            // 
            // lblSettingsPushToTalkMode
            // 
            this.lblSettingsPushToTalkMode.AutoSize = true;
            this.lblSettingsPushToTalkMode.Location = new System.Drawing.Point(12, 129);
            this.lblSettingsPushToTalkMode.Name = "lblSettingsPushToTalkMode";
            this.lblSettingsPushToTalkMode.Size = new System.Drawing.Size(100, 13);
            this.lblSettingsPushToTalkMode.TabIndex = 9;
            this.lblSettingsPushToTalkMode.Text = "Push-To-Talk mode";
            // 
            // cbSettingsPushToTalkKey
            // 
            this.cbSettingsPushToTalkKey.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbSettingsPushToTalkKey.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSettingsPushToTalkKey.FormattingEnabled = true;
            this.cbSettingsPushToTalkKey.Location = new System.Drawing.Point(226, 166);
            this.cbSettingsPushToTalkKey.Name = "cbSettingsPushToTalkKey";
            this.cbSettingsPushToTalkKey.Size = new System.Drawing.Size(185, 21);
            this.cbSettingsPushToTalkKey.TabIndex = 12;
            // 
            // lblSettingsPushToTalkKey
            // 
            this.lblSettingsPushToTalkKey.AutoSize = true;
            this.lblSettingsPushToTalkKey.Location = new System.Drawing.Point(12, 169);
            this.lblSettingsPushToTalkKey.Name = "lblSettingsPushToTalkKey";
            this.lblSettingsPushToTalkKey.Size = new System.Drawing.Size(91, 13);
            this.lblSettingsPushToTalkKey.TabIndex = 11;
            this.lblSettingsPushToTalkKey.Text = "Push-To-Talk key";
            // 
            // frmSettings
            // 
            this.AcceptButton = this.btnSettingsSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnSettingsCancel;
            this.ClientSize = new System.Drawing.Size(423, 233);
            this.Controls.Add(this.cbSettingsPushToTalkKey);
            this.Controls.Add(this.lblSettingsPushToTalkKey);
            this.Controls.Add(this.cbSettingsPushToTalkMode);
            this.Controls.Add(this.lblSettingsPushToTalkMode);
            this.Controls.Add(this.cbSettingsRecordingDevice);
            this.Controls.Add(this.lblSettingsSpeechRecordingDevice);
            this.Controls.Add(this.cbSettingsSynthesizer);
            this.Controls.Add(this.lblSettingsSpeechSynthesizer);
            this.Controls.Add(this.btnSettingsCancel);
            this.Controls.Add(this.btnSettingsSave);
            this.Controls.Add(this.cbSettingsLanguage);
            this.Controls.Add(this.lblSettingsRecognizerLang);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(433, 263);
            this.Name = "frmSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Settings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblSettingsRecognizerLang;
        private System.Windows.Forms.Button btnSettingsSave;
        private System.Windows.Forms.Button btnSettingsCancel;
        private System.Windows.Forms.Label lblSettingsSpeechSynthesizer;
        private System.Windows.Forms.ComboBox cbSettingsSynthesizer;
        private System.Windows.Forms.Label lblSettingsSpeechRecordingDevice;
        private System.Windows.Forms.ComboBox cbSettingsRecordingDevice;
        private System.Windows.Forms.ComboBox cbSettingsLanguage;
        private System.Windows.Forms.ComboBox cbSettingsPushToTalkMode;
        private System.Windows.Forms.Label lblSettingsPushToTalkMode;
        private System.Windows.Forms.ComboBox cbSettingsPushToTalkKey;
        private System.Windows.Forms.Label lblSettingsPushToTalkKey;
    }
}