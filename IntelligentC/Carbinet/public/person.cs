using System;
using System.Collections.Generic;
using System.Text;

namespace Carbinet
{
    public class Person
    {
        public string id_num;
        public string name;
        public string sex;
        public string email;
        public int age;
        public string bj;
        //public string nj;
        public string deviceID;

        public Person(string _id, string _name, string _sex, int _age, string _email, string _bj, string _deviceID)
        {
            this.id_num = _id;
            this.name = _name;
            this.sex = _sex;
            this.age = _age;
            this.email = _email;
            this.bj = _bj;
            this.deviceID = _deviceID;
        }
        public Person(string _id)
        {
            this.id_num = _id;
        }

    }
}
