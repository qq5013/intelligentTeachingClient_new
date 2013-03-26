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


            //this.initialInfoTable();
            //InitializePanelControl();
            //InitialClassRoom();

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
