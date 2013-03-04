using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace Carbinet
{
    /*
     显示出指定时间段内所有考勤记录的出勤率
     */
    public partial class frmCheckStatics : Form
    {
        public frmCheckStatics()
        {
            InitializeComponent();
            // Set the titles and axis labels
            //myPane.Title.Text = "考勤率统计";
            //myPane.XAxis.Title.Text = "考勤时间";
            //myPane.YAxis.Title.Text = "出勤率";
        }

        private void frmCheckStatics_Load(object sender, EventArgs e)
        {
            this.dtpEnd.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            this.dtpStart.Value = this.dtpEnd.Value.AddMonths(-1);
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            DateTime dtTime_start = this.dtpStart.Value;
            DateTime dtTime_end = this.dtpEnd.Value;
            if (DateTime.Compare(dtTime_end, dtTime_start) <= 0)
            {
                MessageBox.Show("时间段选择不正确！", "信息提示");
                return;
            }

            string time_start = dtTime_start.ToString("yyyy-MM-dd HH:mm:ss");
            string time_end = dtTime_end.ToString("yyyy-MM-dd HH:mm:ss");



            string sqlSelect_check_record = "select record_id,create_time,info from check_record where create_time between '{0}' and '{1}'";
            DataTable dtCheck_record = CsharpSQLiteHelper.ExecuteTable(sqlSelect_check_record, new object[2] { time_start, time_end });

            int iStuden_count = 0;
            DataTable studentInfoTable = studentInfoCtl.getAllStudentInfo();
            iStuden_count = studentInfoTable.Rows.Count;

            //GraphPane myPane = this.zedGraphControl1.GraphPane;
            //// Set the titles and axis labels
            //myPane.Title.Text = "考勤率统计";
            //myPane.XAxis.Title.Text = "考勤时间";
            //myPane.YAxis.Title.Text = "出勤率";


            // Make up some random data points
            double x, y;
            //PointPairList list = new PointPairList();

            //if (dtCheck_record.Rows.Count > 0)
            //{
            //    DataTable dt = new DataTable();
            //    dt.Columns.Add("考勤时间");
            //    dt.Columns.Add("出勤率(%)");
            //    dt.Columns.Add("备注");

            //    for (int i = 0; i < dtCheck_record.Rows.Count; i++)
            //    {
            //        string info = dtCheck_record.Rows[i]["info"].ToString();
            //        string strCreate_time = dtCheck_record.Rows[i]["create_time"].ToString();
            //        DateTime dtCreate_time = DateTime.Parse(strCreate_time);
            //        string record_id = dtCheck_record.Rows[i]["record_id"].ToString();
            //        string sqlSelect_student_check_record =
            //            "select student_id from T_STUDENT_CHECK_INFO where record_id = '{0}'";
            //        DataTable dtStudent_check_record = CsharpSQLiteHelper.ExecuteTable(sqlSelect_student_check_record, new object[1] { record_id });
            //        int iChecked_count = dtStudent_check_record.Rows.Count;
            //        int check_percent = iChecked_count * 100 / iStuden_count;
            //        x = (double)new XDate(dtCreate_time);
            //        y = check_percent;
            //        list.Add(x, y);

            //        dt.Rows.Add(new object[3] { strCreate_time, check_percent.ToString(), info });
            //    }
            //    // Generate a red curve with diamond
            //    // symbols, and "My Curve" in the legend
            //    myPane.RemoveAllCurve();
            //    CurveItem myCurve = myPane.AddCurve("考勤率曲线", list, Color.Red, SymbolType.Diamond);

            //    // Set the XAxis to date type
            //    myPane.XAxis.Type = AxisType.Date;

            //    // Tell ZedGraph to refigure the axes since the data 
            //    // have changed
            //    this.zedGraphControl1.AxisChange();
            //    this.zedGraphControl1.Refresh();

            //    this.dataGridView1.Columns.Clear();
            //    this.dataGridView1.DataSource = dt;
            //    this.dataGridView1.Columns[0].Width = 150;
            //    this.dataGridView1.Columns[1].Width = 120;
            //    this.dataGridView1.Columns[2].Width = 400;

            //    Debug.WriteLine("zde");
            //}
            //else
            //{
            //    MessageBox.Show("该段时间内没有考勤记录！", "信息提示");
            //}

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dtpStart_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void dtpEnd_ValueChanged(object sender, EventArgs e)
        {

        }

    }
}
