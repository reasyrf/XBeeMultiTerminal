using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Collections;

namespace XBee_Multi_Terminal
{
    public partial class FormTerminal : Form
    {
        public FormTerminal()
        {
            InitializeComponent();
        }
        private string MACAddress = "";
        private string label = "";

        public String GetLabel()
        {
            return label;
        }

        public void SetLabel(String text)
        {
            label = text;
            buttonSetMAC_Click(null, null);
        }

        public String GetMACAddress()
        {
            return MACAddress;
        }


        private void FormTerminal_Load(object sender, EventArgs e)
        {
            textBoxMACAddressH.CharacterCasing = CharacterCasing.Upper;
            textBoxMACAddressL.CharacterCasing = CharacterCasing.Upper;
            richTextBoxTerminal.Enabled = false;
            //TODO: copy selected text
        }

        public void AddTextToTerminal(String text)
        {
            try
            {
                if (text.Contains('\b'))
                {
                    //richTextBoxTerminal.Text = richTextBoxTerminal.Text.Substring(0, richTextBoxTerminal.TextLength - 1);
                    richTextBoxTerminal.Select(richTextBoxTerminal.TextLength - 1, richTextBoxTerminal.TextLength);
                    richTextBoxTerminal.SelectedText = "";
                }
                else if (text.ToArray()[0] == '\a')
                {
                }
                else
                {
                    richTextBoxTerminal.AppendText(text);
                }
            }
            catch { }
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
                buttonSetMAC.Focus();
            }
            buttonSetMAC.Enabled = true;
        }

        public void SetMACAddress(String text)
        {
            if (text.Length == 16)
            {
                MACAddress = text;
                textBoxMACAddressH.Text = text.Substring(0, 8);
                textBoxMACAddressL.Text = text.Substring(8, 8);
                buttonSetMAC_Click(null, null);


            }
        }
        private void buttonSetMAC_Click(object sender, EventArgs e)
        {
            if (textBoxMACAddressH.Text.Length != 8)
            {
                textBoxMACAddressH.Focus();
            }
            else if (textBoxMACAddressL.Text.Length != 8)
            {
                textBoxMACAddressL.Focus();
            }
            else
            {
                richTextBoxTerminal.Enabled = true;
               
                MACAddress = textBoxMACAddressH.Text + textBoxMACAddressL.Text ;
                UpdateTitle();
                buttonSetMAC.Enabled = false;
                richTextBoxTerminal.Focus();
                timerScrollback.Start();
                ((FormMultiTerm)this.ParentForm).sessionSaved = false;
            }
        }

        private void UpdateTitle()
        {
            if (MACAddress != "")
            {
                this.Text = label + " [" + textBoxMACAddressH.Text + " " + textBoxMACAddressL.Text + "]";
            } else
            {
                this.Text = label ;
            }
        }

        private void richTextBoxTerminal_KeyPress(object sender, KeyPressEventArgs e)
        {
            byte[] character = new byte[] { 0x00 };

            character[0] = (byte)e.KeyChar;
            TransmitCharacters(character);
            
            if (!checkBoxEchoChars.Checked) e.Handled = true;

        }

