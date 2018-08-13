using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ticket.Models.Ticket;
using Ticket.BLL.Ticket;

namespace Ticket.Website.Controllers.Admin
{

    public class TicketTypeController : BaseController
    {
        protected readonly TicketTypeBLL bll = new TicketTypeBLL();

        // 列表（分页）
        // GET: /Admin/TicketType/PageList
        [HttpGet]
        public ActionResult PageList()
        {
            return View("Index");
        } 

        // 列表分页
        // GET: /Admin/TicketType/PageList
        [HttpPost]
        public JsonResult PageList(int page, int limit)
        {
            return Json(bll.GetPageList(page, limit), JsonRequestBehavior.AllowGet);
        }

        // 待添加
        // GET: /Admin/TicketType/Add
        [HttpGet]
        public ActionResult Add()
        {
            return View("Show");
        }

        // 添加
        // GET: /Admin/TicketType/Add
        [HttpPost]
        public ActionResult Add(TicketTypeModel model)
        {
            model.ID = 0;
            return Json(bll.Save(model), JsonRequestBehavior.AllowGet);
        }

        // 待编辑
        // GET: /Admin/TicketType/Edit
        [HttpGet]
        public ActionResult Edit(int Id)
        {
            ViewBag.model = bll.GetModelById(Id);
            return View("Show");
        }

        // 编辑
        // GET: /Admin/TicketType/Edit
        [HttpPost]
        public ActionResult Edit(TicketTypeModel model)
        {
            return Json(bll.Save(model), JsonRequestBehavior.AllowGet);
        }

        // 删除（物理删除）
        // GET: /Admin/TicketType/Delete/id
        [HttpPost]
        public ActionResult DeleteById(long Id)
        {
            return Json(bll.DeleteById(Id), JsonRequestBehavior.AllowGet);
        }

        // 删除（逻辑删除，状态修改为-1）
        // GET: /Admin/TicketType/Delete/Ids
        [HttpPost]
        public ActionResult Delete(long[] Ids)
        {
            return Json(bll.SetStatus(Ids, -1), JsonRequestBehavior.AllowGet);
        }

        // 启用
        // GET: /Admin/TicketType/SetEnable
        [HttpPost]
        public ActionResult SetEnable(long[] Ids)
        {
            return Json(bll.SetStatus(Ids, 1), JsonRequestBehavior.AllowGet);
        }

        //禁用
        // GET: /Admin/TicketType/SetUnable
        [HttpPost]
        public ActionResult SetUnable(long[] Ids)
        {
            return Json(bll.SetStatus(Ids, 0), JsonRequestBehavior.AllowGet);
        }  

    }
}
