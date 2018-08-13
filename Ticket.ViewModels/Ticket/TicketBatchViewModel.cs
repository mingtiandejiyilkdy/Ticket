using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ticket.Common;
using Ticket.Enum;

namespace Ticket.ViewModels.Ticket
{
    /// <summary>
    /// 凭据批次
    /// </summary>
    public class TicketBatchViewModel : BaseViewModel
    {
        public long ID { get; set; }
        /// <summary>
        /// 凭据类型
        /// </summary>
        public long TicketTypeId { get; set; }
        /// <summary>
        /// 凭据类型字符串
        /// </summary>
        public string TicketTypeIdStr { get; set; }
        /// <summary>
        /// 批次名称
        /// </summary>
        public string TicketBatchName { get; set; }
        /// <summary>
        /// 批次号
        /// </summary>
        public string TicketBatchNo { get; set; }
        /// <summary>
        /// 批次前缀
        /// </summary>
        public string TicketPrefix { get; set; }
        /// <summary>
        /// 生成张数
        /// </summary>
        public long Count { get; set; }
        /// <summary>
        /// 票据批次生成方式 1=手动创建；2=导入
        /// </summary>
        public int DataFrom { get; set; }
        public string DataFromStr
        {
            get
            {
                return Util.getStatus(Status, typeof(DataFromEnum));
            }
        }
        public int Status { get; set; }
        public string StatusStr
        {
            get
            {
                return Util.getStatus(Status, typeof(StatusEnum));
            }
        }
    }
}
