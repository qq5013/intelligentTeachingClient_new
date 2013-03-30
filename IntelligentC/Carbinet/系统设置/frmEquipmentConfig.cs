using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using intelligentMiddleWare;
using System.Diagnostics;
using MetroFramework.Forms;

namespace Carbinet
{
    public partial class frmEquipmentConfig : MetroForm, I_event_notify
    {
        DataTable dtRoomConfig = null;
        EquipmentConfigCtl ctl = new EquipmentConfigCtl();
        roomConfigCtl configCtl = new roomConfigCtl();
        DocumentFile docLink = null;//暂存点击到的位置的链接


        public frmEquipmentConfig()
        {
            InitializeComponent();

            this.numCountofGroup.Value = (decimal)this.ctl.getClassroomConfig(0);
            this.numCountofRow.Value = (decimal)this.ctl.getClassroomConfig(1);
            this.numCountofColumn.Value = (decimal)this.ctl.getClassroomConfig(2);

            this.dtRoomConfig = MemoryTable.dtRoomConfig;

            this.Shown += new EventHandler(frmEquipmentConfig_Shown);
            this.FormClosing += new FormClosingEventHandler(frmEquipmentConfig_FormClosing);

            MiddleWareCore.set_mode(MiddleWareMode.设备绑定);
            MiddleWareCore.event_receiver = this;

            Program.frmClassRoom.resetClassRoomState();
        }

        void frmEquipmentConfig_FormClosing(object sender, FormClosingEventArgs e)
        {
            //保存配置信息 
            for (int i = 0; i < this.dtRoomConfig.Rows.Count; i++)
            {
                DataRow dr = dtRoomConfig.Rows[i];
                int group = int.Parse(dr["IGROUP"].ToString());
                int row = int.Parse(dr["IROW"].ToString());
                int column = int.Parse(dr["ICOLUMN"].ToString());
                if (!this.configCtl.ConfigExists(group))
                {
                    this.configCtl.AddNewConfig(group, row, column);
                }
                else
                {
                    this.configCtl.updateRoomConfig(group, row, 0);
                    this.configCtl.updateRoomConfig(group, column, 1);
                }
            }
        }


        void frmEquipmentConfig_Shown(object sender, EventArgs e)
        {
            DataRow[] rows = dtRoomConfig.Select("IGROUP=1");
            if (rows.Length > 0)
            {
                int column = int.Parse(rows[0]["ICOLUMN"].ToString());
                int row = int.Parse(rows[0]["IROW"].ToString());
                this.numCountofColumn.Value = (decimal)column;
                this.numCountofRow.Value = (decimal)row;
            }
        }

        void doc_Click(object sender, EventArgs e)
        {
            DocumentFile doc = (DocumentFile)sender;
            this.docLink = doc;
            //string[] strA = doc.doc.Text.Split(',');
            try
            {
                this.numLocofGroup.Value = doc.carbinetIndex;
                this.numLocofRow.Value = doc.floorNumber;
                this.numLocofColumn.Value = doc.columnNumber;

                //this.numLocofGroup.Value = decimal.Parse(strA[0]);
                //this.numLocofRow.Value = decimal.Parse(strA[1]);
                //this.numLocofColumn.Value = decimal.Parse(strA[2]);

                this.txtEquipmentID.Text = doc.Text;
            }
            catch (System.Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.docLink != null)
            {
                if (this.txtEquipmentID.Text != null && this.txtEquipmentID.Text.Length > 0)
                {
                    docLink.setText(this.txtEquipmentID.Text);

                    // 保存到数据库中
                    int group = (int)this.numLocofGroup.Value;
                    int row = (int)this.numLocofRow.Value;
                    int column = (int)this.numLocofColumn.Value;
                    if (!this.ctl.CheckExists(group, row, column))
                    {
                        this.ctl.AddMapConfig(this.txtEquipmentID.Text, group, row, column);
                    }
                    else
                    {
                        this.ctl.updateMapConfig(this.txtEquipmentID.Text, group, row, column);
                    }
                }
            }
        }

        private void numCountofGroup_ValueChanged(object sender, EventArgs e)
        {
            this.cmbSelectedRow.Items.Clear();
            int groupNumber = (int)this.numCountofGroup.Value;
            for (int i = 1; i <= groupNumber; i++)
            {
                this.cmbSelectedRow.Items.Add(i.ToString());
            }
            if (this.cmbSelectedRow.SelectedIndex == -1)
            {
                this.cmbSelectedRow.SelectedIndex = 0;
            }

            //this.pictureBox1.Controls.Clear();

            //当group总数增大时，需要插入新数据
            //当总数减少时，则要删除值最大的一行数据
            if (this.dtRoomConfig != null)
            {
                DataRow[] rows = this.dtRoomConfig.Select("IGROUP < 99");
                if (rows.Length > 0)
                {
                    int maxGroup = (int)rows[0]["maxGroup"];
                    if (maxGroup < groupNumber)//调高了排数
                    {
                        this.dtRoomConfig.Rows.Add(new object[] { groupNumber, 1, 1 });
                    }
                    if (maxGroup > groupNumber)//调低了排数
                    {
                        rows = this.dtRoomConfig.Select("IGROUP > " + groupNumber);
                        this.dtRoomConfig.Rows.Remove(rows[0]);
                    }
                }
            }

            //this.InitialClassRoom();
            //this.ctl.updateClassRoomConfig((int)this.numCountofGroup.Value, 0);
        }

        private void numCountofRow_ValueChanged(object sender, EventArgs e)
        {
            if (this.dtRoomConfig == null)
            {
                return;
            }
            int group = this.cmbSelectedRow.SelectedIndex + 1;
            DataRow[] rows = this.dtRoomConfig.Select("IGROUP=" + group);
            if (rows.Length > 0)
            {
                rows[0]["IROW"] = (int)this.numCountofRow.Value;
            }
            //this.pictureBox1.Controls.Clear();
            //this.InitialClassRoom();
            //this.ctl.updateClassRoomConfig((int)this.numCountofRow.Value, 1);
        }

        private void numCountofColumn_ValueChanged(object sender, EventArgs e)
        {
            if (this.dtRoomConfig == null)
            {
                return;
            }
            int group = this.cmbSelectedRow.SelectedIndex + 1;
            DataRow[] rows = this.dtRoomConfig.Select("IGROUP=" + group);
            if (rows.Length > 0)
            {
                rows[0]["ICOLUMN"] = (int)this.numCountofColumn.Value;
            }
            //this.pictureBox1.Controls.Clear();
            //this.InitialClassRoom();
            // this.ctl.updateClassRoomConfig((int)this.numCountofColumn.Value, 2);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbSelectedRow_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.dtRoomConfig == null)
            {
                return;
            }
            int group = this.cmbSelectedRow.SelectedIndex + 1;
            DataRow[] rows = this.dtRoomConfig.Select("IGROUP=" + group);
            if (rows.Length > 0)
            {
                int column = int.Parse(rows[0]["ICOLUMN"].ToString());
                int row = int.Parse(rows[0]["IROW"].ToString());
                this.numCountofColumn.Value = (decimal)column;
                this.numCountofRow.Value = (decimal)row;
            }
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
                    string remoteDeviceID = p.remoteDeviceID;

                    this.txtEquipmentID.Text = remoteDeviceID;
                };

                this.Invoke(dele, evt);
            }
        }
    }
}
