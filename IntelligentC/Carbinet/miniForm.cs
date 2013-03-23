using Config;
using intelligentMiddleWare;
using MetroFramework.Forms;
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
    public partial class frmFloat : MetroForm
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

        List<PictureBox> pblst = new List<PictureBox>();
        public frmFloat()
        {
            InitializeComponent();

            this.Move += Form1_Move;
            Screen[] screens = System.Windows.Forms.Screen.AllScreens;
            for (int i = 0; i < screens.Length; i++)
            {
                Screen sc = screens[i];
                if (sc.Primary == true)
                {
                    Rectangle rect = sc.WorkingArea;
                    this.Left = (int)(rect.Width - this.Width - 50);
                    //this.Left = (int)(rect.Width * 0.7);
                    this.Top = (int)(rect.Height * 0.0);
                }
            }
            pblst.Add(pictureBox1);
            pblst.Add(pictureBox2);
            pblst.Add(pictureBox3);
            pblst.Add(pictureBox4);
            int gap = 3;
            this.pictureBox3.Left = this.pictureBox4.Left + pictureBox4.Width + gap;
            this.pictureBox2.Left = this.pictureBox3.Left + pictureBox3.Width + gap;
            this.pictureBox1.Left = this.pictureBox2.Left + pictureBox2.Width + gap;

            this.pictureBox4.Image = (Image)global::Carbinet.Properties.Resources.MB_touch;//考勤
            this.pictureBox3.Image = (Image)global::Carbinet.Properties.Resources.MB_Hand;//实时互动
            this.pictureBox2.Image = (Image)global::Carbinet.Properties.Resources.MB_tasks;//课堂测验
            this.pictureBox1.Image = (Image)global::Carbinet.Properties.Resources.MB_shut_down;//退出


            foreach (PictureBox pb in pblst)
            {
                pb.MouseHover += pictureBox_mouse_hover;
                pb.MouseLeave += pictureBox_mouse_leave;
            }

            pictureBox4.Click += (sender, e) =>
            {
                frmCheckInit startCheck = new frmCheckInit();
                startCheck.ShowDialog();
            };



            pictureBox1.Click += (sender, e) =>
            {
                Program.closeAllForms();
                this.Close();
            };
            pictureBox2.Click += (sender, e) =>
                {
                    frmRTTest frm = new frmRTTest();
                    MiddleWareCore.set_mode(MiddleWareMode.课堂测验);
                    frm.ShowDialog();
                };
            pictureBox3.Click += (sender, e) =>
            {
                Program.frmSelect.Visible = !Program.frmSelect.Visible;

            };
            //this.initial_popup_menu();
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
            Program.frmSelect.Left = this.Left;
            Program.frmSelect.Top = this.Top + this.Height;
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
