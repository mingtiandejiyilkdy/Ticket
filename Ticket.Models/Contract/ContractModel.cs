using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PWMIS.DataMap.Entity;
using Ticket.Interface;

namespace Ticket.Models.Contract
{
    /// <summary>
    /// 合同
    /// </summary>
    public class ContractModel : EntityBase,ILongID
    {
        public ContractModel()
        {
            TableName = "P_Contract";
            IdentityName = "ContractId";
            PrimaryKeys.Add("ContractId");//主键
        }
        /// <summary>
        /// 合同ID
        /// </summary>
        public long ID
        {
            get { return getProperty<long>("ContractId"); }
            set { setProperty("ContractId", value); }
        }
        /// <summary>
        /// 客户Id
        /// </summary>
        public long CustomId
        {
            get { return getProperty<long>("CustomId"); }
            set { setProperty("CustomId", value); }
        }
        /// <summary>
        /// 合同编号
        /// </summary>
        public string ContractNo
        {
            get { return getProperty<string>("ContractNo"); }
            set { setProperty("ContractNo", value); }
        } 
        /// <summary>
        /// 款项1（应收）金额
        /// </summary>
        public Decimal MoneyTypeOneAmount
        {
            get { return getProperty<Decimal>("MoneyTypeOneAmount"); }
            set { setProperty("MoneyTypeOneAmount", value); }
        }
        /// <summary>
        /// 款项2（赠送）金额
        /// </summary>
        public Decimal MoneyTypeTwoAmount
        {
            get { return getProperty<Decimal>("MoneyTypeTwoAmount"); }
            set { setProperty("MoneyTypeTwoAmount", value); }
        }
        /// <summary>
        /// 款项3（置换）金额
        /// </summary>
        public Decimal MoneyTypeThreeAmount
        {
            get { return getProperty<Decimal>("MoneyTypeThreeAmount"); }
            set { setProperty("MoneyTypeThreeAmount", value); }
        }
        /// <summary>
        /// 合同金额
        /// </summary>
        public Decimal ContractAmount
        {
            get { return getProperty<Decimal>("ContractAmount"); }
            set { setProperty("ContractAmount", value); }
        }

        /// <summary>
        /// 已使用金额
        /// </summary>
        public decimal Deductions
        {
            get { return getProperty<decimal>("Deductions"); }
            set { setProperty("Deductions", value); }
        }

        /// <summary>
        /// 可用余额
        /// </summary>
        public decimal Balance
        {
            get { return getProperty<decimal>("Balance"); }
            set { setProperty("Balance", value); }
        }
        /// <summary>
        /// 余额校验Salt
        /// </summary>
        public string Salt
        {
            get { return getProperty<string>("Salt"); }
            set { setProperty("Salt", value, 50); }
        }
        /// <summary>
        /// 余额校验码
        /// </summary>
        public string BalanceKey
        {
            get { return getProperty<string>("BalanceKey"); }
            set { setProperty("BalanceKey", value, 50); }
        }

        public string Attachment
        {
            get { return getProperty<string>("Attachment"); }
            set { setProperty("Attachment", value, 200); }
        }
        /// <summary>
        /// 最后付款时间
        /// </summary>
        public DateTime Deadline
        {
            get { return getProperty<DateTime>("Deadline"); }
            set { setProperty("Deadline", value); }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            get { return getProperty<string>("Remark"); }
            set { setProperty("Remark", value); }
        } 
        /// <summary>
        /// 排序
        /// </summary>
        public int Sort
        {
            get { return getProperty<int>("Sort"); }
            set { setProperty("Sort", value); }
        }
        /// <summary>
        /// 状态
        /// </summary>
        public int Status
        {
            get { return getProperty<int>("Status"); }
            set { setProperty("Status", value); }
        }
        public long CreateId
        {
            get { return getProperty<long>("CreateId"); }
            set { setProperty("CreateId", value); }
        }
        public string CreateUser
        {
            get { return getProperty<string>("CreateUser"); }
            set { setProperty("CreateUser", value); }
        }
        public string CreateIP
        {
            get { return getProperty<string>("CreateIP"); }
            set { setProperty("CreateIP", value, 20); }
        }
        public DateTime CreateTime
        {
            get { return getProperty<DateTime>("CreateTime"); }
            set { setProperty("CreateTime", value); }
        }

        public long UpdateId
        {
            get { return getProperty<long>("UpdateId"); }
            set { setProperty("UpdateId", value); }
        }
        public string UpdateUser
        {
            get { return getProperty<string>("UpdateUser"); }
            set { setProperty("UpdateUser", value); }
        }
        public string UpdateIP
        {
            get { return getProperty<string>("UpdateIP"); }
            set { setProperty("UpdateIP", value, 20); }
        }
        public DateTime UpdateTime
        {
            get { return getProperty<DateTime>("UpdateTime"); }
            set { setProperty("UpdateTime", value); }
        }


        /// <summary>
        /// 租户Id
        /// </summary>
        public long TenantId
        {
            get { return getProperty<long>("TenantId"); }
            set { setProperty("TenantId", value); }
        }    
    }
}
