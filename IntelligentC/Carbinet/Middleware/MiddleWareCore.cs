using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Threading;
using System.Drawing;

namespace intelligentMiddleWare
{
    //public enum miniButton
    //{
    //    显示座位, 考勤, 实时互动, 课堂测验, 退出
    //}
    public enum MiddleWareMode
    {
        考勤,
        实时互动,
        课堂测验,
        设备绑定,
        学生卡绑定,
        无
    }
    public interface I_event_handler
    {
        void prepare_handler();
    }
    public interface I_mini_form_show_notify
    {
        void refreshPie(List<string> captionList, List<int> valueList, List<Color> colorList, bool bShowLabel);
        void show_tip(string tip);
    }
    public interface I_event_notify
    {
        void receive_a_new_event();
    }
    public class MiddleWareCore
    {
        public static I_event_notify event_receiver = null;
        public static MiddleWareMode mode = MiddleWareMode.考勤;

        //public static bool isInitialized = false;
        static List<ProtocolHelper> data_list = new List<ProtocolHelper>();
        static List<IntelligentEvent> event_list = new List<IntelligentEvent>();
        public static ManualResetEvent MRE_data_block = new ManualResetEvent(true);
        public static ManualResetEvent MRE_event_block = new ManualResetEvent(true);

        public static void Set_new_data(ProtocolHelper helper)
        {
            //添加数据
            MRE_data_block.WaitOne();
            MRE_data_block.Reset();
            event_producer(helper);
            data_list.Add(helper);
            MRE_data_block.Set();
        }
        static void event_producer(ProtocolHelper helper)
        {
            bool bNotify_receiver = false;
            //产生事件
            IntelligentEvent evt = new IntelligentEvent();
            //evt.epcID = helper.epcID;
            ProtocolHelper_to_IntelligentEvent(ref helper, ref evt);
            evt.time_stamp = IntelligentEvent.get_time_stamp();
            int iCount = 0;
            switch (mode)
            {
                case MiddleWareMode.考勤:
                    // 检查该学生是第一次发送考勤信息还是重复发送
                    if (helper.epcID == null || helper.epcID.Length <= 0)
                    {
                        evt.name = IntelligentEvent.event_empty;
                    }
                    else
                    {
                        iCount = 0;
                        evt.event_unit_list.Add(IntelligentEventUnit.new_epc);
                        //evt.name = IntelligentEvent.check_new_epc;
                        for (int i = data_list.Count - 1; i >= 0; i--)
                        {
                            ProtocolHelper p = data_list[i];
                            if (p.epcID == helper.epcID)//
                            {
                                iCount++;
                                if (iCount >= 1)
                                {
                                    evt.event_unit_list.Remove(IntelligentEventUnit.new_epc);
                                    evt.event_unit_list.Add(IntelligentEventUnit.repeat_epc);
                                    //可能同一个卡在不同的终端重复考勤
                                    if (p.remoteDeviceID != helper.remoteDeviceID)
                                    {
                                        evt.event_unit_list.Add(IntelligentEventUnit.epc_on_another_device);
                                    }
                                    break;
                                }

                            }
                        }
                        bNotify_receiver = true;
                    }

                    break;
                case MiddleWareMode.实时互动:
                    if (helper.epcID == null || helper.epcID.Length <= 0)
                    {
                        evt.name = IntelligentEvent.event_empty;
                    }
                    else
                    {
                        evt.event_unit_list.Add(IntelligentEventUnit.new_epc);
                        iCount = 0;
                        for (int i = data_list.Count - 1; i >= 0; i--)
                        {
                            ProtocolHelper p = data_list[i];
                            if (p.epcID == helper.epcID)//找到了同一个人
                            {
                                iCount++;
                                if (iCount >= 1)
                                {
                                    evt.event_unit_list.Remove(IntelligentEventUnit.new_epc);
                                    evt.event_unit_list.Add(IntelligentEventUnit.repeat_epc);
                                    //可能同一个卡在不同的终端重复
                                    if (p.remoteDeviceID != helper.remoteDeviceID)
                                    {
                                        evt.event_unit_list.Add(IntelligentEventUnit.epc_on_another_device);
                                    }
                                    if (helper.questionValue != string.Empty && helper.questionValue.Length > 0 &&
                                        helper.questionValue != p.questionValue)
                                    {
                                        evt.event_unit_list.Add(IntelligentEventUnit.change_answer);
                                    }
                                    break;
                                }

                            }
                        }
                        bNotify_receiver = true;

                    }
                    break;
                case MiddleWareMode.课堂测验:
                    if (helper.epcID == null || helper.epcID.Length <= 0)
                    {
                        evt.name = IntelligentEvent.event_empty;
                    }
                    else
                    {
                        evt.event_unit_list.Add(IntelligentEventUnit.new_epc);
                        //evt.name = IntelligentEvent.class_question_new_answer;
                        iCount = 0;
                        for (int i = data_list.Count - 1; i >= 0; i--)
                        {
                            ProtocolHelper p = data_list[i];
                            if (p.epcID == helper.epcID)
                            {
                                iCount++;
                                if (iCount >= 1)
                                {
                                    evt.event_unit_list.Remove(IntelligentEventUnit.new_epc);
                                    //设备id和问题答案都没有改变
                                    if (p.questionValue == helper.questionValue && p.remoteDeviceID == helper.remoteDeviceID)
                                    {
                                        //evt.name = IntelligentEvent.class_question_repeat_answer;
                                        evt.event_unit_list.Add(IntelligentEventUnit.repeat_epc);
                                        break;
                                    }
                                    //设备id没有改变，问题答案改变
                                    if (p.questionValue != helper.questionValue && p.remoteDeviceID == helper.remoteDeviceID)
                                    {
                                        evt.event_unit_list.Add(IntelligentEventUnit.change_answer);
                                        evt.event_unit_list.Add(IntelligentEventUnit.repeat_epc);
                                        //evt.name = IntelligentEvent.class_question_new_answer;
                                        break;
                                    }
                                    //设备id改变，问题答案不变
                                    if (p.questionValue == helper.questionValue && p.remoteDeviceID != helper.remoteDeviceID)
                                    {
                                        //evt.name = IntelligentEvent.class_question_repeat_answer_on_another_device;
                                        evt.event_unit_list.Add(IntelligentEventUnit.epc_on_another_device);
                                        evt.event_unit_list.Add(IntelligentEventUnit.repeat_epc);
                                        break;
                                    }
                                    //设备id和问题答案都改变
                                    if (p.questionValue != helper.questionValue && p.remoteDeviceID != helper.remoteDeviceID)
                                    {
                                        //evt.name = IntelligentEvent.class_question_change_answer_on_another_device;
                                        evt.event_unit_list.Add(IntelligentEventUnit.change_answer);
                                        evt.event_unit_list.Add(IntelligentEventUnit.repeat_epc);
                                        evt.event_unit_list.Add(IntelligentEventUnit.epc_on_another_device);
                                        break;
                                    }
                                    break;
                                }
                            }
                        }
                        bNotify_receiver = true;
                    }
                    break;
                case MiddleWareMode.设备绑定:
                    if (helper.remoteDeviceID == null || helper.remoteDeviceID.Length <= 0)
                    {
                        evt.name = IntelligentEvent.event_empty;
                    }
                    else
                    {
                        evt.event_unit_list.Add(IntelligentEventUnit.new_epc);
                    }
                    bNotify_receiver = true;
                    break;
                case MiddleWareMode.学生卡绑定:
                    if (helper.epcID == null || helper.epcID.Length <= 0)
                    {
                        evt.name = IntelligentEvent.event_empty;
                    }
                    else
                    {
                        evt.event_unit_list.Add(IntelligentEventUnit.new_epc);
                    }
                    bNotify_receiver = true;
                    break;
            }

            MRE_event_block.WaitOne();
            MRE_event_block.Reset();
            event_list.Add(evt);
            MRE_event_block.Set();

            if (event_receiver != null && bNotify_receiver == true && mode != MiddleWareMode.无)
            {
                event_receiver.receive_a_new_event();
            }
        }
        static void ProtocolHelper_to_IntelligentEvent(ref ProtocolHelper helper, ref IntelligentEvent evt)
        {
            evt.epcID = helper.epcID;
            evt.localDeviceID = helper.localDeviceID;
            evt.remoteDeviceID = helper.remoteDeviceID;
            evt.questionID = helper.questionID;
            evt.questionValue = helper.questionValue;

        }
        public static IntelligentEvent get_a_event()
        {
            IntelligentEvent evt = null;
            MRE_event_block.WaitOne();
            MRE_event_block.Reset();
            if (event_list.Count > 0)
            {
                evt = event_list[event_list.Count - 1];
                event_list.Remove(evt);
            }
            MRE_event_block.Set();
            return evt;
        }
        public static void set_mode(MiddleWareMode _mode, I_event_notify receiver)
        {
            set_mode(_mode);
            event_receiver = receiver;
        }
        public static void set_mode(MiddleWareMode _mode)
        {
            mode = _mode;
            data_list.Clear();
            event_list.Clear();
        }

    }
}
