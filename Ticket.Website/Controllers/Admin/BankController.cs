﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ticket.BLL.Bank;
using Ticket.Models.Bank;

namespace Ticket.Website.Controllers.Admin
{

    public class BankController : BaseController
    {
        protected readonly BankBLL bll = new BankBLL();

        // 列表页面（分页）
        // GET: /Admin/Bank/PageList
        [HttpGet]
        public ActionResult PageList()
        {
            return View("Index");
        }

        // 列表方法（分页）
        // GET: /Admin/Bank/PageList
        [HttpPost]
        public JsonResult PageList(int page, int limit)
        {
            return Json(bll.GetPageList(page, limit), JsonRequestBehavior.AllowGet);
        }


        // 待添加
        // GET: /Admin/Bank/Add
        [HttpGet]
        public ActionResult Add()
        {
            return View("Show");
        }

        // 添加
        // GET: /Admin/Bank/Add
        [HttpPost]
        public ActionResult Add(BankModel model)
        {
            model.ID = 0;
            return Json(bll.Save(model), JsonRequestBehavior.AllowGet);
        }

        // 待编辑
        // GET: /Admin/Bank/Edit
        [HttpGet]
        public ActionResult Edit(int Id)
        {
            ViewBag.model = bll.GetModelById(Id);
            return View("Show");
        }

        // 编辑
        // GET: /Admin/Bank/Edit
        [HttpPost]
        public ActionResult Edit(BankModel model)
        {
            return Json(bll.Save(model), JsonRequestBehavior.AllowGet);
        } 

        // 删除（逻辑删除，状态修改为-1）
        // GET: /Admin/Bank/Delete/Ids
        [HttpPost]
        public ActionResult Delete(long[] Ids)
        {
            return Json(bll.SetStatus(Ids, -1), JsonRequestBehavior.AllowGet);
        }

        // 启用
        // GET: /Admin/Bank/SetEnable
        [HttpPost]
        public ActionResult SetEnable(long[] Ids)
        {
            return Json(bll.SetStatus(Ids, 1), JsonRequestBehavior.AllowGet);
        }

        //禁用
        // GET: /Admin/Bank/SetUnable
        [HttpPost]
        public ActionResult SetUnable(long[] Ids)
        {
            return Json(bll.SetStatus(Ids, 0), JsonRequestBehavior.AllowGet);
        }  

    }
}
