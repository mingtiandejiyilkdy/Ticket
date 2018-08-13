using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;  
using Ticket.BLL.Merchant;
using Ticket.Models.Merchant;

namespace Ticket.Website.Controllers.Admin
{

    public class MerchantController : BaseController
    {
        protected readonly MerchantBLL bll = new MerchantBLL();
        protected readonly MerchantTypeBLL merchantTypeBLL = new MerchantTypeBLL();
        // 列表页面 分页
        // GET: /Admin/Merchant/PageList
        [HttpGet]
        public ActionResult PageList()
        {
            return View("Index");
        } 

        //
        // GET: /Admin/Merchant/List
        [HttpPost]
        public JsonResult PageList(int page, int limit)
        {
            return Json(bll.GetPageList(page, limit), JsonRequestBehavior.AllowGet);
        }

        // 待添加
        // GET: /Admin/Merchant/Add
        [HttpGet]
        public ActionResult Add()
        {
            ViewBag.selectTrees = merchantTypeBLL.GetSelectTrees();
            return View("Show");
        }

        // 添加
        // GET: /Admin/Merchant/Add
        [HttpPost]
        public ActionResult Add(MerchantModel model)
        {
            model.ID = 0;
            return Json(bll.Save(model), JsonRequestBehavior.AllowGet);
        }

        // 待编辑
        // GET: /Admin/Merchant/Edit
        [HttpGet]
        public ActionResult Edit(int Id)
        {
            ViewBag.selectTrees = merchantTypeBLL.GetSelectTrees();
            ViewBag.model = bll.GetModelById(Id);
            return View("Show");
        }

        // 编辑
        // GET: /Admin/Merchant/Edit
        [HttpPost]
        public ActionResult Edit(MerchantModel model)
        {
            return Json(bll.Save(model), JsonRequestBehavior.AllowGet);
        }

        // 删除（逻辑删除，状态修改为-1）
        // GET: //Admin/Merchant/Delete
        [HttpPost]
        public ActionResult DeleteById(long[] Ids)
        {
            return Json(bll.SetStatus(Ids, -1), JsonRequestBehavior.AllowGet);
        }

        // 启用
        // GET: /Admin/Merchant/SetEnable
        [HttpPost]
        public ActionResult SetEnable(long[] Ids)
        {
            return Json(bll.SetStatus(Ids,1), JsonRequestBehavior.AllowGet);
        }

        //禁用
        // GET: /Admin/Merchant/SetUnable
        [HttpPost]
        public ActionResult SetUnable(long[] Ids)
        {
            return Json(bll.SetStatus(Ids, 0), JsonRequestBehavior.AllowGet);
        }

    }
}
