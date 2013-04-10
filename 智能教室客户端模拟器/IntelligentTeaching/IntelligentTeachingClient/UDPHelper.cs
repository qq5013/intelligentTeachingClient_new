using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace IntelligentTeachingClient
{
    public interface I_new_msg_arrived
    {
        void handle_new_message_login();
        void handle_new_message_config();
    }
    public class UDPHelper
    {
        static Socket Login_ServerSocket;
        static byte[] byteData = new byte[1024];

        public static I_new_msg_arrived handler = null;
        public static bool stopped = false;

        public static void SendData(string data, EndPoint epRemote)
        {
            try
            {
                //Using UDP sockets
                Socket clientSocket = new Socket(AddressFamily.InterNetwork,
                       SocketType.Dgram, ProtocolType.Udp);
                byte[] byteData = Encoding.UTF8.GetBytes(data);
                clientSocket.BeginSendTo(byteData, 0, byteData.Length, SocketFlags.None, epRemote, null, null);

            }
            catch (Exception ex)
            {
                Debug.WriteLine("SendData => " + ex.Message);
            }
        }
        public static void SendData(string data, string dest_IP, int dest_port)
        {
            try
            {
                //IP address of the server machine
                IPAddress ipAddress = IPAddress.Parse(dest_IP);

                IPEndPoint ipEndPoint = new IPEndPoint(ipAddress, dest_port);
                EndPoint epServer = (EndPoint)ipEndPoint;
                SendData(data, epServer);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("SendData => " + ex.Message);
            }
        }

        /// <summary>
        /// 监听登陆请求信息和教师端IP信息
        /// </summary>
        /// <param name="port"></param>
        public static void StartLogin_UDPServer(int port)
        {
            Login_ServerSocket = createUDPServer(port, new AsyncCallback(OnReceiveLoginRequest));
        }
        static Socket createUDPServer(int port, AsyncCallback callback)
        {
            Socket socket = null;
            try
            {
                //We are using UDP sockets
                socket = new Socket(AddressFamily.InterNetwork,
                    SocketType.Dgram, ProtocolType.Udp);
                IPAddress ip = IPAddress.Parse(GetLocalIP4());
                IPEndPoint ipEndPoint = new IPEndPoint(ip, port);
                socket.Bind(ipEndPoint);
                //防止客户端强行中断造成的异常
                long IOC_IN = 0x80000000;
                long IOC_VENDOR = 0x18000000;
                long SIO_UDP_CONNRESET = IOC_IN | IOC_VENDOR | 12;

                byte[] optionInValue = { Convert.ToByte(false) };
                byte[] optionOutValue = new byte[4];
                socket.IOControl((int)SIO_UDP_CONNRESET, optionInValue, optionOutValue);

                IPEndPoint ipeSender = new IPEndPoint(IPAddress.Any, 0);
                //The epSender identifies the incoming clients
                EndPoint epSender = (EndPoint)ipeSender;

                //Start receiving data
                socket.BeginReceiveFrom(byteData, 0, byteData.Length,
                    SocketFlags.None, ref epSender, callback, epSender);

                Debug.WriteLine("UDP " + ip.ToString() + ":" + port.ToString() + " Listening...");
            }
            catch (Exception ex)
            {
                Debug.WriteLine("createUDPServer => " + ex.Message);
            }
            return socket;
        }
        static void OnReceiveLoginRequest(IAsyncResult ar)
        {
            //接收到的数据格式
            // [id,data,epc,data]  对应的正则表达式  \[id,(?<id>\w+),epc,(?<epc>\w{0,})\]   必须要求有ID存在
            // 教师端IP信息
            // [ip,data,port,data,port_login,data] 正则表达式  \[ip,(?<ip>.{4,}),port,(?<port>\w+),port_login,(?<port_login>\w+)\]
            try
            {
                IPEndPoint ipeSender = new IPEndPoint(IPAddress.Any, 0);
                EndPoint epSender = (EndPoint)ipeSender;

                Login_ServerSocket.EndReceiveFrom(ar, ref epSender);

                string strReceived = Encoding.UTF8.GetString(byteData);

                Array.Clear(byteData, 0, byteData.Length);

                Debug.WriteLine("OnReceiveLoginRequest => " + strReceived);

                string strToBroadcast = string.Empty;
                Regex regexLogin = new Regex(@"\[id,(?<id>\w+),epc,(?<epc>\w{0,}),equipmentID,(?<equipmentID>\w{0,})\]");
                MatchCollection matches = regexLogin.Matches(strReceived);
                foreach (Match mc in matches)
                {
                    string epc_match = mc.Groups["epc"].Value;
                    string id_match = mc.Groups["id"].Value;
                    string equipmentID = mc.Groups["equipmentID"].Value;

                    Debug.WriteLine(string.Format("Match => epc = {0}   student ID = {1}", epc_match, id_match));

                    if ((id_match == GlobalPara.clientID) && epc_match.Length > 0)
                    {
                        GlobalPara.client_epc = epc_match;
                        GlobalPara.equipmentID = equipmentID;
                        if (handler != null)
                        {
                            handler.handle_new_message_login();
                        }
                        break;
                    }
                }

                Regex regexIP = new Regex(@"\[ip,(?<ip>.{4,}),port,(?<port>\w+),port_login,(?<port_login>\w+)\]");
                MatchCollection matchesIP = regexIP.Matches(strReceived);
                foreach (Match mc in matchesIP)
                {
                    string ip = mc.Groups["ip"].Value;
                    string port = mc.Groups["port"].Value;
                    string port_login = mc.Groups["port_login"].Value;
                    Debug.WriteLine(string.Format("Match => ip = {0}   port = {1}  port_login = {2}", ip, port, port_login));

                    GlobalPara.dest_IP = ip;
                    GlobalPara.dest_port = port;
                    GlobalPara.dest_login_port = port_login;
                    if (handler != null)
                    {
                        handler.handle_new_message_config();
                    }
                }
                if (stopped == true)
                {
                    Debug.WriteLine("停止接听广播");
                    return;
                }
                //Start listening to the message send by the user
                Login_ServerSocket.BeginReceiveFrom(byteData, 0, byteData.Length, SocketFlags.None, ref epSender,
                    new AsyncCallback(OnReceiveLoginRequest), epSender);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("OnReceiveRFID => " + ex.Message);
            }

        }

        static string GetLocalIP4()
        {
            IPAddress ipAddress = null;
            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            for (int i = 0; i < ipHostInfo.AddressList.Length; i++)
            {
                ipAddress = ipHostInfo.AddressList[i];
                if (ipAddress.AddressFamily == AddressFamily.InterNetwork)
                {
                    break;
                }
                else
                {
                    ipAddress = null;
                }
            }
            if (null == ipAddress)
            {
                return null;
            }
            return ipAddress.ToString();
        }

    }
}
