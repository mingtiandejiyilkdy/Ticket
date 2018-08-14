using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PWMIS.DataMap.Entity;
using Ticket.Interface;

namespace Ticket.Models.Bank
{
    public class BankAccountModel : EntityBase,ILongID
    {
        public BankAccountModel()
        {
            TableName = "P_BankAccount";
            IdentityName = "BankAccountId";
            PrimaryKeys.Add("BankAccountId");//主键
        }

        public long ID
        {
            get { return getProperty<long>("BankAccountId"); }
            set { setProperty("BankAccountId", value); }
        }

        public string BankAccountName
        {
            get { return getProperty<string>("BankAccountName"); }
            set { setProperty("BankAccountName", value, 50); }
        }

        public long BankId
        {
            get { return getProperty<long>("BankId"); }
            set { setProperty("BankId", value); }
        }

        public string BankAccountCode
        {
            get { return getProperty<string>("BankAccountCode"); }
            set { setProperty("BankAccountCode", value, 50); }
        }


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
        /// 商家Id
        /// </summary>
        public long TenantId
        {
            get { return getProperty<long>("TenantId"); }
            set { setProperty("TenantId", value); }
        }
    }
}