using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ticket.Common;
using Ticket.Enum;

namespace Ticket.ViewModels.Merchant
{
    public class MerchantViewModel : BaseViewModel
    {
        /// <summary>
        /// 商家ID
        /// </summary>
        public long ID { get; set; }
        /// <summary>
        /// 商家类型ID
        /// </summary>
        public long MerchantTypeId { get; set; }
        /// <summary>
        /// 商家类型
        /// </summary>
        public string MerchantTypeName { get; set; }
        /// <summary>
        /// 商家名称
        /// </summary>
        public string MerchantName { get; set; }
        /// <summary>
        /// 商家简称
        /// </summary>
        public string MerchantShortName { get; set; }
        /// <summary>
        /// 联系人
        /// </summary>
        public string LinkName { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string LinkPhone { get; set; }
        /// <summary>
        /// 联系手机
        /// </summary>
        public string LinkMobile { get; set; }
        /// <summary>
        /// 商家区域
        /// </summary>
        public string MerchantArea { get; set; }
        /// <summary>
        /// 商家地址
        /// </summary>
        public string MerchantAddress { get; set; }
        public int Status { get; set; }
        public string StatusStr
        {
            get { return Util.getStatus(Status, typeof(MerchantStatusEnum)); }
        }
    }
}
