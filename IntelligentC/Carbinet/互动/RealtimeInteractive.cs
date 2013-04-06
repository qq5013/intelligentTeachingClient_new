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
    public class RealtimeInteractive : I_event_notify, I_event_handler, I_call_back
    {
        DataTable dtAnswerRecord = null;
        I_mini_form_show_notify formToNotify;
        #region 各个选项对应的颜色
        Color clrA = Color.FromArgb(0, 177, 89);
        Color clrB = Color.FromArgb(243, 119, 53);
        Color clrC = Color.FromArgb(209, 17, 65);
        Color clrD = Color.FromArgb(0, 174, 219);

        // legend
        List<string> textList = null;
        List<MetroColorStyle> styleList = null;
        #endregion
        public RealtimeInteractive(I_mini_form_show_notify _notify_form)
        {
            this.formToNotify = _notify_form;

            this.textList = new List<string>();
            this.textList.Add("A 完全明白");
            this.textList.Add("B 一知半解");
            this.textList.Add("C 不知所云");
            this.textList.Add("D 尚未选择");
            this.styleList = new List<MetroColorStyle>();
            this.styleList.Add(MetroColorStyle.Green);
            this.styleList.Add(MetroColorStyle.Orange);
            this.styleList.Add(MetroColorStyle.Red);
            this.styleList.Add(MetroColorStyle.Blue);
        }
        //public void SetShowForm(I_mini_form_show_notify _notify_form)
        //{
        //    this.formToNotify = _notify_form;
        //}
        public void receive_a_new_event()
        {
            this.handle_event();
        }
        private void handle_event()
        {
            IntelligentEvent evt = MiddleWareCore.get_a_event();
            if (evt != null)
            {

                IntelligentEvent IEvent = (IntelligentEvent)evt;
                string epcID = IEvent.epcID;
                string remoteDeviceID = IEvent.remoteDeviceID;
                string check_time = IEvent.time_stamp;
                string studentName = string.Empty;
                string question_value = IEvent.questionValue.ToUpper();;

                //终端发送的答案必须符合要求 A B C 或者 D

                if (question_value != "A" && question_value != "B" && question_value != "C" && question_value != "D")
                {
                    return;
                }

                //1 更改本地信息
                //2 更改饼图
                //3 更改座位状态

                int totalCount = MemoryTable.studentInfoTable.Rows.Count;

                Person person = MemoryTable.getPersonByEpc(epcID);
                equipmentPosition ep = MemoryTable.getEquipmentConfigMapInfoByDeviceID(remoteDeviceID);
                if (ep != null)
                {
                    if (IEvent.event_unit_list.IndexOf(IntelligentEventUnit.epc_on_another_device) >= 0)
                    {
                        ////这里要处理一下同一个学生用不一个设备发送答案的情况
                        equipmentPosition ep_old = MemoryTable.getEquipmentInfoByEpc(epcID);
                        this.setChairState(ep_old, DocumentFileState.InitialState, "");
                        MemoryTable.clearEquipmentAndStudentCombining(epcID);
                    }

                    if (person != null)
                    {
                        studentName = person.name;
                        this.setChairState(ep, studentName);
                        MemoryTable.setEquipmentInfoCombineStudentID(ep, person.epc);
                        this.setPersonAnswer(person.id_num, question_value);
                    }


                    DocumentFileState dfs = this.getStateByAnswer(question_value);
                    this.setChairState(ep, dfs);

                    this.refreshPie();
                }
            }
        }
        private void refreshPie()
        {
            DataRow[] rowsA = this.dtAnswerRecord.Select("answer = 'A'");
            DataRow[] rowsB = this.dtAnswerRecord.Select("answer = 'B'");
            DataRow[] rowsC = this.dtAnswerRecord.Select("answer = 'C'");
            DataRow[] rowsD = this.dtAnswerRecord.Select("answer = 'D'");

            int iA = rowsA.Length;
            int iB = rowsB.Length;
            int iC = rowsC.Length;
            int iD = rowsD.Length;

            List<int> valueList = this.getValueList(iA, iB, iC, iD);
            NotifyFormToRefreshPie(valueList, true);
        }
        #region 工具函数
        private List<string> getcaptionList()
        {
            List<string> captionList = new List<string>();
            captionList.Add("A");
            captionList.Add("B");
            captionList.Add("C");
            captionList.Add("D");
            return captionList;
        }
        private List<Color> getColorList()
        {
            List<Color> colorList = new List<Color>();
            colorList.Add(this.clrA);
            colorList.Add(this.clrB);
            colorList.Add(this.clrC);
            colorList.Add(this.clrD);
            return colorList;
        }
        private List<int> getValueList(int a, int b, int c, int d)
        {
            List<int> valueList = new List<int>();
            valueList.Add(a);
            valueList.Add(b);
            valueList.Add(c);
            valueList.Add(d);
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
        #endregion

        private void setPieToInitializeState()
        {
            List<int> valueList = this.getValueList(1, 1, 1, 1);
            NotifyFormToRefreshPie(valueList, false);
        }

        private DocumentFileState getStateByAnswer(string answer)
        {
            DocumentFileState dfs = DocumentFileState.InitialState;
            switch (answer)
            {
                case "A":
                    dfs = DocumentFileState.Green;
                    break;
                case "B":
                    dfs = DocumentFileState.Orange;
                    break;
                case "C":
                    dfs = DocumentFileState.Red;
                    break;
                case "D":
                    dfs = DocumentFileState.Blue;
                    break;
            }
            return dfs;
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

        #region setPersonAnswer
        private void setPersonAnswer(string answer)
        {
            int total = this.dtAnswerRecord.Rows.Count;
            for (int i = 0; i < total; i++)
            {
                DataRow dr = this.dtAnswerRecord.Rows[i];
                dr["answer"] = answer;
            }
        }
        private void setPersonAnswer(string studentID, string answer)
        {
            DataRow[] rows = this.dtAnswerRecord.Select(string.Format("studenID = '{0}'", studentID));
            if (rows.Length > 0)
            {
                rows[0]["answer"] = answer;
            }
            else
            {
                this.dtAnswerRecord.Rows.Add(new object[] { studentID, answer });
            }
        }
        #endregion

        private void initialInfoTable()
        {
            this.dtAnswerRecord = new DataTable();
            this.dtAnswerRecord.Columns.Add("studenID", typeof(string));
            this.dtAnswerRecord.Columns.Add("answer", typeof(string));

            int totalCount = MemoryTable.studentInfoTable.Rows.Count;
            for (int i = 0; i < totalCount; i++)
            {
                this.dtAnswerRecord.Rows.Add(new object[] { MemoryTable.studentInfoTable.Rows[i]["STUDENTID"], "" });
            }
        }

        public void prepare_handler()
        {
            MiddleWareCore.set_mode(MiddleWareMode.实时互动, this);
            Program.frmClassRoom.setCallBackInvoker(this);//点击座位时的回调
            Program.frmFloat.setLegend(this.textList,this.styleList);
            initialInfoTable();
            this.setPersonAnswer("D");
            setPieToInitializeState();

            Program.frmClassRoom.resetClassRoomState();
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
    }
}
