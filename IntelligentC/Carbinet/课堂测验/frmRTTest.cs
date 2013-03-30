using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using intelligentMiddleWare;
using Nexus.Windows.Forms;
using MetroFramework.Forms;

namespace Carbinet
{
    public partial class frmRTTest : MetroForm, I_event_notify, I_event_handler
    {
        #region 成员
        frmQuestionMngCtl ctl = new frmQuestionMngCtl();

        DataTable dtQuestion = null;
        DataTable dtQuestion_answer_record = null;//存放每个学生的答题记录
        List<Carbinet> groups = new List<Carbinet>();
        string question_relative_path = "questions";
        string test_id = string.Empty;

        string current_question_id = string.Empty;//当前问题的id

        #region 各个选项对应的颜色
        Color clrNotKnown = Color.FromArgb(0, 174, 219);
        Color clrAnswered = Color.FromArgb(0, 177, 89);
        #endregion
        I_mini_form_show_notify formToNotify;

        #endregion

        #region 初始化

        public frmRTTest(I_mini_form_show_notify _notify_form)
        {
            InitializeComponent();
            this.formToNotify = _notify_form;
            this.test_id = string.Format("{0}{1}", "test_id", DateTime.Now.ToString("yyyyMMddHHmmss"));
            this.FormClosing += new FormClosingEventHandler(frmRTTest_FormClosing);
        }

        private void frmRTTest_FormClosing(object sender, FormClosingEventArgs e)
        {
            //for (int i = 0; i < studentInfoTable.Rows.Count; i++)
            //{
            //    DataRow dr = studentInfoTable.Rows[i];
            //    dr["answer"] = "";
            //}
            this.save_answer_info();
        }

        private void initialInfoTable()
        {
            this.dtQuestion = new DataTable();
            dtQuestion.Columns.Add("question_id", typeof(string));
            dtQuestion.Columns.Add("caption", typeof(string));
            dtQuestion.Columns.Add("answer", typeof(string));
            dtQuestion.Columns.Add("question_index", typeof(string));//播放次序
            dtQuestion.Columns.Add("state", typeof(string));//标识是否正在播放

            this.dtQuestion_answer_record = new DataTable();
            dtQuestion_answer_record.Columns.Add("student_id", typeof(string));
            dtQuestion_answer_record.Columns.Add("question_id", typeof(string));
            dtQuestion_answer_record.Columns.Add("answer", typeof(string));

            //统一初始化
            DataTable dtTemp = frmQuestionMngCtl.getAllQuestion();
            if (dtTemp != null && dtTemp.Rows.Count > 0)
            {
                for (int i = 0; i < dtTemp.Rows.Count; i++)
                {
                    DataRow dr = dtTemp.Rows[i];
                    this.dtQuestion.Rows.Add(dr["question_id"], dr["caption"], dr["answer"], dr["question_index"], "false");
                }
            }

        }

        //页面初始化时，饼图初始状态，并不一定与之后恢复的状态相同
        private void setPieToInitializeState()
        {
            List<int> valueList = this.getValueList(1, 1);
            NotifyFormToRefreshPie(valueList, false);
        }
        #region 工具函数

        private List<string> getcaptionList()
        {
            List<string> captionList = new List<string>();
            captionList.Add("未完成");
            captionList.Add("已完成");
            return captionList;
        }
        private List<Color> getColorList()
        {
            List<Color> colorList = new List<Color>();
            colorList.Add(this.clrNotKnown);
            colorList.Add(this.clrAnswered);
            return colorList;
        }
        private List<int> getValueList(int a, int b)
        {
            List<int> valueList = new List<int>();
            valueList.Add(a);
            valueList.Add(b);
            return valueList;
        }
        void NotifyFormToRefreshPie(List<int> valueList, bool bShowLabel)
        {
            List<string> captionList = this.getcaptionList();
            List<Color> colorList = getColorList();
            if (this.formToNotify != null)
            {
                this.formToNotify.refreshPie(captionList, valueList, colorList, bShowLabel);
            }
        }
        #endregion

        #endregion

