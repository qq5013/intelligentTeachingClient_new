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

        public static Person getPersonByEpc(string _epc)
        {
            DataRow[] rows = studentInfoTable.Select(string.Format("epc = '{0}'", _epc));
            if (rows.Length > 0)
            {
                DataRow dr = rows[0];
                return new Person(dr["STUDENTID"] as string, dr["NAME"] as string,
                    dr["SEX"] as string, (int)dr["AGE"], dr["EMAIL"] as string,
                    dr["CLASS_NAME"] as string, _epc);

            }
            else
            {
                return null;
            }
        }
        public static void initializeTabes()
        {
            if (isInitialized) return;
            //教室座位
            dtRoomConfig = roomConfigCtl.getAllRoomConfigInfo();
            dtRoomConfig.Columns.Add("totalColumn", typeof(int));
            dtRoomConfig.Columns["totalColumn"].Expression = "Sum(ICOLUMN)";
            dtRoomConfig.Columns.Add("maxGroup", typeof(int));
            dtRoomConfig.Columns["maxGroup"].Expression = "Max(IGROUP)";

            //学生信息
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
            mapConfigsTable = EquipmentConfigCtl.getAllMapConfigs();
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
