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
    public partial class frmEquipmentConfig : MetroForm, I_event_notify, I_call_back
    {
        public frmEquipmentConfig()
        {
            InitializeComponent();

            this.numCountofGroup.Value = MemoryTable.dtRoomConfig.Rows.Count;
            setCmbSelectItems((int)this.numCountofGroup.Value);
            setDefaultCountOfColumnAndRow();

            Program.frmClassRoom.resetClassRoomState();

            this.refreshChairEquipmentID();

            this.Shown += new EventHandler(frmEquipmentConfig_Shown);
            this.FormClosing += new FormClosingEventHandler(frmEquipmentConfig_FormClosing);
            this.numCountofGroup.ValueChanged += new System.EventHandler(this.numCountofGroup_ValueChanged);
            this.cmbSelectedRow.SelectedIndexChanged += new System.EventHandler(this.cmbSelectedRow_SelectedIndexChanged);
            this.numCountofRow.ValueChanged += new System.EventHandler(this.numCountofRow_ValueChanged);
            this.numCountofColumn.ValueChanged += new System.EventHandler(this.numCountofColumn_ValueChanged);

            MiddleWareCore.set_mode(MiddleWareMode.设备绑定);
            MiddleWareCore.event_receiver = this;
        }
        private void refreshChairEquipmentID()
        {
            Program.frmClassRoom.resetClassRoomState();
            //有设备绑定的座位显示出绑定设备的ID
            List<equipmentPosition> list = MemoryTable.getAllEquipmentConfigMapInfo();
            foreach (equipmentPosition ep in list)
            {
                Program.frmClassRoom.changeChairState(ep.group, ep.formatedPosition(), ep.equipmentID);
            }
            Program.frmClassRoom.Refresh();
        }
        void frmEquipmentConfig_FormClosing(object sender, FormClosingEventArgs e)
        {
            Program.frmClassRoom.setCallBackInvoker(null);
            Program.frmClassRoomConfig = null;
            //保存配置信息 
            //for (int i = 0; i < this.dtRoomConfig.Rows.Count; i++)
            //{
            //    DataRow dr = dtRoomConfig.Rows[i];
            //    int group = int.Parse(dr["IGROUP"].ToString());
            //    int row = int.Parse(dr["IROW"].ToString());
            //    int column = int.Parse(dr["ICOLUMN"].ToString());
            //    if (!this.configCtl.ConfigExists(group))
            //    {
            //        this.configCtl.AddNewConfig(group, row, column);
            //    }
            //    else
            //    {
            //        this.configCtl.updateRoomConfig(group, row, 0);
            //        this.configCtl.updateRoomConfig(group, column, 1);
            //    }
            //}
        }

        private void setDefaultCountOfColumnAndRow()
        {
            DataRow[] rows = MemoryTable.dtRoomConfig.Select("IGROUP=0");
            if (rows.Length > 0)
            {
                int column = int.Parse(rows[0]["ICOLUMN"].ToString());
                int row = int.Parse(rows[0]["IROW"].ToString());
                this.setCountOfColumnAndRow(row, column);
            }
        }

        void frmEquipmentConfig_Shown(object sender, EventArgs e)
        {
            Program.frmClassRoom.setCallBackInvoker(this);
        }

        private void setCountOfColumnAndRow(int countOfRow, int countOfColumn)
        {
            this.numCountofColumn.Value = (decimal)countOfColumn;
            this.numCountofRow.Value = (decimal)countOfRow;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //更新新位置的设备绑定信息，并同步到设置页面中
            MemoryTable.setEquipmentAndPosCombining(carbinetIndex, floorNumber, columnNumber, this.txtEquipmentID.Text);
            //更新座位上的设备ID显示
            this.refreshChairEquipmentID();

            //if (this.docLink != null)
            //{
            //    if (this.txtEquipmentID.Text != null && this.txtEquipmentID.Text.Length > 0)
            //    {
            //        docLink.setText(this.txtEquipmentID.Text);

            //        // 保存到数据库中
            //        int group = (int)this.numLocofGroup.Value;
            //        int row = (int)this.numLocofRow.Value;
            //        int column = (int)this.numLocofColumn.Value;
            //        if (!this.ctl.CheckExists(group, row, column))
            //        {
            //            this.ctl.AddMapConfig(this.txtEquipmentID.Text, group, row, column);
            //        }
            //        else
            //        {
            //            this.ctl.updateMapConfig(this.txtEquipmentID.Text, group, row, column);
            //        }
            //    }
            //}
        }

        private void NotifyClassRoomToRefresh()
        {
            Program.frmClassRoom.resetClassRoomConfig();
        }
        private void setCmbSelectItems(int groupNumber)
        {
            #region 更新下拉列表中的row
            this.cmbSelectedRow.Items.Clear();
            for (int i = 1; i <= groupNumber; i++)
            {
                this.cmbSelectedRow.Items.Add(i.ToString());
            }
            if (this.cmbSelectedRow.SelectedIndex == -1)
            {
                this.cmbSelectedRow.SelectedIndex = 0;
            }
            #endregion
        }
        //group数量发生变化
        private void numCountofGroup_ValueChanged(object sender, EventArgs e)
        {
            int groupNumber = (int)this.numCountofGroup.Value;

            this.setCmbSelectItems(groupNumber);

            //当前只在保存配置数据，当确定上传配置信息时再保存至数据库中，再从数据库中提取信息上传
            //当group总数增大时，需要插入新数据
            //当总数减少时，则要删除值最大的一行数据
            MemoryTable.refreshGroupCount(groupNumber);
            this.NotifyClassRoomToRefresh();
        }
        //row数目发生变化
        private void numCountofRow_ValueChanged(object sender, EventArgs e)
        {
            int group = this.cmbSelectedRow.SelectedIndex;
            MemoryTable.refreshGroupRowCount(group, (int)this.numCountofRow.Value);
            this.NotifyClassRoomToRefresh();
        }
        //column数目发生变化
        private void numCountofColumn_ValueChanged(object sender, EventArgs e)
        {
            int group = this.cmbSelectedRow.SelectedIndex;
            MemoryTable.refreshGroupColumnCount(group, (int)this.numCountofColumn.Value);
            this.NotifyClassRoomToRefresh();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbSelectedRow_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (MemoryTable.dtRoomConfig == null)
            {
                return;
            }
            int group = this.cmbSelectedRow.SelectedIndex;
            DataRow[] rows = MemoryTable.dtRoomConfig.Select("IGROUP=" + group);
            if (rows.Length > 0)
            {
                int column = int.Parse(rows[0]["ICOLUMN"].ToString());
                int row = int.Parse(rows[0]["IROW"].ToString());
                this.setCountOfColumnAndRow(row, column);
                //this.numCountofColumn.Value = (decimal)column;
                //this.numCountofRow.Value = (decimal)row;
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


        int carbinetIndex, floorNumber, columnNumber;

        public void callback()
        {
            carbinetIndex = Program.frmClassRoom.carbinetIndex;
            floorNumber = Program.frmClassRoom.floorNumber;
            columnNumber = Program.frmClassRoom.columnNumber;

            this.numLocofGroup.Value = carbinetIndex;
            this.numLocofRow.Value = floorNumber;
            this.numLocofColumn.Value = columnNumber;
            equipmentPosition ep = MemoryTable.getEquipmentConfigMapInfoByPos(carbinetIndex, floorNumber, columnNumber);
            if (ep != null)
            {
                this.txtEquipmentID.Text = ep.equipmentID;
            }
            else
            {
                this.txtEquipmentID.Text = string.Empty;
            }
        }
    }
}
