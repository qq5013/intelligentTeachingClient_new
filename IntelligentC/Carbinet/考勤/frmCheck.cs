using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using intelligentMiddleWare;
using System.Threading;

namespace Carbinet
{
    public partial class frmCheck : Form, I_event_notify
    {

        #region Members
        BackgroundWorker backgroundWorker1 = new BackgroundWorker();
        public static ManualResetEvent MRE_wait_event_block = new ManualResetEvent(false);

        // 物资大小 50*32
        // 位置 159,158
        // 层大小 113 * 38
        // 每层的物资的间隔 6
        public DataTable studentInfoTable = null;
        public DataTable mapConfigsTable = null;
        DataTable dtRoomConfig = null;

        List<Carbinet> groups = new List<Carbinet>();

        studentInfoCtl stuCtl = new studentInfoCtl();
        EquipmentConfigCtl ctl = new EquipmentConfigCtl();
        roomConfigCtl configCtl = new roomConfigCtl();

        string dtStart, dtEnd;
        string check_record_id = string.Empty;

        Thread thread_listening;
        #endregion
        // 291,147
        public frmCheck(string record_id, string dtStart, string dtEnd)
            : this()
        {
            this.check_record_id = record_id;
            this.dtEnd = dtEnd;
            this.dtStart = dtStart;
        }
        public frmCheck()
        {
            InitializeComponent();
            this.initialInfoTable();
            InitializePanelControl();

            InitialClassRoom();

            this.Shown += new EventHandler(Form1_Shown);
            this.FormClosing += new FormClosingEventHandler(Form1_FormClosing);
            this.FormClosed += new FormClosedEventHandler(Form1_FormClosed);
            this.VisibleChanged += new EventHandler(Form1_VisibleChanged);
            MiddleWareCore.event_receiver = this;
            MiddleWareCore.set_mode(MiddleWareMode.考勤);

            //thread_listening = new Thread(new ThreadStart(begin_wait_event));
            //thread_listening.Start();
            //backgroundWorker1.DoWork += new DoWorkEventHandler(BackgroundThreadWork);

            //backgroundWorker1.WorkerSupportsCancellation = true;
            //this.backgroundWorker1.WorkerReportsProgress = true;
            //this.backgroundWorker1.ProgressChanged += new ProgressChangedEventHandler(backgroundWorker1_ProgressChanged);
            //backgroundWorker1.RunWorkerAsync(null);

        }

        void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            begin_wait_event();

        }

