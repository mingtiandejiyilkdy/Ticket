using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ticket.ViewModels
{
    public class BaseViewModel
    {
        public string Remark { get; set; } 
        public int Sort { get; set; }
        public long CreateId { get; set; }
        public string CreateUser { get; set; }
        public string CreateIP { get; set; }
        public DateTime CreateTime { get; set; }
        public string UpdateBy { get; set; }
        public string UpdateIP { get; set; }
        public DateTime? UpdateTime { get; set; }
        public string CreateTimeStr
        {
            get { return CreateTime.ToString("yyyy-MM-dd HH:mm:ss"); }
        }
        public string UpdateTimeStr
        {
            get { return UpdateTime == Convert.ToDateTime("1900-01-01 00:00:00") ? "" : Convert.ToDateTime(UpdateTime).ToString("yyyy-MM-dd HH:mm:ss"); }
        }
    }
}
