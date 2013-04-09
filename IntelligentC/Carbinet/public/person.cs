using System;
using System.Collections.Generic;
using System.Text;

namespace Carbinet
{
    public class equipmentPosition
    {
        public string equipmentID;
        public int group;
        public int row;
        public int column;
        public equipmentPosition(string _id, int _group, int _row, int _column)
        {
            this.equipmentID = _id;
            this.group = _group;
            this.row = _row;
            this.column = _column;
        }
        public string formatedPosition()
        {
            return string.Format("{0},{1},{2}", this.group, row, column);
        }
    }
    public class Person
    {
        public string id_num;
        public string name;
        public string sex;
        public string email;
        public int age;
        public string bj;
        //public string nj;
        public string epc;

        public Person(string _id, string _name, string _sex, int _age, string _email, string _bj, string _epc)
        {
            this.id_num = _id;
            this.name = _name;
            this.sex = _sex;
            this.age = _age;
            this.email = _email;
            this.bj = _bj;
            this.epc = _epc;
        }
        public Person(string _id)
        {
            this.id_num = _id;
        }
        public Person() { }
    }
}
