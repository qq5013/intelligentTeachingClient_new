using Config;
using intelligentMiddleWare;
using MetroFramework.Forms;
using Nexus.Windows.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Carbinet
{
    public partial class frmFloat : MetroForm, I_event_notify
    {
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ContextMenu notifyContextMenu;
        private System.Windows.Forms.MenuItem menuItemClose;
        private System.Windows.Forms.MenuItem menuItemSerialConfig;
        private System.Windows.Forms.MenuItem menuItemEquipmentConfig;
        private System.Windows.Forms.MenuItem menuItemQuestionMng;
        private System.Windows.Forms.MenuItem menuItemAnalysis;
        private System.Windows.Forms.MenuItem menuItemStudentMng;
        private System.Windows.Forms.MenuItem menuItemAbout;


        #region 各个选项对应的颜色
        Color clrA = Color.FromArgb(0, 177, 89);
        Color clrB = Color.FromArgb(243, 119, 53);
        Color clrC = Color.FromArgb(209, 17, 65);
        Color clrD = Color.FromArgb(0, 174, 219);
        #endregion

        List<PictureBox> pblst = new List<PictureBox>();
        public frmFloat()
        {
            InitializeComponent();
            pblst.Add(pictureBox5);
            pblst.Add(pictureBox4);
            pblst.Add(pictureBox3);
            pblst.Add(pictureBox2);
            pblst.Add(pictureBox1);


            this.pictureBox5.Image = (Image)global::Carbinet.Properties.Resources.Groups;//显示座位
            this.pictureBox4.Image = (Image)global::Carbinet.Properties.Resources.MB_touch;//考勤
            this.pictureBox3.Image = (Image)global::Carbinet.Properties.Resources.MB_Hand_副本;//实时互动
            this.pictureBox2.Image = (Image)global::Carbinet.Properties.Resources.MB_tasks;//课堂测验
            this.pictureBox1.Image = (Image)global::Carbinet.Properties.Resources.MB_shut_down;//退出

            PieChart1.ItemStyle.SurfaceAlphaTransparency = 0.92F;
            PieChart1.FocusedItemStyle.SurfaceAlphaTransparency = 0.92F;
            PieChart1.FocusedItemStyle.SurfaceBrightnessFactor = 0.3F;
            PieChart1.Inclination = 1.55F;
            PieChart1.AutoSizePie = true;
            PieChart1.Thickness = 31;
            PieChart1.Rotation = 0.0F;
            PieChart1.ShowEdges = false;
            //PieChart1.Radius = 90F;

            //this.Move += Form1_Move;
            Screen[] screens = System.Windows.Forms.Screen.AllScreens;
            for (int i = 0; i < screens.Length; i++)
            {
                Screen sc = screens[i];
                if (sc.Primary == true)
                {
                    Rectangle rect = sc.WorkingArea;
                    //this.Left = (int)(rect.Width - this.Width);
                    //this.Left = (int)(rect.Width * 0.7);
                    this.Top = (int)(rect.Height * 0.0);
                    this.Height = rect.Height;
                    this.Left = 0;
                    this.Width = 85;

                    // 从高度150开始计算按钮所能使用的空间，然后将其垂直居中
                    resetPbYPos(rect.Height);
                }
            }




            //int gap = 3;
            //this.pictureBox3.Left = this.pictureBox4.Left + pictureBox4.Width + gap;
            //this.pictureBox2.Left = this.pictureBox3.Left + pictureBox3.Width + gap;
            //this.pictureBox1.Left = this.pictureBox2.Left + pictureBox2.Width + gap;




            //foreach (PictureBox pb in pblst)
            //{
            //    pb.MouseHover += pictureBox_mouse_hover;
            //    pb.MouseLeave += pictureBox_mouse_leave;
            //}

            //pictureBox4.Click += (sender, e) =>
            //{
            //    frmCheckInit startCheck = new frmCheckInit();
            //    startCheck.ShowDialog();
            //};



            //pictureBox1.Click += (sender, e) =>
            //{
            //    Program.closeAllForms();
            //    this.Close();
            //};
            //pictureBox2.Click += (sender, e) =>
            //    {
            //        frmRTTest frm = new frmRTTest();
            //        //MiddleWareCore.set_mode(MiddleWareMode.课堂测验);
            //        frm.ShowDialog();
            //    };
            pictureBox3.Click += (sender, e) =>
            {
                //Program.frmSelect.Visible = !Program.frmSelect.Visible;
                MiddleWareCore.set_mode(MiddleWareMode.实时互动);
                setPieToInitializeState();
                this.setPersonAnswer("");
            };
            pictureBox5.Click += (sender, e) =>
                {
                    Program.frmClassRoom.ShowDialog();
                };
            //this.initial_popup_menu();
            setPieToInitializeState();
            this.initialInfoTable();
            MiddleWareCore.event_receiver = this;
        }
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
                    IntelligentEvent IEvent = (IntelligentEvent)o;
                    string epcID = IEvent.epcID;
                    string remoteDeviceID = IEvent.remoteDeviceID;
                    string check_time = IEvent.time_stamp;
                    string studentName = string.Empty;
                    string question_value = IEvent.questionValue;

                    //1 更改本地信息
                    //2 更改饼图
                    //3 更改座位状态

                    int totalCount = MemoryTable.studentInfoTable.Rows.Count;

                    Person person = MemoryTable.getPersonByEpc(epcID);
                    equipmentPosition ep = MemoryTable.getEquipmentConfigMapInfo(remoteDeviceID);
                    if (ep != null)
                    {
                        if (IEvent.event_unit_list.IndexOf(IntelligentEventUnit.epc_on_another_device) >= 0)
                        {
                            ////这里要处理一下同一个学生用不一个设备发送答案的情况
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
        private void setChairState(equipmentPosition ep, DocumentFileState dfs, string text)
        {
            setChairState(ep, dfs);
            Program.frmClassRoom.changeChairState(ep.group, ep.formatedPosition(), text);
        }
        private void setChairState(equipmentPosition ep, string text)
        {
            Program.frmClassRoom.changeChairState(ep.group, ep.formatedPosition(), text);
        }

        private void setChairState(equipmentPosition ep, DocumentFileState dfs)
        {
            Program.frmClassRoom.changeChairState(ep.group, ep.formatedPosition(), dfs);
        }
        DataTable dtAnswerRecord = null;
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
            else
            {
                this.dtAnswerRecord.Rows.Add(new object[] { studentID, answer });
            }
        }
        private void initialInfoTable()
        {
            //this.dtRoomConfig = MemoryTable.dtRoomConfig;
            //this.studentInfoTable = MemoryTable.studentInfoTable;
            //this.mapConfigsTable = MemoryTable.mapConfigsTable;

            this.dtAnswerRecord = new DataTable();
            this.dtAnswerRecord.Columns.Add("studenID", typeof(string));
            this.dtAnswerRecord.Columns.Add("answer", typeof(string));

            //学生ID与设备ID绑定，查找学生位置时，先通过本表查询设备ID，在通过设备ID确定学生位置
            //this.dtStudentAndEquipmentCombining = new DataTable();
            //this.dtStudentAndEquipmentCombining.Columns.Add("studenID", typeof(string));
            //this.dtStudentAndEquipmentCombining.Columns.Add("equipmentID", typeof(string));

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
            int totalCount = MemoryTable.studentInfoTable.Rows.Count;

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
        private void setPieToInitializeState()
        {
            PieChart1.Items.Clear();
            PieChart1.Items.Add(new PieChartItem(25, this.clrA, "0", "A", 0));
            PieChart1.Items.Add(new PieChartItem(25, this.clrB, "0", "B", 0));
            PieChart1.Items.Add(new PieChartItem(25, this.clrC, "0", "C", 0));
            PieChart1.Items.Add(new PieChartItem(25, this.clrD, "0", "D", 0));
        }
        void resetPbYPos(int screenHeight)
        {
            int totalCount = this.pblst.Count;

            int defaultTop = 150;
            int defaultVGap = 20;
            int totalHeight = 80 * totalCount + defaultVGap * (totalCount - 1);
            int newTop = (screenHeight - totalHeight) / 2;
            if (newTop > defaultTop)
            {
                defaultTop = newTop;
            }
            for (int i = 0; i < totalCount; i++)
            {
                this.pblst[i].Top = defaultTop + 80 * i + defaultVGap * i;
            }
            //this.pictureBox4.Top = defaultTop;
            //this.pictureBox3.Top = defaultTop + 80 + defaultVGap;
            //this.pictureBox2.Top = defaultTop + 80 * 2 + defaultVGap * 2;
            //this.pictureBox1.Top = defaultTop + 80 * 3 + defaultVGap * 3;

        }
        void pictureBox_mouse_hover(object sender, EventArgs e)
        {
            ((PictureBox)sender).BackColor = Color.YellowGreen;

        }
        void pictureBox_mouse_leave(object sender, EventArgs e)
        {
            ((PictureBox)sender).BackColor = Color.White;
        }
        private void Form1_Move(object sender, EventArgs e)
        {
            //Program.frmSelect.Left = this.Left;
            //Program.frmSelect.Top = this.Top + this.Height;
        }
        #region
        private void initial_popup_menu()
        {
            this.menuItemClose = new System.Windows.Forms.MenuItem();
            this.menuItemClose.Index = 0;
            this.menuItemClose.Text = "退出(&X)";
            this.menuItemClose.Click += (sender, e) =>
            {
                this.Close();
            };

            menuItemAbout = new System.Windows.Forms.MenuItem();
            menuItemAbout.Index = 1;
            menuItemAbout.Text = "关于(&A)";
            menuItemAbout.Click += (sender, e) =>
            {
                about frm = new about();
                frm.ShowDialog();
            };

            menuItemSerialConfig = new System.Windows.Forms.MenuItem();
            menuItemSerialConfig.Index = 2;
            menuItemSerialConfig.Text = "串口设置(&C)";
            menuItemSerialConfig.Click += (sender, e) =>
            {
                frmSerialPortConfig frm = new frmSerialPortConfig();
                frm.ShowDialog();
            };



            menuItemEquipmentConfig = new System.Windows.Forms.MenuItem();
            menuItemEquipmentConfig.Index = 3;
            menuItemEquipmentConfig.Text = "教室设置(&E)";
            menuItemEquipmentConfig.Click += new EventHandler(menuItemEquipmentConfig_Click);

            menuItemQuestionMng = new System.Windows.Forms.MenuItem();
            menuItemQuestionMng.Index = 4;
            menuItemQuestionMng.Text = "题目管理(&Q)";
            menuItemQuestionMng.Click += new EventHandler(menuItemQuestionMng_Click);

            menuItemAnalysis = new System.Windows.Forms.MenuItem();
            menuItemAnalysis.Index = 5;
            menuItemAnalysis.Text = "统计分析(&S)";
            menuItemAnalysis.Click += new EventHandler(menuItemAnalysis_Click);

            menuItemStudentMng = new System.Windows.Forms.MenuItem();
            menuItemStudentMng.Index = 6;
            menuItemStudentMng.Text = "学生管理(&T)";
            menuItemStudentMng.Click += new EventHandler(menuItemStudentMng_Click);



            this.notifyContextMenu = new System.Windows.Forms.ContextMenu();
            this.notifyContextMenu.MenuItems.Add(menuItemStudentMng);
            this.notifyContextMenu.MenuItems.Add(menuItemAnalysis);
            this.notifyContextMenu.MenuItems.Add(menuItemQuestionMng);
            this.notifyContextMenu.MenuItems.Add(menuItemEquipmentConfig);
            this.notifyContextMenu.MenuItems.Add(menuItemSerialConfig);
            this.notifyContextMenu.MenuItems.Add(menuItemAbout);
            this.notifyContextMenu.MenuItems.Add(menuItemClose);

            this.components = new System.ComponentModel.Container();

            // Create the NotifyIcon.
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);

            // The Icon property sets the icon that will appear
            // in the systray for this application.
            notifyIcon1.Icon = new Icon("5.ico");

            // The ContextMenu property sets the menu that will
            // appear when the systray icon is right clicked.
            notifyIcon1.ContextMenu = this.notifyContextMenu;

            // The Text property sets the text that will be displayed,
            // in a tooltip, when the mouse hovers over the systray icon.
            notifyIcon1.Text = "智能教学互动系统";
            notifyIcon1.Visible = true;

            notifyIcon1.BalloonTipTitle = "智能教学互动系统已经启动";
            notifyIcon1.BalloonTipText = "更多功能请点击...";
            notifyIcon1.ShowBalloonTip(15);
        }
        void menuItemAbout_Click(object sender, EventArgs e)
        {
            about frm = new about();
            frm.ShowDialog();
        }

        void frmMainFloat_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.notifyIcon1.Dispose();
        }
        void menuItemStudentMng_Click(object sender, EventArgs e)
        {
            FrmRfidCheck_StudentManage frm = new FrmRfidCheck_StudentManage();
            frm.Show();
        }

        void menuItemAnalysis_Click(object sender, EventArgs e)
        {
            //frmCheckAnalysis frm = new frmCheckAnalysis();
            frmCheckStatics frm = new frmCheckStatics();
            frm.Show();
        }

        void menuItemQuestionMng_Click(object sender, EventArgs e)
        {
            frmQuestionMng frm = new frmQuestionMng();
            frm.Show();
        }

        void menuItemEquipmentConfig_Click(object sender, EventArgs e)
        {
            frmEquipmentConfig frm = new frmEquipmentConfig();
            frm.Show();
        }



        #endregion

    }
}
