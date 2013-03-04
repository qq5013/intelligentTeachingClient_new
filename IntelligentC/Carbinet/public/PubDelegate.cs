using System;
using System.Collections.Generic;
using System.Text;

namespace Carbinet
{
    public enum InternalCommand
    {
        NextQuestion,
        PreQuestion,
        CloseForm
    }
    public delegate void deleInternalCommandInvoke(InternalCommand cmd,object o);
    public delegate void deleUpdateContorl(string s);
    public delegate void deleUpdateCommContorl(string type, object o, string s);
    public delegate void deleControlInvoke(object o);

}
