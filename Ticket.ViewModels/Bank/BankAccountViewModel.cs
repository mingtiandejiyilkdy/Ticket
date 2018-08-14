using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ticket.Common;
using Ticket.Enum;

namespace Ticket.ViewModels.Bank
{
    public class BankAccountViewModel : BaseViewModel
    {
        public long ID { get; set; }
        public string BankAccountName { get; set; }
        public long BankId { get; set; }
        public string BankName { get; set; }
        public string BankAccountCode { get; set; } 
        public int Status { get; set; }
         
        public string StatusStr
        {
            get { return Util.getStatus(Status, typeof(BaseEnum.CredentialTypeEnum)); }
        }
    }
}
