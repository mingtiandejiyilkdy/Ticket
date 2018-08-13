using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PWMIS.DataMap.Entity;
using Ticket.Interface;

namespace Ticket.Models
{
    public class AdminAccount : EntityBase, ILongID
    {
        public AdminAccount()
        {
            TableName = "P_Admin_Account";
            IdentityName = "AccountId";
            PrimaryKeys.Add("AccountId");//主键
        }


        public long ID
        {
            get { return getProperty<long>("AccountId"); }
            set { setProperty("AccountId", value); }
        }

        public string AccountName
        {
            get { return getProperty<string>("AccountName"); }
            set { setProperty("AccountName", value, 20); }
        }

        public string AccountPwd
        {
            get { return getProperty<string>("AccountPwd"); }
            set { setProperty("AccountPwd", value, 50); }
        }
        public string Salt
        {
            get { return getProperty<string>("Salt"); }
            set { setProperty("Salt", value, 50); }
        }

        public string TrueName
        {
            get { return getProperty<string>("TrueName"); }
            set { setProperty("TrueName", value, 20); }
        }

        public string Mobile
        {
            get { return getProperty<string>("Mobile"); }
            set { setProperty("Mobile", value, 11); }
        }

        public string Email
        {
            get { return getProperty<string>("Email"); }
            set { setProperty("Email", value, 50); }
        }
        public int Status
        {
            get { return getProperty<int>("Status"); }
            set { setProperty("Status", value); }
        }

        public DateTime LoginTime
        {
            get { return getProperty<DateTime>("LoginTime"); }
            set { setProperty("LoginTime", value); }
        }

        public DateTime LastLoginTime
        {
            get { return getProperty<DateTime>("LastLoginTime"); }
            set { setProperty("LastLoginTime", value); }
        }
        public int LoginCount
        {
            get { return getProperty<int>("LoginCount"); }
            set { setProperty("LoginCount", value); }
        }
        public DateTime CreateTIme
        {
            get { return getProperty<DateTime>("CreateTIme"); }
            set { setProperty("CreateTIme", value); }
        }

        /// <summary>
        /// 租户
        /// </summary>
        public long TenantId
        {
            get { return getProperty<long>("TenantId"); }
            set { setProperty("TenantId", value); }
        }
    }
}

