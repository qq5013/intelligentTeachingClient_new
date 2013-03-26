using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using intelligentMiddleWare;
using MetroFramework.Forms;

namespace Carbinet
{
    public partial class frmSelect : MetroForm, I_event_notify
    {
        #region 成员
        //public Form previousForm = null;
        //frmInfo4Student frmStudent = null;
        //bool teachingState = false;
        //string path = null;
        //bool showPPT = true;
        // 物资大小 50*32
        // 位置 159,158
        // 层大小 113 * 38
        // 每层的物资的间隔 6
        List<Carbinet> groups = new List<Carbinet>();
        public DataTable studentInfoTable = null;
        public DataTable mapConfigsTable = null;
        DataTable dtRoomConfig = null;

        #region 各个选项对应的颜色
        Color clrA = Color.FromArgb(0, 177, 89);
        Color clrB = Color.FromArgb(243, 119, 53);
        //Color clrB = Color.FromArgb(244, 119, 52);
        Color clrC = Color.FromArgb(209, 17, 65);
        Color clrD = Color.FromArgb(0, 174, 219);
        //Color clrE_purple = Color.FromArgb(181, 33, 239);
        #endregion
        //EquipmentConfigCtl ctl = new EquipmentConfigCtl();
        //studentInfoCtl stuCtl = new studentInfoCtl();
        //roomConfigCtl configCtl = new roomConfigCtl();
        //bool bSeatVisible = true;

        //int form_initial_heigth = 0;
        //int form_initial_width = 0;
        //int right_groups_initial_width = 0;
        //int right_groups_initial_left = 0;
        //int shown_width = 0;
        //int shown_heigth = 0;
        // 291,147
        #endregion
        #region 初始化
        public frmSelect()
        {
            InitializeComponent();

            //this.form_initial_width = this.Width;
            //this.right_groups_initial_left = this.groupBox_command.Left;
            //this.right_groups_initial_width = this.groupBox_command.Width;
            //this.form_initial_heigth = this.Height;
            //this.shown_width = this.right_groups_initial_width + 15;
            //this.shown_heigth = this.groupBox_command.Height + this.groupPie.Height + 30;
            //this.btn1.BackColor = this.clrNotKnown;
            //this.btn2.BackColor = this.clrA;
            //this.btn3.BackColor = this.clrB;
            //this.btn4.BackColor = this.clrC;
            //this.btn5.BackColor = this.clrD;


            //this.initialInfoTable();
            //InitializePanelControl();
            //InitialClassRoom();

            //this.groupBoxChair.Visible = fals;

            //bSeatVisible = true;
            //this.groupBoxChair.Visible = false;
            //this.btnHideSeat.Text = "显示座位(&S)";
            //this.Width = this.shown_width;
            //this.Height = this.shown_heigth;
            //this.Left = this.Left + this.form_initial_width - this.shown_width;
            //this.groupPie.Left = 5;
            //this.groupBox_command.Left = 5;
            //this.groupPie.Left = 5;
            //this.btnMin.Left = this.Width - 100 - 5;
            //this.btnClearState.Left = this.btnMin.Left - 100;
            //this.btnQuit.Left = this.Width - 10 - this.btnQuit.Width;
            Screen[] screens = System.Windows.Forms.Screen.AllScreens;
            for (int i = 0; i < screens.Length; i++)
            {
                Screen sc = screens[i];
                if (sc.Primary == true)
                {
                    Rectangle rect = sc.WorkingArea;
                    //this.Left = (int)(rect.Width * 0.7);
                    this.Left = (int)(rect.Width - this.Width - 50);
                    this.Top = (int)(rect.Height * 0.1);
                }
            }

            this.initialInfoTable();
            InitializePanelControl();
            //InitialClassRoom();
            Program.frmClassRoom.resetClassRoomState();

            this.FormClosing += new FormClosingEventHandler(Form1_FormClosing);
            this.VisibleChanged += new EventHandler(frmSelect_VisibleChanged);
            MiddleWareCore.event_receiver = this;
            MiddleWareCore.set_mode(MiddleWareMode.实时互动);
        }



