using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ticket.Enum;
using Ticket.Common;

namespace Ticket.ViewModels.Ticket
{
    public class TicketViewModel : BaseViewModel
    {
        public long ID { get; set; }
        public long TicketTypeId { get; set; }
        public string TicketCode { get; set; }
        public BaseEnum.ConsumptionlevelEnum Consumptionlevel { get; set; }
        public BaseEnum.MoneyTypeEnum MoneyTyp { get; set; }
        public long CustomID { get; set; }
        public decimal InitialAmount { get; set; }
        public decimal CostAmount { get; set; }
        public decimal Balance { get; set; }
        public BaseEnum.CredentialEnum Status { get; set; }
        public bool IsExpire { get; set; }
        public bool IsActivated { get; set; }
        public string MakeTime { get; set; }
        public string ExpireDate { get; set; }
        public string TicketBatchNo { get; set; }
        public long GrantBy { get; set; }
        public string GrantTime { get; set; }

        public string StatusStr
        {
            get
            {
                return Util.getStatus((int)Status, typeof(BaseEnum.CredentialTypeEnum));
            }
        }
    }
}
