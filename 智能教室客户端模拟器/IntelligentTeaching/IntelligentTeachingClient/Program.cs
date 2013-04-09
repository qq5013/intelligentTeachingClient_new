using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;
using IntelligentTeachingClient;

namespace IntelligentTeaching
{
    public class GlobalPara
    {
        public static string dest_IP = "127.0.0.1";
        public static string dest_port = "5000";
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
            Application.Run(new main_form());


           //frmParent fp = new frmParent();
            
           // login frmLogin = new login(fp);
            //fp._frm = frmLogin;

           // Application.Run(fp);


            //Program.Test1();
        }

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
