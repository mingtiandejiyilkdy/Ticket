using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;   
using Ticket.Models.Merchant;
using Ticket.BLL.Merchant;

namespace Ticket.Website.Controllers.Admin
{

    public class MerchantTypeController : BaseController
    {
        protected readonly MerchantTypeBLL bll = new MerchantTypeBLL();
        //
        // GET: /Admin/MerchantType/PageList
        [HttpGet]
        public ActionResult PageList()
        {
            return View("Index");
        } 

        //
        // GET: /Admin/MerchantType/List
        [HttpPost]
        public JsonResult PageList(int page, int limit)
        {
            return Json(bll.GetPageList(page, limit), JsonRequestBehavior.AllowGet);
        }

        // 待添加
        // GET: /Admin/MerchantType/Add
        [HttpGet]
        public ActionResult Add()
        { 
            return View("Show");
        }

        // 添加
        // GET: /Admin/MerchantType/Add
        [HttpPost]
        public ActionResult Add(MerchantTypeModel model)
        {
            model.ID = 0;
            return Json(bll.Save(model), JsonRequestBehavior.AllowGet);
        }

        // 待编辑
        // GET: /Admin/MerchantType/Edit
        [HttpGet]
        public ActionResult Edit(int Id)
        { 
            ViewBag.model = bll.GetModelById(Id);
            return View("Show");
        }

        // 编辑
        // GET: /Admin/MerchantType/Edit
        [HttpPost]
        public ActionResult Edit(MerchantTypeModel model)
        {
            return Json(bll.Save(model), JsonRequestBehavior.AllowGet);
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
