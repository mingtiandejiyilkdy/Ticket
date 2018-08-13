using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PWMIS.DataMap.Entity;
using Ticket.Interface;

namespace Ticket.Models.Custom
{
    /// <summary>
    /// 客户信息
    /// </summary>
    public class CustomModel : EntityBase, ILongID
    {
        public CustomModel()
        {
            TableName = "P_Custom";
            IdentityName = "CustomId";
            PrimaryKeys.Add("CustomId");//主键
        }
        /// <summary>
        /// 客户ID
        /// </summary>
        public long ID
        {
            get { return getProperty<long>("CustomId"); }
            set { setProperty("CustomId", value); }
        }
        /// <summary>
        /// 客户类型
        /// </summary>
        public long CustomTypeId
        {
            get { return getProperty<long>("CustomTypeId"); }
            set { setProperty("CustomTypeId", value); }
        }
        /// <summary>
        /// 客户名称
        /// </summary>
        public string CustomName
        {
            get { return getProperty<string>("CustomName"); }
            set { setProperty("CustomName", value, 50); }
        }
        /// <summary>
        /// 联系人
        /// </summary>
        public string LinkName
        {
            get { return getProperty<string>("LinkName"); }
            set { setProperty("LinkName", value, 50); }
        }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string LinkPhone
        {
            get { return getProperty<string>("LinkPhone"); }
            set { setProperty("LinkPhone", value, 20); }
        }
        /// <summary>
        /// 联系手机
        /// </summary>
        public string LinkMobile
        {
            get { return getProperty<string>("LinkMobile"); }
            set { setProperty("LinkMobile", value, 15); }
        }
        /// <summary>
        /// 客户区域
        /// </summary>
        public string CustomArea
        {
            get { return getProperty<string>("CustomArea"); }
            set { setProperty("CustomArea", value, 15); }
        }
        /// <summary>
        /// 客户地址
        /// </summary>
        public string CustomAddress
        {
            get { return getProperty<string>("CustomAddress"); }
            set { setProperty("CustomAddress", value, 15); }
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
        /// 租户Id
        /// </summary>
        public long TenantId
        {
            get { return getProperty<long>("TenantId"); }
            set { setProperty("TenantId", value); }
        }    
    }
}
