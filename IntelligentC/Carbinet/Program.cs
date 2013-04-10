using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;
using intelligentMiddleWare;
using System.Drawing;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

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
            importData();
            MemoryTable.initializeTabes();
            frmClassRoom = new frmClassRoom();
            frmFloat = new frmFloat();
            StaticDataPort.openDataPort(5000);//普通的系统数据交互端口
            LoginManager.StartRFID_UDPServer(5001);//读卡器传送数据端口
            LoginManager.StartLogin_UDPServer(5002);//登陆端口
            LoginManager.start_broadcasting_config(5000, 5002);

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

        static void importData()
        { 
            roomConfigCtl.clearRoomConfigOfDB();
            //教室配置
            string strReadFilePath1 = @"./config/roomConfig.txt";
            StreamReader srReadFile1 = new StreamReader(strReadFilePath1);
            string roomConfig = srReadFile1.ReadToEnd();
            //string roomConfig = "[{\"group\":0,\"row\":3,\"column\":1},{\"group\":1,\"row\":2,\"column\":2},{\"group\":2,\"row\":3,\"column\":1}]";
            Debug.WriteLine(roomConfig);
            List<RoomConfig> list = (List<RoomConfig>)JsonConvert.DeserializeObject<List<RoomConfig>>(roomConfig);
            roomConfigCtl.AddNewConfig(list);

            EquipmentConfigCtl.clearEquipmentMapOfDB();
            //设备位置映射
            string strReadFilePath2 = @"./config/equipmentMaps.txt";
            StreamReader srReadFile2 = new StreamReader(strReadFilePath2);
            string equipmentMaps = srReadFile2.ReadToEnd();
            //string equipmentMaps = "[{\"equipmentID\":\"equip000001\",\"group\":0,\"row\":1,\"column\":1},{\"equipmentID\":\"equip000002\",\"group\":0,\"row\":2,\"column\":1},{\"equipmentID\":\"equip000004\",\"group\":1,\"row\":1,\"column\":1},{\"equipmentID\":\"equip000006\",\"group\":1,\"row\":1,\"column\":2},{\"equipmentID\":\"equip000005\",\"group\":1,\"row\":2,\"column\":1},{\"equipmentID\":\"equip000007\",\"group\":1,\"row\":2,\"column\":2},{\"equipmentID\":\"equip000008\",\"group\":2,\"row\":1,\"column\":1},{\"equipmentID\":\"equip000009\",\"group\":2,\"row\":2,\"column\":1}]";
            List<equipmentPosition> listMap = (List<equipmentPosition>)JsonConvert.DeserializeObject<List<equipmentPosition>>(equipmentMaps);
            EquipmentConfigCtl.AddMapConfig(listMap);

            //学生基本信息，客户端支持更改的只有绑定的学生卡
            studentInfoCtl.clearStudentInfo();
            string strReadFilePath3 = @"./config/Person.txt";
            StreamReader srReadFile3 = new StreamReader(strReadFilePath3);
            string Person = srReadFile3.ReadToEnd();
            //string Person = "[{\"id_num\":\"CE4D939787\",\"name\":\"李俊\",\"sex\":\"男\",\"email\":\"111\",\"age\":11,\"bj\":\"一班\",\"epc\":\"stu000002\"},{\"id_num\":\"CE4D9397871\",\"name\":\"李韬\",\"sex\":\"\",\"email\":\"111\",\"age\":1,\"bj\":\"二班\",\"epc\":\"stu000001\"}]";
            List<Person> personList = (List<Person>)JsonConvert.DeserializeObject<List<Person>>(Person);
            studentInfoCtl.addStudentInfo(personList);        
        }
        public static void exportData()
        {
            //教室配置
            string roomConfig = MemoryTable.getRoomConfigJson();
            string strReadFilePath1 = @"./config/roomConfig.txt";
            StreamWriter srWriteFile1 = new StreamWriter(strReadFilePath1);
            srWriteFile1.Write(roomConfig);
            srWriteFile1.Close();

            //设备位置映射
            EquipmentConfigCtl.clearEquipmentMapOfDB();
            string strReadFilePath2 = @"./config/equipmentMaps.txt";
            string equiMap = MemoryTable.getEquipmentMapJson();
            StreamWriter srWriteFile2 = new StreamWriter(strReadFilePath2);
            srWriteFile2.Write(equiMap);
            srWriteFile2.Close();

            //学生基本信息，客户端支持更改的只有绑定的学生卡
            string strReadFilePath3 = @"./config/Person.txt";
            string studentInfo = MemoryTable.getStudentInfoJson();
            StreamWriter srWriteFile3 = new StreamWriter(strReadFilePath3);
            srWriteFile3.Write(studentInfo);
            srWriteFile3.Close();
        }
        public static void testJson()
        {
            //string roomConfig = MemoryTable.getRoomConfigJson();
            //Debug.WriteLine(MemoryTable.getEquipmentMapJson());
            //Debug.WriteLine(MemoryTable.getStudentInfoJson());
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
