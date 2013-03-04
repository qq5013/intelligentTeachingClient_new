using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using intelligentMiddleWare;

namespace Carbinet
{
    public partial class frmRTTest : Form, I_event_notify
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
        #endregion
        #region 初始化


        public frmRTTest()
        {
            InitializeComponent();

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

            this.Shown += new EventHandler(frmRTTest_Shown);
            this.FormClosing += new FormClosingEventHandler(frmRTTest_FormClosing);
            this.VisibleChanged += new EventHandler(frmRTTest_VisibleChanged);
            MiddleWareCore.event_receiver = this;
        }

        void frmRTTest_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible == true)
            {
                if (this.studentInfoTable != null)
                {
                    for (int i = 0; i < this.studentInfoTable.Rows.Count; i++)
                    {
                        studentInfoTable.Rows[i]["answer"] = "";
                    }
                }
            }
        }

        void frmRTTest_FormClosing(object sender, FormClosingEventArgs e)
        {
            //commonSocket.eventDataReived = null;
            for (int i = 0; i < studentInfoTable.Rows.Count; i++)
            {
                DataRow dr = studentInfoTable.Rows[i];
                dr["answer"] = "";
            }
            this.save_answer_info();
            //if (this.eventInvokeCommand != null)
            //{
            //    this.eventInvokeCommand(InternalCommand.CloseForm, null);
            //}
        }

        void frmRTTest_Shown(object sender, EventArgs e)
        {
            this.initialInfoTable();
            InitializePanelControl();

            this.groupBoxChair.Top = 98;

            InitialClassRoom();

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
        #region 各个选项对应的颜色
        Color clrNotKnown = Color.FromArgb(227, 229, 232);
        Color clrA = Color.Red;
        Color clrB = Color.Green;
        Color clrC = Color.Blue;
        Color clrD = Color.Purple;
        Color clrAnswered = Color.FromArgb(245, 118, 49);
        #endregion
        private void InitializePanelControl()
        {
            m_panelDrawing.LeftMargin = 10;
            m_panelDrawing.RightMargin = 10;
            m_panelDrawing.TopMargin = 10;
            m_panelDrawing.BottomMargin = 10;
            m_panelDrawing.FitChart = true;
            m_panelDrawing.EdgeLineWidth = 1;
            //m_panelDrawing.Values = new decimal[] { 0, 0 };
            m_panelDrawing.Values = new decimal[] { this.studentInfoTable.Rows.Count, 0 };
            //            m_panelDrawing.Values = new decimal[] { this.studentInfoTable.Rows.Count, 0, 0, 0, 0 };
            int alpha = 160;

            //m_panelDrawing.Colors = new Color[] {Color.FromArgb(alpha, clrNotKnown),
            //                                Color.FromArgb(alpha, clrA), 
            //                                Color.FromArgb(alpha, clrB),
            //                                Color.FromArgb(alpha, clrC)
            //                             ,  Color.FromArgb(alpha, clrD)};
            m_panelDrawing.Colors = new Color[] {Color.FromArgb(alpha, clrNotKnown),
                                          Color.FromArgb(alpha, clrAnswered)};
            //m_panelDrawing.SliceRelativeDisplacements = new float[] { 0.1F, 0.2F, 0.2F, 0.2F };
            m_panelDrawing.Texts = new string[] { this.studentInfoTable.Rows.Count.ToString(), "0" };
            //            m_panelDrawing.Texts = new string[] { "100%", "", "", "", "" };
            //            m_panelDrawing.Texts = new string[] { "未知", "A", "B", "C", "D" };
            //m_panelDrawing.ToolTips = new string[] { "尚未选择", 
            //                           "选择A","选择B","选择C","选择D"};
            m_panelDrawing.ToolTips = new string[] { "尚未选择", 
                                       "已选择"};
            m_panelDrawing.Font = new Font("Arial", 10F);
            m_panelDrawing.ForeColor = SystemColors.WindowText;
            m_panelDrawing.SliceRelativeHeight = 0.1F;
            m_panelDrawing.InitialAngle = -90F;
        }


        private void InitialClassRoom()
        {
            this.button6.Left = (this.pictureBox1.Width - this.button6.Width) / 2 + this.pictureBox1.Left;

            int numberOfGroup = dtRoomConfig.Rows.Count;
            int widthOfRoom = this.pictureBox1.Width;
            int heightOfRow = 38;

            int totalColumns = numberOfGroup;
            DataRow[] rows4Sum = dtRoomConfig.Select("IGROUP=1");
            if (rows4Sum.Length > 0)
            {
                totalColumns = int.Parse(rows4Sum[0]["totalColumn"].ToString());
            }
            int numberOfUnit = totalColumns + numberOfGroup - 1;
            int widthOfUnit = widthOfRoom / numberOfUnit;
            int groupInitialLeft = 0;

            for (int i = 0; i < numberOfGroup; i++)
            {
                int numberofColumn = 1;
                int numberOfRow = 1;

                DataRow[] rows = dtRoomConfig.Select(string.Format("IGROUP={0}", i + 1));
                if (rows.Length > 0)
                {
                    numberofColumn = int.Parse(rows[0]["ICOLUMN"].ToString());
                    numberOfRow = int.Parse(rows[0]["IROW"].ToString());
                }
                int groupWidth = numberofColumn * widthOfUnit;

                Carbinet group = new Carbinet(this.pictureBox1.Controls);
                group.Left = groupInitialLeft;
                group.Top = 67;
                this.groups.Add(group);
                //初始化每一排的行
                int initialTop = 0;
                for (int irow = 1; irow <= numberOfRow; irow++, initialTop = initialTop + (int)(1.7 * heightOfRow))
                {
                    CarbinetFloor row = new CarbinetFloor(group, irow, this.pictureBox1.Controls);
                    row.Width = groupWidth;
                    row.Height = heightOfRow;
                    row.relativeTop = initialTop;
                    row.relativeLeft = 0;

                    group.AddFloor(row);

                    for (int k = 1; k <= numberofColumn; k++)
                    {
                        // 如果座位与设备已经设置绑定的话，则在此处将座位与设备ID相挂钩
                        DataRow[] rowsMap = mapConfigsTable.Select(
                            string.Format("IGROUP = {0} and IROW = {1} and ICOLUMN = {2}",
                                            i.ToString(), irow.ToString(), k.ToString()));

                        string _equipmentID = i.ToString() + "," + irow.ToString() + "," + k.ToString();
                        if (rowsMap.Length > 0)
                        {
                            _equipmentID = (string)rowsMap[0]["EQUIPEMNTID"];
                        }
                        DocumentFile df = new DocumentFile(_equipmentID, irow);
                        df.Width = widthOfUnit;
                        df.Height = heightOfRow;
                        df.carbinetIndex = i;
                        df.floorNumber = irow;
                        df.columnNumber = k;
                        df.indexBase = k.ToString();
                        df.Click += new EventHandler(df_Click);
                        group.AddDocFile(df);
                    }

                }
                groupInitialLeft += groupWidth + widthOfUnit;
            }
        }
        #endregion
        #region 事件处理
        //单击座位时的处理
        void df_Click(object sender, EventArgs e)
        {
            DocumentFile df = (DocumentFile)sender;
            string studentID = null;
            DataRow[] rows = this.mapConfigsTable.Select("EQUIPEMNTID = '" + df.name + "'");
            if (rows.Length > 0)
            {
                studentID = (string)rows[0]["studenID"];
                if (studentID == null || studentID.Length <= 0)
                {
                    return;
                }
                frmShowStudentInfo frm = new frmShowStudentInfo(studentID);
                frm.ShowDialog();
            }
        }
        //显示座位、题目
        private void button3_Click(object sender, EventArgs e)
        {
            if (this.showChair)//当前正在显示座位
            {
                this.groupBoxQuestion.Visible = true;
                this.groupBoxChair.Visible = false;
                this.button3.Text = "显示座位";
                this.showChair = false;
            }
            else
            {
                this.groupBoxChair.Visible = true;
                this.groupBoxQuestion.Visible = false;
                this.button3.Text = "显示题目";
                this.showChair = true;

            }
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
                    this.btnNext.Enabled = false;
                }
                else
                {
                    this.btnNext.Enabled = true;
                }
                this.btnPre.Enabled = true;

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
                    this.btnPre.Enabled = false;
                }
                else
                {
                    this.btnPre.Enabled = true;
                }
                this.btnNext.Enabled = true;

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
            m_panelDrawing.Values = new decimal[] { iUnknown, iAnswered };
            m_panelDrawing.Texts = new string[] { iUnknown.ToString(), iAnswered.ToString() };

            MiddleWareCore.set_mode(MiddleWareMode.课堂测验);
            //for (int j = 0; j < this.studentInfoTable.Rows.Count; j++)
            //{
            //    DataRow dr = this.studentInfoTable.Rows[j];
            //    string student_id = dr["STUDENTID"].ToString();
            //    DataRow[] rowsForDuplicate = this.mapConfigsTable.Select("studenID = '" + student_id + "'");
            //    if (rowsForDuplicate.Length > 0)//说明已经有过对应
            //    {
            //        int groupIndex2 = int.Parse(rowsForDuplicate[0]["IGROUP"].ToString());
            //        Carbinet _carbinet2 = this.groups[groupIndex2];
            //        _carbinet2.setDocBGImage((string)rowsForDuplicate[0]["EQUIPEMNTID"], imgNormal);
            //        _carbinet2.setDocText((string)rowsForDuplicate[0]["EQUIPEMNTID"], "");
            //        rowsForDuplicate[0]["studenID"] = "";
            //    }
            //}
            //for (int i = 0; i < rows.Length; i++)
            //{
            //    string student_id = rows[i]["student_id"].ToString();
            //    rows = this.studentInfoTable.Select("STUDENTID = '" + student_id + "'");
            //    rowsMap = this.mapConfigsTable.Select("EQUIPEMNTID = '" + remoteDeviceID + "'");
            //    if (rows.Length > 0 && rowsMap.Length > 0)
            //    {
            //        string answer = question_value;
            //        int groupIndex = int.Parse(rowsMap[0]["IGROUP"].ToString());
            //        studentName = (string)rows[0]["NAME"];

            //        Carbinet _carbinet = this.groups[groupIndex];
            //        _carbinet.setDocText(remoteDeviceID, studentName);

            //        //这里要处理一下同一个学生用不一个设备发送答案的情况
            //        // 根据就是每一次客户端发送信息时，服务端都要把发送过来的标签和设备重新绑定一次
            //        // 如果之前绑定过并且和现在的不同，则说明该标签之前用别的设备发送过信息


            //        rowsMap[0]["studenID"] = epcID;//这里把设备和标签绑定到一起
            //        _carbinet.setDocBGImage(remoteDeviceID, imgAnswered);

            //    }
            //}

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
                this.btnPre.Enabled = true;
                this.btnNext.Enabled = true;
            }
            if (this.dtQuestion.Rows.Count <= 1)
            {
                this.btnNext.Enabled = false;
                this.btnPre.Enabled = false;
            }
            this.btnStart.Enabled = false;
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
                    DataRow[] rowsUnknown = null;
                    int totalCount = this.studentInfoTable.Rows.Count;

                    //如果只是重复发送，不需要做什么
                    if (p.event_unit_list.IndexOf(IntelligentEventUnit.repeat_epc) >= 0)
                    {
                        //如果重复发送之外，还改变了设备的ID
                        if (p.event_unit_list.IndexOf(IntelligentEventUnit.epc_on_another_device) >= 0)
                        {
                            rows = this.studentInfoTable.Select("STUDENTID = '" + epcID + "'");
                            rowsMap = this.mapConfigsTable.Select("EQUIPEMNTID = '" + remoteDeviceID + "'");
                            if (rows.Length > 0 && rowsMap.Length > 0)
                            {
                                string answer = question_value;
                                int groupIndex = int.Parse(rowsMap[0]["IGROUP"].ToString());
                                studentName = (string)rows[0]["NAME"];

                                Carbinet _carbinet = this.groups[groupIndex];
                                _carbinet.setDocText(remoteDeviceID, studentName);

                                //这里要处理一下同一个学生用不一个设备发送答案的情况
                                // 根据就是每一次客户端发送信息时，服务端都要把发送过来的标签和设备重新绑定一次
                                // 如果之前绑定过并且和现在的不同，则说明该标签之前用别的设备发送过信息
                                DataRow[] rowsForDuplicate = this.mapConfigsTable.Select("studenID = '" + epcID + "'");
                                if (rowsForDuplicate.Length > 0)//说明已经有过对应
                                {
                                    //if (((string)rowsForDuplicate[0]["EQUIPEMNTID"]) != remoteDeviceID)//根据设备和根据学号找的记录不一样，肯定有重复
                                    {
                                        int groupIndex2 = int.Parse(rowsForDuplicate[0]["IGROUP"].ToString());
                                        Carbinet _carbinet2 = this.groups[groupIndex2];
                                        _carbinet2.setDocBGImage((string)rowsForDuplicate[0]["EQUIPEMNTID"], imgNormal);
                                        _carbinet2.setDocText((string)rowsForDuplicate[0]["EQUIPEMNTID"], "");
                                        rowsForDuplicate[0]["studenID"] = "";
                                    }
                                }

                                rowsMap[0]["studenID"] = epcID;//这里把设备和标签绑定到一起
                                _carbinet.setDocBGImage(remoteDeviceID, imgAnswered);

                            }
                        }
                        //如果重复发送之外，还改变了问题的答案
                        if (p.event_unit_list.IndexOf(IntelligentEventUnit.change_answer) >= 0)
                        {
                            rows = this.studentInfoTable.Select("STUDENTID = '" + epcID + "'");
                            if (rows.Length > 0)
                            {
                                rows[0]["answer"] = question_value;
                            }
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
                            rows = this.studentInfoTable.Select("STUDENTID = '" + epcID + "'");
                            rowsMap = this.mapConfigsTable.Select("EQUIPEMNTID = '" + remoteDeviceID + "'");
                            if (rows.Length > 0 && rowsMap.Length > 0)
                            {
                                string answer = question_value;
                                int groupIndex = int.Parse(rowsMap[0]["IGROUP"].ToString());
                                studentName = (string)rows[0]["NAME"];

                                Carbinet _carbinet = this.groups[groupIndex];
                                _carbinet.setDocText(remoteDeviceID, studentName);

                                rowsMap[0]["studenID"] = epcID;//这里把设备和标签绑定到一起
                                _carbinet.setDocBGImage(remoteDeviceID, imgAnswered);
                                rows[0]["answer"] = question_value;

                                /*
                                if (answer == "A")
                                {
                                    rows[0]["answer"] = "A";
                                    // _carbinet.setDocBGColor(data.equipmentID, this.clrA);
                                    //_carbinet.setDocBGImage(data.equipmentID, imgA);
                                }
                                if (answer == "B")
                                {
                                    //_carbinet.setDocBGImage(data.equipmentID, imgB);
                                    rows[0]["answer"] = "B";
                                }
                                if (answer == "C")
                                {
                                    rows[0]["answer"] = "C";
                                    //_carbinet.setDocBGImage(data.equipmentID, imgC);
                                    //_carbinet.setDocBGColor(data.equipmentID, this.clrC);
                                }
                                if (answer == "D")
                                {
                                    rows[0]["answer"] = "D";
                                    //_carbinet.setDocBGImage(data.equipmentID, imgD);
                                    //_carbinet.setDocBGColor(data.equipmentID, this.clrD);
                                }
                                //*/
                                //设置饼图
                                rowsUnknown = this.studentInfoTable.Select("answer = ''");
                                int iUnknown = rowsUnknown.Length;
                                int iAnswered = totalCount - iUnknown;
                                string strUnknown = iUnknown.ToString();
                                string strAnswered = iAnswered.ToString();
                                m_panelDrawing.Values = new decimal[] { iUnknown, iAnswered };
                                m_panelDrawing.Texts = new string[] { iUnknown.ToString(), iAnswered.ToString() };

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
                    //switch (p.name)
                    //{
                    //    case IntelligentEvent.class_question_repeat_answer:
                    //        //考勤数据和显示页面都不需要更新，
                    //        break;
                    //    case IntelligentEvent.class_question_new_answer:
                    //        //处理该事件需要更新数据和显示页面
                    //        rows = this.studentInfoTable.Select("STUDENTID = '" + epcID + "'");
                    //        rowsMap = this.mapConfigsTable.Select("EQUIPEMNTID = '" + remoteDeviceID + "'");
                    //        if (rows.Length > 0 && rowsMap.Length > 0)
                    //        {
                    //            string answer = question_value;
                    //            int groupIndex = int.Parse(rowsMap[0]["IGROUP"].ToString());
                    //            studentName = (string)rows[0]["NAME"];

                    //            Carbinet _carbinet = this.groups[groupIndex];
                    //            _carbinet.setDocText(remoteDeviceID, studentName);

                    //            rowsMap[0]["studenID"] = epcID;//这里把设备和标签绑定到一起
                    //            _carbinet.setDocBGImage(remoteDeviceID, imgAnswered);
                    //            ///*
                    //            if (answer == "A")
                    //            {
                    //                rows[0]["answer"] = "A";
                    //                // _carbinet.setDocBGColor(data.equipmentID, this.clrA);
                    //                //_carbinet.setDocBGImage(data.equipmentID, imgA);
                    //            }
                    //            if (answer == "B")
                    //            {
                    //                //_carbinet.setDocBGImage(data.equipmentID, imgB);
                    //                rows[0]["answer"] = "B";
                    //            }
                    //            if (answer == "C")
                    //            {
                    //                rows[0]["answer"] = "C";
                    //                //_carbinet.setDocBGImage(data.equipmentID, imgC);
                    //                //_carbinet.setDocBGColor(data.equipmentID, this.clrC);
                    //            }
                    //            if (answer == "D")
                    //            {
                    //                rows[0]["answer"] = "D";
                    //                //_carbinet.setDocBGImage(data.equipmentID, imgD);
                    //                //_carbinet.setDocBGColor(data.equipmentID, this.clrD);
                    //            }
                    //            //*/
                    //            //设置饼图
                    //            rowsUnknown = this.studentInfoTable.Select("answer = ''");
                    //            int iUnknown = rowsUnknown.Length;
                    //            int iAnswered = totalCount - iUnknown;
                    //            string strUnknown = iUnknown.ToString();
                    //            string strAnswered = iAnswered.ToString();
                    //            m_panelDrawing.Values = new decimal[] { iUnknown, iAnswered };
                    //            m_panelDrawing.Texts = new string[] { iUnknown.ToString(), iAnswered.ToString() };

                    //        }

                    //        //更新答题记录
                    //        rows = this.dtQuestion_answer_record.Select(string.Format("student_id = '{0}' and question_id = '{1}'", epcID, current_question_id));
                    //        if (rows.Length > 0)
                    //        {
                    //            rows[0]["answer"] = question_value;
                    //        }
                    //        else
                    //        {
                    //            dtQuestion_answer_record.Rows.Add(new object[3] { epcID, current_question_id, question_value });
                    //        }
                    //        break;
                    //    case IntelligentEvent.class_question_repeat_answer_on_another_device:
                    //        //数据不需要更新，但是显示页面需要更新
                    //        rows = this.studentInfoTable.Select("STUDENTID = '" + epcID + "'");
                    //        rowsMap = this.mapConfigsTable.Select("EQUIPEMNTID = '" + remoteDeviceID + "'");
                    //        if (rows.Length > 0 && rowsMap.Length > 0)
                    //        {
                    //            string answer = question_value;
                    //            int groupIndex = int.Parse(rowsMap[0]["IGROUP"].ToString());
                    //            studentName = (string)rows[0]["NAME"];

                    //            Carbinet _carbinet = this.groups[groupIndex];
                    //            _carbinet.setDocText(remoteDeviceID, studentName);

                    //            //这里要处理一下同一个学生用不一个设备发送答案的情况
                    //            // 根据就是每一次客户端发送信息时，服务端都要把发送过来的标签和设备重新绑定一次
                    //            // 如果之前绑定过并且和现在的不同，则说明该标签之前用别的设备发送过信息
                    //            DataRow[] rowsForDuplicate = this.mapConfigsTable.Select("studenID = '" + epcID + "'");
                    //            if (rowsForDuplicate.Length > 0)//说明已经有过对应
                    //            {
                    //                //if (((string)rowsForDuplicate[0]["EQUIPEMNTID"]) != remoteDeviceID)//根据设备和根据学号找的记录不一样，肯定有重复
                    //                {
                    //                    int groupIndex2 = int.Parse(rowsForDuplicate[0]["IGROUP"].ToString());
                    //                    Carbinet _carbinet2 = this.groups[groupIndex2];
                    //                    _carbinet2.setDocBGImage((string)rowsForDuplicate[0]["EQUIPEMNTID"], imgNormal);
                    //                    _carbinet2.setDocText((string)rowsForDuplicate[0]["EQUIPEMNTID"], "");
                    //                    rowsForDuplicate[0]["studenID"] = "";
                    //                }
                    //            }

                    //            rowsMap[0]["studenID"] = epcID;//这里把设备和标签绑定到一起
                    //            _carbinet.setDocBGImage(remoteDeviceID, imgAnswered);

                    //        }
                    //        break;
                    //    case IntelligentEvent.class_question_change_answer:
                    //        rows = this.studentInfoTable.Select("STUDENTID = '" + epcID + "'");
                    //        if (rows.Length > 0)
                    //        {
                    //            string answer = question_value;

                    //            ///*
                    //            if (answer == "A")
                    //            {
                    //                rows[0]["answer"] = "A";
                    //            }
                    //            if (answer == "B")
                    //            {
                    //                rows[0]["answer"] = "B";
                    //            }
                    //            if (answer == "C")
                    //            {
                    //                rows[0]["answer"] = "C";
                    //            }
                    //            if (answer == "D")
                    //            {
                    //                rows[0]["answer"] = "D";
                    //            }

                    //        }
                    //        //更新答题记录
                    //        rows = this.dtQuestion_answer_record.Select(string.Format("student_id = '{0}' and question_id = {1}", epcID, current_question_id));
                    //        if (rows.Length > 0)
                    //        {
                    //            rows[0]["answer"] = question_value;
                    //        }
                    //        else
                    //        {
                    //            dtQuestion_answer_record.Rows.Add(new object[3] { epcID, current_question_id, question_value });
                    //        }
                    //        break;
                    //    case IntelligentEvent.class_question_change_answer_on_another_device:
                    //        rows = this.studentInfoTable.Select("STUDENTID = '" + epcID + "'");
                    //        rowsMap = this.mapConfigsTable.Select("EQUIPEMNTID = '" + remoteDeviceID + "'");
                    //        if (rows.Length > 0 && rowsMap.Length > 0)
                    //        {
                    //            string answer = question_value;
                    //            int groupIndex = int.Parse(rowsMap[0]["IGROUP"].ToString());
                    //            studentName = (string)rows[0]["NAME"];

                    //            Carbinet _carbinet = this.groups[groupIndex];
                    //            _carbinet.setDocText(remoteDeviceID, studentName);

                    //            //这里要处理一下同一个学生用不一个设备发送答案的情况
                    //            // 根据就是每一次客户端发送信息时，服务端都要把发送过来的标签和设备重新绑定一次
                    //            // 如果之前绑定过并且和现在的不同，则说明该标签之前用别的设备发送过信息
                    //            DataRow[] rowsForDuplicate = this.mapConfigsTable.Select("studenID = '" + epcID + "'");
                    //            if (rowsForDuplicate.Length > 0)//说明已经有过对应
                    //            {
                    //                int groupIndex2 = int.Parse(rowsForDuplicate[0]["IGROUP"].ToString());
                    //                Carbinet _carbinet2 = this.groups[groupIndex2];
                    //                _carbinet2.setDocBGImage((string)rowsForDuplicate[0]["EQUIPEMNTID"], imgNormal);
                    //                _carbinet2.setDocText((string)rowsForDuplicate[0]["EQUIPEMNTID"], "");
                    //                rowsForDuplicate[0]["studenID"] = "";
                    //            }


                    //            rowsMap[0]["studenID"] = epcID;//这里把设备和标签绑定到一起
                    //            _carbinet.setDocBGImage(remoteDeviceID, imgAnswered);

                    //            if (answer == "A")
                    //            {
                    //                rows[0]["answer"] = "A";
                    //            }
                    //            if (answer == "B")
                    //            {
                    //                rows[0]["answer"] = "B";
                    //            }
                    //            if (answer == "C")
                    //            {
                    //                rows[0]["answer"] = "C";
                    //            }
                    //            if (answer == "D")
                    //            {
                    //                rows[0]["answer"] = "D";
                    //            }

                    //        }
                    //        //更新答题记录
                    //        rows = this.dtQuestion_answer_record.Select(string.Format("student_id = '{0}' and question_id = {1}", epcID, current_question_id));
                    //        if (rows.Length > 0)
                    //        {
                    //            rows[0]["answer"] = question_value;
                    //        }
                    //        else
                    //        {
                    //            dtQuestion_answer_record.Rows.Add(new object[3] { epcID, current_question_id, question_value });
                    //        }
                    //        break;
                    //}

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



    }
}
