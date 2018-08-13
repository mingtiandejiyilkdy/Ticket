using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PWMIS.DataMap.Entity;
using Ticket.Interface;

namespace Ticket.Models
{
    /// <summary>
    /// 操作
    /// </summary>
    public class AdminAction : EntityBase, ILongID
    {
        public AdminAction()
        {
            TableName = "P_Admin_Action";
            IdentityName = "ActionId";
            PrimaryKeys.Add("ActionId");//主键
        }
        /// <summary>
        /// 操作Id
        /// </summary>
        public long ID
        {
            get { return getProperty<long>("ActionId"); }
            set { setProperty("ActionId", value); }
        }
        /// <summary>
        /// 关联菜单
        /// </summary>
        public long MenuId
        {
            get { return getProperty<long>("MenuId"); }
            set { setProperty("MenuId", value); }
        }
        /// <summary>
        /// 操作关键字
        /// </summary>
        public string ActionKey
        {
            get { return getProperty<string>("ActionKey"); }
            set { setProperty("ActionKey", value, 50); }
        }
        /// <summary>
        /// 操作名称
        /// </summary>
        public string ActionName
        {
            get { return getProperty<string>("ActionName"); }
            set { setProperty("ActionName", value, 20); }
        } 
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            get { return getProperty<string>("Remark"); }
            set { setProperty("Remark", value, 200); }
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
        /// 状态 0=禁用；1=启用；-1=删除；
        /// </summary>
        public int Status
        {
            get { return getProperty<int>("Status"); }
            set { setProperty("Status", value); }
        }
        /// <summary>
        /// 创建人Id
        /// </summary>
        public long CreateId
        {
            get { return getProperty<long>("CreateId"); }
            set { setProperty("CreateId", value); }
        }
        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateUser
        {
            get { return getProperty<string>("CreateUser"); }
            set { setProperty("CreateUser", value); }
        }
        /// <summary>
        /// 创建IP
        /// </summary>
        public string CreateIP
        {
            get { return getProperty<string>("CreateIP"); }
            set { setProperty("CreateIP", value, 20); }
        }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime
        {
            get { return getProperty<DateTime>("CreateTime"); }
            set { setProperty("CreateTime", value); }
        }
        /// <summary>
        /// 更新人Id
        /// </summary>
        public long UpdateId
        {
            get { return getProperty<long>("UpdateId"); }
            set { setProperty("UpdateId", value); }
        }
        /// <summary>
        /// 更新人
        /// </summary>
        public string UpdateUser
        {
            get { return getProperty<string>("UpdateUser"); }
            set { setProperty("UpdateUser", value); }
        }
        /// <summary>
        /// 更新时间
        /// </summary>
        public string UpdateIP
        {
            get { return getProperty<string>("UpdateIP"); }
            set { setProperty("UpdateIP", value, 20); }
        }
        /// <summary>
        /// 更新时间
        /// </summary>
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
