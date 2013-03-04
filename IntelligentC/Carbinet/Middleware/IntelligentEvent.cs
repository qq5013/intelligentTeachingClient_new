using System;
using System.Collections.Generic;
using System.Text;

namespace intelligentMiddleWare
{
    public class IntelligentEventUnit
    {
        public const string new_epc = "new_epc";
        public const string repeat_epc = "repeat_epc";
        public const string epc_on_another_device = "epc_on_another_device";
        public const string change_answer = "change_answer";
    }
    public class IntelligentEvent
    {
        #region event name defination
        public static string event_empty = "event_empty";

        //考勤事件
        public  const string check_new_epc = "check_new_epc";//第一次考勤
        public const string check_repeat_epc = "check_repeat_epc";//重复考勤
        public const string check_repeat_epc_on_another_device = "check_repeat_epc_on_another_device";//在不同的终端重复考勤

        //即时互动事件
        public const string realtime_question_new_epc = "realtime_question_new_epc";//第一次发送
        public const string realtime_question_repeat_epc = "realtime_question_repeat_epc";//重复发送
        public const string realtime_question_repeat_epc_on_another_device = "realtime_question_repeat_epc_on_another_device";
        //public const string realtime_question_repeat_epc_on_another_device = "realtime_question_repeat_epc_on_another_device";

        //课堂测试事件
        public const string class_question_new_answer = "class_question_new_answer";//第一次发送答案
        public const string class_question_repeat_answer = "class_question_repeat_answer";//重复发送答案
        public const string class_question_repeat_answer_on_another_device = "class_question_repeat_answeron_another_device";//更换设备重复发送答案
        public const string class_question_change_answer = "class_question_change_answer";//更改答案
        public const string class_question_change_answer_on_another_device = "class_question_change_answer_on_another_device";
        #endregion
        public string name = string.Empty;
        public string time_stamp = string.Empty;
        public string localDeviceID = string.Empty;
        public string epcID = string.Empty;
        public string questionID = string.Empty;
        public string questionValue = string.Empty;
        public string remoteDeviceID = string.Empty;
        public List<string> event_unit_list = new List<string>();//所有时间基本单位的集合

        public static string get_time_stamp()
        {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        public string toString()
        {
            return string.Format("name = {0},remoteDeviceID = {1} time = {2}",
                                 this.name, this.remoteDeviceID, this.time_stamp);
        }
    }
}