        void BackgroundThreadWork(object sender, DoWorkEventArgs e)
        {
            this.backgroundWorker1.ReportProgress(1);
            //begin_wait_event();
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
                    DataRow[] rows = null;

                    bool bRefresh_ui = false;
                    if (p.event_unit_list.IndexOf(IntelligentEventUnit.epc_on_another_device) >= 0)//重复考勤
                    {
                        //考勤数据不需要更新，但是显示页面需要更新

                        rows = this.studentInfoTable.Select("STUDENTID = '" + epcID + "'");
                        if (rows.Length > 0)
                        {
                            rows[0]["status"] = "1";
                            rows[0]["checkTime"] = check_time;
                            studentName = (string)rows[0]["NAME"];
                        }

                        rows = this.mapConfigsTable.Select("studenID = '" + epcID + "'");
                        if (rows.Length > 0)
                        {
                            //此时需要将之前设为考勤状态的位置变回未考勤状态
                            int groupIndex = int.Parse(rows[0]["IGROUP"].ToString());
                            Carbinet _carbinet = this.groups[groupIndex];
                            _carbinet.setDocBGImage((string)rows[0]["EQUIPEMNTID"], (Image)global::Carbinet.Properties.Resources.grey);
                            _carbinet.setDocText((string)rows[0]["EQUIPEMNTID"], "");
                            rows[0]["studenID"] = "";
                        }
                        //rows = this.mapConfigsTable.Select("EQUIPEMNTID = '" + remoteDeviceID + "'");
                        //if (rows.Length > 0)
                        //{
                        //    rows[0]["studenID"] = epcID;
                        //    int groupIndex = int.Parse(rows[0]["IGROUP"].ToString());
                        //    //界面展示
                        //    Carbinet _carbinet = this.groups[groupIndex];
                        //    _carbinet.setDocBGImage(remoteDeviceID, (Image)global::Carbinet.Properties.Resources.orange);
                        //    _carbinet.setDocText(remoteDeviceID, studentName);
                        //}
                        bRefresh_ui = true;
                    }
                    if (p.event_unit_list.IndexOf(IntelligentEventUnit.new_epc) >= 0)//第一次考勤 
                    {
                        //处理该事件需要更新学生考勤数据和显示页面

                        //更新考勤信息
                        //rows = this.checkTable.Select("equipmentID = '" + data.equipmentID + "'");
                        //根据接收到的信息，首先将学生出勤状态置为 1，之后将控件的显示状态改为绿色
                        if (string.Compare(this.dtStart, check_time) <= 0 && string.Compare(this.dtEnd, check_time) >= 0)
                        {
                            rows = this.studentInfoTable.Select("STUDENTID = '" + epcID + "'");
                            if (rows.Length > 0)
                            {
                                rows[0]["status"] = "1";
                                rows[0]["checkTime"] = check_time;
                                studentName = (string)rows[0]["NAME"];
                            }
                            bRefresh_ui = true;
                        }
                    }
                    if (bRefresh_ui == true)
                    {
                        rows = this.mapConfigsTable.Select("EQUIPEMNTID = '" + remoteDeviceID + "'");
                        if (rows.Length > 0)
                        {
                            rows[0]["studenID"] = epcID;
                            int groupIndex = int.Parse(rows[0]["IGROUP"].ToString());
                            //界面展示
                            Carbinet _carbinet = this.groups[groupIndex];
                            //_carbinet.setDocBGColor(data.equipmentID, Color.Green);
                            _carbinet.setDocBGImage(remoteDeviceID, (Image)global::Carbinet.Properties.Resources.orange);
                            _carbinet.setDocText(remoteDeviceID, studentName);

                            // 查找考勤与未考勤的学生的数量，显示在饼图上
                            rows = this.studentInfoTable.Select("status = '1'");
                            int checkedCount = rows.Length;
                            int uncheckedCount = this.studentInfoTable.Rows.Count - checkedCount;
                            Debug.WriteLine(
                                string.Format("Form1.updateStatus  -> checked = {0} unchecked = {1}"
                                , checkedCount, uncheckedCount));
                            m_panelDrawing.Values = new decimal[] { uncheckedCount, checkedCount };
                            string strchecked = "", strUnchecked = "";
                            if (checkedCount > 0)
                            {
                                strchecked = (checkedCount / (checkedCount + uncheckedCount)).ToString() + "%";
                            }
                            if (uncheckedCount > 0)
                            {
                                strUnchecked = (uncheckedCount / (checkedCount + uncheckedCount)).ToString() + "%";
                            }

                            m_panelDrawing.ToolTips = new string[] { "缺勤："+uncheckedCount.ToString(), 
                                        "出勤："+checkedCount.ToString()};
                        }
                    }
                    //switch (p.name)
                    //{
                    //    case IntelligentEvent.check_repeat_epc:
                    //        //考勤数据和显示页面都不需要更新，
                    //        break;
                    //    case IntelligentEvent.check_new_epc:
                    //        //处理该事件需要更新学生考勤数据和显示页面

                    //        //更新考勤信息
                    //        //rows = this.checkTable.Select("equipmentID = '" + data.equipmentID + "'");
                    //        //根据接收到的信息，首先将学生出勤状态置为 1，之后将控件的显示状态改为绿色
                    //        if (string.Compare(this.dtStart, check_time) <= 0 && string.Compare(this.dtEnd, check_time) >= 0)
                    //        {
                    //            rows = this.studentInfoTable.Select("STUDENTID = '" + epcID + "'");
                    //            if (rows.Length > 0)
                    //            {
                    //                rows[0]["status"] = "1";
                    //                rows[0]["checkTime"] = check_time;
                    //                studentName = (string)rows[0]["NAME"];
                    //            }
                    //            rows = this.mapConfigsTable.Select("EQUIPEMNTID = '" + remoteDeviceID + "'");
                    //            if (rows.Length > 0)
                    //            {
                    //                rows[0]["studenID"] = epcID;
                    //                int groupIndex = int.Parse(rows[0]["IGROUP"].ToString());
                    //                //界面展示
                    //                Carbinet _carbinet = this.groups[groupIndex];
                    //                //_carbinet.setDocBGColor(data.equipmentID, Color.Green);
                    //                _carbinet.setDocBGImage(remoteDeviceID, (Image)global::Carbinet.Properties.Resources.orange);
                    //                _carbinet.setDocText(remoteDeviceID, studentName);

                    //                // 查找考勤与未考勤的学生的数量，显示在饼图上
                    //                rows = this.studentInfoTable.Select("status = '1'");
                    //                int checkedCount = rows.Length;
                    //                int uncheckedCount = this.studentInfoTable.Rows.Count - checkedCount;
                    //                Debug.WriteLine(
                    //                    string.Format("Form1.updateStatus  -> checked = {0} unchecked = {1}"
                    //                    , checkedCount, uncheckedCount));
                    //                m_panelDrawing.Values = new decimal[] { uncheckedCount, checkedCount };
                    //                string strchecked = "", strUnchecked = "";
                    //                if (checkedCount > 0)
                    //                {
                    //                    strchecked = (checkedCount / (checkedCount + uncheckedCount)).ToString() + "%";
                    //                }
                    //                if (uncheckedCount > 0)
                    //                {
                    //                    strUnchecked = (uncheckedCount / (checkedCount + uncheckedCount)).ToString() + "%";
                    //                }

                    //                m_panelDrawing.ToolTips = new string[] { "缺勤："+uncheckedCount.ToString(), 
                    //                    "出勤："+checkedCount.ToString()};
                    //            }
                    //        }

                    //        break;
                    //    case IntelligentEvent.check_repeat_epc_on_another_device:
                    //        //考勤数据不需要更新，但是显示页面需要更新

                    //        rows = this.studentInfoTable.Select("STUDENTID = '" + epcID + "'");
                    //        if (rows.Length > 0)
                    //        {
                    //            rows[0]["status"] = "1";
                    //            rows[0]["checkTime"] = check_time;
                    //            studentName = (string)rows[0]["NAME"];
                    //        }

                    //        rows = this.mapConfigsTable.Select("studenID = '" + epcID + "'");
                    //        if (rows.Length > 0)//至少第二次考勤
                    //        {
                    //            //此时需要将之前设为考勤状态的位置变回未考勤状态
                    //            int groupIndex = int.Parse(rows[0]["IGROUP"].ToString());
                    //            Carbinet _carbinet = this.groups[groupIndex];
                    //            _carbinet.setDocBGImage((string)rows[0]["EQUIPEMNTID"], (Image)global::Carbinet.Properties.Resources.grey);
                    //            _carbinet.setDocText((string)rows[0]["EQUIPEMNTID"], "");
                    //            rows[0]["studenID"] = "";
                    //        }
                    //        rows = this.mapConfigsTable.Select("EQUIPEMNTID = '" + remoteDeviceID + "'");
                    //        if (rows.Length > 0)
                    //        {
                    //            rows[0]["studenID"] = epcID;
                    //            int groupIndex = int.Parse(rows[0]["IGROUP"].ToString());
                    //            //界面展示
                    //            Carbinet _carbinet = this.groups[groupIndex];
                    //            _carbinet.setDocBGImage(remoteDeviceID, (Image)global::Carbinet.Properties.Resources.orange);
                    //            _carbinet.setDocText(remoteDeviceID, studentName);
                    //        }
                    //        break;
                    //}

                };

