using System;
using System.Collections.Generic;
using System.Text;

namespace IntelligentTeaching
{
    /*
 * 	[select,主节点地址,子节点地址,rfid卡号,问题序号,子节点的选择]
 * */
    public class SelectCommand
    {
        public string master_node = string.Empty;
        public string child_node = string.Empty;
        public string rfid = string.Empty;
        public string question_index = string.Empty;
        public string value = string.Empty;

        public SelectCommand()
        {

        }
        public SelectCommand(string master_node, string child_node, string rfid, string question_index, string value)
        {
            this.master_node = master_node;
            this.child_node = child_node;
            this.rfid = rfid;
            this.question_index = question_index;
            this.value = value;
        }

        public string GetCommand()
        {
            string cmd = string.Empty;
            cmd = string.Format("[select,{0},{1},{2},{3},{4}]", 
                this.master_node, this.child_node, this.rfid, this.question_index, this.value);
            return cmd;
        }
    }

    public enum Mode
    {
        单选=0,
        多选=1,
        考勤=2
        //信息=2
    }
    //The commands for interaction between the server and the client
    public enum Command
    {
        Login,      //Log into the server
        Logout,     //Logout of the server
        Message,    //Send a text message to all the chat clients
        List,       //Get a list of users in the chat room from the server
        Null        //No command
    }

    //The data structure by which the server and the client interact with 
    //each other
    public class Data
    {
        //Default constructor
        public Data()
        {
            messageType = null;
            equipmentID = null;
            tagID = null;
            key = null;
            value = null;
            this.dataSource = null;
            //this.cmdCommand = Command.Null;
            //this.strMessage = null;
            //this.strName = null;
        }

        //Converts the bytes into an object of type Data
        public Data(byte[] data)
        {
            this.dataSource = Encoding.UTF8.GetString(data).Trim();
            int indexTail = this.dataSource.IndexOf("]");
            this.dataSource = this.dataSource.Substring(0, indexTail + 1);
            //  [messageType, equipmentID, tagID,key,value]

            string strTemp = this.dataSource.Substring(1, this.dataSource.Length - 2);
            string[] strA = strTemp.Split(',');
            if (strA.Length == 5)
            {
                this.messageType = strA[0];
                this.equipmentID = strA[1];
                this.tagID = strA[2];
                this.key = strA[3];
                this.value = strA[4];
            }

            ////The first four bytes are for the Command
            //this.cmdCommand = (Command)BitConverter.ToInt32(data, 0);

            ////The next four store the length of the name
            //int nameLen = BitConverter.ToInt32(data, 4);

            ////The next four store the length of the message
            //int msgLen = BitConverter.ToInt32(data, 8);

            ////This check makes sure that strName has been passed in the array of bytes
            //if (nameLen > 0)
            //    this.strName = Encoding.UTF8.GetString(data, 12, nameLen);
            //else
            //    this.strName = null;

            ////This checks for a null message field
            //if (msgLen > 0)
            //    this.strMessage = Encoding.UTF8.GetString(data, 12 + nameLen, msgLen);
            //else
            //    this.strMessage = null;
        }
        public string toString()
        {
            if (this.dataSource == null)
            {
                this.dataSource = "[";
                if (this.messageType == null || this.messageType.Length <= 0)
                {
                    this.messageType = "9999";
                }
                this.dataSource += this.messageType + ",";
                if (this.equipmentID == null || this.equipmentID.Length <= 0)
                {
                    this.equipmentID = "9999";
                }
                this.dataSource += this.equipmentID + ",";
                if (this.tagID == null || this.tagID.Length <= 0)
                {
                    this.tagID = "9999";
                }
                this.dataSource += this.tagID + ",";
                if (this.key == null || this.key.Length <= 0)
                {
                    this.key = "9999";
                }
                this.dataSource += this.key + ",";
                if (this.value == null || this.value.Length <= 0)
                {
                    this.value = "9999";
                }
                this.dataSource += this.value + "]";
            }
            return this.dataSource;
        }
        //Converts the Data structure into an array of bytes
        public byte[] ToByte()
        {
            List<byte> result = new List<byte>();


            byte[] bytes = Encoding.UTF8.GetBytes(this.toString());
            result.AddRange(bytes);

            ////First four are for the Command
            //result.AddRange(BitConverter.GetBytes((int)cmdCommand));

            ////byte[] btName = BitConverter.GetBytes(strName.Length);
            ////Add the length of the name
            //if (strName != null)
            //{
            //    byte[] btName = Encoding.UTF8.GetBytes(strName);
            //    result.AddRange(BitConverter.GetBytes(btName.Length));
            //}
            //else
            //{
            //    result.AddRange(BitConverter.GetBytes(0));
            //}
            ////Length of the message
            //if (strMessage != null)
            //{
            //    byte[] btMessage = Encoding.UTF8.GetBytes(strMessage);
            //    result.AddRange(BitConverter.GetBytes(btMessage.Length));
            //}
            //else
            //{
            //    result.AddRange(BitConverter.GetBytes(0));
            //}

            ////Add the name
            //if (strName != null)
            //    result.AddRange(Encoding.UTF8.GetBytes(strName));

            ////And, lastly we add the message text to our array of bytes
            //if (strMessage != null)
            //    result.AddRange(Encoding.UTF8.GetBytes(strMessage));

            return result.ToArray();
        }
        public string messageType;
        public string equipmentID;
        public string tagID;
        public string key;
        public string value;
        public string dataSource;


        public string strName;      //Name by which the client logs into the room
        public string strMessage;   //Message text
        public Command cmdCommand;  //Command type (login, logout, send message, etcetera)
    }
}
