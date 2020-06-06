namespace physics_debugger
{
    partial class Main
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
            this.mainSplitContainer = new System.Windows.Forms.SplitContainer();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.propertiesRichTextBox = new System.Windows.Forms.RichTextBox();
            this.propertiesRichTextBox2 = new System.Windows.Forms.RichTextBox();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.mainTabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.frameDetailsRichTextBox = new System.Windows.Forms.RichTextBox();
            this.settingsTabControl = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.mainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openTelemetryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveTelemetryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.connectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.connectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.disconnectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cameraToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lookAtPointToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resetCameraToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.trackSelectedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.centreSelectedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zoomSelectedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.commandsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.goToFrameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.updateTimer = new System.Windows.Forms.Timer(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.playForwardsButton = new System.Windows.Forms.Button();
            this.playBackwardsButton = new System.Windows.Forms.Button();
            this.frameCounterTextBox = new System.Windows.Forms.TextBox();
            this.goToLastFrameButton = new System.Windows.Forms.Button();
            this.nextFrameButton = new System.Windows.Forms.Button();
            this.frameTrackBar = new System.Windows.Forms.TrackBar();
            this.previousFrameButton = new System.Windows.Forms.Button();
            this.goToFirstFrameButton = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.mainViewport = new Renderer.DirectXControl();
            ((System.ComponentModel.ISupportInitialize)(this.mainSplitContainer)).BeginInit();
            this.mainSplitContainer.Panel1.SuspendLayout();
            this.mainSplitContainer.Panel2.SuspendLayout();
            this.mainSplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).BeginInit();
            this.splitContainer4.Panel1.SuspendLayout();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            this.mainTabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.settingsTabControl.SuspendLayout();
            this.mainMenuStrip.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.frameTrackBar)).BeginInit();
            this.SuspendLayout();
            // 
            // mainSplitContainer
            // 
            this.mainSplitContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mainSplitContainer.Location = new System.Drawing.Point(13, 93);
            this.mainSplitContainer.Margin = new System.Windows.Forms.Padding(4);
            this.mainSplitContainer.Name = "mainSplitContainer";
            // 
            // mainSplitContainer.Panel1
            // 
            this.mainSplitContainer.Panel1.Controls.Add(this.splitContainer3);
            // 
            // mainSplitContainer.Panel2
            // 
            this.mainSplitContainer.Panel2.Controls.Add(this.splitContainer2);
            this.mainSplitContainer.Size = new System.Drawing.Size(1318, 778);
            this.mainSplitContainer.SplitterDistance = 250;
            this.mainSplitContainer.SplitterWidth = 5;
            this.mainSplitContainer.TabIndex = 0;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Margin = new System.Windows.Forms.Padding(4);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.propertiesRichTextBox);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.propertiesRichTextBox2);
            this.splitContainer3.Size = new System.Drawing.Size(250, 778);
            this.splitContainer3.SplitterDistance = 406;
            this.splitContainer3.SplitterWidth = 5;
            this.splitContainer3.TabIndex = 0;
            // 
            // propertiesRichTextBox
            // 
            this.propertiesRichTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertiesRichTextBox.Location = new System.Drawing.Point(0, 0);
            this.propertiesRichTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.propertiesRichTextBox.Name = "propertiesRichTextBox";
            this.propertiesRichTextBox.Size = new System.Drawing.Size(250, 406);
            this.propertiesRichTextBox.TabIndex = 0;
            this.propertiesRichTextBox.Text = "Prototype properties panel";
            // 
            // propertiesRichTextBox2
            // 
            this.propertiesRichTextBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertiesRichTextBox2.Location = new System.Drawing.Point(0, 0);
            this.propertiesRichTextBox2.Margin = new System.Windows.Forms.Padding(4);
            this.propertiesRichTextBox2.Name = "propertiesRichTextBox2";
            this.propertiesRichTextBox2.Size = new System.Drawing.Size(250, 367);
            this.propertiesRichTextBox2.TabIndex = 0;
            this.propertiesRichTextBox2.Text = "Prototype properties panel 2";
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Margin = new System.Windows.Forms.Padding(4);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.splitContainer4);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.settingsTabControl);
            this.splitContainer2.Size = new System.Drawing.Size(1063, 778);
            this.splitContainer2.SplitterDistance = 793;
            this.splitContainer2.SplitterWidth = 5;
            this.splitContainer2.TabIndex = 0;
            // 
            // splitContainer4
            // 
            this.splitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer4.Location = new System.Drawing.Point(0, 0);
            this.splitContainer4.Margin = new System.Windows.Forms.Padding(4);
            this.splitContainer4.Name = "splitContainer4";
            this.splitContainer4.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer4.Panel1
            // 
            this.splitContainer4.Panel1.Controls.Add(this.mainTabControl);
            // 
            // splitContainer4.Panel2
            // 
            this.splitContainer4.Panel2.Controls.Add(this.frameDetailsRichTextBox);
            this.splitContainer4.Size = new System.Drawing.Size(793, 778);
            this.splitContainer4.SplitterDistance = 530;
            this.splitContainer4.SplitterWidth = 5;
            this.splitContainer4.TabIndex = 0;
            // 
            // mainTabControl
            // 
            this.mainTabControl.Controls.Add(this.tabPage1);
            this.mainTabControl.Controls.Add(this.tabPage2);
            this.mainTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainTabControl.Location = new System.Drawing.Point(0, 0);
            this.mainTabControl.Margin = new System.Windows.Forms.Padding(4);
            this.mainTabControl.Name = "mainTabControl";
            this.mainTabControl.SelectedIndex = 0;
            this.mainTabControl.Size = new System.Drawing.Size(793, 530);
            this.mainTabControl.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.mainViewport);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(4);
            this.tabPage1.Size = new System.Drawing.Size(785, 501);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(4);
            this.tabPage2.Size = new System.Drawing.Size(785, 501);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // frameDetailsRichTextBox
            // 
            this.frameDetailsRichTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.frameDetailsRichTextBox.Location = new System.Drawing.Point(0, 0);
            this.frameDetailsRichTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.frameDetailsRichTextBox.Name = "frameDetailsRichTextBox";
            this.frameDetailsRichTextBox.Size = new System.Drawing.Size(793, 243);
            this.frameDetailsRichTextBox.TabIndex = 0;
            this.frameDetailsRichTextBox.Text = "Frame properties prototype";
            // 
            // settingsTabControl
            // 
            this.settingsTabControl.Controls.Add(this.tabPage3);
            this.settingsTabControl.Controls.Add(this.tabPage4);
            this.settingsTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.settingsTabControl.Location = new System.Drawing.Point(0, 0);
            this.settingsTabControl.Margin = new System.Windows.Forms.Padding(4);
            this.settingsTabControl.Name = "settingsTabControl";
            this.settingsTabControl.SelectedIndex = 0;
            this.settingsTabControl.Size = new System.Drawing.Size(265, 778);
            this.settingsTabControl.TabIndex = 0;
            // 
            // tabPage3
            // 
            this.tabPage3.Location = new System.Drawing.Point(4, 25);
            this.tabPage3.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(4);
            this.tabPage3.Size = new System.Drawing.Size(257, 749);
            this.tabPage3.TabIndex = 0;
            this.tabPage3.Text = "tabPage3";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            this.tabPage4.Location = new System.Drawing.Point(4, 25);
            this.tabPage4.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(4);
            this.tabPage4.Size = new System.Drawing.Size(257, 749);
            this.tabPage4.TabIndex = 1;
            this.tabPage4.Text = "tabPage4";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // mainMenuStrip
            // 
            this.mainMenuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.connectionToolStripMenuItem,
            this.cameraToolStripMenuItem,
            this.commandsToolStripMenuItem});
            this.mainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.mainMenuStrip.Name = "mainMenuStrip";
            this.mainMenuStrip.Size = new System.Drawing.Size(1344, 30);
            this.mainMenuStrip.TabIndex = 1;
            this.mainMenuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openTelemetryToolStripMenuItem,
            this.saveTelemetryToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(46, 26);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openTelemetryToolStripMenuItem
            // 
            this.openTelemetryToolStripMenuItem.Name = "openTelemetryToolStripMenuItem";
            this.openTelemetryToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openTelemetryToolStripMenuItem.Size = new System.Drawing.Size(259, 26);
            this.openTelemetryToolStripMenuItem.Text = "Open Telemetry...";
            this.openTelemetryToolStripMenuItem.Click += new System.EventHandler(this.openTelemetryToolStripMenuItem_Click);
            // 
            // saveTelemetryToolStripMenuItem
            // 
            this.saveTelemetryToolStripMenuItem.Name = "saveTelemetryToolStripMenuItem";
            this.saveTelemetryToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveTelemetryToolStripMenuItem.Size = new System.Drawing.Size(259, 26);
            this.saveTelemetryToolStripMenuItem.Text = "Save Telemetry...";
            this.saveTelemetryToolStripMenuItem.Click += new System.EventHandler(this.saveTelemetryToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(256, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(259, 26);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // connectionToolStripMenuItem
            // 
            this.connectionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.connectToolStripMenuItem,
            this.disconnectToolStripMenuItem});
            this.connectionToolStripMenuItem.Name = "connectionToolStripMenuItem";
            this.connectionToolStripMenuItem.Size = new System.Drawing.Size(98, 26);
            this.connectionToolStripMenuItem.Text = "Connection";
            // 
            // connectToolStripMenuItem
            // 
            this.connectToolStripMenuItem.Name = "connectToolStripMenuItem";
            this.connectToolStripMenuItem.Size = new System.Drawing.Size(165, 26);
            this.connectToolStripMenuItem.Text = "Connect...";
            this.connectToolStripMenuItem.Click += new System.EventHandler(this.connectToolStripMenuItem_Click);
            // 
            // disconnectToolStripMenuItem
            // 
            this.disconnectToolStripMenuItem.Name = "disconnectToolStripMenuItem";
            this.disconnectToolStripMenuItem.Size = new System.Drawing.Size(165, 26);
            this.disconnectToolStripMenuItem.Text = "Disconnect";
            this.disconnectToolStripMenuItem.Click += new System.EventHandler(this.disconnectToolStripMenuItem_Click);
            // 
            // cameraToolStripMenuItem
            // 
            this.cameraToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lookAtPointToolStripMenuItem,
            this.resetCameraToolStripMenuItem,
            this.toolStripSeparator2,
            this.trackSelectedToolStripMenuItem,
            this.centreSelectedToolStripMenuItem,
            this.zoomSelectedToolStripMenuItem});
            this.cameraToolStripMenuItem.Name = "cameraToolStripMenuItem";
            this.cameraToolStripMenuItem.Size = new System.Drawing.Size(74, 26);
            this.cameraToolStripMenuItem.Text = "Camera";
            // 
            // lookAtPointToolStripMenuItem
            // 
            this.lookAtPointToolStripMenuItem.Name = "lookAtPointToolStripMenuItem";
            this.lookAtPointToolStripMenuItem.Size = new System.Drawing.Size(196, 26);
            this.lookAtPointToolStripMenuItem.Text = "Look at Point";
            // 
            // resetCameraToolStripMenuItem
            // 
            this.resetCameraToolStripMenuItem.Name = "resetCameraToolStripMenuItem";
            this.resetCameraToolStripMenuItem.Size = new System.Drawing.Size(196, 26);
            this.resetCameraToolStripMenuItem.Text = "Reset Camera";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(193, 6);
            // 
            // trackSelectedToolStripMenuItem
            // 
            this.trackSelectedToolStripMenuItem.Name = "trackSelectedToolStripMenuItem";
            this.trackSelectedToolStripMenuItem.Size = new System.Drawing.Size(196, 26);
            this.trackSelectedToolStripMenuItem.Text = "Track Selected";
            // 
            // centreSelectedToolStripMenuItem
            // 
            this.centreSelectedToolStripMenuItem.Name = "centreSelectedToolStripMenuItem";
            this.centreSelectedToolStripMenuItem.Size = new System.Drawing.Size(196, 26);
            this.centreSelectedToolStripMenuItem.Text = "Centre Selected";
            // 
            // zoomSelectedToolStripMenuItem
            // 
            this.zoomSelectedToolStripMenuItem.Name = "zoomSelectedToolStripMenuItem";
            this.zoomSelectedToolStripMenuItem.Size = new System.Drawing.Size(196, 26);
            this.zoomSelectedToolStripMenuItem.Text = "Zoom Selected";
            // 
            // commandsToolStripMenuItem
            // 
            this.commandsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.goToFrameToolStripMenuItem});
            this.commandsToolStripMenuItem.Name = "commandsToolStripMenuItem";
            this.commandsToolStripMenuItem.Size = new System.Drawing.Size(98, 26);
            this.commandsToolStripMenuItem.Text = "Commands";
            // 
            // goToFrameToolStripMenuItem
            // 
            this.goToFrameToolStripMenuItem.Name = "goToFrameToolStripMenuItem";
            this.goToFrameToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.G)));
            this.goToFrameToolStripMenuItem.Size = new System.Drawing.Size(226, 26);
            this.goToFrameToolStripMenuItem.Text = "Go to Frame";
            this.goToFrameToolStripMenuItem.Click += new System.EventHandler(this.goToFrameToolStripMenuItem_Click);
            // 
            // updateTimer
            // 
            this.updateTimer.Interval = 16;
            this.updateTimer.Tick += new System.EventHandler(this.updateTimer_Tick);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.playForwardsButton);
            this.panel1.Controls.Add(this.playBackwardsButton);
            this.panel1.Controls.Add(this.frameCounterTextBox);
            this.panel1.Controls.Add(this.goToLastFrameButton);
            this.panel1.Controls.Add(this.nextFrameButton);
            this.panel1.Controls.Add(this.frameTrackBar);
            this.panel1.Controls.Add(this.previousFrameButton);
            this.panel1.Controls.Add(this.goToFirstFrameButton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 30);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1344, 64);
            this.panel1.TabIndex = 2;
            // 
            // playForwardsButton
            // 
            this.playForwardsButton.Location = new System.Drawing.Point(147, 0);
            this.playForwardsButton.Name = "playForwardsButton";
            this.playForwardsButton.Size = new System.Drawing.Size(42, 52);
            this.playForwardsButton.TabIndex = 8;
            this.playForwardsButton.Text = ">";
            this.playForwardsButton.UseVisualStyleBackColor = true;
            this.playForwardsButton.Click += new System.EventHandler(this.playForwardsButton_Click);
            // 
            // playBackwardsButton
            // 
            this.playBackwardsButton.Location = new System.Drawing.Point(99, 0);
            this.playBackwardsButton.Name = "playBackwardsButton";
            this.playBackwardsButton.Size = new System.Drawing.Size(42, 52);
            this.playBackwardsButton.TabIndex = 7;
            this.playBackwardsButton.Text = "<";
            this.playBackwardsButton.UseVisualStyleBackColor = true;
            this.playBackwardsButton.Click += new System.EventHandler(this.playBackwardsButton_Click);
            // 
            // frameCounterTextBox
            // 
            this.frameCounterTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.frameCounterTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.frameCounterTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 22.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.frameCounterTextBox.Location = new System.Drawing.Point(1206, 3);
            this.frameCounterTextBox.Name = "frameCounterTextBox";
            this.frameCounterTextBox.ReadOnly = true;
            this.frameCounterTextBox.Size = new System.Drawing.Size(135, 42);
            this.frameCounterTextBox.TabIndex = 3;
            this.frameCounterTextBox.Text = "000000";
            this.frameCounterTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // goToLastFrameButton
            // 
            this.goToLastFrameButton.Location = new System.Drawing.Point(243, 0);
            this.goToLastFrameButton.Name = "goToLastFrameButton";
            this.goToLastFrameButton.Size = new System.Drawing.Size(42, 52);
            this.goToLastFrameButton.TabIndex = 6;
            this.goToLastFrameButton.Text = ">>l";
            this.goToLastFrameButton.UseVisualStyleBackColor = true;
            this.goToLastFrameButton.Click += new System.EventHandler(this.goToLastFrameButton_Click);
            // 
            // nextFrameButton
            // 
            this.nextFrameButton.Location = new System.Drawing.Point(195, 0);
            this.nextFrameButton.Name = "nextFrameButton";
            this.nextFrameButton.Size = new System.Drawing.Size(42, 52);
            this.nextFrameButton.TabIndex = 5;
            this.nextFrameButton.Text = ">l";
            this.nextFrameButton.UseVisualStyleBackColor = true;
            this.nextFrameButton.Click += new System.EventHandler(this.nextFrameButton_Click);
            // 
            // frameTrackBar
            // 
            this.frameTrackBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.frameTrackBar.Location = new System.Drawing.Point(291, 0);
            this.frameTrackBar.Maximum = 0;
            this.frameTrackBar.Name = "frameTrackBar";
            this.frameTrackBar.Size = new System.Drawing.Size(909, 56);
            this.frameTrackBar.TabIndex = 2;
            this.frameTrackBar.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.frameTrackBar.Scroll += new System.EventHandler(this.frameTrackBar_Scroll);
            // 
            // previousFrameButton
            // 
            this.previousFrameButton.Location = new System.Drawing.Point(51, 0);
            this.previousFrameButton.Name = "previousFrameButton";
            this.previousFrameButton.Size = new System.Drawing.Size(42, 52);
            this.previousFrameButton.TabIndex = 1;
            this.previousFrameButton.Text = "l<";
            this.previousFrameButton.UseVisualStyleBackColor = true;
            this.previousFrameButton.Click += new System.EventHandler(this.previousFrameButton_Click);
            // 
            // goToFirstFrameButton
            // 
            this.goToFirstFrameButton.Location = new System.Drawing.Point(3, 0);
            this.goToFirstFrameButton.Name = "goToFirstFrameButton";
            this.goToFirstFrameButton.Size = new System.Drawing.Size(42, 52);
            this.goToFirstFrameButton.TabIndex = 0;
            this.goToFirstFrameButton.Text = "l<<";
            this.goToFirstFrameButton.UseVisualStyleBackColor = true;
            this.goToFirstFrameButton.Click += new System.EventHandler(this.goToFirstFrameButton_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Location = new System.Drawing.Point(0, 875);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1344, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // mainViewport
            // 
            this.mainViewport.BackColor = System.Drawing.Color.SpringGreen;
            this.mainViewport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainViewport.Location = new System.Drawing.Point(4, 4);
            this.mainViewport.Margin = new System.Windows.Forms.Padding(5);
            this.mainViewport.Name = "mainViewport";
            this.mainViewport.Size = new System.Drawing.Size(777, 493);
            this.mainViewport.TabIndex = 0;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1344, 897);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.mainSplitContainer);
            this.Controls.Add(this.mainMenuStrip);
            this.MainMenuStrip = this.mainMenuStrip;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Main";
            this.Text = "Form1";
            this.mainSplitContainer.Panel1.ResumeLayout(false);
            this.mainSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mainSplitContainer)).EndInit();
            this.mainSplitContainer.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer4.Panel1.ResumeLayout(false);
            this.splitContainer4.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).EndInit();
            this.splitContainer4.ResumeLayout(false);
            this.mainTabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.settingsTabControl.ResumeLayout(false);
            this.mainMenuStrip.ResumeLayout(false);
            this.mainMenuStrip.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.frameTrackBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer mainSplitContainer;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.RichTextBox propertiesRichTextBox;
        private System.Windows.Forms.RichTextBox propertiesRichTextBox2;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.SplitContainer splitContainer4;
        private System.Windows.Forms.TabControl mainTabControl;
        private System.Windows.Forms.TabPage tabPage1;
        private Renderer.DirectXControl mainViewport;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.RichTextBox frameDetailsRichTextBox;
        private System.Windows.Forms.TabControl settingsTabControl;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.MenuStrip mainMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.Timer updateTimer;
        private System.Windows.Forms.ToolStripMenuItem connectToolStripMenuItem;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TrackBar frameTrackBar;
        private System.Windows.Forms.Button previousFrameButton;
        private System.Windows.Forms.Button goToFirstFrameButton;
        private System.Windows.Forms.Button goToLastFrameButton;
        private System.Windows.Forms.Button nextFrameButton;
        private System.Windows.Forms.TextBox frameCounterTextBox;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Button playForwardsButton;
        private System.Windows.Forms.Button playBackwardsButton;
        private System.Windows.Forms.ToolStripMenuItem connectionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem disconnectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openTelemetryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveTelemetryToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem cameraToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lookAtPointToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resetCameraToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem trackSelectedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem centreSelectedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem zoomSelectedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem commandsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem goToFrameToolStripMenuItem;
    }
}

