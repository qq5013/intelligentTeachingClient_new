using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using intelligentMiddleWare;
using MetroFramework.Forms;

namespace Carbinet
{
    public partial class frmCheckInit : MetroForm
    {
        public frmCheckInit()
        {
            InitializeComponent();
        }

        private void frmCheckInit_Load(object sender, EventArgs e)
        {
            this.lblCheckGuid.Text = string.Format("{0}{1}", "prefix", DateTime.Now.ToString("yyyyMMddHHmmss"));
            this.dtpEnd.Value = DateTime.Now;
            this.dtpStart.Value = DateTime.Now;
            this.txtInfo.Text = string.Empty;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmCheckInitctl.insert_record(this.lblCheckGuid.Text, this.dtpStart.Text, this.dtpEnd.Text, this.txtInfo.Text, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            frmCheck check = new frmCheck(this.lblCheckGuid.Text, this.dtpStart.Text, this.dtpEnd.Text);
            check.ShowDialog();
            MiddleWareCore.set_mode(intelligentMiddleWare.MiddleWareMode.考勤);
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
