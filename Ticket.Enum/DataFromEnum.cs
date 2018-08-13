using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ticket.Enum
{
    /// <summary>
    /// 票据批次生成方式 1=手动创建；2=导入
    /// </summary>
    public enum DataFromEnum
    { 
        手动创建 = 1,
        导入 = 2,
    }
}
