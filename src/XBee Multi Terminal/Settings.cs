using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace XBee_Multi_Terminal
{


    public partial class FormSettings : Form
    {
        XBee_Multi_Terminal.Properties.Settings programSettings;

        public FormSettings()
        {
            InitializeComponent();
        }

        private void FormSettings_Load(object sender, EventArgs e)
        {
            programSettings = new XBee_Multi_Terminal.Properties.Settings();
            propertyGridSettings.SelectedObject = programSettings;

            
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            programSettings.Save();
            if (!((FormMultiTerm)this.MdiParent).serialConnected)
            {
                ((FormMultiTerm)this.MdiParent).RefreshSerialPortLabel();
                this.MdiParent.Refresh();

            }

            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormSettings_FormClosed(object sender, FormClosedEventArgs e)
        {
           //This is to prevent windows from maximising after closing settings.
           //Unfortunately there is no way to prevent them being modified,
           //but this is the next best thing. It's not ideal, but it's less annoying.
            foreach (Form form in this.MdiParent.MdiChildren)
            {
                if (form.Text == "Debug")
                {
                    form.WindowState = FormWindowState.Minimized;
                }
                  else if (form.Text != "Settings")
                {
                    form.WindowState = FormWindowState.Normal;
                } 


            }
            //this.MdiParent.ResumeLayout();
            this.MdiParent.LayoutMdi(System.Windows.Forms.MdiLayout.Cascade);
        }


    }
}
