using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;

namespace Carbinet
{
    static class Program
    {
        //相当于全局变量 
        public static frmSelect frmSelect = null;
        public static frmFloat frmFloat = null;
        public static frmMain frmMain = null;
        public static frmCheck frmCheck = null;
        public static frmRTTest frmTest = null;
        public static frmClassRoom frmClassRoom = null;
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //frmMain = new frmMain();
            frmClassRoom = new frmClassRoom();
            frmSelect = new frmSelect();
            //frmMainFloat frmStart = new frmMainFloat();
            frmFloat = new frmFloat();
            //Application.Run(frmStart);
            Application.Run(frmFloat);

            //frmCheckInit frmStart = new frmCheckInit();
            //frmCheckStatics frmStart = new frmCheckStatics();
            //Application.Run(frmMain);
            //Application.Run(new Form1());
            //Application.Run(new frmLogin());
            //frmCheck = new frmCheck();
            //frmTest = new frmRTTest();
            //Program.compareString();
        }
        public static void closeAllForms()
        {
            frmClassRoom.Close();
            frmSelect.Close();
        }
        public static void compareString()
        {
            string str1 = "abc";
            string str2 = "ABC";
            string str3 = "bca";
            int i = string.CompareOrdinal(str1, str3);
            Debug.WriteLine(
                string.Format("Program.compareString  -> abc vs bca = {0}"
                , i.ToString()));
        }
    }
}
