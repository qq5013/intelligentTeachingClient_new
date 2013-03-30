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
            DataRow[] rows = studentInfoTable.Select(string.Format("STUDENTID = '{0}'", _epc));
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

        public static void clearEquipmentAndStudentCombining(string id)
        {
            DataRow[] rowsMap = mapConfigsTable.Select("studenID = '" + id + "'");
            if (rowsMap.Length > 0)
            {
                for (int i = 0; i < rowsMap.Length; i++)
                {
                    DataRow dr = rowsMap[i];
                    dr["studenID"] = "";
                }
            }
        }

        public static string getPersonIDByPosition(int group, int row, int column)
        {
            string id = null;
            DataRow[] rowsMap = mapConfigsTable.Select(string.Format("IGROUP = {0} and IROW = {1} and ICOLUMN = {2}", group, row, column));
            if (rowsMap.Length > 0)
            {
                DataRow dr = rowsMap[0];
                id = (string)dr["studenID"];
            }
            return id;
        }

        public static void setEquipmentInfoCombineStudentID(equipmentPosition pos, string id)
        {
            DataRow[] rowsMap = mapConfigsTable.Select(string.Format("IGROUP = {0} and IROW = {1} and ICOLUMN = {2}", pos.group, pos.row, pos.column));
            if (rowsMap.Length > 0)
            {
                DataRow dr = rowsMap[0];
                dr["studenID"] = id;
            }
        }

        public static equipmentPosition getEquipmentInfoByStudentID(string id)
        {
            DataRow[] rowsMap = mapConfigsTable.Select("studenID = '" + id + "'");
            if (rowsMap.Length > 0)
            {
                DataRow dr = rowsMap[0];
                equipmentPosition ep = new equipmentPosition((string)dr["EQUIPEMNTID"], (int)dr["IGROUP"], (int)dr["IROW"], (int)dr["ICOLUMN"]);
                return ep;
            }
            return null;
        }

        public static equipmentPosition getEquipmentConfigMapInfo(string remoteDeviceID)
        {
            DataRow[] rowsMap = mapConfigsTable.Select("EQUIPEMNTID = '" + remoteDeviceID + "'");
            if (rowsMap.Length > 0)
            {
                DataRow dr = rowsMap[0];
                equipmentPosition ep = new equipmentPosition(remoteDeviceID, (int)dr["IGROUP"], (int)dr["IROW"], (int)dr["ICOLUMN"]);
                return ep;
            }
            return null;
        }

        public static void initializeTabes()
        {
            if (isInitialized) return;
            //教室座位
            dtRoomConfig = roomConfigCtl.getAllRoomConfigInfo();
            //dtRoomConfig.Columns.Add("totalColumn", typeof(int));
            //dtRoomConfig.Columns["totalColumn"].Expression = "Sum(ICOLUMN)";
            //dtRoomConfig.Columns.Add("maxGroup", typeof(int));
            //dtRoomConfig.Columns["maxGroup"].Expression = "Max(IGROUP)";

            //学生信息
            studentInfoTable = studentInfoCtl.getAllStudentInfo();
            studentInfoTable.CaseSensitive = false;

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
