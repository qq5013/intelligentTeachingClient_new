using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Windows.Forms;

namespace Carbinet
{
    public class frmQuestionMngCtl
    {
        public static string sqlSelect_getAllQuestion =
            @"select question_id,caption,answer,question_index from T_QUESTION  order by question_index asc";
        public static string sqlInsert_addQuestion =
            @"insert into T_QUESTION(question_id,caption,answer,question_index) values('{0}','{1}','{2}','{3}');";
        public static string sqlUpdate_updateQuestion =
            @"update T_QUESTION set caption = '{0}',answer='{1}',question_index='{2}' where question_id = '{3}';";
        public static string sqlSelect_getSpecifiedQuestion =
            @"SELECT  QUESTION_NO,content,answer 	FROM T_QUESTION where content='{0}'";
        // @"SELECT  QUESTION_NO,content,answer 	FROM T_QUESTION where QUESTION_NO=@QUESTION_NO";
        public static string sqlDelete_delete_specified_question =
            @"delete from T_QUESTION where question_id = '{0}'";

        public static string sql_Insert_question_answer_info =
            @"insert into question_answer_record(question_id,student_id,answer) values('{0}','{1}','{2}')";
        public static string sql_select_question_answer_info =
            @"select question_id,student_id,answer from question_answer_record where question_id = '{0}' and student_id = '{1}'";
        public static string sql_update_question_answer_info =
            @"update question_answer_record set answer = '{0}' where question_id = '{1}' and student_id = '{2}'";

        //返回false时可能为要更新的数据和数据库中现有数据一致
        public static bool update_question_record(string question_id, string student_id, string answer)
        {
            string sql = sql_update_question_answer_info;
            try
            {
                int result = 0;
                result = CsharpSQLiteHelper.ExecuteNonQuery(
                            sql
                            , new object[3]
                                        {
                                           answer
                                           ,question_id
                                           ,student_id
                                        });

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
        public static bool add_question_record(string question_id, string student_id, string answer)
        {
            string sql = sql_Insert_question_answer_info;
            try
            {
                int result = 0;
                result = CsharpSQLiteHelper.ExecuteNonQuery(
                            sql
                            , new object[3]
                                        {
                                           question_id
                                           ,student_id
                                           ,answer
                                        });

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
        public static bool question_record_exist(string question_id, string student_id)
        {
            try
            {
                DataTable dt = CsharpSQLiteHelper.ExecuteTable(sql_select_question_answer_info,
                                                                new object[2] {  question_id,student_id});
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
        public static bool QuestionExists(string content)
        {
            DataSet ds = null;
            try
            {
                DataTable dt = CsharpSQLiteHelper.ExecuteTable(sqlSelect_getSpecifiedQuestion, new object[1] { content });
                if (dt.Rows.Count > 0)
                {
                    return true;
                }
                //ds = SQLiteHelper.ExecuteDataSet(
                //          SQLiteHelper.connectString,
                //           sqlSelect_getSpecifiedQuestion, new object[1] { content });
                //if (ds != null)
                //{
                //    if (ds.Tables.Count > 0)
                //    {
                //        if (ds.Tables[0].Rows.Count > 0)
                //        {
                //            return true;
                //        }
                //    }
                //}
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("查询数据库时出现错误：" + ex.Message);
            }
            return false;
        }
        public static bool deleteQuestion(string question_id)
        {
            string sql = sqlDelete_delete_specified_question;
            try
            {
                int result = 0;
                result = int.Parse(CsharpSQLiteHelper.ExecuteNonQuery(
                            sql
                            , new object[1]
                                                    {
                                                       question_id
                                                    }).ToString());
                //result = int.Parse(SQLiteHelper.ExecuteNonQuery(SQLiteHelper.connectString,
                //                            sql
                //                            , new object[1]
                //                                    {
                //                                       question_id
                //                                    }).ToString());

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
        public static bool updateQuestion(string question_id, string caption, string answer, string question_index)
        {
            string sql = sqlUpdate_updateQuestion;
            try
            {
                int result = 0;
                result = int.Parse(CsharpSQLiteHelper.ExecuteNonQuery(
                            sql
                            , new object[4]
                                                    {
                                                        caption
                                                        ,answer
                                                        ,question_index
                                                        ,question_id
                                                    }).ToString());
                //result = int.Parse(SQLiteHelper.ExecuteNonQuery(SQLiteHelper.connectString,
                //                            sql
                //                            , new object[4]
                //                                    {
                //                                        caption
                //                                        ,answer
                //                                        ,question_index
                //                                        ,question_id
                //                                    }).ToString());

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

        public static bool AddQuestion(string question_id, string caption, string answer, string question_index)
        {
            try
            {
                int result = int.Parse(CsharpSQLiteHelper.ExecuteNonQuery(
                             sqlInsert_addQuestion
                             , new object[4]
                                                    {
                                                        question_id
                                                        ,caption
                                                        ,answer
                                                        ,question_index
                                                    }).ToString());
                //int result = int.Parse(SQLiteHelper.ExecuteNonQuery(SQLiteHelper.connectString,
                //                             sqlInsert_addQuestion
                //                             , new object[4]
                //                                    {
                //                                        question_id
                //                                        ,caption
                //                                        ,answer
                //                                        ,question_index
                //                                    }).ToString());
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


        public static DataTable getAllQuestion()
        {
            DataSet ds = null;
            try
            {
                DataTable dt = CsharpSQLiteHelper.ExecuteTable(sqlSelect_getAllQuestion, null);
                return dt;
                //ds = SQLiteHelper.ExecuteDataSet(
                //          SQLiteHelper.connectString,
                //           sqlSelect_getAllQuestion, null);
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
