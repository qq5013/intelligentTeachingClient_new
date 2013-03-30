using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Config;

namespace Carbinet
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
            this.Shown += new EventHandler(frmMain_Shown);
            this.FormClosed += new FormClosedEventHandler(frmMain_FormClosed);
        }

        void frmMain_Shown(object sender, EventArgs e)
        {
            commonSocket.StartListen();
        }

        void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }


        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            frmCheck frm = new frmCheck();
            frm.ShowDialog();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            frmSelect frm = new frmSelect();
            frm.ShowDialog();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            frmEquipmentConfig frm = new frmEquipmentConfig();
            frm.ShowDialog();
        }

        private void toolStripButton1_Click_1(object sender, EventArgs e)
        {
            //frmCheckAnalysis frm = new frmCheckAnalysis();
            //frm.ShowDialog();
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            frmSerialPortConfig frm = new frmSerialPortConfig();
            frm.ShowDialog();
        }

        private void 课堂测试_Click(object sender, EventArgs e)
        {
            //frmRTTest frm = new frmRTTest();
            //frm.ShowDialog();

        }

        private void 题目管理_Click(object sender, EventArgs e)
        {
            frmQuestionMng frm = new frmQuestionMng();
            frm.ShowDialog();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            FrmRfidCheck_StudentManage frm = new FrmRfidCheck_StudentManage();
            frm.ShowDialog();
        }

        private void 启动考勤ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCheck frm = new frmCheck();
            frm.ShowDialog();
        }

        private void 即时互动RToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //frmSelect frm = new frmSelect();
            //if (Program.frmFloat == null)
            //{
            //    Program.frmFloat = new frmFloat();

            //}

            //frm.ShowDialog();
            this.Hide();
            Program.frmFloat.Show();
        }

        private void 课堂测试TToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //frmRTTest frm = new frmRTTest();
            //frm.ShowDialog();
        }

        private void 串口设置CToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSerialPortConfig frm = new frmSerialPortConfig();
            frm.ShowDialog();
        }

        private void 题目管理QToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmQuestionMng frm = new frmQuestionMng();
            frm.ShowDialog();
        }

        private void 学生管理SToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmRfidCheck_StudentManage frm = new FrmRfidCheck_StudentManage();
            frm.ShowDialog();
        }

        private void 教师设置LToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmEquipmentConfig frm = new frmEquipmentConfig();
            frm.ShowDialog();
        }

        private void 考勤统计AToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //frmCheckAnalysis frm = new frmCheckAnalysis();
            //frm.ShowDialog();
        }
    }
}
