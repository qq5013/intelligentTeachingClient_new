using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Diagnostics;


namespace IntelligentTeaching
{

    public partial class SGSClient : Form
    {
        public Socket clientSocket; //The main client socket
        public EndPoint epServer;   //The EndPoint of the server
        public string strName;      //Name by which the user logs into the room

        private byte[] byteData = new byte[1024];

        int question_index = 1;
        bool power_state = false;

        string selected_value = string.Empty;//选项值
        int page_index = 1;//显示选项的页面看

        public SGSClient()
        {
            InitializeComponent();
            this.initialSocket();
        }
        public SGSClient(string rfid, string equip)
            : this()
        {
            this.textBox2.Text = rfid;
            this.textBox1.Text = equip;
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

                //this.clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                //IPAddress ipAddress = IPAddress.Parse(this.txtip.Text);
                //int port = int.Parse(this.txtPort.Text);
                ////Server is listening on port 1000
                //IPEndPoint ipEndPoint = new IPEndPoint(ipAddress, port);

                ////Connect to the server
                //this.clientSocket.BeginConnect(ipEndPoint, new AsyncCallback(OnConnect), null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "客户端", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //Broadcast the message typed by the user to everyone
        private void btnSend_Click(object sender, EventArgs e)
        {
            if (!power_state)
            {
                return;
            }
            SelectCommand sc =
                new SelectCommand(
                "master_node_id",
                 this.textBox1.Text,
                 this.textBox2.Text,
                 this.question_index.ToString(),
                 this.selected_value);
            string cmd = sc.GetCommand();
            updateLogText(cmd);

            byte[] byteData = Encoding.UTF8.GetBytes(cmd);
            clientSocket.BeginSendTo(byteData, 0, byteData.Length, SocketFlags.None, epServer, new AsyncCallback(OnSend), null);

            //this.initialSocket();
            return;
            //try
            //{
            //    //Fill the info for the message to be send
            //    Data msgToSend = new Data();
            //    msgToSend.messageType = "01";
            //    //////////////////////////////////////////////////////////////////////////
            //    int mode = (int)Enum.Parse(typeof(Mode), this.txtInfo.Text, true);
            //    switch (mode)
            //    {
            //        case (int)Mode.单选:

            //            break;
            //        case (int)Mode.多选:

            //            break;
            //        case (int)Mode.考勤:
            //            break;

            //        //case (int)Mode.信息:
            //        //    this.txtMessage.Text += "A";
            //        //    break;
            //    }

            //    //


            //    msgToSend.equipmentID = this.textBox1.Text;
            //    msgToSend.tagID = this.textBox2.Text;

            //    msgToSend.key = mode.ToString();
            //    msgToSend.value = this.txtMessage.Text;
            //    //msgToSend.strName = strName;
            //    //msgToSend.strMessage = txtMessage.Text;
            //    //msgToSend.cmdCommand = Command.Message;

            //    byte[] byteData = msgToSend.ToByte();

            //    //Send it to the server
            //    clientSocket.BeginSend(byteData, 0, byteData.Length, SocketFlags.None, new AsyncCallback(OnSend), null);
            //    //this.txtChatBox.Text += msgToSend.toString() + "\r\n";

            //    txtMessage.Text = null;
            //    Debug.WriteLine("send Data :" + msgToSend.toString());
            //}
            //catch (Exception)
            //{
            //    MessageBox.Show("无法向服务端发送信息", "客户端： " + strName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

        private void OnSend(IAsyncResult ar)
        {
            try
            {
                clientSocket.EndSend(ar);
                //clientSocket.Close();
            }
            catch (ObjectDisposedException)
            { }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "客户端： " + strName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OnReceive(IAsyncResult ar)
        {
            try
            {
                clientSocket.EndReceive(ar);

                Data msgReceived = new Data(byteData);
                //Accordingly process the message received
                switch (msgReceived.cmdCommand)
                {
                    case Command.Login:
                        //lstChatters.Items.Add(msgReceived.strName);
                        break;

                    case Command.Logout:
                        // lstChatters.Items.Remove(msgReceived.strName);
                        break;

                    case Command.Message:
                        break;

                    case Command.List:
                        //lstChatters.Items.AddRange(msgReceived.strMessage.Split('*'));
                        //lstChatters.Items.RemoveAt(lstChatters.Items.Count - 1);
                        txtChatBox.Text += strName + " 加入到聊天室\r\n";
                        break;
                }

                if (msgReceived.strMessage != null && msgReceived.cmdCommand != Command.List)
                    txtChatBox.Text += msgReceived.strMessage + "\r\n";

                byteData = new byte[1024];

                clientSocket.BeginReceive(byteData,
                                          0,
                                          byteData.Length,
                                          SocketFlags.None,
                                          new AsyncCallback(OnReceive),
                                          null);

            }
            catch (ObjectDisposedException)
            { }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "客户端： " + strName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {


            //this.txtInfo.Text = Mode.单选.ToString();

            //this.Text = "客户端： " + strName;

            ////The user has logged into the system so we now request the server to send
            ////the names of all users who are in the chat room
            //Data msgToSend = new Data();
            //msgToSend.cmdCommand = Command.List;
            //msgToSend.strName = strName;
            //msgToSend.strMessage = null;

            //byteData = msgToSend.ToByte();

            //clientSocket.BeginSend(byteData, 0, byteData.Length, SocketFlags.None, new AsyncCallback(OnSend), null);

            //byteData = new byte[1024];
            ////Start listening to the data asynchronously
            //clientSocket.BeginReceive(byteData,
            //                           0,
            //                           byteData.Length,
            //                           SocketFlags.None,
            //                           new AsyncCallback(OnReceive),
            //                           null);

        }

        //private void txtMessage_TextChanged(object sender, EventArgs e)
        //{
        //    if (txtMessage.Text.Length == 0)
        //        btnSend.Enabled = false;
        //    else
        //        btnSend.Enabled = true;
        //}

        private void SGSClient_FormClosing(object sender, FormClosingEventArgs e)
        {
            //if (MessageBox.Show("确定要退出吗？", "客户端： " + strName,
            //    MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No)
            //{
            //    e.Cancel = true;
            //    return;
            //}

            try
            {
                ////Send a message to logout of the server
                //Data msgToSend = new Data();
                //msgToSend.cmdCommand = Command.Logout;
                //msgToSend.strName = strName;
                //msgToSend.strMessage = null;

                //byte[] b = msgToSend.ToByte();
                ////byte[] b = Encoding.UTF8.GetBytes(msgToSend)
                //clientSocket.Send(b, 0, b.Length, SocketFlags.None);
                //clientSocket.Close();
            }
            catch (ObjectDisposedException)
            { }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "客户端： " + strName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtMessage_KeyDown(object sender, KeyEventArgs e)
        {
            if (!power_state)
            {
                return;
            }
            if (e.KeyCode == Keys.Enter)
            {
                btnSend_Click(sender, null);
            }
        }

        private void buttonOpen_Click(object sender, EventArgs e)
        {
            if (this.buttonOpen.Text.IndexOf("打开") >= 0)
            {

                try
                {
                    this.clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                    IPAddress ipAddress = IPAddress.Parse(this.txtip.Text);
                    int port = int.Parse(this.txtPort.Text);
                    //Server is listening on port 1000
                    IPEndPoint ipEndPoint = new IPEndPoint(ipAddress, port);

                    //Connect to the server
                    this.clientSocket.BeginConnect(ipEndPoint, new AsyncCallback(OnConnect), null);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "客户端", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                this.clientSocket.Close();
                this.buttonOpen.Text = "打开";
                this.Text = "未连接服务器";
            }

        }
        private void OnConnect(IAsyncResult ar)
        {
            try
            {
                clientSocket.EndConnect(ar);

                try
                {
                    //Fill the info for the message to be send
                    Data msgToSend = new Data();
                    msgToSend.messageType = "01";
                    //////////////////////////////////////////////////////////////////////////
                    int mode = (int)Enum.Parse(typeof(Mode), this.txtInfo.Text, true);
                    switch (mode)
                    {
                        case (int)Mode.单选:

                            break;
                        case (int)Mode.多选:

                            break;
                        case (int)Mode.考勤:
                            break;

                        //case (int)Mode.信息:
                        //    this.txtMessage.Text += "A";
                        //    break;
                    }

                    //


                    msgToSend.equipmentID = this.textBox1.Text;
                    msgToSend.tagID = this.textBox2.Text;

                    msgToSend.key = mode.ToString();
                    msgToSend.value = this.txtMessage.Text;
                    //msgToSend.strName = strName;
                    //msgToSend.strMessage = txtMessage.Text;
                    //msgToSend.cmdCommand = Command.Message;

                    byte[] byteData = msgToSend.ToByte();

                    //Send it to the server
                    clientSocket.BeginSend(byteData, 0, byteData.Length, SocketFlags.None, new AsyncCallback(OnSend), null);
                    this.Invoke(new deleControlInvoke(this.updateLogText), msgToSend.toString());
                    //this.txtChatBox.Invoke(new deleControlInvoke(this.updateLogText), msgToSend.toString());
                    //this.txtChatBox.Text += msgToSend.toString() + "\r\n";

                    //txtMessage.Text = null;
                    Debug.WriteLine("send Data :" + msgToSend.toString());
                }
                catch (Exception e)
                {
                    MessageBox.Show("无法向服务端发送信息  " + e.Message, "客户端： " + strName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }


                //if (this.InvokeRequired)
                //{
                //    string str = "已连接服务器（" + this.txtip.Text + ":" + this.txtPort.Text + "）";
                //    this.Invoke(new deleControlInvoke(updateFormText), str);
                //}
                //We are connected so we login into the server
                // Data msgToSend = new Data();
                // msgToSend.cmdCommand = Command.Login;
                //msgToSend.strName = txtName.Text;
                //msgToSend.strMessage = null;

                //byte[] b = msgToSend.ToByte();

                //Send the message to the server
                //clientSocket.BeginSend(b, 0, b.Length, SocketFlags.None, new AsyncCallback(OnSend), null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "客户端", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void updateLogText(object str)
        {
            this.txtChatBox.Text = (string)str + "\r\n" + this.txtChatBox.Text;
            //this.txtMessage.Text = null;
        }
        private void updateFormText(object str)
        {
            this.Text = (string)str;
            this.buttonOpen.Text = "关闭";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttona_Click(object sender, EventArgs e)
        {
            this.setMessageContent("A");

        }
        void setMessageContent(string _in)
        {
            if (!power_state)
            {
                return;
            }
            if (_in != null)
            {
                if (this.selected_value.IndexOf(_in) != -1)
                {
                    //当前已经选择了该选项
                    selected_value = selected_value.Remove(selected_value.IndexOf(_in), 1);
                }
                else
                {
                    List<char> list = new List<char>();
                    list.AddRange((selected_value + _in).ToCharArray());
                    list.Sort();
                    selected_value = new string(list.ToArray());
                }
            }
            string msg_current = string.Empty;
            if (selected_value.Length > 0)
            {
                if (this.page_index == 2)
                {
                    //如果已经按过翻页
                    if (selected_value.Length > 5)
                    {
                        msg_current = selected_value.Substring(5);
                    }
                }
                else
                {
                    int length = selected_value.Length;
                    msg_current = selected_value.Substring(0, length < 5 ? length : 5);
                }
            }
            this.txtMessage.Text = msg_current;
            //if (msg_current.IndexOf(_in) != -1)
            //{
            //    //当前已经选择了该选项
            //    msg_current = msg_current.Remove(msg_current.IndexOf(_in), 1);
            //    return;

            //}
            //List<char> list = new List<char>();
            //list.AddRange((msg_current + _in).ToCharArray());
            //list.Sort();
            //this.txtMessage.Text = new string(list.ToArray());

            //int mode = (int)Enum.Parse(typeof(Mode), this.txtInfo.Text, true);
            //switch (mode)
            //{
            //    case (int)Mode.单选:
            //        this.txtMessage.Text = _in;
            //        break;
            //    case (int)Mode.多选:
            //        string str = this.txtMessage.Text;
            //        if (str.IndexOf(_in) == -1)
            //        {
            //            str += _in;
            //            List<char> list = new List<char>();
            //            list.AddRange((str).ToCharArray());
            //            list.Sort();
            //            this.txtMessage.Text = new string(list.ToArray());
            //        }
            //        else
            //        {
            //            //把 A 删除

            //            this.txtMessage.Text = str.Replace(_in, "");
            //        }
            //        break;
            //case (int)Mode.信息:
            //    this.txtMessage.Text += "A";
            //    break;
            //}
        }
        private void buttonb_Click(object sender, EventArgs e)
        {
            this.setMessageContent("B");
            //this.txtMessage.Text = "B";
        }

        private void buttonc_Click(object sender, EventArgs e)
        {
            this.setMessageContent("C");
            //this.txtMessage.Text = "C";
        }

        private void buttond_Click(object sender, EventArgs e)
        {
            this.setMessageContent("D");
            //            this.txtMessage.Text = "D";

        }

        // <
        private void button2_Click(object sender, EventArgs e)
        {
            this.page_index = 1;
            this.setMessageContent(null);
            //this.txtMessage.Text = null;
            //int mode = (int)Enum.Parse(typeof(Mode), this.txtMode.Text, true);
            //if (mode < (int)Mode.考勤)
            //{
            //    mode++;
            //}
            //else
            //{
            //    mode = (int)Mode.单选;
            //}
            //this.txtMode.Text = Enum.GetName(typeof(Mode), mode);

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.page_index = 2;
            this.setMessageContent(null);
            //this.txtMessage.Text = null;
            //int mode = (int)Enum.Parse(typeof(Mode), this.txtMode.Text, true);
            //if (mode > (int)Mode.单选)
            //{
            //    mode--;
            //}
            //else
            //{
            //    mode = (int)Mode.考勤;
            //}
            //this.txtMode.Text = Enum.GetName(typeof(Mode), mode);

        }

        private void btnPower_Click(object sender, EventArgs e)
        {
            if (!power_state)
            {
                this.txtInfo.Text = "电量：100%";
                power_state = true;
                this.txtQuestionIndex.Text = this.question_index.ToString("D3");
                return;
            }
            else
            {
                this.txtInfo.Text = string.Empty;
                power_state = false;
                this.txtMessage.Text = string.Empty;
                this.txtQuestionIndex.Text = string.Empty;
                this.question_index = 1;
                this.selected_value = string.Empty;
            }

        }

        private void buttonUP_Click(object sender, EventArgs e)
        {
            if (!power_state)
            {
                return;
            }
            if (this.question_index > 1)
            {
                this.question_index--;
                this.txtQuestionIndex.Text = this.question_index.ToString("D3");
            }
        }

        private void buttonDown_Click(object sender, EventArgs e)
        {
            if (!power_state)
            {
                return;
            }
            this.question_index++;
            this.txtQuestionIndex.Text = this.question_index.ToString("D3");
        }

        private void buttonE_Click(object sender, EventArgs e)
        {
            this.setMessageContent("E");

        }

        private void btnF_Click(object sender, EventArgs e)
        {
            this.setMessageContent("F");

        }

        private void btnG_Click(object sender, EventArgs e)
        {
            this.setMessageContent("G");

        }

        private void btnH_Click(object sender, EventArgs e)
        {
            this.setMessageContent("H");

        }

        private void btnI_Click(object sender, EventArgs e)
        {
            this.setMessageContent("I");

        }

        private void btnJ_Click(object sender, EventArgs e)
        {
            this.setMessageContent("J");

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            this.txtChatBox.Text = string.Empty;
        }

        //private void txtMode_TextChanged(object sender, EventArgs e)
        //{
        //    if (this.txtMode.Text == "考勤")
        //    {
        //        this.txtMessage.Text = "考勤";
        //    }
        //}

    }


}