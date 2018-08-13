using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ticket.Common;
using Ticket.Enum;

namespace Ticket.ViewModels
{
    public class AccountViewModel : BaseViewModel
    {
        public long ID { get; set; }
        public long TenantId { get; set; }
        public string AccountName { get; set; }
        public string TrueName { get; set; }
        public int Status { get; set; }
        public DateTime LoginTime { get; set; }
        public DateTime LastLoginTime { get; set; }
        public int LoginCount { get; set; }

        public string StatusStr
        {
            get { return Util.getStatus(Status, typeof(AccountStatusEnum)); }
        }
        public string LoginTimeStr
        {
            get { return LoginTime == Convert.ToDateTime("1900-01-01 00:00:00") ? "未登录" : LoginTime.ToString("yyyy-MM-dd HH:mm:ss"); }
        }
        public string LastLoginTimeStr
        {
            get { return LastLoginTime == Convert.ToDateTime("1900-01-01 00:00:00") ? "未登录" : LastLoginTime.ToString("yyyy-MM-dd HH:mm:ss"); }
        }
    }
}