        void frmSelect_VisibleChanged(object sender, EventArgs e)
        {
            this.Top = Program.frmFloat.Height + Program.frmFloat.Top;
            this.Left = Program.frmFloat.Left;
            if (this.Visible == true)
            {
                this.clearSelectStatus();
                MiddleWareCore.event_receiver = this;
            }

        }


        void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        private void InitializePanelControl()
        {
            m_panelDrawing.LeftMargin = 10;
            m_panelDrawing.RightMargin = 10;
            m_panelDrawing.TopMargin = 10;
            m_panelDrawing.BottomMargin = 10;
            m_panelDrawing.FitChart = true;
            m_panelDrawing.EdgeLineWidth = 0;
            m_panelDrawing.Values = new decimal[] { 0, 0, 0, this.studentInfoTable.Rows.Count };
            int alpha = 100;

            m_panelDrawing.Colors = new Color[] {Color.FromArgb(alpha, clrA),
                                            Color.FromArgb(alpha, clrB), 
                                            Color.FromArgb(alpha, clrC),
                                            Color.FromArgb(alpha, clrD)};
            //m_panelDrawing.SliceRelativeDisplacements = new float[] { 0.1F, 0.2F, 0.2F, 0.2F };
            m_panelDrawing.Texts = new string[] { "0%", "0%", "0%", "100%" };
            //            m_panelDrawing.Texts = new string[] { "未知", "A", "B", "C", "D" };
            m_panelDrawing.ToolTips = new string[] { "选择A", "选择B", "选择C", "选择D" };
            m_panelDrawing.Font = new Font("Arial", 10F);
            m_panelDrawing.ForeColor = Color.White;
            m_panelDrawing.SliceRelativeHeight = 0.1F;
            m_panelDrawing.InitialAngle = -90F;
            //m_panelDrawing.in
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


        }

