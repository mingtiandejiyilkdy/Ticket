using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Data.Entity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Ticket.Models
{
    public class LogOnModel 
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        [DisplayName("用户名")]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "用户名必须在6到50位字符间!")]
        public string UserName { get; set; }

        [Required]
        [DisplayName("密  码")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "密码必须在6到50位字符间!")]
        public string Password { get; set; }

        public bool RememberMe { get; set; } 
    }
}
