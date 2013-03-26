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
    public partial class frmRTTest : MetroForm, I_event_notify
    {
        #region 成员
        frmQuestionMngCtl ctl = new frmQuestionMngCtl();

        DataTable studentInfoTable = null;
        DataTable mapConfigsTable = null;
        DataTable dtRoomConfig = null;
        DataTable dtQuestion = null;
        DataTable dtQuestion_answer_record = null;//存放每个学生的答题记录
        List<Carbinet> groups = new List<Carbinet>();
        public event deleInternalCommandInvoke eventInvokeCommand;
        //frmRTTestStudent frmStudent = null;
        bool showChair = false;//是否显示座位
        string question_relative_path = "questions";
        string test_id = string.Empty;

        string current_question_id = string.Empty;//当前问题的id

        #region 各个选项对应的颜色
        Color clrNotKnown = Color.FromArgb(0, 174, 219);
        Color clrAnswered = Color.FromArgb(0, 177, 89);

        #endregion

        #endregion
        #region 初始化


        public frmRTTest()
        {
            InitializeComponent();
            #region PieChart1
            PieChart1.ItemStyle.SurfaceAlphaTransparency = 0.92F;
            PieChart1.FocusedItemStyle.SurfaceAlphaTransparency = 0.92F;
            PieChart1.FocusedItemStyle.SurfaceBrightnessFactor = 0.3F;
            PieChart1.Inclination = 1.047F;
            PieChart1.AutoSizePie = true;
            PieChart1.Thickness = 31;
            PieChart1.Rotation = 0.1396263F;
            PieChart1.ShowEdges = false;

            #endregion


            this.test_id = string.Format("{0}{1}", "test_id", DateTime.Now.ToString("yyyyMMddHHmmss"));

            dtQuestion = new DataTable();
            dtQuestion.Columns.Add("question_id", typeof(string));
            dtQuestion.Columns.Add("caption", typeof(string));
            dtQuestion.Columns.Add("answer", typeof(string));
            dtQuestion.Columns.Add("question_index", typeof(string));//播放次序
            dtQuestion.Columns.Add("state", typeof(string));//标识是否正在播放

            this.dtQuestion_answer_record = new DataTable();
            dtQuestion_answer_record.Columns.Add("student_id", typeof(string));
            dtQuestion_answer_record.Columns.Add("question_id", typeof(string));
            dtQuestion_answer_record.Columns.Add("answer", typeof(string));

            this.initialInfoTable();
            InitializePanelControl();

            this.Shown += new EventHandler(frmRTTest_Shown);
            this.FormClosing += new FormClosingEventHandler(frmRTTest_FormClosing);
            this.VisibleChanged += new EventHandler(frmRTTest_VisibleChanged);
            MiddleWareCore.event_receiver = this;
        }

        void frmRTTest_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible == true)
            {
                this.clearSelectStatus();
                MiddleWareCore.event_receiver = this;
            }

            //if (this.Visible == true)
            //{
            //    if (this.studentInfoTable != null)
            //    {
            //        for (int i = 0; i < this.studentInfoTable.Rows.Count; i++)
            //        {
            //            studentInfoTable.Rows[i]["answer"] = "";
            //        }
            //    }
            //}
        }
        void clearSelectStatus()
        {
            Program.frmClassRoom.resetClassRoomState();
            MemoryTable.resetAllPersonAnswer("B");//默认都选B，也就是不选择
            this.InitializePanelControl();
        }
        void frmRTTest_FormClosing(object sender, FormClosingEventArgs e)
        {
            //for (int i = 0; i < studentInfoTable.Rows.Count; i++)
            //{
            //    DataRow dr = studentInfoTable.Rows[i];
            //    dr["answer"] = "";
            //}
            this.save_answer_info();
        }

        void frmRTTest_Shown(object sender, EventArgs e)
        {


            //this.groupBoxChair.Top = 98;

            //InitialClassRoom();

            //this.clearSelectStatus();
            //commonSocket.eventDataReived = disposeReceivedData;

            //frmStudent = new frmRTTestStudent(this);

            //Screen[] screens = System.Windows.Forms.Screen.AllScreens;
            //for (int i = 0; i < screens.Length; i++)
            //{
            //    Screen sc = screens[i];
            //    if (sc.Primary == false)
            //    {
            //        Rectangle rect = sc.WorkingArea;
            //        this.frmStudent.Left = rect.Left;
            //        this.frmStudent.Top = rect.Top;
            //        this.frmStudent.Width = rect.Width;
            //        this.frmStudent.Height = rect.Height;
            //        frmStudent.Show();
            //    }
            //}
        }
        private void initialInfoTable()
        {

            //统一初始化
            if (MemoryTable.isInitialized == false)
            {
                MemoryTable.initializeTabes();
            }
            this.dtRoomConfig = MemoryTable.dtRoomConfig;
            this.studentInfoTable = MemoryTable.studentInfoTable;
            this.mapConfigsTable = MemoryTable.mapConfigsTable;
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

        private void InitializePanelControl()
        {
            PieChart1.Items.Clear();
            PieChart1.Items.Add(new PieChartItem(0, this.clrAnswered, "0", "A", 0));
            int total = this.studentInfoTable.Rows.Count;
            PieChart1.Items.Add(new PieChartItem(total, this.clrNotKnown, total.ToString(), "B", 0));
        }

        #endregion
        #region 事件处理
        //单击座位时的处理

        //显示座位、题目
        private void button3_Click(object sender, EventArgs e)
        {

        }
        //切换题目
        private void btnNext_Click(object sender, EventArgs e)
        {
            ////发送下一题命令
            //if (this.eventInvokeCommand != null)
            //{
            //    this.eventInvokeCommand(InternalCommand.NextQuestion, null);
            //}
            DataRow[] rows = this.dtQuestion.Select("state = 'true'");
            if (rows.Length > 0)
            {
                DataRow dr = rows[0];
                int index = dtQuestion.Rows.IndexOf(dr);
                index++;
                DataRow drNext = dtQuestion.Rows[index];
                dr["state"] = "false";
                this.current_question_id = drNext["question_id"].ToString();
                drNext["state"] = "true";
                string html = this.GetHtmlFile(current_question_id);
                this.editor1.Clear();
                if (html != string.Empty)
                {
                    this.editor1.DocumentText = html;
                }
                if (index == this.dtQuestion.Rows.Count - 1)
                {
                    this.metroBtnNext.Enabled = false;
                }
                else
                {
                    this.metroBtnNext.Enabled = true;
                }
                this.metroBtnPre.Enabled = true;

                this.reset_test_status();
            }

        }

        private void btnPre_Click(object sender, EventArgs e)
        {
            ////发送上一题命令
            //if (this.eventInvokeCommand != null)
            //{
            //    this.eventInvokeCommand(InternalCommand.PreQuestion, null);
            //}
            DataRow[] rows = this.dtQuestion.Select("state = 'true'");//
            if (rows.Length > 0)
            {
                DataRow dr = rows[0];
                int index = dtQuestion.Rows.IndexOf(dr);
                index--;
                DataRow drNext = dtQuestion.Rows[index];
                dr["state"] = "false";
                this.current_question_id = drNext["question_id"].ToString();
                drNext["state"] = "true";
                string html = this.GetHtmlFile(current_question_id);
                this.editor1.Clear();
                if (html != string.Empty)
                {
                    this.editor1.DocumentText = html;
                }
                if (index == 0)
                {
                    this.metroBtnPre.Enabled = false;
                }
                else
                {
                    this.metroBtnPre.Enabled = true;
                }
                this.metroBtnPre.Enabled = true;

                this.reset_test_status();
            }
        }
        //设置当前题目的答题状态，包括作为和饼图
        void reset_test_status()
        {
            DataRow[] rows = this.dtQuestion_answer_record.Select(string.Format("question_id = '{0}'", current_question_id));
            int total_student_count = this.studentInfoTable.Rows.Count;
            int iAnswered = rows.Length;
            int iUnknown = total_student_count - iAnswered;
            string strUnknown = iUnknown.ToString();
            string strAnswered = iAnswered.ToString();
            //m_panelDrawing.Values = new decimal[] { iUnknown, iAnswered };
            //m_panelDrawing.Texts = new string[] { iUnknown.ToString(), iAnswered.ToString() };

            MiddleWareCore.set_mode(MiddleWareMode.课堂测验);
        }
        private void btnStart_Click(object sender, EventArgs e)
        {
            if (this.dtQuestion.Rows.Count > 0)
            {
                DataRow dr0 = this.dtQuestion.Rows[0];
                current_question_id = dr0["question_id"].ToString();
                dr0["state"] = "true";
                string html = this.GetHtmlFile(current_question_id);
                this.editor1.Clear();
                if (html != string.Empty)
                {
                    this.editor1.DocumentText = html;
                }
                this.metroBtnPre.Enabled = true;
                this.metroBtnNext.Enabled = true;
            }
            if (this.dtQuestion.Rows.Count <= 1)
            {
                this.metroBtnPre.Enabled = false;
                this.metroBtnNext.Enabled = false;
            }
            this.metroBtnStart.Enabled = false;
            this.reset_test_status();
        }
        public void receive_a_new_event()
        {
            this.handle_event();
        }
        void handle_event()
        {
            IntelligentEvent evt = MiddleWareCore.get_a_event();
            if (evt != null)
            {
                deleControlInvoke dele = delegate(object o)
                {
                    IntelligentEvent p = (IntelligentEvent)o;
                    string epcID = p.epcID;
                    string remoteDeviceID = p.remoteDeviceID;
                    string check_time = p.time_stamp;
                    string studentName = string.Empty;
                    string question_value = p.questionValue;
                    DataRow[] rows = null;
                    DataRow[] rowsMap = null;
                    int totalCount = this.studentInfoTable.Rows.Count;

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

                                studentName = person.name;
                                Program.frmClassRoom.changeChairState(groupIndex, ep.formatedPosition(), studentName);
                                MemoryTable.setEquipmentInfoCombineStudentID(ep, person.epc);

                                MemoryTable.setPersonAnswer(epcID, question_value);

                                Program.frmClassRoom.changeChairState(groupIndex, ep.formatedPosition(), DocumentFileState.Green);
                            }
                            //rows = this.studentInfoTable.Select("STUDENTID = '" + epcID + "'");
                            //rowsMap = this.mapConfigsTable.Select("EQUIPEMNTID = '" + remoteDeviceID + "'");

                            //if (rows.Length > 0 && rowsMap.Length > 0)
                            //{
                            //    int groupIndex = int.Parse(rowsMap[0]["IGROUP"].ToString());
                            //    studentName = (string)rows[0]["NAME"];

                            //    Carbinet _carbinet = this.groups[groupIndex];
                            //    _carbinet.setDocText(remoteDeviceID, studentName);

                            //    //这里要处理一下同一个学生用不一个设备发送答案的情况
                            //    // 根据就是每一次客户端发送信息时，服务端都要把发送过来的标签和设备重新绑定一次
                            //    // 如果之前绑定过并且和现在的不同，则说明该标签之前用别的设备发送过信息
                            //    DataRow[] rowsForDuplicate = this.mapConfigsTable.Select("studenID = '" + epcID + "'");
                            //    if (rowsForDuplicate.Length > 0)//说明已经有过对应
                            //    {
                            //        //if (((string)rowsForDuplicate[0]["EQUIPEMNTID"]) != remoteDeviceID)//根据设备和根据学号找的记录不一样，肯定有重复
                            //        {
                            //            int groupIndex2 = int.Parse(rowsForDuplicate[0]["IGROUP"].ToString());
                            //            Carbinet _carbinet2 = this.groups[groupIndex2];
                            //            _carbinet2.setDocBGImage((string)rowsForDuplicate[0]["EQUIPEMNTID"], imgNormal);
                            //            _carbinet2.setDocText((string)rowsForDuplicate[0]["EQUIPEMNTID"], "");
                            //            rowsForDuplicate[0]["studenID"] = "";
                            //        }
                            //    }

                            //    rowsMap[0]["studenID"] = epcID;//这里把设备和标签绑定到一起
                            //    _carbinet.setDocBGImage(remoteDeviceID, imgAnswered);

                            //}
                        }
                        //如果重复发送之外，还改变了问题的答案
                        if (p.event_unit_list.IndexOf(IntelligentEventUnit.change_answer) >= 0)
                        {
                            MemoryTable.setPersonAnswer(epcID, question_value);

                            //更新答题记录
                            rows = this.dtQuestion_answer_record.Select(string.Format("student_id = '{0}' and question_id = {1}", epcID, current_question_id));
                            if (rows.Length > 0)
                            {
                                rows[0]["answer"] = question_value;
                            }
                            else
                            {
                                dtQuestion_answer_record.Rows.Add(new object[3] { epcID, current_question_id, question_value });
                            }
                        }
                    }
                    else
                        if (p.event_unit_list.IndexOf(IntelligentEventUnit.new_epc) >= 0)
                        {
                            //处理该事件需要更新数据和显示页面
                            if (person != null && ep != null)
                            {
                                MemoryTable.setPersonAnswer(epcID, question_value);
                                MemoryTable.setEquipmentInfoCombineStudentID(ep, epcID);
                                Program.frmClassRoom.changeChairState(ep.group, ep.formatedPosition(), person.name);

                                DataRow[] rowsUnknown = MemoryTable.getPersonAnswerRows("B");
                                DataRow[] rowsAnswered = MemoryTable.getPersonAnswerRows("A");
                                int iUnknown = rowsUnknown.Length;
                                int iAnswered = rowsAnswered.Length;
                                string strUnknown = iUnknown.ToString();
                                string strAnswered = iAnswered.ToString();

                                PieChart1.Items.Clear();
                                PieChart1.Items.Add(new PieChartItem(iAnswered, this.clrAnswered, iAnswered.ToString(), "A", 0));
                                PieChart1.Items.Add(new PieChartItem(iUnknown, this.clrNotKnown, iUnknown.ToString(), "B", 10));

                            }

                            //更新答题记录
                            rows = this.dtQuestion_answer_record.Select(string.Format("student_id = '{0}' and question_id = '{1}'", epcID, current_question_id));
                            if (rows.Length > 0)
                            {
                                rows[0]["answer"] = question_value;
                            }
                            else
                            {
                                dtQuestion_answer_record.Rows.Add(new object[3] { epcID, current_question_id, question_value });
                            }
                        }
                };

                this.Invoke(dele, evt);
            }
        }
        #endregion
        #region 内部函数

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

        Image imgNormal = (Image)global::Carbinet.Properties.Resources.grey;
        Image imgA = (Image)global::Carbinet.Properties.Resources.red;
        Image imgB = (Image)global::Carbinet.Properties.Resources.orange;
        Image imgC = (Image)global::Carbinet.Properties.Resources.blue;
        Image imgD = (Image)global::Carbinet.Properties.Resources.pink;
        Image imgAnswered = (Image)global::Carbinet.Properties.Resources.orange;


        #endregion

        private void metroButton1_Click(object sender, EventArgs e)
        {
            Program.frmClassRoom.ShowDialog();
        }






    }
}