        #region 接口函数
        public void receive_a_new_event()
        {
            this.handle_event();
        }
        private void handle_event()
        {
            IntelligentEvent evt = MiddleWareCore.get_a_event();
            if (evt != null)
            {
                deleControlInvoke dele = delegate(object o)
                {
                    IntelligentEvent p = (IntelligentEvent)o;
                    string epcID = p.epcID;
                    string remoteDeviceID = p.remoteDeviceID;
                    string question_value = p.questionValue;
                    int totalCount = MemoryTable.studentInfoTable.Rows.Count;

                    Person person = MemoryTable.getPersonByEpc(epcID);
                    equipmentPosition ep = MemoryTable.getEquipmentConfigMapInfo(remoteDeviceID);
                    //如果只是重复发送，不需要做什么
                    if (p.event_unit_list.IndexOf(IntelligentEventUnit.repeat_epc) >= 0)
                    {
                        //如果重复发送之外，还改变了设备的ID
                        if (p.event_unit_list.IndexOf(IntelligentEventUnit.epc_on_another_device) >= 0)
                        {
                            if (person != null && ep != null)
                            {
                                int groupIndex = ep.group;

                                equipmentPosition ep_old = MemoryTable.getEquipmentInfoByStudentID(epcID);
                                Program.frmClassRoom.changeChairState(ep_old.group, ep_old.formatedPosition(), DocumentFileState.InitialState);
                                Program.frmClassRoom.changeChairState(ep_old.group, ep_old.formatedPosition(), "");
                                MemoryTable.clearEquipmentAndStudentCombining(epcID);

                                Program.frmClassRoom.changeChairState(groupIndex, ep.formatedPosition(), person.name);
                                MemoryTable.setEquipmentInfoCombineStudentID(ep, person.epc);

                                //MemoryTable.setPersonAnswer(epcID, question_value);

                                Program.frmClassRoom.changeChairState(groupIndex, ep.formatedPosition(), DocumentFileState.Green);
                            }
                        }
                        //如果重复发送之外，还改变了问题的答案，按照设计，这里不需要更改饼图
                        if (p.event_unit_list.IndexOf(IntelligentEventUnit.change_answer) >= 0)
                        {
                            //MemoryTable.setPersonAnswer(epcID, question_value);

                            this.refreshAnswerRecord(epcID, question_value);
                        }
                    }
                    else
                        if (p.event_unit_list.IndexOf(IntelligentEventUnit.new_epc) >= 0)
                        {
                            //处理该事件需要更新数据和显示页面
                            if (person != null && ep != null)
                            {
                                MemoryTable.setEquipmentInfoCombineStudentID(ep, epcID);
                                Program.frmClassRoom.changeChairState(ep.group, ep.formatedPosition(), DocumentFileState.Green);
                                Program.frmClassRoom.changeChairState(ep.group, ep.formatedPosition(), person.name);
                            }

                            //MemoryTable.setPersonAnswer(epcID, question_value);
                            this.refreshAnswerRecord(epcID, question_value);
                            this.refreshPie();
                        }
                };

                this.Invoke(dele, evt);
            }
        }
        public void prepare_handler()
        {
            this.initialInfoTable();
            MiddleWareCore.set_mode(MiddleWareMode.课堂测验, this);
            Program.frmClassRoom.resetClassRoomState();
            setPieToInitializeState();

            this.Show();
        }
        #endregion

        #region 事件处理
        //切换题目
        private void btnNext_Click(object sender, EventArgs e)
        {
            int index = changeQuestion(true);
            if (index >= 0)
            {
                if (index == this.dtQuestion.Rows.Count - 1)
                {
                    this.metroBtnNext.Enabled = false;
                }
                else
                {
                    this.metroBtnNext.Enabled = true;
                }
                this.metroBtnPre.Enabled = true;
            }
        }