        private void InitialClassRoom()
        {
            //this.button6.Left = (this.pictureBox1.Width - this.button6.Width) / 2 + this.pictureBox1.Left;

            //int numberOfGroup = dtRoomConfig.Rows.Count;
            //int widthOfRoom = this.pictureBox1.Width;
            //int heightOfRow = 38;

            //int totalColumns = numberOfGroup;
            //DataRow[] rows4Sum = dtRoomConfig.Select("IGROUP=1");
            //if (rows4Sum.Length > 0)
            //{
            //    totalColumns = int.Parse(rows4Sum[0]["totalColumn"].ToString());
            //}
            //int numberOfUnit = totalColumns + numberOfGroup - 1;
            //int widthOfUnit = widthOfRoom / numberOfUnit;
            //int groupInitialLeft = 0;

            //for (int i = 0; i < numberOfGroup; i++)
            //{
            //    int numberofColumn = 1;
            //    int numberOfRow = 1;

            //    DataRow[] rows = dtRoomConfig.Select(string.Format("IGROUP={0}", i + 1));
            //    if (rows.Length > 0)
            //    {
            //        numberofColumn = int.Parse(rows[0]["ICOLUMN"].ToString());
            //        numberOfRow = int.Parse(rows[0]["IROW"].ToString());
            //    }
            //    int groupWidth = numberofColumn * widthOfUnit;

            //    Carbinet group = new Carbinet(this.pictureBox1.Controls);
            //    group.Left = groupInitialLeft;
            //    group.Top = 67;
            //    this.groups.Add(group);
            //    //初始化每一排的行
            //    int initialTop = 0;
            //    for (int irow = 1; irow <= numberOfRow; irow++, initialTop = initialTop + (int)(1.7 * heightOfRow))
            //    {
            //        CarbinetFloor row = new CarbinetFloor(group, irow, this.pictureBox1.Controls);
            //        row.Width = groupWidth;
            //        row.Height = heightOfRow;
            //        row.relativeTop = initialTop;
            //        row.relativeLeft = 0;

            //        group.AddFloor(row);

            //        for (int k = 1; k <= numberofColumn; k++)
            //        {
            //            // 如果座位与设备已经设置绑定的话，则在此处将座位与设备ID相挂钩
            //            DataRow[] rowsMap = mapConfigsTable.Select(
            //                string.Format("IGROUP = {0} and IROW = {1} and ICOLUMN = {2}",
            //                                i.ToString(), irow.ToString(), k.ToString()));

            //            string _equipmentID = i.ToString() + "," + irow.ToString() + "," + k.ToString();
            //            if (rowsMap.Length > 0)
            //            {
            //                _equipmentID = (string)rowsMap[0]["EQUIPEMNTID"];
            //            }
            //            DocumentFile df = new DocumentFile(_equipmentID, irow);
            //            df.Width = widthOfUnit;
            //            df.Height = heightOfRow;
            //            df.carbinetIndex = i;
            //            df.floorNumber = irow;
            //            df.columnNumber = k;
            //            df.indexBase = k.ToString();
            //            df.Click += new EventHandler(df_Click);
            //            group.AddDocFile(df);
            //        }

            //    }
            //    groupInitialLeft += groupWidth + widthOfUnit;
            //}
        }
        #endregion
        #region 内部函数
        //void StaticSerialPort_evtParseReceivedData_frmSelect(ProtocolHelper helper)
        //{
        //    deleControlInvoke dele = delegate(object o)
        //    {
        //        ProtocolHelper p = (ProtocolHelper)o;
        //        DataRow[] rows = null;
        //        DataRow[] rowsMap = null;
        //        int totalCount = this.studentInfoTable.Rows.Count;
        //        //                int totalCount = this.infoTable.Rows.Count;
        //        //rows = this.infoTable.Select("equipmentID = '" + data.equipmentID + "'");
        //        rows = this.studentInfoTable.Select("STUDENTID = '" + p.epcID + "'");
        //        rowsMap = this.mapConfigsTable.Select("EQUIPEMNTID = '" + p.remoteDeviceID + "'");
        //        string studentName = string.Empty;
        //        if (rows.Length > 0 && rowsMap.Length > 0)
        //        {
        //            string answer = p.questionValue;
        //            int groupIndex = int.Parse(rowsMap[0]["IGROUP"].ToString());
        //            studentName = (string)rows[0]["NAME"];

        //            Carbinet _carbinet = this.groups[groupIndex];
        //            _carbinet.setDocText(p.remoteDeviceID, studentName);

        //            //这里要处理一下同一个学生用不一个设备发送答案的情况
        //            DataRow[] rowsForDuplicate = this.mapConfigsTable.Select("studenID = '" + p.epcID + "'");
        //            if (rowsForDuplicate.Length > 0)//说明已经有过对应
        //            {
        //                if (((string)rowsForDuplicate[0]["EQUIPEMNTID"]) != p.remoteDeviceID)//根据设备和根据学号找的记录不一样，肯定有重复
        //                {
        //                    int groupIndex2 = int.Parse(rowsForDuplicate[0]["IGROUP"].ToString());
        //                    Carbinet _carbinet2 = this.groups[groupIndex2];
        //                    _carbinet2.setDocBGImage((string)rowsForDuplicate[0]["EQUIPEMNTID"], (Image)global::Carbinet.Properties.Resources.grey);
        //                    _carbinet2.setDocText((string)rowsForDuplicate[0]["EQUIPEMNTID"], "");
        //                    rowsForDuplicate[0]["studenID"] = "";
        //                }
        //            }


        //            rowsMap[0]["studenID"] = p.epcID;


