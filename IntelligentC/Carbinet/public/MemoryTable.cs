using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Newtonsoft.Json;

namespace Carbinet
{
    public class MemoryTable
    {
        public static bool isInitialized = false;
        public static DataTable studentInfoTable = null;
        public static DataTable mapConfigsTable = null;
        public static DataTable dtRoomConfig = null;

        public static void setEquipmentAndPosCombining(int group, int row, int column, string deviceID)
        {
            DataRow[] rowsMap = mapConfigsTable.Select(string.Format("IGROUP = {0} and IROW = {1} and ICOLUMN = {2}", group, row, column));
            if (rowsMap.Length > 0)
            {
                DataRow dr = rowsMap[0];
                dr["EQUIPEMNTID"] = deviceID;
            }
        }

        public static Person getPersonByID(string _id)
        {
            DataRow[] rows = studentInfoTable.Select(string.Format("STUDENTID = '{0}'", _id));
            if (rows.Length > 0)
            {
                DataRow dr = rows[0];
                return new Person(_id, dr["NAME"] as string,
                    dr["SEX"] as string, (int)dr["AGE"], dr["EMAIL"] as string,
                    dr["CLASS_NAME"] as string, dr["epc"] as string);
            }
            else
            {
                return null;
            }
        }
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

        public static void clearEquipmentAndStudentCombining(string epc)
        {
            DataRow[] rowsMap = mapConfigsTable.Select("epc = '" + epc + "'");
            if (rowsMap.Length > 0)
            {
                for (int i = 0; i < rowsMap.Length; i++)
                {
                    DataRow dr = rowsMap[i];
                    dr["epc"] = "";
                }
            }
        }

        public static string getPersonEpcByPosition(int group, int row, int column)
        {
            string epc = null;
            DataRow[] rowsMap = mapConfigsTable.Select(string.Format("IGROUP = {0} and IROW = {1} and ICOLUMN = {2}", group, row, column));
            if (rowsMap.Length > 0)
            {
                DataRow dr = rowsMap[0];
                epc = (string)dr["epc"];
            }
            return epc;
        }

        public static void setEquipmentInfoCombineStudentID(equipmentPosition pos, string epc)
        {
            DataRow[] rowsMap = mapConfigsTable.Select(string.Format("IGROUP = {0} and IROW = {1} and ICOLUMN = {2}", pos.group, pos.row, pos.column));
            if (rowsMap.Length > 0)
            {
                DataRow dr = rowsMap[0];
                dr["epc"] = epc;
            }
        }

        #region 获取设备与位置绑定信息
        public static equipmentPosition getEquipmentInfoByEpc(string epc)
        {
            DataRow[] rowsMap = mapConfigsTable.Select("epc = '" + epc + "'");
            if (rowsMap.Length > 0)
            {
                DataRow dr = rowsMap[0];
                equipmentPosition ep = new equipmentPosition((string)dr["EQUIPEMNTID"], (int)dr["IGROUP"], (int)dr["IROW"], (int)dr["ICOLUMN"]);
                return ep;
            }
            return null;
        }

        public static equipmentPosition getEquipmentConfigMapInfoByPos(int group, int row, int column)
        {
            DataRow[] rowsMap = mapConfigsTable.Select(string.Format("IGROUP = {0} and IROW = {1} and ICOLUMN = {2}", group, row, column));
            if (rowsMap.Length > 0)
            {
                DataRow dr = rowsMap[0];
                equipmentPosition ep = new equipmentPosition((string)dr["EQUIPEMNTID"], group, row, column);
                return ep;
            }
            return null;
        }
        public static equipmentPosition getEquipmentConfigMapInfoByDeviceID(string remoteDeviceID)
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
        public static List<equipmentPosition> getAllEquipmentConfigMapInfo()
        {
            List<equipmentPosition> list = new List<equipmentPosition>();
            int count = mapConfigsTable.Rows.Count;
            for (int i = 0; i < count; i++)
            {
                DataRow dr = mapConfigsTable.Rows[i];
                equipmentPosition ep = new equipmentPosition((string)dr["EQUIPEMNTID"], (int)dr["IGROUP"], (int)dr["IROW"], (int)dr["ICOLUMN"]);
                list.Add(ep);
            }
            return list;
        }
        #endregion

