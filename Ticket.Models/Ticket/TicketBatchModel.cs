using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PWMIS.DataMap.Entity;
using Ticket.Interface;

namespace Ticket.Models.Ticket
{
    /// <summary>
    /// 票据批次
    /// </summary>
    public class TicketBatchModel : EntityBase, ILongID
    {
        public TicketBatchModel()
        {
            TableName = "P_TicketBatch";
            IdentityName = "TicketBatchId";
            PrimaryKeys.Add("TicketBatchId");//主键
        }
        /// <summary>
        /// 票据批次Id
        /// </summary>
        public long ID
        {
            get { return getProperty<long>("TicketBatchId"); }
            set { setProperty("TicketBatchId", value); }
        }
        /// <summary>
        /// 票据类型
        /// </summary>
        public long TicketTypeId
        {
            get { return getProperty<long>("TicketTypeId"); }
            set { setProperty("TicketTypeId", value); }
        }
        /// <summary>
        /// 票据批次名称
        /// </summary>
        public string TicketBatchName
        {
            get { return getProperty<string>("TicketBatchName"); }
            set { setProperty("TicketBatchName", value, 50); }
        }
        /// <summary>
        /// 票据批次号
        /// </summary>
        public string TicketBatchNo
        {
            get { return getProperty<string>("TicketBatchNo"); }
            set { setProperty("TicketBatchNo", value, 50); }
        }
        /// <summary>
        /// 票据批次前缀
        /// </summary>
        public string TicketPrefix
        {
            get { return getProperty<string>("TicketPrefix"); }
            set { setProperty("TicketPrefix", value, 50); }
        }
        /// <summary>
        /// 票据批次张数
        /// </summary>
        public long Count
        {
            get { return getProperty<long>("Count"); }
            set { setProperty("Count", value); }
        }
        /// <summary>
        /// 票据批次生成方式 1=手动创建；2=导入
        /// </summary>
        public int DataFrom
        {
            get { return getProperty<int>("DataFrom"); }
            set { setProperty("DataFrom", value); }
        }

        /// <summary>
        /// 是否旧数据
        /// </summary>
        public bool IsOld
        {
            get { return getProperty<bool>("IsOld"); }
            set { setProperty("IsOld", value); }
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
        /// 租户Id
        /// </summary>
        public long TenantId
        {
            get { return getProperty<long>("TenantId"); }
            set { setProperty("TenantId", value); }
        } 
    }
}

