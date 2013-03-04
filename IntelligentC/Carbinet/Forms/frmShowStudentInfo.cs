using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Carbinet
{
    public partial class frmShowStudentInfo : Form
    {
        public frmShowStudentInfo(string studendID)
        {
            InitializeComponent();
            studentInfoCtl stuCtl = new studentInfoCtl();
            DataTable dt = stuCtl.getStudentInfo(studendID);
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                this.lblName.Text = (string)dr["NAME"];
                this.lblID.Text = (string)dr["STUDENTID"];
                this.lblClass.Text = (string)dr["CLASS_NAME"];
                this.lblEmail.Text = (string)dr["EMAIL"];
            }
            else
            {
                this.lblName.Text = "";
                this.lblID.Text = "";
                this.lblClass.Text = "";
                this.lblEmail.Text = "";
            }

        }
    }
}