                this.Invoke(dele, evt);
            }
        }
        void begin_wait_event()
        {
            while (true)
            {
                MRE_wait_event_block.WaitOne();
                MRE_wait_event_block.Reset();


            }
        }

        bool SaveCheckInfo()
        {

            List<CheckInfo> list = new List<CheckInfo>();
            DataRow[] rows = this.studentInfoTable.Select("status = 1");
            //            DataRow[] rows = this.studentInfoTable.Select("status = '1'");
            for (int i = 0; i < rows.Length; i++)
            {

                DataRow dr = rows[i];
                CheckInfo ci = new CheckInfo();
                ci.record_id = this.check_record_id;
                ci.STUDENTID = (string)dr["STUDENTID"];
                ci.CHECK_TIME = (string)dr["checkTime"];

                list.Add(ci);

            }
            bool bDetailInfo = this.stuCtl.AddCheckInfo(list);
            return bDetailInfo;
            //bool bClassInfo = false;
            //int checkedCount = rows.Length;
            //if (this.studentInfoTable.Rows.Count > 0)
            //{
            //    string percentage = (100 * checkedCount / this.studentInfoTable.Rows.Count).ToString();
            //    bClassInfo = this.stuCtl.AddClassCheckInfo(
            //                       DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
            //                       Configures.ClassName,
            //                       percentage);

            //}
            //if (bDetailInfo == true && bClassInfo == true)
            //{
            //    return true;
            //}
            //else
            //{
            //    return false;
            //}

        }



        void StaticSerialPort_evtParseReceivedData(ProtocolHelper helper)
        {
            deleControlInvoke dele = delegate(object o)
            {
                ProtocolHelper _helper = (ProtocolHelper)o;

                string epcID = _helper.epcID;
                string remoteDeviceID = _helper.remoteDeviceID;
                //更新考勤信息
                DataRow[] rows = null;
                //rows = this.checkTable.Select("equipmentID = '" + data.equipmentID + "'");
                //根据接收到的信息，首先将学生出勤状态置为 1，之后将控件的显示状态改为绿色
                rows = this.studentInfoTable.Select("STUDENTID = '" + epcID + "'");
                string studentName = string.Empty;
                if (rows.Length > 0)
                {
                    rows[0]["status"] = "1";
                    rows[0]["checkTime"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    studentName = (string)rows[0]["NAME"];
                }

                //处理多次考勤情况
                rows = this.mapConfigsTable.Select("studenID = '" + epcID + "'");
                if (rows.Length > 0)//至少第二次考勤
                {
                    //此时需要将之前设为考勤状态的位置变回未考勤状态
                    int groupIndex = int.Parse(rows[0]["IGROUP"].ToString());
                    Carbinet _carbinet = this.groups[groupIndex];
                    _carbinet.setDocBGImage((string)rows[0]["EQUIPEMNTID"], (Image)global::Carbinet.Properties.Resources.grey);
                    _carbinet.setDocText((string)rows[0]["EQUIPEMNTID"], "");
                    rows[0]["studenID"] = "";
                }

                //rows = this.checkTable.Select("equipmentID = '" + data.equipmentID + "'");
                rows = this.mapConfigsTable.Select("EQUIPEMNTID = '" + remoteDeviceID + "'");
                if (rows.Length > 0)
                {
                    rows[0]["studenID"] = epcID;
                    int groupIndex = int.Parse(rows[0]["IGROUP"].ToString());
                    //界面展示
                    Carbinet _carbinet = this.groups[groupIndex];
                    //_carbinet.setDocBGColor(data.equipmentID, Color.Green);
                    _carbinet.setDocBGImage(remoteDeviceID, (Image)global::Carbinet.Properties.Resources.orange);
                    _carbinet.setDocText(remoteDeviceID, studentName);

                    rows = this.studentInfoTable.Select("status = '1'");
                    int checkedCount = rows.Length;
                    int uncheckedCount = this.studentInfoTable.Rows.Count - checkedCount;
                    Debug.WriteLine(
                        string.Format("Form1.updateStatus  -> checked = {0} unchecked = {1}"
                        , checkedCount, uncheckedCount));
                    m_panelDrawing.Values = new decimal[] { uncheckedCount, checkedCount };
                    string strchecked = "", strUnchecked = "";
                    if (checkedCount > 0)
                    {
                        strchecked = (checkedCount / (checkedCount + uncheckedCount)).ToString() + "%";
                    }
                    if (uncheckedCount > 0)
                    {
                        strUnchecked = (uncheckedCount / (checkedCount + uncheckedCount)).ToString() + "%";
                    }

                    m_panelDrawing.ToolTips = new string[] { "缺勤："+uncheckedCount.ToString(), 
                                    "出勤："+checkedCount.ToString()};
                }
            };
            this.Invoke(dele, helper);
            //throw new NotImplementedException();
        }
        //void disposeReceivedData(Data data)
        //{
        //    this.Invoke(new deleControlInvoke(this.updateStatus), data);

        //}
        private void updateStatus(object o)
        {
            Data data = (Data)o;
            if (data.key == ((int)Mode.考勤).ToString())
            {
                Debug.WriteLine(
                    string.Format("Form1.updateStatus  -> data = {0}"
                    , data.toString()));
                //更新考勤信息
                DataRow[] rows = null;
                //rows = this.checkTable.Select("equipmentID = '" + data.equipmentID + "'");
                //根据接收到的信息，首先将学生出勤状态置为 1，之后将控件的显示状态改为绿色
                rows = this.studentInfoTable.Select("STUDENTID = '" + data.tagID + "'");
                string studentName = string.Empty;
                if (rows.Length > 0)
                {
                    rows[0]["status"] = "1";
                    rows[0]["checkTime"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    studentName = (string)rows[0]["NAME"];
                }

                //处理多次考勤情况
                rows = this.mapConfigsTable.Select("studenID = '" + data.tagID + "'");
                if (rows.Length > 0)//至少第二次考勤
                {
                    //if (((string)rows[0]["EQUIPEMNTID"]) == data.equipmentID)//如果两次的设备ID相同，则表示在同一台设备上考勤
                    //{

                    //    return;
                    //}
                    //else
                    {
                        //此时需要将之前设为考勤状态的位置变回未考勤状态
                        int groupIndex = int.Parse(rows[0]["IGROUP"].ToString());
                        Carbinet _carbinet = this.groups[groupIndex];
                        _carbinet.setDocBGImage((string)rows[0]["EQUIPEMNTID"], (Image)global::Carbinet.Properties.Resources.red);
                        _carbinet.setDocText((string)rows[0]["EQUIPEMNTID"], "");
                        rows[0]["studenID"] = "";
                    }
                }

                //rows = this.checkTable.Select("equipmentID = '" + data.equipmentID + "'");
                rows = this.mapConfigsTable.Select("EQUIPEMNTID = '" + data.equipmentID + "'");
                if (rows.Length > 0)
                {
                    //rows[0]["sdudentID"] = data.tagID;
                    //if (((string)rows[0]["studentName"]) == null || ((string)rows[0]["studentName"]).Length <= 0)
                    //{
                    //    rows[0]["studentName"] = data.tagID;//todo 这里应该检索学生信息
                    //}
                    //rows[0]["status"] = "1";
                    rows[0]["studenID"] = data.tagID;
                    int groupIndex = int.Parse(rows[0]["IGROUP"].ToString());
                    //界面展示
                    Carbinet _carbinet = this.groups[groupIndex];
                    //_carbinet.setDocBGColor(data.equipmentID, Color.Green);
                    _carbinet.setDocBGImage(data.equipmentID, (Image)global::Carbinet.Properties.Resources.orange);
                    _carbinet.setDocText(data.equipmentID, studentName);

                    rows = this.studentInfoTable.Select("status = '1'");
                    int checkedCount = rows.Length;
                    int uncheckedCount = this.studentInfoTable.Rows.Count - checkedCount;
                    Debug.WriteLine(
                        string.Format("Form1.updateStatus  -> checked = {0} unchecked = {1}"
                        , checkedCount, uncheckedCount));
                    m_panelDrawing.Values = new decimal[] { uncheckedCount, checkedCount };
                    string strchecked = "", strUnchecked = "";
                    if (checkedCount > 0)
                    {
                        strchecked = (checkedCount / (checkedCount + uncheckedCount)).ToString() + "%";
                    }
                    if (uncheckedCount > 0)
                    {
                        strUnchecked = (uncheckedCount / (checkedCount + uncheckedCount)).ToString() + "%";
                    }

                    m_panelDrawing.ToolTips = new string[] { "缺勤："+uncheckedCount.ToString(), 
                                    "出勤："+checkedCount.ToString()};
                    //if (uncheckedCount + checkedCount > 0)
                    //{
                    //    m_panelDrawing.ToolTips = new string[] { "缺勤率：", 
                    //                   "出勤率："};
                    //}
                    //else
                    //{
                    //    m_panelDrawing.ToolTips = new string[] { "缺勤率：0%", 
                    //                   "出勤率：0%"};
                    //}
                }
            }
        }



        //private void button3_Click(object sender, EventArgs e)
        //{

        //}
        #region 初始化函数
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
        private void InitializePanelControl()
        {
            m_panelDrawing.LeftMargin = 10;
            m_panelDrawing.RightMargin = 10;
            m_panelDrawing.TopMargin = 10;
            m_panelDrawing.BottomMargin = 10;
            m_panelDrawing.FitChart = true;
            m_panelDrawing.EdgeLineWidth = 1;
            m_panelDrawing.Values = new decimal[] { this.studentInfoTable.Rows.Count, 0 };
            //            m_panelDrawing.Values = new decimal[] { this.checkTable.Rows.Count, 0 };
            int alpha = 160;
            m_panelDrawing.Colors = new Color[] { Color.FromArgb(alpha, Color.FromArgb(230,232,235)), 
                                    Color.FromArgb(alpha, Color.FromArgb(247,115,41))};
            // 247,115,41 ORANGE
            //m_panelDrawing.SliceRelativeDisplacements = new float[] { 0.1F, 0.2F, 0.2F, 0.2F };
            //m_panelDrawing.Texts = new string[] { "缺勤","出勤"};
            m_panelDrawing.Texts = new string[] { "100%", "" };
            m_panelDrawing.ToolTips = new string[] { "缺勤:100%", 
                                       "出勤:0%"};
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


            /*
            int numberOfGroup = this.ctl.getClassroomConfig(0);
            int numberofColumn = this.ctl.getClassroomConfig(2);
            int numberOfRow = this.ctl.getClassroomConfig(1);
            int widthOfRoom = this.pictureBox1.Width;
            int heightOfRow = 38;

            int numberOfUnit = numberOfGroup * numberofColumn + numberOfGroup - 1;
            int widthOfUnit = widthOfRoom / numberOfUnit;
            int groupInitialLeft = 0;
            int groupWidth = numberofColumn * widthOfUnit;

            for (int i = 0; i < numberOfGroup; i++)
            {
                Carbinet group = new Carbinet(this.pictureBox1.Controls);
                group.Left = groupInitialLeft;
                Debug.WriteLine(
                    string.Format("Form1.InitialClassRoom  -> groupInitialLeft = {0}"
                    , groupInitialLeft.ToString()));
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
                        DataRow[] rows = mapConfigsTable.Select(
                            string.Format("IGROUP = {0} and IROW = {1} and ICOLUMN = {2}",
                                            i.ToString(), irow.ToString(), k.ToString()));

                        string _equipmentID = i.ToString() + "," + irow.ToString() + "," + k.ToString();
                        if (rows.Length > 0)
                        {
                            _equipmentID = (string)rows[0]["EQUIPEMNTID"];
                        }
                        DocumentFile df = new DocumentFile(_equipmentID, irow);
                        df.Width = widthOfUnit;
                        df.Height = heightOfRow;
                        df.carbinetIndex = i;
                        df.floorNumber = irow;
                        df.columnNumber = k;
                        df.indexBase = k.ToString();
                        df.setBackgroundImage(global::Carbinet.Properties.Resources.red);
                        df.Click += new EventHandler(df_Click);
                        group.AddDocFile(df);
                    }
                }
                groupInitialLeft += groupWidth + widthOfUnit;
            }
            */
        }

        #endregion
        #region 事件处理
        void df_Click(object sender, EventArgs e)
        {
            DocumentFile df = (DocumentFile)sender;
            string studentID = null;
            DataRow[] rows = this.mapConfigsTable.Select("EQUIPEMNTID = '" + df.name + "'");
            if (rows.Length > 0)
            {
                //rows[0]["sdudentID"] = data.tagID;
                //if (((string)rows[0]["studentName"]) == null || ((string)rows[0]["studentName"]).Length <= 0)
                //{
                //    rows[0]["studentName"] = data.tagID;//todo 这里应该检索学生信息
                //}
                //rows[0]["status"] = "1";
                studentID = (string)rows[0]["studenID"];
                if (studentID == null || studentID.Length <= 0)
                {
                    return;
                }
                frmShowStudentInfo frm = new frmShowStudentInfo(studentID);
                frm.ShowDialog();
            }
        }
        private void button7_Click(object sender, EventArgs e)
        {
            //frmEquipmentConfig frm = new frmEquipmentConfig();
            //frm.previousForm = this;
            //this.Hide();
            //frm.ShowDialog();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            //frmSelect frm = new frmSelect();
            //frm.previousForm = this;
            //frm.studentInfoTable = this.studentInfoTable;
            //frm.mapConfigsTable = this.mapConfigsTable;
            //this.Hide();
            //frm.Show();
        }
        void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Application.Exit();
        }
        void Form1_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible == true)
            {
                //commonSocket.eventDataReived = disposeReceivedData;
                //StaticDataPort.evtParseReceivedData += new delevoid_ProtocolHelper(StaticSerialPort_evtParseReceivedData);
            }
        }

        void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //commonSocket.eventDataReived = null;
            //StaticSerialPort.evtParseReceivedData -= StaticSerialPort_evtParseReceivedData;
            //StaticDataPort.evtParseReceivedData = null;
            //StaticDataPort.closeDataPort();
            //保存考勤信息
            if (this.SaveCheckInfo())
            {

            }
            MiddleWareCore.event_receiver = null;
            //this.backgroundWorker1.CancelAsync();
            //e.Cancel = true;
            //this.Hide();
        }
        void Form1_Shown(object sender, EventArgs e)
        {
            StaticDataPort.openDataPort();
        }
        public void receive_a_new_event()
        {
            //MRE_wait_event_block.Set();
            this.handle_event();
        }

        #endregion



    }
}
