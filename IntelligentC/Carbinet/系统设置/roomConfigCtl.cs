using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Windows.Forms;

namespace Carbinet
{
    public class roomConfigCtl
    {
        public static string sqlSelect_allRoomConfig =
            @"SELECT IGROUP,IROW,ICOLUMN from T_ROOM_CONFIG order by IGROUP asc;";
        public static string sqlSelect_SpecifiedRoomConfig =
            @"SELECT IGROUP,IROW,ICOLUMN from T_ROOM_CONFIG  where IGROUP = {0} ;";

        public static string sqlUpdate_updateRoomConfigRow =
            @"update T_ROOM_CONFIG set IROW = {0} where IGROUP = {1}";
        public static string sqlUpdate_updateRoomConfigColumn =
             @"update T_ROOM_CONFIG set ICOLUMN = {0} where IGROUP = {1}";
        public static string sqlInsert_addConfig =
            @"insert into T_ROOM_CONFIG(IGROUP,IROW,ICOLUMN) values({0},{1},{2})";

        public bool AddNewConfig(int group, int row, int column)
        {
            try
            {
                int result = int.Parse(CsharpSQLiteHelper.ExecuteNonQuery(
                                             sqlInsert_addConfig
                                             , new object[3] { group, row, column }).ToString());
                if (result > 0)
                {
                    return true;
                }
            }
            catch (System.Exception ex)
            {

                MessageBox.Show("更新数据时出现错误：" + ex.Message);
            }
            return false;
        }

        public bool ConfigExists(int group)
        {
            try
            {
                DataTable dt = CsharpSQLiteHelper.ExecuteTable(sqlSelect_SpecifiedRoomConfig, new object[1] { group });
                if (dt.Rows.Count > 0)
                {
                    return true;
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("查询数据库时出现错误：" + ex.Message);
            }
            return false;
        }
        /// <summary>
        /// 更新配置信息
        /// </summary>
        /// <param name="group"></param>
        /// <param name="value"></param>
        /// <param name="type">0，row   1 column</param>
        /// <returns></returns>
        public bool updateRoomConfig(int group, int value, int type)
        {
            string sql = null;
            switch (type)
            {
                case 0:
                    sql = sqlUpdate_updateRoomConfigRow;
                    break;
                case 1:
                    sql = sqlUpdate_updateRoomConfigColumn;
                    break;
            }
            if (null == sql)
            {
                return false;
            }
            try
            {
                int result = int.Parse(CsharpSQLiteHelper.ExecuteNonQuery(
                             sql, new object[2] { value, group }).ToString());

                if (result > 0)
                {
                    return true;
                }
            }
            catch (System.Exception ex)
            {

                MessageBox.Show("更新数据时出现错误：" + ex.Message);
            }
            return false;
        }

        public static DataTable getAllRoomConfigInfo()
        {
            try
            {
                DataTable dt = CsharpSQLiteHelper.ExecuteTable(sqlSelect_allRoomConfig, null);
                return dt;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("查询数据库时出现错误：" + ex.Message);
            }
            return null;
        }
    }
}
