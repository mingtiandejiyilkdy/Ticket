using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ticket.Common;
using Ticket.Enum;

namespace Ticket.ViewModels
{
    public class MenuViewModel : BaseViewModel
    {
        public long ID { get; set; }
        public long ParentID { get; set; }
        public string MenuKey { get; set; }
        public string MenuName { get; set; }
        public string MenuUrl { get; set; }
        public int MenuType { get; set; }  
        public int Sort { get; set; }
        public int Status { get; set; }
        public string StatusStr
        {
            get { return Util.getStatus(Status, typeof(StatusEnum)); }
        }
    }


}
