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
            @"select p.STUDENTID, p.NAME, p.SEX, p.AGE, p.CLASS_NAME, p.EMAIL, p_link.DEVICEID epc  from T_STUDENTINFO as p left join T_STUDENTINFO_LINK_EPC as p_link on p.STUDENTID=p_link.STUDENTID;";
        public static string sqlSelect_GetSpecifiedStudentInfo =
            @"select STUDENTID,NAME,SEX,AGE,CLASS_NAME ,EMAIL from T_STUDENTINFO where STUDENTID = '{0}'";
        public static string sqlInsert_AddCheckInfo =
            @"INSERT into T_STUDENT_CHECK_INFO(record_id, student_id ,check_time) VALUES('{0}','{1}','{2}');";
        public static string sqlInsert_AddClassCheckInfo =
            @"INSERT into T_CLASS_CHECK_INFO(CHECK_TIME,CLASS_NAME,PERCENTAGE) values('{0}','{1}','{2}');";
        static string sqlDelete_clearStudentInfo = "delete from T_STUDENTINFO;";
        static string sqlDelete_clearStudentEpcLink = "delete from T_STUDENTINFO_LINK_EPC;";
        static string sqlInsert_addStudent = "insert into T_STUDENTINFO values('{0}','{1}','{2}',{3},'{4}','{5}');";
        static string sqlInsert_addStudentEpcLink = "insert into T_STUDENTINFO_LINK_EPC values('{0}','{1}');";



        public static void addStudentInfo(List<Person> list)
        {
            foreach (Person person in list)
            {
                addStudentInfo(person);
            }
        }
        public static bool addStudentInfo(Person person)
        {
            return addStudentInfo(person.id_num, person.name, person.sex, person.age, person.bj, person.email, person.epc);
        }
        public static bool addStudentInfo(string id, string name, string sex, int age, string class_name, string email, string epc)
        {
            try
            {
                int result1 = CsharpSQLiteHelper.ExecuteNonQuery(
                             sqlInsert_addStudent
                             , new object[] { id, name, sex, age, class_name, email });
                int result2 = CsharpSQLiteHelper.ExecuteNonQuery(
                             sqlInsert_addStudentEpcLink
                             , new object[] { id, epc });
                if (result1 > 0 && result2 > 0)
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
       
        public static void clearStudentInfo()
        {
            try
            {
                CsharpSQLiteHelper.ExecuteNonQuery(sqlDelete_clearStudentInfo, null);
                CsharpSQLiteHelper.ExecuteNonQuery(sqlDelete_clearStudentEpcLink, null);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("更新数据时出现错误：" + ex.Message);
            }
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

        public static bool AddCheckInfo(string record_id, string STUDENTID, string CHECK_TIME)
        {
            bool bR = true;
            int result = CsharpSQLiteHelper.ExecuteNonQuery(sqlInsert_AddCheckInfo, new object[3] { record_id, STUDENTID, CHECK_TIME });
            if (result <= 0)
            {
                bR = false;
            }
            return bR;
        }
        public DataTable getStudentInfo(string id)
        {
            try
            {
                DataTable dt = CsharpSQLiteHelper.ExecuteTable(sqlSelect_GetSpecifiedStudentInfo, new object[1] { id });
                return dt;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("查询数据库时出现错误：" + ex.Message);
            }
            return null;
        }
        public static DataTable getAllStudentInfo()
        {
            try
            {
                DataTable dt = CsharpSQLiteHelper.ExecuteTable(sqlSelect_allGetStudentInfo, null);
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
