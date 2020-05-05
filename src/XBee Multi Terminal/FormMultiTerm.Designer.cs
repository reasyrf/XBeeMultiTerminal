namespace XBee_Multi_Terminal
{
    partial class FormMultiTerm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMultiTerm));
            this.menuStripMain = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openSerialPortToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openPreviousSessionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveCurrentSessionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.addClientToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createVirtualComToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.detectClientsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.closeAllClientTerminalsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.commandsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setBroadcastAddressToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setCoordinatorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showRSSIToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.windowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cascadeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tileHorizontallyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tileVerticallyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabelConnectionStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelDivider = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelSerialPortSettings = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelDivider2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelFirmware = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelDivider3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelRSSI = new System.Windows.Forms.ToolStripStatusLabel();
            this.serialPort = new System.IO.Ports.SerialPort(this.components);
            this.saveFileDialogSession = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialogSession = new System.Windows.Forms.OpenFileDialog();
            this.timerRSSI = new System.Windows.Forms.Timer(this.components);
            this.toolTipHover = new System.Windows.Forms.ToolTip(this.components);
            this.timerHeatbeat = new System.Windows.Forms.Timer(this.components);
            this.menuStripMain.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStripMain
            // 
            this.menuStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.commandsToolStripMenuItem,
            this.windowToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStripMain.Location = new System.Drawing.Point(0, 0);
            this.menuStripMain.Name = "menuStripMain";
            this.menuStripMain.Size = new System.Drawing.Size(1092, 24);
            this.menuStripMain.TabIndex = 0;
            this.menuStripMain.Text = "menuStrip";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openSerialPortToolStripMenuItem,
            this.openPreviousSessionToolStripMenuItem,
            this.saveCurrentSessionToolStripMenuItem,
            this.openSettingsToolStripMenuItem,
            this.toolStripSeparator1,
            this.addClientToolStripMenuItem,
            this.createVirtualComToolStripMenuItem,
            this.detectClientsToolStripMenuItem,
            this.toolStripSeparator2,
            this.closeAllClientTerminalsToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // openSerialPortToolStripMenuItem
            // 
            this.openSerialPortToolStripMenuItem.Name = "openSerialPortToolStripMenuItem";
            this.openSerialPortToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.openSerialPortToolStripMenuItem.Text = "&Open Serial Port";
            this.openSerialPortToolStripMenuItem.Click += new System.EventHandler(this.openSerialPortToolStripMenuItem_Click);
            // 
            // openPreviousSessionToolStripMenuItem
            // 
            this.openPreviousSessionToolStripMenuItem.Name = "openPreviousSessionToolStripMenuItem";
            this.openPreviousSessionToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.openPreviousSessionToolStripMenuItem.Text = "Open &Previous Session";
            this.openPreviousSessionToolStripMenuItem.Click += new System.EventHandler(this.openPreviousSessionToolStripMenuItem_Click);
            // 
            // saveCurrentSessionToolStripMenuItem
            // 
            this.saveCurrentSessionToolStripMenuItem.Name = "saveCurrentSessionToolStripMenuItem";
            this.saveCurrentSessionToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.saveCurrentSessionToolStripMenuItem.Text = "&Save Current Session";
            this.saveCurrentSessionToolStripMenuItem.Click += new System.EventHandler(this.saveCurrentSessionToolStripMenuItem_Click);
            // 
            // openSettingsToolStripMenuItem
            // 
            this.openSettingsToolStripMenuItem.Name = "openSettingsToolStripMenuItem";
            this.openSettingsToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.openSettingsToolStripMenuItem.Text = "S&ettings";
            this.openSettingsToolStripMenuItem.Click += new System.EventHandler(this.openSettingsToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(205, 6);
            // 
            // addClientToolStripMenuItem
            // 
            this.addClientToolStripMenuItem.Name = "addClientToolStripMenuItem";
            this.addClientToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.addClientToolStripMenuItem.Text = "&Add Client Terminal";
            this.addClientToolStripMenuItem.Click += new System.EventHandler(this.addClientToolStripMenuItem_Click);
            // 
            // createVirtualComToolStripMenuItem
            // 
            this.createVirtualComToolStripMenuItem.Name = "createVirtualComToolStripMenuItem";
            this.createVirtualComToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.createVirtualComToolStripMenuItem.Text = "Create &Named Pipe";
            this.createVirtualComToolStripMenuItem.Click += new System.EventHandler(this.createVirtualComToolStripMenuItem_Click);
            // 
            // detectClientsToolStripMenuItem
            // 
            this.detectClientsToolStripMenuItem.Name = "detectClientsToolStripMenuItem";
            this.detectClientsToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.detectClientsToolStripMenuItem.Text = "&Detect Clients";
            this.detectClientsToolStripMenuItem.Click += new System.EventHandler(this.detectClientsToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(205, 6);
            // 
            // closeAllClientTerminalsToolStripMenuItem
            // 
            this.closeAllClientTerminalsToolStripMenuItem.Name = "closeAllClientTerminalsToolStripMenuItem";
            this.closeAllClientTerminalsToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.closeAllClientTerminalsToolStripMenuItem.Text = "&Close All Client Terminals";
            this.closeAllClientTerminalsToolStripMenuItem.Click += new System.EventHandler(this.closeAllClientTerminalsToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // commandsToolStripMenuItem
            // 
            this.commandsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.setBroadcastAddressToolStripMenuItem,
            this.setCoordinatorToolStripMenuItem,
            this.showRSSIToolStripMenuItem});
            this.commandsToolStripMenuItem.Name = "commandsToolStripMenuItem";
            this.commandsToolStripMenuItem.Size = new System.Drawing.Size(110, 20);
            this.commandsToolStripMenuItem.Text = "X&Bee Commands";
            // 
            // setBroadcastAddressToolStripMenuItem
            // 
            this.setBroadcastAddressToolStripMenuItem.Name = "setBroadcastAddressToolStripMenuItem";
            this.setBroadcastAddressToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.setBroadcastAddressToolStripMenuItem.Text = "Set &Broadcast";
            this.setBroadcastAddressToolStripMenuItem.Click += new System.EventHandler(this.setBroadcastAddressToolStripMenuItem_Click);
            // 
            // setCoordinatorToolStripMenuItem
            // 
            this.setCoordinatorToolStripMenuItem.Name = "setCoordinatorToolStripMenuItem";
            this.setCoordinatorToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.setCoordinatorToolStripMenuItem.Text = "Set Coordinato&r";
            this.setCoordinatorToolStripMenuItem.Click += new System.EventHandler(this.setCoordinatorToolStripMenuItem_Click);
            // 
            // showRSSIToolStripMenuItem
            // 
            this.showRSSIToolStripMenuItem.Name = "showRSSIToolStripMenuItem";
            this.showRSSIToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.showRSSIToolStripMenuItem.Text = "Show &RSSI";
            this.showRSSIToolStripMenuItem.Click += new System.EventHandler(this.showRSSIToolStripMenuItem_Click);
            // 
            // windowToolStripMenuItem
            // 
            this.windowToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cascadeToolStripMenuItem,
            this.tileHorizontallyToolStripMenuItem,
            this.tileVerticallyToolStripMenuItem});
            this.windowToolStripMenuItem.Name = "windowToolStripMenuItem";
            this.windowToolStripMenuItem.Size = new System.Drawing.Size(63, 20);
            this.windowToolStripMenuItem.Text = "&Window";
            // 
            // cascadeToolStripMenuItem
            // 
            this.cascadeToolStripMenuItem.Name = "cascadeToolStripMenuItem";
            this.cascadeToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.cascadeToolStripMenuItem.Text = "C&ascade";
            this.cascadeToolStripMenuItem.Click += new System.EventHandler(this.cascadeToolStripMenuItem_Click);
            // 
            // tileHorizontallyToolStripMenuItem
            // 
            this.tileHorizontallyToolStripMenuItem.Name = "tileHorizontallyToolStripMenuItem";
            this.tileHorizontallyToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.tileHorizontallyToolStripMenuItem.Text = "Tile &Horizontally";
            this.tileHorizontallyToolStripMenuItem.Click += new System.EventHandler(this.tileHorizontallyToolStripMenuItem_Click);
            // 
            // tileVerticallyToolStripMenuItem
            // 
            this.tileVerticallyToolStripMenuItem.Name = "tileVerticallyToolStripMenuItem";
            this.tileVerticallyToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.tileVerticallyToolStripMenuItem.Text = "Tile &Vertically";
            this.tileVerticallyToolStripMenuItem.Click += new System.EventHandler(this.tileVerticallyToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.helpToolStripMenuItem1,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // helpToolStripMenuItem1
            // 
            this.helpToolStripMenuItem1.Name = "helpToolStripMenuItem1";
            this.helpToolStripMenuItem1.Size = new System.Drawing.Size(107, 22);
            this.helpToolStripMenuItem1.Text = "&Help";
            this.helpToolStripMenuItem1.Click += new System.EventHandler(this.helpToolStripMenuItem1_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.aboutToolStripMenuItem.Text = "&About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelConnectionStatus,
            this.toolStripStatusLabelDivider,
            this.toolStripStatusLabelSerialPortSettings,
            this.toolStripStatusLabelDivider2,
            this.toolStripStatusLabelFirmware,
            this.toolStripStatusLabelDivider3,
            this.toolStripStatusLabelRSSI});
            this.statusStrip.Location = new System.Drawing.Point(0, 637);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.ShowItemToolTips = true;
            this.statusStrip.Size = new System.Drawing.Size(1092, 22);
            this.statusStrip.TabIndex = 5;
            this.statusStrip.Text = "statusStrip";
            // 
            // toolStripStatusLabelConnectionStatus
            // 
            this.toolStripStatusLabelConnectionStatus.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripStatusLabelConnectionStatus.Name = "toolStripStatusLabelConnectionStatus";
            this.toolStripStatusLabelConnectionStatus.Size = new System.Drawing.Size(91, 17);
            this.toolStripStatusLabelConnectionStatus.Text = "Disconnected";
            this.toolStripStatusLabelConnectionStatus.ToolTipText = "Click to toggle connection.";
            this.toolStripStatusLabelConnectionStatus.Click += new System.EventHandler(this.toolStripStatusLabelConnectionStatus_Click);
            // 
            // toolStripStatusLabelDivider
            // 
            this.toolStripStatusLabelDivider.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripStatusLabelDivider.Name = "toolStripStatusLabelDivider";
            this.toolStripStatusLabelDivider.Size = new System.Drawing.Size(14, 17);
            this.toolStripStatusLabelDivider.Text = "|";
            // 
            // toolStripStatusLabelSerialPortSettings
            // 
            this.toolStripStatusLabelSerialPortSettings.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripStatusLabelSerialPortSettings.Name = "toolStripStatusLabelSerialPortSettings";
            this.toolStripStatusLabelSerialPortSettings.Size = new System.Drawing.Size(49, 17);
            this.toolStripStatusLabelSerialPortSettings.Text = "Serial";
            this.toolStripStatusLabelSerialPortSettings.ToolTipText = "Click to change settings.";
            this.toolStripStatusLabelSerialPortSettings.Click += new System.EventHandler(this.toolStripStatusLabelSerialPortSettings_Click);
            // 
            // toolStripStatusLabelDivider2
            // 
            this.toolStripStatusLabelDivider2.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripStatusLabelDivider2.Name = "toolStripStatusLabelDivider2";
            this.toolStripStatusLabelDivider2.Size = new System.Drawing.Size(14, 17);
            this.toolStripStatusLabelDivider2.Text = "|";
            // 
            // toolStripStatusLabelFirmware
            // 
            this.toolStripStatusLabelFirmware.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripStatusLabelFirmware.Name = "toolStripStatusLabelFirmware";
            this.toolStripStatusLabelFirmware.Size = new System.Drawing.Size(787, 17);
            this.toolStripStatusLabelFirmware.Spring = true;
            this.toolStripStatusLabelFirmware.Text = "    ";
            this.toolStripStatusLabelFirmware.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripStatusLabelDivider3
            // 
            this.toolStripStatusLabelDivider3.Name = "toolStripStatusLabelDivider3";
            this.toolStripStatusLabelDivider3.Size = new System.Drawing.Size(10, 17);
            this.toolStripStatusLabelDivider3.Text = "|";
            // 
            // toolStripStatusLabelRSSI
            // 
            this.toolStripStatusLabelRSSI.Image = global::XBee_Multi_Terminal.Properties.Resources.p0;
            this.toolStripStatusLabelRSSI.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripStatusLabelRSSI.Name = "toolStripStatusLabelRSSI";
            this.toolStripStatusLabelRSSI.Size = new System.Drawing.Size(112, 17);
            this.toolStripStatusLabelRSSI.Text = " ";
            // 
            // serialPort
            // 
            this.serialPort.PortName = global::XBee_Multi_Terminal.Properties.Settings.Default.PortName;
            // 
            // saveFileDialogSession
            // 
            this.saveFileDialogSession.Filter = "Multi Terminal Session|*.xmts";
            this.saveFileDialogSession.Title = "Save Session";
            // 
            // openFileDialogSession
            // 
            this.openFileDialogSession.Filter = "Multi Terminal Session|*.xmts";
            this.openFileDialogSession.Title = "Open Session";
            // 
            // timerRSSI
            // 
            this.timerRSSI.Interval = 1000;
            this.timerRSSI.Tick += new System.EventHandler(this.timerRSSI_Tick);
            // 
            // timerHeatbeat
            // 
            this.timerHeatbeat.Interval = 500;
            this.timerHeatbeat.Tick += new System.EventHandler(this.timerHeatbeat_Tick);
            // 
            // FormMultiTerm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1092, 659);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStripMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStripMain;
            this.Name = "FormMultiTerm";
            this.Text = "XBee Multi Terminal";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMultiTerm_FormClosing);
            this.Load += new System.EventHandler(this.FormMultiTerm_Load);
            this.menuStripMain.ResumeLayout(false);
            this.menuStripMain.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStripMain;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openSettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addClientToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.IO.Ports.SerialPort serialPort;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelConnectionStatus;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelSerialPortSettings;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelDivider;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelDivider2;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelFirmware;
        private System.Windows.Forms.ToolStripMenuItem commandsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openSerialPortToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createVirtualComToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setBroadcastAddressToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveCurrentSessionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setCoordinatorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openPreviousSessionToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog saveFileDialogSession;
        private System.Windows.Forms.OpenFileDialog openFileDialogSession;
        private System.Windows.Forms.ToolStripMenuItem windowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cascadeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tileHorizontallyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tileVerticallyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeAllClientTerminalsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem detectClientsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelDivider3;
        private System.Windows.Forms.Timer timerRSSI;
        private System.Windows.Forms.ToolStripMenuItem showRSSIToolStripMenuItem;
        private System.Windows.Forms.ToolTip toolTipHover;
        private System.Windows.Forms.Timer timerHeatbeat;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelRSSI;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    }
}

