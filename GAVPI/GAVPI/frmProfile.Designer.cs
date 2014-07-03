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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmProfile));
            this.menuProfile = new System.Windows.Forms.MenuStrip();
            this.stripProfileFile = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stripProfileTrigger = new System.Windows.Forms.ToolStripMenuItem();
            this.addNewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stripProfileActionSequence = new System.Windows.Forms.ToolStripMenuItem();
            this.addNewToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.stripProfileHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.splitProfile = new System.Windows.Forms.SplitContainer();
            this.splitProfileTriggers = new System.Windows.Forms.SplitContainer();
            this.lblProfileTriggers = new System.Windows.Forms.Label();
            this.dgTriggers = new System.Windows.Forms.DataGridView();
            this.contextProfileTriggers = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lblProfileTriggerEvents = new System.Windows.Forms.Label();
            this.dgTriggerEvents = new System.Windows.Forms.DataGridView();
            this.contextProfileTriggerEvents = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.lblProfileActionSequences = new System.Windows.Forms.Label();
            this.dgActionSequences = new System.Windows.Forms.DataGridView();
            this.contextProfileActionSequences = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.taddtoeventToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.editToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.statusProfile = new System.Windows.Forms.StatusStrip();
            this.newStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuProfile.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitProfile)).BeginInit();
            this.splitProfile.Panel1.SuspendLayout();
            this.splitProfile.Panel2.SuspendLayout();
            this.splitProfile.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitProfileTriggers)).BeginInit();
            this.splitProfileTriggers.Panel1.SuspendLayout();
            this.splitProfileTriggers.Panel2.SuspendLayout();
            this.splitProfileTriggers.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgTriggers)).BeginInit();
            this.contextProfileTriggers.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgTriggerEvents)).BeginInit();
            this.contextProfileTriggerEvents.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgActionSequences)).BeginInit();
            this.contextProfileActionSequences.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuProfile
            // 
            this.menuProfile.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stripProfileFile,
            this.stripProfileTrigger,
            this.stripProfileActionSequence,
            this.stripProfileHelp});
            this.menuProfile.Location = new System.Drawing.Point(0, 0);
            this.menuProfile.Name = "menuProfile";
            this.menuProfile.Size = new System.Drawing.Size(986, 24);
            this.menuProfile.TabIndex = 0;
            this.menuProfile.Text = "menuStrip1";
            // 
            // stripProfileFile
            // 
            this.stripProfileFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveToolStripMenuItem,
            this.closeToolStripMenuItem});
            this.stripProfileFile.Name = "stripProfileFile";
            this.stripProfileFile.Size = new System.Drawing.Size(36, 20);
            this.stripProfileFile.Text = "File";
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.closeToolStripMenuItem.Text = "Close";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
            // 
            // stripProfileTrigger
            // 
            this.stripProfileTrigger.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addNewToolStripMenuItem});
            this.stripProfileTrigger.Name = "stripProfileTrigger";
            this.stripProfileTrigger.Size = new System.Drawing.Size(50, 20);
            this.stripProfileTrigger.Text = "Trigger";
            // 
            // addNewToolStripMenuItem
            // 
            this.addNewToolStripMenuItem.Name = "addNewToolStripMenuItem";
            this.addNewToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.addNewToolStripMenuItem.Text = "New";
            this.addNewToolStripMenuItem.Click += new System.EventHandler(this.addNewToolStripMenuItem_Click);
            // 
            // stripProfileActionSequence
            // 
            this.stripProfileActionSequence.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addNewToolStripMenuItem1});
            this.stripProfileActionSequence.Name = "stripProfileActionSequence";
            this.stripProfileActionSequence.Size = new System.Drawing.Size(96, 20);
            this.stripProfileActionSequence.Text = "Action Sequence";
            // 
            // addNewToolStripMenuItem1
            // 
            this.addNewToolStripMenuItem1.Name = "addNewToolStripMenuItem1";
            this.addNewToolStripMenuItem1.Size = new System.Drawing.Size(95, 22);
            this.addNewToolStripMenuItem1.Text = "New";
            this.addNewToolStripMenuItem1.Click += new System.EventHandler(this.addNewToolStripMenuItem1_Click);
            // 
            // stripProfileHelp
            // 
            this.stripProfileHelp.Name = "stripProfileHelp";
            this.stripProfileHelp.Size = new System.Drawing.Size(41, 20);
            this.stripProfileHelp.Text = "Help";
            // 
            // splitProfile
            // 
            this.splitProfile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitProfile.Location = new System.Drawing.Point(0, 24);
            this.splitProfile.Name = "splitProfile";
            // 
            // splitProfile.Panel1
            // 
            this.splitProfile.Panel1.Controls.Add(this.splitProfileTriggers);
            // 
            // splitProfile.Panel2
            // 
            this.splitProfile.Panel2.Controls.Add(this.lblProfileActionSequences);
            this.splitProfile.Panel2.Controls.Add(this.dgActionSequences);
            this.splitProfile.Size = new System.Drawing.Size(986, 544);
            this.splitProfile.SplitterDistance = 669;
            this.splitProfile.TabIndex = 1;
            // 
            // splitProfileTriggers
            // 
            this.splitProfileTriggers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitProfileTriggers.Location = new System.Drawing.Point(0, 0);
            this.splitProfileTriggers.Name = "splitProfileTriggers";
            this.splitProfileTriggers.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitProfileTriggers.Panel1
            // 
            this.splitProfileTriggers.Panel1.Controls.Add(this.lblProfileTriggers);
            this.splitProfileTriggers.Panel1.Controls.Add(this.dgTriggers);
            // 
            // splitProfileTriggers.Panel2
            // 
            this.splitProfileTriggers.Panel2.BackColor = System.Drawing.Color.MintCream;
            this.splitProfileTriggers.Panel2.Controls.Add(this.lblProfileTriggerEvents);
            this.splitProfileTriggers.Panel2.Controls.Add(this.dgTriggerEvents);
            this.splitProfileTriggers.Size = new System.Drawing.Size(669, 544);
            this.splitProfileTriggers.SplitterDistance = 196;
            this.splitProfileTriggers.TabIndex = 2;
            // 
            // lblProfileTriggers
            // 
            this.lblProfileTriggers.AutoSize = true;
            this.lblProfileTriggers.Location = new System.Drawing.Point(3, 0);
            this.lblProfileTriggers.Name = "lblProfileTriggers";
            this.lblProfileTriggers.Size = new System.Drawing.Size(45, 13);
            this.lblProfileTriggers.TabIndex = 1;
            this.lblProfileTriggers.Text = "Triggers";
            // 
            // dgTriggers
            // 
            this.dgTriggers.AllowUserToAddRows = false;
            this.dgTriggers.AllowUserToDeleteRows = false;
            this.dgTriggers.AllowUserToResizeRows = false;
            this.dgTriggers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgTriggers.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgTriggers.BackgroundColor = System.Drawing.Color.White;
            this.dgTriggers.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(144)))), ((int)(((byte)(169)))), ((int)(((byte)(89)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgTriggers.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgTriggers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgTriggers.ContextMenuStrip = this.contextProfileTriggers;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgTriggers.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgTriggers.Location = new System.Drawing.Point(6, 16);
            this.dgTriggers.MultiSelect = false;
            this.dgTriggers.Name = "dgTriggers";
            this.dgTriggers.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(144)))), ((int)(((byte)(169)))), ((int)(((byte)(89)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgTriggers.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgTriggers.RowHeadersVisible = false;
            this.dgTriggers.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dgTriggers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgTriggers.Size = new System.Drawing.Size(660, 177);
            this.dgTriggers.TabIndex = 0;
            this.dgTriggers.SelectionChanged += new System.EventHandler(this.dgTriggers_SelectionChanged);
            // 
            // contextProfileTriggers
            // 
            this.contextProfileTriggers.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newStripMenuItem,
            this.editToolStripMenuItem,
            this.deleteToolStripMenuItem});
            this.contextProfileTriggers.Name = "contextProfileTriggers";
            this.contextProfileTriggers.Size = new System.Drawing.Size(153, 92);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.editToolStripMenuItem.Text = "Edit";
            this.editToolStripMenuItem.Click += new System.EventHandler(this.editToolStripMenuItem_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // lblProfileTriggerEvents
            // 
            this.lblProfileTriggerEvents.AutoSize = true;
            this.lblProfileTriggerEvents.Location = new System.Drawing.Point(12, 0);
            this.lblProfileTriggerEvents.Name = "lblProfileTriggerEvents";
            this.lblProfileTriggerEvents.Size = new System.Drawing.Size(76, 13);
            this.lblProfileTriggerEvents.TabIndex = 2;
            this.lblProfileTriggerEvents.Text = "Trigger Events";
            // 
            // dgTriggerEvents
            // 
            this.dgTriggerEvents.AllowUserToAddRows = false;
            this.dgTriggerEvents.AllowUserToDeleteRows = false;
            this.dgTriggerEvents.AllowUserToResizeRows = false;
            this.dgTriggerEvents.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgTriggerEvents.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgTriggerEvents.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgTriggerEvents.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgTriggerEvents.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgTriggerEvents.ContextMenuStrip = this.contextProfileTriggerEvents;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgTriggerEvents.DefaultCellStyle = dataGridViewCellStyle5;
            this.dgTriggerEvents.GridColor = System.Drawing.Color.White;
            this.dgTriggerEvents.Location = new System.Drawing.Point(6, 16);
            this.dgTriggerEvents.MultiSelect = false;
            this.dgTriggerEvents.Name = "dgTriggerEvents";
            this.dgTriggerEvents.ReadOnly = true;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgTriggerEvents.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dgTriggerEvents.RowHeadersVisible = false;
            this.dgTriggerEvents.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgTriggerEvents.Size = new System.Drawing.Size(660, 303);
            this.dgTriggerEvents.TabIndex = 3;
            // 
            // contextProfileTriggerEvents
            // 
            this.contextProfileTriggerEvents.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteToolStripMenuItem2});
            this.contextProfileTriggerEvents.Name = "contextProfileTriggerEvents";
            this.contextProfileTriggerEvents.Size = new System.Drawing.Size(107, 26);
            // 
            // deleteToolStripMenuItem2
            // 
            this.deleteToolStripMenuItem2.Name = "deleteToolStripMenuItem2";
            this.deleteToolStripMenuItem2.Size = new System.Drawing.Size(106, 22);
            this.deleteToolStripMenuItem2.Text = "Delete";
            this.deleteToolStripMenuItem2.Click += new System.EventHandler(this.deleteToolStripMenuItem2_Click);
            // 
            // lblProfileActionSequences
            // 
            this.lblProfileActionSequences.AutoSize = true;
            this.lblProfileActionSequences.Location = new System.Drawing.Point(3, 0);
            this.lblProfileActionSequences.Name = "lblProfileActionSequences";
            this.lblProfileActionSequences.Size = new System.Drawing.Size(91, 13);
            this.lblProfileActionSequences.TabIndex = 2;
            this.lblProfileActionSequences.Text = "ActionSequences";
            // 
            // dgActionSequences
            // 
            this.dgActionSequences.AllowUserToAddRows = false;
            this.dgActionSequences.AllowUserToDeleteRows = false;
            this.dgActionSequences.AllowUserToResizeRows = false;
            this.dgActionSequences.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgActionSequences.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgActionSequences.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgActionSequences.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dgActionSequences.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgActionSequences.ContextMenuStrip = this.contextProfileActionSequences;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgActionSequences.DefaultCellStyle = dataGridViewCellStyle8;
            this.dgActionSequences.GridColor = System.Drawing.Color.White;
            this.dgActionSequences.Location = new System.Drawing.Point(6, 16);
            this.dgActionSequences.MultiSelect = false;
            this.dgActionSequences.Name = "dgActionSequences";
            this.dgActionSequences.ReadOnly = true;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgActionSequences.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.dgActionSequences.RowHeadersVisible = false;
            this.dgActionSequences.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgActionSequences.Size = new System.Drawing.Size(304, 503);
            this.dgActionSequences.TabIndex = 0;
            // 
            // contextProfileActionSequences
            // 
            this.contextProfileActionSequences.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.taddtoeventToolStripMenuItem1,
            this.toolStripSeparator1,
            this.editToolStripMenuItem1,
            this.deleteToolStripMenuItem1});
            this.contextProfileActionSequences.Name = "contextProfileActionSequences";
            this.contextProfileActionSequences.Size = new System.Drawing.Size(166, 76);
            // 
            // taddtoeventToolStripMenuItem1
            // 
            this.taddtoeventToolStripMenuItem1.Name = "taddtoeventToolStripMenuItem1";
            this.taddtoeventToolStripMenuItem1.Size = new System.Drawing.Size(165, 22);
            this.taddtoeventToolStripMenuItem1.Text = "Add to Trigger Event";
            this.taddtoeventToolStripMenuItem1.Click += new System.EventHandler(this.taddtoeventToolStripMenuItem1_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(162, 6);
            // 
            // editToolStripMenuItem1
            // 
            this.editToolStripMenuItem1.Name = "editToolStripMenuItem1";
            this.editToolStripMenuItem1.Size = new System.Drawing.Size(165, 22);
            this.editToolStripMenuItem1.Text = "Edit";
            this.editToolStripMenuItem1.Click += new System.EventHandler(this.editToolStripMenuItem1_Click);
            // 
            // deleteToolStripMenuItem1
            // 
            this.deleteToolStripMenuItem1.Name = "deleteToolStripMenuItem1";
            this.deleteToolStripMenuItem1.Size = new System.Drawing.Size(165, 22);
            this.deleteToolStripMenuItem1.Text = "Delete";
            this.deleteToolStripMenuItem1.Click += new System.EventHandler(this.deleteToolStripMenuItem1_Click);
            // 
            // statusProfile
            // 
            this.statusProfile.Location = new System.Drawing.Point(0, 546);
            this.statusProfile.Name = "statusProfile";
            this.statusProfile.Size = new System.Drawing.Size(986, 22);
            this.statusProfile.TabIndex = 2;
            this.statusProfile.Text = "statusStrip1";
            // 
            // newStripMenuItem
            // 
            this.newStripMenuItem.Name = "newStripMenuItem";
            this.newStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.newStripMenuItem.Text = "New";
            this.newStripMenuItem.Click += new System.EventHandler(this.newStripMenuItem_Click);
            // 
            // frmProfile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(986, 568);
            this.Controls.Add(this.statusProfile);
            this.Controls.Add(this.splitProfile);
            this.Controls.Add(this.menuProfile);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuProfile;
            this.MinimumSize = new System.Drawing.Size(729, 291);
            this.Name = "frmProfile";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Profile";
            this.menuProfile.ResumeLayout(false);
            this.menuProfile.PerformLayout();
            this.splitProfile.Panel1.ResumeLayout(false);
            this.splitProfile.Panel2.ResumeLayout(false);
            this.splitProfile.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitProfile)).EndInit();
            this.splitProfile.ResumeLayout(false);
            this.splitProfileTriggers.Panel1.ResumeLayout(false);
            this.splitProfileTriggers.Panel1.PerformLayout();
            this.splitProfileTriggers.Panel2.ResumeLayout(false);
            this.splitProfileTriggers.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitProfileTriggers)).EndInit();
            this.splitProfileTriggers.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgTriggers)).EndInit();
            this.contextProfileTriggers.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgTriggerEvents)).EndInit();
            this.contextProfileTriggerEvents.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgActionSequences)).EndInit();
            this.contextProfileActionSequences.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuProfile;
        private System.Windows.Forms.SplitContainer splitProfile;
        private System.Windows.Forms.DataGridView dgTriggers;
        private System.Windows.Forms.DataGridView dgActionSequences;
        private System.Windows.Forms.ToolStripMenuItem stripProfileTrigger;
        private System.Windows.Forms.ToolStripMenuItem stripProfileActionSequence;
        private System.Windows.Forms.ToolStripMenuItem addNewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addNewToolStripMenuItem1;
        private System.Windows.Forms.Label lblProfileTriggers;
        private System.Windows.Forms.Label lblProfileActionSequences;
        private System.Windows.Forms.ContextMenuStrip contextProfileTriggers;
        private System.Windows.Forms.ContextMenuStrip contextProfileActionSequences;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem1;
        private System.Windows.Forms.SplitContainer splitProfileTriggers;
        private System.Windows.Forms.Label lblProfileTriggerEvents;
        private System.Windows.Forms.DataGridView dgTriggerEvents;
        private System.Windows.Forms.StatusStrip statusProfile;
        private System.Windows.Forms.ToolStripMenuItem stripProfileHelp;
        private System.Windows.Forms.ToolStripMenuItem taddtoeventToolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ContextMenuStrip contextProfileTriggerEvents;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem stripProfileFile;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newStripMenuItem;
    }
}