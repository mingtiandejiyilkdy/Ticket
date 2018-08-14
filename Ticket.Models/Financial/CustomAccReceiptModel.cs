using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PWMIS.DataMap.Entity;
using Ticket.Interface;

namespace Ticket.Models.Financial
{
    /// <summary>
    /// 客户应收款
    /// </summary>
    public class CustomAccReceiptModel : EntityBase,ILongID
    {
        /// <summary>
        /// 客户应收款
        /// </summary>
        public CustomAccReceiptModel()
        {
            TableName = "P_Custom_AccReceipt";
            IdentityName = "AccReceiptId";
            PrimaryKeys.Add("AccReceiptId");//主键
        }
        /// <summary>
        /// 应收款Id
        /// </summary>
        public long ID
        {
            get { return getProperty<long>("AccReceiptId"); }
            set { setProperty("AccReceiptId", value); }
        }
        /// <summary>
        /// 充值编号
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
        /// 应收金额
        /// </summary>
        public decimal CurrentAmount
        {
            get { return getProperty<decimal>("CurrentAmount"); }
            set { setProperty("CurrentAmount", value); }
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
        public DateTime? UpdateTime
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
