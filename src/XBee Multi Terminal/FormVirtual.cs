using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using ClientServerUsingNamedPipes;


namespace XBee_Multi_Terminal
{
    public partial class FormVirtual : Form
    {
        public FormVirtual()
        {
            InitializeComponent();
        }
        ClientServerUsingNamedPipes.Server.PipeServer pipeServer;
        private string pipeString = "";
        private string MACAddress = "";

        public String GetPipeName()
        {
            return pipeString;
        }
        public String GetMACAddress()
        {
            return MACAddress;
        }

        private void FormVirtual_Load(object sender, EventArgs e)
        {
            textBoxMACAddressH.CharacterCasing = CharacterCasing.Upper;
            textBoxMACAddressL.CharacterCasing = CharacterCasing.Upper;
        }

        public void AddTextToTerminal(String text)
        {
            pipeServer.SendMessage(text, pipeString);
        }

        private void textBoxMACAddressH_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Regex.IsMatch(e.KeyChar.ToString(), "^[0-9a-fA-F\b]+$")) && e.KeyChar != 22 && e.KeyChar != 3) e.Handled = true;

        }

        private void textBoxMACAddressL_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Regex.IsMatch(e.KeyChar.ToString(), "^[0-9a-fA-F\b]+$")) && e.KeyChar != 22 && e.KeyChar != 3) e.Handled = true;

        }

        private void textBoxMACAddressH_TextChanged(object sender, EventArgs e)
        {
            if (!Regex.IsMatch(textBoxMACAddressH.Text.ToString(), "^[0-9a-fA-F\b]+$"))
            {
                textBoxMACAddressH.Text = "";
            }
            if (textBoxMACAddressH.Text.Length > 8 )
            {
                textBoxMACAddressL.Text = textBoxMACAddressH.Text.Substring(8, textBoxMACAddressH.Text.Length - 8);
                textBoxMACAddressH.Text = textBoxMACAddressH.Text.Substring(0, 8);
            }
            if (textBoxMACAddressH.Text.Length == 8)
            {
                

                textBoxMACAddressL.Focus();
            }
            buttonSetMAC.Enabled = true;
        }

        private void textBoxMACAddressL_TextChanged(object sender, EventArgs e)
        {
            if (!Regex.IsMatch(textBoxMACAddressL.Text.ToString(), "^[0-9a-fA-F\b]+$"))
            {
                textBoxMACAddressL.Text = "";
            }
            if (textBoxMACAddressL.Text.Length == 8)
            {
                textBoxPipeName.Focus();
            }
            buttonSetMAC.Enabled = true;
        }

        public void setMACAddress(String text)
        {
            if (text.Length == 16)
            {

                textBoxMACAddressH.Text = text.Substring(0, 8);
                textBoxMACAddressL.Text = text.Substring(8, 8);
                buttonSetMAC_Click(null, null);


            }
        }

        public void setPipe(String text)
        {
            if (text.Length >= 4)
            {

                textBoxPipeName.Text = text;
                buttonSetMAC_Click(null, null);


            }
        }

        private void createPipe()
        {
            pipeString = textBoxPipeName.Text;
            pipeServer = new ClientServerUsingNamedPipes.Server.PipeServer(pipeString);
            pipeServer.Start();
            this.Text = textBoxPipeName.Text + " [" + textBoxMACAddressH.Text + " " + textBoxMACAddressL.Text + "]";
            MACAddress = textBoxMACAddressH.Text + textBoxMACAddressL.Text;
            buttonSetMAC.Enabled = false;
            labelPipe.Text = "\\\\.\\pipe\\" + textBoxPipeName.Text;


            pipeServer.MessageReceivedEvent += (sender, args) =>
            {

                FormMultiTerm parentForm = this.MdiParent as FormMultiTerm;
                if (parentForm != null)
                {
                    try
                    {
                        //Transmit chars here
                        XBee.XBeeAddress64 destAddressLong = new XBee.XBeeAddress64(Convert.ToUInt64(MACAddress, 16));
                        XBee.XBeeAddress16 destAddressShort = new XBee.XBeeAddress16(Convert.ToUInt16("FFFE", 16));
                        XBee.XBeeNode destNode = new XBee.XBeeNode { Address16 = destAddressShort, Address64 = destAddressLong };
                        XBee.Frames.TransmitDataRequest frame = new XBee.Frames.TransmitDataRequest(destNode);
                        frame.SetRFData(Encoding.GetEncoding("UTF-8").GetBytes(args.Message.ToCharArray()));
                        parentForm.bee.Execute(frame);
                    }
                    catch (Exception ex)
                    { //Serial probably not connected, just ignore 
                        if (ex.Message == "The port is closed.")
                        {
                            ((FormMultiTerm)this.ParentForm).DisconnectSerialWithException(ex);
                        }
                    }
                }
            };
        }

        private void buttonSetMAC_Click(object sender, EventArgs e)
        {
            try
            {
                pipeServer.Stop();
                pipeServer = null;
            }
            catch { }

            if (textBoxMACAddressH.Text.Length != 8)
            {
                textBoxMACAddressH.Focus();
            }
            else if (textBoxMACAddressL.Text.Length != 8)
            {
                textBoxMACAddressL.Focus();
            } else if (textBoxPipeName.Text.Length < 1)
            {
                textBoxPipeName.Focus();
            }
            else
            {           
                try
                {
                    createPipe();
                    ((FormMultiTerm)this.ParentForm).sessionSaved = false;
                }
                catch (System.IO.IOException)
                {
                    //MessageBox.Show("Named pipe already in use!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //textBoxPipeName.Focus();
                }
                //pipeServer.

            }
        }

        private void textBoxPipeName_TextChanged(object sender, EventArgs e)
        {
            buttonSetMAC.Enabled = true;
        }

        private void FormVirtual_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                pipeServer.Stop();
                pipeServer = null;
            }
            catch { }
        }
    }
}