        //            if (answer == "A")
        //            {
        //                rows[0]["answer"] = "A";
        //                // _carbinet.setDocBGColor(data.equipmentID, this.clrA);
        //                _carbinet.setDocBGImage(p.remoteDeviceID, (Image)global::Carbinet.Properties.Resources.yellow);
        //            }
        //            if (answer == "B")
        //            {
        //                _carbinet.setDocBGImage(p.remoteDeviceID, (Image)global::Carbinet.Properties.Resources.orange);
        //                rows[0]["answer"] = "B";
        //            }
        //            if (answer == "C")
        //            {
        //                rows[0]["answer"] = "C";
        //                _carbinet.setDocBGImage(p.remoteDeviceID, (Image)global::Carbinet.Properties.Resources.blue);
        //                //_carbinet.setDocBGColor(data.equipmentID, this.clrC);
        //            }
        //            if (answer == "D")
        //            {
        //                rows[0]["answer"] = "D";
        //                _carbinet.setDocBGImage(p.remoteDeviceID, (Image)global::Carbinet.Properties.Resources.purple);
        //                //_carbinet.setDocBGColor(data.equipmentID, this.clrD);
        //            }

        //            DataRow[] rowsA = this.studentInfoTable.Select("answer = 'A'");
        //            DataRow[] rowsB = this.studentInfoTable.Select("answer = 'B'");
        //            DataRow[] rowsC = this.studentInfoTable.Select("answer = 'C'");
        //            DataRow[] rowsD = this.studentInfoTable.Select("answer = 'D'");
        //            int iA = rowsA.Length;
        //            int iB = rowsB.Length;
        //            int iC = rowsC.Length;
        //            int iD = rowsD.Length;
        //            int iUnknown = totalCount - iA - iB - iC - iD;
        //            m_panelDrawing.Values = new decimal[] { iUnknown, iA, iB, iC, iD };
        //            string strA = "", strB = "", strC = "", strD = "", strUnknown = "";

        //            if (iUnknown > 0)
        //            {
        //                strUnknown = (iUnknown * 100 / totalCount).ToString() + "%";
        //            }
        //            if (iA > 0)
        //            {
        //                strA = (iA * 100 / totalCount).ToString() + "%";
        //            }
        //            if (iB > 0)
        //            {
        //                strB = (iB * 100 / totalCount).ToString() + "%";
        //            }
        //            if (iC > 0)
        //            {
        //                strC = (iC * 100 / totalCount).ToString() + "%";
        //            }
        //            if (iD > 0)
        //            {
        //                strD = (iD * 100 / totalCount).ToString() + "%";
        //            }
        //            m_panelDrawing.Texts = new string[] { strUnknown, strA, strB, strC, strD };

        //        }
        //    };
        //    this.Invoke(dele, helper);
        //}
        //void disposeReceivedData(Data data)
        //{
        //    this.Invoke(new deleControlInvoke(this.updateStatus), data);

        //}
        //private void updateStatus(object o)
        //{
        //    Data data = (Data)o;
        //    if (data.key == ((int)Mode.单选).ToString())
        //    {
        //        DataRow[] rows = null;
        //        DataRow[] rowsMap = null;
        //        int totalCount = this.studentInfoTable.Rows.Count;
        //        //                int totalCount = this.infoTable.Rows.Count;
        //        //rows = this.infoTable.Select("equipmentID = '" + data.equipmentID + "'");
        //        rows = this.studentInfoTable.Select("STUDENTID = '" + data.tagID + "'");
        //        rowsMap = this.mapConfigsTable.Select("EQUIPEMNTID = '" + data.equipmentID + "'");
        //        string studentName = string.Empty;
        //        if (rows.Length > 0 && rowsMap.Length > 0)
        //        {
        //            string answer = data.value;
        //            int groupIndex = int.Parse(rowsMap[0]["IGROUP"].ToString());
        //            studentName = (string)rows[0]["NAME"];

        //            Carbinet _carbinet = this.groups[groupIndex];
        //            _carbinet.setDocText(data.equipmentID, studentName);

