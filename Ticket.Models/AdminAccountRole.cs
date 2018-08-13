using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PWMIS.DataMap.Entity;

namespace Ticket.Models
{
    public class AdminAccountRole : EntityBase
    {
        public AdminAccountRole()
        {
            TableName = "P_Admin_Account_Role";
            IdentityName = "AccountRoleId";
            PrimaryKeys.Add("AccountRoleId");//主键
        }

        public long ID
        {
            get { return getProperty<long>("AccountRoleId"); }
            set { setProperty("AccountRoleId", value); }
        }

        public long AccountID
        {
            get { return getProperty<long>("AccountID"); }
            set { setProperty("AccountID", value); }
        }

        public long RoleID
        {
            get { return getProperty<long>("RoleID"); }
            set { setProperty("RoleID", value); }
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
