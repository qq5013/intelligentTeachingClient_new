using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using BarChart;

namespace Carbinet
{
    public partial class frmCheckAnalysis : Form
    {
        private HBarChart barChart;

        public frmCheckAnalysis()
        {
            InitializeComponent();

            this.groupBoxClassCheck.Top = 12;

            this.chart1.XAxis.Scale.MajorUnit = 1;

            this.comboBox1.Items.Add("信息学院一班");
            this.comboBox1.Items.Add("信息学院二班");
            this.comboBox1.SelectedIndex = 0;

            this.dateTimePicker1.Value = DateTime.Now.AddMonths(-1);
            this.dateTimePicker2.Value = DateTime.Now;

            /*
            barChart = new HBarChart();
            this.panel1.Controls.Add(barChart);
            barChart.Dock = DockStyle.Fill;

            // Not AutoScale
            barChart.SizingMode = HBarChart.BarSizingMode.Normal;

            // Percent
            barChart.Values.Mode = CValueProperty.ValueMode.String;
            // Glass


            barChart.Border.Width = 10;
            barChart.Shadow.Mode = CShadowProperty.Modes.Both;
            barChart.Shadow.WidthInner = 1;
            barChart.Shadow.WidthOuter = 4;
            barChart.Shadow.ColorOuter = Color.FromArgb(100, 0, 0, 0);

            barChart.Description.Visible = true;

            barChart.Values.Visible = true;

            barChart.Label.Visible = true;
            barChart.BarWidth = 24;
            barChart.Border.Width = 0;
            barChart.BarTooltip.Active = false;
            //barChart.Description.Text = "出勤率统计";
            */
        }

        private void button1_Click(object sender, EventArgs e)
        {



        }

        private void button3_Click(object sender, EventArgs e)
        {
            string className = this.comboBox1.Text;
            string startTime = this.dateTimePicker1.Value.ToString("yyyy-MM-dd");
            string endTime = this.dateTimePicker2.Value.ToString("yyyy-MM-dd");
            DataTable dt = studentInfoCtl.GetClassCheckInfo(className, startTime, endTime);
            //DataColumn dc = new DataColumn("序号", typeof(int));
            //dt.Columns.Add(dc);
            this.dataGridView1.DataSource = dt;
            this.dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            int headerW = this.dataGridView1.RowHeadersWidth;
            int columnsW = 0;
            DataGridViewColumnCollection columns = this.dataGridView1.Columns;
            if (columns.Count > 0)
            {
                for (int i = 0; i < columns.Count; i++)
                {
                    columnsW += columns[i].Width;
                }
                if (columnsW + headerW < this.dataGridView1.Width)
                {
                    int leftTotalWidht = this.dataGridView1.Width - columnsW - headerW;
                    int eachColumnAddedWidth = leftTotalWidht / (columns.Count);
                    for (int i = 0; i < columns.Count; i++)
                    {
                        columns[i].Width += eachColumnAddedWidth;
                    }
                }
            }

            /////////////////////////////////////////////////////////////////////////////////////
            double y = 0;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                try
                {
                    DateTime date = DateTime.Parse(dr["考勤时间"].ToString());
                    string rate = dr["出勤率"].ToString();
                    y = double.Parse(rate);
                    ChartSeriesValue csv = new ChartSeriesValue(date, (decimal)y);
                    this.chart1.Series.Values.Add(csv);
                    if (i == 0)
                    {
                        this.chart1.XAxis.Scale.Minimum = date.AddDays(-0.5);
                    }
                    if (i == dt.Rows.Count - 1)
                    {
                        this.chart1.XAxis.Scale.Maximum = date.AddDays(0.5);
                    }
                }
                catch (System.Exception ex)
                {

                }

            }

            /////////////////////////////////////////////////////////////////////////////////////


            /*
            barChart.Items.Clear();

            Color clrGreen = Color.FromArgb(255, 82, 206, 60);
            Color clrYellow = Color.FromArgb(255, 234, 234, 134);
            Color clrRed = Color.FromArgb(255, 253, 95, 77);
            double y = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                try
                {
                    string date = dr["考勤时间"].ToString().Substring(0, 10);
                    string rate = dr["出勤率"].ToString();
                    y = double.Parse(rate);
                    Color clrSelected = clrGreen;
                    if (y < 85)
                    {
                        clrSelected = clrYellow;
                    }
                    if (y < 60)
                    {
                        clrSelected = clrRed;
                    }
                    barChart.Add(rate + "%", y, (i + 1).ToString(), clrSelected);
                    //barChart.Add(rate + "%", y, (i + 1).ToString(), Color.FromArgb(255, 200, 255, 255));
                }
                catch (System.Exception ex)
                {

                }

            }
            barChart.RedrawChart();
            */


            /*
            GraphPane myPane = zgc.GraphPane;

            // Set the Titles

            myPane.Title.Text = "出勤率折线图";
            myPane.XAxis.Title.Text = "考勤时间";
            myPane.YAxis.Title.Text = "出勤率";

            // Make up some data arrays based on the Sine function

            double x, y;
            PointPairList list = new PointPairList();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                try
                {
                    DateTime date = DateTime.Parse(dr["考勤时间"].ToString());
                    x = (double)new XDate(date.Year, date.Month, date.Day);
                    y = double.Parse(dr["出勤率"].ToString());
                    list.Add(x, y);
                }
                catch (System.Exception ex)
                {
                	
                }

            }

            // Generate a red curve with diamond

            // symbols, and "Porsche" in the legend

            LineItem myCurve = myPane.AddCurve("出勤率折线图",
                  list, Color.Red, SymbolType.Diamond);


            // Tell ZedGraph to refigure the

            // axes since the data have changed

            zgc.AxisChange();
            */
        }
    }
}
