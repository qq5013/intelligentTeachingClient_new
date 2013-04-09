using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;

namespace Carbinet
{
    public class LoginManager
    {
        static List<string> epcList = new List<string>();
        static Socket RFID_ServerSocket;
        static Socket Login_ServerSocket;
        static byte[] byteData = new byte[1024];

        public static void Broadcast(string data)
        {
            Broadcast(data, 15000);
        }
        public static void Broadcast(string data, int port)
        {
            Debug.WriteLine("Broadcast => " + data);

            Socket broadcastSocket = new Socket(AddressFamily.InterNetwork,
               SocketType.Dgram, ProtocolType.Udp);
            broadcastSocket.EnableBroadcast = true;
            IPEndPoint iep = new IPEndPoint(IPAddress.Broadcast, port);
            byte[] byteData = System.Text.Encoding.UTF8.GetBytes(data);
            try
            {
                broadcastSocket.BeginSendTo(byteData, 0,
                                            byteData.Length, SocketFlags.None,
                                            iep, null, null);
            }
            catch (System.Exception ex)
            {
                Debug.WriteLine("Broadcast => " + ex.Message);
            }
        }

        /// <summary>
        /// 监听登陆请求信息
        /// </summary>
        /// <param name="port"></param>
        public static void StartLogin_UDPServer(int port)
        {
            Login_ServerSocket = createUDPServer(port, new AsyncCallback(OnReceiveLoginRequest));
        }

