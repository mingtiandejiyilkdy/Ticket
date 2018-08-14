using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ticket.BLL.Financial;
using Ticket.Models.Financial;

namespace Ticket.Website.Controllers.Admin
{

    public class CustomAccReceiptController : BaseController
    {
        protected readonly CustomAccReceiptBLL bll = new CustomAccReceiptBLL();

        // 列表页面（分页）
        // GET: /Admin/CustomAccReceipt/PageList
        [HttpGet]
        public ActionResult PageList()
        {
            return View("Index");
        }

        // 列表方法（分页）
        // GET: /Admin/CustomAccReceipt/PageList
        [HttpPost]
        public JsonResult PageList(int page, int limit)
        {
            return Json(bll.GetPageList(page, limit), JsonRequestBehavior.AllowGet);
        } 
    }
}