        private void btnPre_Click(object sender, EventArgs e)
        {
            int index = changeQuestion(false);
            if (index >= 0)
            {
                if (index == 0)
                {
                    this.metroBtnPre.Enabled = false;
                }
                else
                {
                    this.metroBtnPre.Enabled = true;
                }
                this.metroBtnPre.Enabled = true;
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            int index = changeQuestion(true);
            if (index >= 0)
            {
                this.metroBtnPre.Enabled = true;
                this.metroBtnNext.Enabled = true;
                if (this.dtQuestion.Rows.Count <= 1)
                {
                    this.metroBtnPre.Enabled = false;
                    this.metroBtnNext.Enabled = false;
                }
                this.metroBtnStart.Enabled = false;
            }

        }

        #endregion

        #region 内部函数
        //设置当前题目的答题状态
        private void reset_test_status(string question_id)
        {
            DataRow[] rows = this.dtQuestion_answer_record.Select(string.Format("question_id = '{0}'", question_id));
            int total_student_count = MemoryTable.studentInfoTable.Rows.Count;
            int iAnswered = rows.Length;
            int iUnknown = total_student_count - iAnswered;
            NotifyFormToRefreshPie(this.getValueList(iUnknown, iAnswered), true);

            MiddleWareCore.set_mode(MiddleWareMode.课堂测验);

            Program.frmClassRoom.resetClassRoomState();
            //设置教室座位  根据问题查找学生ID，然后根据ID查找位置
            for (int i = 0; i < rows.Length; i++)
            {
                string studentID = (string)rows[i]["student_id"];
                equipmentPosition ep = MemoryTable.getEquipmentInfoByStudentID(studentID);
                Person psn = MemoryTable.getPersonByEpc(studentID);
                Program.frmClassRoom.changeChairState(ep.group, ep.formatedPosition(), DocumentFileState.Green);
                Program.frmClassRoom.changeChairState(ep.group, ep.formatedPosition(), psn.name);
            }
        }
        /// <summary>
        /// 切换题目
        /// </summary>
        /// <param name="b">true 往后切换；否则回退</param>
        /// <returns>切换后的题目的索引；如果为-1，切换失败</returns>
        private int changeQuestion(bool b)
        {
            int index = -1;
            if (this.dtQuestion.Rows.Count > 0)
            {
                DataRow[] rows = this.dtQuestion.Select("state = 'true'");
                if (rows.Length > 0)
                {
                    DataRow dr = rows[0];
                    index = dtQuestion.Rows.IndexOf(dr);
                    dr["state"] = "false";
                    if (b) { index += 1; }
                    else { index -= 1; }

                }
                else { index = 0; }

                DataRow drNext = dtQuestion.Rows[index];
                this.current_question_id = drNext["question_id"].ToString();
                drNext["state"] = "true";
                string html = this.GetHtmlFile(this.current_question_id);
                this.editor1.Clear();
                if (html != string.Empty)
                {
                    this.editor1.DocumentText = html;
                }

                this.reset_test_status(this.current_question_id);

            }

            return index;
        }

        private void refreshAnswerRecord(string student_id, string question_value)
        {
            //更新答题记录
            DataRow[] rows = this.dtQuestion_answer_record.Select(string.Format("student_id = '{0}' and question_id = '{1}'", student_id, current_question_id));
            if (rows.Length > 0)
            {
                rows[0]["answer"] = question_value;
            }
            else
            {
                dtQuestion_answer_record.Rows.Add(new object[3] { student_id, current_question_id, question_value });
            }
        }

        private void refreshPie()
        {
            int rowsTotal = MemoryTable.studentInfoTable.Rows.Count;
            DataRow[] rowsAnswered = this.dtQuestion_answer_record.Select(string.Format("question_id = '{0}'", current_question_id));//可以得到该题目已回答的人数
            int iUnknown = rowsTotal - rowsAnswered.Length;
            int iAnswered = rowsAnswered.Length;
            NotifyFormToRefreshPie(this.getValueList(iUnknown, iAnswered), true);
        }

        private string GetHtmlFile(string filename)
        {
            string path = Application.StartupPath + "\\" + this.question_relative_path + "\\" + filename + "\\" + filename + ".html";

            string html = string.Empty;
            if (File.Exists(path))
            {
                using (StreamReader reader = File.OpenText(path))
                {
                    html = reader.ReadToEnd();
                    reader.Close();
                    string file_path = Path.GetDirectoryName(path);
                    html = html.Replace("image_path:", "file://" + file_path);
                }
            }
            return html;
        }

        void save_answer_info()
        {
            for (int i = 0; i < dtQuestion_answer_record.Rows.Count; i++)
            {
                DataRow dr = dtQuestion_answer_record.Rows[i];
                string student_id = dr["student_id"].ToString();
                string question_id = dr["question_id"].ToString();
                string answer = dr["answer"].ToString();

                if (frmQuestionMngCtl.question_record_exist(question_id, student_id))
                {
                    frmQuestionMngCtl.update_question_record(question_id, student_id, answer);
                }
                else
                {
                    frmQuestionMngCtl.add_question_record(question_id, student_id, answer);
                }
            }
        }
        #endregion
    }
}
