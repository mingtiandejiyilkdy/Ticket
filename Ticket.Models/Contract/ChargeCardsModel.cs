using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PWMIS.DataMap.Entity;
using Ticket.Interface;

namespace Ticket.Models.Contract
{
    /// <summary>
    /// 充卡记录
    /// </summary>
    public class ChargeCardsModel : EntityBase, ILongID
    {
        public ChargeCardsModel()
        {
            TableName = "P_Custom_ChargeCards";
            IdentityName = "ChargeCardId";
            PrimaryKeys.Add("ChargeCardId");//主键
        }
        /// <summary>
        /// 充卡Id
        /// </summary>
        public long ID
        {
            get { return getProperty<long>("ChargeCardId"); }
            set { setProperty("ChargeCardId", value); }
        }
        /// <summary>
        /// 充卡编号
        /// </summary>
        public string ChargeCardNo
        {
            get { return getProperty<string>("ChargeCardNo"); }
            set { setProperty("ChargeCardNo", value, 50); }
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
        /// 票据类型
        /// </summary>
        public long TicketTypeID
        {
            get { return getProperty<long>("TicketTypeID"); }
            set { setProperty("TicketTypeID", value); }
        }
        /// <summary>
        /// 款项类型
        /// </summary>
        public int MoneyType
        {
            get { return getProperty<int>("MoneyType"); }
            set { setProperty("MoneyType", value); }
        }
        /// <summary>
        /// 当前充值张数
        /// </summary>
        public decimal CurrentCount
        {
            get { return getProperty<decimal>("CurrentCount"); }
            set { setProperty("CurrentCount", value); }
        }
        /// <summary>
        /// 充值面额
        /// </summary>
        public decimal FaceAmount
        {
            get { return getProperty<decimal>("FaceAmount"); }
            set { setProperty("FaceAmount", value); }
        }
        /// <summary>
        /// 当前充值金额
        /// </summary>
        public decimal CurrentAmount
        {
            get { return getProperty<decimal>("CurrentAmount"); }
            set { setProperty("CurrentAmount", value); }
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
        /// 票据批次
        /// </summary>
        public long TicketBatchId
        {
            get { return getProperty<long>("TicketBatchId"); }
            set { setProperty("TicketBatchId", value); }
        }
        /// <summary>
        /// 卡号开始
        /// </summary>
        public long TicketStart
        {
            get { return getProperty<long>("TicketStart"); }
            set { setProperty("TicketStart", value); }
        }
        /// <summary>
        /// 卡号结束
        /// </summary>
        public long TicketEnd
        {
            get { return getProperty<long>("TicketEnd"); }
            set { setProperty("TicketEnd", value); }
        }
        /// <summary>
        /// 消费级别
        /// </summary>
        public int Consumptionlevel
        {
            get { return getProperty<int>("Consumptionlevel"); }
            set { setProperty("Consumptionlevel", value); }
        }
        /// <summary>
        /// 是否通卡
        /// </summary>
        public int IsCommonCard
        {
            get { return getProperty<int>("IsCommonCard"); }
            set { setProperty("IsCommonCard", value); }
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
        public DateTime? UpdateTime
        {
            get { return getProperty<DateTime>("UpdateTime"); }
            set { setProperty("UpdateTime", value); }
        }

        /// <summary>
        /// 商家Id
        /// </summary>
        public long TenantId
        {
            get { return getProperty<long>("TenantId"); }
            set { setProperty("TenantId", value); }
        }
    }
}

