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
    public partial class frmClassRoom : MetroForm
    {
        int CHAIR_HEIGHT = 38;
        List<Carbinet> groups = new List<Carbinet>();
        public frmClassRoom()
        {
            InitializeComponent();

            ClassRoomConfig roomConfig = new ClassRoomConfig();
            roomConfig.GroupList.Add(new ClassRoomGroup(0, 8, 2));
            roomConfig.GroupList.Add(new ClassRoomGroup(1, 8, 2));
            roomConfig.GroupList.Add(new ClassRoomGroup(2, 8, 2));
            roomConfig.GroupList.Add(new ClassRoomGroup(3, 8, 2));
            int blackSpace = 100;
            this.InitialClassRoom(roomConfig, this.Width - blackSpace, 100, 40, blackSpace / 2);

            this.Shown += frmSelect_Shown;
            this.FormClosing += frmClassRoom_FormClosing;
        }

        void frmClassRoom_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }
        public void resetClassRoomState()
        {
            foreach (Carbinet c in groups)
            {
                c.resetAllDocState();
            }
        }
        void frmSelect_Shown(object sender, EventArgs e)
        {
            //测试函数
            //this.changeChairState(0, 1, 1, DocumentFileState.Green, "文字测试");
        }
        /*
         对于一个本页面来说，只需要接受改变具体某个座位状态的能力即可
         * 将某个座位设置为枚举的状态
         * 座位上的文字的改变
         * 座位恢复为初始状态
         */
        public void changeChairState(int _groupIndex, int _rowIndex, int _columnIndex, DocumentFileState _state, string _text)
        {
            string id = string.Format("{0},{1},{2}", _groupIndex, _rowIndex, _columnIndex);
            changeChairState(_groupIndex, id, _text);
            changeChairState(_groupIndex, id, _state);
        }
        public void changeChairState(int _groupIndex, string _equipmentID, string _text)
        {
            Carbinet _carbinet = this.groups[_groupIndex];
            _carbinet.setDocText(_equipmentID, _text);
        }
        public void changeChairState(int _groupIndex, string _equipmentID, DocumentFileState _state)
        {
            Carbinet _carbinet = this.groups[_groupIndex];
            _carbinet.setColorStyle(_equipmentID, _state);
            //switch (_state)
            //{
            //    case DocumentFileState.InitialState:
            //        _carbinet.setColorStyle(_equipmentID, _state);
            //        break;
            //    default:
            //        _carbinet.setColorStyle(_equipmentID, _state);
            //        break;
            //}

        }
        #region
        //把教室的座位定义好
        private void InitialClassRoom(ClassRoomConfig _roomConfig, int _widthOfRoom, int _groupTop, int _groupGap, int _firstGroupLeft)
        {
            int heightOfDocumentFile = CHAIR_HEIGHT;

            int countOfGroup = _roomConfig.GroupCount;
            int numberOfUnit = _roomConfig.GetTotalColumn();
            int widthOfUnit = (_widthOfRoom - (countOfGroup - 1) * _groupGap) / numberOfUnit;
            for (int groupIndex = 0; groupIndex < countOfGroup; groupIndex++)
            {
                int countOfColumnInGroup = _roomConfig.GroupList[groupIndex].ColumnCount;
                int countOfFloorInGroup = _roomConfig.GroupList[groupIndex].RowCount;

                int currentGroupWidth = countOfColumnInGroup * widthOfUnit;

                Carbinet group = new Carbinet(this.Controls);
                group.Left = _firstGroupLeft;
                group.Top = _groupTop;
                this.groups.Add(group);
                //初始化每一排的行
                int initialTop = 0;
                for (int iFloorIndex = 1; iFloorIndex <= countOfFloorInGroup; iFloorIndex++, initialTop = initialTop + (int)(1.7 * heightOfDocumentFile))
                {
                    group.AddFloor(this.initialFloor(group, iFloorIndex, currentGroupWidth, heightOfDocumentFile, initialTop));

                    for (int columnIndex = 1; columnIndex <= countOfColumnInGroup; columnIndex++)
                    {
                        string _equipmentID = string.Format("{0},{1},{2}", groupIndex, iFloorIndex, columnIndex);
                        group.AddDocFile(this.initialDocumentFile(_equipmentID, iFloorIndex, widthOfUnit, heightOfDocumentFile, groupIndex, iFloorIndex, columnIndex, null));
                    }
                }
                _firstGroupLeft += currentGroupWidth + _groupGap;
            }
        }
        CarbinetFloor initialFloor(Carbinet group, int irow, int _rowWidth, int _rowHeight, int _rowTop)
        {
            CarbinetFloor row = new CarbinetFloor(group, irow, this.Controls);
            row.Width = _rowWidth;
            row.Height = _rowHeight;
            row.relativeTop = _rowTop;
            row.relativeLeft = 0;

            return row;
        }
        DocumentFile initialDocumentFile(string _equipmentID, int _iFloor, int _dfWidth, int _dfHeight, int _carbinetIndex, int _floorNumber, int _columnNumber, EventHandler handler)
        {
            DocumentFile df = new DocumentFile(_equipmentID, _iFloor);
            df.Width = _dfWidth;
            df.Height = _dfHeight;
            df.carbinetIndex = _carbinetIndex;
            df.floorNumber = _floorNumber;
            df.columnNumber = _columnNumber;
            df.indexBase = _columnNumber.ToString();
            df.Click += handler;
            return df;
        }

        #endregion
    }
}
