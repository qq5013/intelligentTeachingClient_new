using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;
using intelligentMiddleWare;
using System.Drawing;

namespace Carbinet
{
    static class Program
    {
        //相当于全局变量 
        //public static frmSelect frmSelect = null;
        public static frmFloat frmFloat = null;
        public static frmRTTest frmTest = null;
        public static frmClassRoom frmClassRoom = null;
        public static frmEquipmentConfig frmClassRoomConfig = null;
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            MemoryTable.initializeTabes();
            frmClassRoom = new frmClassRoom();
            frmFloat = new frmFloat();
            StaticDataPort.openDataPort();
            Application.Run(frmFloat);

            //Application.Run(new frmTest());

            //frmMainFloat frmStart = new frmMainFloat();
            //Application.Run(frmStart);
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
            if (frmClassRoomConfig != null)
            {
                frmClassRoomConfig.Close();
            }
            //frmSelect.Close();
        }

        public static void setFloatFormPos()
        {
            Screen[] screens = System.Windows.Forms.Screen.AllScreens;
            for (int i = 0; i < screens.Length; i++)
            {
                Screen sc = screens[i];
                if (sc.Primary == true)
                {
                    Rectangle rect = sc.WorkingArea;
                    frmFloat.Height = rect.Height;
                    frmFloat.Left = (int)(rect.Width - frmFloat.Width);
                    //this.Left = (int)(rect.Width * 0.7);
                    frmFloat.Top = (int)(rect.Height * 0.0);
                }
            }
        }

        public static void compareString()
        {
            string str1 = "abc";
            string str3 = "bca";
            int i = string.CompareOrdinal(str1, str3);
            Debug.WriteLine(
                string.Format("Program.compareString  -> abc vs bca = {0}"
                , i.ToString()));
        }
    }
}