        //            //这里要处理一下同一个学生用不一个设备发送答案的情况
        //            DataRow[] rowsForDuplicate = this.mapConfigsTable.Select("studenID = '" + data.tagID + "'");
        //            if (rowsForDuplicate.Length > 0)//说明已经有过对应
        //            {
        //                if (((string)rowsForDuplicate[0]["EQUIPEMNTID"]) != data.equipmentID)//根据设备和根据学号找的记录不一样，肯定有重复
        //                {
        //                    int groupIndex2 = int.Parse(rowsForDuplicate[0]["IGROUP"].ToString());
        //                    Carbinet _carbinet2 = this.groups[groupIndex2];
        //                    _carbinet2.setDocBGImage((string)rowsForDuplicate[0]["EQUIPEMNTID"], (Image)global::Carbinet.Properties.Resources.yellow);
        //                    _carbinet2.setDocText((string)rowsForDuplicate[0]["EQUIPEMNTID"], "");
        //                    rowsForDuplicate[0]["studenID"] = "";
        //                }
        //            }


        //            rowsMap[0]["studenID"] = data.tagID;


        //            if (answer == "A")
        //            {
        //                rows[0]["answer"] = "A";
        //                // _carbinet.setDocBGColor(data.equipmentID, this.clrA);
        //                _carbinet.setDocBGImage(data.equipmentID, (Image)global::Carbinet.Properties.Resources.red);
        //            }
        //            if (answer == "B")
        //            {
        //                _carbinet.setDocBGImage(data.equipmentID, (Image)global::Carbinet.Properties.Resources.orange);
        //                rows[0]["answer"] = "B";
        //            }
        //            if (answer == "C")
        //            {
        //                rows[0]["answer"] = "C";
        //                _carbinet.setDocBGImage(data.equipmentID, (Image)global::Carbinet.Properties.Resources.blue);
        //                //_carbinet.setDocBGColor(data.equipmentID, this.clrC);
        //            }
        //            if (answer == "D")
        //            {
        //                rows[0]["answer"] = "D";
        //                _carbinet.setDocBGImage(data.equipmentID, (Image)global::Carbinet.Properties.Resources.pink);
        //                //_carbinet.setDocBGColor(data.equipmentID, this.clrD);
        //            }

        //            DataRow[] rowsA = this.studentInfoTable.Select("answer = 'A'");
        //            DataRow[] rowsB = this.studentInfoTable.Select("answer = 'B'");
        //            DataRow[] rowsC = this.studentInfoTable.Select("answer = 'C'");
        //            DataRow[] rowsD = this.studentInfoTable.Select("answer = 'D'");
        //            int iA = rowsA.Length;
        //            int iB = rowsB.Length;
        //            int iC = rowsC.Length;
        //            int iD = rowsD.Length;
        //            int iUnknown = totalCount - iA - iB - iC - iD;
        //            m_panelDrawing.Values = new decimal[] { iUnknown, iA, iB, iC, iD };
        //            string strA = "", strB = "", strC = "", strD = "", strUnknown = "";

        //            if (iUnknown > 0)
        //            {
        //                strUnknown = (iUnknown * 100 / totalCount).ToString() + "%";
        //            }
        //            if (iA > 0)
        //            {
        //                strA = (iA * 100 / totalCount).ToString() + "%";
        //            }
        //            if (iB > 0)
        //            {
        //                strB = (iB * 100 / totalCount).ToString() + "%";
        //            }
        //            if (iC > 0)
        //            {
        //                strC = (iC * 100 / totalCount).ToString() + "%";
        //            }
        //            if (iD > 0)
        //            {
        //                strD = (iD * 100 / totalCount).ToString() + "%";
        //            }
        //            m_panelDrawing.Texts = new string[] { strUnknown, strA, strB, strC, strD };
        //            //if (totalCount > 0)
        //            //{
        //            //    m_panelDrawing.ToolTips = new string[] {
        //            //                    string.Format("尚未选择:{0}%",iUnknown*100/totalCount), 
        //            //                   string.Format("选择A:{0}%",(iA*100/totalCount)),
        //            //                   string.Format("选择B:{0}%",(iB*100/totalCount)),
        //            //                  string.Format( "选择C:{0}%",(iC*100/totalCount)),
        //            //                   string.Format("选择D:{0}%",(iD*100/totalCount))};