        /// <summary>
        /// 监听RFID刷卡信息
        /// </summary>
        /// <param name="port">使用的端口</param>
        public static void StartRFID_UDPServer(int port)
        {
            RFID_ServerSocket = createUDPServer(port, new AsyncCallback(OnReceiveRFID));
            //try
            //{
            //    //We are using UDP sockets
            //    serverSocket = new Socket(AddressFamily.InterNetwork,
            //        SocketType.Dgram, ProtocolType.Udp);
            //    IPAddress ip = IPAddress.Parse(GetLocalIP4());
            //    IPEndPoint ipEndPoint = new IPEndPoint(ip, port);
            //    serverSocket.Bind(ipEndPoint);
            //    //防止客户端强行中断造成的异常
            //    long IOC_IN = 0x80000000;
            //    long IOC_VENDOR = 0x18000000;
            //    long SIO_UDP_CONNRESET = IOC_IN | IOC_VENDOR | 12;

            //    byte[] optionInValue = { Convert.ToByte(false) };
            //    byte[] optionOutValue = new byte[4];
            //    serverSocket.IOControl((int)SIO_UDP_CONNRESET, optionInValue, optionOutValue);

            //    IPEndPoint ipeSender = new IPEndPoint(IPAddress.Any, 0);
            //    //The epSender identifies the incoming clients
            //    EndPoint epSender = (EndPoint)ipeSender;

            //    //Start receiving data
            //    serverSocket.BeginReceiveFrom(byteData, 0, byteData.Length,
            //        SocketFlags.None, ref epSender, new AsyncCallback(OnReceiveRFID), epSender);

            //    Debug.WriteLine("UDP " + ip.ToString() + ":" + port.ToString() + " Listening...");
            //}
            //catch (Exception ex)
            //{
            //    Debug.WriteLine("StartUDPServer => " + ex.Message);
            //}
        }
        static void OnReceiveLoginRequest(IAsyncResult ar)
        {
            //接收到客户端的登陆请求
            //请求中带有客户端输入的学生ID，本服务端需要和学生信息进行匹配，
            //首先学号查找与学号对应的卡号，然后查找卡号是否已经读取到，如果已经读取到，则广播发送登陆信息给客户端
            //如果卡号尚未读取到，则客户端可以等待，然后读取到卡号的时候会发送登陆信息
            //接收到的数据格式
            // [id,data,epc,data]  对应的正则表达式  \[id,(?<id>\w+),epc,(?<epc>\w{0,})\]   必须要求有ID存在
            try
            {
                IPEndPoint ipeSender = new IPEndPoint(IPAddress.Any, 0);
                EndPoint epSender = (EndPoint)ipeSender;

                Login_ServerSocket.EndReceiveFrom(ar, ref epSender);

                string strReceived = Encoding.UTF8.GetString(byteData);

                Array.Clear(byteData, 0, byteData.Length);
                Debug.WriteLine("OnReceiveLoginRequest => " + strReceived);

                string strToBroadcast = string.Empty;
                Regex regex = new Regex(@"\[id,(?<id>\w+),epc,(?<epc>\w{0,})\]");
                MatchCollection matches = regex.Matches(strReceived);
                foreach (Match mc in matches)
                {
                    string epc_match = mc.Groups["epc"].Value;
                    string id_match = mc.Groups["id"].Value;
                    Debug.WriteLine(string.Format("Match => epc = {0}   student ID = {1}", epc_match, id_match));
                    //查找是否已经读取到卡号
                    if (epcList.Contains(epc_match))
                    {
                        Person person = MemoryTable.getPersonByEpc(epc_match);
                        if (person != null)
                        {
                            strToBroadcast = string.Format("[id,{0},epc,{1}]", person.id_num, epc_match);
                            Broadcast(strToBroadcast);
                        }
                    }
                }

                //先假设接收到的都是卡号
                //查找学生信息里面有没有该卡号，如果有的话，广播卡号和学号，可以附带未使用的设备号
                //格式为 [epc,id,equipmentID]

                //Start listening to the message send by the user
                Login_ServerSocket.BeginReceiveFrom(byteData, 0, byteData.Length, SocketFlags.None, ref epSender,
                    new AsyncCallback(OnReceiveLoginRequest), epSender);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("OnReceiveRFID => " + ex.Message);
            }

        }
        static void OnReceiveRFID(IAsyncResult ar)
        {
            try
            {
                IPEndPoint ipeSender = new IPEndPoint(IPAddress.Any, 0);
                EndPoint epSender = (EndPoint)ipeSender;

                RFID_ServerSocket.EndReceiveFrom(ar, ref epSender);

                string strReceived = Encoding.UTF8.GetString(byteData);

                Array.Clear(byteData, 0, byteData.Length);
                Debug.WriteLine("OnReceiveRFID => " + strReceived);
                //先假设接收到的都是卡号
                //查找学生信息里面有没有该卡号，如果有的话，广播卡号和学号，可以附带未使用的设备号
                //格式为 [epc,id,equipmentID]
                string epc = strReceived.Substring(0, strReceived.IndexOf("\0"));
                epcList.Add(epc);

                string strToBroadcast = string.Empty;
                Person person = MemoryTable.getPersonByEpc(epc);
                if (person != null)
                {
                    strToBroadcast = string.Format("[id,{0},epc,{1}]", person.id_num, epc);
                    Broadcast(strToBroadcast);

                    //Regex regex = new Regex(@"\[(?<epc>\w+),(?<id>\w+)\]");
                    //MatchCollection matches = regex.Matches(strToBroadcast);
                    //foreach (Match mc in matches)
                    //{
                    //    string epc_match = mc.Groups["epc"].Value;
                    //    string id_match = mc.Groups["id"].Value;
                    //    Debug.WriteLine(string.Format("Match => epc = {0}   student ID = {1}", epc_match, id_match));
                    //}
                    //equipmentPosition ep = MemoryTable.getEquipmentInfoByEpc(epc);
                    //if (ep == null)//学生尚未绑定座位位置，为其选择一个空的位置
                    //{

                    //}
                    //else
                    //{
                    //    strToBroadcast = string.Format("[{0},{1}]", epc, person.id_num);
                    //}
                }
                //Start listening to the message send by the user
                RFID_ServerSocket.BeginReceiveFrom(byteData, 0, byteData.Length, SocketFlags.None, ref epSender,
                    new AsyncCallback(OnReceiveRFID), epSender);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("OnReceiveRFID => " + ex.Message);
            }
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
