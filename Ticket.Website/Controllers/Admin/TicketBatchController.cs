using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ticket.Models.Ticket;
using Ticket.BLL.Ticket;

namespace Ticket.Website.Controllers.Admin
{
    public class TicketBatchController : BaseController
    {
        protected readonly TicketBatchBLL bll = new TicketBatchBLL();
        protected readonly TicketTypeBLL ticketTypeBLL = new TicketTypeBLL();

        // 列表（分页）
        // GET: /Admin/TicketBatch/PageList
        [HttpGet]
        public ActionResult PageList()
        {
            return View("Index");
        }

        // 列表分页
        // GET: /Admin/TicketBatch/PageList
        [HttpPost]
        public JsonResult PageList(int page, int limit)
        {
            return Json(bll.GetPageList(page, limit), JsonRequestBehavior.AllowGet);
        }

        // 待添加
        // GET: /Admin/TicketBatch/Add
        [HttpGet]
        public ActionResult Add()
        {
            ViewBag.selectTrees = ticketTypeBLL.GetSelectTrees();
            return View("Show");
        }

        // 添加
        // GET: /Admin/TicketBatch/Add
        [HttpPost]
        public ActionResult Add(TicketBatchModel model)
        {
            model.ID = 0;
            return Json(bll.Save(model), JsonRequestBehavior.AllowGet);
        }

        // 待编辑
        // GET: /Admin/TicketBatch/Edit
        [HttpGet]
        public ActionResult Edit(int Id)
        {
            ViewBag.selectTrees = ticketTypeBLL.GetSelectTrees();
            ViewBag.model = bll.GetModelById(Id);
            return View("Show");
        }

        // 编辑
        // GET: /Admin/TicketBatch/Edit
        [HttpPost]
        public ActionResult Edit(TicketBatchModel model)
        {
            return Json(bll.Save(model), JsonRequestBehavior.AllowGet);
        } 

        // 删除（逻辑删除，状态修改为-1）
        // GET: /Admin/TicketBatch/Delete/Ids
        [HttpPost]
        public ActionResult Delete(long[] Ids)
        {
            return Json(bll.SetStatus(Ids, -1), JsonRequestBehavior.AllowGet);
        }

        // 启用
        // GET: /Admin/TicketBatch/SetEnable
        [HttpPost]
        public ActionResult SetEnable(long[] Ids)
        {
            return Json(bll.SetStatus(Ids, 1), JsonRequestBehavior.AllowGet);
        }

        //禁用
        // GET: /Admin/TicketBatch/SetUnable
        [HttpPost]
        public ActionResult SetUnable(long[] Ids)
        {
            return Json(bll.SetStatus(Ids, 0), JsonRequestBehavior.AllowGet);
        }

    }
}
