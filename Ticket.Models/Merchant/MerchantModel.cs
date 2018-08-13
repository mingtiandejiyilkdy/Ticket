using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PWMIS.DataMap.Entity;

namespace Ticket.Models.Merchant
{
    /// <summary>
    /// 合作商家
    /// </summary>
    public class MerchantModel : EntityBase
    {
        public MerchantModel()
        {
            TableName = "P_Merchant";
            IdentityName = "MerchantId";
            PrimaryKeys.Add("MerchantId");//主键
        }
        /// <summary>
        /// 商家ID
        /// </summary>
        public long ID
        {
            get { return getProperty<long>("MerchantId"); }
            set { setProperty("MerchantId", value); }
        }
        /// <summary>
        /// 商家类型ID
        /// </summary>
        public long MerchantTypeId
        {
            get { return getProperty<long>("MerchantTypeId"); }
            set { setProperty("MerchantTypeId", value); }
        }
        /// <summary>
        /// 商家名称
        /// </summary>
        public string MerchantName
        {
            get { return getProperty<string>("MerchantName"); }
            set { setProperty("MerchantName", value, 50); }
        }
        /// <summary>
        /// 商家简称
        /// </summary>
        public string MerchantShortName
        {
            get { return getProperty<string>("MerchantShortName"); }
            set { setProperty("MerchantShortName", value, 50); }
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
        /// 商家区域
        /// </summary>
        public string MerchantArea
        {
            get { return getProperty<string>("MerchantArea"); }
            set { setProperty("MerchantArea", value, 15); }
        }
        /// <summary>
        /// 商家地址
        /// </summary>
        public string MerchantAddress
        {
            get { return getProperty<string>("MerchantAddress"); }
            set { setProperty("MerchantAddress", value, 15); }
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
