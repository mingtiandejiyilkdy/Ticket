using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ticket.Common;
using Ticket.Enum;

namespace Ticket.ViewModels
{
    public class AdminActionViewModel:BaseViewModel
    {
        /// <summary>
        /// 操作Id
        /// </summary>
        public long ID { get; set; }
        /// <summary>
        /// 关联菜单
        /// </summary>
        public long MenuId { get; set; }        
        /// <summary>
        /// 关联菜单名
        /// </summary>
        public string MenuName { get; set; }
        /// <summary>
        /// 操作关键字
        /// </summary>
        public string ActionKey { get; set; }
        /// <summary>
        /// 操作名称
        /// </summary>
        public string ActionName { get; set; }
        /// <summary>
        /// 状态 0=禁用；1=启用；-1=删除；
        /// </summary>
        public int Status { get; set; } 
        public string StatusStr
        {
            get { return Util.getStatus(Status, typeof(StatusEnum)); }
        }
    }
}
