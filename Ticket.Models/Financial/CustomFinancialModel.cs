using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PWMIS.DataMap.Entity;
using Ticket.Interface;

namespace Ticket.Models.Financial
{
    /// <summary>
    /// 客户财务信息
    /// </summary>
    public class CustomFinancialModel : EntityBase, ILongID
    {
        public CustomFinancialModel()
        {
            TableName = "P_Custom_Financial";
            IdentityName = "CustomFinancialId";
            PrimaryKeys.Add("CustomFinancialId");//主键
        }
        /// <summary>
        /// 客户财务Id
        /// </summary>
        public long ID
        {
            get { return getProperty<long>("CustomFinancialId"); }
            set { setProperty("CustomFinancialId", value); }
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
        /// 款项1（应收）累计金额
        /// </summary>
        public Decimal MoneyTypeOneTotalAmount
        {
            get { return getProperty<Decimal>("MoneyTypeOneTotalAmount"); }
            set { setProperty("MoneyTypeOneTotalAmount", value); }
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
        /// 款项2（赠送）累计金额
        /// </summary>
        public Decimal MoneyTypeTwoTotalAmount
        {
            get { return getProperty<Decimal>("MoneyTypeTwoTotalAmount"); }
            set { setProperty("MoneyTypeTwoTotalAmount", value); }
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
        public Decimal MoneyTypeThreeTotalAmount
        {
            get { return getProperty<Decimal>("MoneyTypeThreeTotalAmount"); }
            set { setProperty("MoneyTypeThreeTotalAmount", value); }
        }
        /// <summary>
        /// 款项3（置换）金额
        /// </summary>
        public Decimal MoneyTypeThreeAmount
        {
            get { return getProperty<Decimal>("MoneyTypeThreeAmount"); }
            set { setProperty("MoneyTypeThreeAmount", value); }
        }

        public string Remark
        {
            get { return getProperty<string>("Remark"); }
            set { setProperty("Remark", value); }
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
