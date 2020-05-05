using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using XBee;

namespace XBee_Multi_Terminal
{
    public partial class FormClientList : Form
    {
        Boolean persistence = false;
        public FormClientList()
        {
            InitializeComponent();
        }

        private void FormClientList_Load(object sender, EventArgs e)
        {

        }

        public void AddClientAddress(String text)
        {
            if (!listBoxClients.Items.Contains(text)) listBoxClients.Items.Add(text);
        }

        private void listBoxClients_MouseDoubleClick(object sender, MouseEventArgs e)
        {


            int index = this.listBoxClients.IndexFromPoint(e.Location);
            if (index != System.Windows.Forms.ListBox.NoMatches)
            {
                FormTerminal newMDIChild = new FormTerminal();
                // Set the Parent Form of the Child window.
                newMDIChild.MdiParent = this.ParentForm;


                // Display the new form.
                newMDIChild.Show();

                newMDIChild.SetMACAddress(listBoxClients.Items[index].ToString());



            }

        }

        private void addClientTerminalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int index = this.listBoxClients.SelectedIndex;
            if (index != System.Windows.Forms.ListBox.NoMatches)
            {
                FormTerminal newMDIChild = new FormTerminal();
                // Set the Parent Form of the Child window.
                newMDIChild.MdiParent = this.ParentForm;


                // Display the new form.
                newMDIChild.Show();

                newMDIChild.SetMACAddress(listBoxClients.Items[index].ToString());



            }
        }

        private void createNamedPipeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int index = this.listBoxClients.SelectedIndex;
            if (index != System.Windows.Forms.ListBox.NoMatches)
            {
                FormVirtual newMDIChild = new FormVirtual();
                // Set the Parent Form of the Child window.
                newMDIChild.MdiParent = this.ParentForm;


                // Display the new form.
                newMDIChild.Show();

                newMDIChild.setMACAddress(listBoxClients.Items[index].ToString());



            }
        }

        public void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (((FormMultiTerm)this.ParentForm).serialConnected == true)
            {
                try
                {
                    if (!persistence)
                    {
                        listBoxClients.Items.Clear();
                    }
                    var request = new XBee.Frames.ATCommand(XBee.Frames.AT.NodeDiscover) { FrameId = 1 };
                    ((FormMultiTerm)this.ParentForm).bee.Execute(request);
                }
                catch (Exception ex)
                { //Serial probably not connected, just ignore 
                    if (ex.Message == "The port is closed.")
                    {
                        ((FormMultiTerm)this.ParentForm).DisconnectSerialWithException(ex);
                    }
                }


            }
        }

        private void enablePersistenceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //TODO make a program setting too
            if (persistence)
            {
                enablePersistenceToolStripMenuItem.Text = "Enable &Persistence";
                persistence = false;
            }
            else
            {
                enablePersistenceToolStripMenuItem.Text = "Disable &Persistence";
                persistence = true;
            }
        }

        private void copyClientsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            String clients = "";
            try
            {

                foreach (String client in listBoxClients.Items)
                {
                    clients = clients + client + ",";
                }
                
                Clipboard.SetText(clients.Remove(clients.Length - 1));
            }
            catch { }

        }
    }
}
