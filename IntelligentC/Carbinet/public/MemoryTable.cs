using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Carbinet
{
    public class MemoryTable
    {

        public static bool isInitialized = false;

        public static DataTable studentInfoTable = null;
        public static DataTable mapConfigsTable = null;
        public static DataTable dtRoomConfig = null;

        //static studentInfoCtl stuCtl = new studentInfoCtl();
        //static EquipmentConfigCtl ctl = new EquipmentConfigCtl();
        static roomConfigCtl configCtl = new roomConfigCtl();

        public static void initializeTabes()
        {
            dtRoomConfig = configCtl.getAllRoomConfigInfo();
            dtRoomConfig.Columns.Add("totalColumn", typeof(int));
            dtRoomConfig.Columns["totalColumn"].Expression = "Sum(ICOLUMN)";
            dtRoomConfig.Columns.Add("maxGroup", typeof(int));
            dtRoomConfig.Columns["maxGroup"].Expression = "Max(IGROUP)";

            studentInfoTable = studentInfoCtl.getAllStudentInfo();
            studentInfoTable.CaseSensitive = false;
            studentInfoTable.Columns.Add("status", typeof(string));
            studentInfoTable.Columns.Add("answer", typeof(string));
            studentInfoTable.Columns.Add("checkTime", typeof(string));
            for (int i = 0; i < studentInfoTable.Rows.Count; i++)
            {
                DataRow dr = studentInfoTable.Rows[i];
                dr["status"] = "0";
                dr["answer"] = "";
                dr["checkTime"] = "";
            }

            //获取设备和位置的对应数据
            mapConfigsTable = ctl.getAllMapConfigs();
            mapConfigsTable.Columns.Add("studenID", typeof(string));
            for (int i = 0; i < mapConfigsTable.Rows.Count; i++)
            {
                DataRow dr = mapConfigsTable.Rows[i];
                dr["studenID"] = "";
            }
            isInitialized = true;
        }

    }
}
