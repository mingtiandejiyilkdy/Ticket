using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ticket.Enum
{
    /// <summary>
    /// 凭据状态
    /// </summary>
    public enum CredentialEnum
    {
        未交付 = 0,
        正常 = 1,
        锁定 = 2,
        删除 = 9
    }
}
