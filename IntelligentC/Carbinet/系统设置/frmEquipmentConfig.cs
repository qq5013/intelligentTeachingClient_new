using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using intelligentMiddleWare;

namespace Carbinet
{
    public partial class frmEquipmentConfig : Form, I_event_notify
    {
        public Form previousForm = null;
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


            if (MemoryTable.isInitialized == false)
            {
                MemoryTable.initializeTabes();
            }
            this.dtRoomConfig = MemoryTable.dtRoomConfig;
            //dtRoomConfig = this.configCtl.getAllRoomConfigInfo();
            //dtRoomConfig.Columns.Add("totalColumn", typeof(int));
            //dtRoomConfig.Columns["totalColumn"].Expression = "Sum(ICOLUMN)";
            //dtRoomConfig.Columns.Add("maxGroup", typeof(int));
            //dtRoomConfig.Columns["maxGroup"].Expression = "Max(IGROUP)";


            this.Shown += new EventHandler(frmEquipmentConfig_Shown);
            this.FormClosing += new FormClosingEventHandler(frmEquipmentConfig_FormClosing);
            this.FormClosed += new FormClosedEventHandler(frmEquipmentConfig_FormClosed);

            MiddleWareCore.set_mode(MiddleWareMode.设备绑定);
            MiddleWareCore.event_receiver = this;
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

        void frmEquipmentConfig_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (this.previousForm != null)
            {
                this.previousForm.Show();
            }
        }

