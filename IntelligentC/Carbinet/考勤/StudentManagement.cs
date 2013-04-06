using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Carbinet
{
    public class StudentManagement
    {
        #region Static Memebers

        static string SqlSelectAllPerson =
            "select  p.STUDENTID \"学号\",p.NAME \"姓名\", p.SEX \"性别\", p.AGE \"年龄\", p.CLASS_NAME \"班级\", p.EMAIL \"邮箱\", p_link.DEVICEID \"学生卡\" FROM T_STUDENTINFO as p left join T_STUDENTINFO_LINK_EPC as p_link on p.STUDENTID=p_link.STUDENTID;";
        static string SqlBindPerson2EPC =
            @"replace into T_STUDENTINFO_LINK_EPC(STUDENTID, DEVICEID) values('{0}', '{1}');";
        static string SqlUnbindPerson2EPC =
            @"replace into T_STUDENTINFO_LINK_EPC(STUDENTID, DEVICEID) values('{0}', '');";
        static string SqlCheckEPCUsed =
            @"select count(STUDENTID) from T_STUDENTINFO_LINK_EPC where DEVICEID = '{0}'";

        static string regexString = @"2\d{3}[-/](1[0-2]|0?[1-9])[-/](3[0-1]|[0-2]?\d)";
        #endregion
        public static string GetFormatDateTimeString(DateTime dt)
        {
            return dt.ToString("yyyy-MM-dd HH:mm:ss");
        }
        public static string GetDateSubString(string strDateTime)
        {
            string strDate = null;
            if (null == strDateTime)
            {
                return strDate;
            }
            if (Regex.IsMatch(strDateTime, regexString))
            {
                strDate = Regex.Match(strDateTime, regexString).ToString();
            }
            return strDate;
        }
        #region Person Management

        public static DataTable GetPersonDataTable()
        {
            DataTable myDataSetPerson = null;
            try
            {
                myDataSetPerson = CsharpSQLiteHelper.ExecuteTable(SqlSelectAllPerson, null);

            }
            catch (System.Exception ex)
            {
                MessageBox.Show("读取学生数据库时出现错误！" + ex.Message);
                return null;
            }

            return myDataSetPerson;
        }

        /// <summary>
        /// 删除学号与EPC的关联
        /// </summary>
        /// <param name="xh"></param>
        /// <returns></returns>
        public static bool UnbindPersonEPC(string xh)
        {
            bool bR = false;
            try
            {
                int result = 0;
                object[] pars = new object[] { xh };
                result = int.Parse(CsharpSQLiteHelper.ExecuteNonQuery(SqlUnbindPerson2EPC, pars).ToString());
                if (result >= 1)
                {
                    bR = true;
                }
                else
                {
                    bR = false;
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("更新记录出现错误：" + ex.Message);
            }
            return bR;
        }
        /// <summary>
        /// 将学号和epc相关联
        /// </summary>
        /// <param name="xh"></param>
        /// <param name="epc"></param>
        /// <returns></returns>
        public static bool BindPersonEPC(string xh, string epc)
        {
            bool bR = false;
            try
            {
                int result = 0;
                object[] pars = new object[2] { xh, epc };
                result = int.Parse(CsharpSQLiteHelper.ExecuteNonQuery(SqlBindPerson2EPC, pars).ToString());
                if (result >= 1)
                {
                    bR = true;
                }
                else
                {
                    bR = false;
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("更新记录出现错误：" + ex.Message);
            }
            return bR;
        }
        #endregion

        public static bool CheckEPCUsed(string epc)
        {
            bool bR = false;
            try
            {
                object[] pars = new object[1]
	                    {
	                       epc
	                    };
                DataTable dt = CsharpSQLiteHelper.ExecuteTable(SqlCheckEPCUsed, pars);
                if (dt.Rows.Count > 0)
                {
                    bR = true;
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("更新记录出现错误：" + ex.Message);
            }
            return bR;
        }


    }
}
