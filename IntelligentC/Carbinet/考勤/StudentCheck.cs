using intelligentMiddleWare;
using MetroFramework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Carbinet
{
    public class StudentCheck : I_event_notify, I_event_handler, I_call_back
    {

        #region 成员
        string record_id = string.Empty;
        I_mini_form_show_notify formToNotify;
        #region 各个选项对应的颜色
        Color clrChecked = Color.FromArgb(243, 119, 53);
        Color clrUnchecked = Color.FromArgb(0, 174, 219);
        // legend
        List<string> textList = null;
        List<MetroColorStyle> styleList = null;
        #endregion
        DataTable dtCheckRecord = null;
        #endregion

        public StudentCheck(string _record_id)
        {
            this.record_id = _record_id;
            this.formToNotify = Program.frmFloat;//通知主菜单显示特定信息

            this.textList = new List<string>();
            this.textList.Add("通过");
            this.textList.Add("迟到");
            this.styleList = new List<MetroColorStyle>();
            this.styleList.Add(MetroColorStyle.Orange);
            this.styleList.Add(MetroColorStyle.Blue);
        }

        #region 工具函数
        private List<string> getcaptionList()
        {
            List<string> captionList = new List<string>();
            captionList.Add("通过");
            captionList.Add("迟到");
            return captionList;
        }
        private List<Color> getColorList()
        {
            List<Color> colorList = new List<Color>();
            colorList.Add(this.clrChecked);
            colorList.Add(this.clrUnchecked);
            return colorList;
        }
        private List<int> getValueList(int a, int b)
        {
            List<int> valueList = new List<int>();
            valueList.Add(a);
            valueList.Add(b);
            return valueList;
        }
        void NotifyFormToRefreshPie(List<int> valueList, bool bShowLabel)
        {
            List<string> captionList = this.getcaptionList();
            List<Color> colorList = getColorList();
            if (this.formToNotify != null)
            {
                this.formToNotify.refreshPie(captionList, valueList, colorList, bShowLabel);
            }
        }
        #region setChairState
        private void setChairState(equipmentPosition ep, DocumentFileState dfs, string text)
        {
            setChairState(ep, dfs);
            Program.frmClassRoom.changeChairState(ep.group, ep.formatedPosition(), text);
        }
        private void setChairState(equipmentPosition ep, string text)
        {
            Program.frmClassRoom.changeChairState(ep.group, ep.formatedPosition(), text);
        }

        private void setChairState(equipmentPosition ep, DocumentFileState dfs)
        {
            Program.frmClassRoom.changeChairState(ep.group, ep.formatedPosition(), dfs);
        }
        #endregion
        #endregion

        #region 内部函数
        private void setPieToInitializeState()
        {
            List<int> valueList = this.getValueList(1, 1);
            NotifyFormToRefreshPie(valueList, false);
        }

        private void initialInfoTable()
        {
            this.dtCheckRecord = new DataTable();
            this.dtCheckRecord.Columns.Add("studenID", typeof(string));
            this.dtCheckRecord.Columns.Add("checked", typeof(string));
            this.dtCheckRecord.Columns.Add("checkTime", typeof(string));

            int totalCount = MemoryTable.studentInfoTable.Rows.Count;
            for (int i = 0; i < totalCount; i++)
            {
                this.dtCheckRecord.Rows.Add(new object[] { MemoryTable.studentInfoTable.Rows[i]["STUDENTID"], "" });
            }
        }

        private void addStudentRecord(string epcID, string checkTime)
        {
            DataRow[] rows = this.dtCheckRecord.Select(string.Format("studenID = '{0}'", epcID));
            if (rows.Length > 0)
            {
                rows[0]["checked"] = "yes";
                rows[0]["checkTime"] = checkTime;
            }
            studentInfoCtl.AddCheckInfo(this.record_id, epcID, checkTime);
        }
        private void refreshPie()
        {
            DataRow[] rowsChecked = this.dtCheckRecord.Select("checked = 'yes'");
            DataRow[] rowsUnchecked = this.dtCheckRecord.Select("checked <> 'yes'");

            int iA = rowsChecked.Length;
            int iB = rowsUnchecked.Length;

            List<int> valueList = this.getValueList(iA, iB);
            NotifyFormToRefreshPie(valueList, true);
        }
        private void handle_event()
        {
            IntelligentEvent evt = MiddleWareCore.get_a_event();
            if (evt != null)
            {
                IntelligentEvent p = (IntelligentEvent)evt;
                string epcID = p.epcID;
                string remoteDeviceID = p.remoteDeviceID;
                string studentName = string.Empty;
                bool bRefresh_ui = false;

                Person person = MemoryTable.getPersonByEpc(epcID);
                equipmentPosition ep = MemoryTable.getEquipmentConfigMapInfoByDeviceID(remoteDeviceID);
                if (person != null && ep != null)
                {
                    if (p.event_unit_list.IndexOf(IntelligentEventUnit.epc_on_another_device) >= 0)//重复考勤
                    {
                        ////这里要处理一下同一个学生用不一个设备发送答案的情况
                        equipmentPosition ep_old = MemoryTable.getEquipmentInfoByEpc(epcID);
                        this.setChairState(ep_old, DocumentFileState.InitialState, "");
                        MemoryTable.clearEquipmentAndStudentCombining(epcID);

                        bRefresh_ui = true;
                    }
                    if (p.event_unit_list.IndexOf(IntelligentEventUnit.new_epc) >= 0)//第一次考勤 
                    {
                        //更新考勤信息
                        this.addStudentRecord(person.id_num, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        bRefresh_ui = true;
                    }
                    if (bRefresh_ui == true)
                    {
                        if (person != null)
                        {
                            studentName = person.name;
                            this.setChairState(ep, studentName);
                            MemoryTable.setEquipmentInfoCombineStudentID(ep, person.epc);
                        }
                        this.refreshPie();
                    }
                }

            }
        }

        #endregion

        #region 接口函数
        public void receive_a_new_event()
        {
            this.handle_event();
        }

        public void callback()
        {
            int carbinetIndex = Program.frmClassRoom.carbinetIndex;
            int floorNumber = Program.frmClassRoom.floorNumber;
            int columnNumber = Program.frmClassRoom.columnNumber;
            string epc = MemoryTable.getPersonEpcByPosition(carbinetIndex, floorNumber, columnNumber);
            Person p = MemoryTable.getPersonByEpc(epc);
            frmShowStudentInfo frm = new frmShowStudentInfo(p.id_num);
            frm.ShowDialog();
        }
        public void prepare_handler()
        {
            MiddleWareCore.set_mode(MiddleWareMode.考勤, this);
            Program.frmFloat.setLegend(this.textList, this.styleList);

            initialInfoTable();
            setPieToInitializeState();

            Program.frmClassRoom.resetClassRoomState();

            Program.frmClassRoom.setCallBackInvoker(this);//点击座位时的回调
            this.formToNotify.show_tip("正在考勤");
        }
        public void closeHandler()
        {
        }
        #endregion





    }
}
