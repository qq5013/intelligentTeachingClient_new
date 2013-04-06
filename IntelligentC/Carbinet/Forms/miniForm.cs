using Config;
using intelligentMiddleWare;
using MetroFramework;
using MetroFramework.Forms;
using Nexus.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Carbinet
{
    public partial class frmFloat : MetroForm, I_mini_form_show_notify
    {
        #region 成员
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ContextMenu notifyContextMenu;
        private System.Windows.Forms.MenuItem menuItemClose;
        private System.Windows.Forms.MenuItem menuItemSerialConfig;
        private System.Windows.Forms.MenuItem menuItemEquipmentConfig;
        private System.Windows.Forms.MenuItem menuItemQuestionMng;
        private System.Windows.Forms.MenuItem menuItemAnalysis;
        private System.Windows.Forms.MenuItem menuItemStudentMng;
        private System.Windows.Forms.MenuItem menuItemAbout;


        I_event_handler event_handler;
        List<PictureBox> pblst = new List<PictureBox>();
        List<PictureBox> pblst2 = new List<PictureBox>();
        frmLegend frmLegend = null;
        List<string> textList = null;
        List<MetroColorStyle> styleList = null;
        #endregion

        public frmFloat()
        {
            #region 控件初始化
            InitializeComponent();
            pblst.Add(pictureBox5);
            pblst.Add(pictureBox4);
            pblst.Add(pictureBox3);
            pblst.Add(pictureBox2);
            pblst.Add(pictureBox1);

            pblst2.Add(pictureBox5_1);
            pblst2.Add(pictureBox4_1);
            pblst2.Add(pictureBox3_1);
            pblst2.Add(pictureBox2_1);
            pblst2.Add(pictureBox1_1);

            this.pictureBox5.Image = (Image)global::Carbinet.Properties.Resources.座位1;//显示座位
            this.pictureBox4.Image = (Image)global::Carbinet.Properties.Resources.打卡1;//考勤
            this.pictureBox3.Image = (Image)global::Carbinet.Properties.Resources.互动1;//实时互动
            this.pictureBox2.Image = (Image)global::Carbinet.Properties.Resources.测验1;//课堂测验
            this.pictureBox1.Image = (Image)global::Carbinet.Properties.Resources.关闭1;//退出

            this.pictureBox5_1.Image = (Image)global::Carbinet.Properties.Resources.座位2;//显示座位
            this.pictureBox4_1.Image = (Image)global::Carbinet.Properties.Resources.打卡2;//考勤
            this.pictureBox3_1.Image = (Image)global::Carbinet.Properties.Resources.互动2;//实时互动
            this.pictureBox2_1.Image = (Image)global::Carbinet.Properties.Resources.测验2;//课堂测验
            this.pictureBox1_1.Image = (Image)global::Carbinet.Properties.Resources.关闭2;//退出


            PieChart1.ItemStyle.SurfaceAlphaTransparency = 0.92F;
            PieChart1.FocusedItemStyle.SurfaceAlphaTransparency = 0.92F;
            PieChart1.FocusedItemStyle.SurfaceBrightnessFactor = 0.3F;
            PieChart1.Inclination = 1.55F;
            PieChart1.AutoSizePie = true;
            PieChart1.Thickness = 31;
            PieChart1.Rotation = 0.0F;
            PieChart1.ShowEdges = false;
            //PieChart1.Radius = 90F;

            PieChart1.MouseHover += PieChart1_MouseHover;
            PieChart1.MouseLeave += PieChart1_MouseLeave;
            #endregion

            #region 设置窗体及主要控件位置
            this.lblTip.Text = string.Empty;

            Screen[] screens = System.Windows.Forms.Screen.AllScreens;
            for (int i = 0; i < screens.Length; i++)
            {
                Screen sc = screens[i];
                if (sc.Primary == true)
                {
                    Rectangle rect = sc.WorkingArea;
                    this.Top = (int)(rect.Height * 0.0);
                    this.Height = rect.Height;
                    this.Left = 0;
                    this.Width = 85;

                    // 从高度150开始计算按钮所能使用的空间，然后将其垂直居中
                    resetPbYPos(rect.Height);
                }
            }
            #endregion

            #region 绑定按钮事件


            pictureBox1_1.Click += (sender, e) =>
            {
                Program.closeAllForms();
                this.Close();
            };
            pictureBox2_1.Click += (sender, e) =>
                {
                    event_handler = (I_event_handler)(new frmRTTest(this));
                    event_handler.prepare_handler();
                };
            pictureBox3_1.Click += (sender, e) =>
            {
                event_handler = new RealtimeInteractive(this);
                event_handler.prepare_handler();
            };
            pictureBox4_1.Click += (sender, e) =>
            {
                frmCheckInit startCheck = new frmCheckInit();
                startCheck.ShowDialog();
            };

            pictureBox5_1.Click += (sender, e) =>
                {
                    Program.frmClassRoom.ShowDialog();
                };

            #endregion
            this.initial_popup_menu();
        }
        public void setLegend(List<string> _textList, List<MetroColorStyle> _styleList)
        {
            this.textList = _textList;
            this.styleList = _styleList;
        }
        //当鼠标离开饼图后，隐藏图例
        void PieChart1_MouseLeave(object sender, EventArgs e)
        {
            if (frmLegend != null)

                frmLegend.Close();
        }
        //当鼠标指在饼图上时，显示图例
        void PieChart1_MouseHover(object sender, EventArgs e)
        {
            //frmLegend = new frmLegend();
            frmLegend = new frmLegend(this.textList, this.styleList);
            frmLegend.Left = this.Width;
            frmLegend.Show();
        }

        //miniButton getMiniButton(PictureBox pb)
        //{
        //    miniButton button = miniButton.显示座位;
        //    switch (pb.Name)
        //    {
        //        case "pictureBox5":
        //            break;
        //        case "pictureBox4":
        //            button = miniButton.考勤;
        //            break;
        //        case "pictureBox3":
        //            button = miniButton.实时互动;
        //            break;
        //        case "pictureBox2":
        //            button = miniButton.课堂测验;
        //            break;
        //        case "pictureBox1":
        //            button = miniButton.退出;
        //            break;
        //    }
        //    return button;
        //}

        //public void setButtonState(miniButton button, bool state)
        //{
        //    switch (button)
        //    {
        //        case miniButton.显示座位:
        //            if (state) this.pictureBox5.Image = (Image)global::Carbinet.Properties.Resources.课堂2;//显示座位
        //            else this.pictureBox5.Image = (Image)global::Carbinet.Properties.Resources.课堂1;//显示座位
        //            break;
        //        case miniButton.考勤:
        //            if (state) this.pictureBox4.Image = (Image)global::Carbinet.Properties.Resources.打卡2;//显示座位
        //            else this.pictureBox4.Image = (Image)global::Carbinet.Properties.Resources.打卡1;//显示座位
        //            break;
        //        case miniButton.课堂测验:
        //            if (state) this.pictureBox2.Image = (Image)global::Carbinet.Properties.Resources.测验2;//显示座位
        //            else this.pictureBox2.Image = (Image)global::Carbinet.Properties.Resources.测验1;//显示座位
        //            break;
        //        case miniButton.实时互动:
        //            if (state) this.pictureBox3.Image = (Image)global::Carbinet.Properties.Resources.提问2;//显示座位
        //            else this.pictureBox3.Image = (Image)global::Carbinet.Properties.Resources.提问1;//显示座位
        //            break;
        //        case miniButton.退出:
        //            if (state) this.pictureBox1.Image = (Image)global::Carbinet.Properties.Resources.关闭2;//显示座位
        //            else this.pictureBox1.Image = (Image)global::Carbinet.Properties.Resources.关闭1;//显示座位
        //            break;
        //    }
        //}

        #region 接口函数
        public void refreshPie(List<string> captionList, List<int> valueList, List<Color> colorList, bool b)
        {
            if (captionList.Count != valueList.Count || valueList.Count != colorList.Count) return;

            int totalCount = valueList.Sum();
            List<string> labelList = new List<string>();
            if (totalCount > 0)
            {
                for (int i = 0; i < captionList.Count; i++)
                {
                    string temp = (valueList[i] * 100 / totalCount) + "%";
                    labelList.Add(temp);
                }
            }
            else return;

            PieChart1.Items.Clear();
            for (int i = 0; i < valueList.Count; i++)
            {
                if (b)
                {
                    PieChart1.Items.Add(new PieChartItem(valueList[i], colorList[i], valueList[i].ToString(), string.Format("{0} {1} ", captionList[i], labelList[i]), 0));
                }
                else
                {
                    PieChart1.Items.Add(new PieChartItem(valueList[i], colorList[i], captionList[i], captionList[i], 0));

                }
            }
        }
        public void show_tip(string tip)
        {
            this.lblTip.Text = tip;
        }
        #endregion

        void resetPbYPos(int screenHeight)
        {
            int totalCount = this.pblst.Count;

            int defaultTop = 150;
            int defaultVGap = 0;
            int totalHeight = 80 * totalCount + defaultVGap * (totalCount - 1);
            int newTop = (screenHeight - totalHeight) / 2;
            if (newTop > defaultTop)
            {
                defaultTop = newTop;
            }
            for (int i = 0; i < totalCount; i++)
            {
                this.pblst[i].Top = defaultTop + 80 * i + defaultVGap * i;
                this.pblst2[i].Top = defaultTop + 80 * i + defaultVGap * i;
                this.pblst2[i].Visible = false;

                int iTemp = i;
                this.pblst[i].MouseMove += (sender, e) =>
                {
                    this.pblst[iTemp].Visible = false;
                    this.pblst2[iTemp].Visible = true;
                };
                this.pblst2[i].MouseLeave += (sender, e) =>
                {
                    this.pblst[iTemp].Visible = true;
                    this.pblst2[iTemp].Visible = false;
                };
            }



        }

        #region 状态栏图标点击事件
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

            //menuItemSerialConfig = new System.Windows.Forms.MenuItem();
            //menuItemSerialConfig.Index = 2;
            //menuItemSerialConfig.Text = "串口设置(&C)";
            //menuItemSerialConfig.Click += (sender, e) =>
            //{
            //    frmSerialPortConfig frm = new frmSerialPortConfig();
            //    frm.ShowDialog();
            //};



            menuItemEquipmentConfig = new System.Windows.Forms.MenuItem();
            menuItemEquipmentConfig.Index = 3;
            menuItemEquipmentConfig.Text = "教室设置(&E)";
            menuItemEquipmentConfig.Click += new EventHandler(menuItemEquipmentConfig_Click);

            //menuItemQuestionMng = new System.Windows.Forms.MenuItem();
            //menuItemQuestionMng.Index = 4;
            //menuItemQuestionMng.Text = "题目管理(&Q)";

            //menuItemAnalysis = new System.Windows.Forms.MenuItem();
            //menuItemAnalysis.Index = 5;
            //menuItemAnalysis.Text = "统计分析(&S)";

            menuItemStudentMng = new System.Windows.Forms.MenuItem();
            menuItemStudentMng.Index = 6;
            menuItemStudentMng.Text = "学生管理(&T)";
            menuItemStudentMng.Click += new EventHandler(menuItemStudentMng_Click);



            this.notifyContextMenu = new System.Windows.Forms.ContextMenu();
            this.notifyContextMenu.MenuItems.Add(menuItemStudentMng);
            //this.notifyContextMenu.MenuItems.Add(menuItemAnalysis);
            //this.notifyContextMenu.MenuItems.Add(menuItemQuestionMng);
            this.notifyContextMenu.MenuItems.Add(menuItemEquipmentConfig);
            //this.notifyContextMenu.MenuItems.Add(menuItemSerialConfig);
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
            notifyIcon1.ShowBalloonTip(5);
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
            frmStudentManage frm = new frmStudentManage();
            frm.ShowDialog();
        }

        void menuItemEquipmentConfig_Click(object sender, EventArgs e)
        {
            frmEquipmentConfig frm = new frmEquipmentConfig();
            Program.frmClassRoomConfig = frm;
            Program.frmClassRoom.Show();
            frm.Left = Program.frmClassRoom.Left + Program.frmClassRoom.Width;
            frm.Top = Program.frmClassRoom.Top;
            frm.Show();
        }



        #endregion

    }
}
