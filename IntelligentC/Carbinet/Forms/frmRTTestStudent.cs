using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace Carbinet
{
    public partial class frmRTTestStudent : Form
    {
        frmRTTest frmParent = null;
        public frmRTTestStudent(frmRTTest frm)
        {
            this.frmParent = frm;
            InitializeComponent();

            this.Shown += new EventHandler(frmRTTestStudent_Shown);
        }

        void frmRTTestStudent_Shown(object sender, EventArgs e)
        {
            this.groupBoxQuestion.Left = 0;
            this.groupBoxQuestion.Width = this.Width;


            this.lblTime.Left = this.Width - this.lblTime.Width - 20;
            this.lblTime.Top = this.Height - this.lblTime.Height - 20;
            this.lblInfo.Left = this.lblTime.Left;
        }

        void frmParent_eventInvokeCommand(InternalCommand cmd, object o)
        {
            Debug.WriteLine(
                string.Format("frmRTTestStudent.frmParent_eventInvokeCommand  ->  = {0}"
                , cmd.ToString()));
            switch ((int)cmd)
            {
                case (int)InternalCommand.PreQuestion:

                    break;
                case (int)InternalCommand.NextQuestion:
                    break;
                case (int)InternalCommand.CloseForm:
                    if (this.frmParent != null)
                    {
                        this.Close();
                    }
                    break;
            }
        }

    }
}
