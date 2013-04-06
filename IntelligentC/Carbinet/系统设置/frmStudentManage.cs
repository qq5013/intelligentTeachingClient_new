using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using intelligentMiddleWare;
using MetroFramework.Forms;

namespace Carbinet
{
    public partial class frmStudentManage : MetroForm, I_event_notify
    {
        public frmStudentManage()
        {
            InitializeComponent();
            this.FormClosed += new FormClosedEventHandler(FrmRfidCheck_StudentManage_FormClosed);
        }

        #region 事件处理
        void FrmRfidCheck_StudentManage_FormClosed(object sender, FormClosedEventArgs e)
        {

        }
        private void btnBind_Click(object sender, EventArgs e)
        {
            string id = this.txtId.Text;
            if (id == string.Empty || id.Length <= 0)
            {
                MessageBox.Show("请选择一名学生！");
                return;
            }
            string epc = this.txtEpc.Text;
            if (epc == string.Empty || epc.Length <= 0)
            {
                DialogResult dr = MessageBox.Show("当前学生卡号为空，这将会将删除该学生的学生卡信息，确定吗？", "提示", MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                {
                    StudentManagement.UnbindPersonEPC(id);
                    this.refreshDBInfo();
                }
            }
            else
            {
                StudentManagement.BindPersonEPC(id, epc);
                this.refreshDBInfo();
            }
        }

        private void FrmRfidCheck_StudentManage_Load(object sender, EventArgs e)
        {
            this.refreshDBInfo();
        }
        private void refreshDBInfo()
        { 
            ShowPerson();
            SetLabelContent();        
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                dataGridView1.Rows[e.RowIndex].Selected = true;
                this.SetLabelContent(dataGridView1.Rows[e.RowIndex]);
            }
        }
        #endregion

        #region 内部函数
        private void ShowPerson()
        {
            DataTable myData = StudentManagement.GetPersonDataTable();
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

            MiddleWareCore.set_mode(MiddleWareMode.学生卡绑定, this);
        }

        private void SetLabelContent()
        {
            DataGridViewRow dr = null;
            if (this.dataGridView1.Rows.Count > 0)
            {
                dr = this.dataGridView1.Rows[0];
                this.SetLabelContent(dr);
            }
        }
        private void SetLabelContent(DataGridViewRow dr)
        {
            if (dr != null)
            {
                txtId.Text = dr.Cells["学号"].Value.ToString();
                txtName.Text = dr.Cells["姓名"].Value.ToString();
                txtbj.Text = dr.Cells["班级"].Value.ToString();
                txtAge.Text = dr.Cells["性别"].Value.ToString();
                txtMail.Text = dr.Cells["邮箱"].Value.ToString();
                txtEpc.Text = dr.Cells["学生卡"].Value.ToString();
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

        #endregion


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
                    this.txtEpc.Text = epcID;
                };

                this.Invoke(dele, evt);
            }
        }

    }
}
