using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using XBee.Frames;
using System.Threading;
using System.Collections;
using System.IO;
using System.Configuration;
using System.Runtime.InteropServices;

namespace XBee_Multi_Terminal
{


    public partial class FormMultiTerm : Form
    {
        
        
        BackgroundWorker packetRecieverWorker = new BackgroundWorker();
        XBee_Multi_Terminal.Properties.Settings programSettings;

        //TODO put in getters for these.
        public Boolean serialConnected = false;
        private Boolean apiRefresh = false;

        public Boolean sessionSaved = true;
        private Boolean notClosing = true;
        private Boolean showRSSI = false;

        ulong routingMode = 0; //AT.RoutingMode
        ulong firmwareVersion = 0; //AT.FirmwareVersion
        ulong serialLow = 0; //AT.SerialNumberLow
        ulong serialHigh = 0; //AT.SerialNumberHigh
        ulong apiEnable = 0; //AT.ApiEnable
        int rssiValue = 0;
        //int taperWorkCount = 0;

        //const int TAPERMESSAGECOUNT = 100; //Taper off thread after this many messages in succession (sleep between received frames).


        public XBee.XBee bee;
        XBee.SerialConnection serialConnection;

        string[] routingModes = { "Standard Router [0]", "Indirect Msg Coordinator [1]", "Non-Routing Module [2]",
            "Non-Routing Coordinator [3]", "Indirect Msg Poller [4]", "N/A [5]", "Non-Routing Poller [6]"};

        string[] aPIModes = { "Transparent Mode [0]", "API Mode Without Escapes [1]", "API Mode With Escapes [2]"};



        public FormMultiTerm()
        {
            InitializeComponent();
        }

        private void addClientToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormTerminal newMDIChild = new FormTerminal();
            // Set the Parent Form of the Child window.
            newMDIChild.MdiParent = this;
            // Display the new form.
            newMDIChild.Show();
        }

        private void FormMultiTerm_Load(object sender, EventArgs e)
        {
            FormLog newMDIChild = new FormLog();
            // Set the Parent Form of the Child window.
            newMDIChild.MdiParent = this;
            // Display the new form.
            newMDIChild.Show();

            packetRecieverWorker.DoWork += packetRecieverWorker_DoWork;
            packetRecieverWorker.ProgressChanged += packetRecieverWorker_ProgressChanged;
            packetRecieverWorker.WorkerReportsProgress = true;
            packetRecieverWorker.RunWorkerAsync();

            bee = new XBee.XBee { ApiType = XBee.ApiTypeValue.Enabled };

            XBee_Multi_Terminal.Properties.Settings current = new XBee_Multi_Terminal.Properties.Settings();

            showRSSI = !current.ShowRSSI; //The next function inverts the showrssi value.

            showRSSIToolStripMenuItem_Click(null, null);

            RefreshSerialPortLabel();


            
        }

        public void RefreshSerialPortLabel()
        {
            programSettings = new XBee_Multi_Terminal.Properties.Settings();
            serialPort.BaudRate = (int)programSettings.BaudRate;
            serialPort.PortName = programSettings.PortName;
            toolStripStatusLabelSerialPortSettings.Text = serialPort.PortName + " " + serialPort.BaudRate;

        }

