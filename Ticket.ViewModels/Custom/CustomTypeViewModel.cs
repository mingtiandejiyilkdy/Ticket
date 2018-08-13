using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ticket.Common;
using Ticket.Enum;

namespace Ticket.ViewModels.Custom
{
    public class CustomTypeViewModel : BaseViewModel
    {
        public long ID { get; set; }
        public string CustomTypeName { get; set; }

        public int Status { get; set; } 
        public string StatusStr
        {
            get { return Util.getStatus(Status, typeof(CustomTypeStatusEnum)); }
        }
    }
}
