using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ticket.BLL;
using Ticket.Models;

namespace Ticket.Website.Controllers.Admin
{
    public class MenuController: BaseController
    {
        protected readonly AdminMenuBLL bll = new AdminMenuBLL();
        // 列表页面（分页）
        // GET: /Admin/Menu/PageList
        [HttpGet]
        public ActionResult PageList()
        {
            return View("Index");
        }

        // 列表数据（分页）
        // GET: /Admin/Menu/PageList
        [HttpPost]
        public JsonResult PageList(int page, int limit)
        {
            return Json(bll.GetPageList(page, limit), JsonRequestBehavior.AllowGet);
        }
        // 待添加
        // GET: /Admin/Action/Add
        [HttpGet]
        public ActionResult Add()
        {
            return View("Show");
        }

        // 添加
        // GET: /Admin/Action/Add
        [HttpPost]
        public ActionResult Add(AdminMenu model)
        {
            model.ID = 0;
            return Json(bll.Save(model), JsonRequestBehavior.AllowGet);
        }

        // 待编辑
        // GET: /Admin/Action/Edit
        [HttpGet]
        public ActionResult Edit(int Id)
        {
            ViewBag.model = bll.GetModelById(Id);
            return View("Show");
        }

        // 编辑
        // GET: /Admin/Action/Edit
        [HttpPost]
        public ActionResult Edit(AdminMenu model)
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

        // 禁用
        // GET: /Admin/Account/SetUnable
        [HttpPost]
        public ActionResult SetUnable(long[] Ids)
        {
            return Json(bll.SetStatus(Ids, 0), JsonRequestBehavior.AllowGet);
        }


        // 获取下拉菜单列表
        // GET: /Admin/Menu/GetMenuTreeSelect
        [HttpGet]
        public JsonResult GetMenuTreeSelect(int Id)
        {
            return Json(bll.GetMenuTreeSelect(Id), JsonRequestBehavior.AllowGet);
        }
    }
}
