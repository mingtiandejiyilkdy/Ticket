using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PWMIS.DataMap.Entity;
using Ticket.Interface;

namespace Ticket.Models
{
    public class AdminMenu : EntityBase, ILongID
    {
        public AdminMenu()
        {
            TableName = "P_Admin_Menu";
            IdentityName = "MenuId";
            PrimaryKeys.Add("MenuId");//主键
        }

        public long ID
        {
            get { return getProperty<long>("MenuId"); }
            set { setProperty("MenuId", value); }
        }
        public long ParentID
        {
            get { return getProperty<long>("ParentID"); }
            set { setProperty("ParentID", value); }
        }
        public string MenuKey
        {
            get { return getProperty<string>("MenuKey"); }
            set { setProperty("MenuKey", value, 50); }
        }
        public string MenuName
        {
            get { return getProperty<string>("MenuName"); }
            set { setProperty("MenuName", value, 20); }
        }
        public string MenuUrl
        {
            get { return getProperty<string>("MenuUrl"); }
            set { setProperty("MenuUrl", value, 100); }
        }
        public int MenuType
        {
            get { return getProperty<int>("MenuType"); }
            set { setProperty("MenuType", value); }
        }
        public string IDPath
        {
            get { return getProperty<string>("IDPath"); }
            set { setProperty("IDPath", value, 500); }
        }
        public string Remark
        {
            get { return getProperty<string>("Remark"); }
            set { setProperty("Remark", value, 200); }
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
