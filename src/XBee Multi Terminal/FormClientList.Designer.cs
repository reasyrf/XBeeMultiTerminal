namespace XBee_Multi_Terminal
{
    partial class FormClientList
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
            this.listBoxClients = new System.Windows.Forms.ListBox();
            this.contextMenuStripCreate = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addClientTerminalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createNamedPipeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.enablePersistenceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyClientsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStripCreate.SuspendLayout();
            this.SuspendLayout();
            // 
            // listBoxClients
            // 
            this.listBoxClients.ContextMenuStrip = this.contextMenuStripCreate;
            this.listBoxClients.FormattingEnabled = true;
            this.listBoxClients.Location = new System.Drawing.Point(0, 0);
            this.listBoxClients.Name = "listBoxClients";
            this.listBoxClients.Size = new System.Drawing.Size(166, 212);
            this.listBoxClients.TabIndex = 0;
            this.listBoxClients.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listBoxClients_MouseDoubleClick);
            // 
            // contextMenuStripCreate
            // 
            this.contextMenuStripCreate.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addClientTerminalToolStripMenuItem,
            this.createNamedPipeToolStripMenuItem,
            this.refreshToolStripMenuItem,
            this.enablePersistenceToolStripMenuItem,
            this.copyClientsToolStripMenuItem});
            this.contextMenuStripCreate.Name = "contextMenuStripCreate";
            this.contextMenuStripCreate.Size = new System.Drawing.Size(180, 136);
            // 
            // addClientTerminalToolStripMenuItem
            // 
            this.addClientTerminalToolStripMenuItem.Name = "addClientTerminalToolStripMenuItem";
            this.addClientTerminalToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.addClientTerminalToolStripMenuItem.Text = "Add Client &Terminal";
            this.addClientTerminalToolStripMenuItem.Click += new System.EventHandler(this.addClientTerminalToolStripMenuItem_Click);
            // 
            // createNamedPipeToolStripMenuItem
            // 
            this.createNamedPipeToolStripMenuItem.Name = "createNamedPipeToolStripMenuItem";
            this.createNamedPipeToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.createNamedPipeToolStripMenuItem.Text = "Create &Named Pipe";
            this.createNamedPipeToolStripMenuItem.Click += new System.EventHandler(this.createNamedPipeToolStripMenuItem_Click);
            // 
            // refreshToolStripMenuItem
            // 
            this.refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
            this.refreshToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.refreshToolStripMenuItem.Text = "&Refresh";
            this.refreshToolStripMenuItem.Click += new System.EventHandler(this.refreshToolStripMenuItem_Click);
            // 
            // enablePersistenceToolStripMenuItem
            // 
            this.enablePersistenceToolStripMenuItem.Name = "enablePersistenceToolStripMenuItem";
            this.enablePersistenceToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.enablePersistenceToolStripMenuItem.Text = "Enable &Persistence";
            this.enablePersistenceToolStripMenuItem.Click += new System.EventHandler(this.enablePersistenceToolStripMenuItem_Click);
            // 
            // copyClientsToolStripMenuItem
            // 
            this.copyClientsToolStripMenuItem.Name = "copyClientsToolStripMenuItem";
            this.copyClientsToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.copyClientsToolStripMenuItem.Text = "&Copy Clients";
            this.copyClientsToolStripMenuItem.Click += new System.EventHandler(this.copyClientsToolStripMenuItem_Click);
            // 
            // FormClientList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(166, 213);
            this.Controls.Add(this.listBoxClients);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "FormClientList";
            this.ShowIcon = false;
            this.Text = "Client List";
            this.Load += new System.EventHandler(this.FormClientList_Load);
            this.contextMenuStripCreate.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox listBoxClients;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripCreate;
        private System.Windows.Forms.ToolStripMenuItem createNamedPipeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addClientTerminalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem refreshToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem enablePersistenceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyClientsToolStripMenuItem;
    }
}