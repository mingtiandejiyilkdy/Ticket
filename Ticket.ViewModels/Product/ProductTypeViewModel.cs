using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ticket.Common;
using Ticket.Enum;

namespace Ticket.ViewModels.Product
{ 
    /// <summary>
    /// 产品类型信息
    /// </summary>
    public class ProductTypeViewModel : BaseViewModel
    { 
        /// <summary>
        /// 产品类型ID
        /// </summary>
        public long ID { get; set; }
        /// <summary>
        /// 行业类型
        /// </summary>
        public long MerchantTypeId { get; set; }
        /// <summary>
        /// 行业类型
        /// </summary>
        public string MerchantTypeName { get; set; }
        /// <summary>
        /// 产品类型名称
        /// </summary>
        public string ProductTypeName{ get;set; } 
        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }
        public string StatusStr
        {
            get { return Util.getStatus(Status, typeof(StatusEnum)); }
        }
    }
}
