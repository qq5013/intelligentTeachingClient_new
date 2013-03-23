using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Windows.Forms;
using System.Collections;

namespace Carbinet
{
    public class CheckInfo
    {
        public string record_id = string.Empty;
        public string STUDENTID = string.Empty;
        public string CHECK_TIME = string.Empty;
        //public string SUBJECT_NAME = string.Empty;
        //public string STATUS = null;
    }
    public class studentInfoCtl
    {
        public static string sqlSelect_allGetStudentInfo =
            @"select STUDENTID,NAME,SEX,AGE,CLASS_NAME ,EMAIL,DEVICEID epc  from T_STUDENTINFO;";
        public static string sqlSelect_GetSpecifiedStudentInfo =
            @"select STUDENTID,NAME,SEX,AGE,CLASS_NAME ,EMAIL from T_STUDENTINFO where STUDENTID = '{0}'";
        public static string sqlInsert_AddCheckInfo =
            @"INSERT into T_STUDENT_CHECK_INFO(record_id, student_id ,check_time) VALUES('{0}','{1}','{2}');";
        public static string sqlInsert_AddClassCheckInfo =
            @"INSERT into T_CLASS_CHECK_INFO(CHECK_TIME,CLASS_NAME,PERCENTAGE) values('{0}','{1}','{2}');";
        public static string sqlSelect_GetClassCheckInfo =
            @"SELECT CLASS_NAME as 班级名称,CHECK_TIME as 考勤时间,PERCENTAGE as 出勤率 from T_CLASS_CHECK_INFO
             where CLASS_NAME = '{0}' and CHECK_TIME > '{1}' and CHECK_TIME < '{2}'";

        public static DataTable GetClassCheckInfo(string className, string start, string end)
        {
            DataSet ds = null;
            try
            {
                DataTable dt = CsharpSQLiteHelper.ExecuteTable(
           sqlSelect_GetClassCheckInfo, new object[3] { className, start, end });
                return dt;
                //ds = SQLiteHelper.ExecuteDataSet(
                //          SQLiteHelper.connectString,
                //           sqlSelect_GetClassCheckInfo, new object[3] { className, start, end });
                //if (ds != null)
                //{
                //    if (ds.Tables.Count > 0)
                //    {
                //        return ds.Tables[0];
                //    }
                //}
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("查询数据库时出现错误：" + ex.Message);
            }
            return null;
        }

        public bool AddClassCheckInfo(string time, string name, string percentage)
        {
            try
            {
                int result = int.Parse(CsharpSQLiteHelper.ExecuteNonQuery(
                             sqlInsert_AddClassCheckInfo
                             , new object[3]
                                                    {
                                                        time
                                                        ,name
                                                        ,percentage
                                                    }).ToString());
                //int result = int.Parse(SQLiteHelper.ExecuteNonQuery(SQLiteHelper.connectString,
                //                             sqlInsert_AddClassCheckInfo
                //                             , new object[3]
                //                                    {
                //                                        time
                //                                        ,name
                //                                        ,percentage
                //                                    }).ToString());
                if (result > 0)
                {
                    return true;
                }
            }
            catch (System.Exception ex)
            {

                MessageBox.Show("添加数据时出现错误：" + ex.Message);
            }
            return false;
        }

        public bool AddCheckInfo(List<CheckInfo> paraList)
        {
            bool bR = true;
            // List<CheckInfo>< object[]> ls = new List<CheckInfo><object[]>();
            //Hashtable SQLStringList = new Hashtable();
            foreach (CheckInfo ci in paraList)
            {
                //SQLStringList.Add(new object[4] { ci.STUDENTID, ci.CHECK_TIME, ci.SUBJECT_NAME, ci.STATUS }, sqlInsert_AddCheckInfo);

                int result = CsharpSQLiteHelper.ExecuteNonQuery(sqlInsert_AddCheckInfo, new object[3] { ci.record_id, ci.STUDENTID, ci.CHECK_TIME });
                if (result <= 0)
                {
                    bR = false;
                }
                //SQLStringList.Add(sqlInsert_AddCheckInfo, new object[3] { ci.STUDENTID, ci.CHECK_TIME, ci.SUBJECT_NAME });
            }
            return bR;
            //return SQLiteHelper.ExecuteSqlTran(
            //                        SQLiteHelper.connectString,
            //                        SQLStringList);

        }

        public DataTable getStudentInfo(string id)
        {
            DataSet ds = null;
            try
            {
                DataTable dt = CsharpSQLiteHelper.ExecuteTable(sqlSelect_GetSpecifiedStudentInfo, new object[1] { id });
                return dt;
                //ds = SQLiteHelper.ExecuteDataSet(
                //          SQLiteHelper.connectString,
                //           sqlSelect_GetSpecifiedStudentInfo, new object[1] { id });
                //if (ds != null)
                //{
                //    if (ds.Tables.Count > 0)
                //    {
                //        return ds.Tables[0];
                //    }
                //}
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("查询数据库时出现错误：" + ex.Message);
            }
            return null;
        }
        public static DataTable getAllStudentInfo()
        {
            DataSet ds = null;
            try
            {
                DataTable dt = CsharpSQLiteHelper.ExecuteTable(sqlSelect_allGetStudentInfo, null);
                return dt;
                //ds = SQLiteHelper.ExecuteDataSet(
                //          SQLiteHelper.connectString,
                //           sqlSelect_allGetStudentInfo, null);
                //if (ds != null)
                //{
                //    if (ds.Tables.Count > 0)
                //    {
                //        return ds.Tables[0];
                //    }
                //}
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("查询数据库时出现错误：" + ex.Message);
            }
            return null;
        }
    }
}
