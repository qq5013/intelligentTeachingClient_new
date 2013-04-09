using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Carbinet
{
    public class RoomConfig
    {
        public int group;
        public int row;
        public int column;

        public RoomConfig(int _group, int _row, int _column)
        {
            this.group = _group;
            this.row = _row;
            this.column = _column;
        }


    }
}
