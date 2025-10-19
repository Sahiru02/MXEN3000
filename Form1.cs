using System;
using System.IO.Ports;
using System.Windows.Forms;

namespace BangBang_GUI
{
    public partial class Form1 : Form
    {
        private SerialPort _port = new SerialPort();
        private const byte START = 255;

        private enum Port : byte
        {
            Input1 = 0,
            Input2 = 1,
            Output1 = 2,
            Output2 = 3,
            RequestOutput1 = 4,
            RequestOutput2 = 5,
            PidEnable = 7,
            SetKp = 10,      // Deadband
            SetKi = 11,      // TurnBoost
            SetKd = 12,      // unused
            SetBase = 13,    // 0..255 base
            SetThresh = 14,  // optional threshold
            SetInvert = 15   // 0/1
        }

        public Form1()
        {
            InitializeComponent();
            comboPorts.Items.AddRange(SerialPort.GetPortNames());
            if (comboPorts.Items.Count > 0) comboPorts.SelectedIndex = 0;
            _port.BaudRate = 9600;
            _port.ReadTimeout = 50;
            _port.WriteTimeout = 50;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            comboPorts.Items.Clear();
            comboPorts.Items.AddRange(SerialPort.GetPortNames());
            if (comboPorts.Items.Count > 0) comboPorts.SelectedIndex = 0;
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                if (_port.IsOpen) { _port.Close(); }
                _port.PortName = comboPorts.Text;
                _port.Open();
                lblStatus.Text = "Connected";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Connect failed: " + ex.Message);
            }
        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            try { if (_port.IsOpen) _port.Close(); } catch { }
            lblStatus.Text = "Disconnected";
        }

        private void SendPacket(Port port, byte data)
        {
            if (!_port.IsOpen) return;
            byte[] buf = new byte[4];
            buf[0] = START;
            buf[1] = (byte)port;
            buf[2] = data;
            buf[3] = (byte)(START + buf[1] + buf[2]);
            try { _port.Write(buf, 0, 4); } catch { }
        }

        private void btnEnable_Click(object sender, EventArgs e)
        {
            SendPacket(Port.PidEnable, 1);
        }

        private void btnDisable_Click(object sender, EventArgs e)
        {
            SendPacket(Port.PidEnable, 0);
            SendPacket(Port.Output1, 0);
            SendPacket(Port.Output2, 0);
        }

        private void chkInvert_CheckedChanged(object sender, EventArgs e)
        {
            SendPacket(Port.SetInvert, chkInvert.Checked ? (byte)1 : (byte)0);
        }

        private void apply_Click(object sender, EventArgs e)
        {
            byte baseSpd = (byte)numBase.Value;        // 0..255
            byte deadband = (byte)numDeadband.Value;   // 0..255
            byte turnBoost = (byte)numTurnBoost.Value; // 0..255
            byte thresh = (byte)numThresh.Value;       // 0..255 (optional)

            SendPacket(Port.SetBase, baseSpd);
            SendPacket(Port.SetKp, deadband);
            SendPacket(Port.SetKi, turnBoost);
            SendPacket(Port.SetKd, 0);                 // unused in firmware
            SendPacket(Port.SetThresh, thresh);
        }

        private void timerPoll_Tick(object sender, EventArgs e)
        {
            // poll sensors and outputs
            SendPacket(Port.Input1, 0);
            SendPacket(Port.Input2, 0);
            SendPacket(Port.RequestOutput1, 0);
            SendPacket(Port.RequestOutput2, 0);

            // read whatever is available in multiples of 4
            try
            {
                while (_port.IsOpen && _port.BytesToRead >= 4)
                {
                    int b0 = _port.ReadByte();
                    if (b0 != START) continue;
                    int p = _port.ReadByte();
                    int d = _port.ReadByte();
                    int c = _port.ReadByte();
                    if (((START + p + d) & 0xFF) != c) continue;
                    switch ((Port)p)
                    {
                        case Port.Input1: lblIn1.Text = d.ToString(); break;
                        case Port.Input2: lblIn2.Text = d.ToString(); break;
                        case Port.RequestOutput1: lblOut1.Text = d.ToString(); break;
                        case Port.RequestOutput2: lblOut2.Text = d.ToString(); break;
                    }
                }
            }
            catch { /* transient serial errors ignored */ }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try { if (_port.IsOpen) _port.Close(); } catch { }
        }
    }
}
