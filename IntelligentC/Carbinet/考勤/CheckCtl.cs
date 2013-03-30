using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Carbinet
{
    public class CheckCtl
    {
        static string sql_insert_record = @"insert into check_record(record_id,info,create_time) values('{0}','{1}','{2}')";
        static string sqlSelect_GetAllCheckInfo =
   "select record_id \"考勤记录\", create_time \"创建时间\", info \"备注\" from check_record order by create_time desc;";

        public static bool insert_record(string id, string info, string create_time)
        {
            bool bR = false;
            try
            {
                int result = 0;
                object[] pars = new object[]
	                    {
                            id,info,create_time
	                    };

                result = int.Parse(CsharpSQLiteHelper.ExecuteNonQuery(sql_insert_record, pars).ToString());
                if (result >= 1)
                {
                    bR = true;
                }
                else
                {
                    bR = false;
                }

            }
            catch
            {
                //MessageBox.Show("出现错误：" + ex.Message);
            }
            return bR;
        }

        public static DataTable getAllCheckRecord()
        {
            return CsharpSQLiteHelper.ExecuteTable(sqlSelect_GetAllCheckInfo, null);
        }
    }
}
