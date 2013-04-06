using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Carbinet
{
    public partial class frmShowStudentInfo : MetroForm
    {
        public frmShowStudentInfo(string studendID)
        {
            InitializeComponent();
            this.lblID.Text = studendID;

            Person person = MemoryTable.getPersonByID(studendID);
            if (person != null)
            {
                this.lblName.Text = person.name;
                this.lblClass.Text = person.bj;
                this.lblEmail.Text = person.email;
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
