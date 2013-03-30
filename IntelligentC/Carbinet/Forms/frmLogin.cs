using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Carbinet
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();

            this.cmbClass.Items.Add("信息学院一班");
            this.cmbClass.Items.Add("信息学院二班");
            this.cmbClass.SelectedIndex = 0;


            this.cmbSubject.Items.Add("高等代数");
            this.cmbSubject.Items.Add("大学英语");
            this.cmbSubject.Items.Add("数据结构");
            this.cmbSubject.Items.Add("市场营销");
            this.cmbSubject.SelectedIndex = 0;

        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            Configures.ClassName = this.cmbClass.Text;
            Configures.SubjectName = this.cmbSubject.Text;

            // Form1 frm = new Form1();
            // frm.Show();
            //Application.Run(new Form1());
            this.Hide();
        }
    }
}
