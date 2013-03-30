using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Config;
using intelligentMiddleWare;

namespace Carbinet
{
    /// <summary>
    /// 考勤  即时互动  课堂测试  退出
    /// </summary>
    public partial class frmMainFloat : Form
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

        //private System.Windows.Forms.NotifyIconChart notifyIconChart1;
        Point mouseOff; //记录鼠标指针的坐标  
        bool leftFlag;
        public frmMainFloat()
        {
            InitializeComponent();
            Screen[] screens = System.Windows.Forms.Screen.AllScreens;
            for (int i = 0; i < screens.Length; i++)
            {
                Screen sc = screens[i];
                if (sc.Primary == true)
                {
                    Rectangle rect = sc.WorkingArea;
                    this.Left = (int)(rect.Width * 0.7);
                    this.Top = (int)(rect.Height * 0.1);
                }
            }

            this.menuItemClose = new System.Windows.Forms.MenuItem();
            this.menuItemClose.Index = 0;
            this.menuItemClose.Text = "退出(&X)";
            this.menuItemClose.Click += new EventHandler(menuItemClose_Click);

            menuItemAbout = new System.Windows.Forms.MenuItem();
            menuItemAbout.Index = 1;
            menuItemAbout.Text = "关于(&A)";
            menuItemAbout.Click += new EventHandler(menuItemAbout_Click);

            menuItemSerialConfig = new System.Windows.Forms.MenuItem();
            menuItemSerialConfig.Index = 2;
            menuItemSerialConfig.Text = "串口设置(&C)";
            menuItemSerialConfig.Click += new EventHandler(menuItemSerialConfig_Click);

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


            //int size = this.pbFloatPie.Width - 2;
            //this.notifyIconChart1 = new System.Windows.Forms.NotifyIconChart(size);

            //this.notifyIconChart1.BackgroundColor = System.Drawing.Color.Transparent;
            //this.notifyIconChart1.ChartType = System.Windows.Forms.NotifyIconChart.ChartTypeEnum.pie;
            //this.notifyIconChart1.Color1 = System.Drawing.Color.Red;
            //this.notifyIconChart1.Color2 = System.Drawing.Color.Green;
            //this.notifyIconChart1.FrameColor = System.Drawing.Color.Transparent;
            //this.notifyIconChart1.NotifyIconObject = null;
            //this.notifyIconChart1.Value1 = 0;
            //this.notifyIconChart1.Value2 = 1;

            //Bitmap bmp = this.notifyIconChart1.GetChartBitmap();
            //this.pbFloatPie.Image = bmp;

            this.MouseDown += new MouseEventHandler(Form1_MouseDown);
            this.MouseMove += new MouseEventHandler(Form1_MouseMove);
            this.MouseUp += new MouseEventHandler(Form1_MouseUp);

            this.FormClosing += new FormClosingEventHandler(frmMainFloat_FormClosing);
            //StaticDataPort.openDataPort();

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

        #region 窗体拖动
        void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            if (leftFlag)
            {
                leftFlag = false;//释放鼠标后标注为false;  
            }
        }

        void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (leftFlag)
            {
                Point mouseSet = Control.MousePosition;
                mouseSet.Offset(mouseOff.X, mouseOff.Y); //设置移动后的位置  
                Location = mouseSet;
            }
        }

        void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouseOff = new Point(-e.X, -e.Y); //得到变量的值  
                leftFlag = true; //点击左键按下时标注为true;  
            }
        }

        #endregion
        #region 菜单点击处理
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            //显示大窗体
            //Program.frmSelect.Left = this.Left;
            //Program.frmSelect.Top = this.Top;
            //Program.frmSelect.Show();
            //this.Hide();
            //Program.frmCheck.Show();
            frmCheckInit startCheck = new frmCheckInit();
            startCheck.ShowDialog();

        }

        private void pbHide_Click(object sender, EventArgs e)
        {
            //this.Hide();
            //Program.frmMain.Show();
            this.Close();
        }
        //即时互动
        private void pbFloatPie_Click(object sender, EventArgs e)
        {

        }

        private void pbTest_Click(object sender, EventArgs e)
        {
            //frmRTTest frm = new frmRTTest();
            //frm.ShowDialog();
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

        void menuItemSerialConfig_Click(object sender, EventArgs e)
        {
            frmSerialPortConfig frm = new frmSerialPortConfig();
            frm.Show();

        }

        void menuItemClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion
        //private void button2_Click(object sender, EventArgs e)
        //{
        //    this.notifyIconChart1.Value1 = 10;
        //    this.notifyIconChart1.Value2 = 5;
        //    Bitmap bmp = this.notifyIconChart1.GetChartBitmap();
        //    this.pictureBox2.Image = bmp;
        //}

    }
}
