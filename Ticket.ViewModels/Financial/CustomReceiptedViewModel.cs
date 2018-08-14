using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ticket.Common;
using Ticket.Enum;

namespace Ticket.ViewModels.Financial
{
    public class CustomReceiptedViewModel : BaseViewModel
    {
        public long ID { get; set; }
        /// <summary>
        /// 客户Id
        /// </summary>
        public long CustomId { get; set; }
        /// <summary>
        /// 客户名称
        /// </summary>
        public long CustomName { get; set; } 
        /// <summary>
        /// 收款编号
        /// </summary>
        public string CustomReceiptedNo { get; set; } 
        public long CustomAccReceiptId { get; set; } 
        /// <summary>
        ///收款账号
        /// </summary>
        public long BankAccountId { get; set; }
        /// <summary>
        /// 本次收款
        /// </summary>
        public decimal CurrentAmount { get; set; }
        /// <summary>
        /// 银行流水号
        /// </summary>
        public string BankSerialNumber { get; set; } 
        public DateTime DateOfEntry { get; set; }  
        public int Status { get; set; } 

        public string StatusStr
        {
            get { return Util.getStatus(Status, typeof(BaseEnum.ProtocolTypeEnum)); }
        }

        public string DateOfEntryStr
        {
            get
            {
                return DateOfEntry.ToString("yyyy-MM-dd HH:mm:ss");
            }
        }  
    }
}
