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
        DataTable studentInfoTable = null;
        DataTable mapConfigsTable = null;
        DataTable dtRoomConfig = null;
        //保存学生发送的答案
        DataTable dtAnswerRecord = null;
        DataTable dtStudentAndEquipmentCombining = null;

        #region 各个选项对应的颜色
        Color clrA = Color.FromArgb(0, 177, 89);
        Color clrB = Color.FromArgb(243, 119, 53);
        Color clrC = Color.FromArgb(209, 17, 65);
        Color clrD = Color.FromArgb(0, 174, 219);
        #endregion

        #endregion
        #region 初始化
        public frmSelect()
        {
            InitializeComponent();

            #region InitializeComponent

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

            #endregion

            this.initialInfoTable();

            this.FormClosing += new FormClosingEventHandler(thisFormClosing);
            this.VisibleChanged += new EventHandler(frmSelect_VisibleChanged);
        }
        #region 窗体事件
        void frmSelect_VisibleChanged(object sender, EventArgs e)
        {
            this.Top = Program.frmFloat.Height + Program.frmFloat.Top;
            this.Left = Program.frmFloat.Left;
            if (this.Visible == true)
            {
                this.setFormToInitialState();
                MiddleWareCore.set_mode(MiddleWareMode.实时互动, this);
            }
        }

        void thisFormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        #endregion

        private void setPieToInitializeState()
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
            this.dtRoomConfig = MemoryTable.dtRoomConfig;
            this.studentInfoTable = MemoryTable.studentInfoTable;
            this.mapConfigsTable = MemoryTable.mapConfigsTable;

            this.dtAnswerRecord = new DataTable();
            this.dtAnswerRecord.Columns.Add("answer", typeof(string));
            this.dtAnswerRecord.Columns.Add("studenID", typeof(string));

            //学生ID与设备ID绑定，查找学生位置时，先通过本表查询设备ID，在通过设备ID确定学生位置
            this.dtStudentAndEquipmentCombining = new DataTable();
            this.dtStudentAndEquipmentCombining.Columns.Add("studenID", typeof(string));
            this.dtStudentAndEquipmentCombining.Columns.Add("equipmentID", typeof(string));

        }

        #endregion
        #region 工具函数
        void setFormToInitialState()
        {
            Program.frmClassRoom.resetClassRoomState();
            this.setPersonAnswer("D");//默认都选D，也就是不选择
            this.setPieToInitializeState();
        }
        private void setPersonAnswer(string answer)
        {
            int total = this.dtAnswerRecord.Rows.Count;
            for (int i = 0; i < total; i++)
            {
                DataRow dr = this.dtAnswerRecord.Rows[i];
                dr["answer"] = answer;
            }
        }
        private void setPersonAnswer(string studentID, string answer)
        {
            DataRow[] rows = this.dtAnswerRecord.Select(string.Format("studenID = '{0}'", studentID));
            if (rows.Length > 0)
            {
                rows[0]["answer"] = answer;
            }
        }
        private DocumentFileState getStateByAnswer(string answer)
        {
            DocumentFileState dfs = DocumentFileState.InitialState;
            switch (answer)
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
            return dfs;
        }
        private void clearEquipmentAndStudentCombining(string studentID)
        {
            DataRow[] rows = this.dtStudentAndEquipmentCombining.Select(string.Format("studenID = '{0}'", studentID));
            if (rows.Length > 0)
            {
                this.dtStudentAndEquipmentCombining.Rows.Remove(rows[0]);
            }
        }
        private void setEquipmentInfoCombineStudentID(string studentID, string equipmentID)
        {
            this.dtStudentAndEquipmentCombining.Rows.Add(new object[] { studentID, equipmentID });
        }
        #endregion

        #region 事件处理

        private void button4_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }

        private void btnClearState_Click(object sender, EventArgs e)
        {
            this.setFormToInitialState();
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

                    //1 更改本地信息
                    //2 更改饼图
                    //3 更改座位状态

                    int totalCount = this.studentInfoTable.Rows.Count;

                    Person person = MemoryTable.getPersonByEpc(epcID);
                    equipmentPosition ep = MemoryTable.getEquipmentConfigMapInfo(remoteDeviceID);
                    if (ep != null)
                    {
                        if (IEvent.event_unit_list.IndexOf(IntelligentEventUnit.epc_on_another_device) >= 0)
                        {
                            //这里要处理一下同一个学生用不一个设备发送答案的情况
                            equipmentPosition ep_old = MemoryTable.getEquipmentInfoByStudentID(epcID);
                            this.setChairState(ep_old, DocumentFileState.InitialState, "");
                            MemoryTable.clearEquipmentAndStudentCombining(epcID);
                        }

                        if (person != null)
                        {
                            studentName = person.name;
                            this.setChairState(ep, studentName);
                            MemoryTable.setEquipmentInfoCombineStudentID(ep, person.epc);
                        }

                        this.setPersonAnswer(epcID, question_value);

                        DocumentFileState dfs = this.getStateByAnswer(question_value);
                        this.setChairState(ep, dfs);

                        this.refreshPie();
                    }
                };
                this.Invoke(dele, evt);
            }
        }
        private void setChairState(equipmentPosition ep, string text)
        {
            Program.frmClassRoom.changeChairState(ep.group, ep.formatedPosition(), text);
        }
        private void setChairState(equipmentPosition ep, DocumentFileState dfs, string text)
        {
            setChairState(ep, dfs);
            Program.frmClassRoom.changeChairState(ep.group, ep.formatedPosition(), text);
        }
        private void setChairState(equipmentPosition ep, DocumentFileState dfs)
        {
            Program.frmClassRoom.changeChairState(ep.group, ep.formatedPosition(), dfs);
        }
        private void refreshPie()
        {
            DataRow[] rowsA = this.dtAnswerRecord.Select("answer = 'A'");
            DataRow[] rowsB = this.dtAnswerRecord.Select("answer = 'B'");
            DataRow[] rowsC = this.dtAnswerRecord.Select("answer = 'C'");
            DataRow[] rowsD = this.dtAnswerRecord.Select("answer = 'D'");
            int iA = rowsA.Length;
            int iB = rowsB.Length;
            int iC = rowsC.Length;
            int iD = rowsD.Length;
            string strA = "", strB = "", strC = "", strD = "";
            int totalCount = this.studentInfoTable.Rows.Count;

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
        #endregion
    }
}
