using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haley.Enums
{
    public enum ActionStatus
    {
        [Description("exception")]
        Exception,
        [Description("warning")]
        Warning,
        [Description("passed")]
        Pass,
        [Description("failed")]
        Fail,
        [Description("none")]
        None,
    }
}
