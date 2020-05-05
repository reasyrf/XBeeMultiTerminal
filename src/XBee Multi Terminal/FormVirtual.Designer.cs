namespace XBee_Multi_Terminal
{
    partial class FormVirtual
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
            this.textBoxMACAddressH = new System.Windows.Forms.TextBox();
            this.labelMACAddress = new System.Windows.Forms.Label();
            this.textBoxMACAddressL = new System.Windows.Forms.TextBox();
            this.buttonSetMAC = new System.Windows.Forms.Button();
            this.labelPipeName = new System.Windows.Forms.Label();
            this.textBoxPipeName = new System.Windows.Forms.TextBox();
            this.labelPipe = new System.Windows.Forms.Label();
            this.SuspendLayout();
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
            this.textBoxMACAddressL.TabIndex = 2;
            this.textBoxMACAddressL.TextChanged += new System.EventHandler(this.textBoxMACAddressL_TextChanged);
            this.textBoxMACAddressL.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxMACAddressL_KeyPress);
            // 
            // buttonSetMAC
            // 
            this.buttonSetMAC.Location = new System.Drawing.Point(189, 42);
            this.buttonSetMAC.Name = "buttonSetMAC";
            this.buttonSetMAC.Size = new System.Drawing.Size(75, 23);
            this.buttonSetMAC.TabIndex = 4;
            this.buttonSetMAC.Text = "Create";
            this.buttonSetMAC.UseVisualStyleBackColor = true;
            this.buttonSetMAC.Click += new System.EventHandler(this.buttonSetMAC_Click);
            // 
            // labelPipeName
            // 
            this.labelPipeName.AutoSize = true;
            this.labelPipeName.Location = new System.Drawing.Point(12, 45);
            this.labelPipeName.Name = "labelPipeName";
            this.labelPipeName.Size = new System.Drawing.Size(62, 13);
            this.labelPipeName.TabIndex = 7;
            this.labelPipeName.Text = "Pipe Name:";
            // 
            // textBoxPipeName
            // 
            this.textBoxPipeName.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxPipeName.Location = new System.Drawing.Point(80, 42);
            this.textBoxPipeName.MaxLength = 12;
            this.textBoxPipeName.Name = "textBoxPipeName";
            this.textBoxPipeName.Size = new System.Drawing.Size(70, 22);
            this.textBoxPipeName.TabIndex = 3;
            this.textBoxPipeName.TextChanged += new System.EventHandler(this.textBoxPipeName_TextChanged);
            // 
            // labelPipe
            // 
            this.labelPipe.AutoSize = true;
            this.labelPipe.Location = new System.Drawing.Point(77, 67);
            this.labelPipe.Name = "labelPipe";
            this.labelPipe.Size = new System.Drawing.Size(19, 13);
            this.labelPipe.TabIndex = 8;
            this.labelPipe.Text = "    ";
            // 
            // FormVirtual
            // 
            this.AcceptButton = this.buttonSetMAC;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(276, 87);
            this.Controls.Add(this.labelPipe);
            this.Controls.Add(this.labelPipeName);
            this.Controls.Add(this.textBoxPipeName);
            this.Controls.Add(this.buttonSetMAC);
            this.Controls.Add(this.textBoxMACAddressL);
            this.Controls.Add(this.labelMACAddress);
            this.Controls.Add(this.textBoxMACAddressH);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FormVirtual";
            this.ShowIcon = false;
            this.Text = "Named Pipe";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormVirtual_FormClosing);
            this.Load += new System.EventHandler(this.FormVirtual_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox textBoxMACAddressH;
        private System.Windows.Forms.Label labelMACAddress;
        private System.Windows.Forms.TextBox textBoxMACAddressL;
        private System.Windows.Forms.Button buttonSetMAC;
        private System.Windows.Forms.Label labelPipeName;
        private System.Windows.Forms.TextBox textBoxPipeName;
        private System.Windows.Forms.Label labelPipe;
    }
}