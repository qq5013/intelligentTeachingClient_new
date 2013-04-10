using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;
using IntelligentTeachingClient;

namespace IntelligentTeachingClient
{
    public class GlobalPara
    {
        public static string dest_IP = string.Empty;
        public static string dest_port = string.Empty;
        public static string client_epc = string.Empty;
        public static string dest_login_port = string.Empty;

        public static string clientID = string.Empty;
        public static string equipmentID = string.Empty;
    }
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new frmParent());
            //Application.Run(new SGSClient());
            frmLogin = new frmLogin();
            Application.Run(frmLogin);


            //frmParent fp = new frmParent();

            // login frmLogin = new login(fp);
            //fp._frm = frmLogin;

            // Application.Run(fp);


            //Program.Test1();
        }
        public static frmLogin frmLogin = null;
        static void Test1()
        {
            string s1 = "dc";
            string s2 = "ba";
            List<char> list = new List<char>();
            list.AddRange((s1 + s2).ToCharArray());
            list.Sort();
            Debug.Write(new string(list.ToArray()));
        }
    }
}
