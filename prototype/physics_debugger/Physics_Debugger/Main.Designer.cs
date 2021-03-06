﻿namespace physics_debugger
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            this.mainSplitContainer = new System.Windows.Forms.SplitContainer();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.sceneGraphView = new physics_debugger.Controls.SceneGraphView.SceneGraphView();
            this.objectDetailsPropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.mainTabControl = new System.Windows.Forms.TabControl();
            this.renderTabPage = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.mainViewport = new Renderer.DirectXControl();
            this.cameraSettingsGroupBox = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.farPlaneTrackBar = new System.Windows.Forms.TrackBar();
            this.nearPlaneTrackBar = new System.Windows.Forms.TrackBar();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cameraSpeedTrackBar = new System.Windows.Forms.TrackBar();
            this.graphTabPage = new System.Windows.Forms.TabPage();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.mainGraph = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.graphChannelPropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.frameStatsPropertyGrid = new System.Windows.Forms.PropertyGrid();
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
            this.graphToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.plotPerformanceGraphToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportDataToCSVToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearAllPlottedDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            ((System.ComponentModel.ISupportInitialize)(this.mainSplitContainer)).BeginInit();
            this.mainSplitContainer.Panel1.SuspendLayout();
            this.mainSplitContainer.Panel2.SuspendLayout();
            this.mainSplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).BeginInit();
            this.splitContainer4.Panel1.SuspendLayout();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            this.mainTabControl.SuspendLayout();
            this.renderTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.cameraSettingsGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.farPlaneTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nearPlaneTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cameraSpeedTrackBar)).BeginInit();
            this.graphTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainGraph)).BeginInit();
            this.groupBox1.SuspendLayout();
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
            this.mainSplitContainer.Panel2.Controls.Add(this.splitContainer4);
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
            this.splitContainer3.Panel1.Controls.Add(this.sceneGraphView);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.objectDetailsPropertyGrid);
            this.splitContainer3.Size = new System.Drawing.Size(250, 778);
            this.splitContainer3.SplitterDistance = 406;
            this.splitContainer3.SplitterWidth = 10;
            this.splitContainer3.TabIndex = 0;
            // 
            // sceneGraphView
            // 
            this.sceneGraphView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sceneGraphView.FrameIdToDisplay = 0;
            this.sceneGraphView.Location = new System.Drawing.Point(0, 0);
            this.sceneGraphView.Name = "sceneGraphView";
            this.sceneGraphView.Size = new System.Drawing.Size(250, 406);
            this.sceneGraphView.TabIndex = 0;
            // 
            // objectDetailsPropertyGrid
            // 
            this.objectDetailsPropertyGrid.DisabledItemForeColor = System.Drawing.SystemColors.ControlText;
            this.objectDetailsPropertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.objectDetailsPropertyGrid.HelpVisible = false;
            this.objectDetailsPropertyGrid.Location = new System.Drawing.Point(0, 0);
            this.objectDetailsPropertyGrid.Name = "objectDetailsPropertyGrid";
            this.objectDetailsPropertyGrid.Size = new System.Drawing.Size(250, 362);
            this.objectDetailsPropertyGrid.TabIndex = 0;
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
            this.splitContainer4.Panel2.Controls.Add(this.frameStatsPropertyGrid);
            this.splitContainer4.Size = new System.Drawing.Size(1063, 778);
            this.splitContainer4.SplitterDistance = 532;
            this.splitContainer4.SplitterWidth = 10;
            this.splitContainer4.TabIndex = 0;
            // 
            // mainTabControl
            // 
            this.mainTabControl.Controls.Add(this.renderTabPage);
            this.mainTabControl.Controls.Add(this.graphTabPage);
            this.mainTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainTabControl.Location = new System.Drawing.Point(0, 0);
            this.mainTabControl.Margin = new System.Windows.Forms.Padding(4);
            this.mainTabControl.Name = "mainTabControl";
            this.mainTabControl.SelectedIndex = 0;
            this.mainTabControl.Size = new System.Drawing.Size(1063, 532);
            this.mainTabControl.TabIndex = 0;
            // 
            // renderTabPage
            // 
            this.renderTabPage.Controls.Add(this.splitContainer1);
            this.renderTabPage.Location = new System.Drawing.Point(4, 25);
            this.renderTabPage.Margin = new System.Windows.Forms.Padding(4);
            this.renderTabPage.Name = "renderTabPage";
            this.renderTabPage.Padding = new System.Windows.Forms.Padding(4);
            this.renderTabPage.Size = new System.Drawing.Size(1055, 503);
            this.renderTabPage.TabIndex = 0;
            this.renderTabPage.Text = "Render";
            this.renderTabPage.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(4, 4);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.mainViewport);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.cameraSettingsGroupBox);
            this.splitContainer1.Size = new System.Drawing.Size(1047, 495);
            this.splitContainer1.SplitterDistance = 748;
            this.splitContainer1.SplitterWidth = 10;
            this.splitContainer1.TabIndex = 0;
            // 
            // mainViewport
            // 
            this.mainViewport.BackColor = System.Drawing.Color.SpringGreen;
            this.mainViewport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainViewport.Location = new System.Drawing.Point(0, 0);
            this.mainViewport.Margin = new System.Windows.Forms.Padding(5);
            this.mainViewport.Name = "mainViewport";
            this.mainViewport.Size = new System.Drawing.Size(748, 495);
            this.mainViewport.TabIndex = 0;
            // 
            // cameraSettingsGroupBox
            // 
            this.cameraSettingsGroupBox.Controls.Add(this.label3);
            this.cameraSettingsGroupBox.Controls.Add(this.label2);
            this.cameraSettingsGroupBox.Controls.Add(this.farPlaneTrackBar);
            this.cameraSettingsGroupBox.Controls.Add(this.nearPlaneTrackBar);
            this.cameraSettingsGroupBox.Controls.Add(this.checkBox1);
            this.cameraSettingsGroupBox.Controls.Add(this.label1);
            this.cameraSettingsGroupBox.Controls.Add(this.cameraSpeedTrackBar);
            this.cameraSettingsGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cameraSettingsGroupBox.Location = new System.Drawing.Point(0, 0);
            this.cameraSettingsGroupBox.Name = "cameraSettingsGroupBox";
            this.cameraSettingsGroupBox.Size = new System.Drawing.Size(289, 495);
            this.cameraSettingsGroupBox.TabIndex = 2;
            this.cameraSettingsGroupBox.TabStop = false;
            this.cameraSettingsGroupBox.Text = "Camera Settings";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 185);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 17);
            this.label3.TabIndex = 7;
            this.label3.Text = "Far plane";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 106);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 17);
            this.label2.TabIndex = 6;
            this.label2.Text = "Near plane";
            // 
            // farPlaneTrackBar
            // 
            this.farPlaneTrackBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.farPlaneTrackBar.BackColor = System.Drawing.Color.White;
            this.farPlaneTrackBar.Location = new System.Drawing.Point(6, 205);
            this.farPlaneTrackBar.Name = "farPlaneTrackBar";
            this.farPlaneTrackBar.Size = new System.Drawing.Size(277, 56);
            this.farPlaneTrackBar.TabIndex = 5;
            this.farPlaneTrackBar.TickStyle = System.Windows.Forms.TickStyle.None;
            // 
            // nearPlaneTrackBar
            // 
            this.nearPlaneTrackBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nearPlaneTrackBar.BackColor = System.Drawing.Color.White;
            this.nearPlaneTrackBar.Location = new System.Drawing.Point(6, 126);
            this.nearPlaneTrackBar.Name = "nearPlaneTrackBar";
            this.nearPlaneTrackBar.Size = new System.Drawing.Size(277, 56);
            this.nearPlaneTrackBar.TabIndex = 4;
            this.nearPlaneTrackBar.TickStyle = System.Windows.Forms.TickStyle.None;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(9, 267);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(163, 21);
            this.checkBox1.TabIndex = 3;
            this.checkBox1.Text = "Simulation Controlled";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Camera Speed:";
            // 
            // cameraSpeedTrackBar
            // 
            this.cameraSpeedTrackBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cameraSpeedTrackBar.BackColor = System.Drawing.Color.White;
            this.cameraSpeedTrackBar.Location = new System.Drawing.Point(6, 47);
            this.cameraSpeedTrackBar.Maximum = 100;
            this.cameraSpeedTrackBar.Name = "cameraSpeedTrackBar";
            this.cameraSpeedTrackBar.Size = new System.Drawing.Size(277, 56);
            this.cameraSpeedTrackBar.TabIndex = 1;
            this.cameraSpeedTrackBar.TickStyle = System.Windows.Forms.TickStyle.None;
            // 
            // graphTabPage
            // 
            this.graphTabPage.Controls.Add(this.splitContainer2);
            this.graphTabPage.Location = new System.Drawing.Point(4, 25);
            this.graphTabPage.Margin = new System.Windows.Forms.Padding(4);
            this.graphTabPage.Name = "graphTabPage";
            this.graphTabPage.Padding = new System.Windows.Forms.Padding(4);
            this.graphTabPage.Size = new System.Drawing.Size(1055, 503);
            this.graphTabPage.TabIndex = 1;
            this.graphTabPage.Text = "Graph";
            this.graphTabPage.UseVisualStyleBackColor = true;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(4, 4);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.mainGraph);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.groupBox1);
            this.splitContainer2.Size = new System.Drawing.Size(1047, 495);
            this.splitContainer2.SplitterDistance = 681;
            this.splitContainer2.SplitterWidth = 10;
            this.splitContainer2.TabIndex = 1;
            // 
            // mainGraph
            // 
            chartArea1.AxisX.Title = "Frame Id";
            chartArea1.AxisY.Title = "Frame time (us)";
            chartArea1.CursorX.IsUserEnabled = true;
            chartArea1.CursorX.IsUserSelectionEnabled = true;
            chartArea1.Name = "ChartArea1";
            this.mainGraph.ChartAreas.Add(chartArea1);
            this.mainGraph.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "Legend1";
            this.mainGraph.Legends.Add(legend1);
            this.mainGraph.Location = new System.Drawing.Point(0, 0);
            this.mainGraph.Name = "mainGraph";
            this.mainGraph.Size = new System.Drawing.Size(681, 495);
            this.mainGraph.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.graphChannelPropertyGrid);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(356, 495);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Settings";
            // 
            // graphChannelPropertyGrid
            // 
            this.graphChannelPropertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.graphChannelPropertyGrid.HelpVisible = false;
            this.graphChannelPropertyGrid.Location = new System.Drawing.Point(3, 18);
            this.graphChannelPropertyGrid.Name = "graphChannelPropertyGrid";
            this.graphChannelPropertyGrid.PropertySort = System.Windows.Forms.PropertySort.Alphabetical;
            this.graphChannelPropertyGrid.Size = new System.Drawing.Size(350, 474);
            this.graphChannelPropertyGrid.TabIndex = 0;
            this.graphChannelPropertyGrid.ToolbarVisible = false;
            // 
            // frameStatsPropertyGrid
            // 
            this.frameStatsPropertyGrid.DisabledItemForeColor = System.Drawing.SystemColors.ControlText;
            this.frameStatsPropertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.frameStatsPropertyGrid.HelpVisible = false;
            this.frameStatsPropertyGrid.Location = new System.Drawing.Point(0, 0);
            this.frameStatsPropertyGrid.Name = "frameStatsPropertyGrid";
            this.frameStatsPropertyGrid.Size = new System.Drawing.Size(1063, 236);
            this.frameStatsPropertyGrid.TabIndex = 0;
            // 
            // mainMenuStrip
            // 
            this.mainMenuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.connectionToolStripMenuItem,
            this.cameraToolStripMenuItem,
            this.commandsToolStripMenuItem,
            this.graphToolStripMenuItem});
            this.mainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.mainMenuStrip.Name = "mainMenuStrip";
            this.mainMenuStrip.Size = new System.Drawing.Size(1344, 28);
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
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(46, 24);
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
            this.connectionToolStripMenuItem.Size = new System.Drawing.Size(98, 24);
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
            this.cameraToolStripMenuItem.Size = new System.Drawing.Size(74, 24);
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
            this.commandsToolStripMenuItem.Size = new System.Drawing.Size(98, 24);
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
            // graphToolStripMenuItem
            // 
            this.graphToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.plotPerformanceGraphToolStripMenuItem,
            this.exportDataToCSVToolStripMenuItem,
            this.clearAllPlottedDataToolStripMenuItem});
            this.graphToolStripMenuItem.Name = "graphToolStripMenuItem";
            this.graphToolStripMenuItem.Size = new System.Drawing.Size(63, 24);
            this.graphToolStripMenuItem.Text = "Graph";
            // 
            // plotPerformanceGraphToolStripMenuItem
            // 
            this.plotPerformanceGraphToolStripMenuItem.Name = "plotPerformanceGraphToolStripMenuItem";
            this.plotPerformanceGraphToolStripMenuItem.Size = new System.Drawing.Size(249, 26);
            this.plotPerformanceGraphToolStripMenuItem.Text = "Plot Performance Graph";
            this.plotPerformanceGraphToolStripMenuItem.Click += new System.EventHandler(this.plotPerformanceGraphToolStripMenuItem_Click);
            // 
            // exportDataToCSVToolStripMenuItem
            // 
            this.exportDataToCSVToolStripMenuItem.Name = "exportDataToCSVToolStripMenuItem";
            this.exportDataToCSVToolStripMenuItem.Size = new System.Drawing.Size(249, 26);
            this.exportDataToCSVToolStripMenuItem.Text = "Export data to CSV...";
            // 
            // clearAllPlottedDataToolStripMenuItem
            // 
            this.clearAllPlottedDataToolStripMenuItem.Name = "clearAllPlottedDataToolStripMenuItem";
            this.clearAllPlottedDataToolStripMenuItem.Size = new System.Drawing.Size(249, 26);
            this.clearAllPlottedDataToolStripMenuItem.Text = "Clear all plotted data";
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
            this.panel1.Location = new System.Drawing.Point(0, 28);
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
            this.Text = "Physics Debugger";
            this.mainSplitContainer.Panel1.ResumeLayout(false);
            this.mainSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mainSplitContainer)).EndInit();
            this.mainSplitContainer.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.splitContainer4.Panel1.ResumeLayout(false);
            this.splitContainer4.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).EndInit();
            this.splitContainer4.ResumeLayout(false);
            this.mainTabControl.ResumeLayout(false);
            this.renderTabPage.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.cameraSettingsGroupBox.ResumeLayout(false);
            this.cameraSettingsGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.farPlaneTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nearPlaneTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cameraSpeedTrackBar)).EndInit();
            this.graphTabPage.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mainGraph)).EndInit();
            this.groupBox1.ResumeLayout(false);
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
        private System.Windows.Forms.SplitContainer splitContainer4;
        private System.Windows.Forms.TabControl mainTabControl;
        private System.Windows.Forms.TabPage renderTabPage;
        private Renderer.DirectXControl mainViewport;
        private System.Windows.Forms.TabPage graphTabPage;
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
        private System.Windows.Forms.TrackBar cameraSpeedTrackBar;
        private System.Windows.Forms.GroupBox cameraSettingsGroupBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TrackBar farPlaneTrackBar;
        private System.Windows.Forms.TrackBar nearPlaneTrackBar;
        private physics_debugger.Controls.SceneGraphView.SceneGraphView sceneGraphView;
        private System.Windows.Forms.PropertyGrid objectDetailsPropertyGrid;
        private System.Windows.Forms.PropertyGrid frameStatsPropertyGrid;
        private System.Windows.Forms.DataVisualization.Charting.Chart mainGraph;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.PropertyGrid graphChannelPropertyGrid;
        private System.Windows.Forms.ToolStripMenuItem graphToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem plotPerformanceGraphToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportDataToCSVToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearAllPlottedDataToolStripMenuItem;
    }
}

