using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using intelligentMiddleWare;
using MetroFramework.Forms;
using Nexus.Windows.Forms;

namespace Carbinet
{
    public partial class frmSelect : MetroForm, I_event_notify
    {
        #region 成员
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

        // 291,147
        #endregion
        #region 初始化
        public frmSelect()
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

            this.FormClosing += new FormClosingEventHandler(Form1_FormClosing);
            this.VisibleChanged += new EventHandler(frmSelect_VisibleChanged);
        }

        void frmSelect_VisibleChanged(object sender, EventArgs e)
        {
            this.Top = Program.frmFloat.Height + Program.frmFloat.Top;
            this.Left = Program.frmFloat.Left;
            if (this.Visible == true)
            {
                this.clearSelectStatus();
                MiddleWareCore.set_mode(MiddleWareMode.实时互动, this);
            }
        }

        void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        private void InitializePanelControl()
        {
            PieChart1.Items.Clear();
            PieChart1.Items.Add(new PieChartItem(0, this.clrA, "0", "A", 0));
            PieChart1.Items.Add(new PieChartItem(0, this.clrB, "0", "B", 0));
            PieChart1.Items.Add(new PieChartItem(0, this.clrC, "0", "C", 0));
            int total = this.studentInfoTable.Rows.Count;
            PieChart1.Items.Add(new PieChartItem(total, this.clrD, total.ToString(), "D", 0));
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

        #endregion
        #region 内部函数
        void clearSelectStatus()
        {
            Program.frmClassRoom.resetClassRoomState();
            MemoryTable.resetAllPersonAnswer("D");//默认都选A，也就是不选择
            this.InitializePanelControl();
        }
        #endregion

        #region 事件处理

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
                        int iA = rowsA.Length;
                        int iB = rowsB.Length;
                        int iC = rowsC.Length;
                        int iD = rowsD.Length;
                        string strA = "", strB = "", strC = "", strD = "";

                        if (totalCount > 0)
                        {
                            strA = (iA * 100 / totalCount).ToString() + "%";
                            strB = (iB * 100 / totalCount).ToString() + "%";
                            strC = (iC * 100 / totalCount).ToString() + "%";
                            strD = (iD * 100 / totalCount).ToString() + "%";
                        }
                        PieChart1.Items.Clear();
                        PieChart1.Items.Add(new PieChartItem(iA, this.clrA, iA.ToString(), "A " + strA, 0));
                        PieChart1.Items.Add(new PieChartItem(iB, this.clrB, iB.ToString(), "B " + strB, 0));
                        PieChart1.Items.Add(new PieChartItem(iC, this.clrC, iC.ToString(), "C " + strC, 0));
                        PieChart1.Items.Add(new PieChartItem(iD, this.clrD, iD.ToString(), "D " + strD, 10));
                    }
                };
                this.Invoke(dele, evt);
            }
        }
        #endregion
    }
}
