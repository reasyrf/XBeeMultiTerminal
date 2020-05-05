namespace XBee_Multi_Terminal
{
    partial class FormTerminal
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
            this.richTextBoxTerminal = new System.Windows.Forms.RichTextBox();
            this.contextMenuStripRichTextBox = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.clearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.textBoxMACAddressH = new System.Windows.Forms.TextBox();
            this.labelMACAddress = new System.Windows.Forms.Label();
            this.textBoxMACAddressL = new System.Windows.Forms.TextBox();
            this.buttonSetMAC = new System.Windows.Forms.Button();
            this.checkBoxEchoChars = new System.Windows.Forms.CheckBox();
            this.timerScrollback = new System.Windows.Forms.Timer(this.components);
            this.contextMenuStripTitle = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.labelTerminalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copySelectedTextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStripRichTextBox.SuspendLayout();
            this.contextMenuStripTitle.SuspendLayout();
            this.SuspendLayout();
            // 
            // richTextBoxTerminal
            // 
            this.richTextBoxTerminal.AcceptsTab = true;
            this.richTextBoxTerminal.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBoxTerminal.BackColor = System.Drawing.Color.Black;
            this.richTextBoxTerminal.ContextMenuStrip = this.contextMenuStripRichTextBox;
            this.richTextBoxTerminal.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBoxTerminal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.richTextBoxTerminal.HideSelection = false;
            this.richTextBoxTerminal.Location = new System.Drawing.Point(0, 34);
            this.richTextBoxTerminal.Name = "richTextBoxTerminal";
            this.richTextBoxTerminal.Size = new System.Drawing.Size(480, 415);
            this.richTextBoxTerminal.TabIndex = 0;
            this.richTextBoxTerminal.Text = "";
            this.richTextBoxTerminal.KeyDown += new System.Windows.Forms.KeyEventHandler(this.richTextBoxTerminal_KeyDown);
            this.richTextBoxTerminal.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.richTextBoxTerminal_KeyPress);
            // 
            // contextMenuStripRichTextBox
            // 
            this.contextMenuStripRichTextBox.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clearToolStripMenuItem,
            this.copyToolStripMenuItem,
            this.copySelectedTextToolStripMenuItem,
            this.pasteToolStripMenuItem});
            this.contextMenuStripRichTextBox.Name = "contextMenuStripRichTextBox";
            this.contextMenuStripRichTextBox.Size = new System.Drawing.Size(174, 114);
            // 
            // clearToolStripMenuItem
            // 
            this.clearToolStripMenuItem.Name = "clearToolStripMenuItem";
            this.clearToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.clearToolStripMenuItem.Text = "C&lear";
            this.clearToolStripMenuItem.Click += new System.EventHandler(this.clearToolStripMenuItem_Click);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.copyToolStripMenuItem.Text = "&Copy All Text";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
            // 
            // pasteToolStripMenuItem
            // 
            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            this.pasteToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.pasteToolStripMenuItem.Text = "&Paste To Terminal";
            this.pasteToolStripMenuItem.Click += new System.EventHandler(this.pasteToolStripMenuItem_Click);
            // 
            // textBoxMACAddressH
            // 
            this.textBoxMACAddressH.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxMACAddressH.Location = new System.Drawing.Point(80, 8);
            this.textBoxMACAddressH.MaxLength = 16;
            this.textBoxMACAddressH.Name = "textBoxMACAddressH";
            this.textBoxMACAddressH.Size = new System.Drawing.Size(70, 22);
            this.textBoxMACAddressH.TabIndex = 1;
            this.textBoxMACAddressH.TextChanged += new System.EventHandler(this.textBoxMACAddressH_TextChanged);
            this.textBoxMACAddressH.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxMACAddressH_KeyPress);
            // 
            // labelMACAddress
            // 
            this.labelMACAddress.AutoSize = true;
            this.labelMACAddress.Location = new System.Drawing.Point(12, 11);
            this.labelMACAddress.Name = "labelMACAddress";
            this.labelMACAddress.Size = new System.Drawing.Size(62, 13);
            this.labelMACAddress.TabIndex = 2;
            this.labelMACAddress.Text = "Client MAC:";
            // 
            // textBoxMACAddressL
            // 
            this.textBoxMACAddressL.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxMACAddressL.Location = new System.Drawing.Point(156, 8);
            this.textBoxMACAddressL.MaxLength = 8;
            this.textBoxMACAddressL.Name = "textBoxMACAddressL";
            this.textBoxMACAddressL.Size = new System.Drawing.Size(70, 22);
            this.textBoxMACAddressL.TabIndex = 4;
            this.textBoxMACAddressL.TextChanged += new System.EventHandler(this.textBoxMACAddressL_TextChanged);
            this.textBoxMACAddressL.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxMACAddressL_KeyPress);
            // 
            // buttonSetMAC
            // 
            this.buttonSetMAC.Location = new System.Drawing.Point(232, 7);
            this.buttonSetMAC.Name = "buttonSetMAC";
            this.buttonSetMAC.Size = new System.Drawing.Size(75, 23);
            this.buttonSetMAC.TabIndex = 5;
            this.buttonSetMAC.Text = "Set";
            this.buttonSetMAC.UseVisualStyleBackColor = true;
            this.buttonSetMAC.Click += new System.EventHandler(this.buttonSetMAC_Click);
            // 
            // checkBoxEchoChars
            // 
            this.checkBoxEchoChars.AutoSize = true;
            this.checkBoxEchoChars.Location = new System.Drawing.Point(358, 10);
            this.checkBoxEchoChars.Name = "checkBoxEchoChars";
            this.checkBoxEchoChars.Size = new System.Drawing.Size(110, 17);
            this.checkBoxEchoChars.TabIndex = 6;
            this.checkBoxEchoChars.Text = "Echo Local Chars";
            this.checkBoxEchoChars.UseVisualStyleBackColor = true;
            this.checkBoxEchoChars.CheckedChanged += new System.EventHandler(this.checkBoxEchoChars_CheckedChanged);
            // 
            // timerScrollback
            // 
            this.timerScrollback.Tick += new System.EventHandler(this.timerScrollback_Tick);
            // 
            // contextMenuStripTitle
            // 
            this.contextMenuStripTitle.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.labelTerminalToolStripMenuItem});
            this.contextMenuStripTitle.Name = "contextMenuStripTitle";
            this.contextMenuStripTitle.Size = new System.Drawing.Size(152, 26);
            // 
            // labelTerminalToolStripMenuItem
            // 
            this.labelTerminalToolStripMenuItem.Name = "labelTerminalToolStripMenuItem";
            this.labelTerminalToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.labelTerminalToolStripMenuItem.Text = "&Label Terminal";
            this.labelTerminalToolStripMenuItem.Click += new System.EventHandler(this.labelTerminalToolStripMenuItem_Click);
            // 
            // copySelectedTextToolStripMenuItem
            // 
            this.copySelectedTextToolStripMenuItem.Name = "copySelectedTextToolStripMenuItem";
            this.copySelectedTextToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.copySelectedTextToolStripMenuItem.Text = "Copy &Selected Text";
            this.copySelectedTextToolStripMenuItem.Click += new System.EventHandler(this.copySelectedTextToolStripMenuItem_Click);
            // 
            // FormTerminal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(480, 449);
            this.Controls.Add(this.checkBoxEchoChars);
            this.Controls.Add(this.buttonSetMAC);
            this.Controls.Add(this.textBoxMACAddressL);
            this.Controls.Add(this.labelMACAddress);
            this.Controls.Add(this.textBoxMACAddressH);
            this.Controls.Add(this.richTextBoxTerminal);
            this.Name = "FormTerminal";
            this.ShowIcon = false;
            this.Text = "Terminal";
            this.Load += new System.EventHandler(this.FormTerminal_Load);
            this.contextMenuStripRichTextBox.ResumeLayout(false);
            this.contextMenuStripTitle.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBoxTerminal;
        private System.Windows.Forms.TextBox textBoxMACAddressH;
        private System.Windows.Forms.Label labelMACAddress;
        private System.Windows.Forms.TextBox textBoxMACAddressL;
        private System.Windows.Forms.Button buttonSetMAC;
        private System.Windows.Forms.CheckBox checkBoxEchoChars;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripRichTextBox;
        private System.Windows.Forms.ToolStripMenuItem clearToolStripMenuItem;
        private System.Windows.Forms.Timer timerScrollback;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripTitle;
        private System.Windows.Forms.ToolStripMenuItem labelTerminalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copySelectedTextToolStripMenuItem;
    }
}