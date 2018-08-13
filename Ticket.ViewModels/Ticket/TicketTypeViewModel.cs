﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ticket.Common;
using Ticket.Enum;

namespace Ticket.ViewModels.Ticket
{
    public class TicketTypeViewModel : BaseViewModel
    {
        public long ID { get; set; }
        public string TicketTypeName { get; set; }
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
