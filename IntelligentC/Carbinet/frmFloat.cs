using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Carbinet
{
    public partial class frmFloat : Form
    {
        private System.Windows.Forms.NotifyIconChart notifyIconChart1;
        Point mouseOff; //记录鼠标指针的坐标  
        bool leftFlag;
        public frmFloat()
        {
            InitializeComponent();

            int size = this.pbFloatPie.Width - 2;
            this.notifyIconChart1 = new System.Windows.Forms.NotifyIconChart(size);
  
            this.notifyIconChart1.BackgroundColor = System.Drawing.Color.Transparent;
            this.notifyIconChart1.ChartType = System.Windows.Forms.NotifyIconChart.ChartTypeEnum.pie;
            this.notifyIconChart1.Color1 = System.Drawing.Color.Red;
            this.notifyIconChart1.Color2 = System.Drawing.Color.Green;
            this.notifyIconChart1.FrameColor = System.Drawing.Color.Transparent;
            this.notifyIconChart1.NotifyIconObject = null;
            this.notifyIconChart1.Value1 = 0;
            this.notifyIconChart1.Value2 = 1;

            Bitmap bmp = this.notifyIconChart1.GetChartBitmap();
            this.pbFloatPie.Image = bmp;

            this.MouseDown += new MouseEventHandler(Form1_MouseDown);
            this.MouseMove += new MouseEventHandler(Form1_MouseMove);
            this.MouseUp += new MouseEventHandler(Form1_MouseUp);
        }

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

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            //显示大窗体
            Program.frmSelect.Left = this.Left;
            Program.frmSelect.Top = this.Top;
            Program.frmSelect.Show();
            this.Hide();
        }

        private void pbHide_Click(object sender, EventArgs e)
        {
            this.Hide();
            Program.frmMain.Show();
        }
        //private void button2_Click(object sender, EventArgs e)
        //{
        //    this.notifyIconChart1.Value1 = 10;
        //    this.notifyIconChart1.Value2 = 5;
        //    Bitmap bmp = this.notifyIconChart1.GetChartBitmap();
        //    this.pictureBox2.Image = bmp;
        //}

    }
}
