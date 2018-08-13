using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ticket.Enum
{
    /// <summary>
    /// 数据标志 -1=已删除；0=禁用；1=启用
    /// </summary>
    public enum StatusEnum
    {
        已删除 = -1,
        禁用 = 0,
        启用 = 1,
    }
}
