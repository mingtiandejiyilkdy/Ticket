using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PWMIS.DataMap.Entity;
using Ticket.Enum;
using Ticket.Interface;

namespace Ticket.Models.Ticket
{
    /// <summary>
    /// 票据信息
    /// </summary>
    public class TicketInfo : EntityBase, ILongID
    {
        public TicketInfo()
        {
            TableName = "P_Ticket_Info";
            IdentityName = "TicketId";
            PrimaryKeys.Add("TicketId");//主键
        }
        /// <summary>
        /// 票据ID
        /// </summary>
        public long ID
        {
            get { return getProperty<long>("TicketId"); }
            set { setProperty("TicketId", value); }
        }
        /// <summary>
        /// 票据类型
        /// </summary>
        public long TicketTypeId
        {
            get { return getProperty<long>("TicketTypeId"); }
            set { setProperty("TicketTypeId", value); }
        }
        /// <summary>
        /// 票据编码
        /// </summary>
        public string TicketCode
        {
            get { return getProperty<string>("TicketCode"); }
            set { setProperty("TicketCode", value, 50); }
        }
        /// <summary>
        /// 票据密码（明文）
        /// </summary>
        public string TicketPwd
        {
            get { return getProperty<string>("TicketPwd"); }
            set { setProperty("TicketPwd", value, 50); }
        }
        /// <summary>
        /// 票据密码（MD5）
        /// </summary>
        public string TicketMD5Pwd
        {
            get { return getProperty<string>("TicketMD5Pwd"); }
            set { setProperty("TicketMD5Pwd", value, 50); }
        }
        /// <summary>
        /// 票据加密salt
        /// </summary>
        public string Salt
        {
            get { return getProperty<string>("Salt"); }
            set { setProperty("Salt", value, 50); }
        }
        /// <summary>
        /// 票据消费级别  0=初始化;1=一级；2=二级；3=三级
        /// </summary>
        public int Consumptionlevel
        {
            get { return getProperty<int>("Consumptionlevel"); }
            set { setProperty("Consumptionlevel", value); }
        }
        /// <summary>
        /// 款项类型 0=初始化；1=应收；2=赠送；3=置换
        /// </summary>
        public int MoneyTyp {
            get { return getProperty<int>("MoneyTyp"); }
            set { setProperty("MoneyTyp", value); }
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
        /// 初始化金额
        /// </summary>
        public decimal InitialAmount
        {
            get { return getProperty<decimal>("InitialAmount"); }
            set { setProperty("InitialAmount", value); }
        }
        /// <summary>
        /// 累计金额
        /// </summary>
        public decimal TotalAmount
        {
            get { return getProperty<decimal>("TotalAmount"); }
            set { setProperty("TotalAmount", value); }
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
        /// 余额
        /// </summary>
        public decimal Balance
        {
            get { return getProperty<decimal>("Balance"); }
            set { setProperty("Balance", value); }
        } 
        /// <summary>
        /// 余额
        /// </summary>
        public string BalanceKey
        {
            get { return getProperty<string>("BalanceKey"); }
            set { setProperty("BalanceKey", value,50); }
        } 
        
        /// <summary>
        /// 是否有效
        /// </summary>
        public bool IsExpire
        {
            get { return getProperty<bool>("IsExpire"); }
            set { setProperty("IsExpire", value); }
        }
        /// <summary>
        /// 是否激活
        /// </summary>
        public bool IsActivated
        {
            get { return getProperty<bool>("IsActivated"); }
            set { setProperty("IsActivated", value); }
        }
        /// <summary>
        /// 制卡时间
        /// </summary>
        public DateTime MakeTime
        {
            get { return getProperty<DateTime>("MakeTime"); }
            set { setProperty("MakeTime", value); }
        }
        /// <summary>
        /// 到期时间
        /// </summary>
        public DateTime ExpireDate
        {
            get { return getProperty<DateTime>("ExpireDate"); }
            set { setProperty("ExpireDate", value); }
        }
        /// <summary>
        /// 批次号
        /// </summary>
        public string TicketBatchNo
        {
            get { return getProperty<string>("TicketBatchNo"); }
            set { setProperty("TicketBatchNo", value,50); }
        }
        /// <summary>
        /// 充值人Id
        /// </summary>
        public long GrantId
        {
            get { return getProperty<long>("GrantId"); }
            set { setProperty("GrantId", value); }
        }
        /// <summary>
        /// 充值人
        /// </summary>
        public long GrantName
        {
            get { return getProperty<long>("GrantName"); }
            set { setProperty("GrantName", value); }
        } 
        /// <summary>
        /// 充值时间
        /// </summary>
        public DateTime GrantTime
        {
            get { return getProperty<DateTime>("GrantTime"); }
            set { setProperty("GrantTime", value); }
        }
        /// <summary>
        /// 票据批次生成方式 1=手动创建；2=导入
        /// </summary>
        public int DataFrom
        {
            get { return getProperty<int>("DataFrom"); }
            set { setProperty("DataFrom", value); }
        }

        /// <summary>
        /// 是否旧数据
        /// </summary>
        public bool IsOld
        {
            get { return getProperty<bool>("IsOld"); }
            set { setProperty("IsOld", value); }
        }
        /// <summary>
        /// 排序
        /// </summary>
        public int Sort
        {
            get { return getProperty<int>("Sort"); }
            set { setProperty("Sort", value); }
        }
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
