using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using intelligentMiddleWare;

namespace Carbinet
{
    public partial class FrmRfidCheck_StudentManage : Form, I_event_notify
    {
        public FrmRfidCheck_StudentManage()
        {
            InitializeComponent();
            this.FormClosed += new FormClosedEventHandler(FrmRfidCheck_StudentManage_FormClosed);
        }

        void FrmRfidCheck_StudentManage_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void ShowPerson()
        {
            DataTable myData = rfidCheck_CheckOn.GetPersonDataSet();
            //if (null == myDataSet)
            //{
            //    return;
            //}
            //dataGridView1.DataSource = myDataSet.Tables[0];
            dataGridView1.DataSource = myData;

            int iNumberofStudents = myData.Rows.Count;
            this.groupBox2.Text = "学生列表 共有学生" + iNumberofStudents.ToString() + "名";

            this.dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            int headerW = this.dataGridView1.RowHeadersWidth;
            int columnsW = 0;
            DataGridViewColumnCollection columns = this.dataGridView1.Columns;
            for (int i = 0; i < columns.Count; i++)
            {
                columnsW += columns[i].Width;
            }
            if (columnsW + headerW < this.dataGridView1.Width)
            {
                int leftTotalWidht = this.dataGridView1.Width - columnsW - headerW;
                int eachColumnAddedWidth = leftTotalWidht / columns.Count;
                for (int i = 0; i < columns.Count; i++)
                {
                    columns[i].Width += eachColumnAddedWidth;
                }
            }
            /* 
            
            dataGridView1.Columns[0].HeaderText = "学号";
            dataGridView1.Columns[1].HeaderText = "姓名";
            dataGridView1.Columns[2].HeaderText = "电话";
            dataGridView1.Columns[3].HeaderText = "邮箱";
            */

            MiddleWareCore.set_mode(MiddleWareMode.学生卡绑定);
            MiddleWareCore.event_receiver = this;
        }

        private void FrmRfidCheck_StudentManage_Load(object sender, EventArgs e)
        {
            ShowPerson();
            SetLabelContent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string strID = txtId.Text;
            if (strID.Length < 0 || strID.Length > 6 || !Regex.IsMatch(strID, "[0-9]{6}"))
            {
                //MessageBox.Show("学号应为六位数字！");
                //return;
            }
            string age = string.Empty;
            try
            {
                int iAge = int.Parse(this.txtAge.Text);
                if (iAge <= 0 || iAge > 150)
                {
                    MessageBox.Show("年龄设置有误！");
                    return;
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("年龄设置有误！");
                return;
            }
            //DataSet myDataSet = new DataSet();
            //myDataSet.ReadXml("Person.xml");
            bool writeData = true;
            if (rfidCheck_CheckOn.PersonExist(new Person(strID)))
            {
                writeData = false;
                MessageBox.Show
                    ("已存在学号为" + strID + "的学生!");
                return;
            }

            if (writeData)
            {
                rfidCheck_CheckOn.PersonAdd(new Person(txtId.Text,
                                                        txtName.Text,
                                                        cmbSex.Text,
                                                        int.Parse(this.txtAge.Text),
                                                        txtMail.Text,
                                                        txtbj.Text,
                                                        txtEpc.Text
                                                        ));

                MessageBox.Show("学号为" + txtId.Text +
                    "的学生新增完成!");

            }
            //dataGridView1.DataSource = myDataSet.Tables["student"];
            ShowPerson();
            SetLabelContent();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            DataSet myDataSet = new DataSet();
            bool bUpdated = false;
            bUpdated = rfidCheck_CheckOn.PersonUpdate(new Person(txtId.Text,
                                                        txtName.Text,
                                                        cmbSex.Text,
                                                        int.Parse(this.txtAge.Text),
                                                        txtMail.Text,
                                                        txtbj.Text,
                                                        txtEpc.Text
                                                        ));
            if (bUpdated)
            {
                ShowPerson();
                SetLabelContent();
                MessageBox.Show("学号" + txtId.Text +
                "已经修改完成!!");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string message = "确定要删除学号为【" + txtId.Text + "】的学生记录吗？";
            string caption = "删除确认";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;

            // Displays the MessageBox.

            result = MessageBox.Show(message, caption, buttons);

            if (result == System.Windows.Forms.DialogResult.Yes)
            {


                bool bDeleted = false;
                bDeleted = rfidCheck_CheckOn.PersonDelete(new Person(txtId.Text,
                                                        txtName.Text,
                                                        cmbSex.Text,
                                                        int.Parse(this.txtAge.Text),
                                                        txtMail.Text,
                                                        txtbj.Text,
                                                        txtEpc.Text
                                                        ));
                if (bDeleted)
                {
                    MessageBox.Show("学号" + txtId.Text +
                    "已经删除完成!!");
                    ShowPerson();
                    SetLabelContent();

                }

            }


        }
        void SetLabelContent()
        {
            DataTable tb = (DataTable)dataGridView1.DataSource;
            if (tb != null && tb.Rows.Count > 0)
            {
                txtId.Text = tb.Rows[0][0].ToString();
                txtName.Text = tb.Rows[0][1].ToString();
                //txtTel.Text = tb.Rows[0][2].ToString();
                //txtMail.Text = tb.Rows[0][3].ToString();
                txtbj.Text = tb.Rows[0][3].ToString();
                txtAge.Text = tb.Rows[0][4].ToString();
                txtMail.Text = tb.Rows[0][5].ToString();
                //txtDevice.Text = tb.Rows[0][6].ToString();
            }
            else
            {
                txtId.Text = null;
                txtName.Text = null;
                txtAge.Text = null;
                txtMail.Text = null;
                txtbj.Text = null;
                txtEpc.Text = null;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                dataGridView1.Rows[e.RowIndex].Selected = true;
                txtId.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                txtName.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                cmbSex.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                txtbj.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                txtAge.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                txtMail.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                //txtDevice.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public void receive_a_new_event()
        {
            this.handle_event();
        }
        void handle_event()
        {
            IntelligentEvent evt = MiddleWareCore.get_a_event();
            if (evt != null)
            {
                deleControlInvoke dele = delegate(object o)
                {
                    IntelligentEvent p = (IntelligentEvent)o;
                    string epcID = p.epcID;
                    this.txtId.Text = epcID;
                };

                this.Invoke(dele, evt);
            }
        }



    }
}
