using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ticket.Common;
using Ticket.Enum;

namespace Ticket.ViewModels.Bank
{
    public class BankViewModel : BaseViewModel
    {
        public long ID { get; set; }
        public string BankName { get; set; }
        public int BankType { get; set; }  
        public int Status { get; set; } 

        public string BankTypeStr
        {
            get { return Util.getStatus(BankType, typeof(BankTypeEnum)); }
        }
        public string StatusStr
        {
            get { return Util.getStatus(Status, typeof(BaseEnum.CredentialTypeEnum)); }
        }
    }
}
