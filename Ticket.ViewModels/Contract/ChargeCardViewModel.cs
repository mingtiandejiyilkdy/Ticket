using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ticket.Common;
using Ticket.Enum;

namespace Ticket.ViewModels.Contract
{
    public class ChargeCardViewModel : BaseViewModel
    {
        public long ID { get; set; }
        public long CustomId { get; set; }
        public string CustomName { get; set; }
        public long TicketTypeID { get; set; }
        public string TicketTypeName { get; set; }
        public int MoneyType { get; set; }
        public decimal CurrentCount { get; set; }
        public decimal FaceAmount { get; set; }
        public decimal CurrentAmount { get; set; }
        public DateTime ExpireDate { get; set; }
        public long TicketBatchId { get; set; }
        public long TicketStart { get; set; }
        public long TicketEnd { get; set; }
        public int Consumptionlevel { get; set; }
        public int IsCommonCard { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }
        public string StatusStr
        {
            get
            {
                return Util.getStatus(Status, typeof(StatusEnum));
            }
        } 
        public string MoneyTypeypeStr
        {
            get { return Util.getStatus(IsCommonCard, typeof(MoneyTypeEnum)); }
        }

        public string IsCommonCardStr
        {
            get { return Util.getStatus(IsCommonCard, typeof(BaseEnum.WhetherEnum)); }
        } 
    }
}
