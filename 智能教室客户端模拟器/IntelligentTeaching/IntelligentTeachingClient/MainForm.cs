using System;
using System.Windows.Forms;

using MetroFramework.Forms;
using System.Net;
using IntelligentTeachingClient;
using System.Net.Sockets;
using System.Text;
using System.Collections.Generic;
using MetroFramework.Controls;

namespace MetroFramework
{
    public partial class MainForm : MetroForm
    {
        public Socket clientSocket; //The main client socket
        public EndPoint epServer;   //The EndPoint of the server
        private byte[] byteData = new byte[1024];
        List<MetroCheckBox> checkboxList = new List<MetroCheckBox>();

        public MainForm()
        {
            InitializeComponent();

            this.checkboxList.Add(metroCheckBoxA);
            this.checkboxList.Add(metroCheckBoxB);
            this.checkboxList.Add(metroCheckBoxC);
            this.checkboxList.Add(metroCheckBoxD);
            this.checkboxList.Add(metroCheckBoxE);
            this.checkboxList.Add(metroCheckBoxF);
            this.checkboxList.Add(metroCheckBoxG);
            this.checkboxList.Add(metroCheckBoxH);
            this.checkboxList.Add(metroCheckBoxI);
            this.checkboxList.Add(metroCheckBoxJ);

            this.txtEPC.Text = GlobalPara.client_epc;
            this.txtEquipmentID.Text = GlobalPara.equipmentID;
            this.txtID.Text = GlobalPara.clientID;
            initialSocket();

            this.FormClosing += (sender, e) => { Program.frmLogin.Close(); };

        }

        private void metroTileSwitch_Click(object sender, EventArgs e)
        {
            metroStyleManager.Style = MetroColorStyle.Blue;
        }

        private void initialSocket()
        {
            try
            {
                //IP address of the server machine
                IPAddress ipAddress = IPAddress.Parse(GlobalPara.dest_IP);
                int port = int.Parse(GlobalPara.dest_port);
                IPEndPoint ipEndPoint = new IPEndPoint(ipAddress, port);
                epServer = (EndPoint)ipEndPoint;
                //Using UDP sockets
                clientSocket = new Socket(AddressFamily.InterNetwork,
                    SocketType.Dgram, ProtocolType.Udp);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "客户端", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void sendCommand(string value)
        {
            SelectCommand sc = new SelectCommand("master_node_id", this.txtEquipmentID.Text, this.txtEPC.Text, "0", value);
            string cmd = sc.GetCommand();

            byte[] byteData = Encoding.UTF8.GetBytes(cmd);
            clientSocket.BeginSendTo(byteData, 0, byteData.Length, SocketFlags.None, epServer, null, null);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.sendCommand("A");
        }

        private void btnMaybe_Click(object sender, EventArgs e)
        {
            this.sendCommand("B");
        }

        private void btnNo_Click(object sender, EventArgs e)
        {
            this.sendCommand("C");
        }

        private void btnSelectAnswerOK_Click(object sender, EventArgs e)
        {
            string answer = string.Empty;
            foreach (MetroCheckBox mck in this.checkboxList)
            {
                if (mck.Checked == true)
                {
                    answer += mck.Text;
                }
            }

            this.sendCommand(answer);
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            this.sendCommand("");
        }
    }
}
