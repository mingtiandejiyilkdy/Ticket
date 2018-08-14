using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ticket.Common;
using Ticket.Enum; 

namespace Ticket.ViewModels.Financial
{
    /// <summary>
    /// 客户应收款
    /// </summary>
    public class CustomAccReceiptViewModel : BaseViewModel
    { 
        /// <summary>
        /// 应收款Id
        /// </summary>
        public long ID { get; set; }
        /// <summary>
        /// 充值编号
        /// </summary>
        public string ChargeCardNo { get; set; }
        /// <summary>
        /// 客户Id
        /// </summary>
        public long CustomId { get; set; }
        /// <summary>
        /// 客户名称
        /// </summary>
        public string CustomName { get; set; }
        
        /// <summary>
        /// 应收金额
        /// </summary>
        public decimal CurrentAmount { get; set; } 
        public int Status { get; set; }
        public string StatusStr
        {
            get { return Util.getStatus(Status, typeof(BaseEnum.ProtocolTypeEnum)); }
        }       

    }
}
