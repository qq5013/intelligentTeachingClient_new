using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Diagnostics;

namespace Carbinet
{
    public delegate void deleNewDataReceived(Data data);
    public class commonSocket
    {
        public static Socket serverSocket = null;
        public static bool bOpen = false;
        public static byte[] byteData = null;
        public static deleNewDataReceived eventDataReived;
        public static bool isOpen()
        {
            return bOpen;
        }
        public static void StartListen()
        {
            if (isOpen())
            {
                return;
            }

            int portNum = 13000;

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
                return;
            }
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, portNum);
            //We are using TCP sockets
            serverSocket = new Socket(AddressFamily.InterNetwork,
                                      SocketType.Stream,
                                      ProtocolType.Tcp);

            //Assign the any IP of the machine and listen on port number 1000
            //IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Any, portNum);

            //Bind and listen on the given address
            serverSocket.Bind(localEndPoint);
            serverSocket.Listen(32);

            //Accept the incoming clients
            serverSocket.BeginAccept(new AsyncCallback(OnAccept), null);
            bOpen = true;
        }
        public static void OnAccept(IAsyncResult ar)
        {
            try
            {
                if (null != serverSocket)
                {
                    Socket clientSocket = serverSocket.EndAccept(ar);

                    //Start listening for more clients
                    serverSocket.BeginAccept(new AsyncCallback(OnAccept), null);
                    byteData = new byte[1024];
                    //Once the client connects then start receiving the commands from her
                    //todo maybe 1024 is not enough
                    clientSocket.BeginReceive(byteData, 0, byteData.Length, SocketFlags.None,
                        new AsyncCallback(OnReceive), clientSocket);
                }
            }
            catch (Exception ex)
            {
#if TRACE
                Debug.WriteLine("footprint -> commonSocket.OnAccept " + ex.Message);
#endif
            }
        }
        public static void OnReceive(IAsyncResult ar)
        {
            try
            {
                Socket clientSocket = (Socket)ar.AsyncState;
                clientSocket.EndReceive(ar);

                //Transform the array of bytes received from the user into an
                //intelligent form of object Data
                Data msgReceived = new Data(byteData);
                clientSocket.Close();
                // to do something here with received data
                Debug.WriteLine(
                    string.Format("commonSocket.OnReceive  -> msgReceived = {0}"
                    , msgReceived.toString()));
                if (eventDataReived != null)
                {
                    eventDataReived(msgReceived);
                }
                byteData = null;
                //clientSocket.BeginReceive(byteData, 0, byteData.Length, SocketFlags.None, new AsyncCallback(OnReceive), clientSocket);
            }
            catch (System.Exception ex)
            {
#if TRACE
                Debug.WriteLine("footprint -> commonSocket.OnReceive " + ex.Message);
#endif
                //MessageBox.Show(ex.Message, "服务器端", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
