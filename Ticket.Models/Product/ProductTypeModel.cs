﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PWMIS.DataMap.Entity;
using Ticket.Interface;

namespace Ticket.Models.Product
{
    /// <summary>
    /// 产品类型信息
    /// </summary>
    public class ProductTypeModel : EntityBase, ILongID
    {
        public ProductTypeModel()
        {
            TableName = "P_ProductType";
            IdentityName = "ProductTypeId";
            PrimaryKeys.Add("ProductTypeId");//主键
        }
        /// <summary>
        /// 产品类型ID
        /// </summary>
        public long ID
        {
            get { return getProperty<long>("ProductTypeId"); }
            set { setProperty("ProductTypeId", value); }
        }
        /// <summary>
        /// 行业类型
        /// </summary>
        public long MerchantTypeId
        {
            get { return getProperty<long>("MerchantTypeId"); }
            set { setProperty("MerchantTypeId", value); }
        }
        /// <summary>
        /// 产品类型名称
        /// </summary>
        public string ProductTypeName
        {
            get { return getProperty<string>("ProductTypeName"); }
            set { setProperty("ProductTypeName", value, 50); }
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
