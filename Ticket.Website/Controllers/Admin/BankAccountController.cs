using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ticket.BLL.Bank;
using Ticket.Models.Bank;

namespace Ticket.Website.Controllers.Admin
{

    public class BankAccountController : BaseController
    {
        protected readonly BankAccountBLL bll = new BankAccountBLL();
        protected readonly BankBLL bankBLL = new BankBLL();

        // 列表页面（分页）
        // GET: /Admin/BankAccount/PageList
        [HttpGet]
        public ActionResult PageList()
        {
            return View("Index");
        }

        // 列表方法（分页）
        // GET: /Admin/BankAccount/PageList
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
            ViewBag.selectTrees = bankBLL.GetSelectTrees();
            return View("Show");
        }

        // 添加
        // GET: /Admin/Bank/Add
        [HttpPost]
        public ActionResult Add(BankAccountModel model)
        {
            model.ID = 0;
            return Json(bll.Save(model), JsonRequestBehavior.AllowGet);
        }

        // 待编辑
        // GET: /Admin/Bank/Edit
        [HttpGet]
        public ActionResult Edit(int Id)
        {
            ViewBag.selectTrees = bankBLL.GetSelectTrees();
            ViewBag.model = bll.GetModelById(Id);
            return View("Show");
        }

        // 编辑
        // GET: /Admin/Bank/Edit
        [HttpPost]
        public ActionResult Edit(BankAccountModel model)
        {
            return Json(bll.Save(model), JsonRequestBehavior.AllowGet);
        }          

        // 删除（逻辑删除，状态修改为-1）
        // GET: /Admin/BankAccount/Delete/Ids
        [HttpPost]
        public ActionResult Delete(long[] Ids)
        {
            return Json(bll.SetStatus(Ids, -1), JsonRequestBehavior.AllowGet);
        }

        // 启用
        // GET: /Admin/BankAccount/SetEnable
        [HttpPost]
        public ActionResult SetEnable(long[] Ids)
        {
            return Json(bll.SetStatus(Ids, 1), JsonRequestBehavior.AllowGet);
        }

        //禁用
        // GET: /Admin/BankAccount/SetUnable
        [HttpPost]
        public ActionResult SetUnable(long[] Ids)
        {
            return Json(bll.SetStatus(Ids, 0), JsonRequestBehavior.AllowGet);
        }  

    }
}
