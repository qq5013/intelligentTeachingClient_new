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
        string record_id = string.Empty;
        public frmCheckInit()
        {
            InitializeComponent();
        }

        private void frmCheckInit_Load(object sender, EventArgs e)
        {
            record_id = string.Format("{0}{1})", "考勤记录(", DateTime.Now.ToString("yyyyMMddHHmmss"));
            this.lblCheckGuid.Text = record_id;
            this.dtpStart.Value = DateTime.Now;
            this.txtInfo.Text = string.Empty;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CheckCtl.insert_record(record_id, this.txtInfo.Text, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            I_event_handler event_handler = (I_event_handler)(new StudentCheck(record_id));
            event_handler.prepare_handler();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            frmSelectCheckRecord frm = new frmSelectCheckRecord(this);
            frm.ShowDialog();
        }
        public void setRecordID(string record)
        {
            this.record_id = record;
            this.lblCheckGuid.Text = record_id;
        }
    }
}