        public void DisconnectSerialWithException(Exception ex)
        {
            Boolean disconnect = true;
            if (ex is InvalidCastException )
            {
                if (apiRefresh)
                {
                    //Just ignore if serial already connected.
                    disconnect = false;
                    
                   
                }
                else
                {
                    //If serial not connected, return error. 
                    toolStripStatusLabelFirmware.Text = "Please retry connecting.";
                    bee = new XBee.XBee { ApiType = XBee.ApiTypeValue.Enabled };
                    //TODO if not in API mode, allow connection, but change APITypeValue.
                }

            }

            else if ( ex is NullReferenceException)
            {
                if (apiRefresh)
                {
                    //Just ignore if serial already connected.
                    disconnect = false;


                }
                else
                {
                    //If serial not connected, return error. 
                    toolStripStatusLabelFirmware.Text = "Invalid port speed or not in API mode (without escapes). Please check config, reset modem and retry connecting.";
                    bee = new XBee.XBee { ApiType = XBee.ApiTypeValue.Enabled };
                    //TODO if not in API mode, allow connection, but change APITypeValue.
                }

            }

            else if (ex is System.IO.IOException)
            {
                toolStripStatusLabelFirmware.Text = "";
                MessageBox.Show("Could not open serial port!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            } else if (ex.Message == "The port is closed.")
            {
                toolStripStatusLabelFirmware.Text = "Serial port disconnected!";
            }

            else
            {
                toolStripStatusLabelFirmware.Text = "Error communicating with xbee module, try a module reset!";
                bee = new XBee.XBee { ApiType = XBee.ApiTypeValue.Enabled };
            }
            if (disconnect)
            {
                serialConnected = false;

                
                serialConnection.Close();


                toolStripStatusLabelConnectionStatus.Text = "Disconnected";
                //toolStripStatusLabelFirmware.Text = "";

                if (serialConnected) { openSerialPortToolStripMenuItem.Text = "&Close Serial Port"; }
                else { openSerialPortToolStripMenuItem.Text = "&Open Serial Port"; }
                apiRefresh = false;
                //toolStripProgressBarRSSI.Value = 0;
            }
        }

        private void RefreshFirmwareDetailsLabels()
        {
            toolStripStatusLabelFirmware.Text = String.Format("Firmware: {0:X4}", firmwareVersion);

            toolStripStatusLabelFirmware.Text = toolStripStatusLabelFirmware.Text + ", " +
                String.Format("Config : {0:X8}", serialHigh);

            toolStripStatusLabelFirmware.Text = toolStripStatusLabelFirmware.Text +
                String.Format("-{0:X8}", serialLow);

            toolStripStatusLabelFirmware.Text = toolStripStatusLabelFirmware.Text + ", " +
                routingModes[routingMode];

            toolStripStatusLabelFirmware.Text = toolStripStatusLabelFirmware.Text + ", " +
                 aPIModes[apiEnable];
        }

        private void RefreshFirmwareDetails()
        {
            //var request = new ATCommand(AT.ApiEnable) { FrameId = 1 };
            //var frame = bee.ExecuteQuery(request, 1000);
            //var value = ((ATCommandResponse)frame).Value;
            
            var request = new ATCommand(AT.FirmwareVersion) { FrameId = 1 };
            bee.Execute(request);
            //var value = ((ATCommandResponse)frame).Value;
            //toolStripStatusLabelFirmware.Text = String.Format("Firmware: {0:X4}", ((ATLongValue)value).Value);

            request = new ATCommand(AT.SerialNumberHigh) { FrameId = 2 };
            bee.Execute(request);
            //value = ((ATCommandResponse)frame).Value;
            //toolStripStatusLabelFirmware.Text = toolStripStatusLabelFirmware.Text + ", " +
            //    String.Format("Config : {0:X8}", ((ATLongValue)value).Value);

            request = new ATCommand(AT.SerialNumberLow) { FrameId = 3 };
            bee.Execute(request);
            //value = ((ATCommandResponse)frame).Value;
            //toolStripStatusLabelFirmware.Text = toolStripStatusLabelFirmware.Text +
            //    String.Format("-{0:X8}", ((ATLongValue)value).Value);

            request = new ATCommand(AT.RoutingMode) { FrameId = 4 };
            bee.Execute(request);
            //value = ((ATCommandResponse)frame).Value;
            //toolStripStatusLabelFirmware.Text = toolStripStatusLabelFirmware.Text + ", " +
            //    routingModes[((ATLongValue)value).Value];

            request = new ATCommand(AT.ApiEnable) { FrameId = 5 };
            bee.Execute(request);
            //value = ((ATCommandResponse)frame).Value;

            //toolStripStatusLabelFirmware.Text = toolStripStatusLabelFirmware.Text + ", " +
            //     aPIModes[((ATLongValue)value).Value];


            /*request = new ATCommand(AT.DestinationHigh) { FrameId = 6 };
            frame = bee.ExecuteQuery(request, 100);
            value = ((ATCommandResponse)frame).Value;
            toolStripStatusLabelFirmware.Text = toolStripStatusLabelFirmware.Text + ", " +
                String.Format("Dest : {0:X8}", ((ATLongValue)value).Value);

            request = new ATCommand(AT.DestinationLow) { FrameId = 7 };
            frame = bee.ExecuteQuery(request, 100);
            value = ((ATCommandResponse)frame).Value;
            toolStripStatusLabelFirmware.Text = toolStripStatusLabelFirmware.Text + 
                String.Format("-{0:X8}", ((ATLongValue)value).Value);*/
            apiRefresh = true;
        }


        private void toolStripStatusLabelConnectionStatus_Click(object sender, EventArgs e)
        {
            if (serialConnected)
            {
                serialConnected = false;

                serialConnection.Close();

                toolStripStatusLabelConnectionStatus.Text = "Disconnected";
                toolStripStatusLabelFirmware.Text = "";
                RefreshSerialPortLabel();
                timerHeatbeat.Enabled = false;
                apiRefresh = false;
                
                
            }
            else
            {
                try
                {
                    
                    toolStripStatusLabelConnectionStatus.Text = "Connecting..";

                    this.Refresh();
                    serialConnection = new XBee.SerialConnection(serialPort.PortName, serialPort.BaudRate);
                    bee.SetConnection(serialConnection);


                    serialConnected = true;
                    RefreshFirmwareDetails();

                    //timerHeatbeat.Enabled = true;
                    toolStripStatusLabelConnectionStatus.Text = "Connected   ";


                }
                catch (Exception ex)
                {

                        DisconnectSerialWithException(ex);
                        timerHeatbeat.Enabled = false;
                    

                }

            }

            if (serialConnected) { openSerialPortToolStripMenuItem.Text = "&Close Serial Port"; }
            else { openSerialPortToolStripMenuItem.Text = "&Open Serial Port"; }

        }

        private void FormMultiTerm_FormClosing(object sender, FormClosingEventArgs e)
        {
            notClosing = false;
            try
            {
                packetRecieverWorker.CancelAsync();
            }
            catch { }
            XBee_Multi_Terminal.Properties.Settings current = new XBee_Multi_Terminal.Properties.Settings();
            

            
            if (!sessionSaved && current.ConfirmSaveOnExit && (GetAnyVirtualForm() != null || GetAnyTerminalForm() != null))
            {
                if (MessageBox.Show("Are you sure you want to close without saving the session?", "Save Session", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                {
                    e.Cancel = true;
                    
                } else
                {
                    try
                    {
                        serialConnected = false;
                        serialConnection.Close();
                    }
                    catch { }
                }
            } else
            {
                try
                {
                    serialConnected = false;
                    serialConnection.Close();
                }
                catch { }
            }
        }


        private void openLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormLog newMDIChild = new FormLog();
            // Set the Parent Form of the Child window.
            newMDIChild.MdiParent = this;
            // Display the new form.
            newMDIChild.Show();
        }

        private void toolStripStatusLabelSerialPortSettings_Click(object sender, EventArgs e)
        {
            openSettingsToolStripMenuItem_Click(null, null);

        }

        //Async handler.
        private void packetRecieverWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            XBee.XBeeFrame lastFrame = null;
            
           // Guid prevFrameGUID = new Guid();

            //Main packet received handler.
            while (notClosing)
            {
                if (bee != null)
                {
                    lastFrame = bee.GetNextQueueFrame();
                    if (lastFrame != null)
                    {
                        //if (bee.GetLastFrameGUID() != prevFrameGUID)
                        //{
                        //lastFrame = bee.GetLastFrame();
                        //prevFrameGUID = bee.GetLastFrameGUID();

                        if (lastFrame.GetCommandId() == XBee.XBeeAPICommandId.RECEIVE_PACKET_RESPONSE)
                        {
                            try
                            {
                                //New packetd created here so the same packet is not reused by the background worker progress changed.
                                Packet packet = new Packet();
                                packet.data = ((XBee.Frames.ZigBeeReceivePacket)lastFrame).Data;
                                packet.address = ((XBee.Frames.ZigBeeReceivePacket)lastFrame).Source.Address64.GetAddress();
                                packet.processed = false;
                                packetRecieverWorker.ReportProgress(0, packet);


                            }
                            catch { }
                        }
                        else if (lastFrame.GetCommandId() == XBee.XBeeAPICommandId.AT_COMMAND_RESPONSE)
                        {
                            try
                            {

                                //ulong routingMode = 0; //AT.RoutingMode
                                //ulong firmwareVersion = 0; //AT.FirmwareVersion
                                //ulong serialLow = 0; //AT.SerialNumberLow
                                //ulong serialHigh = 0; //AT.SerialNumberHigh
                                //ulong apiEnable = 0; //AT.ApiEnable

                                //Is a discover frame?
                                if (((XBee.Frames.ATCommandResponse)lastFrame).GetIsDiscovery())
                                {
                                    Packet packet = new Packet();
                                    packet.address = ((XBee.Frames.ATCommandResponse)lastFrame).GetDiscoverySource().Address64.GetAddress();
                                    packet.processed = false;
                                    packetRecieverWorker.ReportProgress(1, packet);
                                } else if (((XBee.Frames.ATCommandResponse)lastFrame).Command == XBee.Frames.AT.RoutingMode)
                                {
                                    routingMode = ((ATLongValue)(((ATCommandResponse)lastFrame).Value)).Value;
                                    RefreshFirmwareDetailsLabels();

                                }
                                else if (((XBee.Frames.ATCommandResponse)lastFrame).Command == XBee.Frames.AT.FirmwareVersion)
                                {
                                    firmwareVersion = ((ATLongValue)(((ATCommandResponse)lastFrame).Value)).Value;
                                    RefreshFirmwareDetailsLabels();

                                }
                                else if (((XBee.Frames.ATCommandResponse)lastFrame).Command == XBee.Frames.AT.SerialNumberHigh)
                                {
                                    serialHigh = ((ATLongValue)(((ATCommandResponse)lastFrame).Value)).Value;
                                    RefreshFirmwareDetailsLabels();

                                }
                                else if (((XBee.Frames.ATCommandResponse)lastFrame).Command == XBee.Frames.AT.SerialNumberLow)
                                {
                                    serialLow = ((ATLongValue)(((ATCommandResponse)lastFrame).Value)).Value;
                                    RefreshFirmwareDetailsLabels();

                                }
                                else if (((XBee.Frames.ATCommandResponse)lastFrame).Command == XBee.Frames.AT.ApiEnable)
                                {
                                    apiEnable = ((ATLongValue)(((ATCommandResponse)lastFrame).Value)).Value;
                                    RefreshFirmwareDetailsLabels();

                                } else if (((XBee.Frames.ATCommandResponse)lastFrame).Command == XBee.Frames.AT.ReceivedSignalStrength)
                                {
                                    rssiValue = (int)((ATLongValue)(((ATCommandResponse)lastFrame).Value)).Value;
                                    
                                    
                                }

                               
                            }
                            catch{ }
                        }

                        //Taper off thread if many frames are received in succession to prevent GUI lockup
                        //if (taperWorkCount > 0) taperWorkCount--;
                        //else Thread.Sleep(1);
                        

                    }
                    else
                    {
                        //
                        packetRecieverWorker.ReportProgress(-1, new Packet());
                        Thread.Sleep(1);
                        //taperWorkCount = TAPERMESSAGECOUNT;
                    }
                }
                else
                {
                    Thread.Sleep(10);
                }
            }
        }

        private void packetRecieverWorker_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            Packet packet = (Packet)e.UserState;

            //Prevent duplicate packets with the way the progress changed threading model works.
            if (!packet.processed)
            {
                packet.processed = true;
                //Is data received (terminal data)
                if (e.ProgressPercentage == 0)
                {
                    foreach (Form form in Application.OpenForms)
                    {

                        if (form.GetType() == typeof(FormTerminal))
                        {
                            if (((FormTerminal)form).GetMACAddress() == BitConverter.ToString(packet.address).Replace("-", string.Empty))
                                ((FormTerminal)form).AddTextToTerminal(System.Text.Encoding.UTF8.GetString(packet.data));
                        }
                        else if (form.GetType() == typeof(FormVirtual))
                        {
                            if (((FormVirtual)form).GetMACAddress() == BitConverter.ToString(packet.address).Replace("-", string.Empty))
                                ((FormVirtual)form).AddTextToTerminal(System.Text.Encoding.UTF8.GetString(packet.data));
                        }

                    }
                }
                //Is a command response.
                if (e.ProgressPercentage == 1)
                {
                    foreach (Form form in Application.OpenForms)
                    {

                        if (form.Text == "Client List")
                        {
                            ((FormClientList)form).AddClientAddress(BitConverter.ToString(packet.address).Replace("-", string.Empty));
                        }
                    }
                }
            }

        }


        private void aPIEnableToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (serialConnected == true)
            {
                try
                {


                    var request = new ATCommand(AT.ApiEnable) { FrameId = 1 };
                    bee.Execute(request);

                } catch (Exception ex)
                {
                    DisconnectSerialWithException(ex);
                }
            }
            else
            {
                MessageBox.Show("Serial port not connected!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void detectClientsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (serialConnected == true)
            {
                try
                {

                    var request = new ATCommand(AT.NodeDiscover) { FrameId = 1 };
                    bee.Execute(request);

                    Form found = GetForm("Client List");

                    try
                    {
                        if (found != null)
                        {
                            ((FormClientList)found).refreshToolStripMenuItem_Click(null, null);
                        }
                        else
                        {

                            FormClientList newMDIChild = new FormClientList();
                            // Set the Parent Form of the Child window.
                            newMDIChild.MdiParent = this;
                            // Display the new form.
                            newMDIChild.Show();
                        }
                    }
                    catch { }
                }
                catch (Exception ex)
                {
                    DisconnectSerialWithException(ex);
                }


            }
            else
            {
                MessageBox.Show("Serial port not connected!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private Boolean SearchForm(String name)
        {
            Boolean found = false;
            foreach (Form form in Application.OpenForms)
            {
                if (form.Text == name)
                {
                    found = true;
                }

            }
            return found;
        }

        private Form GetForm(String name)
        {

            foreach (Form form in Application.OpenForms)
            {

                if (form.Text == "Client List")
                {
                    return form;
                }
            }
            return null;
        }

        private FormVirtual GetAnyVirtualForm()
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(FormVirtual))
                {
                    return (FormVirtual)form;

                }


            }
            return null;
        }

        private FormTerminal GetAnyTerminalForm()
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(FormTerminal))
                {
                    return (FormTerminal)form;

                }


            }
            return null;
        }

        private void openSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            if (!SearchForm("Settings"))
            {
                FormSettings newMDIChild = new FormSettings();
                // Set the Parent Form of the Child window.
                newMDIChild.MdiParent = this;
                // Display the new form.

                newMDIChild.MaximizeBox = false;
                newMDIChild.MinimizeBox = false;
                newMDIChild.ControlBox = false;
                //newMDIChild.WindowState = FormWindowState.Maximized;
                newMDIChild.Show();
                newMDIChild.Focus();
                newMDIChild.WindowState = FormWindowState.Minimized;
                newMDIChild.WindowState = FormWindowState.Maximized;
            }
        }

        private void openSerialPortToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripStatusLabelConnectionStatus_Click(null, null);

        }

        private void createVirtualComToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormVirtual newMDIChild = new FormVirtual();
            // Set the Parent Form of the Child window.
            newMDIChild.MdiParent = this;
            // Display the new form.
            newMDIChild.Show();
            
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox box = new AboutBox();
            box.ShowDialog();
        }

        private void setBroadcastAddressToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (serialConnected == true)
            {
                try
                {
                    ATValue atValue = new ATLongValue();
                    var request = new ATCommand(AT.DestinationHigh) { FrameId = 1 };
                    request.SetValue(atValue.FromByteArray(new byte[] { 0x00, 0x00 }));
                    bee.Execute(request);
                    //var value = ((ATCommandResponse)frame).Value;

                    request = new ATCommand(AT.DestinationLow) { FrameId = 1 };
                    request.SetValue(atValue.FromByteArray(new byte[] { 0xFF, 0xFE }));
                    bee.Execute(request);
                    //value = ((ATCommandResponse)frame).Value;

                    //RefreshFirmwareDetails();
                }
                catch (Exception ex)
                {
                    DisconnectSerialWithException(ex);

                }
            }
            else
            {
                MessageBox.Show("Serial port not connected!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void setCoordinatorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (serialConnected == true)
            {
                try
                {
                    ATValue atValue = new ATLongValue();
                    var request = new ATCommand(AT.RoutingMode) { FrameId = 1 };
                    request.SetValue(atValue.FromByteArray(new byte[] { 0x03 }));
                    bee.Execute(request);
                    //var value = ((ATCommandResponse)frame).Value;

                    //RefreshFirmwareDetails();
                } catch (Exception ex)
                {
                    DisconnectSerialWithException(ex);

                }


            }
            else
            {
                MessageBox.Show("Serial port not connected!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void saveCurrentSessionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            ArrayList formData = new ArrayList();

            int formCount = 0;

            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(FormTerminal))
                {
                    if (((FormTerminal)form).GetMACAddress() != "")
                    {
                        formCount++;
                        //Save form terminal
                        formData.Add("Terminal, " + ((FormTerminal)form).GetMACAddress() + ", " + ((FormTerminal)form).GetLabel());
                    }

                }
                else if (form.GetType() == typeof(FormVirtual))
                {
                    if (((FormVirtual)form).GetMACAddress() != "")
                    {
                        formCount++;
                        //Save form pipe
                        formData.Add("Pipe, " + ((FormVirtual)form).GetMACAddress() + ", " + ((FormVirtual)form).GetPipeName());
                    }
                }
                

            }
            if (formCount > 0)
            {
                saveFileDialogSession.ShowDialog();
                if (saveFileDialogSession.FileName != "")
                {
                    using (TextWriter writer = File.CreateText(@saveFileDialogSession.FileName))
                    {
                        foreach (String formString in formData)
                        {
                            writer.WriteLine(formString);
                        }
                    }
                    sessionSaved = true;
                }
                
            }
            else
            {
                MessageBox.Show("No terminal sessions to save!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void openPreviousSessionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialogSession.ShowDialog();
            if (openFileDialogSession.FileName != "")
            {
                ArrayList formData = new ArrayList();

                TextReader tr;
                tr = File.OpenText(openFileDialogSession.FileName);

                string form;
                form = tr.ReadLine();
                while (form != null)
                {
                    formData.Add(form);
                    form = tr.ReadLine();
                }

                try
                {
                    foreach (String formString in formData)
                    {
                        if (formString.Split(',')[0] == "Pipe")
                        {
                            FormVirtual newMDIChild = new FormVirtual();
                            // Set the Parent Form of the Child window.
                            newMDIChild.MdiParent = this;


                            // Display the new form.
                            newMDIChild.Show();

                            newMDIChild.setMACAddress(formString.Split(',')[1].Trim());
                            newMDIChild.setPipe(formString.Split(',')[2].Trim());


                        }
                        else if (formString.Split(',')[0] == "Terminal")
                        {
                            FormTerminal newMDIChild = new FormTerminal();
                            // Set the Parent Form of the Child window.
                            newMDIChild.MdiParent = this;


                            // Display the new form.
                            newMDIChild.Show();

                            newMDIChild.SetMACAddress(formString.Split(',')[1].Trim());
                            try //It might not have the label from an old version of the file.
                            {
                                newMDIChild.SetLabel(formString.Split(',')[2].Trim());
                            }
                            catch { }
                        }
                    }
                    //This overrides the saved state of the existing terminals that might be open.
                    //This won't catch on exit if the session hasn't been saved, but it makes things simple
                    sessionSaved = true; 
                }
                catch
                {
                    MessageBox.Show("Error parsing settings file!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
        }

        private void cascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (!SearchForm("Settings"))
            {
                this.LayoutMdi(System.Windows.Forms.MdiLayout.Cascade);
            }
        }

        private void tileHorizontallyToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (!SearchForm("Settings"))
            {
                this.LayoutMdi(System.Windows.Forms.MdiLayout.TileHorizontal);
            }
        }

        private void tileVerticallyToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (!SearchForm("Settings"))
            {
                this.LayoutMdi(System.Windows.Forms.MdiLayout.TileVertical);
            }
        }

        private void closeAllClientTerminalsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form next = GetAnyTerminalForm();
            while (next != null )
            {
                next.Close();
                next = GetAnyTerminalForm();
            }

            next = GetAnyVirtualForm();
            while (next != null)
            {
                next.Close();
                next = GetAnyVirtualForm();
            }

        }

        //TODO: add more serial settings (on/off etc).

        private void helpToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Help.ShowHelp(this, "help.chm");
        }

        private int RoundOff(int i)
        {
            return ((int)Math.Round(i / 10.0)) * 10;
        }
        

        private void timerRSSI_Tick(object sender, EventArgs e)
        {
            if (serialConnected)
            { 
                try
                {
                    //TODO: change update rate and disable
                    var request = new ATCommand(AT.ReceivedSignalStrength) { FrameId = 1 };
                    bee.Execute(request);
                    
                }
                catch  { }

                //Need to update bar here because it is a cross thread operation. The RSSI will be one tick behind
                //because the rssi is received in the async handler.

                int scaledRSSI = rssiValue;

                //The value the XBee reports is accurate between -40 dBM and receiver (RX) sensitivity.
                //The value is in negative dBm 
                if (scaledRSSI < 40) scaledRSSI = 40;

                //Scale RSSI between 0-100
                scaledRSSI = (int)((double)(100 - rssiValue) / 60 * 100);


                switch (RoundOff(scaledRSSI))
                {
                    case 0:
                        this.toolStripStatusLabelRSSI.Image = global::XBee_Multi_Terminal.Properties.Resources.p0;
                        break;
                    case 10:
                        this.toolStripStatusLabelRSSI.Image = global::XBee_Multi_Terminal.Properties.Resources.p10;
                        break;
                    case 20:
                        this.toolStripStatusLabelRSSI.Image = global::XBee_Multi_Terminal.Properties.Resources.p20;
                        break;
                    case 30:
                        this.toolStripStatusLabelRSSI.Image = global::XBee_Multi_Terminal.Properties.Resources.p30;
                        break;
                    case 40:
                        this.toolStripStatusLabelRSSI.Image = global::XBee_Multi_Terminal.Properties.Resources.p40;
                        break;
                    case 50:
                        this.toolStripStatusLabelRSSI.Image = global::XBee_Multi_Terminal.Properties.Resources.p50;
                        break;
                    case 60:
                        this.toolStripStatusLabelRSSI.Image = global::XBee_Multi_Terminal.Properties.Resources.p60;
                        break;
                    case 70:
                        this.toolStripStatusLabelRSSI.Image = global::XBee_Multi_Terminal.Properties.Resources.p70;
                        break;
                    case 80:
                        this.toolStripStatusLabelRSSI.Image = global::XBee_Multi_Terminal.Properties.Resources.p80;
                        break;
                    case 90:
                        this.toolStripStatusLabelRSSI.Image = global::XBee_Multi_Terminal.Properties.Resources.p90;
                        break;
                    case 100:
                        this.toolStripStatusLabelRSSI.Image = global::XBee_Multi_Terminal.Properties.Resources.p100;
                        break;
                    default:
                        break;
                }


                /*if (rssiValue == toolStripProgressBarRSSI.Maximum)
                {
                    // Special case (can't set value > Maximum).
                    toolStripProgressBarRSSI.Value = rssiValue;           // Set the value
                    toolStripProgressBarRSSI.Value = rssiValue - 1;       // Move it backwards
                }
                else
                {
                    toolStripProgressBarRSSI.Value = rssiValue + 1;       // Move past
                }
                toolStripProgressBarRSSI.Value = rssiValue;               // Move to correct value*/
                toolStripStatusLabelRSSI.ToolTipText = "RSSI: -" + rssiValue.ToString() + "dBm";
            }

            
            
        }

        private void showRSSIToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (showRSSI)
            {
                toolStripStatusLabelRSSI.Visible = false;
                toolStripStatusLabelDivider3.Visible = false;
                showRSSIToolStripMenuItem.Text = "Show &RSSI";
                this.toolStripStatusLabelRSSI.Image = global::XBee_Multi_Terminal.Properties.Resources.p0;


                showRSSI = false;
                timerRSSI.Stop();
            } else
            {
                this.toolStripStatusLabelRSSI.Image = global::XBee_Multi_Terminal.Properties.Resources.p0;
                toolStripStatusLabelRSSI.Visible = true;
                
                toolStripStatusLabelDivider3.Visible = true;
                showRSSIToolStripMenuItem.Text = "Hide &RSSI";
                showRSSI = true;
                timerRSSI.Start();
            }
        }

        private void timerHeatbeat_Tick(object sender, EventArgs e)
        {
            try
            {
                var request = new ATCommand(AT.Temperature) { FrameId = 1 };
                bee.Execute(request);
            }
            catch { }
        }
    }

    public class Packet
    {
        public byte[] data;
        public byte[] address;
        public bool processed;
    }
}
