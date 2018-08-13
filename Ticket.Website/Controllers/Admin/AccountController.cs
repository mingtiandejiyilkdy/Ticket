using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ticket.BLL;
using Ticket.Models;

namespace Ticket.Website.Controllers.Admin
{
    public class AccountController : BaseController
    {
        protected readonly AdminAccountBLL bll = new AdminAccountBLL();
        
        // 列表页面（分页）
        // GET: /Admin/Account/PageList
        [HttpGet]
        public ActionResult PageList()
        {
            return View("Index");
        }

        // 列表方法（分页
        // GET: /Admin/Account/PageList
        [HttpPost]
        public JsonResult PageList(int page, int limit)
        {
            return Json(bll.GetPageList(page, limit), JsonRequestBehavior.AllowGet);
        }

        // 待添加
        // GET: /Admin/Account/Add
        [HttpGet]
        public ActionResult Add()
        {
            return View("~/Views/Admin/Account/Show.cshtml");
        }

        // 添加
        // GET: /Admin/Account/Add
        [HttpPost]
        public ActionResult Add(AdminAccount model)
        {
            model.ID = 0;
            return Json(bll.Save(model), JsonRequestBehavior.AllowGet);
        }

        // 待编辑
        // GET: /Admin/Account/Edit
        [HttpGet]
        public ActionResult Edit(int Id)
        { 
            ViewBag.model = bll.GetModelById(Id);
            return View("~/Views/Admin/Account/Show.cshtml");
        }

        // 编辑
        // GET: /Admin/Account/Edit
        [HttpPost]
        public ActionResult Edit(AdminAccount model)
        {
            return Json(bll.Save(model), JsonRequestBehavior.AllowGet);
        }

        // 删除（物理删除）
        // GET: /Admin/Account/Delete/id
        [HttpPost]
        public ActionResult DeleteById(long Id)
        {
            return Json(bll.DeleteById(Id), JsonRequestBehavior.AllowGet);
        }

        // 删除（逻辑删除，状态修改为-1）
        // GET: /Admin/Account/Delete/Ids
        [HttpPost]
        public ActionResult Delete(long[] Ids)
        {
            return Json(bll.SetStatus(Ids, -1), JsonRequestBehavior.AllowGet);
        }

        // 启用
        // GET: /Admin/Account/SetEnable
        [HttpPost]
        public ActionResult SetEnable(long[] Ids)
        {
            return Json(bll.SetStatus(Ids, 1), JsonRequestBehavior.AllowGet);
        }

        //禁用
        // GET: /Admin/Account/SetUnable
        [HttpPost]
        public ActionResult SetUnable(long[] Ids)
        {
            return Json(bll.SetStatus(Ids, 0), JsonRequestBehavior.AllowGet);
        }  
    }
}
