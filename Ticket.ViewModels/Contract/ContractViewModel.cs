using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ticket.Common;
using Ticket.Enum;

namespace Ticket.ViewModels.Contract
{ 
    /// <summary>
    /// 合同
    /// </summary>
    public class ContractViewModel : BaseViewModel
    { 
        /// <summary>
        /// 合同ID
        /// </summary>
        public long ID { get; set; }
        /// <summary>
        /// 客户Id
        /// </summary>
        public long CustomId { get; set; }
        /// <summary>
        /// 客户名称
        /// </summary>
        public string CustomName { get; set; }
        /// <summary>
        /// 客户类型名称
        /// </summary>
        public string CustomTypeName { get; set; }
        /// <summary>
        /// 合同编号
        /// </summary>
        public string ContractNo{get;set;}
        /// <summary>
        /// 联系人
        /// </summary>
        public string LinkName{get;set;}
        /// <summary>
        /// 联系电话
        /// </summary>
        public string LinkPhone{get;set;}
        /// <summary>
        /// 联系手机
        /// </summary>
        public string LinkMobile{get;set;}
        /// <summary>
        /// 款项1（应收）金额
        /// </summary>
        public Decimal MoneyTypeOneAmount{get;set;}
        /// <summary>
        /// 款项2（赠送）金额
        /// </summary>
        public Decimal MoneyTypeTwoAmount{get;set;}
        /// <summary>
        /// 款项3（置换）金额
        /// </summary>
        public Decimal MoneyTypeThreeAmount{get;set;}
        /// <summary>
        /// 合同金额
        /// </summary>
        public Decimal ContractAmount{get;set;}

        /// <summary>
        /// 已使用金额
        /// </summary>
        public decimal Deductions{get;set;}
        /// <summary>
        /// 可用余额
        /// </summary>
        public decimal Balance{get;set;}
        /// <summary>
        /// 余额校验码
        /// </summary>
        public string BalanceKey{get;set;}

        public string Attachment{get;set;}
        /// <summary>
        /// 最后付款时间
        /// </summary>
        public DateTime Deadline{get;set;}
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark{get;set;} 
        /// <summary>
        /// 状态
        /// </summary>
        public int Status{get;set;}

        /// <summary>
        /// 款项1（应收）金额
        /// </summary>
        public string MoneyTypeOneAmountStr { get { return MoneyTypeOneAmount.ToString("N2"); } }
        /// <summary>
        /// 款项2（赠送）金额
        /// </summary>
        public string MoneyTypeTwoAmountStr { get { return MoneyTypeTwoAmount.ToString("N2"); } }
        /// <summary>
        /// 款项3（置换）金额
        /// </summary>
        public string MoneyTypeThreeAmountStr { get { return MoneyTypeThreeAmount.ToString("N2"); } }
        /// <summary>
        /// 合同金额
        /// </summary>
        public string ContractAmountStr { get { return ContractAmount.ToString("N2"); } }

        /// <summary>
        /// 已使用金额
        /// </summary>
        public string DeductionsStr { get { return Deductions.ToString("N2"); } }
        /// <summary>
        /// 可用余额
        /// </summary>
        public string BalanceStr { get { return Balance.ToString("N2"); } }

        public string StatusStr
        {
            get
            {
                return Util.getStatus(Status, typeof(StatusEnum));
            }
        } 
    }
}
