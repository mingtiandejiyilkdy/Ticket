using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ticket.BLL.Financial; 
using Ticket.Models.Financial;
using Ticket.BLL.Custom;
using Ticket.BLL.Bank;
using Ticket.BLL.Ticket;
using Ticket.BLL.Contract;

namespace Ticket.Website.Controllers.Admin
{

    public class CustomReceiptedController : BaseController
    {
        protected readonly CustomReceiptedBLL bll = new CustomReceiptedBLL();
        protected readonly ChargeCardBLL _customChargeCardBLL = new ChargeCardBLL();

        // 列表页面（分页）
        // GET: /Admin/CustomReceipted/PageList
        [HttpGet]
        public ActionResult PageList()
        {
            return View("Index");
        }

        // 列表方法（分页）
        // GET: /Admin/CustomReceipted/PageList
        [HttpPost]
        public JsonResult PageList(int page, int limit)
        {
            return Json(bll.GetPageList(page, limit), JsonRequestBehavior.AllowGet);
        }



        // 待添加
        // GET: /Admin/CustomReceipted/Add
        [HttpGet]
        public ActionResult Add()
        {
            ViewBag.selectTrees = new CustomBLL().GetSelectTrees();
            ViewBag.chargeCardNos = _customChargeCardBLL.GetSelectTrees();
            ViewBag.bankAccounts = new BankAccountBLL().GetSelectTrees();
            return View("Show");
        }

        // 添加
        // GET: /Admin/CustomReceipted/Add
        [HttpPost]
        public ActionResult Add(CustomReceiptedModel model)
        {
            model.ID = 0;
            return Json(bll.Save(model), JsonRequestBehavior.AllowGet);
        }

        // 待编辑
        // GET: /Admin/CustomReceipted/Edit
        [HttpGet]
        public ActionResult Edit(int Id)
        {
            ViewBag.selectTrees = new CustomBLL().GetSelectTrees();
            ViewBag.chargeCardNos = _customChargeCardBLL.GetSelectTrees();
            ViewBag.bankAccounts = new BankAccountBLL().GetSelectTrees();
            ViewBag.model = bll.GetModelById(Id);
            return View("Show");
        }

        // 编辑
        // GET: /Admin/Bank/Edit
        [HttpPost]
        public ActionResult Edit(CustomReceiptedModel model)
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
