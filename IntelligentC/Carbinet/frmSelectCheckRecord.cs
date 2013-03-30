using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Carbinet
{
    public partial class frmSelectCheckRecord : MetroForm
    {
        public string record_id = string.Empty;
        frmCheckInit frmCheckInit = null;
        public frmSelectCheckRecord(frmCheckInit frmCheckInit)
        {
            InitializeComponent();
            this.frmCheckInit = frmCheckInit;
            try
            {
                DataTable dt = CheckCtl.getAllCheckRecord();
                this.dgv1.DataSource = dt;

                this.format_dgv(this.dgv1);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

            this.dgv1.CellClick += dgv1_CellClick;
        }

        void dgv1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                dgv1.Rows[e.RowIndex].Selected = true;
                record_id = dgv1.Rows[e.RowIndex].Cells["考勤记录"].Value.ToString();
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (record_id == string.Empty)
            {
                MessageBox.Show("请至少选择一项记录，或者直接退出！");
                return;
            }
            this.frmCheckInit.setRecordID(this.record_id);
            this.Close();
        }

        void format_dgv(DataGridView dgv)
        {
            dgv.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            int headerW = dgv.RowHeadersWidth;
            int columnsW = 0;
            DataGridViewColumnCollection columns = dgv.Columns;
            for (int i = 0; i < columns.Count; i++)
            {
                columnsW += columns[i].Width;
            }
            if (columnsW + headerW < dgv.Width)
            {
                int leftTotalWidht = dgv.Width - columnsW - headerW - 2;
                int eachColumnAddedWidth = leftTotalWidht / columns.Count;
                for (int i = 0; i < columns.Count; i++)
                {
                    columns[i].Width += eachColumnAddedWidth;
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
