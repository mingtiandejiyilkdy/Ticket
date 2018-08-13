using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ticket.BLL;
using Ticket.Models;
using Ticket.Common;
using Ticket.ViewModels;

namespace Ticket.Website.Controllers.Admin
{

    public class ActionController : BaseController
    {
        protected readonly AdminActionBLL bll = new AdminActionBLL();
        protected readonly AdminMenuBLL menuBLL = new AdminMenuBLL();
        //
        // GET: /Admin/Action/
        public ActionResult Index()
        {
            return View();
        } 

        //
        // GET: /Admin/Action/List
        [HttpGet]
        public JsonResult List(int page, int limit)
        {
            return Json(bll.GetPageList(page, limit), JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /Admin/Action/Add
        [HttpGet]
        public ActionResult Add()
        {
            ViewBag.selectTrees = menuBLL.GetSelectTrees();
            return View("~/Views/Admin/Action/Show.cshtml");
        }

        //
        // GET: /Admin/Action/Add
        [HttpPost]
        public ActionResult Add(AdminAction model)
        {
            model.ID = 0;
            return Json(bll.Save(model), JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /Admin/Action/Edit
        [HttpGet]
        public ActionResult Edit(int Id)
        {
            ViewBag.selectTrees = menuBLL.GetSelectTrees();
            ViewBag.model = bll.GetModelById(Id);
            return View("~/Views/Admin/Action/Show.cshtml");
        }

        //
        // GET: /Admin/Action/Edit
        [HttpPost]
        public ActionResult Edit(AdminAction model)
        { 
            return Json(bll.Save(model), JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /Action/Delete
        [HttpPost]
        public ActionResult DeleteById(int Id)
        {
            return Json(bll.DeleteById(Id), JsonRequestBehavior.AllowGet);
        }



        // GET: /Admin/Action/SetEnable
        [HttpPost]
        public ActionResult SetEnable(long[] Ids)
        {
            return Json(bll.SetStatus(Ids, 1), JsonRequestBehavior.AllowGet);
        }


        // GET: /Admin/Action/SetUnable
        [HttpPost]
        public ActionResult SetUnable(long[] Ids)
        {
           
            return Json(bll.SetStatus(Ids, 0), JsonRequestBehavior.AllowGet);
        } 


    }
}
