using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Carbinet
{
    public class ClassRoomGroup
    {
        public int Index;
        public int RowCount;
        public int ColumnCount;

        public ClassRoomGroup(int _index, int _rowCount, int _columnCount)
        {
            this.Index = _index;
            this.RowCount = _rowCount;
            this.ColumnCount = _columnCount;
        }
    }
    public class ClassRoomConfig
    {
        List<ClassRoomGroup> groupList = new List<ClassRoomGroup>();

        public List<ClassRoomGroup> GroupList
        {
            get { return groupList; }
            set { groupList = value; }
        }
        public ClassRoomConfig()
        {
        }
        public int GroupCount
        {
            get { return groupList.Count; }
        }
        public int GetTotalColumn()
        {
            int total = 0;
            foreach (ClassRoomGroup config in this.GroupList)
            {
                total += config.ColumnCount;
            }
            return total;
        }

    }
}