        //            //}
        //            //else
        //            //{
        //            //    m_panelDrawing.ToolTips = new string[] { "尚未选择", 
        //            //                   "选择A","选择B","选择C","选择D"};


        //            //}
        //        }
        //    }
        //}

        void clearSelectStatus()
        {
            Program.frmClassRoom.resetClassRoomState();
            MemoryTable.resetAllPersonAnswer("D");//默认都选A，也就是不选择

            m_panelDrawing.Values = new decimal[] { 0, 0, 0, this.studentInfoTable.Rows.Count };
            m_panelDrawing.Texts = new string[] { "0%", "0%", "0%", "100%" };
        }

        #endregion

        #region 事件处理


        //void df_Click(object sender, EventArgs e)
        //{
        //    DocumentFile df = (DocumentFile)sender;
        //    string studentID = null;
        //    DataRow[] rows = this.mapConfigsTable.Select("EQUIPEMNTID = '" + df.name + "'");
        //    if (rows.Length > 0)
        //    {
        //        //rows[0]["sdudentID"] = data.tagID;
        //        //if (((string)rows[0]["studentName"]) == null || ((string)rows[0]["studentName"]).Length <= 0)
        //        //{
        //        //    rows[0]["studentName"] = data.tagID;//todo 这里应该检索学生信息
        //        //}
        //        //rows[0]["status"] = "1";

        //        studentID = (string)rows[0]["studenID"];
        //        if (studentID == null || studentID.Length <= 0)
        //        {
        //            return;
        //        }
        //        frmShowStudentInfo frm = new frmShowStudentInfo(studentID);
        //        frm.ShowDialog();
        //    }
        //}


        private void button4_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }

