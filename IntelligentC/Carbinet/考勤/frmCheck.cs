using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
using intelligentMiddleWare;
using System.Threading;
using MetroFramework.Forms;
using Nexus.Windows.Forms;

namespace Carbinet
{
    public partial class frmCheck : MetroForm, I_event_notify
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

        Color clrChecked = Color.FromArgb(243, 119, 53);
        Color clrUncheck = Color.FromArgb(0, 174, 219);
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

            PieChart1.ItemStyle.SurfaceAlphaTransparency = 0.92F;
            PieChart1.FocusedItemStyle.SurfaceAlphaTransparency = 0.92F;
            PieChart1.FocusedItemStyle.SurfaceBrightnessFactor = 0.3F;
            PieChart1.Inclination = 1.047F;
            PieChart1.AutoSizePie = true;
            PieChart1.Thickness = 31;
            PieChart1.Rotation = 0.1396263F;
            PieChart1.ShowEdges = false;
            //PieChart1.Radius = 90F;

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

            this.FormClosing += new FormClosingEventHandler(Form1_FormClosing);
            this.FormClosed += new FormClosedEventHandler(Form1_FormClosed);
            this.VisibleChanged += new EventHandler(Form1_VisibleChanged);

            MiddleWareCore.set_mode(MiddleWareMode.考勤, this);
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
                            //m_panelDrawing.Values = new decimal[] { uncheckedCount, checkedCount };
                            string strchecked = "", strUnchecked = "";
                            if (checkedCount > 0)
                            {
                                strchecked = (checkedCount / (checkedCount + uncheckedCount)).ToString() + "%";
                            }
                            if (uncheckedCount > 0)
                            {
                                strUnchecked = (uncheckedCount / (checkedCount + uncheckedCount)).ToString() + "%";
                            }

                        }
                    }
                };

                this.Invoke(dele, evt);
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
            int total = this.studentInfoTable.Rows.Count;
            setPieItem(0, total, 0);
        }
        private void setPieItem(int a, int b, int offset)
        {
            PieChart1.Items.Clear();
            PieChart1.Items.Add(new PieChartItem(a, this.clrChecked, "", "出勤", 0));
            PieChart1.Items.Add(new PieChartItem(b, this.clrUncheck, "", "缺勤", offset));
        }
        #endregion
        #region 事件处理
        void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Application.Exit();
        }
        void Form1_VisibleChanged(object sender, EventArgs e)
        {
            this.Top = Program.frmFloat.Height + Program.frmFloat.Top;
            this.Left = Program.frmFloat.Left;
            if (this.Visible == true)
            {
                this.clearSelectStatus();
                MiddleWareCore.set_mode(MiddleWareMode.无, this);
            }
        }
        void clearSelectStatus()
        {
            Program.frmClassRoom.resetClassRoomState();
            //MemoryTable.resetAllPersonAnswer("X");//默认X
            this.InitializePanelControl();
        }
        void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //保存考勤信息
            if (this.SaveCheckInfo())
            {

            }
            MiddleWareCore.set_mode(MiddleWareMode.无);
        }

        public void receive_a_new_event()
        {
            this.handle_event();
        }

        #endregion



    }
}