        #region 教室配置
        public static void refreshGroupCount(int _new_count)
        {
            if (_new_count <= 0) return;//最小为1
            int new_group_index = _new_count - 1;
            int groupCount = dtRoomConfig.Rows.Count;
            if (groupCount > 0)
            {
                int current_max_group_index = (int)dtRoomConfig.Rows[groupCount - 1]["IGROUP"];//最大的排在最后，sql里定义
                if (new_group_index > current_max_group_index)//添加新group，注意 group以0开始
                {
                    dtRoomConfig.Rows.Add(new object[] { _new_count - 1, 1, 1 });
                }
                else if (new_group_index < current_max_group_index)
                {
                    DataRow[] rows = dtRoomConfig.Select(string.Format("IGROUP > {0}", new_group_index));
                    for (int i = 0; i < rows.Length; i++)
                    {
                        dtRoomConfig.Rows.Remove(rows[i]);
                    }
                }
            }
        }

        public static void refreshGroupRowCount(int _groupName, int _rowsCount)
        {
            DataRow[] rows = dtRoomConfig.Select("IGROUP = " + _groupName);
            if (rows.Length > 0)
            {
                rows[0]["IROW"] = _rowsCount;
            }
        }

        public static void refreshGroupColumnCount(int _groupName, int _ColumnCount)
        {
            DataRow[] rows = dtRoomConfig.Select("IGROUP = " + _groupName);
            if (rows.Length > 0)
            {
                rows[0]["ICOLUMN"] = _ColumnCount;
            }
        }

        #endregion

        public static void initializeTabes()
        {
            if (isInitialized) return;
            //教室座位
            dtRoomConfig = roomConfigCtl.getAllRoomConfigInfo();

            //学生信息
            studentInfoTable = studentInfoCtl.getAllStudentInfo();
            studentInfoTable.CaseSensitive = false;

            //获取设备和位置的对应数据
            mapConfigsTable = EquipmentConfigCtl.getAllMapConfigs();

            mapConfigsTable.Columns.Add("epc", typeof(string));
            for (int i = 0; i < mapConfigsTable.Rows.Count; i++)
            {
                DataRow dr = mapConfigsTable.Rows[i];
                dr["epc"] = "";
            }
            isInitialized = true;
        }
        public static string getStudentInfoJson()
        {
            List<Person> list = new List<Person>();
            int totalCount = studentInfoTable.Rows.Count;
            for (int i = 0; i < totalCount; i++)
            {
                DataRow dr = studentInfoTable.Rows[i];
                Person person = 
                    new Person((string)dr["STUDENTID"],(string)dr["NAME"],(string)dr["SEX"],(int)dr["AGE"],(string)dr["EMAIL"],(string)dr["CLASS_NAME"],(string)dr["epc"]);
                list.Add(person);
            }
            return JsonConvert.SerializeObject(list);
        }
        public static string getEquipmentMapJson()
        {
            List<equipmentPosition> list = new List<equipmentPosition>();
            int totalCount = mapConfigsTable.Rows.Count;
            for (int i = 0; i < totalCount; i++)
            {
                DataRow dr = mapConfigsTable.Rows[i];
                equipmentPosition ep = new equipmentPosition((string)dr["EQUIPEMNTID"], (int)dr["IGROUP"], (int)dr["IROW"], (int)dr["ICOLUMN"]);
                list.Add(ep);
            }
            return JsonConvert.SerializeObject(list);
        }
        public static string getRoomConfigJson()
        {
            List<RoomConfig> list = new List<RoomConfig>();
            int totalCount = dtRoomConfig.Rows.Count;
            for (int i = 0; i < totalCount; i++)
            {
                DataRow dr = dtRoomConfig.Rows[i];
                RoomConfig rc = new RoomConfig((int)dr["IGROUP"], (int)dr["IROW"], (int)dr["ICOLUMN"]);
                list.Add(rc);
            }

            return JsonConvert.SerializeObject(list);
        }
    }
}
