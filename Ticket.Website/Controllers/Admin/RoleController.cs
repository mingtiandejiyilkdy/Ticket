using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ticket.BLL;
using Ticket.Models;
using Ticket.Common;

namespace Ticket.Website.Controllers.Admin
{

    public class RoleController : BaseController
    {
        protected readonly AdminRoleBLL bll = new AdminRoleBLL();
        protected readonly AdminAccountBLL accountbll = new AdminAccountBLL();
        //
        // GET: /Admin/Role/PageList
        [HttpGet]
        public ActionResult PageList()
        {
            return View("Index");
        } 

        //
        // GET: /Admin/Role/PageList
        [HttpPost]
        public JsonResult PageList(int page, int limit)
        {
            return Json(bll.GetPageList(page, limit), JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /Admin/Role/Show
        public ActionResult Add()
        {
            return View("~/Views/Admin/Role/Show.cshtml");
        }

        //
        // GET: /Admin/Role/Add
        [HttpPost]
        public ActionResult Add(AdminRole model, int[] itemIds)
        {
            model.ID = 0;
            if (itemIds != null)
            {
                model.MenuIds = string.Join(",", itemIds);
            }
            return Json(bll.Save(model), JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /Admin/Role/Show
        public ActionResult Edit(int Id)
        {
            ViewBag.model = bll.GetModelById(Id);
            return View("~/Views/Admin/Role/Show.cshtml");
        }

        //
        // GET: /Admin/Action/Edit
        [HttpPost]
        public ActionResult Edit(AdminRole model, int[] itemIds)
        {
            if (itemIds != null)
            {
                model.MenuIds = string.Join(",", itemIds);
            }
            return Json(bll.Save(model), JsonRequestBehavior.AllowGet);
        }   

        //
        // GET: /AccountAdmin/Delete/Ids
        [HttpPost]
        public ActionResult Delete(long[] Ids)
        {
            return Json(bll.SetStatus(Ids, -1), JsonRequestBehavior.AllowGet);
        }

        // GET: /Admin/Custom/IsEnable
        [HttpPost]
        public ActionResult SetEnable(long[] Ids)
        {
            return Json(bll.SetStatus(Ids, 1), JsonRequestBehavior.AllowGet);
        }


        // GET: /Admin/Custom/SetUnable
        [HttpPost]
        public ActionResult SetUnable(long[] Ids)
        {
            return Json(bll.SetStatus(Ids, 0), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 分配角色
        /// </summary>
        /// <returns></returns>
        // GET: /Admin/Role/SetRole
        public ActionResult SetRole(int Id)
        {
            AdminAccount account = accountbll.GetModelById(Id);
            ViewBag.model = account;
            return View();
        }

        //
        // GET: /Admin/Role/GetRoleTreeSelect
        [HttpGet]
        public JsonResult GetRoleTreeSelect(int Id)
        {
            return Json(bll.GetRoleTreeSelect(Id), JsonRequestBehavior.AllowGet);
        }


        //
        // GET: /Admin/Role/AccountRoleSave
        [HttpPost]
        public ActionResult AccountRoleSave(AdminAccount model, int[] itemIds)
        {
            return Json(bll.SaveAccountRole(model, itemIds), JsonRequestBehavior.AllowGet);
        }
    }
}