        void frmEquipmentConfig_Shown(object sender, EventArgs e)
        {
            this.InitialClassRoom();
            DataRow[] rows = dtRoomConfig.Select("IGROUP=1");
            if (rows.Length > 0)
            {
                int column = int.Parse(rows[0]["ICOLUMN"].ToString());
                int row = int.Parse(rows[0]["IROW"].ToString());
                this.numCountofColumn.Value = (decimal)column;
                this.numCountofRow.Value = (decimal)row;
            }
        }
        private void InitialClassRoom()
        {
            if (this.dtRoomConfig == null)
            {
                return;
            }
            //获取设备和位置的对应数据
            DataTable dt = this.ctl.getAllMapConfigs();

            int numberOfGroup = dtRoomConfig.Rows.Count;
            int widthOfRoom = this.pictureBox1.Width;
            int heightOfRow = 38;

            int totalColumns = numberOfGroup;
            DataRow[] rows4Sum = dtRoomConfig.Select("IGROUP=1");
            if (rows4Sum.Length > 0)
            {
                totalColumns = int.Parse(rows4Sum[0]["totalColumn"].ToString());
            }
            int numberOfUnit = totalColumns + numberOfGroup - 1;
            int widthOfUnit = widthOfRoom / numberOfUnit;
            int groupInitialLeft = 0;

            for (int i = 0; i < numberOfGroup; i++)
            {
                int numberofColumn = 1;
                int numberOfRow = 1;

                DataRow[] rows = dtRoomConfig.Select(string.Format("IGROUP={0}", i + 1));
                if (rows.Length > 0)
                {
                    numberofColumn = int.Parse(rows[0]["ICOLUMN"].ToString());
                    numberOfRow = int.Parse(rows[0]["IROW"].ToString());
                }
                int groupWidth = numberofColumn * widthOfUnit;

                Carbinet group = new Carbinet(this.pictureBox1.Controls);
                group.Left = groupInitialLeft;
                group.Top = 67;
                //this.groups.Add(group);
                //初始化每一排的行
                int initialTop = 0;
                for (int irow = 1; irow <= numberOfRow; irow++, initialTop = initialTop + (int)(1.7 * heightOfRow))
                {
                    CarbinetFloor row = new CarbinetFloor(group, irow, this.pictureBox1.Controls);
                    row.Width = groupWidth;
                    row.Height = heightOfRow;
                    row.relativeTop = initialTop;
                    row.relativeLeft = 0;

                    group.AddFloor(row);

                    for (int k = 1; k <= numberofColumn; k++)
                    {
                        string _equipmentID = i.ToString() + "," + irow.ToString() + "," + k.ToString();
                        DocumentFile df = new DocumentFile(_equipmentID, irow);
                        df.Width = widthOfUnit;
                        df.Height = heightOfRow;
                        DataRow[] rowsMap = dt.Select(
                            string.Format("IGROUP = {0} and IROW = {1} and ICOLUMN = {2}",
                                            i.ToString(), irow.ToString(), k.ToString()));
                        if (rowsMap.Length > 0)
                        {
                            df.Text = (string)rowsMap[0]["EQUIPEMNTID"];
                        }
                        else
                        {
                            df.Text = "";
                        }
                        df.carbinetIndex = i;
                        df.floorNumber = irow;
                        df.columnNumber = k;
                        df.indexBase = k.ToString();
                        //df.doc.Click += new EventHandler(doc_Click);
                        df.Click += new EventHandler(doc_Click);
                        group.AddDocFile(df);
                    }

                }
                groupInitialLeft += groupWidth + widthOfUnit;


            }
            return;
            //int numberOfGroup = (int)this.numCountofGroup.Value;
            //int numberofColumn = (int)this.numCountofColumn.Value;
            //int numberOfRow = (int)this.numCountofRow.Value;


            //int numberOfUnit = numberOfGroup * numberofColumn + numberOfGroup - 1;
            //int widthOfUnit = widthOfRoom / numberOfUnit;
            //int groupInitialLeft = 0;
            //int groupWidth = numberofColumn * widthOfUnit;

            //for (int i = 0; i < numberOfGroup; i++)
            //{
            //    Carbinet group = new Carbinet(this.pictureBox1.Controls);
            //    group.Left = groupInitialLeft;
            //    group.Top = 67;
            //    //this.groups.Add(group);
            //    //初始化每一排的行
            //    int initialTop = 0;
            //    for (int irow = 1; irow <= numberOfRow; irow++, initialTop = initialTop + (int)(1.7 * heightOfRow))
            //    {
            //        CarbinetFloor row = new CarbinetFloor(group, irow, this.pictureBox1.Controls);
            //        row.Width = groupWidth;
            //        row.Height = heightOfRow;
            //        row.relativeTop = initialTop;
            //        row.relativeLeft = 0;

            //        group.AddFloor(row);

            //        for (int k = 1; k <= numberofColumn; k++)
            //        {
            //            string _equipmentID = i.ToString() + "," + irow.ToString() + "," + k.ToString();
            //            DocumentFile df = new DocumentFile(_equipmentID, irow);
            //            df.Width = widthOfUnit;
            //            df.Height = heightOfRow;
            //            DataRow[] rows = dt.Select(
            //                string.Format("IGROUP = {0} and IROW = {1} and ICOLUMN = {2}",
            //                                i.ToString(), irow.ToString(), k.ToString()));
            //            if (rows.Length > 0)
            //            {
            //                df.Text = (string)rows[0]["EQUIPEMNTID"];
            //            }
            //            else
            //            {
            //                df.Text = "";
            //            }
            //            df.carbinetIndex = i;
            //            df.floorNumber = irow;
            //            df.columnNumber = k;
            //            df.indexBase = k.ToString();
            //            //df.doc.Click += new EventHandler(doc_Click);
            //            df.Click += new EventHandler(doc_Click);
            //            group.AddDocFile(df);
            //        }

            //    }
            //    groupInitialLeft += groupWidth + widthOfUnit;
            //}

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

            this.pictureBox1.Controls.Clear();

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

            this.InitialClassRoom();
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
            this.pictureBox1.Controls.Clear();
            this.InitialClassRoom();
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
            this.pictureBox1.Controls.Clear();
            this.InitialClassRoom();
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