        private void TransmitCharacters(byte[] characters)
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
                    frame.SetRFData(characters);
                    parentForm.bee.Execute(frame);
                }
                catch (Exception ex)
                { //Serial probably not connected, just silently ignore 
                    if (ex.Message == "The port is closed.")
                    {
                        ((FormMultiTerm)this.ParentForm).DisconnectSerialWithException(ex);
                    }
                }
            }
        }


        private void richTextBoxTerminal_KeyDown(object sender, KeyEventArgs e)
        {
            //http://wiki.bash-hackers.org/scripting/terminalcodes
            //http://www.comptechdoc.org/os/linux/howlinuxworks/linux_hlvt100.html
            //This is to work with arrow keys and other weird presses
            //We aren't going for full terminal emulation, if you want that, use the 
            //named pipe functionality.
            //TODO clean up transmit code into separate method
            //TODO right click menu for pasting/copy

            Boolean specialCharPressed = false;

            //Check for paste

            bool ctrlV = e.Modifiers == Keys.Control && e.KeyCode == Keys.V;
            bool shiftIns = e.Modifiers == Keys.Shift && e.KeyCode == Keys.Insert;

            if (ctrlV || shiftIns)
            {
                pasteToolStripMenuItem_Click(null, null);
                e.SuppressKeyPress = true;
            }
            else
            {

                byte[] character = new byte[] { 0x00, 0x00, 0x00 };

                /* down = '\E[B' | '\EOB';	# Down Arrow
                 right = '\E[C' | '\EOC';	# Right Arrow
                 up = '\E[A' | '\EOA';	# Up Arrow
                 left = '\E[D' | '\EOD' | '^h';	# Left Arrow or backspace */

                if (e.KeyCode == Keys.Left)
                {
                    character[0] = (byte)0x1B;
                    character[1] = (byte)'[';
                    character[2] = (byte)'D';
                    specialCharPressed = true;
                    e.SuppressKeyPress = true;
                }

                if (e.KeyCode == Keys.Right)
                {
                    character[0] = (byte)0x1B;
                    character[1] = (byte)'[';
                    character[2] = (byte)'C';
                    specialCharPressed = true;
                    e.SuppressKeyPress = true;
                }

                if (e.KeyCode == Keys.Up)
                {
                    character[0] = (byte)0x1B;
                    character[1] = (byte)'[';
                    character[2] = (byte)'A';
                    specialCharPressed = true;
                    e.SuppressKeyPress = true;
                }

                if (e.KeyCode == Keys.Down)
                {
                    character[0] = (byte)0x1B;
                    character[1] = (byte)'[';
                    character[2] = (byte)'B';
                    specialCharPressed = true;
                    e.SuppressKeyPress = true;

                }

                if (e.KeyCode == Keys.Tab)
                {
                    character[0] = (byte)'\t' ;

                    specialCharPressed = true;
                    e.SuppressKeyPress = true;

                }

                //TODO allow selectable enter sequence type in settings
                if (e.KeyCode == Keys.Enter)
                {
                    //character[0] = (byte)'\r';
                    //character[1] = (byte)'\n';
                    character[0] = (byte)'\n';

                    specialCharPressed = true;
                    e.SuppressKeyPress = true;
                }

                if (e.KeyCode == Keys.Back)
                {
                    character[0] = (byte)'\b';

                    specialCharPressed = true;
                    e.SuppressKeyPress = true;
                }

                if (specialCharPressed)
                {
                    //Transmit chars here
                    TransmitCharacters(character);
                }
            }
            if (checkBoxEchoChars.Checked) e.SuppressKeyPress = false;

        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBoxTerminal.Clear();
        }

        private void timerScrollback_Tick(object sender, EventArgs e)
        {
            Int32 maxsize = 100000;
            Int32 dropsize = maxsize / 4;

            if (richTextBoxTerminal.Text.Length > maxsize)
            {
                // this method preserves the text colouring
                // find the first end-of-line past the endmarker

                Int32 endmarker = richTextBoxTerminal.Text.IndexOf('\n', dropsize) + 1;
                if (endmarker < dropsize)
                    endmarker = dropsize;

                richTextBoxTerminal.Select(0, endmarker);
                richTextBoxTerminal.SelectedText = "";
            }
        }

        protected void OnTitlebarClick(Point pos)
        {
            contextMenuStripTitle.Show(pos);
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0xa4)
            {  // Trap WM_NCRBUTTONDOWN
                Point pos = new Point(m.LParam.ToInt32() & 0xffff, m.LParam.ToInt32() >> 16);
                OnTitlebarClick(pos);
                return;
            }
            base.WndProc(ref m);
        }

        private void labelTerminalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InputBoxResult result = InputBox.Show("Label:", "Label Prompt", "",
                                             new InputBoxValidatingHandler(inputBox_Validating));
            if (result.OK)
            {
                //label = result.Text;
                UpdateTitle();
                if (MACAddress != "")
                    ((FormMultiTerm)this.ParentForm).sessionSaved = false;

            }

        }
        private void inputBox_Validating(object sender, InputBoxValidatingArgs e)
        {
            if (e.Text.Trim().Length == 0)
            {
                e.Cancel = true;
                e.Message = "Required";
            } else
            {
                label = e.Text;
            }
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /*
             * If you want to avoid fragmentation, the payload size without 
             * encryption will be 84 bytes, 66 bytes if encryption is enabled 
             * (EE parameter), and 92 bytes if the transmission is a broadcast. 
             * Broadcast transmissions do not use encryption. */
            byte[] send;
            try
            {
                ArrayList characters = new ArrayList(); 

                String pasteData = Clipboard.GetText().ToString();
                foreach (Char ch in pasteData)
                {
                    characters.Add((byte)ch);
                    if (characters.Count >= 66)
                    {
                        send = (byte[])characters.ToArray(typeof(byte));
                        TransmitCharacters(send);
                        characters.Clear();
                        //this.MdiParent.Refresh();
                    }
                }
                if (characters.Count >= 0)
                {
                    send = (byte[])characters.ToArray(typeof(byte));
                    TransmitCharacters(send);
                    characters.Clear();
                }
            }
            catch { }

        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                String copyText = richTextBoxTerminal.Text;
                Clipboard.SetText(copyText);
            }
            catch { }
        }

        private void checkBoxEchoChars_CheckedChanged(object sender, EventArgs e)
        {
            richTextBoxTerminal.Focus();
        }

        private void copySelectedTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                String copyText = richTextBoxTerminal.SelectedText;
                Clipboard.SetText(copyText);
            }
            catch { }
        }
    }
}
