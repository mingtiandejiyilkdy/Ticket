using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PWMIS.DataMap.Entity;
using Ticket.Interface;

namespace Ticket.Models.Financial
{
    /// <summary>
    /// 客户财务明细
    /// </summary>
    public class CustomFinancialDetailModel : EntityBase, ILongID
    {
        public CustomFinancialDetailModel()
        {
            TableName = "P_Custom_FinancialDetail";
            IdentityName = "FinancialDetailId";
            PrimaryKeys.Add("FinancialDetailId");//主键
        }
        /// <summary>
        /// 客户财务明细Id
        /// </summary>
        public long ID
        {
            get { return getProperty<long>("FinancialDetailId"); }
            set { setProperty("FinancialDetailId", value); }
        }
        /// <summary>
        /// 客户财务信息Id
        /// </summary>
        public long CustomFinancialId
        {
            get { return getProperty<long>("FinancialId"); }
            set { setProperty("FinancialId", value); }
        }
        
        /// <summary>
        /// 金额类型
        /// </summary>
        public int MoneyType
        {
            get { return getProperty<int>("MoneyType"); }
            set { setProperty("MoneyType", value); }
        }

        /// <summary>
        /// 财务操作类型
        /// </summary>
        public int FinanciaOpeType
        {
            get { return getProperty<int>("FinanciaOpeType"); }
            set { setProperty("FinanciaOpeType", value); }
        }

        /// <summary>
        /// 当前操作金额
        /// </summary>
        public decimal CurrentAmount
        {
            get { return getProperty<decimal>("Amount"); }
            set { setProperty("Amount", value); }
        }

        /// <summary>
        /// 操作后余额
        /// </summary>
        public decimal Balance
        {
            get { return getProperty<decimal>("Balance"); }
            set { setProperty("Balance", value); }
        }

        /// <summary>
        /// 余额校验Key
        /// </summary>
        public string BalanceKey
        {
            get { return getProperty<string>("BalanceKey"); }
            set { setProperty("BalanceKey", value, 50); }
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