        private void btnClearState_Click(object sender, EventArgs e)
        {
            this.clearSelectStatus();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            bool cutVisibleState = Program.frmClassRoom.Visible;
            if (cutVisibleState == true)
            {
                Program.frmClassRoom.Visible = false;
            }
            else
            {
                Program.frmClassRoom.Visible = true;
            }
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
                    IntelligentEvent IEvent = (IntelligentEvent)o;
                    string epcID = IEvent.epcID;
                    string remoteDeviceID = IEvent.remoteDeviceID;
                    string check_time = IEvent.time_stamp;
                    string studentName = string.Empty;
                    string question_value = IEvent.questionValue;

                    int totalCount = this.studentInfoTable.Rows.Count;

                    Person person = MemoryTable.getPersonByEpc(epcID);
                    equipmentPosition ep = MemoryTable.getEquipmentConfigMapInfo(remoteDeviceID);
                    if (ep != null)
                    {
                        int groupIndex = ep.group;

                        if (IEvent.event_unit_list.IndexOf(IntelligentEventUnit.epc_on_another_device) >= 0)
                        {
                            //这里要处理一下同一个学生用不一个设备发送答案的情况
                            equipmentPosition ep_old = MemoryTable.getEquipmentInfoByStudentID(epcID);
                            Program.frmClassRoom.changeChairState(ep_old.group, ep_old.formatedPosition(), DocumentFileState.InitialState);
                            Program.frmClassRoom.changeChairState(ep_old.group, ep_old.formatedPosition(), "");
                            MemoryTable.clearEquipmentAndStudentCombining(epcID);
                        }

                        if (person != null)
                        {
                            studentName = person.name;
                            Program.frmClassRoom.changeChairState(groupIndex, ep.formatedPosition(), studentName);
                            MemoryTable.setEquipmentInfoCombineStudentID(ep, person.epc);
                        }

                        MemoryTable.setPersonAnswer(epcID, question_value);
                        DocumentFileState dfs = DocumentFileState.InitialState;
                        switch (question_value)
                        {
                            case "A":
                                dfs = DocumentFileState.Green;
                                break;
                            case "B":
                                dfs = DocumentFileState.Orange;
                                break;
                            case "C":
                                dfs = DocumentFileState.Red;
                                break;
                            case "D":
                                dfs = DocumentFileState.Blue;
                                break;
                        }
                        Program.frmClassRoom.changeChairState(groupIndex, ep.formatedPosition(), dfs);

                        DataRow[] rowsA = this.studentInfoTable.Select("answer = 'A'");
                        DataRow[] rowsB = this.studentInfoTable.Select("answer = 'B'");
                        DataRow[] rowsC = this.studentInfoTable.Select("answer = 'C'");
                        DataRow[] rowsD = this.studentInfoTable.Select("answer = 'D'");
                        //DataRow[] rowsE = this.studentInfoTable.Select("answer = 'E'");
                        int iA = rowsA.Length;
                        int iB = rowsB.Length;
                        int iC = rowsC.Length;
                        int iD = rowsD.Length;
                        //int iE = rowsE.Length;
                        //int iUnknown = totalCount - iA - iB - iC - iD - iE;
                        m_panelDrawing.Values = new decimal[] { iA, iB, iC, iD };
                        string strA = "", strB = "", strC = "", strD = "";

                        //if (iUnknown > 0)
                        //{
                        //    strUnknown = (iUnknown * 100 / totalCount).ToString() + "%";
                        //}
                        if (totalCount > 0)
                        {
                            strA = (iA * 100 / totalCount).ToString() + "%";
                            strB = (iB * 100 / totalCount).ToString() + "%";
                            strC = (iC * 100 / totalCount).ToString() + "%";
                            strD = (iD * 100 / totalCount).ToString() + "%";
                        }
                        //if (iB > 0)
                        //{
                        //}
                        //if (iC > 0)
                        //{
                        //    strC = (iC * 100 / totalCount).ToString() + "%";
                        //}
                        //if (iD > 0)
                        //{
                        //    strD = (iD * 100 / totalCount).ToString() + "%";
                        //}
                        m_panelDrawing.Texts = new string[] { strA, strB, strC, strD };

                    }

                    //DataRow[] rowsStudentInfo = null;
                    //DataRow[] rowsMap = null;
                    //rowsStudentInfo = this.studentInfoTable.Select("STUDENTID = '" + epcID + "'");
                    //rowsMap = this.mapConfigsTable.Select("EQUIPEMNTID = '" + remoteDeviceID + "'");
                    //if (rowsStudentInfo.Length > 0 && rowsMap.Length > 0)//有该用户资料和设备资料
                    //{
                    //int groupIndex = int.Parse(rowsMap[0]["IGROUP"].ToString());
                    //studentName = (string)rowsStudentInfo[0]["NAME"];
                    //Carbinet _carbinet = this.groups[groupIndex];

                    //if (IEvent.event_unit_list.IndexOf(IntelligentEventUnit.epc_on_another_device) >= 0)
                    //{
                    //    //这里要处理一下同一个学生用不一个设备发送答案的情况
                    //    equipmentPosition ep_old = MemoryTable.getEquipmentInfoByStudentID(epcID);
                    //    //DataRow[] rowsForDuplicate = this.mapConfigsTable.Select("studenID = '" + epcID + "'");
                    //    //int groupIndex2 = int.Parse(rowsForDuplicate[0]["IGROUP"].ToString());
                    //    //string equipId = (string)rowsForDuplicate[0]["EQUIPEMNTID"];
                    //    Program.frmClassRoom.changeChairState(ep_old.group, ep_old.equipmentID, DocumentFileState.InitialState);
                    //    Program.frmClassRoom.changeChairState(ep_old.group, ep_old.equipmentID, "");
                    //    MemoryTable.clearEquipmentAndStudentCombining(epcID);
                    //    //rowsForDuplicate[0]["studenID"] = "";
                    //}
                    ////}
                    //Program.frmClassRoom.changeChairState(groupIndex, remoteDeviceID, studentName);

                    //rowsMap[0]["studenID"] = epcID;


                    //}
                };

                this.Invoke(dele, evt);
            }
        }
        #endregion
    }
}
